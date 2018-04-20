using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using ESS.FW.Common.Utilities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic;
using ESS.FW.DataAccess.EF.GraphDiff;

namespace ESS.FW.DataAccess.EF
{
    public partial class EfRepository<TEntity, TKey> : BaseRepository<TEntity, TKey> where TEntity : class, new()
    {
        //readonly string _connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
        private static readonly object _lockObj = new object();
        protected readonly DbContext DbContext;
        protected IQueryable<TEntity> DbQuery;
        protected DbSet<TEntity> DbSet;


        //protected readonly IUnitOfWork UnitOfWork;
        public override string KeyField { get;  set; } = "PK";
        public override string VersionField { get; set; } = "Version";

            
        public EfRepository(DbContext dbContext, ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
            DbQuery = DbSet;
        }
        


        public override TEntity Add(TEntity entity)
        {

            return DbSet.Add(entity).Entity;
        }

        public override TEntity Update(TEntity entity)
        {
            var entry = DbContext.Entry(entity);


            if (entry.State == EntityState.Detached)
            {
                var en = DbSet.Find(GetKeyValue(entry));
                if (en == null)
                    Ensure.NotNull(en, "update entity");

                DbContext.Entry(en).CurrentValues.SetValues(entity);
                return en;
            }

            return entity;
        }

        public override TEntity Update(TEntity entity, Expression<Func<IUpdateConfiguration<TEntity>, object>> childMap)
        {
            return DbContext.UpdateGraph(entity, childMap);
        }

        public override TEntity Update(TEntity entity, string[] childMap)
        {
            return DbContext.UpdateGraph(entity, childMap);
        }
        public override TEntity AddOrUpdate(TEntity entity,
            Expression<Func<IUpdateConfiguration<TEntity>, object>> childMap = null)
        {

            var id = (long?)GetKeyValue(DbContext.Entry(entity)).FirstOrDefault();
            if (id > 0)
            {
                if (childMap != null)
                {
                    //DbContext.SetLastModifyTime(DbContext.Entry(entry));

                    return DbContext.UpdateGraph(entity, childMap);
                }
                return Update(entity);
            }
            return Add(entity);
        }

        public override void Delete(TKey id)
        {
            var entity = Get(id);
            if (entity != null)
                DbContext.Entry(entity).State = EntityState.Deleted;
        }

        public override void Delete(TEntity entity)
        {
            var state = DbContext.Entry(entity).State;
            if (state == EntityState.Detached)
                DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        public override void Delete<TElement>(TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> childPath)
        {
            throw new NotImplementedException();
            //var existingParent = DbSet.Find(GetKeyValue(DbContext.Entry(entity)));
            //DbContext.Entry(existingParent).Collection(childPath).Load();
            //DbContext.Entry(existingParent).State = EntityState.Deleted;

            //// Delete children
            //foreach (
            //    var existingChild in DbContext.Entry(existingParent).Collection(childPath).CurrentValue.ToList())
            //{
            //    DbContext.Entry(existingChild).State = EntityState.Deleted;
            //}
        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = DbSet.Where(predicate);
            DbSet.RemoveRange(entities);
        }

        public override void DeleteAll()
        {
            DbSet.RemoveRange(DbSet);
        }

        /// <summary>
        ///     不支持include
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override TEntity Get(TKey id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        ///     复合主键查询
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public override TEntity Get(params object[] keyValues)
        {
            return DbSet.Find(keyValues);
        }

        public override IQueryable<TEntity> GetAll()
        {
            //return DbQuery;
            return DbQuery.AsNoTracking();
        }

        public override IQueryable<TEntity> GetAllPaged(int page, int pageSize, string orderBy = "")
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "PK";
            }
            return DbQuery.OrderBy(orderBy).Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking();
        }

        public override IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbQuery.Where(predicate);
        }

        public override IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool isTrack = true)
        {
            if (isTrack) return Find(predicate);
            return DbQuery.AsNoTracking().Where(predicate);
        }

        public override IQueryable<TEntity> FindPaged(int page, int pageSize, Expression<Func<TEntity, bool>> predicate,
            string orderBy = "")
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "PK";
            }
            return DbQuery.Where(predicate).OrderBy<TEntity>(orderBy).Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking();
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return DbQuery.SingleOrDefault(predicate);
        }

        public override TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return DbQuery.FirstOrDefault(predicate);
        }
        public override TEntity First(Expression<Func<TEntity, bool>> predicate, bool isTrack = true)
        {
            if (isTrack) return First(predicate);
            return DbQuery.AsNoTracking().FirstOrDefault(predicate);
        }

        public override int Count()
        {
            return DbQuery.Count();
        }

        public override int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbQuery.Count(predicate);
        }

        /// <summary>
        ///     不能级联，要不然无法进行intercept
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="path"></param>
        public override void Include<TProperty>(Expression<Func<TEntity, TProperty>> path)
        {
            DbQuery = DbQuery.Include(path);
        }

        /// <summary>
        ///     不能级联，要不然无法进行intercept
        /// </summary>
        /// <param name="path"></param>
        public override void Include(string path)
        {
            DbQuery = DbQuery.Include(path);
        }
        
        

#region private property

#endregion

#region private method

        private static readonly ConcurrentDictionary<Type, string[]> KeyFields =
            new ConcurrentDictionary<Type, string[]>();

        private object[] GetKeyValue(EntityEntry entry)
        {
            IEnumerable<string> keys;
            if (KeyFields.ContainsKey(typeof(TEntity)))
            {
                keys = KeyFields[typeof(TEntity)];
            }
            else
            {
                keys = (DbContext).GetPrimaryKeyFieldsFor(typeof(TEntity)).Select(c => c.Name);
                if (keys != null || keys.Count() >= 0)
                {
                    KeyFields.TryAdd(typeof(TEntity), keys.ToArray());
                }
            }

            return keys.Select(k => entry.Property(k).CurrentValue).ToArray();
        }
#endregion
    }

    
}
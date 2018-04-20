using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text.RegularExpressions;
using ESS.FW.Common.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Configuration = ESS.FW.Common.Configurations.Configuration;

namespace ESS.FW.DataAccess.EF
{
    /// <summary>
    ///     configuration class ef data extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        ///     Use EF to implement the repository.
        ///     dbModelBuilder默认为Null时扫描整个程序集
        /// </summary>
        /// <returns></returns>

        public static Configuration UseEfRepository(this Configuration configuration, Type dbContextType,
             ModelBuilder dbModelBuilder = null)
        {

            //CreateModel(dbContextType, dbModelBuilder);

            ObjectContainer.Current.RegisterGeneric(typeof(IReadRepository<,>), typeof(EfRepository<,>),
                LifeStyle.InstancePerRequest);
            ObjectContainer.Current.RegisterGeneric(typeof(IRepository<,>), typeof(EfRepository<,>),
                LifeStyle.InstancePerRequest);
            
            return configuration;
        }

        ///// <summary>
        /////     手动初始化ef，防止并发错误
        ///// </summary>
        //private static void CreateModel(Type dbContextType, ModelBuilder dbModelBuilder)
        //{
        //    var modelIsNull = false;
        //    if (dbModelBuilder == null)
        //    {
        //        modelIsNull = true;
        //        dbModelBuilder = new ModelBuilder(new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet());
        //    }
            

        //    ObjectContainer.RegisterInstance<ModelBuilder, ModelBuilder>(complieModel);
        //    ObjectContainer.RegisterType(typeof(DbContext), dbContextType, LifeStyle.InstancePerRequest);
        //}
    }
    
}
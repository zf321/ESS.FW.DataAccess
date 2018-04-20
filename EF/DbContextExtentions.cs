using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Xml.Linq;

namespace ESS.FW.DataAccess.EF
{
    public static partial class DbContextExtentions
    {
        public static string GetTableName<T>(this DbContext context) where T : class
        {
            var mapping = context.Model.FindEntityType(typeof(T)).Relational();
            var schema = mapping.Schema;
            var tableName = mapping.TableName;
            return tableName;
        }

        public static string GetTableName(this DbContext context, Type t)
        {
            var mapping = context.Model.FindEntityType(t).Relational();
            var schema = mapping.Schema;
            var tableName = mapping.TableName;
            return tableName;
        }
        

        private static readonly Dictionary<string, string> TableNames = new Dictionary<string, string>();
        
        
        public static IEnumerable<PropertyInfo> GetPrimaryKeyFieldsFor(this DbContext context, Type entityType)
        {
            var metadata = context.Model
                    .FindEntityType(entityType).FindPrimaryKey().Properties;

            if (metadata == null)
            {
                throw new InvalidOperationException(String.Format("The type {0} is not known to the DbContext.", entityType.FullName));
            }

            return metadata.Select(k => entityType.GetProperty(k.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)).ToList();
        }
        public static IEnumerable<INavigation> GetRequiredNavigationPropertiesForType(this DbContext context, Type entityType)
        {
            return context.Model.FindEntityType(entityType).GetNavigations();
        }
        public static IEnumerable<INavigation> GetNavigationPropertiesForType(this DbContext context, Type entityType)
        {
            return context.Model.FindEntityType(entityType).GetNavigations();
        }
        
    }
}

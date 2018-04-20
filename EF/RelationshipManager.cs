//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using System.Reflection;

//namespace ESS.FW.DataAccess.EF
//{

//    public static class RelationshipManager
//    {
//        static Dictionary<Type, List<Relation>> _relations = new Dictionary<Type, List<Relation>>();

//        public static List<Relation> GetEntityProperties(Type entityType)
//        {
//            if (_relations.ContainsKey(entityType))
//                return _relations[entityType];
//            else
//                return null;
//        }
//        public static bool Contain(Type entityType)
//        {
//            return _relations.ContainsKey(entityType);
//        }

//        public static void Register(DbContext context, Type entityType)
//        {
//            if (_relations.ContainsKey(entityType))
//                return;
//            List<string> alreadyHave = new List<string>();
//            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
//            var relation = GetProperty(alreadyHave, metadata, entityType);
//            if (relation != null)
//                _relations.Add(entityType, relation);
//        }

//        static List<Relation> GetProperty(List<string> alreadyHave, MetadataWorkspace metadata, Type entityType)
//        {
//            var entity = metadata.GetItems<EntityType>(DataSpace.CSpace).FirstOrDefault(c => c.Name == entityType.Name);
//            List<Relation> result = new List<Relation>();

//            if (entity == null) return result;

//            foreach (var prop in entity.NavigationProperties)
//            {
//                Relation relation = new Relation();
//                relation.PropertyName = prop.Name;
//                Type propertyType = entityType.GetProperty(prop.Name, BindingFlags.Public | BindingFlags.Instance)?.PropertyType;
//                AssociationType association = prop.RelationshipType as AssociationType;

//                if (association.ReferentialConstraints[0].ToRole.DeclaringType.Name == prop.RelationshipType.Name)
//                {
//                    relation.RelationKeyField = association.ReferentialConstraints[0].ToProperties[0].Name;
//                }
//                if (prop.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
//                {
//                    relation.IsCollectionProperty = true;
//                    propertyType = propertyType.GenericTypeArguments.FirstOrDefault();
//                }
//                relation.MainEntity = propertyType;
//                if (alreadyHave.Contains(propertyType.Name))
//                    continue;

//                alreadyHave.Add(propertyType.Name);
//                var children = GetProperty(alreadyHave, metadata, propertyType);
//                relation.ChildrenEntity.AddRange(children);

//                result.Add(relation);
//            }

//            return result;
//        }
//    }

//    public class Relation
//    {
//        public Relation()
//        {
//            this.IsCollectionProperty = false;
//            ChildrenEntity = new List<Relation>();
//        }

//        public Relation(Type entityType)
//            : this()
//        {
//            this.MainEntity = entityType;
//        }

//        public Type MainEntity { get; set; }

//        public string PropertyName { get; set; }

//        public string RelationKeyField { get; set; }

//        public bool IsCollectionProperty { get; set; }

//        public List<Relation> ChildrenEntity { get; set; }
//    }
//}

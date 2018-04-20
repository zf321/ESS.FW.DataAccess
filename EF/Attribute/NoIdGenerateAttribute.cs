//using System.Data.Entity.Core.Metadata.Edm;
//using System.Linq;

//namespace ESS.FW.DataAccess.EF.Attribute
//{
//    /// <summary>
//    ///     ef 不要设置从表主建
//    /// </summary>
//    public class NoIdGenerateAttribute : System.Attribute
//    {
//        public static bool IsIdGenerate(EdmType type)
//        {
//            MetadataProperty annotation = (type.MetadataProperties.GetValue("KeyMembers", true).Value as
//                ReadOnlyMetadataCollection<EdmMember>)?.FirstOrDefault()?.MetadataProperties
//                .SingleOrDefault(p => p.Name.EndsWith("NoIdGenerate"));

//            return annotation != null;
//        }
//    }
//}
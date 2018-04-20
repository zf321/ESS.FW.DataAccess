//using System.Linq;

//namespace ESS.FW.DataAccess.EF.Attribute
//{
//    /// <summary>
//    ///     ef 设置从表主建
//    /// </summary>
//    public class IdGenerateAttribute : System.Attribute
//    {
//        public static bool IsIdGenerate(EdmType type)
//        {
//            MetadataProperty annotation = (type.MetadataProperties.GetValue("KeyMembers", true).Value as
//                ReadOnlyMetadataCollection<EdmMember>)?.FirstOrDefault()?.MetadataProperties
//                .SingleOrDefault(p => p.Name.EndsWith("IdGenerate"));

//            return annotation != null;
//        }
//    }
//}
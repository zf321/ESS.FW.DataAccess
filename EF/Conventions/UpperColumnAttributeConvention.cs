//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.ModelConfiguration.Configuration;
//using System.Data.Entity.ModelConfiguration.Conventions;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ESS.FW.DataAccess.EF.Conventions
//{
//    /// <summary>
//    /// 表示用于处理模型中属性上找到的 <see cref="T:System.ComponentModel.DataAnnotations.Schema.ColumnAttribute"/> 实例的约定。
//    /// </summary>
//    public class UpperColumnAttributeConvention : ColumnAttributeConvention
//    {
//        /// <summary>
//        /// 将约定应用到指定配置。
//        /// </summary>
//        /// <param name="configuration">配置。</param><param name="attribute">列属性。</param>
//        public override void Apply(ConventionPrimitivePropertyConfiguration configuration, ColumnAttribute attribute)
//        {
//            if (!string.IsNullOrWhiteSpace(attribute.Name))
//                configuration.HasColumnName(attribute.Name.ToUpper());
//            if (!string.IsNullOrWhiteSpace(attribute.TypeName))
//                configuration.HasColumnType(attribute.TypeName);
//            if (attribute.Order < 0)
//                return;
//            configuration.HasColumnOrder(attribute.Order);
//        }
//    }
//}

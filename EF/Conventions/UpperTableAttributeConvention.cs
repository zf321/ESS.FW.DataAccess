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
//    /// 表示用于处理在模型中类型上发现的 <see cref="T:System.ComponentModel.DataAnnotations.Schema.TableAttribute"/> 实例的约定。
//    /// </summary>
//    public class UpperTableAttributeConvention : TableAttributeConvention
//    {
//        /// <summary>
//        /// 将此约定应用于属性。
//        /// </summary>
//        /// <param name="configuration">具有该属性 (Attribute) 的属性 (Property) 的配置。</param><param name="attribute">属性。</param>
//        public override void Apply(ConventionTypeConfiguration configuration, TableAttribute attribute)
//        {
//            if (string.IsNullOrWhiteSpace(attribute.Schema))
//                configuration.ToTable(attribute.Name.ToUpper());
//            else
//                configuration.ToTable(attribute.Name.ToUpper(), attribute.Schema.ToUpper());
//        }
//    }
//}

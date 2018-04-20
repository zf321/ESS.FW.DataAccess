//using System;
//using System.Collections.Generic;
//using System.Data.Entity.ModelConfiguration.Conventions;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ESS.FW.DataAccess.EF.Conventions
//{
//    /// <summary>
//    /// 表示用于处理模型中属性上找到的 实例的约定。
//    /// </summary>
//    public class UpperProperyConvention : Convention
//    {
//        public UpperProperyConvention()
//        {
//            this.Properties().Configure(c =>
//            {
//                var name = c.ClrPropertyInfo.Name;
//                var newName = name.ToUpper();
//                c.HasColumnName(newName);
//            });
//        }
//    }
//}

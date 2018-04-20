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
//    /// string 转换成 un unicode，解决查询慢的问题
//    /// </summary>
//    public class StringUnicodeProperyConvention : Convention
//    {
//        public StringUnicodeProperyConvention()
//        {
//            this.Properties<string>().Configure(c =>
//            {
//                    c.IsUnicode(false);
//            });
//        }
//    }
//}

using System;

namespace ESS.FW.DataAccess
{
    /// <summary>
    /// 获取oracle seq
    /// </summary>
    public interface IIdGenerator
    {
        long NewId(Type type);
    }
}

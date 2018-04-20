using Microsoft.EntityFrameworkCore;
using System;

namespace ESS.FW.DataAccess.EF.Attribute
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class InterceptorMethodAttribute : System.Attribute
    {
        public InterceptorMethodAttribute(EntityState state, bool isPostSave)
        {
            State = state;
            IsPostSave = isPostSave;
        }

        public EntityState State { get; private set; }

        public bool IsPostSave { get; private set; }
    }
}
using System;
using System.Web.Mvc;

namespace French.Tools.Library
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class NoCacheAttribute : OutputCacheAttribute
    {
        public NoCacheAttribute()
        {
            Duration = 1;
            VaryByParam = "*";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCore.DI
{
    /// <summary>
    /// 依赖属性注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Property | AttributeTargets.Method,  AllowMultiple = false)]
    public class InjectionAttribute : Attribute { }
}

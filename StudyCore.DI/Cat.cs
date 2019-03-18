using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StudyCore.DI
{
    public class Cat
    {
        /// <summary>
        /// ConcurrentDictionary 多线程中安全
        /// </summary>
        private ConcurrentDictionary<Type, Type> typeMapping = new ConcurrentDictionary<Type, Type>();

        /// <summary>
        /// 注册接口服务
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void Register(Type from, Type to)
        {
            typeMapping[from] = to;
        }

        /// <summary>
        /// 获取接口服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            Type type;

            if (!typeMapping.TryGetValue(serviceType, out type))
            {
                type = serviceType;
            }
            if (type.IsInterface || type.IsAbstract)
            {
                return null;
            }

            ConstructorInfo constructor = this.GetConstructor(type);
            if (null == constructor)
            {
                return null;
            }

            object[] arguments = constructor.GetParameters().Select(p => this.GetService(p.ParameterType)).ToArray();
            object service = constructor.Invoke(arguments);
            this.InitializeInjectedProperties(service);
            this.InvokeInjectedMethods(service);
            return service;
        }

        /// <summary>
        /// 获取类的构造函数，优先选择带依赖注入特性的函数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            return constructors.FirstOrDefault(c => c.GetCustomAttribute<InjectionAttribute>() != null)
                ?? constructors.FirstOrDefault();
        }

        /// <summary>
        /// 初始化需要依赖注入的属性
        /// </summary>
        /// <param name="service"></param>
        protected virtual void InitializeInjectedProperties(object service)
        {
            PropertyInfo[] properties = service.GetType().GetProperties()
                 .Where(p => p.CanWrite && p.GetCustomAttribute<InjectionAttribute>() != null)
                .ToArray();
            Array.ForEach(properties, p => p.SetValue(service, this.GetService(p.PropertyType)));
        }

        /// <summary>
        /// 调用需要依赖注入的方法
        /// </summary>
        /// <param name="service"></param>
        protected virtual void InvokeInjectedMethods(object service)
        {
            MethodInfo[] methods = service.GetType().GetMethods()
                   .Where(m => m.GetCustomAttribute<InjectionAttribute>() != null)
                  .ToArray();
            Array.ForEach(methods, m =>
            {
                object[] arguments = m.GetParameters().Select(p => this.GetService(p.ParameterType)).ToArray();
                m.Invoke(service, arguments);
            });
        }

        public void Register<T1, T2>()
        {
            Register(typeof(T1), typeof(T2));
        }
        public T GetService<T>() where T : class
        {
            return this.GetService(typeof(T)) as T;
        }
    }
}

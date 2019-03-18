using Microsoft.Extensions.Configuration;
using Sduty.ConsoleTest.Configuration;
using Sduty.ConsoleTest.Redis;
using Sduty.ConsoleTest.SeviceProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sduty.ConsoleTest
{
    class Program
    {
        /* DI 容器测试
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection()
                .AddSingleton<IFoo, Foo>()
                .AddSingleton<IBar>(new Bar())
                .AddSingleton<IBaz>(b => new Baz())
                .AddSingleton<IGux, Gux>();
            
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            
        
            Console.WriteLine("serviceProvider.GetService<IFoo>(): {0}", serviceProvider.GetService<IFoo>());
            Console.WriteLine("serviceProvider.GetService<IBar>(): {0}", serviceProvider.GetService<IBar>());
            Console.WriteLine("serviceProvider.GetService<IBaz>(): {0}", serviceProvider.GetService<IBaz>());
            Console.WriteLine("serviceProvider.GetService<IGux>(): {0}", serviceProvider.GetService<IGux>());
            
            Console.ReadKey();
        }
        */
        /// <summary>
        /// core的 configuration 配置调用
        /// </summary>
        ///<param name="args"></param>
        static void Main(string[] args)
        {
            RedisCacheHelper.Add<string>("mystring", "123456", new TimeSpan(10000));

            var redis = RedisCacheHelper.Get<string>("mystring");

            Console.WriteLine(redis);

            Console.ReadKey();
        }
    }

    
}

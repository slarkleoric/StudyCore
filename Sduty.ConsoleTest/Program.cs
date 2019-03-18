﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Sduty.ConsoleTest.Configuration;
using Sduty.ConsoleTest.Redis;
using Sduty.ConsoleTest.SeviceProvider;
using System;
using System.Collections.Generic;
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
            //Dictionary<string, string> source = new Dictionary<string, string>
            //{
            //    ["LongDatePattern"] = "dddd, MMMM d, yyyy",
            //    ["LongTimePattern"] = "h:mm:ss tt",
            //    ["ShortDatePattern"] = "M/d/yyyy",
            //    ["ShortTimePattern"] = "h:mm tt"
            //};
            //IConfiguration configuration = new ConfigurationBuilder()
            //        .Add(new MemoryConfigurationSource() { InitialData = source })
            //        .Build();

            //DateTimeFormatSettings settings = new DateTimeFormatSettings(configuration);
            //Console.WriteLine("{0,-16}: {1}", "LongDatePattern", settings.LongDatePattern);
            //Console.WriteLine("{0,-16}: {1}", "LongTimePattern", settings.LongTimePattern);
            //Console.WriteLine("{0,-16}: {1}", "ShortDatePattern", settings.ShortDatePattern);
            //Console.WriteLine("{0,-16}: {1}", "ShortTimePattern", settings.ShortTimePattern);



            var redis = RedisCacheHelper.Get<string>("mystring");

            Console.ReadKey();
        }
    }
}

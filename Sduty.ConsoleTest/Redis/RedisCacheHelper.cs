﻿
using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sduty.ConsoleTest.Redis
{
    public class RedisCacheHelper
    {
        private static readonly PooledRedisClientManager pool = null;
        private static readonly string[] redisHosts = null;
        public static int RedisMaxReadPool = 3;//int.Parse(ConfigurationManager.AppSettings["redis_max_read_pool"]);
        public static int RedisMaxWritePool = 1;//int.Parse(ConfigurationManager.AppSettings["redis_max_write_pool"]);

        static RedisCacheHelper()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var ip = builder["Redis:IP"];
            var port = builder["Redis:Port"];
            redisHosts = new string[] { "a123456@"+ ip + ":" + port };
            if (redisHosts.Length > 0)
            {
                pool = new PooledRedisClientManager(redisHosts, redisHosts,
                    new RedisClientManagerConfig()
                    {
                        MaxWritePoolSize = RedisMaxWritePool,
                        MaxReadPoolSize = RedisMaxReadPool,
                        AutoStart = true
                    });
            }
        }
        public static void Add<T>(string key, T value, DateTime expiry)
        {
            if (value == null)
            {
                return;
            }

            if (expiry <= DateTime.Now)
            {
                Remove(key);

                return;
            }

            try
            {
                if (pool != null)
                {
                    using (var r = pool.GetClient())
                    {
                        if (r != null)
                        {
                            r.SendTimeout = 1000;
                            r.Set(key, value, expiry - DateTime.Now);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:{1}发生异常!{2}", "cache", "存储", key);
            }

        }

        public static void Add<T>(string key, T value, TimeSpan slidingExpiration)
        {
            if (value == null)
            {
                return;
            }

            if (slidingExpiration.TotalSeconds <= 0)
            {
                Remove(key);

                return;
            }

            try
            {
                if (pool != null)
                {
                    using (var r = pool.GetClient())
                    {
                        if (r != null)
                        {
                            r.SendTimeout = 1000;
                            bool f= r.Add<T>(key, value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:{1}发生异常!{2}", "cache", "存储", key);
            }

        }



        public static T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return default(T);
            }

            T obj = default(T);

            try
            {
                if (pool != null)
                {
                    using (var r = pool.GetClient())
                    {
                        if (r != null)
                        {
                            r.SendTimeout = 1000;
                            obj = r.Get<T>(key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:{1}发生异常!{2}", "cache", "获取", key);
            }


            return obj;
        }

        public static void Remove(string key)
        {
            try
            {
                if (pool != null)
                {
                    using (var r = pool.GetClient())
                    {
                        if (r != null)
                        {
                            r.SendTimeout = 1000;
                            r.Remove(key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:{1}发生异常!{2}", "cache", "删除", key);
            }

        }

        public static bool Exists(string key)
        {
            try
            {
                if (pool != null)
                {
                    using (var r = pool.GetClient())
                    {
                        if (r != null)
                        {
                            r.SendTimeout = 1000;
                            return r.ContainsKey(key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:{1}发生异常!{2}", "cache", "是否存在", key);
            }

            return false;
        }
    }
}

using Cn.Vcredit.Common.Log;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.Redis
{
    /// <summary>
    /// Redis管理
    /// </summary>
    public class RedisManager
    {
        #region 内部变量
        /// <summary>
        /// 日志记录
        /// </summary>
        protected static ILogger m_Logger;
        #endregion

        /// <summary>
        /// 初始化方法
        /// </summary>
        public RedisManager()
        {
            m_Logger = LogFactory.CreateLogger(this.GetType());
        }

        /// <summary>  
        /// redis配置文件信息  
        /// </summary>  
        private static RedisConfigInfo redisConfigInfo = RedisConfigInfo.GetConfig();

        /// <summary>
        /// Redis客户端管理类
        /// </summary>
        private static PooledRedisClientManager prcm;

        /// <summary>  
        /// 静态构造方法，初始化链接池管理对象  
        /// </summary>  
        static RedisManager()
        {
            CreateManager();
        }

        /// <summary>  
        /// 创建链接池管理对象  
        /// </summary>  
        private static void CreateManager()
        {
            try
            {
                string[] writeServerList = SplitString(redisConfigInfo.WriteServerList, ",");
                string[] readServerList = SplitString(redisConfigInfo.ReadServerList, ",");

                prcm = new PooledRedisClientManager(readServerList, writeServerList,
                                 new RedisClientManagerConfig
                                 {
                                     MaxWritePoolSize = redisConfigInfo.MaxWritePoolSize,
                                     MaxReadPoolSize = redisConfigInfo.MaxReadPoolSize,
                                     AutoStart = redisConfigInfo.AutoStart,
                                 });
            }
            catch (Exception ex)
            {
                m_Logger.Error(ex.Message);
                m_Logger.Error(ex.StackTrace);
            }
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        private static string[] SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }

        /// <summary>  
        /// 客户端缓存操作对象  
        /// </summary>  
        public static IRedisClient GetClient()
        {
            if (prcm == null)
                CreateManager();

            return prcm.GetClient();
        }
    }
}

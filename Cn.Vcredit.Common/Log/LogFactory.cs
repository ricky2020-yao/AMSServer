using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.Common.Log
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description：日志工厂类，管理所有的ILogger对象
    /// </summary>
    public class LogFactory
    {
        /// <summary>
        /// 根据字符串创建日志记录器对象
        /// </summary>
        /// <param name="loggerName"></param>
        /// <returns>记录器接口实例</returns>
        public static ILogger CreateLogger(string loggerName)
        {
            return new ApacheLogger(loggerName);
        }

        /// <summary>
        /// 根据类型的名称创建日志记录器对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>记录器接口实例</returns>
        public static ILogger CreateLogger(Type type)
        {
            return new ApacheLogger(type.ToString());
        }
    }
}

using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.Common.Log
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description：基于Log4net的日志记录器
    /// </summary>
    internal class ApacheLogger : ILogger
    {
        //日志记录器
        private ILog m_Logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="loggerName">记录器对象的名称</param>
        public ApacheLogger(string loggerName)
        {
            m_Logger = LogManager.GetLogger(loggerName);
        }


        /// <summary>
        /// 输出Debug级别的日志
        /// </summary>
        /// <param name="message">要输出的日志信息</param>
        public void Debug(object message)
        {
            m_Logger.Debug(message);
        }

        /// <summary>
        /// 输出Debug级别的日志
        /// </summary>
        /// <param name="message">要输出的日志信息</param>
        /// <param name="ex">要输出的异常信息</param>
        public void Debug(object message, Exception ex)
        {
            m_Logger.Debug(message, ex);
        }

        /// <summary>
        /// 输出Info级别的日志
        /// </summary>
        /// <param name="message">要输出的日志信息</param>
        public void Info(object message)
        {
            m_Logger.Info(message);
        }

        /// <summary>
        /// 输出Info级别的日志
        /// </summary>
        /// <param name="message">要输出的日志信息</param>
        /// <param name="ex">要输出的异常信息</param>
        public void Info(object message, Exception ex)
        {
            m_Logger.Info(message, ex);
        }

        /// <summary>
        /// 输出Error级别的日志
        /// </summary>
        /// <param name="message">要输出的日志信息</param>
        public void Error(object message)
        {
            m_Logger.Error(message);
        }

        /// <summary>
        /// 输出Error级别的日志
        /// </summary>
        /// <param name="message">要输出的日志信息</param>
        /// <param name="ex">要输出的异常信息</param>
        public void Error(object message, Exception ex)
        {
            m_Logger.Error(message, ex);
        }

        /// <summary>
        /// 输出Warn级别的日志
        /// </summary>
        /// <param name="message">要输出的日志信息</param>
        public void Warn(object message)
        {
            m_Logger.Warn(message);
        }

        /// <summary>
        /// 输出Warn级别的日志
        /// </summary>
        /// <param name="message">要输出的日志信息</param>
        /// <param name="ex">要输出的异常信息</param>
        public void Warn(object message, Exception ex)
        {
            m_Logger.Warn(message, ex);
        }

        /// <summary>
        /// 输出Fatal级别的日志
        /// </summary>
        /// <param name="message">要输出的日志信息</param>
        public void Fatal(object message)
        {
            m_Logger.Fatal(message);
        }

        /// <summary>
        /// 输出Fatal级别的日志
        /// </summary>
        /// <param name="message">要输出的日志信息</param>
        /// <param name="ex">要输出的异常信息</param>
        public void Fatal(object message, Exception ex)
        {
            m_Logger.Fatal(message, ex);
        }
    }
}

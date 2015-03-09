using Cn.Vcredit.Common.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cn.Vcredit.AMS.Controller.Manager
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description:线程池管理类
    /// </summary>
    public class ThreadPoolManager
    {
        #region 内部属性
        /// <summary>
        /// 日志记录
        /// </summary>
        private ILogger m_Logger = LogFactory.CreateLogger(typeof(ThreadPoolManager));
        #endregion

        #region 对外方法

        /// <summary>
        /// 将方法排入队列以便执行。此方法在有线程池线程变得可用时执行
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="state"></param>
        /// <returns>如果将方法成功排入队列，则为 true；否则为 false</returns>
        public bool DoWorkInThread(WaitCallback callBack, Object state)
        {
            m_Logger.Debug("开始执行具体服务。");

            try
            {
                return ThreadPool.QueueUserWorkItem(callBack, state);
            }
            catch (Exception ex)
            {
                m_Logger.Debug("执行具体服务异常。");
                m_Logger.Fatal(string.Concat("DoWorkInThread 错误: ", ex.Message));
                m_Logger.Fatal(string.Concat("DoWorkInThread 错误: ", ex.StackTrace));
            }

            return false;
        }
        #endregion
    }
}

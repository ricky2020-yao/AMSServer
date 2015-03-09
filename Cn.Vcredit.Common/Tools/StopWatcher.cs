using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.Common.Tools
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description:记录执行时间类
    /// </summary>
    public class StopWatcher
    {
        #region 内部属性
        private Stopwatch m_StopWatch;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public StopWatcher()
        {
            m_StopWatch = new Stopwatch();
        }
        #endregion

        #region 对外方法
        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            if (m_StopWatch.IsRunning)
            {
                m_StopWatch.Stop();
            }

            m_StopWatch.Reset();
            m_StopWatch.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            m_StopWatch.Stop();
        }

        /// <summary>
        /// 获取当前实例测量得出的总运行时间
        /// </summary>
        /// <returns></returns>
        public long GetRunTime()
        {
            return m_StopWatch.ElapsedMilliseconds;
        }
        #endregion
    }
}

using Cn.Vcredit.Common.Log;
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
    /// Description:监控记录执行时间类
    /// </summary>
    public class StopWatcherAuto : IDisposable
    {
        #region 内部属性
        /// <summary>
        /// 记录执行时间类
        /// </summary>
        private StopWatcher m_Sw = null;
        /// <summary>
        /// 当前执行的方法
        /// </summary>
        private string m_OperatorName = "";
        /// <summary>
        /// 日志记录
        /// </summary>
        private ILogger m_Logger = LogFactory.CreateLogger(typeof(StopWatcherAuto));
        #endregion

        #region IDisposable 成员
        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            try
            {
                m_Sw.Stop();
                m_Logger.Info(string.Format("完成 {0} 操作，共计耗时：{1}."
                    , GetMethod(), m_Sw.GetRunTime()));
            }
            catch (Exception ex)
            {
                m_Logger.Fatal(string.Concat("Dispose StopWatcherAuto出错:", ex.Message));
                m_Logger.Fatal(string.Concat("Dispose StopWatcherAuto出错:", ex.StackTrace));
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public StopWatcherAuto()
        {
            try
            {
                m_Sw = new StopWatcher();
                m_Sw.Start();
            }
            catch (Exception ex)
            {
                m_Logger.Fatal(string.Concat("实例化StopWatcherAuto出错:", ex.Message));
                m_Logger.Fatal(string.Concat("实例化StopWatcherAuto出错:", ex.StackTrace));
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public StopWatcherAuto(string name)
        {
            try
            {
                m_Sw = new StopWatcher();
                m_Sw.Start();
                this.m_OperatorName = name;
            }
            catch (Exception ex)
            {
                m_Logger.Fatal(string.Concat("实例化StopWatcherAuto出错:", ex.Message));
                m_Logger.Fatal(string.Concat("实例化StopWatcherAuto出错:", ex.StackTrace));
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取当前执行的方法名
        /// </summary>
        /// <returns></returns>
        private string GetMethod()
        {
            if (!string.IsNullOrEmpty(this.m_OperatorName))
                return this.m_OperatorName;

            var method = new StackFrame(2).GetMethod();
            string result = method.Name;

            if (string.IsNullOrEmpty(result))
            {
                return string.Empty;
            }
            else
            {
                return result;
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Define
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月28日
    /// Description:委托定义声明类
    /// </summary>
    public class DelegataDefine
    {
        /// <summary>
        /// 日志输出委托
        /// </summary>
        /// <param name="logMessage"></param>
        [Serializable]
        public delegate void ExportLogHandler(string logMessage);
    }
}

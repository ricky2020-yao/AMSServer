using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// 同步信息
    /// </summary>
    public class SyncInfo
    {
        /// <summary>
        /// 表名或者视图
        /// </summary>
        public string TableViewName { get; set; }
        /// <summary>
        /// 目标表
        /// </summary>
        public string DestinationName { get; set; }
        /// <summary>
        /// 目标类型
        /// </summary>
        public int DestinationType { get; set; }
        /// <summary>
        /// 已经同步过的时间
        /// </summary>
        public DateTime HadSyncTime { get; set; }
        /// <summary>
        /// 主键列字符，多个逗号隔开
        /// </summary>
        public string PrimaryKeys { get; set; }
        /// <summary>
        /// 记录是否有效
        /// </summary>
        public bool IsValid { get; set; }
    }
}

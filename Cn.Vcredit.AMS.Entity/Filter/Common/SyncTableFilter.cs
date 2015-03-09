using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.Common
{
    /// <summary>
    /// 同步表的过滤条件
    /// </summary>
    public class SyncTableFilter : BaseFilter
    {
        /// <summary>
        /// 源表名或视图名
        /// </summary>
        public string TableViewName { get; set; }
        /// <summary>
        /// 目标表名
        /// </summary>
        public string DestinationName { get; set; }
        /// <summary>
        /// 表的主键，多个逗号隔开
        /// </summary>
        public string PrimaryKeys { get; set; }
        /// <summary>
        /// 同步数据的条件
        /// </summary>
        public string Condition { get; set; }
        /// <summary>
        /// 强制全部重新加载
        /// </summary>
        public bool ForceReLoad { get; set; }
    }
}

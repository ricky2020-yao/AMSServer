using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// 现收导出报表过滤条件类
    /// </summary>
    public class DeriveSevExportFilter:BaseExportFilter
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 地区Key
        /// </summary>
        public string RegionKey { get; set; }
        /// <summary>
        /// 实收类型
        /// </summary>
        public string ReceiveType { get; set; }
        /// <summary>
        /// 分支权限
        /// </summary>
        public string BranchKey { get; set; }
        /// <summary>
        /// 收款账户
        /// </summary>
        public string AccountID { get; set; }
        /// <summary>
        /// 服务方
        /// </summary>
        public string ServiceSide { get; set; }
        /// <summary>
        /// 选择地区
        /// </summary>
        public string ServiceSideKey { get; set; }
    }
}

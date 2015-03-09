using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-18
    /// Description:担保客户数据导出检索条件
    /// </summary>
    public class ExportGuaranteeDataFilter:BaseExportFilter
    {
        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string BranchKey { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Overdue { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Day { get; set; }
    }
}

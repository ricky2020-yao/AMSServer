using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.ExamineIMP
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月23日
    /// Description:贷后客户逾期情况查询条件类
    /// </summary>
    public class OverDueReportFilter:BaseExportFilter
    {
        /// <summary>
        /// 审核人员类型
        /// 1：初审
        /// 2: 复审
        /// </summary>
        public int AuditType { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public int AuditPerson { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 申诉类型
        /// </summary>
        public int AppealType { get; set; }
    }
}

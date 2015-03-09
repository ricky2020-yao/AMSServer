using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.ExamineIMP
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月24日
    /// Description:逾期订单详情表查询条件类
    /// </summary>
    public class OverDueDetailFilter:BaseFilter
    {
        /// <summary>
        /// 订单ID，可以拼接多个，用","分割
        /// </summary>
        public string BusinessIds { get; set; }

        /// <summary>
        /// GUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 画面输入的业务号
        /// </summary>
        public string BussinessNo { get; set; }

        /// <summary>
        /// 审核人员类型
        /// 1：初审
        /// 2：复审
        /// </summary>
        public int AuditType { get; set; }

        /// <summary>
        /// 审核人员ID
        /// </summary>
        public int AuditPerson { get; set; }

        /// <summary>
        /// 审结开始日期
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 审结结束日期
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 审批产品种类
        /// </summary>
        public string AuditProductKind { get; set; }
    }
}

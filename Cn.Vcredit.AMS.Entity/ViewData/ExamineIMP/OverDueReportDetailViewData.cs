using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.ExamineIMP
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月24日
    /// Description:客户的逾期情况详细表
    /// </summary>
    public class OverDueReportDetailViewData
    {
        /// <summary>
        /// 序号 
        /// </summary>
        public int IndexNo { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string BusinessID { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdenNo { get; set; }
        /// <summary>
        /// 交单日期
        /// </summary>
        public string SubmitTime { get; set; }
        /// <summary>
        /// 审批产品种类
        /// </summary>
        public string LoanKind { get; set; }
        /// <summary>
        /// 初审人
        /// </summary>
        public string AuditerFirstName { get; set; }
        /// <summary>
        /// 复审人
        /// </summary>
        public string AuditerSecondName { get; set; }
        /// <summary>
        /// 放贷日期
        /// </summary>
        public string LoanTime { get; set; }
        /// <summary>
        /// 放贷金额
        /// </summary>
        public string LoanCapital { get; set; }
        /// <summary>
        /// 期限
        /// </summary>
        public int LoanPeriods { get; set; }
        /// <summary>
        /// 已还本金
        /// </summary>
        public string RepayCaptial { get; set; }
        /// <summary>
        /// 还款期数
        /// </summary>
        public int RepatPeriods { get; set; }
        /// <summary>
        /// 清贷日期
        /// </summary>
        public string ClearLoanTime { get; set; }
        /// <summary>
        /// 读取状态
        /// </summary>
        public int ReadState { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年12月3日
    /// Description: 代偿款支付设置条件类
    /// </summary>
    public class InterestFitFilter:BaseFilter
    {
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNO { get; set; }
        /// <summary>
        /// 公司Key
        /// </summary>
        public string BranchKey { get; set; }
        /// <summary>
        /// 贷款方
        /// </summary>
        public string LoanServiceKey { get; set; }
        /// <summary>
        /// 信托方
        /// </summary>
        public string LendingSideKey { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 付款状态
        /// </summary>
        public string PaymentStatus {get;set;}
        /// <summary>
        /// 转担保开始时间
        /// </summary>
        public string GuaranteeFromTime { get; set; }
        /// <summary>
        /// 转担保结束时间
        /// </summary>
        public string GuaranteeToTime { get; set; }
        /// <summary>
        /// 业务号IDs
        /// </summary>
        public string BusinessIDs { get; set; }
        /// <summary>
        /// 支付日期
        /// </summary>
        public string PaymentDate { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNos { get; set; }
    }
}

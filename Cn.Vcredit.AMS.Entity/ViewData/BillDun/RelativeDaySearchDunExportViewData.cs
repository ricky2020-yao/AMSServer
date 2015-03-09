using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:催收单导出结果实体类
    /// </summary>
    public class RelativeDaySearchDunExportViewData
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public string ReceivedType { get; set; }

        /// <summary>
        /// 放贷日期
        /// </summary>
        public DateTime LoanTime { get; set; }

        /// <summary>
        /// 账单日期
        /// </summary>
        public string BillMonth { get; set; }

        /// <summary>
        /// 收款操作日期
        /// </summary>
        public DateTime? ReceivedTime { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 储蓄卡号
        /// </summary>
        public string SavingCard { get; set; }

        /// <summary>
        /// 月本金
        /// </summary>
        public string Capital { get; set; }

        /// <summary>
        /// 月利息
        /// </summary>
        public string Interest { get; set; }

        /// <summary>
        /// 月服务费
        /// </summary>
        public string ServiceFee { get; set; }

        /// <summary>
        /// 月担保管理费
        /// </summary>
        public string GuaranteeFee { get; set; }

        /// <summary>
        /// 罚息
        /// </summary>
        public string PunitiveInterest { get; set; }

        /// <summary>
        /// 本息扣失
        /// </summary>
        public string InterestBuckleFail { get; set; }

        /// <summary>
        /// 服务费扣失
        /// </summary>
        public string ServiceBuckleFail { get; set; }

        /// <summary>
        /// 应收月本金
        /// </summary>
        public decimal ReceivedCapital { get; set; }

        /// <summary>
        /// 应收月利息
        /// </summary>
        public decimal ReceivedInterest { get; set; }

        /// <summary>
        /// 应收月服务费
        /// </summary>
        public decimal ReceivedServiceFee { get; set; }

        /// <summary>
        /// 应收月担保管理费
        /// </summary>
        public decimal ReceivedGuaranteeFee { get; set; }

        /// <summary>
        /// 应收罚息
        /// </summary>
        public decimal ReceivedPunitiveInterest { get; set; }

        /// <summary>
        /// 应收本息扣失
        /// </summary>
        public decimal ReceivedInterestBuckleFail { get; set; }

        /// <summary>
        /// 应收服务费扣失
        /// </summary>
        public decimal ReceivedServiceBuckleFail { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-19
    /// Description:催收单明细收款导出数据类
    /// </summary>
    public class DunDetailReceiveExportViewData
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public byte ReceivedType { get; set; }
        /// <summary>
        /// 数据类型名称
        /// </summary>
        public string ReceivedTypeName { get; set; }
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
        public DateTime ReceivedTime { get; set; }
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
        public decimal Capital { get; set; }
        /// <summary>
        /// 月利息
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// 月服务费
        /// </summary>
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 月担保管理费
        /// </summary>
        public decimal GuaranteeFee { get; set; }
        /// <summary>
        /// 已收罚息
        /// </summary>
        public decimal PunitiveInterest { get; set; }
        /// <summary>
        /// 已收本息扣失
        /// </summary>
        public decimal InterestBuckleFail { get; set; }
        /// <summary>
        /// 已收服务费扣失
        /// </summary>
        public decimal ServiceBuckleFail { get; set; }
        /// <summary>
        /// 已收加收利息
        /// </summary>
        public decimal AddInterest { get; set; }
        /// <summary>
        /// 已收加收服务费
        /// </summary>
        public decimal AddServiceFee { get; set; }
        /// <summary>
        /// 已收清贷服务费
        /// </summary>
        public decimal AdvCleanLoanServiceFee { get; set; }
        /// <summary>
        /// 已收剩余本金
        /// </summary>
        public decimal ResidualCapital { get; set; }
        /// <summary>
        /// 已收提前清贷服务费
        /// </summary>
        public decimal AdvServiceFee { get; set; }
        /// <summary>
        /// 已收加收担保费
        /// </summary>
        public decimal AddGuaranteeFee { get; set; }
        /// <summary>
        /// 已收担保违约金
        /// </summary>
        public decimal GuaranteeLateFee { get; set; }
        /// <summary>
        /// 已收诉讼费
        /// </summary>
        public decimal Litigation { get; set; }
        /// <summary>
        /// 已收诉讼违约金
        /// </summary>
        public decimal LitigationLateFee { get; set; }
        /// <summary>
        /// 应收月本金
        /// </summary>
        public decimal Capital_A { get; set; }
        /// <summary>
        /// 应收月利息
        /// </summary>
        public decimal Interest_A { get; set; }
        /// <summary>
        /// 应收月服务费
        /// </summary>
        public decimal ServiceFee_A { get; set; }
        /// <summary>
        /// 应收月担保管理费
        /// </summary>
        public decimal GuaranteeFee_A { get; set; }
        /// <summary>
        /// 应收罚息
        /// </summary>
        public decimal PunitiveInterest_A { get; set; }
        /// <summary>
        /// 应收罚息
        /// </summary>
        public decimal InterestBuckleFail_A { get; set; }
        /// <summary>
        /// 应收服务费扣失
        /// </summary>
        public decimal ServiceBuckleFail_A { get; set; }
        /// <summary>
        /// 应收加收利息
        /// </summary>
        public decimal AddInterest_A { get; set; }
        /// <summary>
        /// 应收加收服务费
        /// </summary>
        public decimal AddServiceFee_A { get; set; }
        /// <summary>
        /// 应收清贷服务费
        /// </summary>
        public decimal AdvCleanLoanServiceFee_A { get; set; }
        /// <summary>
        /// 应收加收担保管理费
        /// </summary>
        public decimal ResidualCapital_A { get; set; }
        /// <summary>
        /// 应收提前清贷服务费
        /// </summary>
        public decimal AdvServiceFee_A { get; set; }
        /// <summary>
        /// 应收加收担保费
        /// </summary>
        public decimal AddGuaranteeFee_A { get; set; }
        /// <summary>
        /// 应收担保违约金
        /// </summary>
        public decimal GuaranteeLateFee_A { get; set; }
        /// <summary>
        /// 应收诉讼费
        /// </summary>
        public decimal Litigation_A { get; set; }
        /// <summary>
        /// 应收诉讼违约金
        /// </summary>
        public decimal LitigationLateFee_A { get; set; }
    }
}

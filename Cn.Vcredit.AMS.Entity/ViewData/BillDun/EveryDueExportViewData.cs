using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// Author:wichell
    /// CreateTime:2014年10月9日
    /// Description:静态逾期报表导出显示实体
    /// </summary>
    public class EveryDueExportViewData
    {
        /// <summary>
        /// [合同编号]
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// [催收单号]
        /// </summary>
        public long DunID { get; set; }
        /// <summary>
        /// [客户姓名]
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// [借款借据版本]
        /// </summary>
        public string ReceiptVersion { get; set; }
        /// <summary>
        /// [贷款金额]
        /// </summary>
        public decimal LoanCapital { get; set; }
        /// <summary>
        /// [剩余本金]
        /// </summary>
        public decimal ResidualCapital { get; set; }
        /// <summary>
        /// [逾期金额]
        /// </summary>
        public decimal OverdueAmount { get; set; }
        /// <summary>
        /// [本金]
        /// </summary>
        public decimal Capital { get; set; }
        /// <summary>
        /// [利息]
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// [管理费]
        /// </summary>
        public decimal ManagementFee { get; set; }
        /// <summary>
        /// [本息扣失]
        /// </summary>
        public decimal CapitalInterestLoss { get; set; }
        /// <summary>
        /// [服务费]
        /// </summary>
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// [担保管理费]
        /// </summary>
        public decimal GuaranteeFee { get; set; }
        /// <summary>
        /// [服务费扣失]
        /// </summary>
        public decimal ServiceFeeLoss { get; set; }
        /// <summary>
        /// [罚息]
        /// </summary>
        public decimal Penalty { get; set; }
        /// <summary>
        /// [账单费用]
        /// </summary>
        public decimal BillFee { get; set; }
        /// <summary>
        /// [贷款期数]
        /// </summary>
        public int LoanPeriod { get; set; }
        /// <summary>
        /// [贷款期限内已还期数（全部还款）]
        /// </summary>
        public int RepayPeriod { get; set; }
        /// <summary>
        /// [逾期日期]
        /// </summary>
        public string OverdueDate { get; set; }
        /// <summary>
        /// [逾期天数]
        /// </summary>
        public int OverdueDays { get; set; }
        /// <summary>
        /// [当日逾期标记]
        /// </summary>
        public string TodayOverdueMark { get; set; }
        /// <summary>
        /// [期初逾期标记]
        /// </summary>
        public string BeginningOverdueMark { get; set; }
        /// <summary>
        /// [担保]
        /// </summary>
        public string Guarantee { get; set; }
        /// <summary>
        /// [担保日期]
        /// </summary>
        public string GuaranteeDate { get; set; }
        /// <summary>
        /// [担保金额]
        /// </summary>
        public decimal GuaranteeAmount { get; set; }
        /// <summary>
        /// [委外状态]
        /// </summary>
        public string OutStatus { get; set; }
        /// <summary>
        /// [诉讼状态]
        /// </summary>
        public string LitigationStatus { get; set; }
        /// <summary>
        /// [提前清贷状态]
        /// </summary>
        public string CLoanStatus { get; set; }
        /// <summary>
        /// [特殊政策]
        /// </summary>
        public string SpecialPolicy { get; set; }
        /// <summary>
        /// [贷款产品种类]
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// [产品类型]
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// [信托方（放款方）]
        /// </summary>
        public string LendingSide { get; set; }
        /// <summary>
        /// [服务方]
        /// </summary>
        public string ServiceSide { get; set; }
        /// <summary>
        /// [放款日期]
        /// </summary>
        public string LoanTime { get; set; }
        /// <summary>
        /// [销售渠道]
        /// </summary>
        public string SalesChannels { get; set; }
        /// <summary>
        /// [签约地区（城市）]
        /// </summary>
        public string SigningCity { get; set; }
        /// <summary>
        /// [签约门店]
        /// </summary>
        public string SigningShop { get; set; }
        /// <summary>
        /// [团队]
        /// </summary>
        public string SalesTeam { get; set; }
        /// <summary>
        /// [经办人]
        /// </summary>
        public string SalesStaff { get; set; }
        /// <summary>
        /// [日期]
        /// </summary>
        public string StatisticsDate { get; set; }
        public int OverID { get; set; }
        public string BusinessStatus { get; set; }
        public string IdNumber { get; set; }
        public int BusinessID { get; set; }
    }
}

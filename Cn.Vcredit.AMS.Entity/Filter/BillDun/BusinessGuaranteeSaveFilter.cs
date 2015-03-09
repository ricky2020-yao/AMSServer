using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// Desc:担保和诉讼设置保存条件
    /// </summary>
    public class BusinessGuaranteeSaveFilter:BaseFilter
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public byte BusinessStatus { get; set; }
        /// <summary>
        /// 诉讼状态
        /// </summary>
        public byte LawsuitStatus { get; set; }
        /// <summary>
        /// 判决案号
        /// </summary>
        public string LawsuitCode { get; set; }
        /// <summary>
        /// 转诉讼日期
        /// </summary>
        public string ToLitigationTime { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 诉讼费
        /// </summary>
        public string LegalCost{ get; set; }
        /// <summary>
        /// 诉讼违约金
        /// </summary>
        public string LegalPenalty { get; set; }
        /// <summary>
        /// 是否追加新的账单
        /// </summary>
        public bool IsAddNewBill { get; set; }
        /// <summary>
        /// 贷款期数
        /// </summary>
        public int LoanPeriod { get; set; }

        /// <summary>
        /// 贷款本金
        /// </summary>
        public decimal LoanCapital { get; set; }

        /// <summary>
        /// 月利率
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        /// 月服务费率
        /// </summary>
        public decimal ServiceRate { get; set; }

        /// <summary>
        /// 担保方键名
        /// </summary>
        public string GuaranteeSideKey { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string GuaranteeNo { get; set; }

        /// <summary>
        /// 转担保日期
        /// </summary>
        public DateTime GuaranteeTime { get; set; }

        /// <summary>
        /// 已出本金
        /// </summary>
        public decimal CapitalHasBeen { get; set; }

        /// <summary>
        /// 未出本金
        /// </summary>
        public decimal CapitalNotBeen { get; set; }

        /// <summary>
        /// 本息扣失
        /// </summary>
        public decimal CapitalLost { get; set; }

        /// <summary>
        /// 利息
        /// </summary>
        public decimal Interest { get; set; }

        /// <summary>
        /// 未出利息
        /// </summary>
        public decimal InterestNotBeen { get; set; }

        /// <summary>
        /// 罚息
        /// </summary>
        public decimal PenaltyInt { get; set; }
    }
}

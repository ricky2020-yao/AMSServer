using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:清贷原因设置检索结果类
    /// </summary>
    public class ClearLoanReasonSearchViewData
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdenNo { get; set; }
        /// <summary>
        /// 贷款产品
        /// </summary>
        public string LoanKind { get; set; }
        /// <summary>
        /// 贷款金额
        /// </summary>
        public decimal LoanCapital { get; set; }
        /// <summary>
        /// 贷款期数
        /// </summary>
        public int LoanPeriod { get; set; }
        /// <summary>
        /// 放贷日期
        /// </summary>
        public DateTime LoanTime { get; set; }
        /// <summary>
        /// 还款期数
        /// </summary>
        public int RepayedPeriods { get; set; }
        /// <summary>
        /// 实际清贷日
        /// </summary>
        public DateTime ClearLoanTime { get; set; }
        /// <summary>
        /// 清贷状态
        /// </summary>
        public byte CLoanStatus { get; set; }
        /// <summary>
        /// 清贷状态
        /// </summary>
        public string StrCloanStatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public byte BusinessStatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string StrBusinessStatus { get; set; }
        /// <summary>
        /// 担保日期
        /// </summary>
        public DateTime? ToGuaranteeTime { get; set; }
        /// <summary>
        /// 诉讼日期
        /// </summary>
        public DateTime? ToLitigationTime { get; set; }
        /// <summary>
        /// 清贷原因
        /// </summary>
        public string ClearLoanType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ClearLoanRemark { get; set; }
        /// <summary>
        /// 储蓄卡号
        /// </summary>
        public string SavingCard { get; set; }
    }
}

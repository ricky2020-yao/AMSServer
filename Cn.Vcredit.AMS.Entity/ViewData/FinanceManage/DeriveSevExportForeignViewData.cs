using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-12
    /// Description:外贸导出信息
    /// </summary>
    public class DeriveSevExportForeignViewData
    {/// <summary>
        /// 产品类型键名
        /// </summary>
        public string ProductKind { get; set; }
        /// <summary>
        /// 实收类型
        /// </summary>
        public int ReceivedType { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 帐单月
        /// </summary>
        public string BillMonth { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        public DateTime RecTime { get; set; }
        /// <summary>
        /// 本金
        /// </summary>
        public decimal Capital { get; set; }
        /// <summary>
        /// 利息
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// 服务费
        /// </summary>
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 担保费
        /// </summary>
        public decimal GuaranteeFee { get; set; }
        /// <summary>
        /// 本息扣失
        /// </summary>
        public decimal InterestBuckleFail { get; set; }
        /// <summary>
        /// 服务费扣失
        /// </summary>
        public decimal ServiceBuckleFail { get; set; }
        /// <summary>
        /// 罚息
        /// </summary>
        public decimal PunitiveInterest { get; set; }
        /// <summary>
        /// 保险费
        /// </summary>
        public decimal Premium { get; set; }
        /// <summary>
        /// 管理费
        /// </summary>
        public decimal Manage { get; set; }
        /// <summary>
        /// 实收类型
        /// </summary>
        public string StrReviceType { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        public string PayDate { get; set; }

        /// <summary>
        /// 业务代码
        /// </summary>
        public string BusinessCode { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractID { get; set; }
        /// <summary>
        /// 扣款日期
        /// </summary>
        public string DeductDate { get; set; }
        /// <summary>
        /// 扣款类型
        /// </summary>
        public string DeductType { get; set; }
        /// <summary>
        /// 本金金额
        /// </summary>
        public decimal ContractAmount { get; set; }
        /// <summary>
        /// 利息金额
        /// </summary>
        public decimal InterestAmount { get; set; }
        /// <summary>
        /// 服务扣失费用
        /// </summary>
        public decimal ServiceFeedeductcharge { get; set; }
        /// <summary>
        /// 本息扣失费用
        /// </summary>
        public decimal deductcharge { get; set; }
        /// <summary>
        /// 担保费用
        /// </summary>
        public decimal guaranteecharge { get; set; }
        /// <summary>
        /// 清除信用费用
        /// </summary>
        public decimal CleanCreditCharge { get; set; }
        /// <summary>
        /// 罚息费用
        /// </summary>
        public decimal PunitiveInterestAmount { get; set; }
        /// <summary>
        /// 服务费用
        /// </summary>
        public string RepayDuration { get; set; }
        /// <summary>
        /// 合同终止的费用
        /// </summary>
        public string ContractTerminatedDate { get; set; }
        /// <summary>
        /// 服务费用
        /// </summary>
        public string RepaySeqNo { get; set; }
        /// <summary>
        /// 第三方支付代码
        /// </summary>
        public string ThirdPartyCode { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        public string RecieveDate { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public string AccountID { get; set; }
        /// <summary>
        /// 银行
        /// </summary>
        public string BnakID { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 支付日期
        /// </summary>
        public DateTime PaymentDate { get; set; }
    }
}

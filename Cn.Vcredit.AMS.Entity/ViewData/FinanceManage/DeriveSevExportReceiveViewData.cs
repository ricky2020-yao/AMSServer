using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-12
    /// Description:科目实收信息
    /// </summary>
    public class DeriveSevExportReceiveViewData
    {
        /// <summary>
        /// 实收类型
        /// </summary>
        public string StrReviceType { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 本金
        /// </summary>
        public decimal Capital { get; set; }
        /// <summary>
        /// 利息
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// 管理费
        /// </summary>
        public decimal Manage { get; set; }
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
        /// 收款日期
        /// </summary>
        public string PayDate { get; set; }
        /// <summary>
        /// 帐单月
        /// </summary>
        public string BillMonth { get; set; }
        /// <summary>
        /// 实收类型
        /// </summary>
        public int ReceivedType { get; set; }
        /// <summary>
        /// 产品类型键名
        /// </summary>
        public string ProductKind { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        public DateTime RecTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-12
    /// Description:每日扣导出导出实体
    /// </summary>
    public class DeriveRecExportViewData
    {
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 帐单月
        /// </summary>
        public string BillMonth { get; set; }
        /// <summary>
        /// 本金
        /// </summary>
        public decimal CapitalAmt { get; set; }
        /// <summary>
        /// 利息
        /// </summary>
        public decimal InterestAmt { get; set; }
        /// <summary>
        /// 管理费
        /// </summary>
        public decimal ManagementAmt { get; set; }
        /// <summary>
        /// 服务费
        /// </summary>
        public decimal ServiceAmt { get; set; }
        /// <summary>
        /// 担保管理费
        /// </summary>
        public decimal GuaranteeAmt { get; set; }
        /// <summary>
        /// 本息扣失
        /// </summary>
        public decimal CapitalIntBAmt { get; set; }
        /// <summary>
        /// 服务费扣失
        /// </summary>
        public decimal ServiceBAmt { get; set; }
        /// <summary>
        /// 罚息
        /// </summary>
        public decimal PenaltyIntAmt { get; set; }
        /// <summary>
        /// 合计
        /// </summary>
        public decimal Total { get; set; }
    }
}

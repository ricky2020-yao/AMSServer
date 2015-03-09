using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// 订单视图
    /// </summary>
    public class BusinessViewData
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
        public string PersonName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public byte BusinessStatus { get; set; }

        /// <summary>
        /// 冻结码
        /// </summary>
        public string FrozenNo { get; set; }

        /// <summary>
        /// 当期欠费
        /// </summary>
        public decimal CurrentOverAmount { get; set; }

        /// <summary>
        /// 逾期欠费
        /// </summary>
        public decimal OverAmount { get; set; }

        /// <summary>
        /// 清贷状态
        /// </summary>
        public byte CLoanStatus { get; set; }
    }
}

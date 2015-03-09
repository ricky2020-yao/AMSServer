using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年10月21日
    /// Description:担保条目
    /// </summary>
    public class BusinessGuaranteeItemViewData
    {
        /// <summary>
        /// 罚息
        /// </summary>
        public decimal DuePenalty { get; set; }

        /// <summary>
        /// 本金
        /// </summary>
        public decimal DueCapital { get; set; }

        /// <summary>
        /// 本息扣失
        /// </summary>
        public decimal DueCapitalLost { get; set; }

        /// <summary>
        /// 未出本金
        /// </summary>
        public decimal DueResidualCapital { get; set; }

        /// <summary>
        /// 利息(已出+未出)
        /// </summary>
        public decimal DueInterest { get; set; }

        /// <summary>
        /// 未出利息(隐藏不显示,用于入GuaranteeItem做记录)
        /// </summary>
        public decimal DueResidualInterest { get; set; }
    }
}

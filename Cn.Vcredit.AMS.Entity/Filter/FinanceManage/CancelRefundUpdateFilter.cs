using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// 解约更新付款类
    /// </summary>
    public class CancelRefundUpdateFilter : BaseFilter
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public int BusinessId { get; set; }
        /// <summary>
        /// 实退金额
        /// </summary>
        public decimal RefundAmt { get; set; }

        /// <summary>
        /// 付款日期
        /// </summary>
        public DateTime? PayDate { get; set; }

        /// <summary>
        /// 到账日期
        /// </summary>
        public DateTime? ReceivedDate { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public byte PayType { get; set; }
        /// <summary>
        /// 更新内容
        /// </summary>
        public string UpdateContents { get; set; }
    }
}

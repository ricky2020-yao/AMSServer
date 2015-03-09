using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// Author:shwang
    /// Date:20140616
    /// Desc:解约退款查询条件
    /// </summary>
    public class CancelRefundFilter : BaseFilter
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public int? BusinessID { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 解约开始日期
        /// </summary>
        public DateTime? CancelBeginTime { get; set; }

        /// <summary>
        /// 解约结束日期
        /// </summary>
        public DateTime? CancelEndTime { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 付款开始日期
        /// </summary>
        public DateTime? PayBeginTime { get; set; }

        /// <summary>
        /// 付款结束日期
        /// </summary>
        public DateTime? PayEndTime { get; set; }

        /// <summary>
        /// 放贷方key
        /// </summary>
        public string LendingSideKey { get; set; }

        /// <summary>
        /// 是否有付款日期
        /// </summary>
        public bool HasPayDate { get; set; }
    }
}

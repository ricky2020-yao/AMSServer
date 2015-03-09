using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-11-25
    /// Description:填账数据条件
    /// </summary>
    public class PayAccountFilter : SearchBusinessListFilter
    {
        /// <summary>
        /// Json字符串
        /// </summary>
        public string JsonString { get; set; }

        /// <summary>
        /// 调整原因
        /// </summary>
        public string Explain { get; set; }

        /// <summary>
        /// 提前清贷编号
        /// </summary>
        public int CloanApplyID { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amout { get; set; }

        /// <summary>
        /// Dectime
        /// </summary>
        public DateTime? Dectime { get; set; }

        /// <summary>
        /// 更新清贷状态标志
        /// </summary>
        public int UpdateCloanApplyStatus { get; set; }
    }
}

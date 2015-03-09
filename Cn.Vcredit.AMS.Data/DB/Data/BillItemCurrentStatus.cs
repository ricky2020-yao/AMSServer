using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:科目当前状态表
    /// </summary>
    public class BillItemCurrentStatus
    {
        #region- 基本属性 -
        /// <summary>
        /// 帐单款项编号
        /// </summary>
        public long BillItemID { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 帐单编号[关联Bill表格]
        /// </summary>
        public long BillID { get; set; }

        /// <summary>
        /// 实际应收
        /// </summary>
        public decimal DueAmt { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public decimal ReceivedAmt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 足额还款时间
        /// </summary>
        public DateTime? FullPaidTime { get; set; }

        /// <summary>
        /// 是否当期
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 累计生成罚息金额
        /// </summary>
        public decimal PenaltyIntAmt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Updatetime { get; set; }
        #endregion
    }
}

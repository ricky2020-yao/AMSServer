using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年10月11日
    /// Description:罚息日志表
    /// </summary>
    public class PenaltyInt
    {
        #region- 基本属性 -
        /// <summary>
        /// 主键列
        /// </summary>
        public long PenaltyIntID { get; set; }

        /// <summary>
        /// 业务号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 原因帐单
        /// </summary>
        public long ReasonID { get; set; }

        /// <summary>
        /// 原因科目
        /// </summary>
        public long ReasonItemID { get; set; }

        /// <summary>
        /// 所属帐单
        /// </summary>
        public long ToBillID { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 是否搁置
        /// </summary>
        public bool IsShelve { get; set; }

        /// <summary>
        /// 创建帐单
        /// </summary>
        public DateTime CreateTime { get; set; }
        #endregion
    }
}

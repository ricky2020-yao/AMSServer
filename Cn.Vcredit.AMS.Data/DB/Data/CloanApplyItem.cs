using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:提前清贷帐单科目
    /// </summary>
    public class CloanApplyItem
    {
        #region- 基本属性 -
        /// <summary>
        /// 主键
        /// </summary>
        public int CloanApplyItemID { get; set; }

        /// <summary>
        /// 申请记录主表主键
        /// </summary>
        public int CloanApplyID { get; set; }

        /// <summary>
        /// 科目
        /// </summary>
        public byte Subject { get; set; }

        /// <summary>
        /// 是否是减免款项
        /// </summary>
        public bool IsAnnul { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Updatetime { get; set; }

        /// <summary>
        /// 清贷申请记录
        /// </summary>
        public virtual CloanApply CloanApply { get; set; }
        #endregion
    }
}

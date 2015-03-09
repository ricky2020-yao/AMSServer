using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年6月5日
    /// Description:帐单类型
    /// </summary>
    public enum EnumBillKind : byte
    {
        /// <summary>
        /// 普通帐单
        /// </summary>
        [Description("普通帐单")]
        Normal = 1,

        /// <summary>
        /// 担保帐单
        /// </summary>
        [Description("担保帐单")]
        Guarantee = 2,

        /// <summary>
        /// 提前清贷帐单
        /// </summary>
        [Description("提前清贷")]
        Advance = 3,

        /// <summary>
        /// 坏帐帐单
        /// </summary>
        [Description("坏帐帐单")]
        BadDebts = 4,

        /// <summary>
        /// 注销帐单
        /// </summary>
        [Description("注销帐单")]
        Annul = 5,

        /// <summary>
        /// 调整帐单
        /// </summary>
        [Description("调整帐单")]
        Adjust = 6,

        ///<summary>
        /// 诉讼账单
        ///</summary>
        [Description("诉讼账单")]
        Litigation = 8,

        #region 担保/诉讼
        /// <summary>
        /// 新增担保帐单
        /// </summary>
        [Description("担保管理费用")]
        AddGuarantee = 7,

        ///<summary>
        /// 诉讼费用
        ///</summary>
        [Description("诉讼费用")]
        AddLitigation = 9,
        #endregion

        ///<summary>
        /// 诉讼费用
        ///</summary>
        [Description("手续费帐单")]
        Procedures = 10
    }
}

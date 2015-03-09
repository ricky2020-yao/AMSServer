using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:清贷申请类型
    /// </summary>
    public enum EnumCloanApplyKind : byte
    {
        /// <summary>
        /// 提前清贷
        /// </summary>
        [Description("提前清贷")]
        Adv = 1,

        /// <summary>
        /// 坏账清贷
        /// </summary>
        [Description("坏账清贷")]
        Bad = 2
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年5月31日
    /// Description:扣款序列类型
    /// </summary>
    public enum EnumDeductSeqKind : byte
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Description("默认")]
        Default = 0,

        /// <summary>
        /// 扣款序列（每月21日首扣，28日扣款失败生成扣失，次月12日扣款失败生成罚息并创建新一期帐单）
        /// </summary>
        [Description("21-28-12")]
        DS21_28_12 = 1,

        /// <summary>
        /// 扣款序列（每月21日首扣，月末扣款失败生成扣失、罚息，次月5日和15日扣除逾期帐单）
        /// </summary>
        [Description("21-30-5-15")]
        DS21_30_5_15 = 2
    }
}

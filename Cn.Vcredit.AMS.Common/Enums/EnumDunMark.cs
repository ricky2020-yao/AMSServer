using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年7月10日
    /// Description:催收单帐单逾期标记
    /// </summary>
    public enum EnumDunMark : byte
    {
        /// <summary>
        /// 仅偿还本息
        /// </summary>
        [Description("仅偿还本息")]
        m = 50,

        /// <summary>
        /// 逾期一期
        /// </summary>
        [Description("逾期一期")]
        M1 = 1,

        /// <summary>
        /// 逾期两期
        /// </summary>
        [Description("逾期两期")]
        M2 = 2,

        /// <summary>
        /// 逾期三期
        /// </summary>
        [Description("逾期三期")]
        M3 = 3,

        /// <summary>
        /// 逾期四期
        /// </summary>
        [Description("逾期四期")]
        M4 = 4,

        /// <summary>
        /// 逾期五期
        /// </summary>
        [Description("逾期五期")]
        M5 = 5,

        /// <summary>
        /// 逾期六期
        /// </summary>
        [Description("逾期六期")]
        M6 = 6,

        /// <summary>
        /// 逾期七期
        /// </summary>
        [Description("逾期七期")]
        M7 = 7,

        /// <summary>
        /// 逾期八期
        /// </summary>
        [Description("逾期八期")]
        M8 = 8,

        /// <summary>
        /// 逾期九期
        /// </summary>
        [Description("逾期九期")]
        M9 = 9,

        /// <summary>
        /// 逾期十期
        /// </summary>
        [Description("逾期十期")]
        M10 = 10,

        /// <summary>
        /// 逾期十一期
        /// </summary>
        [Description("逾期十一期")]
        M11 = 11,

        /// <summary>
        /// 逾期十二期
        /// </summary>
        [Description("逾期十二期")]
        M12 = 12,

        /// <summary>
        /// 逾期十三期
        /// </summary>
        [Description("逾期十三期")]
        M13 = 13,

        /// <summary>
        /// 逾期十四期
        /// </summary>
        [Description("逾期十四期")]
        M14 = 14,

        /// <summary>
        /// 逾期十五期
        /// </summary>
        [Description("逾期十五期")]
        M15 = 15,

        /// <summary>
        /// 逾期十六期
        /// </summary>
        [Description("逾期十六期")]
        M16 = 16,

        /// <summary>
        /// 逾期十七期
        /// </summary>
        [Description("逾期十七期")]
        M17 = 17,

        /// <summary>
        /// 逾期十八期
        /// </summary>
        [Description("逾期十八期")]
        M18 = 18,

        /// <summary>
        /// 逾期十九期
        /// </summary>
        [Description("逾期十九期")]
        M19 = 19,

        /// <summary>
        /// 逾期二十期
        /// </summary>
        [Description("逾期二十期")]
        M20 = 20,

        /// <summary>
        /// 逾期二十一期
        /// </summary>
        [Description("逾期二十一期")]
        M21 = 21,

        /// <summary>
        /// 逾期二十二期
        /// </summary>
        [Description("逾期二十二期")]
        M22 = 22,

        /// <summary>
        /// 逾期二十三期
        /// </summary>
        [Description("逾期二十三期")]
        M23 = 23,

        /// <summary>
        /// 逾期二十四期
        /// </summary>
        [Description("逾期二十四期")]
        M24 = 24,

        /// <summary>
        /// 逾期二十五期
        /// </summary>
        [Description("逾期二十五期")]
        M25 = 25,

        /// <summary>
        /// 逾期二十六期
        /// </summary>
        [Description("逾期二十六期")]
        M26 = 26,

        /// <summary>
        /// 逾期二十七期
        /// </summary>
        [Description("逾期二十七期")]
        M27 = 27,

        /// <summary>
        /// 逾期二十八期
        /// </summary>
        [Description("逾期二十八期")]
        M28 = 28,

        /// <summary>
        /// 逾期二十九期
        /// </summary>
        [Description("逾期二十九期")]
        M29 = 29,

        /// <summary>
        /// 逾期三十期
        /// </summary>
        [Description("逾期三十期")]
        M30 = 30,

        /// <summary>
        /// 逾期三十一期
        /// </summary>
        [Description("逾期三十一期")]
        M31 = 31,

        /// <summary>
        /// 逾期三十二期
        /// </summary>
        [Description("逾期三十二期")]
        M32 = 32,

        /// <summary>
        /// 逾期三十三期
        /// </summary>
        [Description("逾期三十三期")]
        M33 = 33,

        /// <summary>
        /// 逾期三十四期
        /// </summary>
        [Description("逾期三十四期")]
        M34 = 34,

        /// <summary>
        /// 逾期三十五期
        /// </summary>
        [Description("逾期三十五期")]
        M35 = 35,

        /// <summary>
        /// 逾期三十六期
        /// </summary>
        [Description("逾期三十六期")]
        M36 = 36,

        /// <summary>
        /// 逾期三十七期
        /// </summary>
        [Description("逾期三十七期")]
        M37 = 37,

        /// <summary>
        /// 逾期三十八期
        /// </summary>
        [Description("逾期三十八期")]
        M38 = 38,

        /// <summary>
        /// 逾期三十九期
        /// </summary>
        [Description("逾期三十九期")]
        M39 = 39,

        /// <summary>
        /// 逾期四十期
        /// </summary>
        [Description("逾期四十期")]
        M40 = 40,

        /// <summary>
        /// 逾期四十一期
        /// </summary>
        [Description("逾期四十一期")]
        M41 = 41,

        /// <summary>
        /// 逾期四十二期
        /// </summary>
        [Description("逾期四十二期")]
        M42 = 42,

        /// <summary>
        /// 逾期四十三期
        /// </summary>
        [Description("逾期四十三期")]
        M43 = 43,

        /// <summary>
        /// 逾期四十四期
        /// </summary>
        [Description("逾期四十四期")]
        M44 = 44,

        /// <summary>
        /// 逾期四十五期
        /// </summary>
        [Description("逾期四十五期")]
        M45 = 45,

        /// <summary>
        /// 逾期四十六期
        /// </summary>
        [Description("逾期四十六期")]
        M46 = 46,
        /// <summary>
        /// 逾期四十七期
        /// </summary>
        [Description("逾期四十七期")]
        M47 = 47,
        /// <summary>
        /// 逾期四十八期
        /// </summary>
        [Description("逾期四十八期")]
        M48 = 48,
        /// <summary>
        /// 逾期四十九期
        /// </summary>
        [Description("逾期四十九期")]
        M49 = 49
    }
}

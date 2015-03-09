using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年5月31日
    /// Description:订单状态
    /// </summary>
    public enum EnumBusinessStatus : byte
    {
        [Description("")]
        Default = 0,

        [Description("正常")]
        Normal = 1,

        [Description("担保")]
        Guarantee = 2,

        [Description("诉讼")]
        Litigation = 3,
    }
}

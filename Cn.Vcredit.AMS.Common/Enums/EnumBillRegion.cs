using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年6月7日
    /// Description:筛选帐单的区间
    /// </summary>
    public enum EnumBillRegion : byte
    {
        /// <summary>
        /// 筛选当期帐单
        /// </summary>
        [Description("当期帐单")]
        Current = 1,

        /// <summary>
        /// 筛选逾期帐单
        /// </summary>
        [Description("逾期帐单")]
        Overdue = 2,

        /// <summary>
        /// 筛选所有帐单
        /// </summary>
        [Description("所有帐单")]
        CurrentAndOverdue = 3
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年7月27日
    /// Description:科目性质
    /// </summary>
    public enum EnumSubjectKind : byte
    {
        /// <summary>
        /// 普通
        /// </summary>
        [Description("普通")]
        Normal = 1,

        /// <summary>
        /// 补生成
        /// </summary>
        [Description("补生成")]
        Supplement = 2,
    }
}

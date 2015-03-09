using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年5月30日
    /// Description:诉讼执行状态
    /// </summary>
    public enum EnumLawsuitStatus : byte
    {
        /// <summary>
        /// 全选
        /// </summary>
        [Description("")]
        Default = 0,

        /// <summary>
        /// 未诉讼
        /// </summary>
        [Description("未诉讼")]
        Unlawsuit = 1,

        /// <summary>
        /// 诉讼中
        /// </summary>
        [Description("诉讼中")]
        Lawsuiting = 2,

        /// <summary>
        /// 诉讼完成
        /// </summary>
        [Description("诉讼完成")]
        Lawsuited = 3,

        /// <summary>
        /// 申请执行
        /// </summary>
        [Description("申请执行")]
        ApplyExecute = 4,

        /// <summary>
        /// 执行一次
        /// </summary>
        [Description("执行一次")]
        ExecuteFirst = 5,

        /// <summary>
        /// 执行二次
        /// </summary>
        [Description("执行二次")]
        ExecuteSecond = 6
    }
}

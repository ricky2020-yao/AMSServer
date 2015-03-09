using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年9月6日
    /// Description:清贷申请状态
    /// </summary>
    public enum EnumCloanApplyStatus : byte
    {
        /// <summary>
        /// 已申请
        /// </summary>
        [Description("已申请")]
        Apply = 1,

        /// <summary>
        /// 已通过
        /// </summary>
        [Description("已通过")]
        Pass = 2,

        /// <summary>
        /// 已拒绝
        /// </summary>
        [Description("已拒绝")]
        Reject = 3,

        /// <summary>
        /// 已过期(清贷时间过期)
        /// </summary>
        [Description("已过期")]
        Expire = 4,

        /// <summary>
        /// 已成功
        /// </summary>
        [Description("已成功")]
        Success = 5,

        /// <summary>
        /// 已注销(客户提出注销提前清贷)
        /// </summary>
        [Description("已注销")]
        Annul = 6
    }
}

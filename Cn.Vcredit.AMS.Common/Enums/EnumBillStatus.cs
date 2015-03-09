using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年4月17日
    /// Description:帐单状态
    /// </summary>
    public enum EnumBillStatus : byte
    {
        /// <summary>
        /// 全选
        /// </summary>
        [Description("")]
        Default = 0,

        /// <summary>
        /// 欠费（包括：未付款、部分付款）
        /// </summary>
        [Description("欠费")]
        Debts = 4,

        /// <summary>
        /// 未付款
        /// </summary>
        [Description("未付款")]
        NoPay = 1,

        /// <summary>
        /// 部分付款
        /// </summary>
        [Description("部分付款")]
        PartPay = 2,

        /// <summary>
        /// 全额付款
        /// </summary>
        [Description("全额付款")]
        FullPay = 3
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年6月20日
    /// Description:调整科目
    /// </summary>
    public enum EnumAdjustKind : byte
    {
        /// <summary>
        /// 全选
        /// </summary>
        [Description("")]
        Default = 0,

        #region- 应收类型 -
        /// <summary>
        /// 冲销（正数/负数）
        /// </summary>
        [Description("冲销")]
        WriteOff = 2,

        /// <summary>
        /// 减免（正数/负数）
        /// </summary>
        [Description("减免")]
        Mitigation = 3,
        #endregion

        #region- 实收类型 -
        /// <summary>
        /// 转帐（正数/负数）
        /// </summary>
        [Description("转账")]
        Transfer = 11,

        /// <summary>
        /// 银扣（正数）
        /// </summary>
        [Description("银扣")]
        BankSupport = 12,

        /// <summary>
        /// 减免（正数）
        /// </summary>
        [Description("减免")]
        Exemption = 16,

        /// <summary>
        /// 富友（正数）
        /// </summary>
        [Description("富友")]
        Fuiou = 22,

        /// <summary>
        /// 坏帐（正数）
        /// </summary>
        [Description("坏账")]
        BadDebts = 13,

        /// <summary>
        /// 冲销（负数）
        /// </summary>
        [Description("冲销")]
        Correct = 17,

        /// <summary>
        /// 退回（负数）
        /// </summary>
        [Description("退回")]
        ReturnBack = 28,

        /// <summary>
        /// 财付通(正数)
        /// </summary>
        [Description("财付通")]
        TenPay = 30,

        #endregion

        #region- 应实收阈值 -
        /// <summary>
        /// 应实收分割线（应收款项：类型 < 阈值 实收款项：类型 > 阈值）
        /// </summary>
        [Description("阈值")]
        Thresholds = 10
        #endregion
    }
}

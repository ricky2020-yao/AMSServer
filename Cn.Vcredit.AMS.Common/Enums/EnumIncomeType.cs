using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// 受益方类型
    /// </summary>
    public enum EnumIncomeType : byte
    {
        Default = 0,
        /// <summary>
        /// 银扣服务方
        /// </summary>
        [Description("服务方收益")]
        BankIncomeServ = 1,

        /// <summary>
        /// 银扣信托方
        /// </summary>
        [Description("信托方收益")]
        BankIncomeLend = 2,

        /// <summary>
        /// 每日扣外贸正常客户
        /// </summary>
        [Description("每日扣外贸正常客户")]
        Payment_DWJM_Normal = 3,

        /// <summary>
        /// 每日扣非外贸正常客户收益
        /// </summary>
        [Description("每日扣非外贸正常客户收益")]
        PayDay_Other_All = 4,

        /// <summary>
        /// 相对日收款类型
        /// </summary>
        [Description("成都相对日收款类型")]
        RelativeDay = 5,

        /// <summary>
        /// 银扣外贸担保方
        /// </summary>
        [Description("银扣外贸担保方")]
        BankIncomeGuarant = 6,
    }
}

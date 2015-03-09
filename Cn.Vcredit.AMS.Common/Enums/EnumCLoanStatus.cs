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
    /// Description:清贷状态
    /// </summary>
    public enum EnumCLoanStatus : byte
    {
		/// <summary>
		/// 全选
		/// </summary>
		[Description("")]
		Default = 0,

		/// <summary>
		/// 偿还中
		/// </summary>
        [Description("偿还中")]
        Refunding = 1,

		/// <summary>
		/// 满约清贷
		/// </summary>
        [Description("满约清贷")]
        Appointed = 2,

		/// <summary>
		/// 提前清贷
		/// </summary>
        [Description("提前清贷")]
		Advanced = 3,

		/// <summary>
		/// 坏帐清贷
		/// </summary>
        [Description("坏帐清贷")]
        BadDebtsed = 4,

		/// <summary>
		/// 提前清贷申请
		/// </summary>
		[Description("提前清贷申请")]
		Advancing = 5,

		/// <summary>
		/// 坏账申请
		/// </summary>
		[Description("坏账申请")]
		BadDebtsing = 6,

		/// <summary>
		/// 提前清贷中
		/// </summary>
		[Description("提前清贷中")]
		AdvanceProcess = 7,

		/// <summary>
        /// 注销订单
        /// </summary>
        [Description("注销订单")]
        Annul = 8
    }
}

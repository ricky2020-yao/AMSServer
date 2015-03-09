using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// Author:王正吉
    /// CreateTime:2012年9月29日
    /// Description:合同参与方(职责分类)
    public enum EnumContractSide : byte
    {
        /// <summary>
        /// 区别银行代号
        /// </summary>
        [Description("区别银行代号")]
        BankService = 0,

        /// <summary>
        /// 服务方
        /// </summary>
        [Description("服务方")]
        Service = 1,

        /// <summary>
        /// 担保方
        /// </summary>
        [Description("担保方")]
        Guarantee = 2,

        /// <summary>
        /// 信托方
        /// </summary>
        [Description("信托方")]
        Lend = 3,

        /// <summary>
        /// 管理方
        /// </summary>
        [Description("管理方")]
        Manage = 4,

        /// <summary>
        /// 放贷方
        /// </summary>
        [Description("放贷方")]
        Lending = 5
    }
}

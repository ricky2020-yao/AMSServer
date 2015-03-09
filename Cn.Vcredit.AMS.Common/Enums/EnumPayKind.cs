using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:支付平台种类
    /// </summary>
    public enum EnumPayKind : byte
    {
        /// <summary>
        /// 富友支付平台
        /// </summary>
        [Description("富友")]
        Payment_Fuiou = 1,

        ///// <summary>
        ///// 盛付通支付平台
        ///// </summary>
        //Payment_Shengpay = 2,

        /// <summary>
        /// 银扣方式
        /// </summary>
        [Description("银扣")]
        Payment_Bank = 3
    }
}

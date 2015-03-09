using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.Common.Enums
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月30日
    /// Description:数据加密类型
    /// </summary>
    public enum EnumEncyptionType
    {
        /// <summary>
        /// 无加密解密
        /// </summary>
        [Description("无加密解密")]
        None = 1,

        /// <summary>
        /// AES加密解密
        /// </summary>
        [Description("AES")]
        AES = 2,

        /// <summary>
        /// DES加密解密
        /// </summary>
        [Description("DES")]
        DES = 3,

        /// <summary>
        /// TripleDES加密解密
        /// </summary>
        [Description("TripleDES")]
        TripleDES = 4
    }
}

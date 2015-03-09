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
    /// Description:序列化类型的枚举
    /// </summary>
    public enum SerializeType:byte
    {
        /// <summary>
        /// XML序列化
        /// </summary>
        [Description("XML序列化")]
        Xml = 1,

        /// <summary>
        /// JSON序列化
        /// </summary>
        [Description("JSON序列化")]
        Json = 2,

        /// <summary>
        /// 二进制序列化
        /// </summary>
        [Description("二进制序列化")]
        Binary = 3,

        /// <summary>
        /// SOAP序列化
        /// </summary>
        [Description("SOAP序列化")]
        Soap = 4,

        /// <summary>
        /// 自定义序列化
        /// </summary>
        [Description("自定义序列化")]
        UserDefine = 255
    }
}

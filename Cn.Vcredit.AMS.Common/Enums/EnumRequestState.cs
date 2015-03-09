using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// 请求返回结果
    /// </summary>
    public enum EnumRequestState : byte
    {
        /// <summary>
        /// 已发送请求，未返回请求结果
        /// </summary>
        [Description("已发送请求，未返回请求结果")]
        Default = 0,

        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 1,

        /// <summary>
        /// 拒绝
        /// </summary>
        [Description("拒绝")]
        Reject = 2,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Failed = 3,

        /// <summary>
        /// 请求超时
        /// </summary>
        [Description("请求超时")]
        TimeOut = 4,

        /// <summary>
        /// 处理异常
        /// </summary>
        [Description("处理异常")]
        Error = 5,

        /// <summary>
        /// 回盘成功文件，签名不匹配
        /// </summary>
        [Description("回盘成功文件，签名不匹配")]
        SignError = 6
    }
}

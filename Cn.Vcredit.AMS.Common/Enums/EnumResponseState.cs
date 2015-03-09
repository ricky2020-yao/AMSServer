using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月30日
    /// Description:WebService响应状态表
    /// </summary>
    public enum EnumResponseState
    {
        /// <summary>
        /// 处理成功
        /// </summary>
        [Description("处理成功")]
        Success = 10000,

        /// <summary>
        /// 请求命令格式出错
        /// </summary>
        [Description("请求命令格式出错")]
        RequestCommandError = 10001,

        /// <summary>
        /// 超时
        /// </summary>
        [Description("超时")]
        TimeOut = 10002,

        /// <summary>
        /// 未查询到数据
        /// </summary>
        [Description("未查询到数据")]
        NoResult = 10003,

        /// <summary>
        /// 没有相应的服务被找到
        /// </summary>
        [Description("没有相应的服务被找到")]
        NoService = 10004,

        /// <summary>
        /// 其他错误
        /// </summary>
        [Description("其他错误")]
        Others = 99999
    }
}

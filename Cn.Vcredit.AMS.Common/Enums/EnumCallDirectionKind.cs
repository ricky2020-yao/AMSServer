using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年8月7日
    /// Description:请求类型
    /// </summary>
    public enum EnumCallDirectionKind : byte
    {
        /// <summary>
        /// Http直连
        /// </summary>
        TO_HTTPS_GATEWAY = 1,

        /// <summary>
        /// Ftp直连
        /// </summary>
        TO_FTP_GATEWAY = 2,

        /// <summary>
        /// 导出扣款指令
        /// </summary>
        Export_Pay = 3,

        /// <summary>
        /// 导入扣款指令
        /// </summary>
        Import_Pay = 4,
    }
}

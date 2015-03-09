using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description:扣款指令
    /// </summary>
    public class DeductCommand
    {
        /// <summary>
        /// 扣款指令编号
        /// </summary>
        public int CommandID { get; set; }

        /// <summary>
        /// 服务方编号
        /// </summary>
        public int ServiceSideID { get; set; }

        /// <summary>
        /// 贷款方编号
        /// </summary>
        public int LendingSideID { get; set; }

        /// <summary>
        /// 导出TXT格式（银行相关）
        /// </summary>
        public byte ExportCmdType { get; set; }

        /// <summary>
        /// 导出明细格式（银行相关）
        /// </summary>
        public byte ExportDetailCmdType { get; set; }

        /// <summary>
        /// 解析格式(银行相关)
        /// </summary>
        public byte AnalysisCmdType { get; set; }
    } 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月16日
    /// Description:数据导出基础类
    /// </summary>
    public class BaseExportFilter:BaseFilter
    {
        /// <summary>
        /// 导出文件的标题列表，每个标题用","隔开
        /// </summary>
        public string Titles { get; set; }

        /// <summary>
        /// 导出文件的字段列表，每个字段用","隔开
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 导出文件的类型
        /// </summary>
        public int ExportFileType { get; set; }
    }
}

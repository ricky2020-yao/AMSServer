using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年6月8日
    /// Description:扣款指令导入回盘格式类型
    /// </summary>
    public enum EnumImportCmdKind : byte
    {
        /// <summary>
        /// 第一行无标题 匹配位置{0，1，2，3}
        /// </summary>
        [Description("第一行无标题 匹配位置{0，1，2,3}")]
        Template1 = 11,

        /// <summary>
        /// 第一行有标题 匹配位置{1，2，3，5}
        /// </summary>
        [Description("第一行有标题 匹配位置{1，2，3，5}")]
        Template2 = 21,

        /// <summary>
        /// 第一行有标题 匹配位置{1，2，3，4}
        /// </summary>
        [Description("第一行有标题 匹配位置{1，2，3，4}")]
        Template3 = 31,

        /// <summary>
        /// 杭州工行导入
        /// </summary>
        [Description("杭州工行导入")]
        Template4 = 41,

        /// <summary>
        /// 杭州光大导入
        /// </summary>
        [Description("杭州光大导入")]
        Template5 = 51,
        /// <summary>
        /// 杭州工商外贸导入
        /// </summary>
        [Description("杭州工商外贸导入")]
        Template6 = 61,
    }
}

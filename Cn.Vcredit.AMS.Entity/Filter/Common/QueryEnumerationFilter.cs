using Cn.Vcredit.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.Common
{
    /// <summary>
    /// 查询枚举的Filter
    /// FullKey查询优先级高于EnumID,如果FullKey有值,则优先使用FullKey查询
    /// </summary>
    public class QueryEnumerationFilter:BaseFilter
    {
        /// <summary>
        /// 枚举的FullKey,多个以,分隔
        /// </summary>
        public string FullKey { get; set; }

        /// <summary>
        /// 枚举ID,多个以,分隔 
        /// </summary>
        public string EnumID { get; set; }

        /// <summary>
        /// 获取Enumeration的类型
        /// </summary>
        public int GetEnumType { get; set; }

    }
}

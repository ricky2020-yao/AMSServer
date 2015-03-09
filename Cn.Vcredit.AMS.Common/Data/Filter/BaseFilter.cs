using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Data.Filter
{
    /// <summary>
    /// Author:王书行
    /// CreateTime:2014年8月13日
    /// Description:条件基础类
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// 一页数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页编号，从1开始
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// 排序字段，多个逗号隔开，例如 field1 asc,field2 desc
        /// </summary>
        public string OrderbyStr { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public long RecordCount { get; set; }

        /// <summary>
        /// 记录开始的下标
        /// </summary>
        public int FromIndex { get; set; }
        /// <summary>
        /// 记录结束的下标
        /// </summary>
        public int ToIndex { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
    }
}

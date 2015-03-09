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
    /// Description:数据压缩类型
    /// </summary>
    public enum EnumCompressType
    {
		/// <summary>
		/// 无压缩
		/// </summary>
        [Description("无压缩")]
        None = 1,

		/// <summary>
		/// 内存压缩方式
        /// </summary>
        [Description("内存压缩方式")]
        MemCompress = 2,

		/// <summary>
		/// GZIP方式
        /// </summary>
        [Description("GZIP方式")]
        GZIP = 3
    }
}

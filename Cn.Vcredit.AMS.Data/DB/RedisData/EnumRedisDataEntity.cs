using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.RedisData
{
    /// <summary>
    /// 枚举值的Redis类型
    /// </summary>
    public class EnumRedisDataEntity:RedisDataEntity
    {
        /// <summary>
        /// EnumID
        /// </summary>
        public int EnumID { get; set; }

        /// <summary>
        /// Super
        /// </summary>
        public int Super { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 显示的顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsDisable { get; set; }

        /// <summary>
        /// 是否可删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 父节点的FullKey
        /// </summary>
        public string SuperFullKey { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }
    }
}

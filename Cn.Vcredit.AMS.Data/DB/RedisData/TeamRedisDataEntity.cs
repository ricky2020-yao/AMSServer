using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.RedisData
{
    /// <summary>
    /// 团队实体类
    /// </summary>
    public class TeamRedisDataEntity:RedisDataEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ParentId
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 显示的顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 父节点的FullKey
        /// </summary>
        public string ParentFullKey { get; set; }

        /// <summary>
        /// IsActive
        /// </summary>
        public int IsActive { get; set; }
    }
}

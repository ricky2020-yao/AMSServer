using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    [MongoTableNameAtrr("Mongo.Sys_UserRegion")]
    public class MongoUserRegion : MongoDataEntity
    {
        /// <summary>
        /// 递增序列号
        /// </summary>
        public int SerialId{get;set;}

        /// <summary>
        /// 分部ID
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 分部名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 分部ID
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 城市主键
        /// </summary>
        public string RegionKey { get; set; }
        /// <summary>
        /// 城市全主键
        /// </summary>
        public string FullKey { get; set; }
        /// <summary>
        /// 城市权限键-过度
        /// </summary>
        public string PermissionKey { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 最后修改人Id
        /// </summary>
        public int OperatorId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 排列顺序
        /// </summary>
        public int DisplayOrder {get;set;}
        /// <summary>
        /// 分部名称
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 记录子节点数量
        /// </summary>
        public int ChildCount { get; set; }
        /// <summary>
        /// 组织类型ID
        /// </summary>
        public int OrgTypeId { get; set; }
        /// <summary>
        /// 人员ID 
        /// </summary>
        public int UserId { get; set; }

    }
}

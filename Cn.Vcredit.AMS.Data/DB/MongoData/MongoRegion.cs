using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 地区
    /// </summary>
    [MongoTableNameAtrr("Mongo.Sys_Region")]
    public class MongoRegion : MongoDataEntity
    {
        public int serialId { get; set; }
        public int? cityId { get; set; }
        public string cityName { get; set; }
        public int? parentId { get; set; }
        public bool isActive { get; set; }
        public string RegionKey { get; set; }
        public string FullKey { get; set; }
        public string PermissionKey { get; set; }
        public int? operatorId { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime updateTime { get; set; }
        public int? DisplayOrder { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreaTime { get; set; }
        public int? updateuser { get; set; }

    }
}

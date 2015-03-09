using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    [MongoTableNameAtrr("Mongo.Sys_Store")]
    public class MongoStore : MongoDataEntity
    {
        public int serialId { get; set; }
        public string storeName { get; set; }
        public int? attributeId { get; set; }
        public int? parentId { get; set; }
        public bool isActive { get; set; }
        public string StoreKey { get; set; }
        public string FullKey { get; set; }
        public string PermissionKey { get; set; }
        public int? operatorId { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime updateTime { get; set; }
        public int? DisplayOrder { get; set; }
        public int? StoreType { get; set; }
        public int? parentStoreId { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime openedDate { get; set; }
        public string address { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreaTime { get; set; }
        public int? updateuser { get; set; }

    }
}

using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 分部信息
    /// </summary>
     [MongoTableNameAtrr("Mongo.Sys_DivisionV")]
    public class MongoDivisionV : MongoDataEntity
    {
        public int serialId { get; set;}
        public string divisionName { get; set;}
        
        public int cityId { get; set;}
        public bool isActive { get; set;}
        public string DivisionKey { get; set;}
        public string FullKey { get; set;}
        public string PermissionKey  { get; set;}
        public int? operatorId { get; set;}
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? updateTime { get; set;}
        public int? DisplayOrder { get; set;}
        public int? ChildCount { get; set;}
        public int? OrgTypeId  { get; set;}
        public int? DivisionType { get; set; }
    }
}

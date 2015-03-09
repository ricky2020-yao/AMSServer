using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 订单来源代码
    /// </summary>
    [MongoTableNameAtrr("Mongo.Sys_CustomerSourceKind")]
    public class MongoCustomerSourceKind : MongoDataEntity
    {
        /// <summary>
        /// 订单来源ID
        /// </summary>
        public int KindId { get; set; }
        /// <summary>
        /// 订单来源Code
        /// </summary>
        public string KindCode { get; set; }
        /// <summary>
        /// 订单来源名称
        /// </summary>
        public string KindName { get; set; }
        /// <summary>
        /// 父编号
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Updatetime { get; set; }
      
    }
}

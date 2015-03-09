using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 销售渠道代码
    /// </summary>
    [MongoTableNameAtrr("Mongo.Sys_SaleMode")]
    public class MongoSaleMode : MongoDataEntity
    {
        /// <summary>
        /// 销售渠道ID
        /// </summary>
        public int ModeId{ get; set; }
        /// <summary>
        /// 销售渠道Code
        /// </summary>
        public string ModeCode{ get; set; }
        /// <summary>
        /// 销售渠道名称
        /// </summary>
        public string ModeName { get; set; }
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

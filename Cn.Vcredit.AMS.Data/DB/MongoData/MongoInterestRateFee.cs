using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 订单利率费用代码表
    /// </summary>
    [MongoTableNameAtrr("Mongo.Sys_InterestRateFee")]
    public class MongoInterestRateFee : MongoDataEntity
    {
        /// <summary>
        /// 订单利率费用ID
        /// </summary>
        public int InterestRateFeeId { get; set; }
        /// <summary>
        /// 订单利率费用名称
        /// </summary>
        public string InterestRateFeeName { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Updatetime { get; set; }

    }
}

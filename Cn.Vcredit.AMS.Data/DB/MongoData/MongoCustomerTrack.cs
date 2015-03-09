using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 客户跟踪信息
    /// </summary>
    [MongoTableNameAtrr("Mongo.Suf_CustomerTrack")]
    public class MongoCustomerTrack : MongoDataEntity
    {
        /// <summary>
        /// 客户事件编号
        /// </summary>
        public int CustomersTrackID { get; set; }
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime TrackStartTime { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public byte TrackType { get; set; }
        /// <summary>
        /// 函件
        /// </summary>
        public int Letter { get; set; }
        /// <summary>
        /// 其他联系人
        /// </summary>
        public string OtherContacts { get; set; }
        /// <summary>
        /// 其他情况
        /// </summary>
        public string OtherConditions { get; set; }
        /// <summary>
        /// 访问情况
        /// </summary>
        public byte TrackSituation { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperaterID { get; set; }
        /// <summary>
        /// 逾期原因
        /// </summary>
        public string DueReason { get; set; }
        /// <summary>
        /// 有效联络Code
        /// </summary>
        public string VaildCode { get; set; }
        /// <summary>
        /// 非有效联络Code
        /// </summary>
        public string UnVaildCode { get; set; }
        /// <summary>
        /// 是否同步推送值奥信(0、是 1、否)
        /// </summary>
        public bool IsSynch { get; set; }
        /// <summary>
        /// 承诺还款金额
        /// </summary>
        public decimal PayAmt { get; set; }
        /// <summary>
        /// 承诺还款日期
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime PayDate { get; set; }

        public byte[] TIMESTAMP { get; set; }
    }
}

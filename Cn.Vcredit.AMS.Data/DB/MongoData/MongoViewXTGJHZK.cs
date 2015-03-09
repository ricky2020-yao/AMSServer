using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 信托归集户账款核对
    /// </summary>
    [MongoTableNameAtrr("Mongo.Suf_XTGJHZK")]
    public class MongoViewXTGJHZK:MongoDataEntity
    {
        /// <summary>
        /// 储蓄卡号 
        /// </summary>
        public string SavingCard { get; set; }
        /// <summary>
        /// 账户名 
        /// </summary>
        public string SavingUser { get; set; }
        /// <summary>
        /// 合同号 
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 业务状态 
        /// </summary>
        public short BusinessStatus { get; set; }
        /// <summary>
        /// 产品类型 
        /// </summary>
        public short ProductType { get; set; }
        /// <summary>
        /// 放款方 
        /// </summary>
        public int LendingSide { get; set; }
        /// <summary>
        /// 服务方 
        /// </summary>
        public int ServiceSide { get; set; }
        /// <summary>
        /// 账单月 
        /// </summary>
        public int BillMonth { get; set; }
        /// <summary>
        /// 科目类型 
        /// </summary>
        public short Subject { get; set; }
        /// <summary>
        /// 实收编号 
        /// </summary>
        public long ReceivedID { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 实收/调整类型 
        /// </summary>
        public short ReceivedType { get; set; }
        /// <summary>
        /// 收款时间 
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ReceivedTime { get; set; }
        /// <summary>
        /// 到账时间 
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ToAcountTime { get; set; }
        /// <summary>
        /// 创建时间 
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 支付编号 
        /// </summary>
        public int PayID { get; set; }
        /// <summary>
        /// 地区 
        /// </summary>
        public int Region { get; set; }
        /// <summary>
        /// 收款账号 
        /// </summary>
        public int? AccountID { get; set; }
        /// <summary>
        /// 更新时间 
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Updatetime { get; set; }

    }
}

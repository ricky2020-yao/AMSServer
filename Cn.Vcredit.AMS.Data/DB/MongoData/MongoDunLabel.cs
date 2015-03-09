using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 催收标签信息
    /// </summary>
    [MongoTableNameAtrr("Mongo.Suf_DunLabel")]
    public class MongoDunLabel : MongoDataEntity
    {
        /// <summary>
        /// 标签编号
        /// </summary>
        public long LabelID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 标签编号
        /// </summary>
        public string LabelCode { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string LabelName { get; set; }
        /// <summary>
        /// 创建登录名
        /// </summary>
        public string CreatePerson { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public long PersonID { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string PersonName { get; set; }
        /// <summary>
        /// 最后处理人
        /// </summary>
        public string LastDoPerson { get; set; }

    }
}

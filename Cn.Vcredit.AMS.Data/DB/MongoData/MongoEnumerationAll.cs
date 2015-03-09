using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 枚举表
    /// </summary>
    [MongoTableNameAtrr("Mongo.Sys_EnumerationAll")]
    public class MongoEnumerationAll : MongoDataEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public int? Super { get; set; }
        /// <summary>
        /// 本key
        /// </summary>
        public string EnumKey { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public byte EnumType { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? DisplayOrder { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsDisable { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 老编号
        /// </summary>
        public int? OldID { get; set; }
        /// <summary>
        /// 所有值
        /// </summary>
        public string FullKey { get; set; }
        /// <summary>
        /// 金牌经办人
        /// </summary>
        public int? EnumBusType { get; set; }
        /// <summary>
        /// 流程类型
        /// </summary>
        public int? FlowType { get; set; }

    }
}

using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    [MongoTableNameAtrr("Mongo.Sys_UserPermission")]
    public class MongoUserPermission : MongoDataEntity
    {
        /// <summary>
        /// RolePermission的id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 账户编号
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// fullkey
        /// </summary>
        public string FullKey { get; set; }
        /// <summary>
        /// 是否拒绝权限
        /// </summary>
        public bool RefusedToHave { get; set; }
        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限描述
        /// </summary>
        public string Description  { get; set; }
        /// <summary>
        /// 权限key
        /// </summary>
        public string PermissionKey { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Updatetime { get; set; }

    }
}

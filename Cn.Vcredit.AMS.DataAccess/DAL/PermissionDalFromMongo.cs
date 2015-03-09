using Cn.Vcredit.AMS.Data.Cache.Data;
using Cn.Vcredit.AMS.Data.DB.MongoData;
using Cn.Vcredit.AMS.DataAccess.Common;
using Cn.Vcredit.AMS.DataAccess.Mongo;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// 从mongo取权限信息
    /// </summary>
    public class PermissionDalFromMongo
    {
        /// <summary>
        /// 根据用户Id,查询组织机构权限
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IList<AccountOrgRelation> GetAccountOrgRelationListByUserId(int userId)
        {
            string mongoStr = "{userid:"+ userId + "}";
            return Singleton<BaseMongo>.Instance.QueryByJson<AccountOrgRelation>
                (mongoStr, DataAccessConsts.MongoTable_AccountOrgRelation,string.Empty);
        }

        /// <summary>
        /// 根据用户登录ID，查询用户权限(弃用zzb)
        /// </summary>
        /// <param name="userId">用户登录ID</param>
        /// <param name="isHave">true-拥有；false-拒绝</param>
        /// <returns>用户权限列表</returns>
        public IList<string> GetUserPermissionFullkeyList(int userId, bool isHave)
        {
            List<MongoUserPermission> userPermissionList = GetUserPermissionList(userId, isHave);
            return userPermissionList.Select(a => a.FullKey).Distinct().ToList();
        }

        /// <summary>
        /// 根据用户登录ID，查询用户权限
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="isHave">true-拥有；false-拒绝</param>
        /// <returns>用户权限列表</returns>
        public List<MongoUserPermission> GetUserPermissionList(int userId, bool isHave)
        {
            StringBuilder sbl = new StringBuilder();
            sbl.Append("{UserId:");
            sbl.Append(userId);
            sbl.Append(",RefusedToHave:");
            if (isHave)
                sbl.Append("true } ");
            else
                sbl.Append("false } ");

           return Singleton<BaseMongo>.Instance.QueryByJson<MongoUserPermission>
                (sbl.ToString(), DataAccessConsts.MongoTable_UserPermission, string.Empty);

        }
    }
}

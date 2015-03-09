using Cn.Vcredit.AMS.Data.Cache.Data;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月18日
    /// Description:分布组织结构数据层
    /// </summary>
    public class PermissionDal:BaseDao
    {
        #region 获取用户组织结构权限
        /// <summary>
        /// 获取用户的组织架构权限，不循环递归
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<string> GetUserOrgList(int userId)
        {
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@userId", userId);
            param.Add("@type", 1);

            List<string> list = Query<string>("[user].proc_user_GetAllUserOrgList"
                , param, "SysDB", CommandType.StoredProcedure);
            return list;
        }

        /// <summary>
        ///  获取用户的组织架构权限,循环递归
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<string> GetAllUserOrgList(int userId)
        {
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@userId", userId);
            param.Add("@type", 2);
            List<string> list = Query<string>("[user].proc_user_GetAllUserOrgList"
                , param, "SysDB", CommandType.StoredProcedure);
            return list;
        }

        /// <summary>
        /// 重置人员组织权限（批量）
        /// </summary>
        /// <param name="userids"></param>
        /// <param name="orgids"></param>
        /// <returns></returns>
        public int SaveUserOrgs(string userids, string orgids, int operatorid)
        {
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@userids", userids);
            param.Add("@orgids", orgids);
            param.Add("@operatorid", operatorid);
            List<int> list = Query<int>("[user].[proc_user_SaveUserOrgS]"
                , param, "SysDB", CommandType.StoredProcedure);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return 0;
        }

        /// <summary>
        /// 获取用户的组织架构权限，不循环递归
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<PermissionV> GetUserOrgS(int userId)
        {
            IList<PermissionV> list = new List<PermissionV>();
            string sql = @"select  [Id]
                                  ,[accountId]
                                  ,[UserId]
                                  ,[orgId]
                                  ,[OrgTypeId]
                                  ,[operatorId]
                                  ,[updateTime]
                                  ,OrgName
                                  ,OrgIsSub
                            FROM  [user].[vw_UsersOrg] with(nolock)  WHERE USERID=@UserId order by  [Id] ";
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UserId", userId);
            list = Query<PermissionV>(sql, param, "SysDB");
            return list;
        }

        /// <summary>
        /// 根据用户登录ID，查询用户权限(弃用zzb)
        /// </summary>
        /// <param name="userId">用户登录ID</param>
        /// <param name="IsHave">true-拥有；false-拒绝</param>
        /// <returns></returns>
        public IList<string> GetUserPermissionFullkeyList(int userId, bool IsHave)
        {
            IList<string> list = new List<string>();
            string sql = @"SELECT 
                                distinct
                                [Extent3].[FullKey] AS [FullKey]
                                FROM   [user].[RolePermission] AS [Extent1]
                                INNER JOIN [user].[RoleAccount] AS [Extent2] ON [Extent1].[RoleId] = [Extent2].[RoleId]
                                INNER JOIN [user].[Permission] AS [Extent3] ON [Extent1].[PermissionKey] = [Extent3].[FullKey]
                                WHERE ([Extent2].[AccountId] = @UserId)";
            if (IsHave)
            {
                sql += " and ([Extent1].[RefusedToHave] = 1)";
            }
            else
            {
                sql += " and ([Extent1].[RefusedToHave] =0)";
            }
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UserId", userId);
            list = Query<string>(sql, param, "SysDB");
            return list;
        }

        /// <summary>
        /// 根据用户Id,查询组织机构权限
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IList<AccountOrgRelation> GetAccountOrgRelationListByUserId(int userId)
        {
            IList<AccountOrgRelation> list = new List<AccountOrgRelation>();
            string sql = @"select  *
                           FROM  [user].[AccountOrgRelation] with(nolock)  
                           WHERE userid=@UserId ";
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@UserId", userId);
            list = Query<AccountOrgRelation>(sql, param, "SysDB");
            return list;
        }

        
        #endregion
    }
}

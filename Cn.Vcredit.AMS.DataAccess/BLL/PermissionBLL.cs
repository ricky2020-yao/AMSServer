using Cn.Vcredit.AMS.Data.Cache.Data;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月18日
    /// Description:获取用户组织结构权限操作类
    /// </summary>
    public class PermissionBLL
    { 
        /// <summary>
        /// 获取用户的组织架构权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<string> GetUserOrgList(int userId)
        {
            return Singleton<PermissionDal>.Instance.GetUserOrgList(userId);      
        }

        /// <summary>
        /// 获取用户的组织架构权限,循环递归
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<string> GetAllUserOrgList(int userId)
        {
            return Singleton<PermissionDal>.Instance.GetAllUserOrgList(userId);
        }

        /// <summary>
        /// 重置人员组织权限（批量）
        /// </summary>
        /// <param name="userids"></param>
        /// <param name="orgids"></param>
        /// <returns></returns>
        public int SaveUserOrgs(string userids, string orgids, int operatorid)
        {
            return Singleton<PermissionDal>.Instance.SaveUserOrgs(userids, orgids, operatorid);
        }

        /// <summary>
        /// 获取用户的组织架构权限，不循环递归
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<PermissionV> GetUserOrgS(int userId)
        {
            return Singleton<PermissionDal>.Instance.GetUserOrgS(userId);
        }

        /// <summary>
        /// 根据用户登录ID，查询用户权限
        /// </summary>
        /// <param name="userId">用户登录ID</param>
        /// <param name="IsHave">true-拥有；false-拒绝</param>
        /// <returns></returns>
        public IList<string> GetUserPermissionFullkeyList(int userId, bool IsHave)
        {
            //return Singleton<PermissionDal>.Instance.GetUserPermissionFullkeyList(userId, IsHave);
            return Singleton<PermissionDalFromMongo>.Instance.GetUserPermissionFullkeyList(userId, IsHave);
        }

        /// <summary>
        /// 根据用户Id,查询组织机构权限
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IList<AccountOrgRelation> GetAccountOrgRelationListByUserId(int userId)
        {
            //return Singleton<PermissionDal>.Instance.GetAccountOrgRelationListByUserId(userId);
            return Singleton<PermissionDalFromMongo>.Instance.GetAccountOrgRelationListByUserId(userId);
        }
    }
}

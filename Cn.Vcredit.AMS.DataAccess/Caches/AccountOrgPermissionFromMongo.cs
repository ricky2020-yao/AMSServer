using Cn.Vcredit.AMS.Data.Cache.Data;
using Cn.Vcredit.AMS.DataAccess.BLL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.Caches
{
    public class AccountOrgPermissionFromMongo
    {
        /// <summary>
        /// 根据用户Id,查询组织机构权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<AccountOrgRelation> GetAccountOrgRelationListByUserId(int userId)
        {
            //if (DicAccountOrgPermission == null)
            //    DicAccountOrgPermission = new ConcurrentDictionary<int, IList<AccountOrgRelation>>();

            //IList<AccountOrgRelation> perms = new List<AccountOrgRelation>();
            //if (!DicAccountOrgPermission.TryGetValue(userId, out perms))
            //{
            //    perms = Singleton<PermissionBLL>.Instance.GetAccountOrgRelationListByUserId(userId);
            //    DicAccountOrgPermission.TryAdd(userId, perms);
            //}
            return null;
        }
    }
}

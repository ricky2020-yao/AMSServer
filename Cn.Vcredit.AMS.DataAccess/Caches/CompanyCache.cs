using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Data.Cache.Data;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Data.DB.MongoData;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.Caches
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月29日
    /// Description:公司缓存
    /// </summary>
    public class CompanyCache
    {
        /// <summary>
        /// 公司缓存
        /// </summary>
        public List<Enumeration> CompanyEnumeration { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public CompanyCache()
        {
            //CompanyEnumeration = Singleton<EnumerationDal>.Instance.GetEnumerations(Const.COMPANY);

            //foreach (Enumeration e in CompanyEnumeration)
            //{
            //    e.AllChild = Singleton<EnumerationCache>.Instance
            //        .RecurAllChild(e).OrderBy(a => a.DisplayOrder).ToList();
            //}
        }

        /// <summary>
        /// 获取登录用户地区权限管理的子公司FullKey
        /// </summary>
        /// <param name="userId">userId</param>
        public List<string> GetCompanyKeys(int userId)
        {
            return GetCompanyEnumeration(userId).Select(p => p.FullKey).ToList();

        }

        /// <summary>
        /// 得到有权限公司字典信息
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns>Enumeration</returns>
        public IEnumerable<Enumeration> GetCompanyEnumeration(int userId)
        {
            var partitionsOfAllow = GetRegionList(userId);
            var allcompany = Singleton<EnumerationCache>.Instance.GetEnumerationByKey(Const.COMPANY);

            /*这个修改为AllChild,因为在Compny这个父层下面,原来的子公司划分为两个父级,这两个父级在Compny下面
            原先结构           修改后结构
            company           company
               --子公司            --贷款方
                                    --子公司              
                                  --服务费  
                                    --子公司*/
            if (allcompany == null || allcompany.AllChild.Count == 0)
                return null;

            return allcompany.AllChild.Where(p => partitionsOfAllow.Contains(p.Region.ToUpper()));
        }

        /// <summary>
        /// 获取登录用户地区权限管理的子公司集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<int> CompanyIds(int userId)
        {
            List<string> companyKeys = GetCompanyKeys(userId);
            return Singleton<EnumerationDal>.Instance.GetCompanyIds(companyKeys);
        }

        /// <summary>
        /// 获取用户所拥有访问权限的地区集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<string> GetRegionList(int userId)
        {
            List<string> result = new List<string>();
            GetRegionKeyListByPermission(userId)
                .ForEach(p => { result.Add(p.Replace("SUBSIDIARY", "REGION").ToUpper()); });
            return result;
        }

        /// <summary>
        /// 获取用户所拥有访问权限的地区集合
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>地区集合</returns>
        public List<RegionV> GetRegionVList(int userId)
        {
            List<RegionV> regionVList = GetRegionListByPermission(userId);
            regionVList.ForEach(r => r.FullKey = r.FullKey.Replace("SUBSIDIARY", "REGION").ToUpper());
            return regionVList;
        }

        /// <summary>
        /// 可访问的区域key
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<string> GetRegionKeyListByPermission(int userId)
        {
            List<string> strList = new List<string>();
            strList = GetRegionListByPermission(userId).Select(o => o.FullKey).ToList();
            return strList;
        }

        /// <summary>
        /// 获取登录人区域权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RegionV> GetRegionListByPermission(int userId)
        {
            IList<AccountOrgRelation> perms
                = Singleton<AccountOrgPermissionCache>.Instance
                .GetAccountOrgRelationListByUserId(userId).Where(o => o.OrgLevel == 2).ToList();
            IList<RegionV> regionAllList = Singleton<RegionCache>.Instance.GetRegionListAll();
            IList<DivisionV> divisionList = GetDivisionListByPermission(userId);
            IList<RegionV> regionList = null;
            IList<RegionV> bRegions = null;

            //分配的区域权限
            regionList = (from q in regionAllList
                          join q1 in perms
                          on q.SerialId equals q1.OrgId
                          select q).ToList();
            //分部对应的区域权限
            bRegions = (from q in regionAllList
                        join q1 in divisionList
                        on q.ParentId equals q1.SerialId
                        select q).Distinct().ToList();
            //合并
            regionList = regionList.Union(bRegions).ToList();
            if (regionList != null)
            {
                return regionList.ToList();
            }
            return new List<RegionV>();
        }

        /// <summary>
        /// 获取登录人分公司权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<DivisionV> GetDivisionListByPermission(int userId)
        {
            IList<AccountOrgRelation> perms = Singleton<AccountOrgPermissionCache>.Instance
                .GetAccountOrgRelationListByUserId(userId).Where(o => o.OrgLevel == 1).ToList();
            IList<DivisionV> divisionList = Singleton<DivisionCache>.Instance.DivisionVCache;

            divisionList = (from q in divisionList
                            join q1 in perms
                            on q.SerialId equals q1.OrgId
                            select q).ToList();
            if (divisionList != null)
            {
                return divisionList.ToList();
            }
            return new List<DivisionV>();
        }

        /// <summary>
        /// 得到用户所拥有的权限
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>所拥有的权限</returns>
        public List<MongoUserPermission> GetPermission(int userId)
        {
            //根据账户ID查找拥有和角色的权限
            List<MongoUserPermission> refusedPermissions = Singleton<PermissionDalFromMongo>.Instance.GetUserPermissionList(userId, false);
            List<MongoUserPermission> ownPermissions = Singleton<PermissionDalFromMongo>.Instance.GetUserPermissionList(userId,true);

            //将账户所能使用的权限列表
            //账户权限只在权限检查时使用
            if (refusedPermissions.Count == 0)
            {
                return ownPermissions.Distinct().ToList();
            }
            else
            {
                return ownPermissions.SelectMany(
                    own => refusedPermissions.Where(
                        refused => refused.Id != own.Id)).Distinct().ToList();
            }
        }
    }
}

using Cn.Vcredit.AMS.DictionaryService.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.Common;
using Cn.Vcredit.AMS.Entity.ViewData.Common;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Data.DB.RedisData;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BaseService.BLL;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;

namespace Cn.Vcredit.AMS.DictionaryService.BLL
{
    /// <summary>
    /// 查询枚举业务操作类
    /// </summary>
    public class QueryEnumerationBLL : BaseBLL
    {
        /// <summary>
        /// 查询枚举
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public void QueryEnumeration(BaseFilter baseFilter,ResponseEntity responseEntity)
        {
            QueryEnumerationFilter filter = baseFilter as QueryEnumerationFilter;

            if(null == filter)
            {
                responseEntity.ResponseStatus = (int)EnumResponseState.RequestCommandError;
                responseEntity.ResponseMessage = "获取枚举失败,入参校验错误";
                return;
            }

            if (string.IsNullOrEmpty(filter.FullKey))
            {
                if (string.IsNullOrEmpty(filter.EnumID))
                {
                    responseEntity.ResponseStatus = (int)EnumResponseState.RequestCommandError;
                    responseEntity.ResponseMessage = "获取枚举失败,FullKey和ID均无值";
                    return;
                }
            }
            else
            {
                //传入FullKey的话,优先使用FullKey作为查询条件
                filter.EnumID = string.Empty;

                // 获取所有的下拉框列表数据
                var lstEnumRedisDataEntity = Singleton<RedisEnumOperatorBLL>.Instance.GetEnumRedisDataEntitys<EnumRedisDataEntity>();
                var list = new List<EnumerationViewData>();
                string[] keys = filter.FullKey.Split(
                    WebServiceConst.Separater_Comma.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var key in keys)
                {
                    if (key == SysConst.TEAM)
                        list.AddRange(GetTeamEnumerationViewData(key).ToArray());
                    else if (key == SysConst.REGION_STORE)
                        list.AddRange(GetRegionStoreEnumerationViewData(key).ToArray());
                    else if (key == SysConst.REGION)
                        list.AddRange(GetRegionEnumerationViewData(key).ToArray());
                    else if (key == SysConst.STORE)
                        list.AddRange(GetStoreEnumerationViewData(key).ToArray());
                    else
                        list.AddRange(GetEnumerationViewData(key, lstEnumRedisDataEntity).ToArray());
                }

                if (null != list
                    && list.Count > 0)
                {
                    var responseResult = new ResponseListResult<EnumerationViewData>();
                    responseResult.TotalCount = list.Count;
                    responseResult.LstResult = list;
                    responseEntity.Results = responseResult;
                    responseEntity.ResponseStatus = (int)EnumResponseState.Success;
                    responseEntity.ResponseMessage = "处理成功";
                }
                else
                {
                    ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                    m_Logger.Info("未查询到数据！。");
                    return;
                }
            }
        }
        /// <summary>
        /// 获取下拉框枚举
        /// </summary>
        /// <param name="fullKey"></param>
        /// <returns></returns>
        public List<EnumerationViewData> GetTeamEnumerationViewData(string fullKey)
        {

            var lstEntityTemp = Singleton<RedisEnumOperatorBLL>.Instance.GetEnumRedisDataEntitys<TeamRedisDataEntity>();
            var lstEntity = lstEntityTemp.OrderBy(e => e.DisplayOrder).ToList();

            List<EnumerationViewData> list = new List<EnumerationViewData>();
            EnumerationViewData viewData = null;
            foreach (var entity in lstEntityTemp)
            {
                viewData = new EnumerationViewData();
                viewData.ID = entity.Id;
                viewData.Name = entity.Name;
                viewData.ParentID = entity.ParentId;
                viewData.IsEnable = entity.IsActive;
                viewData.GroupbyName = fullKey;
                viewData.FullKey = entity.Key;
                viewData.ParentFullKey = entity.ParentFullKey;
                list.Add(viewData);
            }

            return list;
        }

        /// <summary>
        /// 获取下拉框枚举
        /// </summary>
        /// <param name="fullKey"></param>
        /// <returns></returns>
        public List<EnumerationViewData> GetRegionEnumerationViewData(string fullKey)
        {

            var lstEntityTemp = Singleton<RedisEnumOperatorBLL>.Instance.GetEnumRedisDataEntitys<RegionRedisDataEntity>();
            var lstEntity = lstEntityTemp.OrderBy(e => e.DisplayOrder).ToList();

            List<EnumerationViewData> list = new List<EnumerationViewData>();
            EnumerationViewData viewData = null;
            foreach (var entity in lstEntityTemp)
            {
                viewData = new EnumerationViewData();
                viewData.ID = entity.EnumID;
                viewData.Name = entity.Name;
                viewData.ParentID = entity.Super;
                viewData.IsEnable = entity.IsDisable == true ? 0 : 1;
                viewData.GroupbyName = fullKey;
                viewData.FullKey = entity.Key;
                viewData.ParentFullKey = entity.SuperFullKey;
                list.Add(viewData);
            }

            return list;
        }
        /// <summary>
        /// 获取下拉框枚举
        /// </summary>
        /// <param name="fullKey"></param>
        /// <returns></returns>
        public List<EnumerationViewData> GetStoreEnumerationViewData(string fullKey)
        {

            var lstEntityTemp = Singleton<RedisEnumOperatorBLL>.Instance.GetEnumRedisDataEntitys<StoreRedisDataEntity>();
            var lstEntity = lstEntityTemp.OrderBy(e => e.DisplayOrder).ToList();

            List<EnumerationViewData> list = new List<EnumerationViewData>();
            EnumerationViewData viewData = null;
            foreach (var entity in lstEntityTemp)
            {
                viewData = new EnumerationViewData();
                viewData.ID = entity.EnumID;
                viewData.Name = entity.Name;
                viewData.ParentID = entity.Super;
                viewData.IsEnable = entity.IsDisable == true ? 0 : 1;
                viewData.GroupbyName = fullKey;
                viewData.FullKey = entity.Key;
                viewData.ParentFullKey = entity.SuperFullKey;
                list.Add(viewData);
            }

            return list;
        }

        /// <summary>
        /// 获取下拉框枚举
        /// </summary>
        /// <param name="fullKey"></param>
        /// <returns></returns>
        public List<EnumerationViewData> GetRegionStoreEnumerationViewData(string fullKey)
        {

            var lstEntityTemp = Singleton<RedisEnumOperatorBLL>.Instance.GetEnumRedisDataEntitys<RegionStoreRedisEntity>();
            var lstEntity = lstEntityTemp.OrderBy(e => e.DisplayOrder).ToList();

            List<EnumerationViewData> list = new List<EnumerationViewData>();
            EnumerationViewData viewData = null;
            foreach (var entity in lstEntityTemp)
            {
                viewData = new EnumerationViewData();
                viewData.ID = entity.Id;
                viewData.Name = entity.Name;
                viewData.ParentID = entity.ParentId;
                viewData.IsEnable = entity.IsActive;
                viewData.GroupbyName = fullKey;
                viewData.FullKey = entity.Key;
                viewData.ParentFullKey = entity.ParentFullKey;
                list.Add(viewData);
            }

            return list;
        }

        /// <summary>
        /// 获取下拉框枚举
        /// </summary>
        /// <param name="fullKey"></param>
        /// <param name="lstEnumRedisDataEntity"></param>
        /// <returns></returns>
        public List<EnumerationViewData> GetEnumerationViewData(string fullKey, List<EnumRedisDataEntity> lstEnumRedisDataEntity)
        {
            if (lstEnumRedisDataEntity == null || lstEnumRedisDataEntity.Count == 0)
                return new List<EnumerationViewData>();

            string key = fullKey;
            if (SysConst.LENDINGGROUP == fullKey
                || SysConst.SERVICEGROUP == fullKey
                || SysConst.GUARANTEEGROUP == fullKey
                || SysConst.FORELENDINGGROUP == fullKey)
                key = SysConst.COMPANY;

            List<EnumRedisDataEntity> lstEntityTemp = new List<EnumRedisDataEntity>();
            List<EnumRedisDataEntity> lstEntity = lstEnumRedisDataEntity
                .Where(x => x.SuperFullKey == key && x.IsDisable).OrderBy(e => e.DisplayOrder).ToList();
            switch (fullKey)
            {
                case SysConst.LENDINGGROUP:
                    lstEntityTemp = lstEntity.Where(x => x.Value.Contains("IsLending=1")).OrderBy(e => e.Name).ToList();
                    break;
                case SysConst.SERVICEGROUP:
                    lstEntityTemp = lstEntity.Where(x => x.Value.Contains("IsService=1")).OrderBy(e => e.Name).ToList();
                    break;
                case SysConst.GUARANTEEGROUP:
                    lstEntityTemp = lstEntity.Where(x => x.Value.Contains("IsGuarantee=1")).OrderBy(e => e.Name).ToList();
                    break;
                case SysConst.FORELENDINGGROUP:
                    lstEntityTemp = lstEntity.Where(x => x.Value.Contains("IsLending=1"))
                        .Where(o => o.Key == SysConst.COMPANY_DWJM_LENDING).OrderBy(p => p.Name).ToList();
                    break;
                default:
                    lstEntityTemp = lstEntity;
                    break;
            }

            string keyFieldName = "Key";
            if (SysConst.BUSINESSSTATUS == fullKey
                || SysConst.CLOANSTATUS == fullKey
                || SysConst.BILLSTATUS == fullKey
                || SysConst.LAWSUITSTATUS == fullKey)
                keyFieldName = "Value";

            List<EnumerationViewData> list = new List<EnumerationViewData>();
            EnumerationViewData viewData = null;
            foreach (var entity in lstEntityTemp)
            {
                viewData = new EnumerationViewData();
                viewData.ID = entity.EnumID;
                viewData.Description = entity.Description;
                if (keyFieldName == "Key")
                    viewData.FullKey = entity.Key;
                else if (keyFieldName == "Value")
                    viewData.FullKey = entity.Value;

                //viewData.FullKey = GetFullKey(keyFieldName, entity);
                viewData.Name = entity.Name;
                viewData.IsEnable = entity.IsDisable == true ? 0 : 1;
                viewData.ParentFullKey = fullKey;
                viewData.GroupbyName = fullKey;
                list.Add(viewData);
            }

            return list;
        }

        /// <summary>
        /// 获取FullKey
        /// </summary>
        /// <param name="keyFieldName"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GetFullKey(string keyFieldName, EnumRedisDataEntity entity)
        {
            if (entity == null)
                return "";

            Type type = typeof(EnumRedisDataEntity);
            PropertyInfo info = type.GetProperty(keyFieldName);

            if (info != null)
              return  info.GetValue(entity).ToString();

            return "";
        }
    }
}

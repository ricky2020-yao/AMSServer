using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BusinessService.DAL;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-9-18
    /// Description:订单详情信息逻辑处理类
    /// </summary>
    public class BusinessDetailBLL:BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(SearchBusinessListFilter baseFilter, ResponseEntity responseEntity)
        {
            if (baseFilter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            List<string> companys = Singleton<RedisEnumOperatorBLL>.Instance.GetUserOwnCompanyKeys(responseEntity.UserId);
            baseFilter.BranchKey = string.Join("','", companys.ToArray());

            var businessDetail
                = Singleton<BusinessDetailDAL<BusinessDetailViewData>>.Instance.SearchData(baseFilter);

            if (businessDetail == null || businessDetail.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                foreach (var business in businessDetail)
                {
                    business.LoanKindName = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_LoanKind_6, business.LoanKind).Name;
                    business.RegionName = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_Region_9, business.Region).Name;
                }

                var responseResult = new ResponseListResult<BusinessDetailViewData>();
                responseResult.TotalCount = businessDetail.Count;
                responseResult.LstResult = businessDetail;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }
    }
}

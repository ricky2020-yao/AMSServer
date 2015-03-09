using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.RelativeDaySearchDunService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.RelativeDaySearchDunService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-19
    /// Description:代偿卡信息查询逻辑处理类
    /// </summary>
    public class AdaptationCardSearchBLL : BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            var filter = baseFilter as SearchBusinessListFilter;
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            // 获取件数
            int totalCount = Singleton<DunAdaptationCardDAL<AdaptationCardDetailData>>.Instance.GetCount(baseFilter);
            if (totalCount <= 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
                return;
            }

            if (filter.PageNo < 1)
                filter.PageNo = 1;

            int fromIndex = (filter.PageNo - 1) * filter.PageSize;
            int toIndex = filter.PageNo * filter.PageSize;
            if (toIndex > totalCount)
                toIndex = totalCount;

            filter.FromIndex = fromIndex;
            filter.ToIndex = toIndex;

            var lstResult = Singleton<DunAdaptationCardDAL<AdaptationCardDetailData>>.Instance.SearchData(filter);

            if (lstResult == null || lstResult.Count < 2)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {  
                lstResult.ForEach(p =>
                        {
                            p.AdaBankName = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(RedisEnumOperatorBLL.HashId_BankList_8, p.AdaBankName).Name;
                        });

                var responseResult = new ResponseListResult<AdaptationCardDetailData>();
                responseResult.TotalCount = totalCount;
                responseResult.LstResult = lstResult;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }
    }
}

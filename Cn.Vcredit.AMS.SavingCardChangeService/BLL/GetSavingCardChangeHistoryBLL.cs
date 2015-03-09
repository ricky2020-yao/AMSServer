using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.SavingCardChangeService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.SavingCardChangeService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月29日
    /// Description:查询储蓄卡历史修改信息逻辑处理类
    /// </summary>
    public class GetSavingCardChangeHistoryBLL:BaseBLL
    {
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public virtual void SearchData(SavingCardChangeFilter filter
            , ResponseEntity responseEntity)
        {
            if (filter == null)
                return;

            var reportDetailList = Singleton<GetSavingCardChangeHistoryDAL<SavingCardChangeHistoryViewData>>
                .Instance.SearchData(filter);

            if (reportDetailList == null || reportDetailList.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                SetBranchName(reportDetailList);
                var responseResult = new ResponseListResult<SavingCardChangeHistoryViewData>();
                responseResult.TotalCount = reportDetailList.Count;
                responseResult.LstResult = reportDetailList;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }

        /// <summary>
        /// 设置门店的名称
        /// </summary>
        /// <param name="lstData"></param>
        private void SetBranchName(List<SavingCardChangeHistoryViewData> lstData)
        {
            foreach (var data in lstData)
            {
                data.BranckName = string.Format("{0}-{1}"
                    , Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_Region_9, data.Region).Name
                        , Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_Store_12, data.BranchKey).Name);
            }
        }
    }
}

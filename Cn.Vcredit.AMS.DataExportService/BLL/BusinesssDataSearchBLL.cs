using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.DataExportService.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月14日
    /// Description:订单筛选检索数据处理类
    /// </summary>
    public class BusinesssDataSearchBLL : BaseBLL
    {
        /// <summary>
        /// 根据过滤条件，返回检索数据(分页)
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        /// <returns></returns>
        public virtual void SearchDataPagingByFilter(
            BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            BusinesssDataSearchDAL<BusinessExportViewData> dal = new BusinesssDataSearchDAL<BusinessExportViewData>();
            // 获取件数
            int totalCount = dal.GetCount(baseFilter);

            if (totalCount <= 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
                return;
            }

            if (baseFilter.PageNo < 1)
                baseFilter.PageNo = 1;

            int fromIndex = (baseFilter.PageNo - 1) * baseFilter.PageSize;
            int toIndex = baseFilter.PageNo * baseFilter.PageSize;
            if (toIndex > totalCount)
                toIndex = totalCount;

            baseFilter.FromIndex = fromIndex;
            baseFilter.ToIndex = toIndex;
            var result = dal.SearchData(baseFilter);

            if (result == null || result.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {
                CreateViewBusinessExtPivot(result);
                var responseResult = new ResponseListResult<BusinessExportViewData>();
                responseResult.LstResult = result;
                responseResult.TotalCount = totalCount;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }

        /// <summary>
        /// 创建ViewBusinessExtPivot列表视图
        /// </summary>
        /// <param name="lstViewData">催收单信息</param>
        /// <returns>返回催收单视图</returns>
        private void CreateViewBusinessExtPivot(List<BusinessExportViewData> lstViewData)
        {
            foreach (var business in lstViewData)
            {
                business.BankKey = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_BankList_8, business.BankKey).Name;
                business.LoanKind = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_LoanKind_6, business.LoanKind).Name;
                business.ProductKind = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_ProductKind_7, business.ProductKind).Name;
                business.BusinessStatus = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_BusinessStatus_10, business.BusinessStatus).Name;
                business.CLoanStatus = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_CLoanStatus_11, business.CLoanStatus).Name;
                business.OverMonth = business.OverMonth.ValueToEnum<EnumDunMark>().ToString();
                business.ServiceSideKey = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_ServiceGroup_3, business.ServiceSideKey).Name;
                business.GuaranteeSideKey = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_GuaranteeGroup_5, business.GuaranteeSideKey).Name;
                business.LendingSideKey = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_LendingGroup_4, business.LendingSideKey).Name;
                business.BranchKey = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_Store_12, business.BranchKey).Name;
                business.SalesManID = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_User_2, business.SalesManID.ToString()).Name;
                business.ZClearLoanTime = business.LoanTime.AddMonths(business.LoanPeriod);
                business.ManagementRate = business.ManagementRate.HasValue ? business.ManagementRate.Value : 0;
            }
        }
    }
}

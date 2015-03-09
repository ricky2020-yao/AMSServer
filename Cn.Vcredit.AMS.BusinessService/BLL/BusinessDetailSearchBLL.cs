using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BusinessService.DAL;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.BLL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-11-28
    /// Description:订单详情-订单检索服务业务逻辑处理
    /// </summary>
    public class BusinessDetailSearchBLL:BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(SearchBusinessListFilter filter, ResponseEntity responseEntity)
        {
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            if (filter.IsFilterServiceSideKey)
            {
                List<string> companys = Singleton<RedisEnumOperatorBLL>.Instance.GetUserOwnCompanyKeys(responseEntity.UserId);
                filter.BranchKey = string.Join("','", companys.ToArray());
            }

            if(filter.IsFilterBranchKeys)
            {
                List<string> stores = Singleton<RedisEnumOperatorBLL>.Instance.GetUserOwnStoreKeys(responseEntity.UserId);
                filter.BranchKeys = string.Join("','", stores.ToArray());                
            }

            var businessDetail
                = Singleton<BusinessDetailDAL<BusinessDetailViewData>>.Instance.SearchData(filter);

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
                    business.BranchKeyName = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_Store_12, business.BranchKey).Name;
                }
                filter.BusinessID = businessDetail[0].BusinessID;

                var lstBills = Singleton<BillDetailDAL<BillDetailViewData>>.Instance.SearchData(filter);
                var lstBillItems = Singleton<BillItemDetailDAL<BillItemDetailViewData>>.Instance.SearchData(filter);
                var lstReceives = Singleton<ReceiveDetailDAL<ReceiveDetailViewData>>.Instance.SearchData(filter);
                var lstReceiveCut = Singleton<ReceiveCutDetailDAL<ReceivedCutViewData>>.Instance.SearchData(filter);
                var lastCloseDay = Singleton<GetLatestCloseTimeBLL>.Instance.GetLatestCloseTimeViewData(businessDetail[0].ServiceSideID);
                businessDetail[0].LatestTime = lastCloseDay.LatestTime;

                List<PenaltyIntViewData> lstPenaltyInt = null;
                // 罚息
                if (filter.IsSearchPenaltyInt)
                    lstPenaltyInt = Singleton<PenaltyIntDetailDAL<PenaltyIntViewData>>.Instance.SearchData(filter);

                // 代偿卡
                List<AdaptationCardDetailData> lstACard = null;
                if (filter.IsSearchAdaptationCard)
                {
                    lstACard = Singleton<AdaptationCardDAL<AdaptationCardDetailData>>.Instance.SearchData(filter);
                    lstACard.ForEach(p =>
                        {
                            p.AdaBankName = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_BankList_8, p.AdaBankName).Name;
                        });
                }

                StringBuilder sbHead = new StringBuilder();
                int billLength = 0;
                int billItemLength = 0;
                int receiveLength = 0;
                int receiveCutLength = 0;
                int penaltyIntLength = 0;
                int aCardLength = 0;

                if (lstBills != null && lstBills.Count != 0) billLength = lstBills.Count;
                if (lstBillItems != null && lstBillItems.Count != 0) billItemLength = lstBillItems.Count;
                if (lstReceives != null && lstReceives.Count != 0) receiveLength = lstReceives.Count;
                if (lstReceiveCut != null && lstReceiveCut.Count != 0) receiveCutLength = lstReceiveCut.Count;
                if (lstPenaltyInt != null && lstPenaltyInt.Count != 0) penaltyIntLength = lstPenaltyInt.Count;
                if (lstACard != null && lstACard.Count != 0) aCardLength = lstACard.Count;

                sbHead.AppendFormat("{0}{1}{2}{1}{3}{1}{4}{1}{5}{1}{6}{1}{7}"
                    , 1, WebServiceConst.Separater_Semicolon, billLength, billItemLength
                    , receiveLength, receiveCutLength, penaltyIntLength, aCardLength);

                StringBuilder sbContent = new StringBuilder();
                sbContent.AppendLine(sbHead.ToString());
                sbContent.AppendLine(businessDetail[0].ToString());
                if (lstBills != null && lstBills.Count != 0)
                {
                    foreach (var bill in lstBills)
                    {
                        sbContent.AppendLine(bill.ToString());
                    }
                }

                if (lstBillItems != null && lstBillItems.Count != 0)
                {
                    foreach (var billItem in lstBillItems)
                    {
                        sbContent.AppendLine(billItem.ToString());
                    }
                }

                if (lstReceives != null && lstReceives.Count != 0)
                {
                    foreach (var receive in lstReceives)
                    {
                        sbContent.AppendLine(receive.ToString());
                    }
                }

                if (lstReceiveCut != null && lstReceiveCut.Count != 0)
                {
                    foreach (var receiveCut in lstReceiveCut)
                    {
                        sbContent.AppendLine(receiveCut.ToString());
                    }
                }
                if (lstPenaltyInt != null && lstPenaltyInt.Count != 0)
                {
                    foreach (var penaltyInt in lstPenaltyInt)
                    {
                        sbContent.AppendLine(penaltyInt.ToString());
                    }
                }
                if (lstACard != null && lstACard.Count != 0)
                {
                    foreach (var aCard in lstACard)
                    {
                        sbContent.AppendLine(aCard.ToString());
                    }
                }

                var responseResult = new ResponseFileResult();
                responseResult.Result = ConvertUtility.CodingToByte(sbContent.ToString(), 2);

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }
    }
}

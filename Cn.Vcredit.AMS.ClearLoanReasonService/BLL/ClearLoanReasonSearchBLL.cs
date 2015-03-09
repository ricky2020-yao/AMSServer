using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.ClearLoanReasonService.DAL;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.RelativeDaySearchDunService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.ClearLoanReasonService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:清贷原因设置检索逻辑处理类
    /// </summary>
    public class ClearLoanReasonSearchBLL : BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            var filter = baseFilter as ClearLoanReasonSearchFilter;
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            var lstResult = Singleton<ClearLoanReasonSearchDAL<ClearLoanReasonSearchViewData>>
                .Instance.SearchData(baseFilter);

            if (lstResult == null ||  lstResult.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {
                var business = lstResult[0];
                filter.BusinessIds = business.BusinessID.ToString();
                var lstBills = Singleton<ClearLoanBillDetailDAL<Bill>>.Instance.SearchData(filter);
                var lstBillItems = Singleton<ClearLoanBillItemDetailDAL<BillItem>>.Instance.SearchData(filter);

                ConvertToBills(lstBills, lstBillItems, null);

                foreach (var bus in lstResult)
                {
                    bus.LoanKind = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_LoanKind_6, bus.LoanKind).Name;
                    bus.StrBusinessStatus = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_BusinessStatus_10, bus.BusinessStatus.ToString()).Name;
                    bus.StrCloanStatus = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_CLoanStatus_11, bus.CLoanStatus.ToString()).Name;
                    //bus.ClearLoanTime = bus.LoanTime.AddMonths(bus.LoanPeriod);
                    bus.RepayedPeriods
                        = CalculateRepayedPeriods(lstBills.Where(x => x.BusinessID == bus.BusinessID));
                }

                var responseResult = new ResponseListResult<ClearLoanReasonSearchViewData>();
                responseResult.TotalCount = 1;
                responseResult.LstResult = lstResult;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }

        /// <summary>
        /// 转换成账单
        /// </summary>
        /// <param name="lstBillDetails"></param>
        /// <param name="lstBillItemDetails"></param>
        /// <param name="lstReceivedDetails"></param>
        /// <returns></returns>
        public void ConvertToBills(
              List<Bill> lstBillDetails
            , List<BillItem> lstBillItemDetails
            , List<Received> lstReceivedDetails)
        {
            if (lstBillDetails == null || lstBillDetails.Count == 0)
                return;

            foreach (var billDetail in lstBillDetails)
            {
                billDetail.BillItems = ConvertToBillItems(billDetail, lstBillItemDetails, lstReceivedDetails);
            }
        }

        /// <summary>
        /// 转换成账单科目
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="lstBillItemDetails"></param>
        /// <param name="lstReceivedDetails"></param>
        /// <returns></returns>
        public List<BillItem> ConvertToBillItems(
            Bill bill
            , List<BillItem> lstBillItemDetails
            , List<Received> lstReceivedDetails)
        {
            if (lstBillItemDetails == null || lstBillItemDetails.Count == 0)
                return null;

            var lstBillItems = lstBillItemDetails.Where(x => x.BillID == bill.BillID).ToList();

            if (lstBillItems == null || lstBillItems.Count == 0)
                return null;

            foreach (var billItem in lstBillItems)
            {
                billItem.Receiveds = ConvertToReceived(bill, billItem, lstReceivedDetails);
            }

            return lstBillItems;
        }

        /// <summary>
        /// 转换成实收信息
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="billItem"></param>
        /// <param name="lstReceiveDetailViewDatas"></param>
        /// <returns></returns>
        private List<Received> ConvertToReceived(Bill bill
            , BillItem billItem
            , List<Received> lstReceiveDetailViewDatas)
        {
            List<Received> lstReceives = new List<Received>();
            if (lstReceiveDetailViewDatas == null || lstReceiveDetailViewDatas.Count == 0)
                return null;

            List<Received> lstReceive = lstReceiveDetailViewDatas.Where(x => x.BillItemID == billItem.BillItemID).ToList();
            if (lstReceive == null || lstReceive.Count == 0)
                return null;

            foreach (var detail in lstReceive)
            {
                detail.Bill = bill;
                detail.BillItem = billItem;
            }

            return lstReceive;
        }

        /// <summary>
        /// 计算订单的已全额还款期数
        /// </summary>
        /// <param name="lstBills"></param>
        /// <returns></returns>
        private int CalculateRepayedPeriods(IEnumerable<Bill> lstBills)
        {
            if (lstBills == null)
                return 0;

            return lstBills.Where(p => p.BillStatus == 3 && (p.BillType == (byte)EnumBillKind.Normal ||
                 p.BillType == (byte)EnumBillKind.Guarantee) &&
                 p.BillItems.Where(o => o.Subject == (byte)EnumCostSubject.Capital).Count() == 1).Count();
        }
    }
}

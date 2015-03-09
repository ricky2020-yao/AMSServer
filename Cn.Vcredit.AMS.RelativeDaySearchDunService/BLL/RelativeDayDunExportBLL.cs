using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.RelativeDaySearchDunService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.RelativeDaySearchDunService.BLL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-12-15
    /// Desc:催收单导出业务处理类
    /// </summary>
    public class RelativeDayDunExportBLL:BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void ExportData(BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            var filter = baseFilter as RelativeDayExportDunFilter;
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            filter.ContractNo = filter.ContractNo;
            var lstBusiness = Singleton<RelativeDayDunExportDAL<Business>>.Instance.SearchData(filter);
            if(lstBusiness == null || lstBusiness.Count == 0)
            {                
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }

            var business = lstBusiness[0];
            filter.BusinessIds = business.BusinessID.ToString();
            var lstBills = Singleton<DunBillDetailDAL<Bill>>.Instance.SearchData(filter);
            var lstBillItems = Singleton<DunBillItemDetailDAL<BillItem>>.Instance.SearchData(filter);
            var lstReceived = Singleton<DunReceiveDetailDAL<Received>>.Instance.SearchData(filter);

            Utility.ConvertToBills(lstBills, lstBillItems, lstReceived);
            List<Received> list = new List<Received>();
            var bills = business.Bills;
            foreach (var bill in lstBills)
            {
                list.AddRange(lstReceived.FindAll(o => o.BillID == bill.BillID));
            }

            var ret = list.Where(p => p.PayID != 0).OrderBy(p => p.BillID)
                .GroupBy(p => p.BillID).Select(x => new { Key = x.Key, Member = x.ToList() });

            if (ret.Count() == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult, "当前催收单无收款记录!");
                m_Logger.Info("当前催收单无收款记录!");
                return;
            }

            var lstExportDatas = new List<RelativeDaySearchDunExportViewData>();
            RelativeDaySearchDunExportViewData data = null;
            for (int i = 2; i <= ret.Count() + 1; i++)
            {
                var lists = ret.ElementAtOrDefault(i - 2).Member;
                var monthlist = lists.GroupBy(p => p.PayID).Select(o => new { key = o.Key, RetList = o.ToList() });
                foreach (var a in monthlist)
                {
                    data = new RelativeDaySearchDunExportViewData();
                    var first = lists.FirstOrDefault();
                    data.ReceivedType = first.strReceivedType;
                    data.LoanTime = business.LatestTime;
                    data.BillMonth = first.Bill.BillMonth;
                    data.ReceivedTime = first.ReceivedTime;
                    data.CustomerName = business.CustomerName;
                    data.SavingCard = business.SavingCard;
                    data.Capital = Utility.GetDecimal(lists, EnumCostSubject.Capital);
                    data.Interest = Utility.GetDecimal(lists, EnumCostSubject.Interest);
                    data.ServiceFee = Utility.GetDecimal(lists, EnumCostSubject.ServiceFee);
                    data.GuaranteeFee = Utility.GetDecimal(lists, EnumCostSubject.GuaranteeFee);
                    data.PunitiveInterest = Utility.GetDecimal(lists, EnumCostSubject.PunitiveInterest);
                    data.InterestBuckleFail = Utility.GetDecimal(lists, EnumCostSubject.InterestBuckleFail);
                    data.ServiceBuckleFail = Utility.GetDecimal(lists, EnumCostSubject.ServiceBuckleFail);
                    data.ReceivedCapital = Utility.GetReceivedDecimal(first.Bill, EnumCostSubject.Capital);
                    data.ReceivedInterest = Utility.GetReceivedDecimal(first.Bill, EnumCostSubject.Interest);
                    data.ReceivedServiceFee = Utility.GetReceivedDecimal(first.Bill, EnumCostSubject.ServiceFee);
                    data.ReceivedGuaranteeFee = Utility.GetReceivedDecimal(first.Bill, EnumCostSubject.GuaranteeFee);
                    data.ReceivedPunitiveInterest = Utility.GetReceivedDecimal(first.Bill, EnumCostSubject.PunitiveInterest);
                    data.ReceivedInterestBuckleFail = Utility.GetReceivedDecimal(first.Bill, EnumCostSubject.InterestBuckleFail);
                    data.ReceivedServiceBuckleFail = Utility.GetReceivedDecimal(first.Bill, EnumCostSubject.ServiceBuckleFail);
                    lstExportDatas.Add(data);
                }
            }

            if (lstExportDatas == null || lstExportDatas.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {
                // 设置输出文件
                SetExportFile(filter, lstExportDatas, responseEntity);
            }
        }
    }
}

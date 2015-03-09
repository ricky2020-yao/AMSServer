using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.RelativeDaySearchDunService
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-12-15
    /// Desc:共通处理
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// 转换成账单
        /// </summary>
        /// <param name="lstBillDetails"></param>
        /// <param name="lstBillItemDetails"></param>
        /// <param name="lstReceivedDetails"></param>
        /// <returns></returns>
        public static void ConvertToBills(
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
        public static List<BillItem> ConvertToBillItems(
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
        private static List<Received> ConvertToReceived(Bill bill
            , BillItem billItem
            , List<Received> lstReceiveDetailViewDatas)
        {
            List<Received> lstReceives = new List<Received>();
            if (lstReceiveDetailViewDatas == null || lstReceiveDetailViewDatas.Count == 0)
                return null ;

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
        /// 获取已收金额
        /// </summary>
        /// <param name="list"></param>
        /// <param name="subj"></param>
        /// <returns></returns>
        public static string GetDecimal(List<Received> list, EnumCostSubject subj)
        {
            var ret = list.FirstOrDefault(p => p.BillItem.Subject == (byte)subj);
            return ret == null ? "0" : ret.Amount.ToAmtString();
        }
        /// <summary>
        /// 获取应收金额
        /// </summary>
        /// <param name="list"></param>
        /// <param name="subj"></param>
        /// <returns></returns>
        public static decimal GetReceivedDecimal(Bill bill, EnumCostSubject subj)
        {
            var ret = bill.BillItems.FirstOrDefault(p => p.Subject == (byte)subj);
            return ret == null ? 0 : ret.Amount;
        }
    }
}

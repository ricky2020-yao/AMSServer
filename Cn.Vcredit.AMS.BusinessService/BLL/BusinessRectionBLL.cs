using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BusinessService.Common;
using Cn.Vcredit.AMS.BusinessService.DAL;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-11-25
    /// Description:订单详情业务-自动减免逻辑处理类
    /// </summary>
    public class BusinessRectionBLL : BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void RectionData(SearchBusinessListFilter baseFilter, ResponseEntity responseEntity)
        {
            if (baseFilter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            // 获取订单信息
            Business business = BusinessHelper.GetBusinessInfo(baseFilter);            
            if (business == null || business.Bills.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult, "无账单处理!");
                m_Logger.Info("无账单处理!");
                return;
            }

            List<long> lstBillItemIDs = new List<long>();
            // 获取实收信息
            var lstReceiveds = GetReceivedInfo(business, baseFilter.RecTime, responseEntity.UserId, ref lstBillItemIDs);
            if (lstReceiveds == null || lstReceiveds.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult, "无实收信息处理!");
                m_Logger.Info("无实收信息处理!");
                return;
            }

            string guid = Guid.NewGuid().ToString();
            string billItemIds = string.Join(",", lstBillItemIDs.ToArray());
            baseFilter.BillItemIds = billItemIds;
            baseFilter.Guid = guid;
            // 保存实收信息到数据库
            BusinessHelper.SaveReceivedTempDataBase(guid, lstReceiveds);

            // 订单更新
            Singleton<BaseUpdateBLL<BusinessRectionDAL>>.Instance.UpdateData(baseFilter, responseEntity);
            ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
        }

        /// <summary>
        /// 获取实收信息
        /// </summary>
        /// <param name="business"></param>
        /// <param name="recTime"></param>
        /// <param name="userId"></param>
        /// <param name="lstBillItemIds"></param>
        /// <returns></returns>
        private List<Received> GetReceivedInfo(Business business, DateTime recTime, int userId, ref List<long> lstBillItemIds)
        {
            List<Received> lstReceiveds = new List<Received>();
            //所有欠费账单
            var bills = business.Bills.Where(o => o.BillStatus != (byte)EnumBillStatus.FullPay).ToList();

            var billitems = new List<BillItem>();
            bills.Where(p => p.BillType != (byte)EnumBillKind.Annul
                && !p.IsShelve).ToList().ForEach(o => o.BillItems.ForEach(p => billitems.Add(p)));
            billitems = billitems.Where(o => o.DueAmt - o.ReceivedAmt > 0).ToList();
            foreach (var item in billitems)
            {
                decimal count = item.DueAmt - item.ReceivedAmt;
                item.ReceivedAmt = item.ReceivedAmt + count;
                //item.DueAmt = item.DueAmt + count;
                Received r = new Received
                {
                    Amount = count,//减免由应收调整为实收,所以金额为正数 By Baker 2014年8月25日
                    BillID = item.BillID,
                    BillItemID = item.BillItemID,
                    CreateTime = DateTime.Now,
                    Explain = "自动减免",
                    PayID = 0,
                    ReceivedTime = recTime,
                    ReceivedType = (byte)EnumAdjustKind.Exemption,//减免由应收调整为实收,ReceivedType变更为16
                    OperatorID = userId
                };
                lstBillItemIds.Add(item.BillItemID);
                if (item.Receiveds == null)
                    item.Receiveds = new List<Received>();
                item.Receiveds.Add(r);
                lstReceiveds.Add(r);
            }

            return lstReceiveds;
        }

    }
}

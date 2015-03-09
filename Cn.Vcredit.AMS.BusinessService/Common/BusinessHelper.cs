using Cn.Vcredit.AMS.BusinessService.DAL;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.Common
{
    /// <summary>
    /// Json账单
    /// </summary>
    public class JsonBill
    {
        /// <summary>
        /// 账单ID
        /// </summary>
        public long BillId { get; set; }
        /// <summary>
        /// 实收信息
        /// </summary>
        public List<Received> Subjects { get; set; }
    }

    /// <summary>
    /// 更新项目
    /// </summary>
    public class UpdateItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal ReceivedAmt { get; set; }

        /// <summary>
        /// 全部还款时间
        /// </summary>
        public DateTime? FullPaidTime { get; set; }

        /// <summary>
        /// 类型
        /// 1:Bill
        /// 2:BillItem DueAmt
        /// 3:BillItem ReceivedAmt
        /// 4:Receive
        /// </summary>
        public byte Type { get; set; }
    }

    /// <summary>
    /// 订单服务共同处理
    /// </summary>
    public static class BusinessHelper
    {
        /// <summary>
        /// 保存实收信息到数据库
        /// </summary>
        /// <param name="receive"></param>
        public static void SaveReceivedDataBase(List<Received> lstReceiveds)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("BillID", typeof(long)));
            dt.Columns.Add(new DataColumn("BillItemID", typeof(long)));
            dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("ReceivedType", typeof(byte)));
            dt.Columns.Add(new DataColumn("PayID", typeof(int)));
            dt.Columns.Add(new DataColumn("ReceivedTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("CreateTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("OperatorID", typeof(int)));
            dt.Columns.Add(new DataColumn("Explain", typeof(string)));
            dt.Columns.Add(new DataColumn("DeductionID", typeof(int)));
            dt.Columns.Add(new DataColumn("ToAccountID", typeof(int)));
            dt.Columns.Add(new DataColumn("ToAcountTime", typeof(DateTime)));

            DataRow dr = null;
            foreach (Received receive in lstReceiveds)
            {
                dr = dt.NewRow();
                dr["BillID"] = receive.BillID;
                dr["BillItemID"] = receive.BillItemID;
                dr["Amount"] = receive.Amount;
                dr["ReceivedType"] = receive.ReceivedType;
                dr["PayID"] = receive.PayID;

                if (receive.ReceivedTime.HasValue)
                    dr["ReceivedTime"] = receive.ReceivedTime;
                else
                    dr["ReceivedTime"] = DBNull.Value;

                dr["CreateTime"] = receive.CreateTime;
                dr["OperatorID"] = receive.OperatorID;
                dr["Explain"] = receive.Explain;
                dr["DeductionID"] = receive.DeductionID;

                if (receive.ToAccountID.HasValue)
                    dr["ToAccountID"] = receive.ToAccountID;
                else
                    dr["ToAccountID"] = DBNull.Value;

                if (receive.ToAcountTime.HasValue)
                    dr["ToAcountTime"] = receive.ToAcountTime;
                else
                    dr["ToAcountTime"] = DBNull.Value;

                dt.Rows.Add(dr);
            }

            dt.TableName = "Received";
            SqlBulkHelper.ExecuteBulkImport("Received", dt, "PostLoanDB", 1);
        }

        /// <summary>
        /// 保存实收信息到数据库
        /// <param name="guid"></param>
        /// <param name="lstReceiveds"></param>
        public static void SaveReceivedTempDataBase(string guid, List<Received> lstReceiveds)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("BillID", typeof(long)));
            dt.Columns.Add(new DataColumn("BillItemID", typeof(long)));
            dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("ReceivedType", typeof(byte)));
            dt.Columns.Add(new DataColumn("PayID", typeof(int)));
            dt.Columns.Add(new DataColumn("ReceivedTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("CreateTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("OperatorID", typeof(int)));
            dt.Columns.Add(new DataColumn("Explain", typeof(string)));
            dt.Columns.Add(new DataColumn("DeductionID", typeof(int)));
            dt.Columns.Add(new DataColumn("ToAccountID", typeof(int)));
            dt.Columns.Add(new DataColumn("ToAcountTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Guid", typeof(string)));

            DataRow dr = null;
            foreach (Received receive in lstReceiveds)
            {
                dr = dt.NewRow();
                dr["BillID"] = receive.BillID;
                dr["BillItemID"] = receive.BillItemID;
                dr["Amount"] = receive.Amount;
                dr["ReceivedType"] = receive.ReceivedType;
                dr["PayID"] = receive.PayID;

                if (receive.ReceivedTime.HasValue)
                    dr["ReceivedTime"] = receive.ReceivedTime;
                else
                    dr["ReceivedTime"] = DBNull.Value;

                dr["CreateTime"] = receive.CreateTime;
                dr["OperatorID"] = receive.OperatorID;
                dr["Explain"] = receive.Explain;
                dr["DeductionID"] = receive.DeductionID;

                if (receive.ToAccountID.HasValue)
                    dr["ToAccountID"] = receive.ToAccountID;
                else
                    dr["ToAccountID"] = DBNull.Value;

                if (receive.ToAcountTime.HasValue)
                    dr["ToAcountTime"] = receive.ToAcountTime;
                else
                    dr["ToAcountTime"] = DBNull.Value;

                dr["Guid"] = guid;
                dt.Rows.Add(dr);
            }

            dt.TableName = "ReceivedTemp";
            SqlBulkHelper.ExecuteBulkImport("ReceivedTemp", dt, "PostLoanDB", 1);
        }

        /// <summary>
        /// 保存更新条目信息到数据库
        /// <param name="guid"></param>
        /// <param name="lstUpdateItems"></param>
        public static void SaveUpdateItemTempDataBase(string guid, List<UpdateItem> lstUpdateItems)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Id", typeof(long)));
            dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("ReceivedAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("FullPaidTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Type", typeof(byte)));
            dt.Columns.Add(new DataColumn("Guid", typeof(string)));

            DataRow dr = null;
            foreach (UpdateItem receive in lstUpdateItems)
            {
                dr = dt.NewRow();
                dr["Id"] = receive.Id;
                dr["Amount"] = receive.Amount;
                dr["ReceivedAmt"] = receive.ReceivedAmt;

                if (receive.FullPaidTime.HasValue)
                    dr["FullPaidTime"] = receive.FullPaidTime;
                else
                    dr["FullPaidTime"] = DBNull.Value;

                dr["Type"] = receive.Type;
                dr["Guid"] = guid;
                dt.Rows.Add(dr);
            }

            dt.TableName = "UpdateItemTemp";
            SqlBulkHelper.ExecuteBulkImport("UpdateItemTemp", dt, "PostLoanDB", 1);
        }

        /// <summary>
        /// 保存账单科目信息到数据库
        /// <param name="guid"></param>
        /// <param name="lstBillItems"></param>
        public static void SaveBillItemTempDataBase(string guid, List<BillItem> lstBillItems)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("BillID", typeof(long)));
            dt.Columns.Add(new DataColumn("Subject", typeof(byte)));
            dt.Columns.Add(new DataColumn("DueDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DueAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("ReceivedAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CreateTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("FullPaidTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Overdue", typeof(int)));
            dt.Columns.Add(new DataColumn("SubjectType", typeof(byte)));
            dt.Columns.Add(new DataColumn("OperatorID", typeof(int)));
            dt.Columns.Add(new DataColumn("IsCurrent", typeof(bool)));
            dt.Columns.Add(new DataColumn("IsShelve", typeof(bool)));
            dt.Columns.Add(new DataColumn("BusinessID", typeof(byte)));
            dt.Columns.Add(new DataColumn("PenaltyIntAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Guid", typeof(string)));

            DataRow dr = null;
            foreach (BillItem receive in lstBillItems)
            {
                dr = dt.NewRow();
                dr["BillID"] = receive.BillID;
                dr["Subject"] = receive.Subject;
                //dr["DueDate"] = receive.DueDate;
                dr["Amount"] = receive.Amount;
                dr["DueAmt"] = receive.DueAmt;
                dr["ReceivedAmt"] = receive.ReceivedAmt;
                dr["CreateTime"] = receive.CreateTime;
                dr["FullPaidTime"] = receive.FullPaidTime;
                //dr["Overdue"] = receive.Overdue;
                dr["SubjectType"] = receive.SubjectType;
                dr["OperatorID"] = receive.OperatorID;
                dr["IsCurrent"] = receive.IsCurrent;
                dr["IsShelve"] = receive.IsShelve;
                dr["BusinessID"] = receive.BusinessID;
                dr["PenaltyIntAmt"] = receive.PenaltyIntAmt;
                dr["Guid"] = guid;
                dt.Rows.Add(dr);
            }

            dt.TableName = "BillItemTemp";
            SqlBulkHelper.ExecuteBulkImport("BillItemTemp", dt, "PostLoanDB", 1);
        }

        /// <summary>
        /// 获取业务信息
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        public static Business GetBusinessInfo(SearchBusinessListFilter filter)
        {
            List<string> companys = Singleton<RedisEnumOperatorBLL>.Instance.GetUserOwnCompanyKeys(filter.UserId);
            filter.BranchKey = string.Join("','", companys.ToArray());

            var lstBusinessDetail = Singleton<BusinessDetailDAL<BusinessDetailViewData>>.Instance.SearchData(filter);
            var detail = new BusinessDetailViewData();
            if (lstBusinessDetail == null || lstBusinessDetail.Count == 0)
                return null;

            detail = lstBusinessDetail[0];
            filter.BusinessID = detail.BusinessID;

            var lstBillDetails = Singleton<BillDetailDAL<BillDetailViewData>>.Instance.SearchData(filter);
            var lstBillItemDetails = Singleton<BillItemDetailDAL<BillItemDetailViewData>>.Instance.SearchData(filter);
            return ConvertToBusiness(detail, lstBillDetails, lstBillItemDetails); 
        }

        /// <summary>
        /// 转换成Business实体类
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="lstBillDetails"></param>
        /// <param name="lstBillItemDetails"></param>
        /// <returns></returns>
        private static Business ConvertToBusiness(BusinessDetailViewData detail
            , List<BillDetailViewData> lstBillDetails, List<BillItemDetailViewData> lstBillItemDetails)
        {
            Business business = null;
            if (detail != null)
            {
                business = new Business();
                business.BusinessID = detail.BusinessID;
                business.BusinessStatus = detail.BusinessStatus;
                business.ClearLoanTime = detail.ClearLoanTime;
                business.CLoanStatus = detail.CLoanStatus;
                business.ContractNo = detail.ContractNo;
                business.CreateTime = detail.CreateTime;
                business.CurrentOverAmount = detail.CurrentOverAmount;
                business.CustomerID = detail.CustomerID;
                business.CustomerName = detail.CustomerName;
                business.FrozenNo = detail.FrozenNo;
                business.IsRepayment = detail.IsRepayment;
                business.LawsuitStatus = detail.LawsuitStatus;
                business.LendingSideKey = detail.LendingSideKey;
                business.LoanCapital = detail.LoanCapital;
                business.LoanKind = detail.LoanKind;
                business.LoanKindName = detail.LoanKindName;
                business.RegionName = detail.RegionName;
                business.LoanPeriod = detail.LoanPeriod;
                business.LoanTime = detail.LoanTime;
                business.OtherAmount = detail.OtherAmount;
                business.OverAmount = detail.OverAmount;
                business.ProductKind = detail.ProductKind;
                business.ProductType = detail.ProductType;
                business.Region = detail.Region;
                business.SavingCard = detail.SavingCard;
                business.ToGuaranteeTime = detail.ToGuaranteeTime;
                business.ToLitigationTime = detail.ToLitigationTime;
                business.IdentityNo = detail.IdentityNo;

                business.Bills = ConvertToBills(business, lstBillDetails, lstBillItemDetails);
            }
            return business;
        }

        /// <summary>
        /// 转换成账单
        /// </summary>
        /// <param name="business"></param>
        /// <param name="lstBillDetails"></param>
        /// <param name="lstBillItemDetails"></param>
        /// <returns></returns>
        private static List<Bill> ConvertToBills(Business business
            , List<BillDetailViewData> lstBillDetails
            , List<BillItemDetailViewData> lstBillItemDetails)
        {
            List<Bill> lstBills = new List<Bill>();
            if (lstBillDetails == null || lstBillDetails.Count == 0)
                return lstBills;

            Bill bill = null;
            foreach (var billDetail in lstBillDetails)
            {
                bill = new Bill();
                bill.BillID = billDetail.BillID;
                bill.BusinessID = billDetail.BusinessID;
                bill.BillType = billDetail.BillType;
                bill.BillStatus = billDetail.BillStatus;
                bill.BillMonth = billDetail.BillMonth;
                bill.BeginTime = billDetail.BeginTime;
                bill.EndTime = billDetail.EndTime;
                bill.LimitTime = billDetail.LimitTime;
                bill.CreateTime = billDetail.CreateTime;
                bill.FullPaidTime = billDetail.FullPaidTime;
                bill.IsCurrent = billDetail.IsCurrent;
                bill.IsShelve = billDetail.IsShelve;
                bill.Business = business;
                bill.BillItems = ConvertToBillItems(business, bill, lstBillItemDetails);
                lstBills.Add(bill);
            }

            return lstBills;
        }

        /// <summary>
        /// 转换成账单科目
        /// </summary>
        /// <param name="business"></param>
        /// <param name="bill"></param>
        /// <param name="lstBillItemDetails"></param>
        /// <returns></returns>
        private static List<BillItem> ConvertToBillItems(Business business
            , Bill bill
            , List<BillItemDetailViewData> lstBillItemDetails)
        {
            List<BillItem> billItems = new List<BillItem>();
            if (lstBillItemDetails == null || lstBillItemDetails.Count == 0)
                return billItems;

            List<BillItemDetailViewData> lstBillItems = lstBillItemDetails.Where(x => x.BillID == bill.BillID).ToList();

            if (lstBillItems == null || lstBillItems.Count == 0)
                return billItems;

            BillItem billitem = null;
            foreach (var billItemDetail in lstBillItems)
            {
                billitem = new BillItem();
                billitem.BillItemID = billItemDetail.BillItemID;
                billitem.BillID = billItemDetail.BillID;
                billitem.Subject = billItemDetail.Subject;
                billitem.SubjectType = billItemDetail.SubjectType;
                billitem.Amount = billItemDetail.Amount;
                billitem.DueAmt = billItemDetail.DueAmt;
                billitem.ReceivedAmt = billItemDetail.ReceivedAmt;
                billitem.PenaltyIntAmt = billItemDetail.PenaltyIntAmt;
                billitem.CreateTime = billItemDetail.CreateTime;
                billitem.FullPaidTime = billItemDetail.FullPaidTime;
                billitem.IsCurrent = billItemDetail.IsCurrent;
                billitem.IsShelve = billItemDetail.IsShelve;
                billitem.BusinessID = billItemDetail.BusinessID;
                billitem.Bill = bill;
                billItems.Add(billitem);
            }

            return billItems;
        }

        /// <summary>
        /// 将json字符串转换成调整科目
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool JsonToReceivedItem(PayAccountFilter filter, string guid)
        {
            try
            {
                var bills = JsonConvert.DeserializeObject<List<JsonBill>>(filter.JsonString);
                var tempReceived = new List<Received>();
                var tempBillItems = new List<string>();
                var receivedList = new List<Received>();
                var billItems = new List<BillItem>();

                AddReceivedItem(bills, tempReceived, tempBillItems, receivedList, filter);
                AddBillItemAndReceivedItem(tempReceived, tempBillItems, receivedList, billItems, filter);

                BusinessHelper.SaveReceivedTempDataBase(guid, receivedList);
                BusinessHelper.SaveBillItemTempDataBase(guid, billItems);
                return CreateReceivedItem(receivedList, guid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Target:订单明细页[ReliefAndCorrect]、创建帐单页[CreateBillsTask]
        /// Description:插入新增调整科目
        /// </summary>
        /// <param name="list"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool CreateReceivedItem(List<Received> list, string guid)
        {
            List<UpdateItem> lstUpdateItem = new List<UpdateItem>();
            var ints = list.Select(p => p.BillItemID).ToList();
            SearchBusinessListFilter filter = new SearchBusinessListFilter();
            filter.BillItemIds = string.Join(",", ints.ToArray());
            var billItems 
                = Singleton<BillItemDetailByIdsDAL<BillItemDetailViewData>>.Instance.SearchData(filter);
            var receiveds
                 = Singleton<ReceiveDetailByIdsDAL<ReceiveDetailViewData>>.Instance.SearchData(filter);

            UpdateItem updateItem = null;
            List<ReceiveDetailViewData> lstReceives = null;
            foreach (var item in billItems)
            {
                updateItem = new UpdateItem();
                updateItem.Id = item.BillItemID;

                var thresholds = (byte)EnumAdjustKind.Thresholds;

                lstReceives = receiveds.Where(x => x.BillItemID == item.BillItemID).ToList();
                if (lstReceives == null)
                    continue;

                var receivedDB = lstReceives.Where(p => p.ReceivedType > thresholds).Sum(p => p.Amount);
                var receivedNow = list.Where(c => c.ReceivedType > thresholds &&
                    c.BillItemID == item.BillItemID).Sum(d => d.Amount);

                // 重新计算该应收科目实收总额
                item.ReceivedAmt = receivedDB + receivedNow;

                var dueDB = lstReceives.Where(p => p.ReceivedType <= thresholds).Sum(p => p.Amount);
                var dueNow = list.Where(p => p.ReceivedType <= thresholds &&
                    p.BillItemID == item.BillItemID).Sum(p => p.Amount);
                // 重新计算该应收科目应收总额
                item.DueAmt = item.Amount + dueDB + dueNow;

                // 实收大于应收则返回错误
                if (item.ReceivedAmt > item.DueAmt ||
                    item.ReceivedAmt < 0 || item.DueAmt < 0)
                    return false;

                // 如果实收大于等于应收，则更新全额支付时间
                if (item.ReceivedAmt >= item.DueAmt)
                    item.FullPaidTime = DateTime.Now;

                updateItem.Amount = item.DueAmt;
                updateItem.ReceivedAmt = item.ReceivedAmt;
                updateItem.FullPaidTime = item.FullPaidTime;
                updateItem.Type = 2;

                lstUpdateItem.Add(updateItem);
            }

            BusinessHelper.SaveUpdateItemTempDataBase(guid, lstUpdateItem);
            return true;
        }

        /// <summary>
        /// 针对已存在的帐单科目添加调整款项
        /// </summary>
        /// <param name="bills"></param>
        /// <param name="tempReceived"></param>
        /// <param name="tempBillItems"></param>
        /// <param name="receivedList"></param>
        /// <param name="filter"></param>
        private static void AddReceivedItem(List<JsonBill> bills
            , List<Received> tempReceived
            , List<string> tempBillItems
            , List<Received> receivedList
            , PayAccountFilter filter)
        {
            bills.ForEach(p =>
            {
                p.Subjects.ForEach(
                    o =>
                    {
                        o.BillID = p.BillId;
                        o.CreateTime = DateTime.Now;
                        o.ReceivedTime = filter.RecTime;
                        o.OperatorID = filter.UserId;
                        o.Explain = filter.Explain;
                        if (o.TempBillItemID.Contains('_'))
                        {
                            tempReceived.Add(o);
                            if (!tempBillItems.Contains(o.TempBillItemID))
                                tempBillItems.Add(o.TempBillItemID);
                        }
                        else
                        {
                            o.BillItemID = long.Parse(o.TempBillItemID);
                            receivedList.Add(o);
                        }
                    });
            });
        }

        /// <summary>
        /// 针对刚创建的帐单科目添加调整款项
        /// </summary>
        /// <param name="tempReceived"></param>
        /// <param name="tempBillItems"></param>
        /// <param name="receivedList"></param>
        /// <param name="filter"></param>
        private static void AddBillItemAndReceivedItem(List<Received> tempReceived
            , List<string> tempBillItems
            , List<Received> receivedList
            , List<BillItem> billItems
            , PayAccountFilter filter)
        {
            if (tempBillItems.Count > 0)
            {
                foreach (var strbillItem in tempBillItems)
                {
                    var arr = strbillItem.Split('_');
                    var billItem = new BillItem
                    {
                        BillID = arr[0].ToLong(),
                        CreateTime = DateTime.Now,
                        Subject = arr[1].ToByte(),
                        SubjectType = (byte)EnumSubjectKind.Supplement,
                        Amount = arr[2].ToDecimal(),
                        DueAmt = arr[2].ToDecimal(),
                        OperatorID = filter.UserId,
                    };
                    billItems.Add(billItem);
                }
                receivedList.AddRange(tempReceived);
            }
        }
    }
}

using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.GuarLitigationService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.GuarLitigationService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年10月21日
    /// Description:担保和诉讼设置状态修改保存逻辑操作类
    /// </summary>
    public class GuarLitigationSaveBLL : BaseBLL
    {
        /// <summary>
        /// 保存修改信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public virtual void Update(BusinessGuaranteeSaveFilter filter
            , ResponseEntity responseEntity)
        {
            string guid = "";
            if (filter.IsAddNewBill)
            {
                guid = Guid.NewGuid().ToString();
                Bill bill = CreateBill(filter, EnumBillKind.Litigation);
                bill.BillItems = new List<BillItem>();
                // 诉讼费
                bill.BillItems.Add(CreateBillItem(EnumCostSubject.Litigation, filter.LegalCost.ToDecimal(), filter.UserId));
                // 诉讼违约金
                bill.BillItems.Add(CreateBillItem(EnumCostSubject.LitigationLateFee, filter.LegalPenalty.ToDecimal(), filter.UserId));
                //未出账款项列表
                bill.BillItems.AddRange(NoCreateBillItem(filter));

                // 保存账单信息到数据库
                SaveBillToDataBase(guid, bill);
                // 保存账单明细到数据库
                SaveBillItemToDataBase(guid, bill.BillItems);
            }

            int count = Singleton<GuarLitigationSaveDAL>.Instance.UpdateBusiness(guid, filter);

            if (count <= 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, "更新失败。");
                m_Logger.Info("更新失败。");
            }
            else
            {
                var responseResult = new ResponseListResult<BusinessViewData>();
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }

        /// <summary>
        /// 创建担保诉讼账单
        /// </summary>
        /// <param name="business"></param>
        /// <param name="subj"></param>
        /// <returns></returns>
        private Bill CreateBill(BusinessGuaranteeSaveFilter filter, EnumBillKind subj)
        {
            return new Bill
            {
                CreateTime = DateTime.Now,
                BillMonth = DateTime.Now.ToBillMonthString(),
                BeginTime = DateTime.Now,
                BillType = (byte)subj,
                BillStatus = (byte)EnumBillStatus.NoPay,
                EndTime = DateTime.Now.AddMonths(1).Date.AddSeconds(-1),
                LimitTime = DateTime.Now.SetDay(21),
            };
        }

        /// <summary>
        /// 创建担保诉讼账单
        /// </summary>
        /// <param name="subj"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public BillItem CreateBillItem(EnumCostSubject subj, decimal amount, int userId)
        {
            return new BillItem
            {
                Amount = amount,
                CreateTime = DateTime.Now,
                DueAmt = amount,
                Subject = (byte)subj
            };
        }

        /// <summary>
        /// 未出账金款项列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private List<BillItem> NoCreateBillItem(BusinessGuaranteeSaveFilter filter)
        {
            List<BillItem> billitems = new List<BillItem>();
            int num = filter.LoanPeriod - Singleton<GuarLitigationSaveDAL>.Instance.GetNormalBillsCount(filter.BusinessID);

            //本金
            decimal bj = filter.LoanCapital - Singleton<GuarLitigationSaveDAL>.Instance.GetBillItemsAmount(filter.BusinessID);
            if (bj > 0)
                billitems.Add(CreateBillItem(EnumCostSubject.Capital, bj, filter.UserId));
            //利息
            decimal lx = SubjectFormula.CalculatInterest(filter.LoanCapital, filter.InterestRate) * num;
            if (lx > 0)
                billitems.Add(CreateBillItem(EnumCostSubject.Interest, lx, filter.UserId));
            //服务费\担保费
            decimal dbfwf = SubjectFormula.CalculatServiceFee(filter.LoanCapital, filter.ServiceRate, true) * num;
            if (dbfwf > 0)
            {
                if (string.IsNullOrEmpty(filter.GuaranteeSideKey))
                {
                    billitems.Add(CreateBillItem(EnumCostSubject.ServiceFee, dbfwf * 2, filter.UserId));
                }
                else
                {
                    billitems.Add(CreateBillItem(EnumCostSubject.ServiceFee, dbfwf, filter.UserId));
                    billitems.Add(CreateBillItem(EnumCostSubject.GuaranteeFee, dbfwf, filter.UserId));
                }
            }

            //服务费扣失
            decimal fwfks = SubjectFormula.CalculatBuckleFail(filter.ContractNo) * num; ;
            if (fwfks > 0)
                billitems.Add(CreateBillItem(EnumCostSubject.ServiceBuckleFail, fwfks, filter.UserId));//服务费扣失

            return billitems;
        }

        /// <summary>
        /// 保存账单信息到数据库
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="bill"></param>
        private void SaveBillToDataBase(string guid, Bill bill)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("BillType", typeof(byte)));
            dt.Columns.Add(new DataColumn("BillStatus", typeof(byte)));
            dt.Columns.Add(new DataColumn("BillMonth", typeof(string)));
            dt.Columns.Add(new DataColumn("BeginTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("EndTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("LimitTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("CreateTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Guid", typeof(string)));

            DataRow dr = dt.NewRow();
            dr["BillType"] = bill.BillType;
            dr["BillStatus"] = bill.BillStatus;
            dr["BillMonth"] = bill.BillMonth;
            dr["BeginTime"] = bill.BeginTime;
            dr["EndTime"] = bill.EndTime;
            dr["LimitTime"] = bill.LimitTime;
            dr["CreateTime"] = bill.CreateTime;
            dr["Guid"] = guid;
            dt.Rows.Add(dr);

            dt.TableName = "BillTemp";

            try
            {
                SqlBulkHelper.ExecuteBulkImport("BillTemp", dt, "PostLoanDB", 1);
            }
            catch (Exception ex)
            {
                m_Logger.Error("保存账单信息到数据库异常：" + ex.Message);
                m_Logger.Error("保存账单信息到数据库异常：" + ex.StackTrace);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 保存账单明细到数据库
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="lstBillItems"></param>
        private void SaveBillItemToDataBase(string guid, List<BillItem> lstBillItems)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Subject", typeof(byte)));
            dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DueAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CreateTime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Guid", typeof(string)));

            DataRow dr = null;
            foreach (var bill in lstBillItems)
            {
                dr = dt.NewRow();
                dr["Subject"] = bill.Subject;
                dr["Amount"] = bill.Amount;
                dr["DueAmt"] = bill.DueAmt;
                dr["CreateTime"] = bill.CreateTime;
                dr["Guid"] = guid;
                dt.Rows.Add(dr);
            }

            dt.TableName = "BillItemTemp";

            try
            {
                SqlBulkHelper.ExecuteBulkImport("BillItemTemp", dt, "PostLoanDB", lstBillItems.Count);
            }
            catch (Exception ex)
            {
                m_Logger.Error("保存账单明细到数据库异常：" + ex.Message);
                m_Logger.Error("保存账单明细到数据库异常：" + ex.StackTrace);
            }
            finally
            {
            }
        }
    }
}

using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.Common.DataTableExtensions;
using Cn.Vcredit.AMS.RelativeDaySearchDunService.DAL;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;

namespace Cn.Vcredit.AMS.RelativeDaySearchDunService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:订单筛选导出数据处理类
    /// </summary>
    public class RelativeDayDunSearchBLL:BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            var filter = baseFilter as RelativeDaySearchDunFilter;
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            DataSet dsResult = Singleton<RelativeDayDunSearchDAL<RelativeDaySearchDunViewData>>
                .Instance.SearchDataToSet(baseFilter);

            if (dsResult == null || dsResult.Tables == null || dsResult.Tables.Count < 2)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {
                var responseResult = new ResponseListResult<RelativeDaySearchDunViewData>();
                responseResult.TotalCount = dsResult.Tables[1].Rows[0][0].ToString().ToInt();
                responseResult.LstResult = CreateDunExtendList(responseEntity.UserId,
                    dsResult.Tables[0].ConvertToList<RelativeDaySearchDunViewData>(), filter);

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }

        /// <summary>
        /// 创建催收单视图
        /// </summary>
        /// <param name="userId">催收单信息</param>
        /// <param name="lstDetails">催收单信息</param>
        /// <param name="filter">过滤条件</param>
        /// <returns>返回催收单视图</returns>
        private List<RelativeDaySearchDunViewData> CreateDunExtendList(int userId
            , List<RelativeDaySearchDunViewData> lstDetails, RelativeDaySearchDunFilter filter)
        {
            List<RelativeDaySearchDunViewData> result = new List<RelativeDaySearchDunViewData>();

            try
            {
                //string punInterest = "";
                List<string> keys = filter.MyCompanyIds.Split(
                    WebServiceConst.Separater_Comma.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                if (keys == null || keys.Count == 0)
                    return result;

                // ******************************************************************
                // CR181 定义临时变量tempOverDueDays 表示逾期天数
                // 2014 年8月5日 wangzhangming
                // *****************************************************************
                //int tempOverDueDays = 0;
                List<int> lstBusinessIds = new List<int>();
                if (keys.Count > 0)
                {
                    //lstDetails.ForEach(x => { lstBusinessIds.Add(x.BusinessID); });
                    //filter.BusinessIds = string.Join(WebServiceConst.Separater_Comma_Quote, lstBusinessIds.ToArray());

                    //var lstBills = Singleton<DunBillDetailDAL<Bill>>.Instance.SearchData(filter);
                    //var lstBillItems = Singleton<DunBillItemDetailDAL<BillItem>>.Instance.SearchData(filter);

                    // 转换成Bills
                    //Utility.ConvertToBills(lstBills, lstBillItems, null);

                    foreach (var item in lstDetails)
                    { 
                        #region PCR012删除月罚息 wichell 2014年10月13日 13:44:10
                        //item.
                        //var bills = lstBills.FindAll(x => x.BusinessID == item.BusinessID).ToList();
                        //bills = BillDAL.GetArrearsBills(bills, true).Where(o => o.BillItems.Count > 1).ToList();
                        //decimal Capital = 0;
                        //decimal Interest = 0;
                        //decimal Manage = 0;

                        //foreach (var itm in bills)
                        //{
                        //    Capital += itm.BillItems.FirstOrDefault(o => o.Subject == (byte)EnumCostSubject.Capital) == null ? 0 :
                        //    itm.BillItems.FirstOrDefault(o => o.Subject == (byte)EnumCostSubject.Capital).DueAmt;
                        //    Interest += itm.BillItems.FirstOrDefault(o => o.Subject == (byte)EnumCostSubject.Interest) == null ? 0 :
                        //    itm.BillItems.FirstOrDefault(o => o.Subject == (byte)EnumCostSubject.Interest).DueAmt;
                        //    Manage += itm.BillItems.FirstOrDefault(o => o.Subject == (byte)EnumCostSubject.ServiceFee) == null ? 0 :
                        //    itm.BillItems.FirstOrDefault(o => o.Subject == (byte)EnumCostSubject.ServiceFee).DueAmt;
                        //}

                        //if (keys.Contains(SysConst.COMPANY_WX_CDWS_LENDING))//曹贝 2013年11月5日 替换成常量
                        //{
                        //    decimal count = Capital + Interest + Manage;
                        //    punInterest = (count * (decimal)0.001).ToAmtString() + " / " + (count * (decimal)0.03).ToAmtString();
                        //}
                        //else
                        //{
                        //    decimal count = Capital + Interest;
                        //    punInterest = (count * (decimal)0.03).ToAmtString();
                        //}
                        #endregion
                        //// ***************************************************************************
                        //// CR181 逾期天数的计算逻辑为：从账单的欠费日期开始计算（欠费当天算第一天），
                        //// 直至当前日期（包含当前日期）
                        //// 2014 年8月5日   wangzhangming
                        //// ***************************************************************************
                        //var b = bills.OrderBy(p => p.DueDate).FirstOrDefault();
                        //tempOverDueDays = b == null ? 0 : (Int32)(DateTime.Now - b.DueDate.Value).TotalDays + 1;

                        string overDue = string.IsNullOrEmpty(item.OverdueNumber) ? string.Empty : string.Format("M{0}", item.OverdueNumber);
                        result.Add(new RelativeDaySearchDunViewData
                        {
                            //HouseholdAddress =item.HouseholdAddress,
                            ContractNo = item.ContractNo,
                            BusinessID = item.BusinessID,
                            CustomerID = item.CustomerID,
                            CustomerName = item.CustomerName,
                            DebtsAmt = item.DebtsAmt,
                            DunID = item.DunID,
                            DunNumber = item.DunNumber,
                            LoanCapital = item.LoanCapital,
                            LoanTime = item.LoanTime,
                            OverdueNumber = overDue, //.ToByte().ValueToName<EnumDunMark>(),
                            //Telephone = string.Empty,
                            CompanyAddress = string.Empty,
                            CreateTime = item.CreateTime,
                            Duner = item.Duner,
                            IsFreeze = item.IsFreeze.Length > 0 ? "<font color='red'>冻结</font>" : "正常",
                            //PunInterest = Pun, wichell  2014年10月13日 13:42:42 PCR012
                            OverDueDays = (string.IsNullOrEmpty(overDue) ? "-" : overDue)
                                     + "/" +
                                //tempOverDueDays.ToString(),
                                     item.DueDays,
                            ResidualCapital = item.ResidualCapital,
                            DunCodeName = item.DunCodeName,
                            LastTrackTime = item.LastTrackTime,
                            PersonID = item.PersonID,
                            OutSourceTime = item.OutSourceTime,
                            SavingUser = item.SavingUser
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }

    }
}

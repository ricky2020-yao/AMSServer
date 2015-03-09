using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.BLL.Products;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.BusinessService.Common;
using Cn.Vcredit.AMS.BusinessService.DAL;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.TypeConvert;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.BLL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-11-25
    /// Desc:订单详情-填账服务业务逻辑处理
    /// </summary>
    public class BusinessPayAccountBLL:BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public void PayAccount(PayAccountFilter filter, ResponseEntity responseEntity)
        {
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            string message = "";
            string guid = Guid.NewGuid().ToString();
            if (!string.IsNullOrEmpty(filter.JsonString))
            {
                if (!BusinessHelper.JsonToReceivedItem(filter, guid))
                {
                    message = "错误，实收金额不允许大于应收金额！";
                    ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, message);
                    m_Logger.Info(message);
                    return;
                }
            }

            if(filter.Amout > 0)
            {
                // 获取订单信息
                Business business = BusinessHelper.GetBusinessInfo(filter);
                List<UpdateItem> lstUpdateItem = new List<UpdateItem>();
                List<Received> lstReceived = new List<Received>();
                
                // 根据不同订单清贷状态分别处理
                if (business.CLoanStatus == (byte)EnumCLoanStatus.AdvanceProcess)
                {
                    #region 提前清贷自动填帐
                    var bills = BillDAL.GetArrearsBills(business.Bills);
                    var countAmout = bills.Sum(b => b.BillItems.Sum(i => i.DueAmt - i.ReceivedAmt));

                    // 提前清贷订单自定义付款金额必须大于等于提前清贷总额
                    if (filter.Amout < countAmout)
                    {
                        message = "抱歉，您的输入金额不足，提前清贷订单【欠费：" + countAmout + "】必须足额支付！";
                        ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, message);
                        m_Logger.Info(message);
                        return;
                    }
                    AutoReceivedByCLoan(business, 0, filter, lstUpdateItem, lstReceived);

                    // 插入临时表数据
                    BusinessHelper.SaveUpdateItemTempDataBase(guid, lstUpdateItem);
                    BusinessHelper.SaveReceivedTempDataBase(guid, lstReceived);

                    // 更新订单、帐单信息
                    filter.UpdateCloanApplyStatus = 1;
                    filter.Guid = guid;
                    Singleton<BusinessPayAccountDAL>.Instance.Update(filter);

                    // 更新订单、帐单信息
                    //UpdateBusinessInfo(filter, responseEntity);
                    //decimal losAmt = filter.Amout - countAmout;
                    var result = new PayAccountResultViewData();
                    result.LostAmt = filter.Amout - countAmout;

                    var responseResult = new ResponseListResult<PayAccountResultViewData>();
                    responseResult.TotalCount = 1;
                    responseResult.LstResult = new List<PayAccountResultViewData>();
                    responseResult.LstResult.Add(result);

                    responseEntity.Results = responseResult;
                    responseEntity.ResponseStatus = (int)EnumResponseState.Success;

                    return;
                    #endregion
                }
                else if (business.CLoanStatus == (byte)EnumCLoanStatus.Refunding)
                {
                    #region 普通账单自动填帐
					VcreditProduct s = null;
					if(business.ProductType == 8)
						s = new JingAnMortgageLoan(business);
					else if (business.ProductType == 9)
						s = new JingAnUnMortgageLoan(business);
					else
						 s = new UnsecuredLoan(business);

                    //成都填充项判断
                    List<BillItem> list = s.GetFillItems(filter.Amout);
                    if (list == null || list.Count < 1)
                    {
                        message = "金额不足,无法填充款项!";
                        ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, message);
                        m_Logger.Info(message);
                        return;
                    }
                    decimal couAcmou = 0;
                    //余额
                    foreach (var item in list)
                    {
                        decimal ssrmt = item.DueAmt - item.ReceivedAmt;
                        couAcmou += ssrmt;
                        item.ReceivedAmt = item.DueAmt;
                        item.FullPaidTime = filter.Dectime; 
                        
                        lstUpdateItem.Add(new UpdateItem
                        {
                            Id = item.BillItemID,
                            Amount = item.DueAmt,
                            FullPaidTime = filter.Dectime,
                            Type = 3
                        });

                        //_FinanceFactory.CreateBillItemDAL().Update(item);
                        //_FinanceContext.SaveChanges();
                        lstReceived.Add(new Received
                        {
                            BillID = item.BillID,
                            BillItemID = item.BillItemID,
                            Amount = ssrmt,
                            CreateTime = DateTime.Now,
                            OperatorID = filter.UserId,
                            PayID = 0,
                            ReceivedTime = filter.Dectime,
                            ReceivedType = (byte)EnumAdjustKind.Transfer,
                            DeductionID = 0,
                            Explain = ""
                        });
                    }

                    // 插入临时表数据
                    BusinessHelper.SaveUpdateItemTempDataBase(guid, lstUpdateItem);
                    BusinessHelper.SaveReceivedTempDataBase(guid, lstReceived);

                    // 更新订单、帐单信息
                    filter.UpdateCloanApplyStatus = 0;
                    filter.Guid = guid;
                    Singleton<BusinessPayAccountDAL>.Instance.Update(filter);

                    // 更新订单、帐单信息
                    //UpdateBusinessInfo(filter, responseEntity);
                    //decimal losAmt = filter.Amout - couAcmou;

                    var result = new PayAccountResultViewData();
                    result.LostAmt = filter.Amout - couAcmou;

                    var responseResult = new ResponseListResult<PayAccountResultViewData>();
                    responseResult.TotalCount = 1;
                    responseResult.LstResult = new List<PayAccountResultViewData>();
                    responseResult.LstResult.Add(result);

                    responseEntity.Results = responseResult;
                    responseEntity.ResponseStatus = (int)EnumResponseState.Success;
                    //ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                    #endregion
                }
                else
                {
                    message = "抱歉，当前的订单状态不允许手工扣款！";
                    ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, message);
                    m_Logger.Info(message);
                    return;
                }
            }
        }

        /// <summary>
        /// 为提前清贷订单进行自动填帐收款
        /// </summary>
        /// <param name="business"></param>
        /// <param name="payid"></param>
        /// <param name="filter"></param>
        /// <param name="lstUpdateItem"></param>
        /// <param name="lstReceived"></param>
        protected void AutoReceivedByCLoan(Business business, int payid, PayAccountFilter filter
            , List<UpdateItem> lstUpdateItem, List<Received> lstReceived)
        {
            var bills = BillDAL.GetArrearsBills(business.Bills);

            CloanApply apply = null;
            var lstApply = Singleton<CloanApplyDetailDAL<CloanApply>>.Instance.SearchData(filter);
            if (lstApply == null || lstApply.Count == 0)
                return;

            apply = lstApply[0];
            IEnumerable<CloanApplyItem> items = new List<CloanApplyItem>();
            if (apply != null)
            {
                filter.CloanApplyID = apply.CloanApplyID;
                apply.CloanApplyItems = Singleton<CloanApplyItemDetailDAL<CloanApplyItem>>.Instance.SearchData(filter);

                items = apply.CloanApplyItems.Where(p => p.IsAnnul);
            }

            if (bills.Count > 0)
            {
                if (items.Count() > 0)//有减免项，先做减免
                {
                    //提前清贷服务费重置为不包括减免金额
                    Bill advbill = bills.FirstOrDefault(p => p.BillType == (byte)EnumBillKind.Advance);
                    BillItem billitem = advbill.BillItems.FirstOrDefault(p => p.Subject == (byte)EnumCostSubject.AdvServiceFee);

                    billitem.DueAmt = billitem.DueAmt + items.Sum(p => p.Amount);
                    lstUpdateItem.Add(new UpdateItem
                    {
                        Id = billitem.BillItemID,
                        Amount = billitem.DueAmt,
                        Type = 2
                    });
                    //billItemDAL.Update(billitem);
                    foreach (var item in items)
                    {
                        AnnulBillItemBySubAndAmount(bills, item.Subject, item.Amount, payid, DateTime.Now, lstReceived, lstUpdateItem);
                    }
                }

                foreach (var bill in bills)
                {
                    FtpAdvRecevied(bill.BillItems.Where(p => p.DueAmt - p.ReceivedAmt > 0).ToList(), payid, DateTime.Now
                        , filter.UserId, filter.Explain, lstReceived, lstUpdateItem, EnumAdjustKind.Transfer);
                }
                //apply.CloanApplyStatus = (byte)EnumCloanApplyStatus.Success;
                //cLoanApplyDAL.Update(apply);
            }
            else
            {
                m_Logger.Error("提前清贷出错，未找到相应账单:" + business.BusinessID); ;
            }
        }

        /// <summary>
        /// 更新业务信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        private void UpdateBusinessInfo(PayAccountFilter filter, ResponseEntity responseEntity)
        {
            // 订单更新
            Singleton<BaseUpdateBLL<BusinessUpdateInfoDAL>>.Instance.UpdateData(filter, responseEntity);
        }

        /// <summary>
        /// Author:陈伟
        /// Target:支付基类[PaymentBase]
        /// Description:根据款项科目和减免金额进行减免操作(按照款项由近及远的顺序填充减免款项和金额)
        /// </summary>
        /// <param name="bills"></param>
        /// <param name="sub"></param>
        /// <param name="amount"></param>
        /// <param name="tranceid"></param>
        /// <param name="receviedtime"></param>
        /// <param name="receivedList"></param>
        /// <param name="lstUpdateItem"></param>
        private void AnnulBillItemBySubAndAmount(List<Bill> bills, byte sub, decimal amount,
            int tranceid, DateTime receviedtime, List<Received> receivedList, List<UpdateItem> lstUpdateItem)
        {
            bills.OrderByDescending(x => x.BillID).ToList().ForEach(p =>
            {
                BillItem item = p.BillItems.FirstOrDefault(o => o.Subject == sub);
                if (item != null && amount > 0)
                {
                    decimal dunamount = item.DueAmt - item.ReceivedAmt;
                    decimal recevamount = (amount - dunamount) > 0 ? dunamount : amount;//当前账单款项能扣金额
                    var recevieditem = new Received
                    {
                        Amount = -recevamount,
                        BillID = p.BillID,
                        BillItemID = item.BillItemID,
                        CreateTime = DateTime.Now,
                        OperatorID = 0,
                        PayID = 0,
                        ReceivedTime = receviedtime,
                        ReceivedType = (byte)EnumAdjustKind.Mitigation,
                        Explain = "第三方支付提前清贷减免"
                    };
                    receivedList.Add(recevieditem);
                    
                    amount = amount - recevamount;
                    item.DueAmt = item.DueAmt - recevamount;
                    var updateItem = new UpdateItem
                    {
                        Id = item.BillItemID,
                        Amount = item.DueAmt,
                        Type = 2
                    };
                    lstUpdateItem.Add(updateItem);
                    //dal.Update(item);
                }
            });
        }
        
        /// <summary>
        /// Author:陈伟
        /// Target:FTP提前清贷收款
        /// Description:根据款项列表进行收款操作
        /// </summary>
        /// <param name="billitems"></param>
        /// <param name="payid"></param>
        /// <param name="receviedtime"></param>
        /// <param name="userid"></param>
        /// <param name="explain"></param>
        /// <param name="adk"></param>
        /// <param name="receivedList"></param>
        /// <param name="lstUpdateItem"></param>
        private void FtpAdvRecevied(List<BillItem> billitems, int payid, DateTime receviedtime
            , int userid, string explain, List<Received> receivedList, List<UpdateItem> lstUpdateItem
            , EnumAdjustKind adk = EnumAdjustKind.Fuiou)
        {
            foreach (var item in billitems)
            {
                var recevieditem = new Received
                {
                    Amount = item.DueAmt - item.ReceivedAmt,
                    BillID = item.BillID,
                    BillItemID = item.BillItemID,
                    CreateTime = DateTime.Now,
                    OperatorID = userid,
                    PayID = payid,
                    ReceivedTime = receviedtime,
                    ReceivedType = (byte)adk,
                    Explain = explain
                };
                receivedList.Add(recevieditem);

                var updateItem = new UpdateItem
                {
                    Id = item.BillItemID,
                    Amount = item.DueAmt,
                    FullPaidTime = DateTime.Now,
                    Type = 3
                };
                lstUpdateItem.Add(updateItem);
            }
        }
    }
}

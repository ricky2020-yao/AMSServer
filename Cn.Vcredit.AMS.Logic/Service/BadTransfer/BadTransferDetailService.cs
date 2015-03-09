using Cn.Vcredit.AMS.Common.Command;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Common.Data;
using Cn.Vcredit.AMS.Common.Data.Filter;
using Cn.Vcredit.AMS.Common.Data.ViewData;
using Cn.Vcredit.AMS.Common.Entity;
using Cn.Vcredit.AMS.Common.Entity.ReponseResult;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Common.Service.Data;
using Cn.Vcredit.AMS.Common.Service.Interface;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Logic.BLL.FinanceProducts;
using Cn.Vcredit.AMS.Logic.Common;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.Service.BadTransfer
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:获取坏账清贷详细信息服务
    /// </summary>
    public class BadTransferDetailService: BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            string errorMessage = "";
            // 记录执行时间类
            using (StopWatcherAuto auto = new StopWatcherAuto())
            {
                try
                {
                    // 定义接收客户端参数的变量
                    IDictionary<string, string> paraDict = requestEntity.Parameters;
                    BadTransferFilter filter = LogicUtility.ConvertToFilterFromDict<BadTransferFilter>(paraDict);

                    // 获取坏账清贷欠费账单信息（欠费账单(当期+逾期)）
                    var oweBills
                        = Singleton<BadTransferDal>.Instance.GetBadTransferOweBill(filter.CloanApplyID);

                    int businessId = 0;
                    string path = "";
                    int i = 0;
                    var detailResults = new List<BadTransferDetailResultData>();
                    if (oweBills != null && oweBills.Count > 0)
                    {
                        var data = oweBills[0];
                        businessId = data.BusinessID;
                        path = data.Path;
                        var product = ProductFactory.Instance.GetProduct(data.BusinessLogicID);
                        var dirItems = product.GetProductItems();
                        foreach (var item in dirItems)
                        {
                            var subj = item.Key.ValueToEnum<EnumCostSubject>();
                            m_Logger.Debug("费用科目:" + subj);
                            decimal tAmt = oweBills.Where(x => x.Subject == (byte)subj)
                                        .Sum(o => o.DueAmt - o.ReceivedAmt);
                            m_Logger.Debug("费用金额:" + tAmt);

                            var resultData = new BadTransferDetailResultData();
                            resultData.SubjectId = (byte)subj;
                            resultData.SubjectName = item.Value.Replace("月", string.Empty);
                            resultData.SubjectValue = tAmt;
                            resultData.SubjectType = WebServiceConst.BadTransferFeeType_OweBill;
                            resultData.Order = ++i;
                            resultData.Path = path;
                            detailResults.Add(resultData);
                        }
                    }

                    if (businessId != 0)
                    {
                        // 获取坏账清贷欠费账单信息（其它费用）
                        var otherResult
                            = Singleton<BadTransferDal>.Instance.GetBadTransferOtherFee(businessId);

                        if (otherResult != null && otherResult.Count > 0)
                        {
                            foreach (BadTransferDetailOtherData data in otherResult)
                            {
                                var resultData = new BadTransferDetailResultData();
                                resultData.SubjectId = (byte)EnumCostSubject.GuaranteeLateFee;
                                resultData.SubjectName = EnumCostSubject.GuaranteeLateFee.ToDescription();
                                resultData.SubjectValue = data.GuaranteeLateFee;
                                resultData.SubjectType = WebServiceConst.BadTransferFeeType_Other;
                                resultData.Order = ++i;
                                detailResults.Add(resultData);

                                resultData = new BadTransferDetailResultData();
                                resultData.SubjectId = (byte)EnumCostSubject.Litigation;
                                resultData.SubjectName = EnumCostSubject.Litigation.ToDescription();
                                resultData.SubjectValue = data.Litigation;
                                resultData.SubjectType = WebServiceConst.BadTransferFeeType_Other;
                                resultData.Order = ++i;
                                detailResults.Add(resultData);

                                resultData = new BadTransferDetailResultData();
                                resultData.SubjectId = (byte)EnumCostSubject.LitigationLateFee;
                                resultData.SubjectName = EnumCostSubject.LitigationLateFee.ToDescription();
                                resultData.SubjectValue = data.LitigationLateFee;
                                resultData.SubjectType = WebServiceConst.BadTransferFeeType_Other;
                                resultData.Order = ++i;
                                detailResults.Add(resultData);
                            }
                        }
                    }

                    var responseResult = new ResponseListResult<BadTransferDetailResultData>();
                    responseResult.LstResult = detailResults;

                    responseEntity.ResponseStatus = (int)EnumResponseState.Success;
                    responseEntity.Results = responseResult;
                }
                catch (Exception ex)
                {
                    errorMessage = "";
                    responseEntity.ResponseStatus = (int)EnumResponseState.Others;
                    responseEntity.ResponseMessage = "获取坏账清贷详细信息失败。" + ex.Message.ToString();
                    m_Logger.Error("获取坏账清贷详细信息失败:" + ex.Message.ToString());
                    m_Logger.Error("获取坏账清贷详细信息失败:" + ex.StackTrace.ToString());
                }
                finally
                {
                    if (errorMessage.Length > 0)
                    {
                        responseEntity.ResponseStatus = (int)EnumResponseState.Others;
                        responseEntity.ResponseMessage = errorMessage;
                    }
                }
            }
        }
    }
}

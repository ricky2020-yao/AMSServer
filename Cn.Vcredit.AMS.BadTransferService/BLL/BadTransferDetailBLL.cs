using Cn.Vcredit.AMS.BadTransferService.BLL.FinanceProducts;
using Cn.Vcredit.AMS.BadTransferService.DAL;
using Cn.Vcredit.AMS.BadTransferService.Data.ViewData;
using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BadTransferService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:获取坏账清贷详细信息业务处理类
    /// </summary>
    public class BadTransferDetailBLL
    {
        #region 内部变量
        /// <summary>
        /// 日志记录
        /// </summary>
        protected ILogger m_Logger;
        #endregion

        /// <summary>
        /// 初始化方法
        /// </summary>
        public BadTransferDetailBLL()
        {
            m_Logger = LogFactory.CreateLogger(this.GetType());
        }

        /// <summary>
        /// 根据过滤条件，返回检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        /// <returns></returns>
        public void SearchDataByFilter(BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            BadTransferFilter filter = baseFilter as BadTransferFilter;
            if (filter == null)
                return;

            // 获取坏账清贷欠费账单信息（欠费账单(当期+逾期)）
            var oweBills
                = Singleton<BadTransferDetailDAL>.Instance.GetBadTransferOweBill(filter.CloanApplyID);

            int businessId = 0;
            string path = "";
            int i = 0;
            var detailResults = new List<BadTransferDetailResultViewData>();
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

                    var resultData = new BadTransferDetailResultViewData();
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
                    = Singleton<BadTransferDetailDAL>.Instance.GetBadTransferOtherFee(businessId);

                if (otherResult != null && otherResult.Count > 0)
                {
                    foreach (BadTransferDetailOtherData data in otherResult)
                    {
                        var resultData = new BadTransferDetailResultViewData();
                        resultData.SubjectId = (byte)EnumCostSubject.GuaranteeLateFee;
                        resultData.SubjectName = EnumCostSubject.GuaranteeLateFee.ToDescription();
                        resultData.SubjectValue = data.GuaranteeLateFee;
                        resultData.SubjectType = WebServiceConst.BadTransferFeeType_Other;
                        resultData.Order = ++i;
                        detailResults.Add(resultData);

                        resultData = new BadTransferDetailResultViewData();
                        resultData.SubjectId = (byte)EnumCostSubject.Litigation;
                        resultData.SubjectName = EnumCostSubject.Litigation.ToDescription();
                        resultData.SubjectValue = data.Litigation;
                        resultData.SubjectType = WebServiceConst.BadTransferFeeType_Other;
                        resultData.Order = ++i;
                        detailResults.Add(resultData);

                        resultData = new BadTransferDetailResultViewData();
                        resultData.SubjectId = (byte)EnumCostSubject.LitigationLateFee;
                        resultData.SubjectName = EnumCostSubject.LitigationLateFee.ToDescription();
                        resultData.SubjectValue = data.LitigationLateFee;
                        resultData.SubjectType = WebServiceConst.BadTransferFeeType_Other;
                        resultData.Order = ++i;
                        detailResults.Add(resultData);
                    }
                }
            }

            var responseResult = new ResponseListResult<BadTransferDetailResultViewData>();
            responseResult.LstResult = detailResults;

            ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
            responseEntity.Results = responseResult;
        }
    }
}

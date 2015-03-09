using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.GuarLitigationService.DAL;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.GuarLitigationService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年10月21日
    /// Description:担保和诉讼设置批量转担保逻辑操作类
    /// </summary>
    public class GuarLitigationInBatchNumBLL : BaseBLL
    {
        /// <summary>
        /// 批量转担保
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public void BatchConvertGurantee(BusinessGuaranteeFilter filter
            , ResponseEntity responseEntity)
        {
            var lstBus = Singleton<GuarLitigationInBatchNumDAL<Business>>.Instance.SearchData(filter);

            if (lstBus == null || lstBus.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.BusinessError, "查询不到订单。");
                m_Logger.Info("查询不到订单。");
                return;
            }
            string errorMessage = "";
            var lstBids = new List<int>();
            foreach (var bus in lstBus)
            {
                if (!string.IsNullOrEmpty(bus.GuaranteeNum))
                {
                    errorMessage = string.Format("错误，订单号[{0}]已存在担保批次号[{1}]", bus.BusinessID, bus.GuaranteeNum);
                    break;
                }
                else if (bus.BusinessStatus == (byte)EnumBusinessStatus.Normal)
                {
                    errorMessage = string.Format("错误，订单号[{0}]尚未担保，不允许输入担保批次号[{1}]", bus.BusinessID, bus.GuaranteeNum);
                    break;
                }
                else if (bus.LendingSideKey == SysConst.COMPANY_DWJM_LENDING)
                {
                    errorMessage = string.Format("错误，订单号[{0}]是外贸订单,不支持手动录入担保批次号", bus.BusinessID);
                    break;
                }
                else if (bus.BusinessStatus == (byte)EnumBusinessStatus.Guarantee)
                {
                    lstBids.Add(bus.BusinessID);
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.BusinessError, errorMessage);
                m_Logger.Info(errorMessage);
            }
            else if (lstBids != null && lstBids.Count != 0)
            {
                int resultCount = Singleton<GuarLitigationInBatchNumDAL<Business>>
                    .Instance.UpdateBatchNum(string.Join(WebServiceConst.Separater_Comma, lstBids.ToArray()), filter.GuaranteeNum);

                var responseResult = new ResponseListResult<BusinessViewData>();
                responseResult.TotalCount = resultCount;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }
    }
}

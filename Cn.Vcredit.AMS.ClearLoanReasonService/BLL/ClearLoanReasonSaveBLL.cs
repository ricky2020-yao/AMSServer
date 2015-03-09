using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.ClearLoanReasonService.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.ClearLoanReasonService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:清贷原因设置保存逻辑处理类
    /// </summary>
    public class ClearLoanReasonSaveBLL : BaseBLL
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void SaveData(BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            var filter = baseFilter as ClearLoanReasonSaveFilter;
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            var searchFilter = new ClearLoanReasonSearchFilter();
            searchFilter.ContractNo = filter.ContractNo;

            var lstResult = Singleton<ClearLoanReasonSearchDAL<ClearLoanReasonSearchViewData>>
                .Instance.SearchData(searchFilter);

            if (lstResult == null || lstResult.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, "内部异常");
                m_Logger.Info("内部异常。");
                return;
            }
            else
            {
                var business = lstResult[0];

                if (business.ClearLoanType == filter.ClearLoanType
                    && business.ClearLoanRemark == filter.ClearLoanRemark)
                {
                    ServiceUtility.SetResponseStatus(responseEntity
                        , EnumResponseState.Others, "未更改原因，请更改原因后保存。");
                    m_Logger.Info("未更改原因，请更改原因后保存。");
                    return;
                }

                filter.BusinessId = business.BusinessID;
                int ret = Singleton<ClearLoanReasonSaveDAL>.Instance.Update(filter);
                if (ret > 0)
                {
                    var responseResult = new ResponseListResult<ClearLoanReasonSearchViewData>();
                    responseResult.TotalCount = 1;
                    responseResult.LstResult = lstResult;

                    ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                    responseEntity.Results = responseResult;
                }
                else
                {
                    ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, "更新失败。");
                }
            }
        }
    }
}

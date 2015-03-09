using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.EveryDueRepayReportService.Common;
using Cn.Vcredit.AMS.EveryDueRepayReportService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.EveryDueRepayReportService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月18日
    /// Description:每日逾期客户还款明细报表检索逻辑处理类
    /// </summary>
    public class EveryDueRepayReportSearchBLL : BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(EveryDueRepayReportSearchFilter filter, ResponseEntity responseEntity)
        {
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            var reportDetailList
                = Singleton<EveryDueRepayReportSearchDAL<EveryDueRepayDetailReportViewData>>
                .Instance.SearchData(filter);

            if (reportDetailList == null || reportDetailList.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                ConvertUtil.ConvertEveryDueRepayDetailReportViewData(reportDetailList);

                IDictionary<string, object> outPutParam = filter.outParams;
                int totalCount = 0;
                if (!int.TryParse(outPutParam["@TotalCount"].ToString(), out totalCount))
                    totalCount = reportDetailList.Count;

                reportDetailList.ForEach(x => x.TotalCount = totalCount);
                var responseResult = new ResponseListResult<EveryDueRepayDetailReportViewData>();
                responseResult.TotalCount = totalCount;
                responseResult.LstResult = reportDetailList;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }
    }
}

using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.EveryDueReportService.DAL;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.BaseService.BLL;

namespace Cn.Vcredit.AMS.EveryDueReportService.BLL
{
    /// <summary>
    /// Author:wichell
    /// CreateTime:2014年10月9日
    /// Description:每日逾期静态报表查询
    /// </summary>
    public class EveryDueReportSearchBLL : BaseBLL
    {
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public virtual void Search(EveryDueReportFilter filter
            , ResponseEntity responseEntity)
        {
            if (filter == null)
                return;

            var reportDetailList = Singleton<EveryDueReportSearchDAL<EveryDueReportViewData>>
                .Instance.SearchData(filter);
            int count = Singleton<EveryDueReportSearchDAL<EveryDueReportViewData>>
                .Instance.GetCount(filter);

            if (reportDetailList == null || reportDetailList.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                var responseResult = new ResponseListResult<EveryDueReportViewData>();
                responseResult.TotalCount = count;
                responseResult.LstResult = reportDetailList;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }
    }
}

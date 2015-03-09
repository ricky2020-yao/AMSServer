using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.XtgJhzkCheckService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.XtgJhzkCheckService.BLL
{
    /// <summary>
    /// Author:Ricky
    /// CreateTime:2014-12-9
    /// Description:信托归集户账款核对清除到账日期逻辑处理层
    /// </summary>
    public class XtgJhzkCheckSearchBLL:BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(XtgJhzkCheckFilter filter, ResponseEntity responseEntity)
        {
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            // 获取件数
            int totalCount = 0;
            if (filter.IsReGetCount)
                totalCount = Singleton<XtgJhzkCheckSearchDAL<XtgJhzkViewData>>.Instance.GetCount(filter);
            else
                totalCount = filter.TotalCount;

            if (totalCount <= 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
                return;
            }

            decimal totalAmount = 0;
            if (filter.IsReGetCount)
                totalAmount = Singleton<XtgJhzkCheckSearchDAL<XtgJhzkViewData>>.Instance.GetSumAmount(filter);
            else
                totalAmount = filter.TotalAmount;

            if (filter.PageNo < 1)
                filter.PageNo = 1;

            int fromIndex = (filter.PageNo - 1) * filter.PageSize;
            int toIndex = filter.PageNo * filter.PageSize;
            if (toIndex > totalCount)
                toIndex = totalCount;

            filter.FromIndex = fromIndex;
            filter.ToIndex = toIndex;
            var lstXtgJhzkViewDatas = Singleton<XtgJhzkCheckSearchDAL<XtgJhzkViewData>>.Instance.SearchData(filter);

            if (lstXtgJhzkViewDatas == null || lstXtgJhzkViewDatas.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {
                lstXtgJhzkViewDatas.ForEach(x => x.TotalAmount = totalAmount);
                var responseResult = new ResponseListResult<XtgJhzkViewData>();
                responseResult.TotalCount = totalCount;
                responseResult.LstResult = lstXtgJhzkViewDatas;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }
    }
}

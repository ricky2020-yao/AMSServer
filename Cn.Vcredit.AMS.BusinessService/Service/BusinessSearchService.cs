using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Data.DB.MongoData;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.AMS.DataAccess.Mongo;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.BusinessService.DAL;
using Cn.Vcredit.Common.Enums;
using System.ComponentModel;


namespace Cn.Vcredit.AMS.BusinessService.Service
{
    /// <summary>
    /// Author:shwang
    /// Date:2014-08-15
    /// Desc:订单查询
    /// </summary>
    [Description("订单查询服务")]
    public class BusinessSearchService : BaseService.Service.BaseService
    {
        /// <summary>
        /// 服务执行
        /// </summary>
        /// <param name="requestEntity">请求数据</param>
        /// <param name="responseEntity">返回数据</param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            IDictionary<string, string> paraDict = requestEntity.Parameters;
            SearchBusinessListFilter filter = ServiceUtility.ConvertToFilterFromDict<SearchBusinessListFilter>(paraDict);
            List<BusinessViewData> businessViewDataList = Singleton<BusinessDAL>.Instance.GetViewData(filter);

            if (filter.RecordCount <= 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
                return;
            }
            else
            {
                ResponseListResult<BusinessViewData> result = new ResponseListResult<BusinessViewData>();

                result.TotalCount = (int)filter.RecordCount;
                result.LstResult = businessViewDataList;
                responseEntity.ResponseStatus = (int)EnumResponseState.Success;
                responseEntity.Results = result;
            }
        }
    }
}

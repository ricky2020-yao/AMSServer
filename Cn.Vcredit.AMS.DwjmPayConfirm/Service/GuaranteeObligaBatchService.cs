﻿using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.DwjmPayConfirm.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.DwjmPayConfirm.Service
{
    /// <summary>
    /// 履行担保义务批次服务
    /// </summary>
    [Description("履行担保义务批次服务")]
    public class GuaranteeObligaBatchService : BaseService.Service.BaseService
    {
        /// <summary>
        /// 服务执行
        /// </summary>
        /// <param name="requestEntity">请求数据</param>
        /// <param name="responseEntity">返回数据</param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            IDictionary<string, string> paraDict = requestEntity.Parameters;
            GuaranteeBatchFilter filter = ServiceUtility.ConvertToFilterFromDict<GuaranteeBatchFilter>(paraDict);
            List<GuaranteeObligaBatchViewData> guaranteeObligaBatchList = Singleton<GuaranteeObligaBatchDal>.Instance.GetGuaranteeObligationBatch(filter);
            ResponseListResult<GuaranteeObligaBatchViewData> result = new ResponseListResult<GuaranteeObligaBatchViewData>();
            result.TotalCount = (int)filter.RecordCount;
            result.LstResult = guaranteeObligaBatchList;
            responseEntity.ResponseStatus = (int)EnumResponseState.Success;
            responseEntity.Results = result;
        }
    }
}

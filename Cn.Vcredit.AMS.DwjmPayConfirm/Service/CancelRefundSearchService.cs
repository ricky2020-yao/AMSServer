using Cn.Vcredit.AMS.BaseService.Common;
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
    /// 解约退款查询服务
    /// </summary>
    [Description("解约退款查询服务")]
    public class CancelRefundSearchService : BaseService.Service.BaseService
    {
        /// <summary>
        /// 服务执行
        /// </summary>
        /// <param name="requestEntity">请求数据</param>
        /// <param name="responseEntity">返回数据</param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            IDictionary<string, string> paraDict = requestEntity.Parameters;
            CancelRefundFilter filter = ServiceUtility.ConvertToFilterFromDict<CancelRefundFilter>(paraDict);
            List<CancelRefundViewData> cancelRefundList = Singleton<CancelRefundDal>.Instance.GetCancelRefund(filter);
            ResponseListResult<CancelRefundViewData> result = new ResponseListResult<CancelRefundViewData>();
            result.TotalCount = filter.RecordCount;
            result.LstResult = cancelRefundList;
            responseEntity.ResponseStatus = (int)EnumResponseState.Success;
            responseEntity.Results = result;
        }
    }
}

using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BusinessService.BLL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.Service
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-11-25
    /// Desc:订单详情-保存调整款项服务
    /// </summary>
    [Description("订单详情-保存调整款项服务")]
    public class BusinessSaveService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 服务执行
        /// </summary>
        /// <param name="requestEntity">请求数据</param>
        /// <param name="responseEntity">返回数据</param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            IDictionary<string, string> paraDict = requestEntity.Parameters;
            var filter = ServiceUtility.ConvertToFilterFromDict<PayAccountFilter>(paraDict);
            filter.UserId = requestEntity.UserId;

            // 保存调整款项
            Singleton<BusinessSaveBLL>.Instance.PayAccount(filter, responseEntity);
        }
    }
}

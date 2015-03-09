using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.FulfilGuaranteeService.DAL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.FulfilGuaranteeService.Service
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-10
    /// Description:履行担保查询服务
    /// </summary>
    [Description("履行担保查询服务")]
    public class FulfilGuaranteeSearchService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            var filter
                = ServiceUtility.ConvertToFilterFromDict<GuaranteeBatchFilter>(requestEntity.Parameters);
            filter.UserId = responseEntity.UserId;

            // 获取履行担保
            Singleton<BaseSearchBLL<GuaranteeBatchViewData
                , FulfilGuaranteeSearchDAL<GuaranteeBatchViewData>>>.Instance
                .SearchDataPagingByFilter(filter, responseEntity);
        }
    }
}

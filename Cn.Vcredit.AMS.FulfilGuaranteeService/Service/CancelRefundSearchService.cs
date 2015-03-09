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
    /// Description:解约退款查询服务
    /// </summary>
    [Description("解约退款查询服务")]
    public class CancelRefundSearchService:BaseService.Service.BaseService
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
                = ServiceUtility.ConvertToFilterFromDict<CancelRefundFilter>(requestEntity.Parameters);
            filter.UserId = responseEntity.UserId;

            // 解约退款查询
            Singleton<BaseSearchBLL<CancelRefundViewData
                , CancelRefundSearchDAL<CancelRefundViewData>>>.Instance
                .SearchDataPagingByFilter(filter, responseEntity);
        }
    }
}

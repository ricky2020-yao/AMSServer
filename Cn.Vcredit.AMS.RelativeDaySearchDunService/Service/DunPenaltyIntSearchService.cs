using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.RelativeDaySearchDunService.DAL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.RelativeDaySearchDunService.Service
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-18
    /// Description:罚息信息查询服务
    /// </summary>
    [Description("罚息信息查询服务")]
    public class DunPenaltyIntSearchService : BaseService.Service.BaseService
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
                = ServiceUtility.ConvertToFilterFromDict<SearchBusinessListFilter>(requestEntity.Parameters);

            // 罚息信息查询
            Singleton<BaseSearchBLL<PenaltyIntViewData
                , DunPenaltyIntDetailDAL<PenaltyIntViewData>>>.Instance.SearchDataPagingByFilter(filter, responseEntity);
        }
    }
}

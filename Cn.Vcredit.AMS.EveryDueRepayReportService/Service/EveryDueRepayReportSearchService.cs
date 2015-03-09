using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.EveryDueRepayReportService.BLL;
using Cn.Vcredit.AMS.EveryDueRepayReportService.DAL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.EveryDueRepayReportService.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月10日
    /// Description:每日逾期客户还款明细报表查询服务
    /// </summary>
    [Description("每日逾期客户还款明细报表查询服务")]
    public class EveryDueRepayReportSearchService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            EveryDueRepayReportSearchFilter filter
                = ServiceUtility.ConvertToFilterFromDict<EveryDueRepayReportSearchFilter>(requestEntity.Parameters);

            // 客户查询
            Singleton<EveryDueRepayReportSearchBLL>.Instance.SearchData(filter, responseEntity);
        }
    }
}

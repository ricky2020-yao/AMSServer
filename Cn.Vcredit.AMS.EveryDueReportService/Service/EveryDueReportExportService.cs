using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.EveryDueReportService.BLL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.SavingCardChangeService.Service
{
    /// <summary>
    /// Author:wichell
    /// CreateTime:2014年10月9日
    /// Description:每日逾期静态报表导出服务
    /// </summary>
    [Description("每日逾期静态报表导出服务")]
    public class EveryDueReportExportService : BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            EveryDueReportFilter filter
                = ServiceUtility.ConvertToFilterFromDict<EveryDueReportFilter>(requestEntity.Parameters);

            // 储蓄卡修改提交修改
            Singleton<EveryDueReportExportBLL>.Instance.Search(filter, responseEntity);
        }
    }
}

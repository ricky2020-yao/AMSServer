using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.RelativeDaySearchDunService.BLL;
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
    /// CreateTime:2014-12-16
    /// Description:催收单明细收款导出服务
    /// </summary>
    [Description("催收单明细收款导出服务")]
    public class DunDetailExportService:BaseService.Service.BaseService
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
                = ServiceUtility.ConvertToFilterFromDict<ExportBusinessListFilter>(requestEntity.Parameters);

            // 催收单明细收款导出服务
            Singleton<DunDetailExportBLL>.Instance.ExportData(filter, responseEntity);
        }
    }
}

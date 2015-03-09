using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.DataExportService.BLL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.Service
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-12
    /// Description:每日扣导出服务
    /// </summary>
    [Description("每日扣导出服务")]
    public class DeriveRecExportService:BaseService.Service.BaseService
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
                = ServiceUtility.ConvertToFilterFromDict<DeriveRecExportFilter>(requestEntity.Parameters);

            // 每日扣导出
            Singleton<DeriveRecExportBLL>.Instance.ExportData(filter, responseEntity);
        }
    }
}

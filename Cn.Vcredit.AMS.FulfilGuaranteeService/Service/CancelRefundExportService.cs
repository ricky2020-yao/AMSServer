using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.FulfilGuaranteeService.BLL;
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
    /// Description:合同解约退款明细导出服务
    /// </summary>
    [Description("合同解约退款明细导出服务")]
    public class CancelRefundExportService:BaseService.Service.BaseService
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
                = ServiceUtility.ConvertToFilterFromDict<CancelRefundExportFilter>(requestEntity.Parameters);
            filter.UserId = responseEntity.UserId;

            // 导出合同解约退款明细
            Singleton<CancelRefundExportBLL>.Instance.ExportData(filter, responseEntity);
        }
    }
}

using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.ExportGuaranteeDataService.BLL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.ExportGuaranteeDataService.Service
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-18
    /// Description:担保客户数据导出
    /// </summary>
    [Description("担保客户数据导出")]
    public class ExportGuaranteeDataService:BaseService.Service.BaseService
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
                = ServiceUtility.ConvertToFilterFromDict<ExportGuaranteeDataFilter>(requestEntity.Parameters);

            // 担保客户数据导出
            Singleton<ExportGuaranteeDataBLL>.Instance.ExportData(filter, responseEntity);
        }
    }
}

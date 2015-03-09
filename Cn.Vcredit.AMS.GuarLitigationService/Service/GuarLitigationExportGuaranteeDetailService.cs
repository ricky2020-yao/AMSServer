using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.GuarLitigationService.BLL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.GuarLitigationService.Service
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年10月20日
    /// Description:担保和诉讼设置导出担保明细服务
    /// </summary>
    [Description("担保和诉讼设置导出担保明细服务")]
    public class GuarLitigationExportGuaranteeDetailService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            BusinessGuaranteeFilter filter
                = ServiceUtility.ConvertToFilterFromDict<BusinessGuaranteeFilter>(requestEntity.Parameters);

            // 担保和诉讼设置导出
            Singleton<GuarLitigationExportGuaranteeDetailBLL>.Instance.Search(filter, responseEntity);
        }
    }
}

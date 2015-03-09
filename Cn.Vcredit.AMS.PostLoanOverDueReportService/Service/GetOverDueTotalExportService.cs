using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.ExamineIMP;
using Cn.Vcredit.AMS.PostLoanOverDueReportService.BLL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.PostLoanOverDueReportService.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月14日
    /// Description:审核员贷后客户逾期情况汇总情况表导出服务
    /// </summary>
    [Description("审核员贷后客户逾期情况汇总情况表导出服务")]
    public class GetOverDueTotalExportService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            OverDueReportFilter filter
                = ServiceUtility.ConvertToFilterFromDict<OverDueReportFilter>(requestEntity.Parameters);
            filter.UserId = responseEntity.UserId;

            //检索数据获取出审核员贷后客户逾期情况汇总情况表
            Singleton<GetOverDueTotalExportBLL>.Instance.SearchData(filter, responseEntity);
        }
    }
}

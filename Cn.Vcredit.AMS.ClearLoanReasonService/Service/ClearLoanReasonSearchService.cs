using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.ClearLoanReasonService.BLL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.ClearLoanReasonService.Service
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:清贷原因设置查询服务
    /// </summary>
    [Description("清贷原因设置查询服务")]
    public class ClearLoanReasonSearchService : BaseService.Service.BaseService
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
                = ServiceUtility.ConvertToFilterFromDict<ClearLoanReasonSearchFilter>(requestEntity.Parameters);

            // 清贷原因设置导出
            Singleton<ClearLoanReasonSearchBLL>.Instance.SearchData(filter, responseEntity);
        }
    }
}

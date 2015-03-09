using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.DataExportService.BLL;
using Cn.Vcredit.AMS.DataExportService.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月05日
    /// Description:订单筛选导出服务
    /// </summary>
    [Description("订单筛选导出服务")]
    public class BusinesssDataExportService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            BusinessExportFilter filter
                = ServiceUtility.ConvertToFilterFromDict<BusinessExportFilter>(requestEntity.Parameters);

            // 检索数据
            Singleton<BusinesssDataExportBLL>.Instance.SearchData(filter, responseEntity);
        }
    }
}

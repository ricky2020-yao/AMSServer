using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.ExamineIMP;
using Cn.Vcredit.AMS.Entity.ViewData.ExamineIMP;
using Cn.Vcredit.AMS.PostLoanOverDueReportService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.PostLoanOverDueReportService.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月24日
    /// Description:获取订单的详情信息服务
    /// </summary>
    [Description("获取订单的详情信息服务")]
    public class GetOverDueReportDetailService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            OverDueDetailFilter filter
                = ServiceUtility.ConvertToFilterFromDict<OverDueDetailFilter>(requestEntity.Parameters);
            filter.UserId = responseEntity.UserId;

            //根据订单号获取订单的逾期信息
            Singleton<BaseSearchBLL<OverDueReportDetailViewData, GetOverDueReportDetailDAL<OverDueReportDetailViewData>>>
                .Instance.SearchData(filter, responseEntity);
        }
    }
}

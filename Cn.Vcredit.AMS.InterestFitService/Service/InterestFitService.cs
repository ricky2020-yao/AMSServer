using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.InterestFitService.BLL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.InterestFitService.Service
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年12月3日
    /// Description:获取代偿款支付设置信息
    /// </summary>
    [Description("获取代偿款支付设置信息")]
    public class InterestFitService:BaseService.Service.BaseService
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
                = ServiceUtility.ConvertToFilterFromDict<InterestFitFilter>(requestEntity.Parameters);

            // 获取代偿款支付设置信息
            Singleton<InterestFitBLL>.Instance.SearchData(filter, responseEntity);
        }
    }
}

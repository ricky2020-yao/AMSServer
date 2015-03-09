using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.InterestFitService.DAL;
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
    /// CreateTime:2014年12月4日
    /// Description：更新代偿款支付剩余本息支付日
    /// </summary>
    [Description("更新代偿款支付剩余本息支付日")]
    public class InterestFitUpdateService : BaseService.Service.BaseService
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

            // 更新代偿款支付剩余本息支付日
            Singleton < BaseUpdateBLL<InterestFitUpdateDAL>>.Instance.UpdateData(filter, responseEntity);
        }
    }
}

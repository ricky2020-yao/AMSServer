using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.CustomerSearchService.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.CustomerService;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.CustomerSearchService.Service
{
    /// <summary>
    /// Author:Ricky
    /// CreateTime:2015年3月5号
    /// Description:更新客户发送短信标志的服务
    /// </summary>
    [Description("更新客户发送短信标志的服务")]
    public class UpdateCustomerSendMessgeService : BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            CustomerSearchFilter filter
                = ServiceUtility.ConvertToFilterFromDict<CustomerSearchFilter>(requestEntity.Parameters);
            filter.UserId = responseEntity.UserId;

            // 更新客户发送短信标志
            Singleton<BaseUpdateBLL<UpdateCustomerSendMessgeDAL>>.Instance.UpdateData(filter, responseEntity);
        }
    }
}

using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.CustomerSearchService.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.CustomerService;
using Cn.Vcredit.AMS.Entity.ViewData.CustomerService;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.CustomerSearchService.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月9日
    /// Description:客户查询服务
    /// </summary>
    [Description("客户查询服务")]
    public class GetCustomerResultService : BaseService.Service.BaseService
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

            // 客户查询
            Singleton<BaseSearchBLL<
                CustomerSearchViewData, GetCustomerResultDAL<CustomerSearchViewData>>>.Instance.SearchDataPagingByFilter(filter, responseEntity);
        }
    }
}

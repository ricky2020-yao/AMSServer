using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.XtgJhzkCheckService.BLL;
using Cn.Vcredit.AMS.XtgJhzkCheckService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.XtgJhzkCheckService.Service
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年9月01日
    /// Description:信托归集户账款核对查询服务
    /// </summary>
    [Description("信托归集户账款核对查询服务")]
    public class XtgJhzkCheckSearchService:BaseService.Service.BaseService
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
                = ServiceUtility.ConvertToFilterFromDict<XtgJhzkCheckFilter>(requestEntity.Parameters);
            filter.UserId = responseEntity.UserId;

            // 获取信托归集户账款核对数据
            Singleton<XtgJhzkCheckSearchBLL>.Instance.SearchData(filter, responseEntity);
        }
    }
}

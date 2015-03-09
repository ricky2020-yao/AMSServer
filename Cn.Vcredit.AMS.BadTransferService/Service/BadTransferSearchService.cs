using Cn.Vcredit.AMS.BadTransferService.BLL;
using Cn.Vcredit.AMS.BadTransferService.DAL;
using Cn.Vcredit.AMS.BadTransferService.Data.ViewData;
using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Data;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BadTransferService
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月12日
    /// Description:坏账清贷检索服务
    /// </summary>
    [Description("坏账清贷检索服务")]
    public class BadTransferSearchService : BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            BadTransferFilter filter = ServiceUtility.ConvertToFilterFromDict<BadTransferFilter>(requestEntity.Parameters);
            filter.UserId = responseEntity.UserId;

            // 检索数据
            Singleton<BaseSearchBLL<BadTransferSearchViewData, BadTransferSearchDAL<BadTransferSearchViewData>>>
                .Instance.SearchData(filter, responseEntity);
        }
    }
}

using Cn.Vcredit.AMS.BadTransferService.BLL;
using Cn.Vcredit.AMS.BadTransferService.BLL.FinanceProducts;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BadTransferService.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:获取坏账清贷详细信息服务
    /// </summary>
    [Description("获取坏账清贷详细信息服务")]
    public class BadTransferDetailService : BaseService.Service.BaseService
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

            // 获取坏账清贷欠费账单信息（欠费账单(当期+逾期)）
            Singleton<BadTransferDetailBLL>.Instance.SearchDataByFilter(filter, responseEntity);
        }
    }
}

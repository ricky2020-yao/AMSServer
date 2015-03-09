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
    /// Description:保存清贷原因设置服务
    /// </summary>
    [Description("保存清贷原因设置服务")]
    public class ClearLoanReasonSaveService:BaseService.Service.BaseService
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
                = ServiceUtility.ConvertToFilterFromDict<ClearLoanReasonSaveFilter>(requestEntity.Parameters);

            // 保存清贷原因
            Singleton<ClearLoanReasonSaveBLL>.Instance.SaveData(filter, responseEntity);
        }
    }
}

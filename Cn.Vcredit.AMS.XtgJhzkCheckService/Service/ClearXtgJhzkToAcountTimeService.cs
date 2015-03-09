using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
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
    /// Author:姚海凡
    /// CreateTime:2014年9月01日
    /// Description:信托归集户账款核对清除到账日期服务
    /// </summary>
    [Description("信托归集户账款核对清除到账日期服务")]
    public class ClearXtgJhzkToAcountTimeService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            XtgJhzkCheckFilter filter
                = ServiceUtility.ConvertToFilterFromDict<XtgJhzkCheckFilter>(requestEntity.Parameters);
            filter.UserId = responseEntity.UserId;

            // 清除到账日期
            Singleton<BaseUpdateBLL<ClearXtgJhzkToAcountTimeDAL>>.Instance.UpdateData(filter, responseEntity);
        }
    }
}

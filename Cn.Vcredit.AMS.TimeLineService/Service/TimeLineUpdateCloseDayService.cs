using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.TimeLineService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.TimeLineService.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月19日
    /// Description:时间轴设置更新关帐日服务
    /// </summary>
    [Description("时间轴设置更新关帐日服务")]
    public class TimeLineUpdateCloseDayService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            TimeLineUpdateFilter filter
                = ServiceUtility.ConvertToFilterFromDict<TimeLineUpdateFilter>(requestEntity.Parameters);
            filter.UserId = responseEntity.UserId;

            // 时间轴设置更新关帐日
            Singleton<BaseUpdateBLL<TimeLineUpdateCloseDayDAL>>.Instance.UpdateData(filter, responseEntity);
        }
    }
}

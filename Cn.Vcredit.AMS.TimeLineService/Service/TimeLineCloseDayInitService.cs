using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.Caches;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.TimeLineService.BLL;
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
    /// Description:时间轴设置关帐日初始化服务
    /// </summary>
    [Description("时间轴设置关帐日初始化服务")]
    public class TimeLineCloseDayInitService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            TimeLineInitFilter filter
                = ServiceUtility.ConvertToFilterFromDict<TimeLineInitFilter>(requestEntity.Parameters);
            filter.UserId = responseEntity.UserId;

            // 获取关账日信息
            Singleton<TimeLineCloseDayInitBLL>.Instance.SearchData(filter, responseEntity);
        }
    }
}

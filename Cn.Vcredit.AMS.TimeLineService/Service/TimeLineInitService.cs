using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BaseService.Service;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.Caches;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
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
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.TimeLineService.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月18日
    /// Description:时间轴设置数据初始化服务
    /// </summary>
    [Description("时间轴设置数据初始化服务")]
    public class TimeLineInitService :BaseService.Service.BaseService
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

            // 获取时间轴设置数据
            Singleton<TimeLineInitBLL>.Instance.GetCloseBillTime(filter, responseEntity);
        }
    }
}

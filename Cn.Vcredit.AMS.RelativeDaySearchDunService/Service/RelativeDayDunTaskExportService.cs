using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.RelativeDaySearchDunService.BLL;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.RelativeDaySearchDunService.Service
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:催收任务导出
    /// </summary>
    [Description("催收任务导出")]
    public class RelativeDayDunTaskExportService:BaseService.Service.BaseService
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
                = ServiceUtility.ConvertToFilterFromDict<RelativeDayExportDunFilter>(requestEntity.Parameters);

            // 获取用户拥有的门店权限
            List<string> companys = Singleton<RedisEnumOperatorBLL>.Instance.GetUserOwnStoreKeys(responseEntity.UserId);
            if (companys != null && companys.Count > 0)
                filter.BranchKey = string.Join(WebServiceConst.Separater_Comma_Quote, companys.ToArray());

            // 催收任务导出
            Singleton<RelativeDayDunTaskExportBLL>.Instance.ExportData(filter, responseEntity);
        }
    }
}

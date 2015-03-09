using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.DataExportService.BLL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.Service
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-12
    /// Description:现收数据导出
    /// </summary>
    [Description("现收数据导出")]
    public class DeriveSevReceivedExportService:BaseService.Service.BaseService
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
                = ServiceUtility.ConvertToFilterFromDict<DeriveSevExportFilter>(requestEntity.Parameters);

            // 获取用户拥有的门店权限
            List<string> companys = Singleton<RedisEnumOperatorBLL>.Instance.GetUserOwnStoreKeys(responseEntity.UserId);
            if(companys != null && companys.Count > 0)
                filter.BranchKey = "'" + string.Join(WebServiceConst.Separater_Comma_Quote, companys.ToArray()) + "'";

            // 现收数据导出
            Singleton<DeriveSevReceivedExportBLL>.Instance.ExportData(filter, responseEntity);
        }
    }
}

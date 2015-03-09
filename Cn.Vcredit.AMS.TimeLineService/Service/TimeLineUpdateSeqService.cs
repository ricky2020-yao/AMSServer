using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.Caches;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.TimeLineService.DAL;
using Cn.Vcredit.Common.Constants;
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
    /// Description:时间轴设置更新序列服务
    /// </summary>
    [Description("时间轴设置更新序列服务")]
    public class TimeLineUpdateSeqService:BaseService.Service.BaseService
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

            List<string> companyKeys = new List<string>();
            if (!string.IsNullOrEmpty(filter.CompanyKeys))
            {
                companyKeys = filter.CompanyKeys.Split(WebServiceConst.Separater_Comma.ToArray()).ToList();
            }

            //List<int> companyIds = Singleton<CompanyCache>.Instance.CompanyIds(requestEntity.UserId);
            if (companyKeys == null || companyKeys.Count == 0)
            {
                responseEntity.ResponseStatus = (int)EnumResponseState.Others;
                responseEntity.ResponseMessage = "当前账户下无子公司Id";
                return;
            }

            string companyKey = companyKeys[0];
            filter.CompanyKey = companyKey;

            // 时间轴设置更新序列
            Singleton<BaseUpdateBLL<TimeLineUpdateSeqDAL>>.Instance.UpdateData(filter, responseEntity);
        }
    }
}

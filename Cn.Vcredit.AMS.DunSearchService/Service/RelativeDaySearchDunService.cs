using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Common;
using Cn.Vcredit.Common.Tools;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.BaseService.Common;

namespace Cn.Vcredit.AMS.DunSearchService.Service
{
    public class RelativeDaySearchDunService : BaseService.Service.BaseService
    {
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 记录执行时间类
            using (StopWatcherAuto auto = new StopWatcherAuto())
            {
                try
                {
                    // 定义接收客户端参数的变量
                    IDictionary<string, string> paraDict = requestEntity.Parameters;
                    DunSearchFilter filter
                      = ServiceUtility.ConvertToFilterFromDict<DunSearchFilter>(paraDict);
                    filter.UserId = responseEntity.UserId;

                    //检索数据获取符合条件的催收单信息
                    //Singleton<GetOverDueReportSearchResultBLL>.Instance.SearchData(filter, responseEntity);
                    Singleton<DunSearchBLL>.Instance.SearchData(filter, responseEntity);
                }
                catch (Exception ex)
                {
                    responseEntity.ResponseStatus = (int)EnumResponseState.Others;
                    responseEntity.ResponseMessage = "催收单列表查询失败。" + ex.Message.ToString();
                    m_Logger.Error("催收单列表查询失败:" + ex.Message.ToString());
                    m_Logger.Error("催收单列表查询失败:" + ex.StackTrace.ToString());
                }
            }
        }
    }
}

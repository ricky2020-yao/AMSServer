using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.DictionaryService.BLL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.Common;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.DictionaryService
{
    /// <summary>
    /// 获取枚举的WebService
    /// </summary>
    [Description("获取枚举服务")]
    public class QueryEnumerationService : BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
         // 记录执行时间类
            using (StopWatcherAuto auto = new StopWatcherAuto())
            {
                IDictionary<string, string> paraDict = requestEntity.Parameters;

                BaseFilter filter = ServiceUtility.ConvertToFilterFromDict<QueryEnumerationFilter>(paraDict);
                filter.UserId = requestEntity.UserId;

                Singleton<QueryEnumerationBLL>.Instance.QueryEnumeration(filter, responseEntity);
            }
        }
    }
}

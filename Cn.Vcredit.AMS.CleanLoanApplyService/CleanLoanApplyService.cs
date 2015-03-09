using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.CleanLoanApplyService
{
    /// <summary>
    /// 提前清贷/坏账清贷 WebService
    /// </summary>
    public class CleanLoanApplyService : BaseService.Service.BaseService
    {
        protected override void DoExecute(Entity.Communication.RequestEntity requestEntity, Entity.Communication.ResponseEntity responseEntity)
        {
            using (StopWatcherAuto auto = new StopWatcherAuto())
            {
                IDictionary<string, string> paraDict = requestEntity.Parameters;

                //BaseFilter filter = ServiceUtility.ConvertToFilterFromDict<QueryEnumerationFilter>(paraDict);
                //filter.UserId = requestEntity.UserId;
            }
        }
    }
}

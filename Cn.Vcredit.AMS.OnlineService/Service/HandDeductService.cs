
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.OnlineService.Data;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.AMS.BaseService.Common;

namespace Cn.Vcredit.AMS.OnlineService.Service
{
    public class HandDeductService : BaseService.Service.BaseService
    {
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            IDictionary<string, string> paraDict = requestEntity.Parameters;
            if (!paraDict.ContainsKey("Businessid"))
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }
            ResponseListResult<HandDeductDistribute> result = new ResponseListResult<HandDeductDistribute>();
            HandDeductDistribute handDeductDistribute = new HandDeductDistribute();
            handDeductDistribute.DistributeGuid = Guid.NewGuid();
            handDeductDistribute.Result = 0;
            handDeductDistribute.Status = 0;

            result.LstResult.Add(handDeductDistribute); 
            ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
            responseEntity.Results = result;
        }
    }
}
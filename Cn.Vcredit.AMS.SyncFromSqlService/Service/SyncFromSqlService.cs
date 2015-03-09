using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.Filter.Common;
using Cn.Vcredit.AMS.SyncFromSqlService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cn.Vcredit.AMS.SyncFromSqlService
{
    /// <summary>
    /// Author:shwang
    /// Date:20141216
    /// Desc:同步数据服务
    /// </summary>
    [Description("同步数据服务")]
    public class SyncFromSqlService : BaseService.Service.BaseService
    {
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            IDictionary<string, string> paraDict = requestEntity.Parameters;
            SyncTableFilter filter = ServiceUtility.ConvertToFilterFromDict<SyncTableFilter>(paraDict);
            string errorInfo;
            bool success = Singleton<SyncSqlDAL>.Instance.SyncTable(filter, out errorInfo);
            ResponseResult result = new ResponseResult();
            if(success)
                responseEntity.ResponseStatus = (int)EnumResponseState.Success;
            else
            {
                responseEntity.ResponseMessage = errorInfo;
                responseEntity.ResponseStatus = (int)EnumResponseState.Others;
            }
            responseEntity.Results = result;
        }
    }
}

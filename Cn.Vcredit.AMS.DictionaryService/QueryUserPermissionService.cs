using Cn.Vcredit.AMS.Entity.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.Caches;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.AMS.Data.DB.MongoData;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.Common.Enums;

namespace Cn.Vcredit.AMS.DictionaryService
{
    /// <summary>
    /// 执行服务查询方法
    /// </summary>
    /// <param name="requestEntity">请求实体</param>
    /// <param name="responseEntity">返回实体</param>
    public class QueryUserPermissionService : BaseService.Service.BaseService
    {
        /// <summary>
        /// 执行服务查询方法
        /// </summary>
        /// <param name="requestEntity">请求实体</param>
        /// <param name="responseEntity">返回实体</param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            IDictionary<string, string> paraDict = requestEntity.Parameters;
            if (!paraDict.ContainsKey("UserId"))
            {
                responseEntity.ResponseStatus = (int)EnumResponseState.RequestCommandError;
                responseEntity.ResponseMessage = "入参校验错误，缺少UserId";
                return;
            }
            int userId;
            if (!int.TryParse(paraDict["UserId"], out userId))
            {
                responseEntity.ResponseStatus = (int)EnumResponseState.RequestCommandError;
                responseEntity.ResponseMessage = "入参校验错误，UserId不是数字";
                return;
            }

            List<MongoUserPermission> userPermissionDataList = Singleton<CompanyCache>.Instance.GetPermission(userId);
            ResponseListResult<MongoUserPermission> result = new ResponseListResult<MongoUserPermission>();
            result.TotalCount = userPermissionDataList.Count;
            result.LstResult = userPermissionDataList;
            responseEntity.ResponseStatus = (int)EnumResponseState.Success;
            responseEntity.Results = result;           
        }
    }
}

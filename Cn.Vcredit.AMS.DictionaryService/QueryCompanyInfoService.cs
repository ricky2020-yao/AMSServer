using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.Caches;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.DictionaryService
{
    /// <summary>
    /// 有权限的公司名称
    /// </summary>
    public class QueryCompanyInfoService : BaseService.Service.BaseService
    {
        /// <summary>
        /// 执行服务查询方法
        /// </summary>
        /// <param name="requestEntity">请求实体</param>
        /// <param name="responseEntity">返回实体</param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
          
            IDictionary<string, string> paraDict = requestEntity.Parameters;
            if(!paraDict.ContainsKey("UserId"))
            {
                responseEntity.ResponseStatus = (int)EnumResponseState.RequestCommandError;
                responseEntity.ResponseMessage = "入参校验错误，缺少UserId";
                return;
            }
            int userId;
            if(!int.TryParse(paraDict["UserId"],out userId))
            {
                responseEntity.ResponseStatus = (int)EnumResponseState.RequestCommandError;
                responseEntity.ResponseMessage = "入参校验错误，UserId不是数字";
                return;
            }

            IEnumerable<Enumeration> companyData = Singleton<CompanyCache>.Instance.GetCompanyEnumeration(userId);
            List<Enumeration> companyDataList = companyData.ToList();
            ResponseListResult<Enumeration> result = new ResponseListResult<Enumeration>();
            result.TotalCount = companyDataList.Count;
            result.LstResult = companyDataList;
            responseEntity.ResponseStatus = (int)EnumResponseState.Success;
            responseEntity.Results = result;           
        }
    }
}

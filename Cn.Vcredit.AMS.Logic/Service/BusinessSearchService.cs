using Cn.Vcredit.Common.DB;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Tools;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.AMS.Common.Data;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Common.Entity;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Common.Command;
using Cn.Vcredit.AMS.Common.Service.Data;
using Cn.Vcredit.AMS.Common.Service.Interface;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.DataAccess.Caches;
using Cn.Vcredit.AMS.Logic.BLL.PayBank;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Logic.Common;
using Cn.Vcredit.AMS.Common.Data.Filter;
using Cn.Vcredit.AMS.DataAccess.Mongo;
using Cn.Vcredit.AMS.Data.DB.MongoData;
using Cn.Vcredit.AMS.Common.Entity.ReponseResult;

namespace Cn.Vcredit.AMS.Logic.Service
{
    public class BusinessSearchService : BaseService,IService
    {
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            IDictionary<string, string> paraDict = requestEntity.Parameters;
            SearchBusinessListFilter filter = LogicUtility.ConvertToFilterFromDict<SearchBusinessListFilter>(paraDict);
            List<MongoBusiness> mongoBusinessList = Singleton<BusinessInfo>.Instance.GetBusinessList(filter);
            ResponseListResult<MongoBusiness> result = new ResponseListResult<MongoBusiness>();
            result.TotalCount = (int)filter.RecordCount;
            result.LstResult = mongoBusinessList;
            responseEntity.ResponseStatus = (int)EnumResponseState.Success;
            responseEntity.Results = result;
        }
    }
}

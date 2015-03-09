using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.GuarBusinessSearchService.DAL;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.GuarBusinessSearchService
{
    [Description("入担保查询服务")]
    public class QueryGuarBusinessService : BaseService.Service.BaseService
    {
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            BaseFilter filter = ServiceUtility.ConvertToFilterFromDict<QueryGuarBusinessFilter>(requestEntity.Parameters);
            filter.UserId = requestEntity.UserId;

            //查询入担保信息
            Singleton<BaseSearchBLL<GuarBusinessViewData, GuarBusinessSearchDAL>>.Instance.SearchData(filter, responseEntity);
        }
    }
}

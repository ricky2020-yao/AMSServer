using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage.BillDun;
using Cn.Vcredit.AMS.GuarBusinessSearchService.BLL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.GuarBusinessSearchService
{
    [Description("入担保导出服务")]
    public class GuarBusinessExportService:BaseService.Service.BaseService
    {
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            QueryGuarBusinessFilter filter = ServiceUtility.ConvertToFilterFromDict<QueryGuarBusinessFilter>(requestEntity.Parameters);
            filter.UserId = requestEntity.UserId;

            //导出入担保信息
            Singleton<GuarBusinessExportServiceBLL>.Instance.SearchData(filter, responseEntity);
        }
    }
}

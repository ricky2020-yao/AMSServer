using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.CustomerService;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cn.Vcredit.AMS.DeductRemindService.BLL;
using System.ComponentModel;

namespace Cn.Vcredit.AMS.DeductRemindService
{
    [Description("首月还款提醒导出服务")]
    public class ExportDeductRemindService:BaseService.Service.BaseService
    {
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            DeductRemindExportFilter filter
                = ServiceUtility.ConvertToFilterFromDict<DeductRemindExportFilter>(requestEntity.Parameters);

            // 客户查询
            Singleton<DeductRemindExportBLL>.Instance.SearchData(filter, responseEntity);
        }
    }
}

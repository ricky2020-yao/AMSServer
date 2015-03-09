using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.DwjmPayConfirm.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.DwjmPayConfirm.Service
{
    /// <summary>
    /// 更新履行担保收款服务
    /// </summary>
    [Description("更新履行担保收款服务")]
    public class UpdateGuaranteeBatchService : BaseService.Service.BaseService
    {
        /// <summary>
        /// 服务执行
        /// </summary>
        /// <param name="requestEntity">请求数据</param>
        /// <param name="responseEntity">返回数据</param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            IDictionary<string, string> paraDict = requestEntity.Parameters;
            GuaranteeBatchUpdateFilter filter = ServiceUtility.ConvertToFilterFromDict<GuaranteeBatchUpdateFilter>(paraDict);
            // 更新数据
            Singleton<BaseUpdateBLL<GuaranteeBatchUpdateDal>>.Instance.UpdateData(filter, responseEntity);
        }
    }
}

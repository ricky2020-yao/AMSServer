using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.GuarLitigationService.DAL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.GuarLitigationService.Service
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年10月22日
    /// Description:担保和诉讼设置手动转担保保存服务
    /// </summary>
    [Description("担保和诉讼设置手动转担保保存服务")]
    public class GuarLitigationManualService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            BusinessGuaranteeSaveFilter filter
                = ServiceUtility.ConvertToFilterFromDict<BusinessGuaranteeSaveFilter>(requestEntity.Parameters);
            filter.UserId = requestEntity.UserId;

            // 担保和诉讼设置手动转担保更新
            Singleton<BaseUpdateBLL<GuarLitigationManualDAL>>.Instance.UpdateData(filter, responseEntity);
        }
    }
}

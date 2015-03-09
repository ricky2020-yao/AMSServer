using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.GuarLitigationService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.GuarLitigationService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年10月21日
    /// Description:担保和诉讼设置导出服务逻辑操作类
    /// </summary>
    public class GuarLitigationEditBLL : BaseBLL
    {
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public virtual void SearchData(BusinessGuaranteeFilter filter
            , ResponseEntity responseEntity)
        {
            var lstBus = Singleton<GuarLitigationEditDAL<BusinessViewData>>
                .Instance.SearchData(filter);

            if (lstBus == null || lstBus.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
                return;
            }

            // 查询关帐日
            DateTime stopDate = Singleton<GuarLitigationEditDAL<BusinessViewData>>.Instance.GetCloseBillDate();
            foreach (var bus in lstBus)
            {
                bus.StopDate = stopDate;
            }

            var responseResult = new ResponseListResult<BusinessViewData>();
            responseResult.TotalCount = lstBus.Count;
            responseResult.LstResult = lstBus;
            

            ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
            responseEntity.Results = responseResult;
        }
    }
}

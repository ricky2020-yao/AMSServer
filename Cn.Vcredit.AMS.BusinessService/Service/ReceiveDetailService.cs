using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BusinessService.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.Service
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-11-25
    /// Desc:订单详情-实收数据查询服务
    /// </summary>
    [Description("订单详情-实收数据查询服务")]
    public class ReceiveDetailService : BaseService.Service.BaseService
    {
        /// <summary>
        /// 服务执行
        /// </summary>
        /// <param name="requestEntity">请求数据</param>
        /// <param name="responseEntity">返回数据</param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            IDictionary<string, string> paraDict = requestEntity.Parameters;
            var filter = ServiceUtility.ConvertToFilterFromDict<SearchBusinessListFilter>(paraDict);

            // 检索数据
            Singleton<BaseSearchBLL<ReceiveDetailViewData
                , ReceiveDetailDAL<ReceiveDetailViewData>>>.Instance.SearchData(filter, responseEntity);
        }
    }
}

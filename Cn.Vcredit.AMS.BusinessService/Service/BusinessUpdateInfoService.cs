using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BusinessService.DAL;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
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
    /// Date:2014-11-14
    /// Desc:订单详情-订单更新服务
    /// </summary>
    [Description("订单详情-订单更新服务")]
    public class BusinessUpdateInfoService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 服务执行
        /// </summary>
        /// <param name="requestEntity">请求数据</param>
        /// <param name="responseEntity">返回数据</param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            SearchBusinessListFilter filter
                = ServiceUtility.ConvertToFilterFromDict<SearchBusinessListFilter>(requestEntity.Parameters);
            filter.UserId = responseEntity.UserId;

            List<string> companys = Singleton<RedisEnumOperatorBLL>.Instance.GetUserOwnCompanyKeys(responseEntity.UserId);
            filter.BranchKey = string.Join("','", companys.ToArray());

            // 订单更新
            Singleton<BaseUpdateBLL<BusinessUpdateInfoDAL>>.Instance.UpdateData(filter, responseEntity);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.Common.Patterns;

namespace Cn.Vcredit.AMS.BusinessService.DAL
{
    /// <summary>
    /// 订单相关逻辑
    /// </summary>
    public class BusinessDAL
    {
        /// <summary>
        /// 查询订单信息
        /// </summary>
        /// <param name="searchBusinessListFilter">查询条件包括分页</param>
        /// <returns>查询数据内容</returns>
        public List<BusinessViewData> GetViewData(SearchBusinessListFilter searchBusinessListFilter)
        {
            return Singleton<DataAccess.DAL.BusinessDal>.Instance.GetBusinessInfo<BusinessViewData>(searchBusinessListFilter);
        }

    }
}

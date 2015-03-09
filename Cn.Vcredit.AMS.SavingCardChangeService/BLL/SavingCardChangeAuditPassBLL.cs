using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.SavingCardChangeService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.SavingCardChangeService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月29日
    /// Description:储蓄卡修改审核通过业务逻辑处理类
    /// </summary>
    public class SavingCardChangeAuditPassBLL : BaseBLL
    {
        /// <summary>
        /// 根据条件更新数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public virtual void UpdateData(SavingCardChangeUpdateFilter baseFilter
            , ResponseEntity responseEntity)
        {
            var lstBusiness
                = Singleton<GetBusinessByContractNoDAL<SavingCardChangeBusinessViewData>>.Instance.SearchData(baseFilter);

            if (lstBusiness == null || lstBusiness.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, "审核失败！");
                return;
            }

            var business = lstBusiness[0];
            baseFilter.BusinessID = business.BusinessID;
            baseFilter.CustomerID = business.CustomerID;

            // 更新数据
            int count = Singleton<SavingCardChangeAuditPassDAL>.Instance.Update(baseFilter);

            if (count > 0)
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
            else
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others);
        }
    }
}

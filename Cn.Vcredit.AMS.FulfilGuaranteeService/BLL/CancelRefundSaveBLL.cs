using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.FulfilGuaranteeService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.FulfilGuaranteeService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-10
    /// Description:解约退款保存服务业务逻辑层
    /// </summary>
    public class CancelRefundSaveBLL:BaseBLL
    {
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public void CancelRefundSave(CancelRefundUpdateFilter filter, ResponseEntity responseEntity)
        {
            if (filter == null || string.IsNullOrEmpty(filter.UpdateContents))
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            string[] lines = filter.UpdateContents.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            if (lines == null || lines.Length == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            CancelRefundViewData data = null;
            CancelRefundUpdateFilter updateFilter = null;
            int totalCount = 0;
            foreach(string line in lines)
            {
                data = CancelRefundViewData.ToEntity(line);
                if(data == null)
                    continue;

                updateFilter = new CancelRefundUpdateFilter();
                updateFilter.BusinessId = data.BusinessId;
                updateFilter.RefundAmt = data.RefundAmt;
                updateFilter.PayDate = data.PayDate;
                updateFilter.ReceivedDate = data.ReceivedDate;
                updateFilter.PayType = data.PayType;

                if (Singleton<CancelRefundSaveDAL>.Instance.Update(updateFilter) > 0)
                    totalCount++;
            }

            if (totalCount == lines.Length)
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
            else if (totalCount < lines.Length)
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, "部分更新成功。");
            else
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, "更新失败。");
        }
    }
}

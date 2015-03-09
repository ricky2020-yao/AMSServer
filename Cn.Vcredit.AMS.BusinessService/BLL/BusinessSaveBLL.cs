using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BusinessService.Common;
using Cn.Vcredit.AMS.BusinessService.DAL;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.BLL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-11-25
    /// Desc:订单详情-保存调整款项业务逻辑处理
    /// </summary>
    public class BusinessSaveBLL : BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public void PayAccount(PayAccountFilter filter, ResponseEntity responseEntity)
        {
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            string message = "";
            string guid = Guid.NewGuid().ToString();

            if (!string.IsNullOrEmpty(filter.JsonString))
            {
                if (!BusinessHelper.JsonToReceivedItem(filter, guid))
                {
                    message = "错误，实收金额不允许大于应收金额！";
                    ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, message);
                    m_Logger.Info(message);
                    return;
                }
            }

            // 更新订单、帐单信息
            filter.BusinessID = filter.BusinessID;
            filter.Guid = guid;

            if (Singleton<BusinessSaveDAL>.Instance.Update(filter) > 0)
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
            else
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others);
        }
    }
}

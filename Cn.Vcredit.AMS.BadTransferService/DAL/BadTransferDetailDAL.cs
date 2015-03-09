using Cn.Vcredit.AMS.BadTransferService.Data.ViewData;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BadTransferService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:获取坏账清贷详细数据访问类
    /// </summary>
    public class BadTransferDetailDAL : BaseSearchDAL<BadTransferSearchViewData>
    {
        /// <summary>
        /// 获取坏账清贷欠费账单信息（欠费账单(当期+逾期)）
        /// </summary>
        /// <param name="cloanApplyID"></param>
        /// <returns></returns>
        public List<BadTransferDetailOweBillData> GetBadTransferOweBill(int cloanApplyID)
        {
            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            inPutParam.Add("@CloanApplyID", cloanApplyID);

            return Query<BadTransferDetailOweBillData>("proc_FinanceManage_GetBadTransferOweBill"
                , inPutParam, GetConnectKey(), System.Data.CommandType.StoredProcedure, 60000);
        }

        /// <summary>
        /// 获取坏账清贷欠费账单信息（其它费用）
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public List<BadTransferDetailOtherData> GetBadTransferOtherFee(int businessId)
        {
            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            inPutParam.Add("@BusinessID", businessId);

            return Query<BadTransferDetailOtherData>("proc_FinanceManage_GetBadTransferOtherFee"
                , inPutParam, GetConnectKey(), System.Data.CommandType.StoredProcedure, 60000);
        }
    }
}

using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cn.Vcredit.Common.TypeConvert;

namespace Cn.Vcredit.AMS.DwjmPayConfirm.DAL
{
    /// <summary>
    /// 解约付款逻辑
    /// </summary>
    public class CancelRefundUpdateDal : BaseUpdateDAL
    {
        /// <summary>
        /// 更新语句
        /// </summary>
        /// <param name="baseFilter">过滤条件</param>
        /// <returns>更新语句</returns>
        protected override string GetUpdateSql(BaseFilter baseFilter)
        {
            CancelRefundUpdateFilter filter = baseFilter as CancelRefundUpdateFilter;
            if (filter == null)
                return null;

            return "SQL\\DwjmPayConfirm\\Update_CancelRefund.sql".ToFileContent(false,
                filter.RefundAmt,filter.PayDate.ToDateTimeString(), filter.ReceivedDate.ToDateTimeString(),
                filter.PayType, filter.BusinessID);
        }
    }
}

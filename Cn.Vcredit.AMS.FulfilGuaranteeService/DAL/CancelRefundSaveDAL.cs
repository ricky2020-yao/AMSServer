using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.FulfilGuaranteeService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-10
    /// Description:解约退款保存服务数据访问层
    /// </summary>
    public class CancelRefundSaveDAL : BaseUpdateDAL
    {
        /// <summary>
        /// 获取指定如何解释命令字符串
        /// </summary>
        /// <returns></returns>
        protected override CommandType GetCommandType()
        {
            return CommandType.Text;
        }

        /// <summary>
        /// 获取检索数据的存储过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetUpdateSql(BaseFilter baseFilter)
        {
            var filter = baseFilter as CancelRefundUpdateFilter;

            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE finance.CancelRefund ");
            sb.Append("    SET PayDate = '" + filter.PayDate + "'");
            sb.Append("      , ReceivedDate = '" + filter.ReceivedDate + "'");
            sb.Append("      , RefundAmt = " + filter.RefundAmt);
            sb.Append("      , PayType = " + filter.PayType);
            sb.Append(" WHERE BusinessID = " + filter.BusinessId );

            return sb.ToString();
        }
    }
}

using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.Common.Tools;
using Cn.Vcredit.AMS.Entity.Filter.CustomerService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.CustomerSearchService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2015年3月5日
    /// Description:更新客户发送短信标志的数据访问层
    /// </summary>
    public class UpdateCustomerSendMessgeDAL:BaseUpdateDAL
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
            CustomerSearchFilter filter = baseFilter as CustomerSearchFilter;

            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE customer.vw_customer_CustomerBasic ");
            sb.Append(" SET IsSendMsg = ~IsSendMsg");
            sb.Append("  WHERE IdenNo = '{0}' ");
            sb.Append("    AND Bid = {1} ");

            _logger.Info(sb.ToString().StringFormat(filter.IdenNO, filter.BusinessId));

            return sb.ToString().StringFormat(filter.IdenNO, filter.BusinessId);
        }
    }
}

using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.ClearLoanReasonService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:清贷原因设置检索数据处理类
    /// </summary>
    public class ClearLoanReasonSearchDAL<T>
        : BaseSearchDAL<T> where T : class, new()
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
        /// 根据过滤条件，返回检索数据的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSql(BaseFilter baseFilter)
        {
            var filter = baseFilter as ClearLoanReasonSearchFilter;
            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT b.BusinessID");
            sb.Append("       ,b.ContractNo");
            sb.Append("       ,v.CustomerName");
            sb.Append("       ,v.IdenNo");
            sb.Append("       ,b.LoanKind");
            sb.Append("       ,b.LoanCapital");
            sb.Append("       ,b.LoanPeriod");
            sb.Append("       ,b.LoanTime");
            sb.Append("       ,b.CLoanStatus");
            sb.Append("       ,b.BusinessStatus");
            sb.Append("       ,b.ToGuaranteeTime");
            sb.Append("       ,b.ToLitigationTime");
            sb.Append("       ,b.ClearLoanType");
            sb.Append("       ,b.ClearLoanRemark");
            sb.Append("       ,b.ClearLoanTime");
            sb.Append("       ,b.SavingCard");
            sb.Append(" FROM dbo.Business b WITH (NOLOCK)");
            sb.Append(" JOIN customer.vw_customer_CustomerBasic v WITH (NOLOCK)");
            sb.Append(" ON b.CustomerID = v.CustomerID");
            sb.Append(" WHERE b.IsRepayment = 0");
            
            if (!string.IsNullOrEmpty(filter.ContractNo))
                sb.Append(" AND b.ContractNo = '" +  filter.ContractNo + "'");
            if (filter.BusinessId != 0)
                sb.Append(" AND b.BusinessID = " + filter.BusinessId);

            return sb.ToString();
        }
    }
}

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
using System.Data;
using Cn.Vcredit.AMS.Entity.Filter;

namespace Cn.Vcredit.AMS.BusinessService.DAL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-11-21
    /// Desc:账单科目查询数据逻辑类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessDetailDAL<T>
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
            var filter = baseFilter as SearchBusinessListFilter;
            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT b.BusinessID");
            sb.Append("       ,b.BusinessStatus");
            sb.Append("       ,b.ClearLoanTime");
            sb.Append("       ,b.CLoanStatus");
            sb.Append("       ,b.ContractNo");
            sb.Append("       ,b.CreateTime");
            sb.Append("       ,b.CurrentOverAmount");
            sb.Append("       ,b.CustomerID");
            sb.Append("       ,b.FrozenNo");
            sb.Append("       ,b.IsRepayment");
            sb.Append("       ,b.LawsuitStatus");
            sb.Append("       ,b.LendingSideKey");
            sb.Append("       ,b.LendingSideID");
            sb.Append("       ,b.ServiceSideKey");
            sb.Append("       ,b.ServiceSideID");
            sb.Append("       ,b.GuaranteeSideKey");
            sb.Append("       ,b.GuaranteeSideID");
            sb.Append("       ,b.LoanCapital");
            sb.Append("       ,b.LoanKind");
            sb.Append("       ,b.LoanPeriod");
            sb.Append("       ,b.LoanTime");
            sb.Append("       ,b.OtherAmount");
            sb.Append("       ,b.OverAmount");
            sb.Append("       ,b.ProductKind");
            sb.Append("       ,b.ProductType");
            sb.Append("       ,b.Region");
            sb.Append("       ,b.SavingCard");
            sb.Append("       ,b.SavingUser");
            sb.Append("       ,b.ToGuaranteeTime");
            sb.Append("       ,b.ToLitigationTime");
            sb.Append("       ,b.CapitalRate");
            sb.Append("       ,b.DepositRate");
            sb.Append("       ,b.BranchKey");
            sb.Append("       ,b.ProceduresRate");
            sb.Append("       ,b.ManagementRate");
            sb.Append("       ,b.InterestRate");
            sb.Append("       ,b.ServiceRate");
            sb.Append("       ,c.CustomerName AS CustomerName");
            sb.Append("       ,c.IdenNo AS IdentityNo");
            sb.Append(" FROM dbo.Business b WITH (NOLOCK)");
            sb.Append(" JOIN customer.vw_customer_Info c ON b.CustomerID = c.CustomerID");
            sb.Append(" WHERE b.BusinessID = " + filter.BusinessID);

            if (!string.IsNullOrEmpty(filter.BranchKey))
                sb.Append(" AND b.ServiceSideKey IN ('" + filter.BranchKey + "')");
            if (!string.IsNullOrEmpty(filter.BranchKeys))
                sb.Append(" AND b.BranchKey IN ('" + filter.BranchKeys + "')");

            return sb.ToString();
        }
    }
}

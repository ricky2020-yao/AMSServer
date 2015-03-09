using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月05日
    /// Description:订单筛选导出检索数据处理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinesssDataSearchDAL<T>
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
        /// 根据过滤条件，返回检索件数的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetCountSql(BaseFilter baseFilter)
        {
            BusinessExportFilter filter = baseFilter as BusinessExportFilter;
            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT COUNT(*) AS TOTALCOUNT");
            sb.Append(" FROM dbo.Business b WITH (NOLOCK)");
            sb.Append(" WHERE 1 = 1 ");

            string condition = CombineCondition(filter);
            if (!string.IsNullOrEmpty(condition))
                sb.Append(condition);

            return sb.ToString();
        }

        /// <summary>
        /// 根据过滤条件，返回检索数据的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSql(BaseFilter baseFilter)
        {
            BusinessExportFilter filter = baseFilter as BusinessExportFilter;
            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * ");
            sb.Append("  FROM ( ");
            sb.Append(" SELECT b.BusinessID");
            sb.Append("         ,ROW_NUMBER() OVER(ORDER BY b.BusinessID) AS RowId");
            sb.Append("         ,b.ContractNo");
            sb.Append("         ,v.CustomerName AS CustomerName");
            sb.Append("         ,v.IdenNo AS IdentityCard");
            sb.Append("         ,b.OverAmount AS OverdueAmt");
            sb.Append("         ,b.CurrentOverAmount AS CurrentDueAmt");
            sb.Append("         ,b.BusinessStatus");
            sb.Append("         ,b.CLoanStatus");
            sb.Append("         ,b.ProductKind");
            sb.Append("         ,b.LendingSideKey");
            sb.Append("         ,b.ServiceSideKey");
            sb.Append("         ,b.GuaranteeSideKey");
            sb.Append("         ,b.LoanKind");
            sb.Append("         ,b.OverMonth");
            sb.Append("         ,b.SalesTeam");
            sb.Append("         ,b.SalesManID");
            sb.Append("         ,b.LoanTime");
            sb.Append("         ,b.ClearLoanTime");
            //sb.Append("         ,DATEADD(month, b.LoanPeriod, b.LoanTime) AS ZClearLoanTime");
            sb.Append("         ,b.ToLitigationTime");
            sb.Append("         ,b.ToGuaranteeTime");
            sb.Append("         ,b.SavingCard");
            sb.Append("         ,b.BankKey");
            sb.Append("         ,b.LoanPeriod");
            sb.Append("         ,b.ResidualCapital");
            sb.Append("         ,b.BranchKey");
            sb.Append("         ,b.LoanCapital");
            sb.Append("         ,b.ServiceRate");
            sb.Append("         ,b.ProceduresRate");
            sb.Append("         ,b.ManagementRate");
            sb.Append(" FROM dbo.Business b WITH (NOLOCK)");
            sb.Append(" JOIN customer.vw_customer_CustomerBasic v WITH (NOLOCK)");
            sb.Append("   ON v.Bid = b.BusinessID");
            sb.Append(" WHERE 1 = 1 ");

            string condition = CombineCondition(filter);
            if (!string.IsNullOrEmpty(condition))
                sb.Append(condition);

            sb.Append(" ) a");
            sb.AppendFormat(" WHERE a.RowId > {0} AND a.RowId <= {1}", filter.FromIndex, filter.ToIndex);

            return sb.ToString();
        }

        /// <summary>
        /// 生成检索条件
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string CombineCondition(BusinessExportFilter filter)
        {
            StringBuilder sb = new StringBuilder();

            if (filter.BusinessID > 0)
                sb.Append(" AND b.BusinessID = " + filter.BusinessID);
            if (!string.IsNullOrEmpty(filter.ProductType))
                sb.Append(" AND b.ProductKind = '" + filter.ProductType + "'");
            if (!string.IsNullOrEmpty(filter.ContractNo))
                sb.Append(" AND b.ContractNo = '" + filter.ContractNo + "'");
            if (filter.BusinessStatus > 0)
                sb.Append(" AND b.BusinessStatus = " + filter.BusinessStatus);
            if (filter.CLoanStatus > 0)
                sb.Append(" AND b.CLoanStatus = " + filter.CLoanStatus);
            if (!string.IsNullOrEmpty(filter.LoanKind))
                sb.Append(" AND b.LoanKind = '" + filter.LoanKind + "'");
            if (filter.OverMonth > 0)
                sb.Append(" AND b.OverMonth = " + filter.OverMonth);
            if (!string.IsNullOrEmpty(filter.LendingSideKey))
                sb.Append(" AND b.LendingSideKey = '" + filter.LendingSideKey + "'");
            if (!string.IsNullOrEmpty(filter.ServiceSideKey))
                sb.Append(" AND b.ServiceSideKey = '" + filter.ServiceSideKey + "'");
            if (!string.IsNullOrEmpty(filter.GuaranteeSideKey))
                sb.Append(" AND b.GuaranteeSideKey = '" + filter.GuaranteeSideKey + "'");
            if (!string.IsNullOrEmpty(filter.BranchKey))
                sb.Append(" AND b.BranchKey = '" + filter.BranchKey + "'");
            if (!string.IsNullOrEmpty(filter.SalesTeam))
                sb.Append(" AND b.SalesTeam = '" + filter.SalesTeam + "'");
            if (filter.SalesManId > 0)
                sb.Append(" AND b.SalesManID = " + filter.SalesManId);
            if (filter.LoanDateBegin.HasValue)
                sb.Append(" AND b.LoanTime >= " + filter.LoanDateBegin);
            if (filter.LoanDateEnd.HasValue)
                sb.Append(" AND b.LoanTime <= " + filter.LoanDateEnd);
            if (filter.CLoanDateBegin.HasValue)
                sb.Append(" AND b.ClearLoanTime >= " + filter.CLoanDateBegin);
            if (filter.CLoanDateEnd.HasValue)
                sb.Append(" AND b.ClearLoanTime <= " + filter.CLoanDateEnd);
            if (filter.LawsuitDateBegin.HasValue)
                sb.Append(" AND b.ToLitigationTime >= " + filter.LawsuitDateBegin);
            if (filter.LawsuitDateEnd.HasValue)
                sb.Append(" AND b.ToLitigationTime <= " + filter.LawsuitDateEnd);
            if (filter.GuarteeDateBegin.HasValue)
                sb.Append(" AND b.ToGuaranteeTime >= " + filter.GuarteeDateBegin);
            if (filter.GuarteeDateEnd.HasValue)
                sb.Append(" AND b.ToGuaranteeTime <= " + filter.GuarteeDateEnd);
            if (filter.ZLoanDateBegin.HasValue)
                sb.Append(" AND DATEADD(month, b.LoanPeriod, b.LoanTime) >= " + filter.ZLoanDateBegin);
            if (filter.ZLoanDateEnd.HasValue)
                sb.Append(" AND DATEADD(month, b.LoanPeriod, b.LoanTime) <= " + filter.ZLoanDateEnd);

            return sb.ToString();
        }
    }
}

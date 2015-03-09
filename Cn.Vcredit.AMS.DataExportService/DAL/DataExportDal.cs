using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月5日
    /// Description:数据导出查询操作类
    /// </summary>
    public class DataExportDal:BaseDao
    {
        /// <summary>
        /// 根据画面检索条件，获取订单筛选件数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int GetViewDataBusinessCount(BusinessExportFilter filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT COUNT(*) AS TOTALCOUNT");
            sb.Append(" FROM [fin].[BusinessBasic] bb");
            sb.Append("  JOIN [fin].[BusinessExtend] be");
            sb.Append("    ON bb.BusinessID = be.BusinessID");
            sb.Append("  JOIN [fin].[BusinessCurrentStaus] bcs");
            sb.Append("    ON bb.BusinessID = bcs.BusinessID");
            sb.Append("  JOIN [fin].[BusinessLawsuit] bl");
            sb.Append("    ON bb.BusinessID = bl.BusinessID");
            sb.Append("  JOIN [fin].[BusinessGuarantee] bg");
            sb.Append("    ON bb.BusinessID = bg.BusinessID");
            sb.Append(" WHERE 1 = 1 ");

            string condition = CombineCondition(filter);
            if (!string.IsNullOrEmpty(condition))
                sb.Append(condition);

            return (int)QueryScalar(sb.ToString()
                , null, "PostLoanDB", System.Data.CommandType.Text);
        }

        /// <summary>
        /// 根据画面检索条件，获取订单筛选结果
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="fromIndex"></param>
        /// <param name="toIndex"></param>
        /// <returns></returns>
        public List<BusinessExportViewData> GetViewDataBusiness(BusinessExportFilter filter,
            int fromIndex, int toIndex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT * ");
            sb.Append("  FROM ( ");
            sb.Append(" SELECT bb.BusinessID");
            sb.Append("         ,bb.ContractNo");
            sb.Append("         ,bb.CustomerName");
            sb.Append("         ,bb.IdentityCard");
            sb.Append("         ,bb.OverAmount AS OverdueAmt");
            sb.Append("         ,bb.CurrentOverAmount AS CurrentDueAmt");
            sb.Append("         ,bcs.BusinessStatus");
            sb.Append("         ,bb.IsFreeze");
            sb.Append("         ,bcs.CLoanStatus");
            sb.Append("         ,be.BusinessLogicID AS ProductType");
            sb.Append("         ,bb.LendingSide AS LendingSideKey");
            sb.Append("         ,bb.ServiceSide AS ServiceSideKey");
            sb.Append("         ,bb.GuaranteeSide AS GuaranteeSideKey");
            sb.Append("         ,bb.LoanKind");
            sb.Append("         ,bcs.OverMonth");
            sb.Append("         ,'' AS SalesTeam");
            sb.Append("         ,0 AS SalesManID");
            sb.Append("         ,bb.LoanTime");
            sb.Append("         ,bcs.ClearLoanTime");
            sb.Append("         ,DATEADD(month, bb.LoanPeriod, bb.LoanTime) AS ZClearLoanTime");
            sb.Append("         ,bl.ToLitigationTime");
            sb.Append("         ,bg.ToGuaranteeTime");
            sb.Append("         ,cc.SavingCard");
            sb.Append("         ,cc.BankID");
            sb.Append("         ,bb.LoanPeriod");
            sb.Append("         ,bcs.ResidualCapital");
            sb.Append("         ,bb.BranchID");
            sb.Append("         ,bb.LoanCapital");
            sb.Append("         ,birs.InterestRateValue AS ServiceRate");
            sb.Append("         ,birp.InterestRateValue AS ProceduresRate");
            sb.Append(" FROM [fin].[BusinessBasic] bb");
            sb.Append("  JOIN [fin].[BusinessExtend] be");
            sb.Append("    ON bb.BusinessID = be.BusinessID");
            sb.Append("  JOIN [fin].[BusinessCurrentStaus] bcs");
            sb.Append("    ON bb.BusinessID = bcs.BusinessID");
            sb.Append("  JOIN [fin].[BusinessLawsuit] bl");
            sb.Append("    ON bb.BusinessID = bl.BusinessID");
            sb.Append("  JOIN [fin].[BusinessGuarantee] bg");
            sb.Append("    ON bb.BusinessID = bg.BusinessID");
            sb.Append("  JOIN [fin].[RelationBusinessCustomerCard] rbc");
            sb.Append("    ON bb.BusinessID = rbc.BusinessID");
            sb.Append("  JOIN [fin].[CustomerCard] cc");
            sb.Append("    ON rbc.CustomerCardID = cc.CardID");
            sb.Append("  JOIN [fin].[BusinessInterestRateFee] birs");
            sb.Append("    ON bb.BusinessID = birs.BusinessID");
            sb.Append("  JOIN [fin].[BusinessInterestRateFee] birp");
            sb.Append("    ON bb.BusinessID = birp.BusinessID");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append("  AND birs.InterestSubject = 3");
            sb.Append("  AND birp.InterestSubject = 10");

            string condition = CombineCondition(filter);
            if (!string.IsNullOrEmpty(condition))
                sb.Append(condition);
            sb.Append(" ) a");
            sb.AppendFormat(" WHERE a.RowId > {0} AND a.RowId <= {1}", fromIndex, toIndex);

            return Query<BusinessExportViewData>(sb.ToString()
                , null, "PostLoanDB", System.Data.CommandType.Text);
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
                sb.Append(" AND bb.Bid = " + filter.BusinessID);
            if (filter.ProductType > 0)
                sb.Append(" AND be.BusinessLogicID = " + filter.ProductType);
            if (!string.IsNullOrEmpty(filter.ContractNo))
                sb.Append(" AND bb.ContractNo = '" + filter.ContractNo + "'");
            if (filter.BusinessStatus > 0)
                sb.Append(" AND bcs.BusinessStatus = " + filter.BusinessStatus);
            if (filter.CLoanStatus > 0)
                sb.Append(" AND bcs.CLoanStatus = " + filter.CLoanStatus);
            if (filter.LoanKind > 0)
                sb.Append(" AND bb.LoanKind = " + filter.LoanKind);
            if (filter.OverMonth > 0)
                sb.Append(" AND bb.OverMonth = " + filter.OverMonth);
            if (filter.LendingSide > 0)
                sb.Append(" AND bb.LendingSide = " + filter.LendingSide);
            if (filter.ServiceSide > 0)
                sb.Append(" AND bb.ServiceSide = " + filter.ServiceSide);
            if (filter.GuaranteeSide > 0)
                sb.Append(" AND bb.GuaranteeSide = " + filter.GuaranteeSide);
            if (filter.BranchId > 0)
                sb.Append(" AND bb.BranchID = " + filter.BranchId);
            if (filter.LoanDateBegin.HasValue)
                sb.Append(" AND bb.LoanTime >= '" + filter.LoanDateBegin.ToString() + "'");
            if (filter.LoanDateEnd.HasValue)
                sb.Append(" AND bb.LoanTime <= '" + filter.LoanDateEnd.ToString() + "'");
            if (filter.CLoanDateBegin.HasValue)
                sb.Append(" AND bcs.ClearLoanTime >= '" + filter.CLoanDateBegin.ToString() + "'");
            if (filter.CLoanDateEnd.HasValue)
                sb.Append(" AND bcs.ClearLoanTime <= '" + filter.CLoanDateEnd.ToString() + "'");
            if (filter.LawsuitDateBegin.HasValue)
                sb.Append(" AND bl.ToLitigationTime >= '" + filter.LawsuitDateBegin.ToString() + "'");
            if (filter.LawsuitDateEnd.HasValue)
                sb.Append(" AND bl.ToLitigationTime <= '" + filter.LawsuitDateEnd.ToString() + "'");
            if (filter.GuarteeDateBegin.HasValue)
                sb.Append(" AND bg.ToGuaranteeTime >= '" + filter.GuarteeDateBegin.ToString() + "'");
            if (filter.GuarteeDateEnd.HasValue)
                sb.Append(" AND bg.ToGuaranteeTime <= '" + filter.GuarteeDateEnd.ToString() + "'");
            if (filter.ZLoanDateBegin.HasValue)
                sb.Append(" AND DATEADD(month, bb.LoanPeriod, bb.LoanTime) >= '" + filter.ZLoanDateBegin.ToString() + "'");
            if (filter.ZLoanDateEnd.HasValue)
                sb.Append(" AND DATEADD(month, bb.LoanPeriod, bb.LoanTime) <= '" + filter.ZLoanDateEnd.ToString() + "'");

            return sb.ToString();
        }
    }
}

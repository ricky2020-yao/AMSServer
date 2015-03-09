using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.InterestFitService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年8月13日
    /// Description:代偿款支付设置数据处理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InterestFitDAL<T>
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
            var filter = baseFilter as InterestFitFilter;
            if (filter == null)
                return "";

            string sql = @" 
                    SELECT a.* 
                          ,c.CustomerName AS CustomerName
                          ,c.IdenNo AS IdentityNo
                     FROM (
                    SELECT row_number() OVER (ORDER BY b.BusinessID ASC) RowIndex
                          ,b.BusinessID
                          ,b.CustomerID
                          ,b.SalesManID
                          ,b.ProductType
                          ,b.LoanCapital
                          ,b.ResidualCapital
                          ,b.LoanPeriod
                          ,b.InterestRate
                          ,b.ServiceRate
                          ,b.PenaltyRate
                          ,b.AdvanceFee
                          ,b.EarnestAmt
                          ,b.Premium
                          ,b.BusinessStatus
                          ,b.CLoanStatus
                          ,b.LawsuitStatus
                          ,b.ContractNo
                          ,b.LendingSideKey
                          ,b.LendingSideID
                          ,b.ServiceSideKey
                          ,b.ServiceSideID
                          ,b.GuaranteeSideKey
                          ,b.GuaranteeSideID
                          ,b.ConstructionBankNo
                          ,b.BranchKey
                          ,b.LoanKind
                          ,b.PaymentDate
                          ,b.ToGuaranteeTime
                          ,b.ToLitigationTime
                          ,b.ClearLoanTime
                          ,b.IsRepayment
                          ,b.ProceduresAmout
                          ,b.GuaranteeNum
                          ,b.DepositRate
                          ,b.ProceduresRate
                          ,b.ProductKind
                          ,b.PrincipalPunish
                          ,b.ServicePunish
                          ,b.ToGuaranteeAmt
                          ,b.CapitalRate
                          ,b.ManagementRate
                        FROM dbo.Business b WITH (NOLOCK) ";

            string conditionSql = GetConditionSql(filter);
            if (!string.IsNullOrEmpty(conditionSql))
                sql += conditionSql;

            sql += " ) a ";
            sql += " JOIN customer.vw_customer_Info c ON a.CustomerID = c.CustomerID";
            sql += " WHERE a.RowIndex > " + filter.FromIndex;
            sql += "   AND a.RowIndex <= " + filter.ToIndex;
            sql += " ORDER BY a.RowIndex";

            return sql;
        }
        
        /// <summary>
        /// 获取检索件数的Sql
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetCountSql(BaseFilter baseFilter)
        {
            var filter = baseFilter as InterestFitFilter;
            if (filter == null)
                return "";

            string sql = @" SELECT COUNT (1)
                              FROM dbo.Business b WITH (NOLOCK) ";

            string conditionSql = GetConditionSql(filter);
            if (!string.IsNullOrEmpty(conditionSql))
                sql += conditionSql;

            return sql;
        }

        /// <summary>
        /// 获取检索条件
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string GetConditionSql(InterestFitFilter filter)
        {
            string sql = "";

            sql += " WHERE  b.BusinessStatus <> 1";

            // 信托方
            if (!string.IsNullOrEmpty(filter.LendingSideKey))
                sql += " AND b.LendingSideKey = '" + filter.LendingSideKey + "'";

            // 批次号
            if (!string.IsNullOrEmpty(filter.BatchNo))
                sql += " AND b.GuaranteeNum = '" + filter.BatchNo + "'";

            // 合同号
            if (!string.IsNullOrEmpty(filter.ContractNO))
                sql += " AND b.ContractNo = '" + filter.ContractNO + "'";

            //转担保日期
            DateTime startime;
            DateTime endtime;
            if (!string.IsNullOrEmpty(filter.GuaranteeFromTime))
                if (DateTime.TryParse(filter.GuaranteeFromTime, out startime))
                    sql += " AND b.ToGuaranteeTime >= " + startime;
            if (!string.IsNullOrEmpty(filter.GuaranteeToTime))
                if (DateTime.TryParse(filter.GuaranteeToTime, out endtime))
                    sql += " AND b.ToGuaranteeTime <= " + endtime;

            // 状 态
            if ("已支付" == filter.PaymentStatus)
                sql += " AND b.PaymentDate IS NOT NULL";
            else if ("未支付" == filter.PaymentStatus)
                sql += " AND b.PaymentDate IS NULL";

            if (!string.IsNullOrEmpty(filter.BranchKey))
            {
                sql += @" AND b.BusinessID IN (
                          SELECT DISTINCT BusinessID 
                            FROM dbo.Bill WITH (NOLOCK) 
                            WHERE CompanyKey IN ('" + filter.BranchKey + "'))";
            }
            // 贷款方
            if (!string.IsNullOrEmpty(filter.LoanServiceKey))
            {
                sql += @" AND b.LendingSideID IN ( 
                            SELECT ba.BankAccountID FROM dbo.BankAccount ba 
                             WHERE ba.CompanyKey = '" + filter.LoanServiceKey + "')";
            }

            return sql;
        }
    }
}

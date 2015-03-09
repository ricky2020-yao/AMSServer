using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.TypeConvert;
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
    /// Description:合同解约退款明细数据访问层
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CancelRefundExportDAL<T>
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
            CancelRefundExportFilter filter = baseFilter as CancelRefundExportFilter;

            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(@" SELECT b.BusinessID ,
                                b.ContractNo ,
                                pe.PersonName AS CustomerName ,
                                b.LoanCapital - ISNULL(b.ProceduresAmout, 0) AS RealLoanCapital ,
                                ISNULL(c.RefundAmt, b.LoanCapital - ISNULL(b.ProceduresAmout, 0)) AS RefundAmt ,
                                c.PayDate ,
                                c.ReceivedDate ,
                                c.PayType ,
                                c.CancelTime
                          FROM  finance.CancelRefund c
                    INNER JOIN dbo.Business b ON c.BusinessID = b.BusinessID
                    INNER JOIN customer.CustomerInfo r ON b.CustomerID = r.CustId
                    INNER JOIN customer.Person pe ON r.PersonId = pe.PersonId ");

            string condition = CombineCondition(filter);
            if (!string.IsNullOrEmpty(condition))
                sb.Append(condition);

            return sb.ToString();
        }

        /// <summary>
        /// 生成检索条件
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string CombineCondition(CancelRefundExportFilter filter)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(filter.Region))
            {
                sb.AppendFormat(@" LEFT JOIN dbo.ConstSysEnum s ON s.super=2619 AND 
                                ((LEN(b.ContractNo)=7 AND s.VALUE='02') OR
                                 (LEN(b.ContractNo)=15 AND SUBSTRING(b.ContractNo,3,2)=s.VALUE) OR 
                                 (LEN(b.ContractNo)=18 AND SUBSTRING(b.ContractNo,6,2)=s.VALUE)
                                )");
            }

            if (filter.HasPayDate)
                sb.AppendFormat(" WHERE c.PayDate IS NOT NULL ");
            else
                sb.AppendFormat(" WHERE c.PayDate IS NULL ");

            if (!filter.LendingSideKey.IsNullString())
                sb.AppendFormat(" AND b.LendingSideKey ='{0}'", filter.LendingSideKey);

            if (filter.BusinessID.HasValue)
                sb.AppendFormat(" AND c.BusinessID ={0}", filter.BusinessID.Value);

            if (!string.IsNullOrEmpty(filter.ContractNo))
                sb.AppendFormat(" AND b.ContractNo ='{0}'", filter.ContractNo);

            if (!string.IsNullOrEmpty(filter.CustomerName))
                sb.AppendFormat(" AND pe.PersonName ='{0}'", filter.CustomerName);

            if (filter.CancelBeginTime.HasValue)
                sb.AppendFormat(" AND c.CancelTime >='{0}'", filter.CancelBeginTime.Value.ToDateTimeString());

            if (!string.IsNullOrEmpty(filter.Region))
                sb.AppendFormat(" AND s.fullkey in('{0}')", filter.Region);

            if (filter.CancelEndTime.HasValue)
                sb.AppendFormat(" AND c.CancelTime <'{0}'", filter.CancelEndTime.Value.ToDateTimeString());

            if (filter.PayBeginTime.HasValue)
                sb.AppendFormat(" AND c.PayDate >='{0}'", filter.PayBeginTime.Value.ToDateTimeString());

            if (filter.PayEndTime.HasValue)
                sb.AppendFormat(" AND c.PayDate <'{0}'", filter.PayEndTime.Value.ToDateTimeString());

            return sb.ToString();
        }
    }
}

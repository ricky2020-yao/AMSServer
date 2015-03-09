using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.CustomerService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.CustomerSearchService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月29日
    /// Description:客户查询数据访问层
    /// </summary>
    public class GetCustomerResultDAL<T> :
        BaseSearchDAL<T> where T : class, new()
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
        /// 获取件数SQL
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetCountSql(BaseFilter baseFilter)
        {
            string sqlStr = @"SELECT COUNT(1) 
                              FROM customer.vw_customer_CustomerBasic v 
                             INNER JOIN dbo.Business b ON v.Bid=b.BusinessId
                             WHERE 1=1 {0}";
            return string.Format(sqlStr, GetCondition(baseFilter));
        }
        /// <summary>
        /// 获取检索数据的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSql(BaseFilter baseFilter)
        {
            CustomerSearchFilter filter = baseFilter as CustomerSearchFilter;
            if (filter == null)
                return "";

            string sqlStr = @"SELECT * FROM 
                                (SELECT b.BusinessID AS BusinessID
                                        , v.CustomerID
                                        , b.ContractNo
                                        , v.CustomerName
                                        , v.IdenNo
                                        , b.SavingCard
                                        , v.mobile AS Mobile
                                        , v.HouseholdType
                                        , ISNULL(e.NAME,'') AS IsLoanSecond
                                        , v.IsSendMsg
                                        , ROW_NUMBER() OVER(ORDER BY v.CustomerID ASC) AS num
                                FROM customer.vw_customer_CustomerBasic v 
                                INNER JOIN dbo.Business b ON v.Bid = b.BusinessId
                                LEFT JOIN dbo.ConstSysEnum e ON e.Fullkey = b.SecondSales
                                WHERE 1=1 {0} )t {1}";

            string pageCondition = string.Format("where t.num between {0} and {1}", filter.FromIndex, filter.ToIndex);
            sqlStr = string.Format(sqlStr, GetCondition(baseFilter), pageCondition);

            return sqlStr;
        }

        /// <summary>
        /// 获取检索条件
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        private string GetCondition(BaseFilter baseFilter)
        {
            CustomerSearchFilter filter = baseFilter as CustomerSearchFilter;
            if (filter == null)
                return "";

            StringBuilder conditionSbl = new StringBuilder();
            if (filter.BusinessId > 0)
                conditionSbl.AppendFormat(" AND b.BusinessID ={0} ", filter.BusinessId);
            if (!string.IsNullOrEmpty(filter.ContractNO))
                conditionSbl.AppendFormat(" AND b.ContractNo ='{0}' ", filter.ContractNO);
            if (!string.IsNullOrEmpty(filter.Mobile))
                conditionSbl.AppendFormat(" AND v.Mobile='{0}'", filter.Mobile);
            if (!string.IsNullOrEmpty(filter.CustomerName))
                conditionSbl.AppendFormat(" AND v.CustomerName='{0}'", filter.CustomerName);
            if (!string.IsNullOrEmpty(filter.IdenNO))
                conditionSbl.AppendFormat(" AND v.IdenNo='{0}'", filter.IdenNO);
            if (!string.IsNullOrEmpty(filter.DropSources))
                conditionSbl.AppendFormat(" AND b.fromSource='{0}'", filter.DropSources);
            if (!string.IsNullOrEmpty(filter.CompanyKey))
                conditionSbl.AppendFormat(" AND b.ServiceSideKey='{0}'", filter.CompanyKey);
            if (!string.IsNullOrEmpty(filter.LendingKey))
                conditionSbl.AppendFormat(" AND b.LendingSideKey='{0}'", filter.LendingKey);

            return conditionSbl.ToString();
        }
    }
}

using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.RelativeDaySearchDunService.DAL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-12-15
    /// Desc:催收单导出数据逻辑类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelativeDayDunExportDAL<T>
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
            var filter = baseFilter as RelativeDayExportDunFilter;
            if (filter == null)
                return "";

            string sql = @" SELECT b.LoanTime
                                    ,b.SavingCard
                                    ,b.BusinessID
                                    ,v.CustomerName AS CustomerName
                            FROM [dbo].[Business] b WITH (NOLOCK)
                            JOIN customer.vw_customer_CustomerBasic v WITH (NOLOCK)
                            ON b.CustomerID = v.CustomerID
                            WHERE b.ContractNo = '";

            sql += filter.ContractNo + "'";

            return sql;
        }
    }
}

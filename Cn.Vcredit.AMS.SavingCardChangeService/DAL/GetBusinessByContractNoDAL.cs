using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.SavingCardChangeService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月29日
    /// Description:根据合同号查询业务信息数据访问层
    /// </summary>
    public class GetBusinessByContractNoDAL<T>:
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
        /// 获取检索数据的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSql(BaseFilter baseFilter)
        {
            SavingCardChangeUpdateFilter filter = baseFilter as SavingCardChangeUpdateFilter;
            if (filter == null)
                return "";

            string sql
                = " SELECT TOP 1 b.BusinessID, b.CustomerID "
                + "   FROM Business b "
                + "  WHERE b.ContractNo = '" + filter.ContractNo + "'"
                + " ORDER BY BusinessID ASC";

            return sql;
        }
    }
}

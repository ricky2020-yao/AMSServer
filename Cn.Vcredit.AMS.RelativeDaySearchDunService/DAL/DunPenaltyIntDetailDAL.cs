using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.RelativeDaySearchDunService.DAL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-12-18
    /// Desc:罚息表数据逻辑类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DunPenaltyIntDetailDAL<T>
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
        /// 获取计算件数的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetCountSql(BaseFilter baseFilter)
        {
            var filter = baseFilter as SearchBusinessListFilter;
            if (filter == null)
                return "";

            string sql = @"SELECT COUNT(1)
                      FROM [dbo].[PenaltyInt]";
            sql += " WHERE BusinessID = " + filter.BusinessID;

            return sql;
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

            string sql = @"SELECT *
                        FROM ( 
                        SELECT ROW_NUMBER() OVER(ORDER BY PenaltyIntID) AS RowId
                          ,[PenaltyIntID]
                          ,[BusinessID]
                          ,[ReasonID]
                          ,[ToBillID]
                          ,[Amount]
                          ,[IsShelve]
                          ,[CreateTime]
                          ,[ReasonItemID]
                      FROM [dbo].[PenaltyInt]";
            sql += " WHERE BusinessID = " + filter.BusinessID;
            sql += " ) t WHERE t.RowId > " + filter.FromIndex + " AND t.RowId <= " + filter.ToIndex;

            return sql;
        }
    }
}

using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.DAL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-12-18
    /// Desc:代偿卡表数据逻辑类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AdaptationCardDAL<T>
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

            string sql = @"SELECT [AdaptationCardID] 
                          ,[CardNo]
                          ,[CardUser]
                          ,[AdaBankName]
                          ,[ValidPath]
                          ,[ValidEndTime]
                          ,[AdaDesc]
                          ,[ValidName]
                          ,[BusinessID]
                          ,[AdaName]
                      FROM [dbo].[AdaptationCard]";
            sql += " WHERE BusinessID = " + filter.BusinessID;

            return sql;
        }
    }
}

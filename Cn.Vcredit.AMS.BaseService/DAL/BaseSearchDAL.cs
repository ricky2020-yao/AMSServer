using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月18日
    /// Description:检索数据数据访问基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class BaseSearchDAL<T> : BaseDao where T : class, new()
    {
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        protected virtual string GetConnectKey()
        {
            return "PostLoanDB";
        }

        /// <summary>
        /// 获取指定如何解释命令字符串
        /// </summary>
        /// <returns></returns>
        protected virtual CommandType GetCommandType()
        {
            return CommandType.Text;
        }

        /// <summary>
        /// 根据过滤条件，返回检索件数的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected virtual string GetCountSql(BaseFilter baseFilter)
        {
            return "";
        }

        /// <summary>
        /// 根据过滤条件，返回检索数据的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected virtual string GetSearchSql(BaseFilter baseFilter)
        {
            return "";
        }

        /// <summary>
        /// 获取检索数据的存储过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected virtual string GetSearchSpName(BaseFilter baseFilter)
        {
            return "";
        }

        /// <summary>
        /// 获取检索数据的存储过程参数列表（入参）
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected virtual IDictionary<string, object> GetSearchSpInParams(BaseFilter baseFilter)
        {
            return null;
        }

        /// <summary>
        /// 获取检索数据的存储过程参数列表(出参)
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected virtual IDictionary<string, object> GetSearchSpOutParams(BaseFilter baseFilter)
        {
            return null;
        }

        /// <summary>
        /// 获取检索数据的件数
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        public virtual int GetCount(BaseFilter baseFilter)
        {
            string sql = GetCountSql(baseFilter);
            if (string.IsNullOrEmpty(sql))
                return 0;

            // 获取件数
            return (int)QueryScalar(sql
                , null, GetConnectKey(), System.Data.CommandType.Text);
        }

        /// <summary>
        /// 获取检索的结果
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        public virtual List<T> SearchData(BaseFilter baseFilter)
        {
            string sql = "";
            IDictionary<string, object> indicParams = null;
            IDictionary<string, object> outdicParams = null;
            CommandType type = GetCommandType();

            // 存储过程
            if (type == CommandType.StoredProcedure)
            {
                sql = GetSearchSpName(baseFilter);
                indicParams = GetSearchSpInParams(baseFilter);
                outdicParams = GetSearchSpOutParams(baseFilter);
            }
            else
            {
                sql = GetSearchSql(baseFilter);
            }

            if (string.IsNullOrEmpty(sql))
                return null;
            if (outdicParams != null)
            {
                var result = Query<T>(sql, indicParams, ref outdicParams, GetConnectKey(), type, 600000);
                baseFilter.outParams = outdicParams;
                return result;
            }
            else
            {
                return Query<T>(sql, indicParams, GetConnectKey(), type, 600000);
            }
        }

        /// <summary>
        /// 获取检索的结果
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        public virtual DataTable SearchDataToDataTable(BaseFilter baseFilter)
        {
            string sql = "";
            IDictionary<string, object> indicParams = null;
            CommandType type = GetCommandType();

            // 存储过程
            if (type == CommandType.StoredProcedure)
            {
                sql = GetSearchSpName(baseFilter);
                indicParams = GetSearchSpInParams(baseFilter);
            }
            else
            {
                sql = GetSearchSql(baseFilter);
            }

            if (string.IsNullOrEmpty(sql))
                return null;

            DataSet ds = QuerySet(sql, indicParams, GetConnectKey(), type);
            if (ds == null || ds.Tables.Count == 0)
                return null;

            return ds.Tables[0];
        }

        /// <summary>
        /// 获取检索的结果
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        public virtual DataSet SearchDataToSet(BaseFilter baseFilter)
        {
            string sql = "";
            IDictionary<string, object> indicParams = null;
            CommandType type = GetCommandType();

            // 存储过程
            if (type == CommandType.StoredProcedure)
            {
                sql = GetSearchSpName(baseFilter);
                indicParams = GetSearchSpInParams(baseFilter);
            }
            else
            {
                sql = GetSearchSql(baseFilter);
            }

            if (string.IsNullOrEmpty(sql))
                return null;

            DataSet ds = QuerySet(sql, indicParams, GetConnectKey(), type);
            if (ds == null || ds.Tables.Count == 0)
                return null;

            return ds;
        }
    }
}

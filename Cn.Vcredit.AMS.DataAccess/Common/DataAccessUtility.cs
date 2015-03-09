using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.Data;
using System.Data;
using Cn.Vcredit.Common.DataTableExtensions;

namespace Cn.Vcredit.AMS.DataAccess.Common
{
    public static class DataAccessUtility
    {
       
        /// <summary>
        /// 通过过滤条件得到查询条件
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns>查询条件</returns>
        public static IMongoQuery GetMongoQueryFromFilter(BaseFilter filter)
        {
             PropertyInfo[] propertyInfoArray = filter.GetType().GetProperties();
             List<IMongoQuery> queryList = new List<IMongoQuery>();
             foreach(PropertyInfo propertyInfo in propertyInfoArray)
             {
                 object val = propertyInfo.GetValue(filter, null);
                 if (val == null)
                     continue;

                 if (val is String && string.IsNullOrEmpty(val.ToString()))
                     continue;

                 if (val is Int32 && Convert.ToInt32(val) == 0)
                     continue;

                 if (val is Int16 && Convert.ToInt16(val) == 0)
                     continue;

                 if(val is Int64 && Convert.ToInt64(val) == 0)
                     continue;

                 if (val is byte && Convert.ToByte(val) == 0)
                     continue;

                 if(val is decimal && Convert.ToDecimal(val)==0)
                     continue;

                 if (val is DateTime && Convert.ToDateTime(val) == DateTime.MinValue)
                     continue;

                 if (DataAccessConsts.BaseFilterProperty.Contains(propertyInfo.Name))
                     continue;

                 BsonValue bsonValue;
                 if(val is decimal)
                     bsonValue = BsonDouble.Create(Convert.ToDouble(val));
                 else
                     bsonValue = BsonValue.Create(val);
                 
                 queryList.Add(Query.EQ(propertyInfo.Name, bsonValue));
             }

             if (queryList.Count == 0)
                 return null;
             else
                 return Query.And(queryList);
        }

        /// <summary>
        /// 转成list，增加null判断
        /// </summary>
        /// <typeparam name="M">类型</typeparam>
        /// <param name="cur">MongoCursor</param>
        /// <returns>List</returns>
        public static List<M> ToListEntity<M>(this MongoCursor<M> cur)
        {
            if (cur == null)
                return null;
            else
                return cur.ToList();
        }

        /// <summary>
        /// 查询出分页数据
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="filter">过滤条件，得到结果后会设置RecordCount值</param>
        /// <param name="sqlStr">查询语句，要求包含2个sql，第一个为分页数据，第二个为总数</param>
        /// <returns>分页数据</returns>
        public static List<T> GetSearchDataByPageNo<T>(BaseFilter filter, string sqlStr) where T : new()
        {
            BaseDao baseDao = new BaseDao();
            DataSet dataSet = baseDao.QuerySet(sqlStr, null, "PostLoanDB");
            if (dataSet == null || dataSet.Tables.Count <= 1)
                return null;

            DataTable dtbData = dataSet.Tables[0];
            filter.RecordCount = Convert.ToInt32(dataSet.Tables[1].Rows[0][0]);
            return dtbData.ConvertToList<T>();
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="filter">过滤条件</param>
        /// <param name="sqlStr">查询语句</param>
        /// <returns>所有数据</returns>
        public static List<T> GetSearchData<T>(BaseFilter filter, string sqlStr) where T : new()
        {
            BaseDao baseDao = new BaseDao();
            DataSet dataSet = baseDao.QuerySet(sqlStr, null, "PostLoanDB");
            if (dataSet == null || dataSet.Tables.Count < 1)
                return null;

            DataTable dtbData = dataSet.Tables[0];
            if(dtbData != null)
                filter.RecordCount = dtbData.Rows.Count;

            return dtbData.ConvertToList<T>();
        }
    }
}

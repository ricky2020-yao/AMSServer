using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.Data.DB.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.AMS.DataAccess.Mongo;
using Cn.Vcredit.Data;
using Cn.Vcredit.AMS.DataAccess.Common;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// 同步相关的处理
    /// </summary>
    public class SyncDal : BaseDao
    {
        
        /// <summary>
        /// 得到需要同步的配置信息
        /// </summary>
        /// <returns>需要同步的配置信息</returns>
        public  List<SyncInfo> GetSyncInfoList()
        {
            string sql = "SQL\\Get_SyncInfo.sql".ToFileContent(false,new object[]{});
            return Query<SyncInfo>(sql, null, DataAccessConsts.ConnectDB_PostLoan);
        }

        /// <summary>
        /// 更新已经同步的时间
        /// </summary>
        /// <param name="tableViewName">表名</param>
        /// <param name="dtm">同步时间</param>
        /// <returns>影响行数</returns>
        public int UpdateSyncTime(string tableViewName,DateTime dtm)
        {
            string sql = "SQL\\Update_SyncInfoSyncTime.sql".ToFileContent(false,tableViewName,dtm.ToString("yyyy-MM-dd HH:mm:ss"));
            return Execute(sql, null, DataAccessConsts.ConnectDB_PostLoan);
        }

        /// <summary>
        /// 得到同步数据
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <returns>同步数据</returns>
        public DataTable GetDataTable(string sqlStr,string connectKey = DataAccessConsts.ConnectDB_PostLoan)
        {
            DataSet dataSet = QuerySet(sqlStr, null, connectKey);
            if (dataSet == null || dataSet.Tables.Count == 0)
                return null;
            else
                return dataSet.Tables[0];
        }

        /// <summary>
        /// 得到dataReader
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader GetDataReader(string sqlStr, int timeOut, string connectKey = DataAccessConsts.ConnectDB_PostLoan)
        {
            return QueryDataReader(sqlStr, null, timeOut, connectKey);
        }

        /// <summary>
        /// 同步数据到mongoDB
        /// </summary>
        /// <param name="primaryKeys">主键列名</param>
        /// <param name="tableName">表名</param>
        /// <param name="dtb">数据</param>
        /// <param name="errorInfo">出错信息</param>
        /// <returns>是否成功</returns>
        public bool SyncToMongoDB(string primaryKeys, string tableName, SqlDataReader dataReader, out string errorInfo)
        {
            return Singleton<BaseMongo>.Instance.UpdateOrInsertRowByRow(primaryKeys, tableName, dataReader, out errorInfo);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="collectionName">视图或者表名</param>
        /// <param name="dtb">表数据</param>
        /// <param name="t">类别</param>
        /// <param name="errorMsg">出错信息</param>
        /// <returns>批量插入是否成功</returns>
        public bool BatchInsertToMongoDB(string collectionName,SqlDataReader dataReader,Type t,out string errorMsg)
        {
            return Singleton<BaseMongo>.Instance.BatchInsertTable(collectionName, dataReader, t, out errorMsg);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.Common.DB
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年7月2日
    /// Description:MSSQL批量导入
    /// </summary>
    public class SqlBulkHelper
    {
        #region- 主函数 -
        /// <summary>
        /// 执行批量导入
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="tableName">表名称</param>
        /// <param name="list">泛型实体集合</param>
        /// <param name="rejectColumn">排除的属性</param>
        /// <param name="connectionKey">连接字符串键名称</param>
        public static void ExecuteBulkImport<T>(string tableName, List<T> list, string connectionKey)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager
                                                .ConnectionStrings[connectionKey].ToString()))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var table = CreateTable(tableName, con);
                SetTable<T>(table, list);
                SqlBulkToDataBase(table, con, 1000000);

                con.Close();
            }
        }
        #endregion

        #region- 功能函数 -
        /// <summary>
        /// 设置表名，查询数据库获取表结构
        /// </summary>
        private const string SQLFormat = "SELECT TOP 0 * FROM {0}";
        private static DataTable CreateTable(string tableName, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(string.Format(SQLFormat, tableName), con);
            DataSet dsObj = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsObj);
            var table = dsObj.Tables[0];
            table.TableName = tableName;

            return table;
        }

        /// <summary>
        /// 将对象集合赋值给数据表格
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="table">数据表格</param>
        /// <param name="list">泛型实体集合</param>
        /// <param name="rejectColumn">排除属性（无需赋值的属性）</param>
        private static void SetTable<T>(DataTable table, List<T> list)
        {
            list.ForEach(p =>
            {
                System.Data.DataRow dr = table.NewRow();
                var properties = p.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var name = property.Name;
                    if (table.Columns.Contains(name))
                    {
                        dr[name] = property.GetValue(p, null) ?? DBNull.Value;
                    }
                }
                table.Rows.Add(dr);
            });
        }

        /// <summary>
        /// 批量将数据写入SqlServer
        /// </summary>
        private static void SqlBulkToDataBase(DataTable table, SqlConnection con, int timeout)
        {
            System.Data.SqlClient.SqlBulkCopy sqlBulkCopu = new SqlBulkCopy(con);
            sqlBulkCopu.DestinationTableName = table.TableName;

            sqlBulkCopu.BulkCopyTimeout = timeout;
            sqlBulkCopu.WriteToServer(table);
        }
        #endregion

        #region Sql
        /// <summary>
        /// 执行批量导入
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="tableName">表名称</param>
        /// <param name="table">DataTable数据</param>
        /// <param name="rejectColumn">排除的属性</param>
        /// <param name="connectionKey">连接字符串键名称</param>
        /// <param name="batchSize">每一批次中的行数</param>
        public static void ExecuteBulkImport(string tableName, DataTable table, string connectionKey, int batchSize)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager
                                                .ConnectionStrings[connectionKey].ToString()))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                SqlBulkToDataBase(table, con, 1000000, batchSize);
                con.Close();
            }
        }

        /// <summary>
        /// 批量将数据写入SqlServer
        /// </summary>
        /// <param name="table"></param>
        /// <param name="con"></param>
        /// <param name="timeout"></param>
        /// <param name="batchSize"></param>
        public static void SqlBulkToDataBase(DataTable table, SqlConnection con, int timeout, int batchSize)
        {
            System.Data.SqlClient.SqlBulkCopy sqlBulkCopu = new SqlBulkCopy(con);
            sqlBulkCopu.DestinationTableName = table.TableName;
            sqlBulkCopu.BatchSize = batchSize;

            foreach (DataColumn dc in table.Columns)
            {
                sqlBulkCopu.ColumnMappings.Add(new SqlBulkCopyColumnMapping(dc.Caption, dc.Caption));
            }

            sqlBulkCopu.BulkCopyTimeout = timeout;
            sqlBulkCopu.WriteToServer(table);
        }
        #endregion
    }
}

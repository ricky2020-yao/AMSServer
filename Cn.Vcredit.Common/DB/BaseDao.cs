using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Tools;

namespace Cn.Vcredit.Common.DB
{
    public class BaseDao
    {
        #region 内部变量
        /// <summary>
        /// 默认连接key
        /// </summary>
        private static string m_ConnectString = "PostLoanDB";
        protected ILogger m_Logger = LogFactory.CreateLogger(typeof(BaseDao));
        private Stopwatch m_Sw = new Stopwatch();
        private int m_DueTime = 10 * 1000;
        #endregion

        /// <summary>
        /// 获取数据连接
        /// </summary>
        /// <returns></returns>
        protected virtual SqlConnection GetConnection()
        {
            return GetConnection(m_ConnectString);
        }

        /// <summary>
        /// 获取数据连接
        /// </summary>
        /// <param name="connectKey"></param>
        /// <returns></returns>
        protected virtual SqlConnection GetConnection(string connectKey)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectKey].ToString());
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            return conn;
        }

        /// <summary>
        /// 获取数据通用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="connectKey"></param>
        /// <param name="commandType"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        protected virtual List<T> Query<T>(string sql, IDictionary<string, object> param,
            string connectKey = "", CommandType commandType = CommandType.Text, int timeOut = 0)
        {
            connectKey = connectKey.Length > 0 ? connectKey : m_ConnectString;

            var args = new DynamicParameters();
            if (param != null)
            {
                foreach (KeyValuePair<string, object> kvp in param)
                {
                    args.Add(kvp.Key, kvp.Value);
                }
            }

            using (var conn = GetConnection(connectKey))
            {
                try
                {
                    m_Sw.Reset();
                    m_Sw.Start();

                    List<T> result = conn.Query<T>(sql, args, null, true, timeOut, commandType).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    m_Logger.Error(LogSql(sql, args));
                    m_Logger.Error(ex.StackTrace);

                    return default(List<T>);
                }
                finally
                {
                    m_Sw.Stop();
                    if (m_Sw.ElapsedMilliseconds > m_DueTime)
                    {
                        m_Logger.Info(LogSql(sql, args) + "耗时:" + m_Sw.ElapsedMilliseconds + "毫秒");
                    }
                }
            }
        }

        /// <summary>
        /// 返回SqlDataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="connectKey">连接配置</param>
        /// <param name="commandType">语句类型</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>SqlDataReader</returns>
        protected virtual SqlDataReader QueryDataReader(string sql, SqlParameter[] paramArray,int timeOut = 0,
            string connectKey = "", CommandType commandType = CommandType.Text)
        {
            connectKey = connectKey.Length > 0 ? connectKey : m_ConnectString;

            SqlConnection conn = GetConnection();
            try
            {
                m_Sw.Reset();
                m_Sw.Start();
                SqlCommand command = new SqlCommand(sql,conn);
                command.CommandType = commandType;
                if(timeOut>0)
                    command.CommandTimeout = timeOut;

                if (paramArray != null)
                    command.Parameters.AddRange(paramArray);

                return command.ExecuteReader();
            }
            catch (Exception ex)
            {
                m_Logger.Error(LogSql(sql, paramArray));
                m_Logger.Error(ex.StackTrace);

                return null;
            }
            finally
            {
                m_Sw.Stop();
                if (m_Sw.ElapsedMilliseconds > m_DueTime)
                {
                    m_Logger.Info(LogSql(sql, paramArray) + "耗时:" + m_Sw.ElapsedMilliseconds + "毫秒");
                }
            }
            
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="sql">执行脚本</param>
        /// <param name="param">输入参数</param>
        /// <param name="outPutParam">输出参数</param>
        /// <param name="connectKey">连接key</param>
        /// <param name="commandType">执行类型</param>
        /// <returns></returns>
        protected virtual List<T> Query<T>(string sql, IDictionary<string, object> param,
            ref IDictionary<string, object> outPutParam, string connectKey = "",
            CommandType commandType = CommandType.Text, int timeOut = 0)
        {
            connectKey = connectKey.Length > 0 ? connectKey : m_ConnectString;

            var args = new DynamicParameters();
            if (param != null)
            {
                foreach (KeyValuePair<string, object> kvp in param)
                {
                    args.Add(kvp.Key, kvp.Value);
                }
            }
            if (outPutParam != null)
            {
                foreach (KeyValuePair<string, object> kvp in outPutParam)
                {
                    args.Add(kvp.Key, kvp.Value, null, ParameterDirection.Output);
                }
            }
            using (var conn = GetConnection(connectKey))
            {
                try
                {
                    m_Sw.Reset();
                    m_Sw.Start();

                    List<T> result = conn.Query<T>(sql, args, null, true, timeOut, commandType).ToList();

                    if (outPutParam != null)
                    {
                        IDictionary<string, object> outPut = new Dictionary<string, object>();
                        foreach (string key in outPutParam.Keys)
                        {
                            outPut.Add(key, args.Get<object>(key));
                        }
                        outPutParam = outPut;
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    m_Logger.Error(LogSql(sql, args));
                    m_Logger.Error(ex.StackTrace);
                    return default(List<T>);
                }
                finally
                {
                    m_Sw.Stop();
                    if (m_Sw.ElapsedMilliseconds > m_DueTime)
                    {
                        m_Logger.Info(LogSql(sql, args) + "耗时:" + m_Sw.ElapsedMilliseconds + "毫秒");
                    }
                }
            }
        }

        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="sql">执行脚本</param>
        /// <param name="param">参数</param>
        /// <param name="connectKey">连接key</param>
        /// <param name="commandType">执行类型</param>
        /// <returns></returns>
        protected virtual int Execute(string sql, IDictionary<string, object> param,
            string connectKey = "", CommandType commandType = CommandType.Text)
        {
            connectKey = connectKey.Length > 0 ? connectKey : m_ConnectString;

            var args = new DynamicParameters();
            if (param != null)
            {
                foreach (KeyValuePair<string, object> kvp in param)
                {
                    args.Add(kvp.Key, kvp.Value);
                }
            }
            using (var conn = GetConnection(connectKey))
            {
                try
                {
                    m_Sw.Reset();
                    m_Sw.Start();
                    return conn.Execute(sql, args, null, null,
                           commandType);
                }
                catch (Exception ex)
                {
                    m_Logger.Error(LogSql(sql, args));
                    m_Logger.Error(ex.StackTrace);
                    return default(int);
                }
                finally
                {
                    m_Sw.Stop();
                    if (m_Sw.ElapsedMilliseconds > m_DueTime)
                    {
                        m_Logger.Info(LogSql(sql, args) + "耗时:" + m_Sw.ElapsedMilliseconds + "毫秒");
                    }
                }
            }
        }

        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="sql">执行脚本</param>
        /// <param name="param">参数</param>
        /// <param name="connectKey">连接key</param>
        /// <param name="commandType">执行类型</param>
        /// <returns></returns>
        protected virtual int Execute(string sql, IDictionary<string, object> param,
            ref IDictionary<string, object> outPutParam, string connectKey = "",
            CommandType commandType = CommandType.Text)
        {
            connectKey = connectKey.Length > 0 ? connectKey : m_ConnectString;

            var args = new DynamicParameters();
            if (param != null)
            {
                foreach (KeyValuePair<string, object> kvp in param)
                {
                    args.Add(kvp.Key, kvp.Value);
                }
            }
            if (outPutParam != null)
            {
                foreach (KeyValuePair<string, object> kvp in outPutParam)
                {
                    args.Add(kvp.Key, kvp.Value, null, ParameterDirection.Output);
                }
            }
            using (var conn = GetConnection(connectKey))
            {
                try
                {
                    m_Sw.Reset();
                    m_Sw.Start();
                    int result = conn.Execute(sql, args, null, null, commandType);

                    if (outPutParam != null)
                    {
                        IDictionary<string, object> outPut = new Dictionary<string, object>();
                        foreach (string key in outPutParam.Keys)
                        {
                            outPut.Add(key, args.Get<object>(key));
                        }
                        outPutParam = outPut;
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    m_Logger.Error(LogSql(sql, args));
                    m_Logger.Error(ex.StackTrace);
                    return default(int);
                }
                finally
                {
                    m_Sw.Stop();
                    if (m_Sw.ElapsedMilliseconds > m_DueTime)
                    {
                        m_Logger.Info(LogSql(sql, args) + "耗时:" + m_Sw.ElapsedMilliseconds + "毫秒");
                    }
                }
            }
        }

        /// <summary>
        /// 检索数据到DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="connectKey"></param>
        /// <param name="commandType"></param>
        /// <returns>返回检索的数据</returns>
        public virtual DataSet QuerySet(string sql, IDictionary<string, object> param,
            string connectKey = "", CommandType commandType = CommandType.Text)
        {
            connectKey = connectKey.Length > 0 ? connectKey : m_ConnectString;

            SqlParameter[] sqlParameter = null;

            if (param != null)
            {
                sqlParameter = new SqlParameter[param.Count];
                int i = 0;
                foreach (KeyValuePair<string, object> kvp in param)
                {
                    sqlParameter.SetValue(new SqlParameter(kvp.Key, kvp.Value), i);
                    i++;
                }
            }

            var conn = new SqlHelperCommon(connectKey);
            try
            {
                m_Sw.Reset();
                m_Sw.Start();
                return conn.ExecuteDataSet(commandType, sql, sqlParameter);
            }
            catch (Exception ex)
            {
                m_Logger.Error(LogSql(sql, sqlParameter));
                m_Logger.Error(ex.StackTrace);
                return default(DataSet);
            }
            finally
            {
                m_Sw.Stop();
                if (m_Sw.ElapsedMilliseconds > m_DueTime)
                {
                    m_Logger.Info(LogSql(sql, sqlParameter) + "耗时:" + m_Sw.ElapsedMilliseconds + "毫秒");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="connectKey"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected virtual object QueryScalar(string sql, IDictionary<string, object> param,
            string connectKey = "", CommandType commandType = CommandType.Text)
        {
            connectKey = connectKey.Length > 0 ? connectKey : m_ConnectString;

            SqlParameter[] sqlParameter = null;

            if (param != null)
            {
                sqlParameter = new SqlParameter[param.Count];
                int i = 0;
                foreach (KeyValuePair<string, object> kvp in param)
                {
                    sqlParameter.SetValue(new SqlParameter(kvp.Key, kvp.Value), i);
                    i++;
                }
            }

            var conn = new SqlHelperCommon(connectKey);
            try
            {
                m_Sw.Reset();
                m_Sw.Start();
                return conn.ExecuteScalar(commandType, sql, sqlParameter);
            }
            catch (Exception ex)
            {
                m_Logger.Error(LogSql(sql, sqlParameter));
                m_Logger.Error(ex.StackTrace);
                return default(DataSet);
            }
            finally
            {
                m_Sw.Stop();
                if (m_Sw.ElapsedMilliseconds > m_DueTime)
                {
                    m_Logger.Info(LogSql(sql, sqlParameter) + "耗时:" + m_Sw.ElapsedMilliseconds + "毫秒");
                }
            }
        }

        /// <summary>
        /// 数据库日志
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private string LogSql(string sql, DynamicParameters args)
        {
            string p = "";
            if (args != null)
            {
                foreach (string name in args.ParameterNames)
                {
                    p += "参数:{0},值:{1}".StringFormat(name, args.Get<object>(name));
                }
            }
            return ("执行脚本:{0},{1}".StringFormat(sql, p));
        }

        /// <summary>
        /// 数据库日志
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private string LogSql(string sql, SqlParameter[] args)
        {
            string p = "";
            if (args != null)
            {
                foreach (SqlParameter param in args)
                {
                    p += "参数:{0},值:{1}".StringFormat(param.ParameterName, param.Value);
                }
            }
            return ("执行脚本:{0},{1}".StringFormat(sql, p));
        }

        /// <summary>
        /// 获取字符串连接的数据库
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public string GetDataBase(string db)
        {
            string result = "";
            using (var sqlConn = GetConnection(db))
            {
                result = sqlConn.Database;
            }

            return result;
        }

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="fields"></param>
        /// <param name="tableName">表名</param>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public bool BatchUpdate<T>(List<T> entities, string[] fields, string tableName, string key)
        {
            StringBuilder sql = new StringBuilder();
            Dictionary<string, object> param = new Dictionary<string, object>();
            for (int i = 0; i < entities.Count; i++)
            {
                T l = entities[i];
                sql.Append(" update " + tableName + " set ");
                string strP = string.Empty;
                foreach (string f in fields)
                {
                    strP += f + "=@" + f + i + ",";
                    param.Add("@" + f + i, l.GetType().GetProperty(f).GetValue(l, null));
                }
                sql.Append(strP.Substring(0, strP.Length - 1));
                sql.Append(" where " + key + "=@" + key + i + " ; ");
                param.Add("@" + key + i, l.GetType().GetProperty(key).GetValue(l, null));
            }
            bool result = Execute(sql.ToString(), param, "",
                   CommandType.Text) > 0;
            return result;
        }
    }
}

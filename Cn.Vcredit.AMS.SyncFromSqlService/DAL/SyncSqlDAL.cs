using Cn.Vcredit.AMS.Data.DB.MongoData;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.DataAccess.Mongo;
using Cn.Vcredit.AMS.Entity.Filter.Common;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.SyncFromSqlService.DAL
{
    public class SyncSqlDAL
    {
        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="syncTableFilter">同步过滤条件</param>
        /// <param name="errorInfo">出错信息</param>
        /// <returns>是否同步成功</returns>
        public bool SyncTable(SyncTableFilter syncTableFilter, out string errorInfo)
        {
            string sqlStr = GetSqlStr(syncTableFilter);
            errorInfo = string.Empty;

            Type type = GetMonogEntitys(syncTableFilter.DestinationName);
            if (type == null)
            {
                errorInfo = "未找到目标表的配置";
                return false;
            }

            SqlDataReader dataReader = Singleton<SyncDal>.Instance.GetDataReader(sqlStr,120);
            if (dataReader == null || !dataReader.HasRows)
            {
                errorInfo = "未找到需要同步的数据";
                return false;
            }
            
            if(syncTableFilter.ForceReLoad)
            {
                //先删除原有数据，再同步
                Singleton<BaseMongo>.Instance.ClearDataFromCollection(syncTableFilter.DestinationName);
            }

            if(syncTableFilter.ForceReLoad || !Singleton<BaseMongo>.Instance.ExistsData(syncTableFilter.DestinationName))
                return Singleton<SyncDal>.Instance.BatchInsertToMongoDB(syncTableFilter.DestinationName, dataReader, type, out errorInfo);
            else
                return Singleton<SyncDal>.Instance.SyncToMongoDB(syncTableFilter.PrimaryKeys, syncTableFilter.DestinationName, dataReader, out errorInfo);
        }

        /// <summary>
        /// 得到数据源查询条件
        /// </summary>
        /// <param name="syncTableFilter">同步表的过滤条件</param>
        /// <returns>查询条件sql</returns>
        private string GetSqlStr(SyncTableFilter syncTableFilter)
        {
            string sqlStr = "SELECT * FROM {0} ";
            sqlStr = string.Format(sqlStr, syncTableFilter.TableViewName);
            if (!syncTableFilter.ForceReLoad && !string.IsNullOrEmpty(syncTableFilter.Condition))
            {
                sqlStr = string.Format("{0} WHERE {1}", sqlStr, syncTableFilter.Condition);
            }
            return sqlStr;
        }

        /// <summary>
        /// 得到所有mongdb实体的类型
        /// </summary>
        /// <param name="destinationName">目标名称</param>
        /// <returns>mongdb实体</returns>
        private Type GetMonogEntitys(string destinationName)
        {
            string absolutePath = System.Environment.CurrentDirectory;
            string filePath = Path.Combine(absolutePath, "Cn.Vcredit.AMS.Data.dll");
            Assembly assembly = Assembly.LoadFile(filePath);
            Type[] types = assembly.GetTypes();
            Dictionary<string, Type> mongoDataEntityDict = new Dictionary<string, Type>(StringComparer.CurrentCultureIgnoreCase);
            foreach (Type type in types)
            {
                if (!type.IsClass || !type.IsSubclassOf(typeof(MongoDataEntity)))
                    continue;

                Attribute attr = type.GetCustomAttribute(typeof(MongoTableNameAtrr));
                if (attr == null)
                    continue;

                MongoTableNameAtrr mongoTableNameAtrr = attr as MongoTableNameAtrr;
                if (mongoTableNameAtrr == null)
                    continue;

                if (mongoDataEntityDict.ContainsKey(mongoTableNameAtrr.tableName))
                    continue;

                if (mongoTableNameAtrr.tableName.Equals(destinationName))
                    return type;
            }
            return null;
        }
    }
}

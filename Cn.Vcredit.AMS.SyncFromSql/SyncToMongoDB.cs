using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.DAL;
using System.Data;
using Cn.Vcredit.Common.Log;
using System.Reflection;
using Cn.Vcredit.AMS.Data.DB.MongoData;
using System.IO;
using Cn.Vcredit.Common.Tools;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Cn.Vcredit.AMS.SyncFromSql
{
    /// <summary>
    /// 同步mongodb数据
    /// </summary>
    public class SyncToMongoDB
    {
        private SyncDal syncDal = new SyncDal();
        Dictionary<string, Type> monogEntitys;
        private ILogger log = LogFactory.CreateLogger("SyncToMongoDB");
        
        /// <summary>
        /// 从数据库同步
        /// </summary>
        /// <returns>是否成功</returns>
        public bool SyncFromSql()
        {
            string info;
            monogEntitys = GetMonogEntitys();
            while (true)
            {
                //得到需要同步的表或者视图
                Dictionary<string, SyncInfo> needSyncInfo = GetNeedSyncInfo();
                System.Console.WriteLine("共有{0}张表或视图需要同步", needSyncInfo.Count);
                log.Info(string.Format("共有{0}张表或视图需要同步", needSyncInfo.Count));

                foreach(var dict in needSyncInfo)
                {
                    try
                    {
                        DateTime syncTime;
                        Stopwatch watch = new Stopwatch();
                        watch.Start();
                        if (SyncSingleTableView(dict.Value, out info, out syncTime))
                        {
                            watch.Stop();
                            System.Console.WriteLine("成功同步表或视图{0}，耗时{1}毫秒", dict.Value.TableViewName, watch.ElapsedMilliseconds);
                            log.Info(string.Format("成功同步表或视图{0}，耗时{1}毫秒", dict.Value.TableViewName,watch.ElapsedMilliseconds));
                            //同步到表，更新同步时间
                            dict.Value.HadSyncTime = syncTime;
                            syncDal.UpdateSyncTime(dict.Value.TableViewName, syncTime);
                        }
                        else
                        {
                            //记录日志，发邮件
                            log.Info(string.Format("同步表或视图{0}失败，原因{1}", dict.Value.TableViewName, info));
                        }
                    }
                    catch(Exception ex)
                    {
                        log.Info(string.Format("同步表或视图{0}失败，原因{1}", dict.Value.TableViewName, ex.Message));
                    }
                }
                Thread.Sleep(60000);
            }
        }

        /// <summary>
        /// 同步单张表
        /// </summary>
        /// <param name="syncInfo">同步信息</param>
        /// <param name="errorInfo">错误信息</param>
        /// <returns>是否成功</returns>
        private bool SyncSingleTableView(SyncInfo syncInfo,out string errorInfo,out DateTime now)
        {
            System.Console.WriteLine("开始同步表或视图{0}", syncInfo.TableViewName);
            log.Info(string.Format("开始同步表或视图{0}", syncInfo.TableViewName));
            bool isFirstTime = IsFirstTime(syncInfo);
            SqlDataReader dataReader = GetSyncData(syncInfo, isFirstTime, out now);
            //System.Console.WriteLine("表或视图{0}需要同步{1}条数据",syncInfo.TableViewName, dataTable.Rows.Count);
            //log.Info(string.Format("表或视图{0}需要同步{1}条数据",syncInfo.TableViewName, dataTable.Rows.Count));
            if (isFirstTime)
            {
                if (!monogEntitys.ContainsKey(syncInfo.DestinationName))
                {
                    errorInfo = string.Format("未能找到{0}的类定义", syncInfo.DestinationName);
                    return false;
                }
                return syncDal.BatchInsertToMongoDB(syncInfo.DestinationName, dataReader, monogEntitys[syncInfo.DestinationName], out errorInfo);
            }
            else
            {
                return syncDal.SyncToMongoDB(syncInfo.PrimaryKeys, syncInfo.DestinationName, dataReader, out errorInfo);
            }
        }

        /// <summary>
        /// 得到需要同步的配置信息
        /// </summary>
        /// <returns>需要同步的配置信息</returns>
        private Dictionary<string,SyncInfo> GetNeedSyncInfo()
        {
            List<SyncInfo> syncInfoList = syncDal.GetSyncInfoList();
            Dictionary<string, SyncInfo> syncInfoDict = 
                new Dictionary<string, SyncInfo>(StringComparer.CurrentCultureIgnoreCase);

            foreach(SyncInfo syncInfo in syncInfoList)
            {
                if (syncInfoDict.ContainsKey(syncInfo.TableViewName))
                    continue;

                if (!syncInfo.IsValid)
                    continue;

                if(syncInfo.DestinationType.Equals(SyncConsts.SyncType_MonogDB))
                {
                    syncInfoDict.Add(syncInfo.TableViewName, syncInfo);
                }
            }
            return syncInfoDict;
        }

        /// <summary>
        /// 取需要同步的数据
        /// </summary>
        /// <param name="syncInfo"></param>
        /// <returns></returns>
        private SqlDataReader GetSyncData(SyncInfo syncInfo,bool IsFirstTime,out DateTime now)
        {
            string sqlStr ;
            now = DateTime.Now;
            if (IsFirstTime)
            {
                sqlStr = string.Format("{0} SELECT * FROM {1} WITH(NOLOCK) ",SyncConsts.ReadUncommittedSet, 
                    syncInfo.TableViewName);
            }
            else
            {
                sqlStr = string.Format("{0} SELECT * FROM {1} WITH(NOLOCK) WHERE Updatetime>='{2}' AND Updatetime<'{3}'",
                    SyncConsts.ReadUncommittedSet, syncInfo.TableViewName, syncInfo.HadSyncTime.ToString(SyncConsts.DatetimeFormat),
                    now.ToString(SyncConsts.DatetimeFormat));
            }
            return syncDal.GetDataReader(sqlStr,SyncConsts.ExecuteTimeOut_120);
        }

        /// <summary>
        /// 得到所有mongdb实体的类型
        /// </summary>
        /// <returns>mongdb实体</returns>
        private Dictionary<string,Type> GetMonogEntitys()
        {
            string absolutePath = System.Environment.CurrentDirectory;
            string filePath = Path.Combine(absolutePath, "Cn.Vcredit.AMS.Data.dll");
            Assembly assembly = Assembly.LoadFile(filePath);
            Type[] types = assembly.GetTypes();
            Dictionary<string,Type> mongoDataEntityDict=new Dictionary<string,Type>(StringComparer.CurrentCultureIgnoreCase);
            foreach(Type type in types)
            {
                if( !type.IsClass || !type.IsSubclassOf(typeof(MongoDataEntity)))
                    continue;

                Attribute attr = type.GetCustomAttribute(typeof(MongoTableNameAtrr));
                if (attr == null)
                    continue;

                MongoTableNameAtrr mongoTableNameAtrr = attr as MongoTableNameAtrr;
                if (mongoTableNameAtrr == null)
                    continue;

                if (mongoDataEntityDict.ContainsKey(mongoTableNameAtrr.tableName))
                    continue;

                mongoDataEntityDict.Add(mongoTableNameAtrr.tableName, type);
            }
            return mongoDataEntityDict;
        }

        /// <summary>
        /// 是否第一次导入
        /// </summary>
        /// <param name="syncInfo">同步信息</param>
        /// <returns>是否第一次导入</returns>
        private bool IsFirstTime(SyncInfo syncInfo)
        {
            return (syncInfo.HadSyncTime == null || syncInfo.HadSyncTime == DateTime.MinValue);
        }
    }

}

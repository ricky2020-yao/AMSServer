using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Entity.Filter;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;
using Cn.Vcredit.AMS.DataAccess.Common;
using MongoDB.Driver.Builders;
using System.Collections;

namespace Cn.Vcredit.AMS.DataAccess.Mongo
{
    public class BaseMongo
    {

        #region 数据库连接相关
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectStr { get; set; }

        /// <summary>
        /// 默认连接config配置
        /// </summary>
        private const string DefaultConnectConfig = "MongoDB";
        /// <summary>
        /// 默认数据库
        /// </summary>
        private const string DefaultMongoDatabase = "admin";
        /// <summary>
        /// 默认页码
        /// </summary>
        private const int DefaultPageNo = 1;
        /// <summary>
        /// 默认一页20条
        /// </summary>
        private const int DefaultPageSize = 20;
        
        /// <summary>
        /// 得到mongo服务器
        /// </summary>
        /// <param name="connectStr">连接字符串</param>
        /// <returns>mongo服务器</returns>
        public MongoServer GetMongoServer(string connectStr = null)
        {
            SetConnectStr(connectStr);
            MongoClient mongoClient = new MongoClient(ConnectStr);
            return mongoClient.GetServer();
        }

        /// <summary>
        /// 得到mongo数据库
        /// </summary>
        /// <param name="mongoServer">mongo服务器</param>
        /// <param name="dbName">数据库名</param>
        /// <returns>mongo数据库</returns>
        public MongoDatabase GetMongoDatabase(MongoServer mongoServer,string dbName = null)
        {
            
            if(string.IsNullOrEmpty(dbName))
            {
                dbName = DefaultMongoDatabase;
            }
            
            if (mongoServer.Settings.DefaultCredentials != null)
            {
                MongoCredentials credentials =
                    new MongoCredentials(mongoServer.Settings.DefaultCredentials.Username, 
                                            mongoServer.Settings.DefaultCredentials.Password,true);

                return mongoServer.GetDatabase(dbName, credentials);
            }
            else
            {
                return mongoServer.GetDatabase(dbName);
            }
        }

        /// <summary>
        /// 得到mongo表对象
        /// </summary>
        /// <param name="mongoDatabase">mongo数据库</param>
        /// <param name="collectionName">表名</param>
        /// <returns>表对象</returns>
        public MongoCollection GetMongoCollection(MongoDatabase mongoDatabase,string collectionName)
        {
            return mongoDatabase.GetCollection(collectionName);
        }

        /// <summary>
        /// 得到mongo表对象
        /// </summary>
        /// <param name="collectionName">表名</param>
        /// <returns>表对象</returns>
        public MongoCollection GetMongoCollection(string collectionName)
        {
            MongoServer server = GetMongoServer();
            server.Connect();
            MongoDatabase database = GetMongoDatabase(server);
            return GetMongoCollection(database, collectionName);
        }


        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="connectStr">不传使用默认值</param>
        public void SetConnectStr(string connectStr = null)
        {
            if(String.IsNullOrEmpty(connectStr))
            {
                ConnectStr = ConfigurationManager.ConnectionStrings[DefaultConnectConfig].ConnectionString;
            }
            else
            {
                ConnectStr = connectStr;
            }
        }

        #endregion

        #region 公用函数
        
        /// <summary>
        /// mongo分页查询
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="query">查询条件</param>
        /// <param name="collectionName">表名</param>
        /// <param name="filter">过滤分页信息</param>
        /// <returns>分页数据</returns>
        public List<T> Query<T>(IMongoQuery query,string collectionName,BaseFilter filter)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            filter.RecordCount = Convert.ToInt32(collection.Count(query));
            SortByDocument sortByDocument = GetSortFromStr(filter.OrderbyStr);
            MongoCursor<T> mongoCursor = collection.FindAs<T>(query);
            //查询性能分析
            //BsonDocument document = collection.FindAs<T>(query).Explain();
            if(sortByDocument !=null)
            {
                mongoCursor = mongoCursor.SetSortOrder(sortByDocument);
            }
            if (filter.PageSize == 0)
            {
                filter.PageSize = DefaultPageSize;
            }
            if(filter.PageNo == 0)
            {
                filter.PageNo = DefaultPageNo;
            }
            return mongoCursor.SetSkip(filter.PageSize * (filter.PageNo - 1)).SetLimit(filter.PageSize).ToListEntity<T>();
        }

        /// <summary>
        /// 查询数据，增加需要的参数字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <param name="exceptFields"></param>
        /// <param name="containtFields"></param>
        /// <returns></returns>
        public List<T> Query<T>(IMongoQuery query, string collectionName, BaseFilter filter, string[] exceptFields, string[] containtFields)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            filter.RecordCount = Convert.ToInt32(collection.Count(query));
            SortByDocument sortByDocument = GetSortFromStr(filter.OrderbyStr);
            MongoCursor<T> mongoCursor = collection.FindAs<T>(query);
            //查询性能分析
            //BsonDocument document = collection.FindAs<T>(query).Explain();
            if (sortByDocument != null)
            {
                mongoCursor = mongoCursor.SetSortOrder(sortByDocument);
            }
            IMongoFields fields = GetFields(exceptFields, containtFields);
            if(fields !=null)
            {
                mongoCursor = mongoCursor.SetFields(fields);
            }

            if (filter.PageSize == 0)
            {
                filter.PageSize = DefaultPageSize;
            }
            if (filter.PageNo == 0)
            {
                filter.PageNo = DefaultPageNo;
            }
            return mongoCursor.SetSkip(filter.PageSize * (filter.PageNo - 1)).SetLimit(filter.PageSize).ToListEntity<T>();
        }

        /// <summary>
        /// 得到需要的字段
        /// </summary>
        /// <param name="exceptFields">排除字段</param>
        /// <param name="containtFields">包含字段</param>
        /// <returns>字段</returns>
        public IMongoFields GetFields(string[] exceptFields,string[] containtFields)
        {
            IMongoFields fields = null;
            if (exceptFields != null && exceptFields.Length > 0)
                fields = Fields.Exclude(exceptFields);

            if (containtFields != null && containtFields.Length > 0)
            {
                if (fields == null)
                    fields = Fields.Include(containtFields);
                else
                    fields = Fields.Exclude(exceptFields).Include(containtFields);
            }
            return fields;
        }

        /// <summary>
        /// 返回mongo查询数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="query">查询条件</param>
        /// <param name="collectionName">表名</param>
        /// <param name="filter">过滤条件</param>
        /// <returns>mongo数据</returns>
        public MongoCursor<T> QueryCursor<T>(IMongoQuery query, string collectionName, BaseFilter filter)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            filter.RecordCount = Convert.ToInt32(collection.Count(query));
            SortByDocument sortByDocument = GetSortFromStr(filter.OrderbyStr);
            MongoCursor<T> mongoCursor = collection.FindAs<T>(query);
            //查询性能分析
            //BsonDocument document = collection.FindAs<T>(query).Explain();
            if (sortByDocument != null)
            {
                mongoCursor = mongoCursor.SetSortOrder(sortByDocument);
            }

            if (filter.PageSize == 0)
            {
                filter.PageSize = DefaultPageSize;
            }
            if (filter.PageNo == 0)
            {
                filter.PageNo = DefaultPageNo;
            }
            return mongoCursor.SetSkip(filter.PageSize * (filter.PageNo - 1)).SetLimit(filter.PageSize);
        }

        /// <summary>
        /// 查询groupby数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="keys">主键列表</param>
        /// <param name="collectionName">表名</param>
        /// <param name="query">查询</param>
        /// <param name="filter">查询条件</param>
        /// <param name="javascriptReduce">脚本</param>
        /// <param name="javascriptFinalize">脚本2</param>
        /// <returns>查询groupby数据</returns>
        public IEnumerable<BsonDocument> QueryGroupby<T>(string[] keys, string collectionName, IMongoQuery query, BaseFilter filter,
            string javascriptReduce,string javascriptFinalize)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            GroupByBuilder groupbyBuilder = new GroupByBuilder(keys);
            BsonDocument doc = new BsonDocument();
           return collection.Group(query, groupbyBuilder, doc, BsonJavaScript.Create(javascriptReduce), BsonJavaScript.Create(javascriptFinalize));
        }

        /// <summary>
        /// mapReduce得到结果
        /// </summary>
        /// <param name="collectionName">表名</param>
        /// <param name="query">查询条件</param>
        /// <param name="finalizeJs">输出结果时转换的js</param>
        /// <param name="mapJs">map js</param>
        /// <param name="reduceJs">reduce js</param>
        /// <param name="limit">前多少条数据</param>
        /// <param name="sortBy">排序</param>
        /// <returns>文档数据</returns>
        public IEnumerable<BsonDocument> MapReduceToEnumerable(string collectionName, IMongoQuery query, string finalizeJs,string mapJs,string reduceJs,int limit,IMongoSortBy sortBy)
        {
            MapReduceResult result = MapReduceToResult(collectionName, query, finalizeJs, mapJs, reduceJs, limit, sortBy);
            if (result.Ok)
                return result.InlineResults;
            else
                return null;
        }

        /// <summary>
        /// mapReduce得到结果
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collectionName">表名</param>
        /// <param name="query">查询条件</param>
        /// <param name="finalizeJs">输出结果时转换的js</param>
        /// <param name="mapJs">map js</param>
        /// <param name="reduceJs">reduce js</param>
        /// <param name="limit">前多少条数据</param>
        /// <param name="sortBy">排序</param>
        /// <returns>自定义类型数据</returns>
        public IEnumerable<T> MapReduceToList<T>(string collectionName, IMongoQuery query, string finalizeJs, string mapJs, string reduceJs, int limit, IMongoSortBy sortBy)
        {
            MapReduceResult result = MapReduceToResult(collectionName, query, finalizeJs, mapJs, reduceJs, limit, sortBy);
            if (result.Ok)
                return result.GetResultsAs<T>();
            else
                return null;
        }

        /// <summary>
        /// mapReduce得到结果
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="query"></param>
        /// <param name="finalizeJs"></param>
        /// <param name="mapJs"></param>
        /// <param name="reduceJs"></param>
        /// <param name="limit"></param>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        public MapReduceResult MapReduceToResult(string collectionName, IMongoQuery query, string finalizeJs,string mapJs,string reduceJs,int limit,IMongoSortBy sortBy)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            MapReduceOptionsBuilder builder = new MapReduceOptionsBuilder();
            builder.SetFinalize(finalizeJs);
            
            if(limit>0)
            { 
                builder.SetLimit(limit);
            }
            if(sortBy !=null)
            { 
                builder.SetSortOrder(sortBy);
            }
            MapReduceOutput output = new MapReduceOutput();
            output.Mode = MapReduceOutputMode.Inline;
            builder.SetOutput(output);

            if (query == null || query == MongoDB.Driver.Builders.Query.Null)
                return collection.MapReduce(mapJs, reduceJs, builder);
            else
                return collection.MapReduce(query, mapJs, reduceJs, builder);
        }

        /// <summary>
        /// mongo不分页查询
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="query">查询条件</param>
        /// <param name="collectionName">表名</param>
        /// <returns>不分页数据</returns>
        public List<J> QueryAll<J>(IMongoQuery query, string collectionName,string orderStr)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            MongoCursor<J> mongoCursor = collection.FindAs<J>(query);
            return ReturnList<J>(mongoCursor, orderStr);
        }

        /// <summary>
        /// mongo不分页查询，增加字段筛选
        /// </summary>
        /// <typeparam name="J"></typeparam>
        /// <param name="query"></param>
        /// <param name="collectionName"></param>
        /// <param name="orderStr"></param>
        /// <param name="exceptFields"></param>
        /// <param name="containtFields"></param>
        /// <returns></returns>
        public List<J> QueryAll<J>(IMongoQuery query, string collectionName, string orderStr,
            string[] exceptFields, string[] containtFields)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            MongoCursor<J> mongoCursor = collection.FindAs<J>(query);
            IMongoFields fields = GetFields(exceptFields, containtFields);
            if(fields!=null)
            {
                mongoCursor.SetFields(fields);
            }
            return ReturnList<J>(mongoCursor, orderStr);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="query">查询条件</param>
        /// <param name="collectionName">表名</param>
        /// <param name="orderStr">排序字段</param>
        /// <returns>数据游标</returns>
        public MongoCursor<T> QueryAllForCursor<T>(IMongoQuery query,string collectionName)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            return collection.FindAllAs<T>();
        }

        /// <summary>
        /// 查询语句查询所有数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="queryJsonStr">查询语句</param>
        /// <param name="collectionName">表名</param>
        /// <returns>所有数据</returns>
        public List<K> QueryByJson<K>(string queryJsonStr, string collectionName, string orderStr)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            IMongoQuery query = GetMongoQueryFromJson(queryJsonStr); 
            var list = collection.FindAs<K>(query);
            if (list == null || list.Count() == 0)
                return new List<K>();

            return ReturnList<K>(list, orderStr);
        }

        /// <summary>
        /// 得到表内所有数据
        /// </summary>
        /// <typeparam name="L">类型</typeparam>
        /// <param name="collectionName">表名</param>
        /// <returns>表内所有数据</returns>
        public List<L> QueryAllRecord<L>(string collectionName, string orderStr)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            return ReturnList<L>(collection.FindAllAs<L>(), orderStr);
        }

        /// <summary>
        /// Json语句得到查询类
        /// </summary>
        /// <param name="queryJsonStr">查询字符</param>
        /// <returns>查询条件</returns>
        public IMongoQuery GetMongoQueryFromJson(string queryJsonStr)
        {
            BsonJavaScript js = new BsonJavaScript(queryJsonStr);
            return MongoDB.Driver.Builders.Query.Where(js);
        }

        /// <summary>
        /// 查询后的数据排序返回list
        /// </summary>
        /// <typeparam name="Z">类型</typeparam>
        /// <param name="mongoCursor">查询出的数据</param>
        /// <param name="orderStr">排序字符</param>
        /// <returns>数据排序返回list</returns>
        public List<Z> ReturnList<Z>(MongoCursor<Z> mongoCursor, string orderStr)
        {
            if (!string.IsNullOrEmpty(orderStr))
            {
                SortByDocument sortByDocument = GetSortFromStr(orderStr);
                mongoCursor = mongoCursor.SetSortOrder();
            }
            return mongoCursor.ToListEntity<Z>();
        }

        public string ReturnA<Z>(MongoCursor<Z> mongoCursor)
        {
            return mongoCursor.ToJson();
        }

        /// <summary>
        /// 一行一行处理更新或者插入数据
        /// </summary>
        /// <param name="primaryKeys">主键列名</param>
        /// <param name="collectionName">表名</param>
        /// <param name="dtb">数据</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>是否成功</returns>
        public bool UpdateOrInsertRowByRow(string primaryKeys, string collectionName, SqlDataReader dataReader, out string errorMsg)
        {
            errorMsg = string.Empty;
            if (dataReader == null || !dataReader.HasRows)
                return true;

            MongoCollection collection = GetMongoCollection(collectionName);
            
            while (dataReader.Read())
            {
                IMongoQuery query = GetQuery(primaryKeys, dataReader);
                MongoDB.Driver.Builders.UpdateBuilder update = new MongoDB.Driver.Builders.UpdateBuilder();

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    BsonValue bsonValue = BsonValue.Create(dataReader[i]);
                    update.AddToSet(dataReader.GetName(i), bsonValue);
                }
                WriteConcernResult result = collection.Update(query, update,UpdateFlags.Upsert);
                if(!result.Ok)
                {
                    errorMsg = result.ErrorMessage;
                    return false;
                }
            }
            dataReader.Close();
            dataReader.Dispose();
            return true;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="collectionName">视图或者表名</param>
        /// <param name="dtb">表数据</param>
        /// <param name="t">类别</param>
        /// <param name="errorMsg">出错信息</param>
        /// <returns>批量插入是否成功</returns>
        public bool BatchInsertTable(string collectionName,SqlDataReader dataReader,Type t,out string errorMsg)
        {
            errorMsg = string.Empty;
            if (dataReader == null || !dataReader.HasRows)
                return true;

            MongoCollection collection = GetMongoCollection(collectionName);

            List<BsonDocument> docList = new List<BsonDocument>();
            while (dataReader.Read())
            {
                BsonDocument bsonDocument = new BsonDocument();

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (dataReader[i] == DBNull.Value || dataReader[i] == null)
                        continue;

                    BsonValue bsonValue;
                    if (dataReader[i] is Decimal)
                        bsonValue = BsonDouble.Create(Convert.ToDouble(dataReader[i]));
                    else
                        bsonValue = BsonValue.Create(dataReader[i]);

                    bsonDocument.Add(dataReader.GetName(i), bsonValue);
                }
                docList.Add(bsonDocument);
                if (docList.Count >= DataAccessConsts.OneBatchCount)
                {
                    System.Console.WriteLine(string.Format("{0}:开始同步{1}条记录",DateTime.Now, docList.Count));
                    OneBatchInsert(t, docList, collection, out errorMsg);
                    System.Console.WriteLine(string.Format("{0}:已同步{1}条记录", DateTime.Now, docList.Count));
                    docList.Clear();
                }
            }
            dataReader.Close();
            dataReader.Dispose();

            return OneBatchInsert(t, docList, collection, out errorMsg);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="collectionName">表名</param>
        /// <param name="documents">数据</param>
        /// <param name="errorMsg">出错信息</param>
        /// <returns>是否成功</returns>
        public bool BatchInsertEnumerable(string collectionName, IEnumerable documents, out string errorMsg)
        {
            errorMsg = string.Empty;
            MongoCollection collection = GetMongoCollection(collectionName);
            IEnumerable<WriteConcernResult> writeConcernResult = collection.InsertBatch(this.GetType(), documents);
            if (writeConcernResult == null)
            {
                errorMsg = "批量插入无返回结果";
                return false;
            }
            IEnumerator<WriteConcernResult> writeConcernResultEnumerator = writeConcernResult.GetEnumerator();
            while (writeConcernResultEnumerator.MoveNext())
            {
                if (!writeConcernResultEnumerator.Current.Ok)
                {
                    errorMsg = writeConcernResultEnumerator.Current.ErrorMessage;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 插入一个批次数据
        /// </summary>
        /// <param name="t">数据类型</param>
        /// <param name="docList">数据</param>
        /// <param name="collection">数据库</param>
        /// <param name="errorMsg">出错信息</param>
        /// <returns>是否成功</returns>
        private bool OneBatchInsert(Type t, List<BsonDocument> docList, MongoCollection collection, out string errorMsg)
        {
            errorMsg = string.Empty;

            
            IEnumerable<WriteConcernResult> writeConcernResult = collection.InsertBatch(t, docList);
            if (writeConcernResult == null)
            {
                errorMsg = "批量插入无返回结果";
                return false;
            }
            IEnumerator<WriteConcernResult> writeConcernResultEnumerator = writeConcernResult.GetEnumerator();
            while (writeConcernResultEnumerator.MoveNext())
            {
                if (!writeConcernResultEnumerator.Current.Ok)
                {
                    errorMsg = writeConcernResultEnumerator.Current.ErrorMessage;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 插入批量数据
        /// </summary>
        /// <param name="t">类型</param>
        /// <param name="reader">数据连接</param>
        /// <param name="collection">mongodb表</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>是否成功</returns>
        private bool OneBatchInsert(Type t,SqlDataReader reader, MongoCollection collection, out string errorMsg)
        {
            errorMsg = string.Empty;
            IEnumerable<WriteConcernResult> writeConcernResult = collection.InsertBatch(t,reader);
            if (writeConcernResult == null)
            {
                errorMsg = "批量插入无返回结果";
                return false;
            }
            IEnumerator<WriteConcernResult> writeConcernResultEnumerator = writeConcernResult.GetEnumerator();
            while (writeConcernResultEnumerator.MoveNext())
            {
                if (!writeConcernResultEnumerator.Current.Ok)
                {
                    errorMsg = writeConcernResultEnumerator.Current.ErrorMessage;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 清除表数据
        /// </summary>
        /// <param name="collectionName">表名</param>
        /// <returns>是否成功</returns>
        public bool ClearDataFromCollection(string collectionName)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            if (!collection.Exists())
                return true;

            if (collection.Count() == 0)
                return true;

            WriteConcernResult concernResult = collection.RemoveAll();
            if (concernResult == null)
                return false;

            return concernResult.Ok;
        }

        /// <summary>
        /// 是否有表数据
        /// </summary>
        /// <param name="collectionName">表名</param>
        /// <returns>是否有数据</returns>
        public bool ExistsData(string collectionName)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            if (!collection.Exists())
                return false;

            if (collection.Count() == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据主键得到查询条件
        /// </summary>
        /// <param name="primaryKeys">主键列名</param>
        /// <param name="dataRow">数据</param>
        /// <returns>IMongoQuery</returns>
        public IMongoQuery GetQuery(string primaryKeys,DataRow dataRow)
        {
            string[] keysArray = primaryKeys.Split(new char[]{Const.CommaChar},
                StringSplitOptions.RemoveEmptyEntries);

            List<IMongoQuery> queryList = new List<IMongoQuery>();
            foreach(string key in keysArray)
            {
                BsonValue bsonValue = BsonValue.Create(dataRow[key]);
                queryList.Add(MongoDB.Driver.Builders.Query.EQ(key,bsonValue));
            }
            return MongoDB.Driver.Builders.Query.And(queryList);
        }

        /// <summary>
        /// 根据主键得到查询条件
        /// </summary>
        /// <param name="primaryKeys">主键列名</param>
        /// <param name="dataRow">数据</param>
        /// <returns>IMongoQuery</returns>
        public IMongoQuery GetQuery(string primaryKeys, SqlDataReader dataReader)
        {
            string[] keysArray = primaryKeys.Split(new char[] { Const.CommaChar },
                StringSplitOptions.RemoveEmptyEntries);

            List<IMongoQuery> queryList = new List<IMongoQuery>();
            foreach (string key in keysArray)
            {
                BsonValue bsonValue = BsonValue.Create(dataReader[key]);
                queryList.Add(MongoDB.Driver.Builders.Query.EQ(key, bsonValue));
            }
            return MongoDB.Driver.Builders.Query.And(queryList);
        }

        /// <summary>
        /// 使用管道方法得到类似select B,Max(C),Count(D) from A where x=1 group by B having Count(D)>0的记录
        /// </summary>
        /// <param name="collectionName">表名</param>
        /// <param name="matchDoc">过滤条件</param>
        /// <param name="groupDoc">分组条件</param>
        /// <param name="havingDoc">后面的过滤条件</param>
        /// <returns>记录</returns>
        public IEnumerable<BsonDocument> Aggregate(string collectionName,BsonDocument matchDoc,BsonDocument groupDoc,
            BsonDocument havingDoc)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            AggregateResult result =  collection.Aggregate(matchDoc, groupDoc, havingDoc);
            if (result.Ok)
                return result.ResultDocuments;
            else
                return null;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="collectionName">表名</param>
        /// <param name="query">查询条件</param>
        /// <param name="info">错误信息</param>
        /// <returns>是否成功</returns>
        public bool DeleteData(string collectionName,IMongoQuery query,out string info)
        {
            MongoCollection collection = GetMongoCollection(collectionName);
            WriteConcernResult result = collection.Remove(query);
            info = string.Empty;
            if(result.Ok)
                return true;
            else
            {
                info = result.ErrorMessage;
                return false;
            }
        }

        #endregion 

        #region 辅助方法
        /// <summary>
        /// 通过字符得到排序类
        /// </summary>
        /// <param name="sortStr">排序字符逗号隔开</param>
        /// <returns>排序类</returns>
        private SortByDocument GetSortFromStr(string sortStr)
        {
            
            if(string.IsNullOrEmpty(sortStr))
                return null;

            string[] sortArray=sortStr.Split(new Char[]{Const.CommaChar},StringSplitOptions.RemoveEmptyEntries);
            List<BsonElement> bsonElementList=new List<BsonElement>();
            int sortDirection = 0;
            foreach(string sortItem in sortArray)
            {
                string[] fieldItemArray = sortItem.Split(new char[]{Const.Space},StringSplitOptions.RemoveEmptyEntries);
                if(fieldItemArray.Length > 1 && fieldItemArray[1].ToUpper()==Const.SortDescUpper)
                    sortDirection = -1;
                else
                    sortDirection = 0;

                bsonElementList.Add(new BsonElement(fieldItemArray[0],sortDirection));
            }

            if (bsonElementList.Count == 0)
                return null;
            else
                return new SortByDocument(bsonElementList);
        }


        
        #endregion
    }
}

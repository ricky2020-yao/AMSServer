using Cn.Vcredit.AMS.Data.DB.MongoData;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cn.Vcredit.Common.TypeConvert;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using MongoDB.Bson;
using Cn.Vcredit.AMS.DataAccess.Mongo;
using Cn.Vcredit.Common.Patterns;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// 催收查询相关
    /// </summary>
    public class DunDal
    {
        /// <summary>
        /// 查询催收信息
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns>催收信息</returns>
        public MongoCursor<MongoDun> GetDunInfo(DunSearchFilter filter)
        {
            List<IMongoQuery> queryList = new List<IMongoQuery>();
            if(!filter.BranchKey.IsNullString())
                queryList.Add(Query.EQ("BranchKey", filter.BranchKey));
            
            if(filter.DunID !=0)
                queryList.Add(Query.EQ("DunID", filter.DunID));

            if(!filter.BusinessID.IsZero())
                queryList.Add(Query.EQ("BusinessID", filter.BusinessID));

            if(!filter.ContractNo.IsNullString())
                queryList.Add(Query.EQ("ContractNo", filter.ContractNo));
            
            if(!filter.Duner.IsNullString())
                queryList.Add(Query.Matches("Duner", filter.Duner));

            if(!filter.DunNumber.IsZero())
                queryList.Add(Query.EQ("DunNumber", filter.DunNumber));

            if(!filter.BusinessStatus.IsZero())
                queryList.Add(Query.EQ("BusinessStatus", filter.BusinessStatus));

            if(!filter.LawsuitStatus.IsZero())
                queryList.Add(Query.EQ("LawsuitStatus", filter.LawsuitStatus));

            if(!filter.LendingSideKey.IsNullString())
                queryList.Add(Query.EQ("LendingSideKey", filter.LendingSideKey));

            if(!filter.PersonName.IsNullString())
                queryList.Add(Query.EQ("CustomerName", filter.PersonName));
            
            if(filter.OutSourceTime !=DateTime.MinValue)
                queryList.Add(Query.EQ("OutSourceTime", filter.OutSourceTime));
           
            if(!filter.ProductKind.IsNullString())
                queryList.Add(Query.EQ("ProductKind", filter.ProductKind));

            if(!filter.IsCurrent.IsZero())
                queryList.Add(Query.EQ("IsCurrent", filter.IsCurrent));

            if(!filter.Region.IsNullString())
                queryList.Add(Query.EQ("Region", filter.Region));

            //if(!filter.VBSDunUnitCode.IsNullString())
            //    queryList.Add(Query.EQ("VBSDunUnitCode", filter.VBSDunUnitCode));
            //if(!filter.StartDays.IsZero())
            //    queryList.Add(Query.EQ("StartDays", filter.StartDays));

            //if (!filter.EndDays.IsZero())
            //    queryList.Add(Query.EQ("EndDays", filter.EndDays));

            //if(!filter.PersonID.IsZero())
            //    queryList.Add(Query.EQ("PersonID", filter.PersonID));

            //if (!filter.LabelType.IsNullString())
            //{
            //    queryList.Add(Query.EQ("LabelType", filter.LabelType));
            //    queryList.Add(Query.EQ("LabelCode", filter.LabelType));
            //}
            
            //if(!filter.VBSDunUnitCode.IsNullString())
            //{
            //    string[] vbsArray = filter.VBSDunUnitCode.Split(',');
            //    List<BsonValue> valueList = new List<BsonValue>();
            //    foreach(string vbs in vbsArray)
            //    {
            //        valueList.Add(vbs);
            //    }
            //    queryList.Add(Query.In("VBSDunUnitCode", valueList));
            //}
            
            //if(!filter.DunCode.IsNullString())
            //{ 
            //    queryList.Add(Query.EQ("DunCode", filter.DunCode));
            //    queryList.Add(Query.Or(Query.EQ("VaildCode", filter.DunCode), Query.EQ("UnVaildCode", filter.DunCode)));
            //}
            IMongoQuery mongoQuery;
            if(queryList.Count==0)
                mongoQuery = Query.Null;
            else
                mongoQuery = Query.And(queryList);

            if(filter.PageSize.IsZero())
                return Singleton<BaseMongo>.Instance.QueryAllForCursor<MongoDun>(mongoQuery, "Mongo.Suf_Dun");
            else
                return Singleton<BaseMongo>.Instance.QueryCursor<MongoDun>(mongoQuery, "Mongo.Suf_Dun", filter);
        }

        /// <summary>
        /// 按personid group by的结果
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="personIdList">个人编号 过滤条件</param>
        /// <returns>personid group by的结果</returns>
        public IEnumerable<BsonDocument> GetDunLabelInfo(DunSearchFilter filter,List<int> personIdList)
        {
            IMongoQuery mongoQuery = Query.Null;
            if(!string.IsNullOrEmpty(filter.LabelType))
                mongoQuery = Query.EQ("LabelCode",filter.LabelType);
            
            if(personIdList !=null && personIdList.Count > 0 )
            {
                if (mongoQuery == Query.Null)
                    mongoQuery = Query.In("PersonId", personIdList.Select(o => BsonValue.Create(o)));
                else
                    mongoQuery = Query.And(Query.In("PersonId", personIdList.Select(o => BsonValue.Create(o))), 
                        mongoQuery);
            }

            string reduceSql = @"function(doc, out){
                if(out.CreateTime== null || doc.CreateTime<=out.CreateTime){
                    out.CreateTime=doc.CreateTime;
                    out.LabelCode=doc.LabelCode;
                    }
                }";
            string finaSql = "function(out){return out;}";
            return Singleton<BaseMongo>.Instance.QueryGroupby<MongoDunLabel>(new string[] { "PersonID"}, "Mongo.Suf_DunLabel",
                mongoQuery, filter, reduceSql, finaSql);
        }

        /// <summary>
        /// 得到最新客户跟踪记录
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="businessIdList"></param>
        /// <returns></returns>
        public List<MongoCustomerTrack> GetDunCustomerTract(DunSearchFilter filter, List<int> businessIdList)
        {
            List<IMongoQuery> queryList = new List<IMongoQuery>();
            if (businessIdList != null && businessIdList.Count > 0)
                queryList.Add(Query.In("BusinessID", businessIdList.Select(o => BsonValue.Create(o))));

            if (filter.LastTrackTime != DateTime.MinValue)
                queryList.Add(Query.EQ("CreateTime", BsonValue.Create(filter.LastTrackTime)));

            if(!string.IsNullOrEmpty(filter.LabelType))
            {
                queryList.Add(Query.Or(Query.EQ("VaildCode", BsonValue.Create(filter.LabelType)),
                    Query.EQ("UnVaildCode", BsonValue.Create(filter.LabelType))));
            }
            IMongoQuery query = Query.Null;
            if(queryList.Count>0)
                query = Query.And(queryList);
            return Singleton<BaseMongo>.Instance.QueryAll<MongoCustomerTrack>(query, "Mongo.Suf_CustomerTrack", string.Empty);
        }

        /// <summary>
        /// 得到客户追踪结果
        /// 废弃该mapreduce方法，太慢
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="businessIdList">过滤订单号</param>
        /// <returns>追踪记录</returns>
        public List<MongoCustomerTrack> GetDunCustomerTractByMapReduce(DunSearchFilter filter, List<int> businessIdList)
        {
            IMongoQuery query = Query.Null;
            if(businessIdList != null && businessIdList.Count>0)
                query= Query.In("BusinessID", businessIdList.Select(o=>BsonValue.Create(o)));

            if(filter.LastTrackTime != DateTime.MinValue)
            {
                query=Query.And(query,Query.GTE("CreateTime",BsonValue.Create(filter.LastTrackTime)));
            }

            string finalizeJs = @"function Finalize(key, reduced) {
                                        if(reduced.VaildCode==null)
                                            reduced.VaildCode='';

                                        if(reduced.UnVaildCode==null)
                                            reduced.UnVaildCode='';

	                                    return reduced;
                                    }";
            string mapJs = @"function Map() {
	                            emit(
		                            this.BusinessID,				
		                            {CreateTime: this.CreateTime, VaildCode: this.VaildCode,UnVaildCode:this.UnVaildCode}); 
                            }";
            string reduceJs = @"function Reduce(key, values) {
	                            var reduced = {CreateTime:new Date(1900,0,1), VaildCode:'',UnVaildCode:''}; 
	                            values.forEach(function(val) {
                                    if(reduced.CreateTime<val.CreateTime){
		                            reduced.CreateTime = val.CreateTime; 	
		                            reduced.VaildCode = val.VaildCode; 
                                    reduced.UnVaildCode = val.UnVaildCode; }});
                                return reduced;}";
            IEnumerable<BsonDocument> bsonDocument = Singleton<BaseMongo>.Instance.MapReduceToEnumerable("Mongo.Suf_CustomerTrack", query, finalizeJs, mapJs, reduceJs,
                0, null);
            
            List<BsonDocument> list = bsonDocument.ToList();
            List<MongoCustomerTrack> customerList = new List<MongoCustomerTrack>();
            foreach(BsonDocument doc in list)
            {
                BsonValue val = doc.GetValue(1);
                if (val == null)
                    continue;

                if (!val.IsBsonDocument)
                    continue;

                BsonDocument bsonDoc = val.ToBsonDocument();
                BsonValue bsonVal;
                MongoCustomerTrack track = new MongoCustomerTrack();
                if(bsonDoc.TryGetValue("CreateTime",out bsonVal))
                {
                    track.CreateTime = bsonVal.AsUniversalTime.ToLocalTime();
                }

                if (filter.LastTrackTime != DateTime.MinValue && filter.LastTrackTime != track.CreateTime)
                    continue;

                if (bsonDoc.TryGetValue("VaildCode", out bsonVal))
                {
                    track.VaildCode = bsonVal.ToString();
                }

                if (bsonDoc.TryGetValue("UnVaildCode", out bsonVal))
                {
                    track.UnVaildCode = bsonVal.ToString();
                }

                if(!string.IsNullOrEmpty(filter.LabelType))
                {
                    if (!string.IsNullOrEmpty(track.VaildCode) && !track.VaildCode.Equals(filter.LabelType))
                        continue;

                    if (string.IsNullOrEmpty(track.VaildCode) && !track.UnVaildCode.Equals(filter.LabelType))
                        continue;
                }
                customerList.Add(track);
            }
            return customerList;
        }
    }
}

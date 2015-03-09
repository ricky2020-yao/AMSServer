using Cn.Vcredit.AMS.Data.DB.MongoData;
using Cn.Vcredit.Common.Patterns;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using Cn.Vcredit.AMS.DataAccess.Common;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;

namespace Cn.Vcredit.AMS.DataAccess.Mongo
{
    public class BusinessInfo
    {
        /// <summary>
        /// 得到订单查询列表
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns>订单信息</returns>
        public List<MongoBusiness> GetBusinessList(SearchBusinessListFilter filter)
        {
            IMongoQuery mongoQuery = DataAccessUtility.GetMongoQueryFromFilter(filter);
            return Singleton<BaseMongo>.Instance.Query<MongoBusiness>(mongoQuery, "Mongo.Suf_Business", filter);
        }
    }
}

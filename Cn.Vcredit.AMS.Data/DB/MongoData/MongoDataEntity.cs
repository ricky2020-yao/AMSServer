using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    public abstract class MongoDataEntity
    {
        /// <summary>
        /// mongodb编号
        /// </summary>
        public ObjectId _id { get; set; }
    }

    public class MongoTableNameAtrr: Attribute
    {
        public string tableName;
        public MongoTableNameAtrr(string tableName)
        {
            this.tableName = tableName;
        }
    }
}

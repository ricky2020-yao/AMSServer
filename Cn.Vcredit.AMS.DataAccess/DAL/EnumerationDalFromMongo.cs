using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.Common;
using Cn.Vcredit.AMS.DataAccess.Mongo;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    public class EnumerationDalFromMongo
    {
        /// <summary>
        /// 根据Key值，获取相应的子枚举
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Enumeration> GetEnumerations(string key)
        {
            string jsCondition="{EnumKey:" + key + "}";
            List<Enumeration> enumerationList = Singleton<BaseMongo>.Instance.QueryByJson<Enumeration>
                (jsCondition, DataAccessConsts.MongoTable_EnumerationAll,string.Empty);
            if (enumerationList == null || enumerationList.Count == 0)
                return null;

            jsCondition = "{Super:" + enumerationList[0].Id + "}";
            return Singleton<BaseMongo>.Instance.QueryByJson<Enumeration>
                (jsCondition, DataAccessConsts.MongoTable_EnumerationAll,"DisplayOrder ASC");
        }

        /// <summary>
        /// 获取所有枚举值
        /// </summary>
        /// <returns></returns>
        public List<Enumeration> GetAllEnumerations()
        { 
            return Singleton<BaseMongo>.Instance.QueryAllRecord<Enumeration>
                (DataAccessConsts.MongoTable_EnumerationAll, "DisplayOrder ASC");
        }

    }
}

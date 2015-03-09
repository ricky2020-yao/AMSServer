using Cn.Vcredit.AMS.Data.Cache.Data;
using Cn.Vcredit.AMS.DataAccess.Common;
using Cn.Vcredit.AMS.DataAccess.Mongo;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// 从mongo取分部信息
    /// </summary>
    public class DivisionDalFromMongo : BaseDao
    {
        /// <summary>
        /// 根据查询条件、排序、参数集，查询城市对象集
        /// </summary>
        /// <returns></returns>
        public IList<DivisionV> GetDivisionList()
        {
            try
            {
                return Singleton<BaseMongo>.Instance.QueryAllRecord<DivisionV>
                    (DataAccessConsts.MongoTable_DivisionV,string.Empty);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 查询城市对象集
        /// </summary>
        /// <returns></returns>
        public IList<RegionV> GetRegionList()
        {
            try
            {
                return Singleton<BaseMongo>.Instance.QueryAllRecord<RegionV>
                    (DataAccessConsts.MongoTable_RegionV,string.Empty);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

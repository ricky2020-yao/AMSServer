using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.Common.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月5日
    /// Description:产品类型数据操作类
    /// </summary>
    public class ProductKindDal:BaseDao
    {
        /// <summary>
        /// 获取产品类型
        /// </summary>
        /// <returns></returns>
        public List<ProductKind> GetProductKinds()
        {
            string sql = "SELECT * FROM [Code].[ProductKind]";
            return Query<ProductKind>(sql, null, "SysDB", System.Data.CommandType.Text, 60000);
        }
    }
}

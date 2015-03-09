using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.Caches
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月5日
    /// Description:产品类型缓存
    /// </summary>
    public class ProductKindCache
    {
        /// <summary>
        /// 产品类型缓存
        /// </summary>
        public List<ProductKind> ProductKinds { get; private set; }
        
        /// <summary>
        /// 初始化
        /// </summary>
        public ProductKindCache()
        {
            ProductKinds = Singleton<ProductKindDal>.Instance.GetProductKinds();
        }
    }
}

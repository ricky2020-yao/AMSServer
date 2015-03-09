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
    /// Description:贷款产品缓存
    /// </summary>
    public class LoanKindCache
    {
        /// <summary>
        /// 产品类型缓存
        /// </summary>
        public List<LoanKind> LoanKinds { get; private set; }
        
        /// <summary>
        /// 初始化
        /// </summary>
        public LoanKindCache()
        {
            LoanKinds = Singleton<LoanKindDal>.Instance.GetLoanKinds();
        }
    }
}

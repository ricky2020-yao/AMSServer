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
    /// Description:客户信息缓存
    /// </summary>
    public class CustomerCache
    {
        /// <summary>
        /// 银行信息缓存
        /// </summary>
        public List<CustomerData> CustomerInfos { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public CustomerCache()
        {
            CustomerInfos = Singleton<CustomerDal>.Instance.GetCustomerInfo();
        }
    }
}

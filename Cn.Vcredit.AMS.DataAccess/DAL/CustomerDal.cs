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
    /// Description:客户信息操作类
    /// </summary>
    public class CustomerDal:BaseDao
    {
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <returns></returns>
        public List<CustomerData> GetCustomerInfo()
        {
            string sql = "SELECT CustomerID, PersonId, CustomerName, IdenNo, Mobile "
             + " FROM [customer].[vw_customer_CustomerBasic]";
            return Query<CustomerData>(sql, null, "PostLoanDB", System.Data.CommandType.Text, 60000);
        }
    }
}

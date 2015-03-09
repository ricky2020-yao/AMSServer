using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:缓存相关数据操作类
    /// </summary>
    public class CacheDal:BaseDao
    {
        /// <summary>
        /// 获取公司收款账户
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<BankAccount> GetBankAccount(string sql)
        {
            return Query<BankAccount>(sql, null, "PostLoanDB", System.Data.CommandType.Text, 60000);
        }
    }
}

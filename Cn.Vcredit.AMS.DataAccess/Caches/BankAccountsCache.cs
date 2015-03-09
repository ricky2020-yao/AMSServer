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
    /// CreateTime:2014年7月28日
    /// Description:公司收款账户缓存
    /// </summary>
    public class BankAccountsCache
    {
        /// <summary>
        /// 公司收款账户
        /// </summary>
        public List<BankAccount> BankAccounts { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public BankAccountsCache()
        {
            string sql = "SELECT * FROM [dbo].[BankAccount]";
            BankAccounts = Singleton<CacheDal>.Instance.GetBankAccount(sql);
        }
    }
}

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
    /// Description:银行信息数据库操作类
    /// </summary>
    public class BankDal:BaseDao
    {
        /// <summary>
        /// 获取银行信息
        /// </summary>
        /// <returns></returns>
        public List<BankInfo> GetBankInfo()
        {
            string sql = "SELECT * FROM [Code].[BankType]";
            return Query<BankInfo>(sql, null, "SysDB", System.Data.CommandType.Text, 60000);
        }
    }
}

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
    /// Description:贷款产品数据操作类
    /// </summary>
    public class LoanKindDal : BaseDao
    {
        /// <summary>
        /// 获取贷款产品
        /// </summary>
        /// <returns></returns>
        public List<LoanKind> GetLoanKinds()
        {
            string sql = "SELECT * FROM [Code].[LoanKind] WHERE IsDisable = 1 AND IsDelete = 0";
            return Query<LoanKind>(sql, null, "SysDB", System.Data.CommandType.Text, 60000);
        }
    }
}

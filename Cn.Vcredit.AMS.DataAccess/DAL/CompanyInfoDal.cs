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
    /// Description:公司信息数据库操作类（服务方，放款方，担保方）
    /// </summary>
    public class CompanyInfoDal:BaseDao
    {
        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <returns></returns>
        public List<CompanyInfo> GetCompanyInfo()
        {
            string sql = "SELECT * FROM [Code].[CompanyInfo]";
            return Query<CompanyInfo>(sql, null, "SysDB", System.Data.CommandType.Text, 60000);
        }
    }
}

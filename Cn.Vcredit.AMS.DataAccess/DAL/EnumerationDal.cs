using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.Common.Tools;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月29日
    /// Description:系统枚举数据操作类
    /// </summary>
    public class EnumerationDal:BaseDao
    {
        /// <summary>
        /// 根据Key值，获取相应的子枚举
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Enumeration> GetEnumerations(string key)
        {
            string sql = "SELECT * FROM [common].[Enumeration]  with(nolock) WHERE Super = "
                + "(SELECT Id FROM [common].[Enumeration]  with(nolock) WHERE EnumKey = '{0}') ORDER BY DisplayOrder".StringFormat(key);

            return Query<Enumeration>(sql, null, "SysDB", System.Data.CommandType.Text, 60000);
        }

        /// <summary>
        /// 根据指定的Key，获取Enumeration
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Enumeration GetEnumerationByKey(string key)
        {
            string sql = "SELECT Id FROM [common].[Enumeration]  with(nolock) WHERE EnumKey = '{0}') ORDER BY DisplayOrder".StringFormat(key);

             List <Enumeration> lstEnumeration = Query<Enumeration>(sql
                 , null, "SysDB", System.Data.CommandType.Text, 60000);

             if (lstEnumeration == null || lstEnumeration.Count == 0)
                 return null;

             return lstEnumeration[0];
        }

        /// <summary>
        /// 获取所有枚举值
        /// </summary>
        /// <returns></returns>
        public List<Enumeration> GetAllEnumerations()
        {
            string sql = "SELECT * FROM [common].[Enumeration] with(nolock) ";

            return Query<Enumeration>(sql, null, "SysDB", System.Data.CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取公司ID
        /// </summary>
        /// <param name="companyKeys"></param>
        /// <returns></returns>
        public List<int> GetCompanyIds(List<string> companyKeys)
        {
            if (companyKeys == null || companyKeys.Count == 0)
                return new List<int>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT f.companyId FROM [Code].[CompanyInfo] f  with(nolock) ");
            sb.Append(" LEFT JOIN [common].[Enumeration] e  with(nolock)  ON e.Name = f.companyname");
            sb.Append(" WHERE e.FullKey IN ('");
            sb.Append(string.Join("','", companyKeys));
            sb.Append("')");

            return Query<int>(sb.ToString(), null, "SysDB", System.Data.CommandType.Text, 60000);
        }
    }
}

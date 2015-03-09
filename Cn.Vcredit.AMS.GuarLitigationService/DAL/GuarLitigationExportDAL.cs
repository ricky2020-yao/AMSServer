using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.GuarLitigationService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年10月20日
    /// Description:担保和诉讼设置导出数据类
    /// </summary>
    public class GuarLitigationExportDAL<T> :
        BaseSearchDAL<T> where T : class, new()
    {
        /// <summary>
        /// 获取指定如何解释命令字符串
        /// </summary>
        /// <returns></returns>
        protected override CommandType GetCommandType()
        {
            return CommandType.Text;
        }

        /// <summary>
        /// 获取检索数据的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSql(BaseFilter baseFilter)
        {
            BusinessGuaranteeFilter filter = baseFilter as BusinessGuaranteeFilter;
            if (filter == null)
                return "";
            
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append("    CustomerName ");
            sql.Append("    ,IdenNo ");
            sql.Append("    ,Mobile ");
            sql.Append("    ,LendingSideKey ");
            sql.Append("    ,OverAmount ");
            sql.Append("    ,OverdueTag ");
            sql.Append("    ,LawsuitStatus ");
            sql.Append("    ,MinMonth ");
            sql.Append("    ,MaxMonth ");
            sql.Append("    ,ExpireDate ");
            sql.Append("    FROM [dbo].[ViewBusinessGuarantee] ");
            sql.Append("   WHERE BusinessID IN ('" + filter.BusinessIDs + "')");

            return sql.ToString();
        }
    }
}

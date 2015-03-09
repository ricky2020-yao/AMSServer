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
    /// Description:担保和诉讼设置导出明细数据类
    /// </summary>
    public class GuarLitigationExportGuaranteeDetailDAL<T> :
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
            sql.Append("     GuaranteeNum ");
            sql.Append("    ,ContractNo ");
            sql.Append("    ,CustomerName ");
            sql.Append("    ,IdenNo ");
            sql.Append("    ,LoanCapital ");
            sql.Append("    ,DateRegion ");
            sql.Append("    ,StopDate ");
            sql.Append("    ,DueCapital ");
            sql.Append("    ,DueInterest ");
            sql.Append("    ,DueService ");
            sql.Append("    ,DuePenltyInt ");
            sql.Append("    ,DueTotal ");
            sql.Append("    ,ReceivedMonth ");
            sql.Append("    FROM [dbo].[ViewGuaranteeDetail] ");
            sql.Append("   WHERE GuaranteeNum = '" + filter.BatchNo + "'");

            return sql.ToString();
        }
    }
}

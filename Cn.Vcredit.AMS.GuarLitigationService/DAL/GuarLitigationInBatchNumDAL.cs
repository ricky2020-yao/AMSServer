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
    /// CreateTime:2014年10月23日
    /// Description:担保和诉讼设置批量转担保数据类
    /// </summary>
    public class GuarLitigationInBatchNumDAL<T> :
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
        /// 获取检索数据的SQL文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSql(BaseFilter baseFilter)
        {
            BusinessGuaranteeFilter filter = baseFilter as BusinessGuaranteeFilter;
            if (filter == null)
                return null;

            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append("     GuaranteeNum ");
            sql.Append("    ,BusinessStatus ");
            sql.Append("    ,LendingSideKey ");
            sql.Append("    ,BusinessID ");
            sql.Append("    FROM [dbo].[Business] WITH (NOLOCK)");
            sql.Append("   WHERE BusinessID IN (" + filter.BusinessIDs + ")");

            return sql.ToString();
        }

        /// <summary>
        /// 批量转担保
        /// </summary>
        /// <param name="businessIds"></param>
        /// <param name="guaranteeNum"></param>
        /// <returns></returns>
        public int UpdateBatchNum(string businessIds, string guaranteeNum)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE [dbo].[Business]");
            sql.Append("     SET GuaranteeNum ='" + guaranteeNum + "'");
            sql.Append("   WHERE BusinessID IN (" + businessIds + ")");

            return Execute(sql.ToString(), null, GetConnectKey(), CommandType.Text);
        }
    }
}

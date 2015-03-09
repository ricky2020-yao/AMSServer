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
    /// Description:担保和诉讼设置编辑数据类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GuarLitigationEditDAL<T> :
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
            sql.Append(" SELECT BusinessID ");
            sql.Append("       ,BusinessStatus");
            sql.Append("       ,LawsuitStatus");
            sql.Append("       ,ToLitigationTime");
            sql.Append("       ,IsRepayment");
            sql.Append("       ,LawsuitCode");
            sql.Append("       ,LoanCapital");
            sql.Append("       ,LoanPeriod");
            sql.Append("       ,InterestRate");
            sql.Append("       ,ServiceRate");
            sql.Append("       ,GuaranteeSideKey");
            sql.Append("       ,ContractNo");
            sql.Append("       ,CustomerID");
            sql.Append("       ,LendingSideKey");
            //sql.Append("       ,StopDate");
            sql.Append("  FROM [dbo].[Business] WITH (NOLOCK)");
            sql.Append("   WHERE BusinessID = " + filter.BusinessID);

            return sql.ToString();
        }

        /// <summary>
        /// 查询关帐日
        /// </summary>
        /// <returns></returns>
        public DateTime GetCloseBillDate()
        {
            return (DateTime)QueryScalar("SELECT TOP 1 StopDate FROM Accounting.CloseBillDate WITH (NOLOCK)"
                , null, GetConnectKey());
        }
    }
}

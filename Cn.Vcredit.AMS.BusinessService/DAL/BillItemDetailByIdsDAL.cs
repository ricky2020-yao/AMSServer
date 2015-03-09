using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.DAL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-11-21
    /// Desc:账单科目查询数据逻辑类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BillItemDetailByIdsDAL<T>
        : BaseSearchDAL<T> where T : class, new()
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
        /// 根据过滤条件，返回检索数据的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSql(BaseFilter baseFilter)
        {
            var filter = baseFilter as SearchBusinessListFilter;
            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT BillItemID");
            sb.Append("       ,BillID");
            sb.Append("       ,Subject");
            sb.Append("       ,DueDate");
            sb.Append("       ,Amount");
            sb.Append("       ,DueAmt");
            sb.Append("       ,ReceivedAmt");
            sb.Append("       ,CreateTime");
            sb.Append("       ,FullPaidTime");
            sb.Append("       ,Overdue");
            sb.Append("       ,SubjectType");
            sb.Append("       ,IsCurrent");
            sb.Append("       ,IsShelve");
            sb.Append("       ,BusinessID");
            sb.Append("       ,PenaltyIntAmt");
            sb.Append(" FROM dbo.BillItem WITH (NOLOCK)");
            sb.Append(" WHERE BillItemID IN (" + filter.BillItemIds + ")");
            sb.Append(" ORDER BY BillID, BillItemID");

            return sb.ToString();
        }
    }
}

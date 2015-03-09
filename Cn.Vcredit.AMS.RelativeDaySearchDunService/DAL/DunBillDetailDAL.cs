using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.RelativeDaySearchDunService.DAL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-11-21
    /// Desc:账单查询数据逻辑类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DunBillDetailDAL<T>
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
            RelativeDaySearchDunFilter filter = baseFilter as RelativeDaySearchDunFilter;
            RelativeDayExportDunFilter exportFilter = baseFilter as RelativeDayExportDunFilter;
            if (filter == null && exportFilter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT BillID");
            sb.Append("       ,BusinessID");
            //sb.Append("       ,CustomerID");
            sb.Append("       ,BillType");
            sb.Append("       ,BillStatus");
            sb.Append("       ,BillMonth");
            sb.Append("       ,CompanyKey");
            sb.Append("       ,BeginTime");
            sb.Append("       ,EndTime");
            sb.Append("       ,LimitTime");
            sb.Append("       ,CreateTime");
            //sb.Append("       ,OperatorID");
            sb.Append("       ,IsCurrent");
            sb.Append("       ,FullPaidTime");
            sb.Append("       ,IsShelve");
            //sb.Append("       ,DeductionID");
            //sb.Append("       ,IsFixed");
            sb.Append("       ,DueDate");
            sb.Append(" FROM dbo.Bill WITH (NOLOCK)");

            if (filter != null)
                sb.Append(" WHERE BusinessID IN ('" + filter.BusinessIds + "')");
            else if (exportFilter != null)
                sb.Append(" WHERE BusinessID IN ('" + exportFilter.BusinessIds + "')");

            sb.Append(" ORDER BY BillID");

            return sb.ToString();
        }
    }
}

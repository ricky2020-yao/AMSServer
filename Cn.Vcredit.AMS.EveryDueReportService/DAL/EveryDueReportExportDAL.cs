using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.EveryDueReportService.DAL
{
    /// <summary>
    /// Author:wichell
    /// CreateTime:2014年10月9日
    /// Description:每日逾期静态报表导出数据类
    /// </summary>
    public class EveryDueReportExportDAL<T> :
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
            EveryDueReportFilter filter = baseFilter as EveryDueReportFilter;
            if (filter == null)
                return "";

            string sqlFilePath = "Services\\SQL\\EveryDueReport\\Sql\\EXPORT_OVERDUESTATIC.sql";

            StringBuilder replaceSQL = new StringBuilder();
            if (!string.IsNullOrEmpty(filter.ContractNo))
                replaceSQL.AppendLine(" and ContractNo=" + "'" + filter.ContractNo + "'");
            if (filter.DunId != 0)
                replaceSQL.AppendLine(" and DunID=" + filter.DunId);
            if (!string.IsNullOrEmpty(filter.IdenNo))
                replaceSQL.AppendLine(" and IdNumber=" + "'" + filter.IdenNo + "'");
            if (!string.IsNullOrEmpty(filter.CustomerName))
                replaceSQL.AppendLine(" and CustomerName=" + "'" + filter.CustomerName + "'");
            if (!string.IsNullOrEmpty(filter.CurDueSign))
                replaceSQL.AppendLine(" and TodayOverdueMark=" + "'" + filter.CurDueSign + "'");
            if (!string.IsNullOrEmpty(filter.FirstDueSign))
                replaceSQL.AppendLine(" and BeginningOverdueMark=" + "'" + filter.FirstDueSign + "'");
            if (!string.IsNullOrEmpty(filter.SaleWay))
                replaceSQL.AppendLine(" and SalesChannels=" + "'" + filter.SaleWay + "'");
            if (!string.IsNullOrEmpty(filter.BusStatus))
                replaceSQL.AppendLine(" and BusinessStatus=" + "'" + filter.BusStatus + "'");
            if (!string.IsNullOrEmpty(filter.LawitStatus))
                replaceSQL.AppendLine(" and LitigationStatus=" + "'" + filter.LawitStatus + "'");
            if (!string.IsNullOrEmpty(filter.ProductType))
                replaceSQL.AppendLine(" and ProductType=" + "'" + filter.ProductType + "'");
            if (!string.IsNullOrEmpty(filter.SignArean))
                replaceSQL.AppendLine(" and SigningCity=" + "'" + filter.SignArean + "'");
            if (!string.IsNullOrEmpty(filter.ExternalStatus))
                replaceSQL.AppendLine(" and OutStatus=" + "'" + filter.ExternalStatus + "'");
            if (!string.IsNullOrEmpty(filter.MinDueDays))
                replaceSQL.AppendLine(" and OverdueDays>=" + filter.MinDueDays);
            if (!string.IsNullOrEmpty(filter.MaxDueDays))
                replaceSQL.AppendLine(" and OverdueDays<=" + filter.MaxDueDays);
            if (!string.IsNullOrEmpty(filter.StartDate))
                replaceSQL.AppendLine(" and StatisticsDate=" + "'" + filter.StartDate + "'");

            if (filter.IsDue.HasValue)
                replaceSQL.AppendLine(filter.IsDue.Value ? " and OverdueAmount>0" : " and OverdueAmount=0");

            return sqlFilePath.ToFileContent(true, replaceSQL.ToString());
        }
    }
}

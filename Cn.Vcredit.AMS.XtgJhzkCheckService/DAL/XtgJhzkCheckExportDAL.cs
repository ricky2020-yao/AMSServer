using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.XtgJhzkCheckService.DAL
{
    /// <summary>
    /// Author:Ricky
    /// CreateTime:2014-12-9
    /// Description:信托归集户账款核对导出数据处理层
    /// </summary>
    public class XtgJhzkCheckExportDAL<T>
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
            var filter = baseFilter as XtgJhzkCheckExportFilter;

            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ");
            sb.Append("        Bid ");
            sb.Append("        ,ContractNo ");
            sb.Append("        ,CreateTime ");
            sb.Append("        ,BillMonth ");
            sb.Append("        ,Subject ");
            sb.Append("        ,SavingCard ");
            sb.Append("        ,SavingUser ");
            sb.Append("        ,Amount ");
            sb.Append("        ,ReceivedType ");
            sb.Append("        ,ReceivedTime ");
            sb.Append("        ,ToAcountTime ");
            sb.Append("        ,Region ");
            sb.Append("        ,ReceivedID ");
            sb.Append(" FROM [dbo].[ViewXTGJHZK] ");

            string condition = CombineCondition(filter);
            if (!string.IsNullOrEmpty(condition))
                sb.Append(condition);

            return sb.ToString();
        }

        /// <summary>
        /// 生成检索条件
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string CombineCondition(XtgJhzkCheckExportFilter filter)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" WHERE ProductType = " + filter.BillType.ToByte());

            if (!string.IsNullOrEmpty(filter.Region))
                sb.Append(" AND Region = '" + filter.Region + "'");
            if (filter.BusinessID != 0)
                sb.Append(" AND Bid = " + filter.BusinessID);
            if (!string.IsNullOrEmpty(filter.ContractNo))
                sb.Append(" AND ContractNo = '" + filter.ContractNo + "'");
            if (!string.IsNullOrEmpty(filter.CashCardNo))
                sb.Append(" AND SavingCard = '" + filter.CashCardNo + "'");
            if (!string.IsNullOrEmpty(filter.BillStatus))
                sb.Append(" AND BusinessStatus = " + filter.BillStatus.ToByte());
            if (!string.IsNullOrEmpty(filter.LendingSide))
                sb.Append(" AND LendingSide = '" + filter.LendingSide + "'");
            if (!string.IsNullOrEmpty(filter.ServiceSide))
                sb.Append(" AND ServiceSide = '" + filter.ServiceSide + "'");
            if (!string.IsNullOrEmpty(filter.ReceiveAccount)
                && "1" == filter.ReceiveAccount)
                sb.Append(" AND AccountID IN (11, 13, 14)");

            if (!string.IsNullOrEmpty(filter.StartTime))
            {
                DateTime receivedTimeB = DateTime.Parse(filter.StartTime);
                if (receivedTimeB > DateTime.Parse(filter.StartTime).Date)
                    receivedTimeB = DateTime.Parse(filter.StartTime);
                else receivedTimeB = DateTime.Parse(filter.StartTime).Date;

                sb.Append(" AND ReceivedTime >= '" + receivedTimeB.ToString() + "'");
            }

            if (!string.IsNullOrEmpty(filter.EndTime))
            {
                DateTime receivedTimeE = DateTime.Parse(filter.EndTime);
                if (receivedTimeE > DateTime.Parse(filter.EndTime).Date)
                    receivedTimeE = DateTime.Parse(filter.EndTime).AddSeconds(1);
                else receivedTimeE = receivedTimeE.Date.AddDays(1);
                sb.Append(" AND ReceivedTime < '" + receivedTimeE.ToString() + "'");
            }

            if (!string.IsNullOrEmpty(filter.ToAccountBeginDate))
                sb.Append(" AND ToAcountTime >= '" + filter.ToAccountBeginDate + "'");
            if (!string.IsNullOrEmpty(filter.ToAccountEndDate))
                sb.Append(" AND ToAcountTime <= '" + filter.ToAccountEndDate + "'");
            if (!string.IsNullOrEmpty(filter.ReceiveType))
                sb.Append(" AND ReceivedType IN ('" + filter.ReceiveType + "')");
            if (!string.IsNullOrEmpty(filter.ReptColSubject))
                sb.Append(" AND Subject IN ('" + filter.ReptColSubject + "')");

            return sb.ToString();
        }
    }
}

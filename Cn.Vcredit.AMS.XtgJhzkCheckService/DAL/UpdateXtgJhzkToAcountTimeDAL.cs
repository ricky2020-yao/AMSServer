using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.XtgJhzkCheckService.DAL
{

    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月01日
    /// Description:信托归集户账款核对清除到账日期数据访问层
    /// </summary>
    public class UpdateXtgJhzkToAcountTimeDAL : BaseUpdateDAL
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
        /// 获取检索数据的存储过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetUpdateSql(BaseFilter baseFilter)
        {
            XtgJhzkCheckFilter filter = baseFilter as XtgJhzkCheckFilter;

            if (filter == null)
                return "";

            if (filter.IsUpdateAll)
                return UpdateAllXtgJhzkToAccountTime(filter);
            else
                return UpdateXtgJhzkToAccountTimeByReceiveId(filter);
        }

        /// <summary>
        /// 根据检索条件，全部更新到账时间
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string UpdateAllXtgJhzkToAccountTime(XtgJhzkCheckFilter filter)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE dbo.Received ");
            sb.Append("    SET ToAcountTime = '" + filter.ToAcountTime.ToString() + "'");
            sb.Append("      , OperatorID =" + filter.OperatorID);
            sb.Append(" WHERE ReceivedID IN (");
            sb.Append("     SELECT ReceivedID FROM dbo.ViewXTGJHZK");
            sb.Append("     WHERE 1 = 1");

            string condition = CombineCondition(filter);
            if (!string.IsNullOrEmpty(condition))
                sb.Append(condition);
            sb.Append(" )");

            return sb.ToString();
        }

        /// <summary>
        /// 根据实收编号，更新到账时间
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string UpdateXtgJhzkToAccountTimeByReceiveId(XtgJhzkCheckFilter filter)
        {
            if (string.IsNullOrEmpty(filter.ReceivedIDs))
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE dbo.Received ");
            sb.Append("    SET ToAcountTime = '" + filter.ToAcountTime.ToString() + "'");
            sb.Append("      , OperatorID =" + filter.OperatorID);
            sb.Append(" WHERE ReceivedID IN (" + filter.ReceivedIDs + ")");

            return sb.ToString();
        }

        /// <summary>
        /// 生成检索条件
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string CombineCondition(XtgJhzkCheckFilter filter)
        {
            StringBuilder sb = new StringBuilder();

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
            if (!string.IsNullOrEmpty(filter.BillType))
                sb.Append(" AND ProductType = " + filter.BillType.ToByte());
            if (!string.IsNullOrEmpty(filter.LendingSide))
                sb.Append(" AND LendingSide = '" + filter.LendingSide + "'");
            if (!string.IsNullOrEmpty(filter.ServiceSide))
                sb.Append(" AND ServiceSide = '" + filter.ServiceSide + "'");

            if (!string.IsNullOrEmpty(filter.ReceiveAccount)
                && "1" == filter.ReceiveAccount)
            {
                sb.Append(" AND AccountID IN (11, 13, 14)");
            }
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

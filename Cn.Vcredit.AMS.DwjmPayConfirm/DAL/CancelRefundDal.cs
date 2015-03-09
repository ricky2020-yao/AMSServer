using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.DataAccess.Common;

namespace Cn.Vcredit.AMS.DwjmPayConfirm.DAL
{
    /// <summary>
    /// 解约退款处理逻辑
    /// </summary>
    public class CancelRefundDal
    {
        /// <summary>
        /// Author:shwang
        /// Date:20140617
        /// Desc:得到解约退款分页数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="pageOption">分页信息</param>
        /// <returns>解约退款数据</returns>
        public List<CancelRefundViewData> GetCancelRefund(CancelRefundFilter filter)
        {
            string payDate = string.Empty;
            if (filter.HasPayDate)
                payDate = " c.PayDate IS NOT NULL ";
            else
                payDate = " c.PayDate IS NULL ";

            StringBuilder conditionSbl = new StringBuilder();

            if (!filter.LendingSideKey.IsNullString())
                conditionSbl.AppendFormat(" AND b.LendingSideKey ='{0}'", filter.LendingSideKey);

            if (filter.BusinessID.HasValue)
                conditionSbl.AppendFormat(" AND c.BusinessID ={0}", filter.BusinessID.Value);

            if (!string.IsNullOrEmpty(filter.ContractNo))
                conditionSbl.AppendFormat(" AND b.ContractNo ='{0}'", filter.ContractNo);

            if (!string.IsNullOrEmpty(filter.CustomerName))
                conditionSbl.AppendFormat(" AND pe.PersonName ='{0}'", filter.CustomerName);

            if (filter.CancelBeginTime.HasValue)
                conditionSbl.AppendFormat(" AND c.CancelTime >='{0}'", filter.CancelBeginTime.Value.ToDateTimeString());

            if (filter.CancelEndTime.HasValue)
                conditionSbl.AppendFormat(" AND c.CancelTime <'{0}'", filter.CancelEndTime.Value.ToDateTimeString());

            string regionJoin = string.Empty;
            if (!string.IsNullOrEmpty(filter.Region))
            {
                regionJoin = @"LEFT JOIN dbo.ConstSysEnum s ON s.super=2619 AND 
                                ((LEN(b.ContractNo)=7 AND s.VALUE='02') OR
                                 (LEN(b.ContractNo)=15 AND SUBSTRING(b.ContractNo,3,2)=s.VALUE) OR 
                                 (LEN(b.ContractNo)=18 AND SUBSTRING(b.ContractNo,6,2)=s.VALUE)
                                )";
                conditionSbl.AppendFormat(" AND s.fullkey in({0})", filter.Region);
            }
            if (filter.PayBeginTime.HasValue)
                conditionSbl.AppendFormat(" AND c.PayDate >='{0}'", filter.PayBeginTime.Value.ToDateTimeString());

            if (filter.PayEndTime.HasValue)
                conditionSbl.AppendFormat(" AND c.PayDate <'{0}'", filter.PayEndTime.Value.ToDateTimeString());

            string pageStr = string.Empty;
            if (filter.PageNo > 0)
            {
                int startNo = (filter.PageNo - 1) * filter.PageSize + 1;
                int endNo = startNo + filter.PageSize - 1;
                pageStr = string.Format("WHERE t.num BETWEEN {0} AND {1} ", startNo, endNo);
            }

            string sqlStr = "SQL\\DwjmPayConfirm\\Select_CancelRefund.sql".ToFileContent(false, payDate, conditionSbl.ToString(), pageStr, regionJoin);
            return DataAccessUtility.GetSearchDataByPageNo<CancelRefundViewData>(filter, sqlStr);
        }
    }
}

using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cn.Vcredit.Common.TypeConvert;
using System.Data;
using Cn.Vcredit.AMS.DataAccess.Common;

namespace Cn.Vcredit.AMS.DwjmPayConfirm.DAL
{
    /// <summary>
    /// 履行担保处理逻辑
    /// </summary>
    public class GuaranteeBatchDal
    {
        /// <summary>
        /// Author:shwang
        /// Date:20140614
        /// Desc:得到担保批次信息
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="pageOption">页面信息</param>
        /// <returns>担保批次信息</returns>
        public List<GuaranteeBatchViewData> GetGuaranteeBatch(GuaranteeBatchFilter filter)
        {
            string payDateCondition;
            if (filter.HasPayDate)
                payDateCondition = "g.PayDate IS NOT NULL";
            else
                payDateCondition = "g.PayDate IS NULL";

            StringBuilder sbl = new StringBuilder();
            if (!string.IsNullOrEmpty(filter.GuaranteeNo))
                sbl.AppendFormat(" AND g.GuaranteeNum='{0}'", filter.GuaranteeNo);

            if (!string.IsNullOrEmpty(filter.Region))
                sbl.AppendFormat(" AND c.fullKey IN({0})", filter.Region);

            if (!string.IsNullOrEmpty(filter.GuaranteeMonth))
                sbl.AppendFormat(" AND g.GuaranteeMonth={0}", filter.GuaranteeMonth.ConvertToInt());

            if (!string.IsNullOrEmpty(filter.ChildCompany))
                sbl.AppendFormat(" AND s.MappingValue='{0}'", filter.ChildCompany);

            if (filter.GuaranteeIndex > 0)
                sbl.AppendFormat(" AND g.GuaranteeIndex={0}", filter.GuaranteeIndex);

            string pageStr = string.Empty;
            if (filter.PageSize > 0)
            {
                int startNo = (filter.PageNo - 1) * filter.PageSize + 1;
                int endNo = startNo + filter.PageSize - 1;
                pageStr = string.Format("WHERE t.num BETWEEN {0} AND {1} ", startNo, endNo);
            }

            string sqlStr = "SQL\\DwjmPayConfirm\\Select_GuaranteeBatch.sql".ToFileContent(false, payDateCondition, sbl.ToString(), pageStr);
            return DataAccessUtility.GetSearchDataByPageNo<GuaranteeBatchViewData>(filter, sqlStr);
        }
    }
}

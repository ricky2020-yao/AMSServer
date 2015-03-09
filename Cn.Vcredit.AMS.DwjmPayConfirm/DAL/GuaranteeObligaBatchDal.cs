using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.DataAccess.Common;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;

namespace Cn.Vcredit.AMS.DwjmPayConfirm.DAL
{
    /// <summary>
    /// 履行担保义务逻辑
    /// </summary>
    public class GuaranteeObligaBatchDal
    {

        /// <summary>
        /// Author:wangzhangming
        /// Date:20140814
        /// Desc:得到外贸担保批次信息
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns>外贸担保批次信息</returns>
        public List<GuaranteeObligaBatchViewData> GetGuaranteeObligationBatch(GuaranteeBatchFilter filter)
        {
            StringBuilder sbl = new StringBuilder();
            // 与需求文档创建者确认过，系统自动生成的外贸担保批次号应从201406月开始算起，
            // 201405月份及更早的担保批次号不认为是系统自动生成的
            if (!string.IsNullOrEmpty(filter.GuaranteeMonth))
                sbl.AppendFormat(" AND g.GuaranteeMonth={0} and g.GuaranteeMonth > 201405 ", filter.GuaranteeMonth.ConvertToInt());

            if (!string.IsNullOrEmpty(filter.Region))
                sbl.AppendFormat(" AND g.Region IN({0})", filter.Region);

            string sqlStr = "SQL\\DwjmPayConfirm\\Select_GuaranteeObligationBatch.sql".ToFileContent(false, sbl.ToString());
            return DataAccessUtility.GetSearchData<GuaranteeObligaBatchViewData>(filter, sqlStr);
           
        }
     
    }
}

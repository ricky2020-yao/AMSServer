using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.EveryDueRepayReportService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月29日
    /// Description:客户查询数据访问层
    /// </summary>
    public class EveryDueRepayReportSearchDAL<T> :
        BaseSearchDAL<T> where T : class, new()
    {
        /// <summary>
        /// 获取指定如何解释命令字符串
        /// </summary>
        /// <returns></returns>
        protected override CommandType GetCommandType()
        {
            return CommandType.StoredProcedure;
        }

        /// <summary>
        /// 获取检索数据的存储过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSpName(BaseFilter baseFilter)
        {
            return "proc_BillDun_GetEveryDueRepayDetailReport";
        }

        /// <summary>
        /// 获取检索数据的存储过程参数列表
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override IDictionary<string, object> GetSearchSpInParams(BaseFilter baseFilter)
        {
            EveryDueRepayReportSearchFilter filter = baseFilter as EveryDueRepayReportSearchFilter;
            if (filter == null)
                return null;

            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            if (filter.BusinessNo != null)
                inPutParam.Add("@BusinessNo", filter.BusinessNo);
            if (filter.ContractNo != null)
                inPutParam.Add("@ContractNo", filter.ContractNo);
            if (filter.ProductType != null)
                inPutParam.Add("@ProductType", filter.ProductType);
            if (filter.OwnRegion != null)
                inPutParam.Add("@OwnRegion", filter.OwnRegion);

            inPutParam.Add("@BeginDate", filter.StartDate);
            inPutParam.Add("@EndDate", filter.EndDate);

            if (filter.StartDays != null)
                inPutParam.Add("@StartDueDays", filter.StartDays);
            if (filter.EndDays != null)
                inPutParam.Add("@EndDueDays", filter.EndDays);

            inPutParam.Add("@FromIndex", filter.FromIndex);
            inPutParam.Add("@EndIndex", filter.ToIndex);

            return inPutParam;
        }

        protected override IDictionary<string, object> GetSearchSpOutParams(BaseFilter baseFilter)
        {
            IDictionary<string, object> outPutParam = new Dictionary<string, object>();
            outPutParam.Add("@TotalCount", 0);

            return outPutParam;
        }
    }
}

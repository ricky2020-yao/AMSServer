using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.Common.TypeConvert;
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
    /// Description:每日逾期静态报表查询数据类
    /// </summary>
    public class EveryDueReportSearchDAL <T> :
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
            return "proc_OverDueStaticByPage";
        }

        /// <summary>
        /// 获取检索数据的存储过程参数列表
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override IDictionary<string, object> GetSearchSpInParams(BaseFilter baseFilter)
        {
            EveryDueReportFilter filter = baseFilter as EveryDueReportFilter;
            if (filter == null)
                return null;

            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(filter.ContractNo))
                inPutParam.Add("@ContractNo", filter.ContractNo);
            if (filter.DunId != 0)
                inPutParam.Add("@DunID", filter.DunId);
            if (!string.IsNullOrEmpty(filter.IdenNo))
                inPutParam.Add("@IdNumber", filter.IdenNo);
            if (!string.IsNullOrEmpty(filter.CustomerName))
                inPutParam.Add("@CustomerName", filter.CustomerName);
            if (!string.IsNullOrEmpty(filter.CurDueSign))
                inPutParam.Add("@TodayOverdueMark", filter.CurDueSign);
            if (!string.IsNullOrEmpty(filter.FirstDueSign))
                inPutParam.Add("@BeginningOverdueMark", filter.FirstDueSign);
            if (!string.IsNullOrEmpty(filter.SaleWay))
                inPutParam.Add("@SaleWay", filter.SaleWay);
            if (!string.IsNullOrEmpty(filter.BusStatus))
                inPutParam.Add("@BusinessStatus", filter.BusStatus);
            if (!string.IsNullOrEmpty(filter.LawitStatus))
                inPutParam.Add("@LitigationStatus", filter.LawitStatus);
            if (!string.IsNullOrEmpty(filter.ProductType))
                inPutParam.Add("@ProductType", filter.ProductType);
            if (!string.IsNullOrEmpty(filter.SignArean))
                inPutParam.Add("@SigningCity", filter.SignArean);
            if (!string.IsNullOrEmpty(filter.ExternalStatus))
                inPutParam.Add("@OutStatus", filter.ExternalStatus);
            if (!string.IsNullOrEmpty(filter.MinDueDays))
                inPutParam.Add("@MinOverdueDays", filter.MinDueDays);
            if (!string.IsNullOrEmpty(filter.MaxDueDays))
                inPutParam.Add("@MaxOverdueDays", filter.MaxDueDays);
            if (!string.IsNullOrEmpty(filter.StartDate))
                inPutParam.Add("@StatisticsDate", filter.StartDate);
            if (filter.IsDue.HasValue)
                inPutParam.Add("@IsDue", filter.IsDue.Value ? 1 : 0);
            if (filter.PageSize != 0)
                inPutParam.Add("@pageSize", filter.PageSize);
            if (filter.PageNo != 0)
                inPutParam.Add("@pageIndex", filter.PageNo);

            return inPutParam;
        }
        /// <summary>
        /// 设置Out参数
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override IDictionary<string, object> GetSearchSpOutParams(BaseFilter baseFilter)
        {
            IDictionary<string, object> outPutParam = new Dictionary<string, object>();
            outPutParam.Add("@pageCount", 0);

            return outPutParam;
        }
        /// <summary>
        /// 获取分页查询总页码
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        public override int GetCount(BaseFilter baseFilter)
        {
            IDictionary<string,object> outPageCountParams = baseFilter.outParams;
            return (int)outPageCountParams["@pageCount"];
        }
    }
}

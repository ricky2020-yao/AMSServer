using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.TimeLineService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月19日
    /// Description:时间轴设置更新序列数据库访问层
    /// </summary>
    public class TimeLineUpdateSeqDAL:BaseUpdateDAL
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
        /// 获取更新数据的存储过程参数列表
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override IDictionary<string, object> GetUpdateSpInParams(BaseFilter baseFilter)
        {
            TimeLineUpdateFilter filter = baseFilter as TimeLineUpdateFilter;

            if (filter == null)
                return null;

            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            inPutParam.Add("@CompanyKey", filter.CompanyKey);
            inPutParam.Add("@UserId", filter.UserId);
            inPutParam.Add("@CurrentMonthString", filter.CurrentMonthString);
            inPutParam.Add("@CurrentMonthFirst", filter.CurrentMonthFirst);
            inPutParam.Add("@CurrentMonthSecond", filter.CurrentMonthSecond);
            inPutParam.Add("@CurrentMonthThird", filter.CurrentMonthThird);
            inPutParam.Add("@NextMonthString", filter.NextMonthString);
            inPutParam.Add("@NextMonthFirst", filter.NextMonthFirst);
            inPutParam.Add("@NextMonthSecond", filter.NextMonthSecond);
            inPutParam.Add("@NextMonthThird", filter.NextMonthThird);
            inPutParam.Add("@MonthAfterNextString", filter.MonthAfterNextString);
            inPutParam.Add("@MonthAfterNextFirst", filter.MonthAfterNextFirst);
            inPutParam.Add("@MonthAfterNextSecond", filter.MonthAfterNextSecond);
            inPutParam.Add("@MonthAfterNextThird", filter.MonthAfterNextThird);
            inPutParam.Add("@LastMonthString", filter.LastMonthString);
            inPutParam.Add("@LastMonthFirst", filter.LastMonthFirst);
            inPutParam.Add("@LastMonthSecond", filter.LastMonthSecond);
            inPutParam.Add("@LastMonthThird", filter.LastMonthThird);

            return inPutParam;
        }

        /// <summary>
        /// 获取检索数据的存储过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetUpdateSpName(BaseFilter baseFilter)
        {
            TimeLineUpdateFilter filter = baseFilter as TimeLineUpdateFilter;

            if (filter == null)
                return "";

            return "proc_FinanceManage_UpdateTimeLineSeq";
        }
    }
}

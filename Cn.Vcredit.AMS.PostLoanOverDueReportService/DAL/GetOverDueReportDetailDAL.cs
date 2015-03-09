using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.ExamineIMP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.PostLoanOverDueReportService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月24日
    /// Description：获取订单的详情信息数据访问层
    /// </summary>
    public class GetOverDueReportDetailDAL<T>:
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
            return "proc_ExamineIMP_GetOverDueReportDetail";
        }

        /// <summary>
        /// 获取检索数据的存储过程参数列表
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override IDictionary<string, object> GetSearchSpInParams(BaseFilter baseFilter)
        {
            OverDueDetailFilter filter = baseFilter as OverDueDetailFilter;
            if (filter == null)
                return null;

            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            inPutParam.Add("@BusinessNo", filter.BussinessNo);
            inPutParam.Add("@ExamineStatus", filter.AuditType);
            inPutParam.Add("@Examiner", filter.AuditPerson);
            inPutParam.Add("@BeginDate", filter.BeginTime);
            inPutParam.Add("@EndDate", filter.EndTime);
            inPutParam.Add("@LoanKind", filter.AuditProductKind);
            inPutParam.Add("@FromIndex", filter.FromIndex);
            inPutParam.Add("@EndIndex", filter.ToIndex);

            return inPutParam;
        }
    }
}

using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.DAL
{
    /// <summary>
    /// Author:Ricky
    /// CreateTime:2014-12-12
    /// Description:导出外贸数据3数据处理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeriveSevForeign3ExportDAL<T>
        : BaseSearchDAL<T> where T : class, new()
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
        /// 返回存贮过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSpName(BaseFilter baseFilter)
        {
            return "proc_FinanceManage_GetTable3ByDWJM";
        }

        /// <summary>
        /// 获取存贮过程输入参数
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>

        protected override IDictionary<string, object> GetSearchSpInParams(BaseFilter baseFilter)
        {
            DeriveSevExportFilter filter = baseFilter as DeriveSevExportFilter;
            if (filter == null)
                return null;

            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            inPutParam.Add("@BeginDate", filter.StartDate);
            inPutParam.Add("@EndDate", filter.EndDate);
            inPutParam.Add("@ServiceSideKey", filter.ServiceSideKey);
            inPutParam.Add("@BranchKeyList", filter.BranchKey);

            return inPutParam;
        }
    }
}

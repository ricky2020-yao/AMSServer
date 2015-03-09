using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.ExportGuaranteeDataService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:订单查询数据处理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExportGuaranteeDataDAL<T>
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
            var filter = baseFilter as ExportGuaranteeDataFilter;
            if (filter == null)
                return "";

            string sqlFile
                = "Services\\SQL\\SELECT_BUSINESS_GUARANTEEDATA.sql"
                .ToFileContent();

            StringBuilder addSQL = new StringBuilder();
            if (!string.IsNullOrEmpty(filter.Region))
                addSQL.Append("AND bus.Region = '" + filter.Region + "'");
            if (!string.IsNullOrEmpty(filter.BranchKey))
                addSQL.Append(" AND bus.BranchKey = '" + filter.BranchKey + "'");
            if (!string.IsNullOrEmpty(filter.Overdue))
                addSQL.Append(" AND list.Overdue = '" + filter.Overdue + "'");

            return string.Format(sqlFile, filter.StartTime, filter.Day, addSQL.ToString());
        }
    }
}

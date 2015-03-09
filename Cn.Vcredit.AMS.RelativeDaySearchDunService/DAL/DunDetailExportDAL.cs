using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.RelativeDaySearchDunService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-19
    /// Description:催收单明细收款导出数据处理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DunDetailExportDAL<T>
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
            var filter = baseFilter as ExportBusinessListFilter;
            if (filter == null)
                return "";

            string sqlFile
                = "Services\\SQL\\DunModuleSQL\\SELECT_DUN_RECEIVED_FORBUSINESSID.sql".ToFileContent();

            return string.Format(sqlFile, filter.BusinessID);
        }
    }
}

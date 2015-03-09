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
    /// Date:2014-12-15
    /// Desc:催收单导出数据逻辑类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelativeDayDunTaskExportDAL<T>
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
            var filter = baseFilter as RelativeDayExportDunFilter;
            if (filter == null)
                return "";

            string fileSql
                = "Services\\SQL\\DunModuleSQL\\SELECT_GetDunListForExport.sql".ToFileContent();

            string addsql = " AND cfg.VBSDunUnitCode in('" +
                    filter.PermissionsUnitKeys + "')";
            addsql = addsql + " AND bus.BranchKey in ('" + filter.BranchKey + "')";

            return string.Format(fileSql, addsql);
        }
    }
}

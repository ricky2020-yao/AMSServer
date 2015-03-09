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
    /// CreateTime:2014-12-15
    /// Description:订单查询数据处理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelativeDayDunSearchDAL<T>
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
            var filter = baseFilter as RelativeDaySearchDunFilter;
            if (filter == null)
                return "";

            string sqlFilePath 
                = "Services\\SQL\\DunModuleSQL\\SELECT_GetDunListForRelativeDay.sql".ToFileContent();

            StringBuilder ExtSql = new StringBuilder();

            //门店催收
            if (filter.IsDunShop)
                ExtSql.Append("AND dun.OverMonth = 1");

            //客维催收
            if (filter.IsDunCustomer)
            {
                if (ExtSql.Length > 0)
                    ExtSql.Clear().Append(" AND (dun.OverMonth = 1 OR dun.OverMonth > 1)");
                else
                    ExtSql.Append(" AND dun.OverMonth > 1");
            }

            //能查看的权限
            if (!string.IsNullOrEmpty(filter.BranchKey))
                ExtSql.Append(" AND bus.BranchKey IN (" + filter.BranchKey + ")");

            //催收单号
            if (filter.DunID != 0)
                ExtSql.Append(" AND dun.DunID = " + filter.DunID);
            // 业务号
            if (filter.DunID != 0)
                ExtSql.Append(" AND bus.BusinessID = " + filter.BusinessID);
            //合同号
            if (!string.IsNullOrEmpty(filter.ContractNo))
                ExtSql.Append(" AND bus.ContractNo = '" + filter.ContractNo + "'");

            // 催收员
            if (!string.IsNullOrEmpty(filter.Duner))
                //催收员
                ExtSql.Append(" and u.Name Like '" + filter.Duner + "%'");

            //催收员对应催收单序号
            if (filter.DunNumber != 0)
                ExtSql.Append(" AND dun.DunNumber =" + filter.DunNumber);

            //订单状态
            if (filter.BusinessStatus != 0)
                ExtSql.Append(" AND bus.BusinessStatus =" + filter.BusinessStatus);

            //诉讼状态
            if (filter.LawsuitStatus != 0)
                ExtSql.Append(" AND bus.LawsuitStatus =" + filter.LawsuitStatus);

            //放贷方
            if (!string.IsNullOrEmpty(filter.LendingSideKey))
                ExtSql.Append(" AND bus.LendingSideKey = '" + filter.LendingSideKey + "'");

            //客户姓名
            if (!string.IsNullOrEmpty(filter.CustomerName))
                ExtSql.Append(" AND per.PersonName = '" + filter.CustomerName + "'");

            //委外时间
            if (!string.IsNullOrEmpty(filter.OutSourceTime))
                ExtSql.Append(" AND dun.OutSourceTime = '" + filter.OutSourceTime + "'");

            //客户姓名
            if (!string.IsNullOrEmpty(filter.CustomerName))
                ExtSql.Append(" AND per.PersonName = '" + filter.CustomerName + "'");
            // 期初逾期状态
            //if (filter.OverMonth != 0)
            //    ExtSql.Append(" AND dun.OverMonth =" + filter.OverMonth);

            // 产品类型
            if (!string.IsNullOrEmpty(filter.ProductKind))
                ExtSql.Append(" AND bus.ProductKind IN (" + filter.ProductKind + ")");

            // 是否是当前催收单
            if (filter.IsCurrent)
                ExtSql.Append(" AND dun.IsClosed = 0");

            #region PCR012
            /*========================PCR012=======================*/
            //wichell 2014年10月13日 10:10:29
            StringBuilder labelandtrackBuilder = new StringBuilder();
            //最后跟进时间
            StringBuilder trackBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(filter.LastTrackTime))
                labelandtrackBuilder.AppendFormat(" AND CAST(#tracktmp.LastTrackTime AS date)='{0}'", filter.LastTrackTime);
            //门店
            if (!string.IsNullOrEmpty(filter.Store))
                ExtSql.Append(" AND bus.BranchKey = '" + filter.Store + "'");
            //城市
            if (!string.IsNullOrEmpty(filter.Region))
                ExtSql.Append(" AND bus.Region = '" + filter.Region + "'");
            //催收单位
            if (!string.IsNullOrEmpty(filter.DunUnit))
                ExtSql.Append(" AND cfg.VBSDunUnitCode = '" + filter.DunUnit + "'");

            //逾期天数开始
            if (!string.IsNullOrEmpty(filter.StartDays))
                ExtSql.Append(" AND dun.BeginningDueDays >= " + filter.StartDays);
            //逾期天数结束
            if (!string.IsNullOrEmpty(filter.EndDays))
                ExtSql.Append(" AND dun.BeginningDueDays <= " + filter.EndDays);

            if (!string.IsNullOrEmpty(filter.PersonID))
                ExtSql.Append(" AND info.PersonID = " + filter.PersonID);

            //标签类型
            StringBuilder labelBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(filter.LabelType))
            {
                labelBuilder.Append(" AND label.LabelCode='" + filter.LabelType + "'");
                labelandtrackBuilder.Append(" AND #labeltmp.LabelCode = '" + filter.LabelType + "'");
            }

            //催收单位【权限】
            if (!string.IsNullOrEmpty(filter.PermissionsUnitKeys))
            {
                ExtSql.Append(" AND cfg.VBSDunUnitCode IN ('" + filter.PermissionsUnitKeys + "')");
            }

            //结果代码
            StringBuilder dunCodeBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(filter.DunCode))
            {
                labelandtrackBuilder.AppendFormat(" AND (#codetemp.VaildCode = '{0}' OR #codetemp.UnVaildCode = '{0}')", filter.DunCode);
            }
            #endregion
            /*=====================================================*/

            string tracksql = trackBuilder.ToString();
            string dunlabelsql = labelBuilder.ToString();
            string labelandtracksql = labelandtrackBuilder.ToString();

            int fromIndex = (filter.PageNo - 1) * filter.PageSize + 1;
            int toIndex = filter.PageNo * filter.PageSize;

            string pageingSql = " AND #finalResult.RowID BETWEEN " + fromIndex + " AND " + toIndex;
            return string.Format(sqlFilePath, ExtSql.ToString(), pageingSql, tracksql, dunlabelsql, labelandtracksql);
        }
    }
}
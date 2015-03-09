using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.DAL
{
    /// <summary>
    /// 清贷申请记录表查询数据逻辑类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CloanApplyDetailDAL<T>
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

            var filter = baseFilter as SearchBusinessListFilter;
            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT CloanApplyID");
            sb.Append("       ,BusinessID");
            sb.Append("       ,UnexpiredMonth");
            sb.Append("       ,OverMonth");
            sb.Append("       ,Path");
            sb.Append("       ,CloanApplyKind");
            sb.Append("       ,CloanApplyStatus");
            sb.Append("       ,ApplyerID");
            sb.Append("       ,CheckerID");
            sb.Append("       ,ApplyTime");
            sb.Append("       ,CheckTime");
            sb.Append(" FROM dbo.CloanApply WITH (NOLOCK)");
            sb.Append(" WHERE BusinessID = " + filter.BusinessID);
            sb.Append("   AND CloanApplyStatus = " + (byte)EnumCloanApplyStatus.Pass);
            sb.Append(" ORDER BY CloanApplyID");

            return sb.ToString();
        }
    }
}

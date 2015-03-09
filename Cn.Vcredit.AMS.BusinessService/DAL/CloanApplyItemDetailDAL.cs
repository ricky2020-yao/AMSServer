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
    /// 清贷申请记录明细表查询数据逻辑类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CloanApplyItemDetailDAL<T>
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
            var filter = baseFilter as PayAccountFilter;
            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT CloanApplyItemID");
            sb.Append("       ,CloanApplyID");
            sb.Append("       ,Subject");
            sb.Append("       ,IsAnnul");
            sb.Append("       ,Amount");
            sb.Append("       ,CreateTime");
            sb.Append(" FROM dbo.CloanApplyItem WITH (NOLOCK)");
            sb.Append(" WHERE CloanApplyID = " + filter.CloanApplyID);
            sb.Append(" ORDER BY CloanApplyItemID");

            return sb.ToString();
        }
    }
}

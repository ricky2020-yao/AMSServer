using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.XtgJhzkCheckService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月01日
    /// Description:信托归集户账款核对清除到账日期数据访问层
    /// </summary>
    public class ClearXtgJhzkToAcountTimeDAL:BaseUpdateDAL
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
        /// 获取检索数据的存储过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetUpdateSql(BaseFilter baseFilter)
        {
            XtgJhzkCheckFilter filter = baseFilter as XtgJhzkCheckFilter;

            if (filter == null)
                return "";

            if (string.IsNullOrEmpty(filter.ReceivedIDs))
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE dbo.Received ");
            sb.Append(" SET ToAcountTime = null");
            sb.Append("      , OperatorID =" + filter.OperatorID);
            sb.Append(" WHERE ReceivedID IN (" + filter.ReceivedIDs + ")");

            return sb.ToString();
        }
    }
}

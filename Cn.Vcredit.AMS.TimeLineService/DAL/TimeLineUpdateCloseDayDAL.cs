using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.Tools;
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
    /// Description:时间轴设置更新关帐日数据访问层
    /// </summary>
    public class TimeLineUpdateCloseDayDAL:BaseUpdateDAL
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
            TimeLineUpdateFilter filter = baseFilter as TimeLineUpdateFilter;

            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE [dbo].[CloseBillDay] ");
            sb.Append(" SET OriginalTime = '{0}' ");
            sb.Append("     , LatestTime = '{1}' ");
            sb.Append("     , CreateTime = GETDATE()");
            sb.Append("     , OperatorID = {2} ");
            //sb.Append("     , Updatetime = GETDATE() ");
            sb.Append("  WHERE CloseBillDayID = {3} ");

            return sb.ToString().StringFormat(filter.OriginalTime
                , filter.LatestTime, filter.UserId, filter.CloseBillDayID);
        }
    }
}

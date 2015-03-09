using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
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
    /// Description:时间轴设置关帐日初始化数据库访问层
    /// </summary>
    public class TimeLineCloseDayInitDAL : BaseSearchDAL<CloseBillDay>
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
        protected override string GetSearchSql(BaseFilter baseFilter)
        {
            return "SELECT * FROM [dbo].[CloseBillDay] with(nolock) ORDER BY CloseBillDayID";
        }
    }
}

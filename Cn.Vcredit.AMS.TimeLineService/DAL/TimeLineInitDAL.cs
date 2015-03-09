using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.DAL;
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
    /// Description:时间轴设置数据初始化数据库访问层
    /// </summary>
    public class TimeLineInitDAL : BaseSearchDAL<DeductSequence>
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
            TimeLineInitFilter filter = baseFilter as TimeLineInitFilter;
                if(filter == null)
                    return "";

            string sql = "SELECT * FROM [dbo].[DeductSequence] with(nolock) WHERE DeductMonth = '{0}' "
                + "AND DSeqType = {1} AND CompanyKey = '{2}'  ORDER BY DeductSeqID ASC"
            .StringFormat(filter.DeductMonth, (byte)filter.Kind, filter.CompanyKey);

            return sql;
        }
    }
}

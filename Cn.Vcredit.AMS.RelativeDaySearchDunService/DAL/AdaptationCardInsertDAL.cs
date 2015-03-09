using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
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
    /// Description:代偿卡新增数据处理层
    /// </summary>
    public class AdaptationCardInsertDAL:BaseUpdateDAL
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
        /// 获取保存数据的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetUpdateSql(Entity.Filter.BaseFilter baseFilter)
        {
            var filter = baseFilter as AdaptationCardFilter;
            if (filter == null)
                return "";

            return @" INSERT INTO [dbo].[AdaptationCard] (
                    CardNo,CardUser,AdaBankName,ValidPath,ValidEndTime,
                    AdaDesc,ValidName,BusinessID,AdaName, CreateTime ) VALUES ( '"
                + filter.CardNo + "','"
                + filter.CardUser + "','"
                + filter.AdaBankName + "','"
                + filter.ValidPath + "','"
                + filter.ValidEndTime + "','"
                + filter.AdaDesc + "','"
                + filter.ValidName + "',"
                + filter.BusinessID + ",'"
                + filter.AdaName + "','"
                + filter.CreateTime + "' )";
        }
    }
}

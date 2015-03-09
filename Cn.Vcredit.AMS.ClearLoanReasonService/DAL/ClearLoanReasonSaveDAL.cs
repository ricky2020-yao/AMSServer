using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.ClearLoanReasonService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:清贷原因设置保存数据处理类
    /// </summary>
    public class ClearLoanReasonSaveDAL : BaseUpdateDAL
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
            var filter = baseFilter as ClearLoanReasonSaveFilter;
            if (filter == null)
                return "";

            string sql = @" UPDATE dbo.Business 
                        SET ClearLoanType = '" + filter.ClearLoanType + "'";
            sql += " ,ClearLoanRemark = '" + filter.ClearLoanRemark + "'";
            sql += " WHERE BusinessID = " + filter.BusinessId;

            return sql;
        }
    }
}

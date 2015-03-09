using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Cn.Vcredit.Common.TypeConvert;

namespace Cn.Vcredit.AMS.InterestFitService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年8月13日
    /// Description:更新代偿款支付剩余本息支付日数据处理类
    /// </summary>
    public class InterestFitUpdateDAL:BaseUpdateDAL
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
        /// 获取更新代偿款支付剩余本息支付日Sql
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetUpdateSql(Entity.Filter.BaseFilter baseFilter)
        {
            var filter = baseFilter as InterestFitFilter;
            if (filter == null)
                return "";

            string sql = "Update dbo.Business Set PaymentDate = ";

            if (!string.IsNullOrEmpty(filter.PaymentDate))
                sql += "'" + filter.PaymentDate.ToDateTime() + "'";
            else sql += " NULL";

            sql += " WHERE ContractNo IN ('" + filter.ContractNos + "')";

            return sql;
        }
    }
}

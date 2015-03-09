using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.FulfilGuaranteeService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-10
    /// Description:履行担保批次保存服务数据访问层
    /// </summary>
    public class FulfilGuaranteeSaveDAL:BaseUpdateDAL
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
            var filter = baseFilter as GuaranteeBatchUpdateFilter;

            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE dun.GuaranteeBatchPay ");
            sb.Append("    SET PayDate = '" + filter.PayDate + "'");
            sb.Append("      , ReceivedDate = '" + filter.ReceivedDate + "'");
            sb.Append("      , PayType = " + filter.PayType);
            sb.Append(" WHERE GuaranteeNum IN ('" + filter.GuaranteeNums + "')");

            return sb.ToString();
        }
    }
}

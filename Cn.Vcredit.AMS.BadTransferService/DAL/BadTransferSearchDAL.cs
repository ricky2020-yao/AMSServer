using Cn.Vcredit.AMS.BadTransferService.Data.ViewData;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BadTransferService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月19日
    /// Description:坏账清贷检索数据访问层
    /// </summary>
    public class BadTransferSearchDAL<T>
        :BaseSearchDAL<T> where T:class, new()
    {
        /// <summary>
        /// 获取指定如何解释命令字符串
        /// </summary>
        /// <returns></returns>
        protected override CommandType GetCommandType()
        {
            return CommandType.StoredProcedure;
        }

        /// <summary>
        /// 获取检索数据的存储过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSpName(BaseFilter baseFilter)
        {
            return "proc_FinanceManage_GetBadTransferInfor";
        }

        /// <summary>
        /// 获取检索数据的存储过程参数列表
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override IDictionary<string, object> GetSearchSpInParams(BaseFilter baseFilter)
        {
            BadTransferFilter filter = baseFilter as BadTransferFilter;
            if (filter == null)
                return null;

            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            inPutParam.Add("@BusinessID", filter.BusinessID);
            inPutParam.Add("@ContractNo", filter.ContractNo);
            inPutParam.Add("@CustomerName", filter.CustomerName);
            inPutParam.Add("@IdenNo", filter.IdenNo);

            return inPutParam;
        }
    }
}

using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.SavingCardChangeService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月21日
    /// Description:储蓄卡变更获取出未清贷的业务信息数据层操作类
    /// </summary>
    public class SavingCardChangeDAL<T>
        : BaseSearchDAL<T> where T : class, new()
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
            return "proc_BillDun_GetNoCleanLoanBusiness";
        }

        /// <summary>
        /// 获取检索数据的存储过程参数列表
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override IDictionary<string, object> GetSearchSpInParams(BaseFilter baseFilter)
        {
            SavingCardChangeFilter filter = baseFilter as SavingCardChangeFilter;
            if (filter == null)
                return null;

            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            if (filter.CustomerName != null)
                inPutParam.Add("@CustomerName", filter.CustomerName);
            if (filter.IdenNO != null)
                inPutParam.Add("@IdenNo", filter.IdenNO);
            if (filter.ContractNo != null)
                inPutParam.Add("@ContractNo", filter.ContractNo);
            if (filter.BusinessId != null)
                inPutParam.Add("@BusinessNo", filter.BusinessId);
            inPutParam.Add("@Type", filter.AuditType);
            inPutParam.Add("@FromIndex", filter.FromIndex);
            inPutParam.Add("@EndIndex", filter.ToIndex);
            if (filter.BranchKeyList != null)
                inPutParam.Add("@BranchKeyList", filter.BranchKeyList);

            return inPutParam;
        }
    }
}

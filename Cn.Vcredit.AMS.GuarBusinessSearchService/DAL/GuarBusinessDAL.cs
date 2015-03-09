using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cn.Vcredit.AMS.GuarBusinessSearchService.DAL
{
    /// <summary>
    /// 入担保查询的数据访问层
    /// </summary>
    public class GuarBusinessSearchDAL : BaseSearchDAL<GuarBusinessViewData>
    {
        /// <summary>
        /// 重写CommandType为StoreProcedure
        /// </summary>
        /// <returns></returns>
        protected override System.Data.CommandType GetCommandType()
        {
            return CommandType.StoredProcedure;
        }


        /// <summary>
        /// 重写SP名称
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSpName(Entity.Filter.BaseFilter baseFilter)
        {
            return "proc_Dun_QueryGuarBusiness";
        }

        /// <summary>
        /// 重写SP入参
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override System.Collections.Generic.IDictionary<string, object> GetSearchSpInParams(Entity.Filter.BaseFilter baseFilter)
        {
            IDictionary<string, object> dicParams = new Dictionary<string, object>();

            QueryGuarBusinessFilter filter = baseFilter as QueryGuarBusinessFilter;
            if (null == filter)
                throw new Exception("入参Filter格式转换失败");

            dicParams.Add("@Region", filter.Region);
            dicParams.Add("@BranchKey", filter.Branch);
            dicParams.Add("@TeamName", filter.Team);
            dicParams.Add("@ProductKind", filter.BusinessKind);
            dicParams.Add("@CustomerType", filter.CustomerType);
            dicParams.Add("@StartLoanTime", filter.LoanStartTime);
            dicParams.Add("@EndLoanTime", filter.LoanEndTime);
            dicParams.Add("@StartGuarTime", filter.GuarStartTime);
            dicParams.Add("@EndGuarTime", filter.GuarEndTime);
            dicParams.Add("@OverMonth", filter.OverDueStatus);
            dicParams.Add("@PaidStatus", filter.ReceiveStauts);
            dicParams.Add("@PageIndex", filter.PageNo);
            dicParams.Add("@PageSize", filter.PageSize);

            return dicParams;
        }
    }
}

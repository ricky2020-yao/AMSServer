using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter.CustomerService;
using Cn.Vcredit.AMS.Entity.ViewData.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.DeductRemindService.DAL
{
    /// <summary>
    /// 首月还款提醒业务逻辑层
    /// </summary>
    public class DeductRemindExportDAL: BaseSearchDAL<DeductRemindExportViewData>
    {
        protected override System.Data.CommandType GetCommandType()
        {
            return System.Data.CommandType.StoredProcedure;
        }

        protected override string GetSearchSpName(Entity.Filter.BaseFilter baseFilter)
        {
            return "proc_Customer_QueryDeductRemind";
        }

        protected override IDictionary<string, object> GetSearchSpInParams(Entity.Filter.BaseFilter baseFilter)
        {
            DeductRemindExportFilter filter = baseFilter as DeductRemindExportFilter;

            if (null == filter)
                return null;

            IDictionary<string,object> dicParams = new Dictionary<string,object>();
            dicParams.Add("@LoanTimeBegin", filter.LoanTimeBegin);
            dicParams.Add("@LoanTimeEnd", filter.LoanTimeEnd);
            dicParams.Add("@SaleModel", filter.SaleModel);
            dicParams.Add("@ProductKind", filter.ProductKind);
            dicParams.Add("@BranchKeyList", filter.BranchKeyList);

            return dicParams;
        }
    }
}

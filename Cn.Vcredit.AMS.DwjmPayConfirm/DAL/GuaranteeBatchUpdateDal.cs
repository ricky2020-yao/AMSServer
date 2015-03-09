using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
namespace Cn.Vcredit.AMS.DwjmPayConfirm.DAL
{
    /// <summary>
    /// 更新担保批次付款数据
    /// </summary>
    public class GuaranteeBatchUpdateDal : BaseUpdateDAL
    {
        /// <summary>
        /// 更新语句
        /// </summary>
        /// <param name="baseFilter">过滤条件</param>
        /// <returns>更新语句</returns>
        protected override string GetUpdateSql(BaseFilter baseFilter)
        {
            GuaranteeBatchUpdateFilter filter = baseFilter as GuaranteeBatchUpdateFilter;
            if(filter==null)
                return null;

            return "SQL\\DwjmPayConfirm\\Update_GuaranteeBatchPay.sql".ToFileContent(false,
                filter.PayDate.ToDateTimeString(),filter.ReceivedDate.ToDateTimeString(),
                filter.PayType,filter.GuaranteeNums);
        }
    }
}

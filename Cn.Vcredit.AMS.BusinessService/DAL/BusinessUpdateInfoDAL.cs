using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.DAL
{
    /// <summary>
    /// 订单详情业务-自动减免数据库访问层
    /// </summary>
    public class BusinessUpdateInfoDAL:BaseUpdateDAL
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
        /// 获取更新数据库存储过程的参数
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override IDictionary<string, object> GetUpdateSpInParams(BaseFilter baseFilter)
        {
            SearchBusinessListFilter filter = baseFilter as SearchBusinessListFilter;
            if (filter == null)
                return null;

            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            inPutParam.Add("@BusinessNo", filter.BusinessID);
            inPutParam.Add("@ContractNo", filter.ContractNo);
            inPutParam.Add("@BranchKey", filter.BranchKey);

            return inPutParam;
        }

        /// <summary>
        /// 获取更新数据的存储过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetUpdateSpName(BaseFilter baseFilter)
        {
            return "pro_Business_UpdateForBusinessID";
        }
    }
}

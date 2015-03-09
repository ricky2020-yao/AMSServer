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
    /// 订单详情业务-保存调整款项数据库访问层
    /// </summary>
    public class BusinessSaveDAL : BaseUpdateDAL
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
            PayAccountFilter filter = baseFilter as PayAccountFilter;
            if (filter == null)
                return null;

            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            inPutParam.Add("@BusinessID", filter.BusinessID);
            inPutParam.Add("@Guid", filter.Guid);

            return inPutParam;
        }

        /// <summary>
        /// 获取更新数据的存储过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetUpdateSpName(BaseFilter baseFilter)
        {
            return "pro_Business_UpdateForBusinessSave";
        }
    }
}

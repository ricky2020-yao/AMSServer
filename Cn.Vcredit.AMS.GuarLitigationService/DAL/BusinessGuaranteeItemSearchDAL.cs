using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.GuarLitigationService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年10月20日
    /// Description:担保和诉讼设置导出明细数据类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessGuaranteeItemSearchDAL<T> :
        BaseSearchDAL<T> where T : class, new()
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
        /// 获取存储过程的名称
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSpName(Entity.Filter.BaseFilter baseFilter)
        {
            return "proc_Dun_GetBHGuaranteeAmount";
        }

        /// <summary>
        /// 获取存贮过程的输入参数
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override IDictionary<string, object> GetSearchSpInParams(Entity.Filter.BaseFilter baseFilter)
        {
            BusinessGuaranteeFilter filter = baseFilter as BusinessGuaranteeFilter;
            if (filter == null)
                return null;

            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            inPutParam.Add("@BusinessID", filter.BusinessID);

            return inPutParam;
        }
    }
}

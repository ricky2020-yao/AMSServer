using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.SavingCardChangeService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月29日
    /// Description:获取储蓄卡审核的信息数据层操作类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetSavingCardAuditInfoDAL<T>
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
            return "proc_BillDun_GetSavingCardAuditInfo";
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
            inPutParam.Add("@BusinessNo", filter.BusinessId);

            return inPutParam;
        }
    }
}

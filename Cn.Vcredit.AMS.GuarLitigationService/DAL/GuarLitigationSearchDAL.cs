using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
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
    /// Description:担保和诉讼设置查询数据类
    /// </summary>
    public class GuarLitigationSearchDAL<T> :
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
        /// 获取检索数据的存储过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSpName(BaseFilter baseFilter)
        {
            return "proc_BillDun_BusinessGuaranteeByPage";
        }

        /// <summary>
        /// 获取检索数据的存储过程参数列表
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override IDictionary<string, object> GetSearchSpInParams(BaseFilter baseFilter)
        {
            BusinessGuaranteeFilter filter = baseFilter as BusinessGuaranteeFilter;
            if (filter == null)
                return null;

            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(filter.CustomerName))
                inPutParam.Add("@CustomerName", filter.CustomerName);
            if (!string.IsNullOrEmpty(filter.IdenNo))
                inPutParam.Add("@IdNumber", filter.IdenNo);
            if (!string.IsNullOrEmpty(filter.ContractNo))
                inPutParam.Add("@ContractNo", filter.ContractNo);
            if (!string.IsNullOrEmpty(filter.OverdueTag))
                inPutParam.Add("@OverdueTag", filter.OverdueTag);
            if (!string.IsNullOrEmpty(filter.GuaranteeSideKey))
                inPutParam.Add("@GuaranteeSideKey", filter.GuaranteeSideKey);
            if (filter.BusinessStatus > 0)
                inPutParam.Add("@BusinessStatus", filter.BusinessStatus);
            if (!string.IsNullOrEmpty(filter.ServiceSideKey))
                inPutParam.Add("@ServiceSideKey", filter.ServiceSideKey);
            if (!string.IsNullOrEmpty(filter.LendingSideKey))
                inPutParam.Add("@LendingSideKey", filter.LendingSideKey);
            if (!string.IsNullOrEmpty(filter.GuaranteeNum))
                inPutParam.Add("@GuaranteeNum", filter.GuaranteeNum);
            if (filter.CapitalCnt > 0)
                inPutParam.Add("@CapitalCnt", filter.CapitalCnt);
            if (!string.IsNullOrEmpty(filter.ToGuaranteeFromTime))
                inPutParam.Add("@ToGuaranteeFromTime", filter.ToGuaranteeFromTime);
            if (!string.IsNullOrEmpty(filter.ToGuaranteeToTime))
                inPutParam.Add("@ToGuaranteeToTime", filter.ToGuaranteeToTime);
            if (!string.IsNullOrEmpty(filter.CompanyKeys))
                inPutParam.Add("@CompanyKeys", filter.CompanyKeys);
            // 需要分页
            if (filter.IsPage)
            {
                inPutParam.Add("@FromIndex", filter.FromIndex);
                inPutParam.Add("@ToIndex", filter.ToIndex);
            }
            return inPutParam;
        }

        /// <summary>
        /// 设置Out参数
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override IDictionary<string, object> GetSearchSpOutParams(BaseFilter baseFilter)
        {
            IDictionary<string, object> outPutParam = new Dictionary<string, object>();
            outPutParam.Add("@PageCount", 0);

            return outPutParam;
        }

        /// <summary>
        /// 获取分页查询总页码
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        public override int GetCount(BaseFilter baseFilter)
        {
            IDictionary<string, object> outPageCountParams = baseFilter.outParams;
            return (int)outPageCountParams["@PageCount"];
        }
    }
}

using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.GuarLitigationService.DAL
{
    public class GuarLitigationManualDAL:BaseUpdateDAL
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
        /// 获取检索数据的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetUpdateSpName(BaseFilter baseFilter)
        {
            return "proc_Dun_ToGuraranteeByManual";
        }

        /// <summary>
        /// 获取更新存贮过程的入参
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override IDictionary<string, object> GetUpdateSpInParams(BaseFilter baseFilter)
        {
            BusinessGuaranteeSaveFilter filter = baseFilter as BusinessGuaranteeSaveFilter;
            if (filter == null)
                return null;

            IDictionary<string, object> inPutParam = new Dictionary<string, object>();
            inPutParam.Add("@BusinessID", filter.BusinessID);
            inPutParam.Add("@GuaranteeNo", filter.GuaranteeNo);
            inPutParam.Add("@GuaranteeTime", filter.GuaranteeTime);
            inPutParam.Add("@CapitalHasBeen", filter.CapitalHasBeen);
            inPutParam.Add("@CapitalNotBeen", filter.CapitalNotBeen);
            inPutParam.Add("@CapitalLost", filter.CapitalLost);
            inPutParam.Add("@Interest", filter.Interest);
            inPutParam.Add("@InterestNotBeen", filter.InterestNotBeen);
            inPutParam.Add("@PenaltyInt", filter.PenaltyInt);
            return inPutParam;
        }
    }
}

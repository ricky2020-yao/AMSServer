using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter.Common;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.DictionaryService.DAL
{
    public class QueryEnumerationDAL : BaseSearchDAL<EnumerationViewData>
    {
        protected override System.Data.CommandType GetCommandType()
        {
            return System.Data.CommandType.StoredProcedure;
        }

        protected override string GetSearchSpName(Entity.Filter.BaseFilter baseFilter)
        {
            return "proc_Common_GetEnumeration";
        }

        protected override IDictionary<string, object> GetSearchSpInParams(Entity.Filter.BaseFilter baseFilter)
        {
            QueryEnumerationFilter filter = baseFilter as QueryEnumerationFilter;

            if(null != filter)
            {
                IDictionary<string, object> dicParameter = new Dictionary<string, object>();

                dicParameter.Add("@FullKey", filter.FullKey);
                dicParameter.Add("@EnumID", filter.EnumID);
                dicParameter.Add("@GetEnumType", filter.GetEnumType);

                return dicParameter;
            }
            return null;
        }
    }
}

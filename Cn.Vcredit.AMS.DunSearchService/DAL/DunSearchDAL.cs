using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DunSearchService
{
    public class DunSearchDAL : BaseSearchDAL<DunSearchDAL>
    {
        protected override System.Data.CommandType GetCommandType()
        {
            return System.Data.CommandType.StoredProcedure;
        }

        protected override string GetSearchSql(Entity.Filter.BaseFilter baseFilter)
        {
            string sql = string.Empty;
            return base.GetSearchSql(baseFilter);
        }
    }
}

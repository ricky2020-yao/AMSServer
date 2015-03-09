using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DunSearchService
{
    public class DunSearchBLL : BaseService.BLL.BaseSearchBLL<DunSearchFilter, DunSearchDAL>
    {
        public void Search()
        { 
            base.SearchData(base.f)
            //base.SearchDataPagingByFilter()
        }
    }
}

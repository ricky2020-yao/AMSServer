using Cn.Vcredit.AMS.BaseService.Entity;
using Cn.Vcredit.AMS.Entity.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.AMS.GuarBusinessSearchService.DAL;
using Cn.Vcredit.AMS.BaseService.Entity.ReponseResult;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.BaseService.BLL;

namespace Cn.Vcredit.AMS.GuarBusinessSearchService.BLL
{
    /// <summary>
    /// 入担保查询的业务逻辑层
    /// </summary>
    public class GuarBusinessBLL : BaseSearchBLL<GuarBusinessViewData, GuarBusinessSearchDAL>
    {
        /// <summary>
        /// 查询入担保的订单信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public void QueryGuarBusiness(BaseFilter filter,ResponseEntity responseEntity)
        {
            var guarBusList = Singleton<GuarBusinessSearchDAL>.Instance.SearchData(filter);

            var responseResult = new ResponseListResult<GuarBusinessViewData>();
            responseResult.LstResult = guarBusList;

            responseEntity.ResponseStatus = (int)EnumResponseState.Success;
            responseEntity.Results = responseResult;
        }
    }
}

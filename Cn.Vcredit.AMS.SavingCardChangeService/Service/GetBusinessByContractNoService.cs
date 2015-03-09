using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.SavingCardChangeService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.SavingCardChangeService.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月29日
    /// Description:根据合同号查询业务信息服务
    /// </summary>
    [Description("根据合同号查询业务信息服务")]
    public class GetBusinessByContractNoService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            SavingCardChangeFilter filter
                = ServiceUtility.ConvertToFilterFromDict<SavingCardChangeFilter>(requestEntity.Parameters);

            // 根据合同号查询业务信息
            Singleton<BaseSearchBLL<SavingCardChangeBusinessViewData
                , GetBusinessByContractNoDAL<SavingCardChangeBusinessViewData>>>.Instance.SearchData(filter, responseEntity);

        }
    }
}

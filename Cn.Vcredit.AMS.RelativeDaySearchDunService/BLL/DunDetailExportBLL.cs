using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.RelativeDaySearchDunService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.RelativeDaySearchDunService.BLL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-12-19
    /// Desc:催收单明细收款导出业务处理类
    /// </summary>
    public class DunDetailExportBLL:BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void ExportData(BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            var filter = baseFilter as ExportBusinessListFilter;
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            if (filter.IsFilterBranchKeys)
            {
                List<string> stores = Singleton<RedisEnumOperatorBLL>.Instance.GetUserOwnStoreKeys(responseEntity.UserId);
                filter.BranchKeys = string.Join("','", stores.ToArray());
            }
            var lstBusiness
                = Singleton<DunBusinessDetailDAL<BusinessDetailViewData>>.Instance.SearchData(filter);
            if (lstBusiness == null || lstBusiness.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult, "无收款记录!");
                m_Logger.Info("无收款记录!");
            }

            var business = lstBusiness[0];

            var lstDunReceived
                = Singleton<DunDetailExportDAL<DunDetailReceiveExportViewData>>.Instance.SearchData(filter);
            if (lstDunReceived == null || lstDunReceived.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult, "无收款记录!");
                m_Logger.Info("无收款记录!");
            }
            else
            {
                lstDunReceived.ForEach(x =>
                    {
                        x.ReceivedTypeName = x.ReceivedType.ValueToDesc<EnumAdjustKind>();
                        x.LoanTime = x.LoanTime.Date;
                        x.ReceivedTime = x.ReceivedTime.Date;
                        x.SavingCard = business.SavingCard;
                        x.CustomerName = business.CustomerName;
                    });
                // 设置输出文件
                SetExportFile(filter, lstDunReceived, responseEntity);
            }
        }
    }
}

using Aspose.Cells;
using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.DataExportService.DAL;
using Cn.Vcredit.AMS.DataExportService.Data;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using Cn.Vcredit.Components.ExcelExport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月14日
    /// Description:订单筛选导出数据处理类
    /// </summary>
    public class BusinesssDataExportBLL:BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            BusinessExportFilter filter = baseFilter as BusinessExportFilter;
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            var reportDetailList 
                = Singleton<BusinesssDataExportDAL<BusinessExportViewData>>
                .Instance.SearchData(baseFilter);

            if (reportDetailList == null || reportDetailList.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                var lstViewBusiness = CreateViewBusinessExtPivot(reportDetailList);
                // 设置输出文件
                SetExportFile(filter, lstViewBusiness, responseEntity);
            }
        }

        /// <summary>
        /// 创建ViewBusinessExtPivot列表视图
        /// </summary>
        /// <param name="lstViewData">催收单信息</param>
        /// <returns>返回催收单视图</returns>
        private List<ViewBusinessExtPivot> CreateViewBusinessExtPivot(List<BusinessExportViewData> lstViewData)
        {
            List<ViewBusinessExtPivot> lstViewBusiness = new List<ViewBusinessExtPivot>();
            ViewBusinessExtPivot viewBusiness = null;
            foreach (var business in lstViewData)
            {
                viewBusiness = new ViewBusinessExtPivot();
                viewBusiness.BusinessID = business.BusinessID;
                viewBusiness.ContractNo = business.ContractNo;
                viewBusiness.CustomerName = business.CustomerName;
                viewBusiness.SavingCard = business.SavingCard;
                viewBusiness.BankName = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_BankList_8, business.BankKey).Name;
                viewBusiness.LoanPeriod = business.LoanPeriod;
                viewBusiness.LoanKind = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_LoanKind_6, business.LoanKind).Name;
                viewBusiness.ProductType = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_ProductKind_7, business.ProductKind).Name;
                viewBusiness.BusinessStatus = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                    RedisEnumOperatorBLL.HashId_BusinessStatus_10, business.BusinessStatus).Name;
                viewBusiness.CLoanStatus = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                    RedisEnumOperatorBLL.HashId_CLoanStatus_11, business.CLoanStatus).Name;
                viewBusiness.ResidualCapital = business.ResidualCapital;
                viewBusiness.OverMonth = business.OverMonth.ToInt().ValueToEnum<EnumDunMark>().ToString();
                viewBusiness.ServiceSideKey = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                         RedisEnumOperatorBLL.HashId_ServiceGroup_3, business.ServiceSideKey).Name;
                viewBusiness.GuaranteeSideKey = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_GuaranteeGroup_5, business.GuaranteeSideKey).Name;
                viewBusiness.LendingSideKey = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_LendingGroup_4, business.LendingSideKey).Name;
                viewBusiness.BranchKey = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_Store_12, business.BranchKey).Name;
                viewBusiness.SalesManID = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        RedisEnumOperatorBLL.HashId_User_2, business.SalesManID.ToString()).Name;
                viewBusiness.SalesTeam = business.SalesTeam;
                viewBusiness.LoanTime = business.LoanTime;
                viewBusiness.ClearLoanTime = business.ClearLoanTime;
                viewBusiness.ZClearLoanTime = business.LoanTime.AddMonths(business.LoanPeriod);
                viewBusiness.ToLitigationTime = business.ToLitigationTime;
                viewBusiness.ToGuaranteeTime = business.ToGuaranteeTime;
                viewBusiness.LoanCapital = business.LoanCapital;
                viewBusiness.ServiceRate = business.ServiceRate;
                viewBusiness.ProceduresRate = business.ProceduresRate;
                viewBusiness.ManagementRate = business.ManagementRate.HasValue ? business.ManagementRate.Value : 0;
                        
                lstViewBusiness.Add(viewBusiness);
            }

            return lstViewBusiness;
        }
    }
}

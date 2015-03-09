using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.ExportGuaranteeDataService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.ExportGuaranteeDataService.BLL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-12-15
    /// Desc:催收单导出业务处理类
    /// </summary>
    public class ExportGuaranteeDataBLL:BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void ExportData(BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            var filter = baseFilter as ExportGuaranteeDataFilter;
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            var lstExportGuaranteeViewDatas = Singleton<ExportGuaranteeDataDAL<ExportGuaranteeViewData>>.Instance.SearchData(filter);
            if(lstExportGuaranteeViewDatas == null || lstExportGuaranteeViewDatas.Count == 0)
            {                
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {
                var lstItems = lstExportGuaranteeViewDatas.GroupBy(p => p.BusinessId).Select(x => new { K = x.Key, V = x.ToList() });

                var lstResults = new List<ExportGuaranteeViewData>();
                int index = 1;
                ExportGuaranteeViewData data = null;
                foreach(var item in lstItems)
                {
                    var singel = item.V.FirstOrDefault();

                    data = new ExportGuaranteeViewData();
                    data.No = index++;
                    data.RegionName = singel.RegionName;
                    data.BranchName = singel.BranchName;
                    data.Team = singel.Team;
                    data.ContractNo = singel.ContractNo;
                    data.CustomerName = singel.CustomerName;
                    data.LoanProduct = singel.LoanProduct;
                    data.LoanCapital = singel.LoanCapital;
                    data.LoanDate = singel.LoanDate;
                    data.LoanPeriod = singel.LoanPeriod;
                    data.LendingSide = singel.LendingSide;
                    data.BusinessStatus = singel.BusinessStatus;
                    data.GuaranteeDate = singel.GuaranteeDate;
                    data.GuaranteeAmount = singel.GuaranteeAmount;
                    data.DunDetail = item.V.Count() > 0 ? string.Join(",", item.V.Select(p => p.OtherConditions)) : "";
                    data.DetailAndResult = "";
                    data.AgentName = singel.AgentName;

                    lstResults.Add(data);
                }

                // 设置输出文件
                SetExportFile(filter, lstResults, responseEntity);
            }
        }
    }
}

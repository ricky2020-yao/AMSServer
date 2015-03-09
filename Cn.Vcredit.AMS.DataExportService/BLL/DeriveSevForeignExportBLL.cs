using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataExportService.Common;
using Cn.Vcredit.AMS.DataExportService.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.BLL
{
    /// <summary>
    /// Author:Ricky
    /// CreateTime:2014-12-12
    /// Description:导出外贸数据2逻辑处理类
    /// </summary>
    public class DeriveSevForeignExportBLL : BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public void ExportData(DeriveSevExportFilter filter, ResponseEntity responseEntity)
        {
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            var reportDetailList
                = Singleton<DeriveSevForeignExportDAL<DeriveSevExportForeignViewData>>.Instance.SearchData(filter);

            if (reportDetailList == null || reportDetailList.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("无数据可导出！。");
            }
            else
            {
                // 格式化成导出的格式
                DataTable dt = FormatToExport(reportDetailList, filter);
                // 设置输出文件
                SetExportFile(filter, dt, responseEntity);
            }
        }

        /// <summary>
        /// 格式化成导出的格式
        /// </summary>
        /// <param name="batchViewList">原始数据</param>
        /// <returns>格式化后结果</returns>
        private DataTable FormatToExport(List<DeriveSevExportForeignViewData> batchViewList, DeriveSevExportFilter filter)
        {
            DataTable dtb = new DataTable();

            string[] columns = filter.Fields.Split(WebServiceConst.Separater_Comma.ToArray());
            foreach (string col in columns)
                dtb.Columns.Add(col);

            foreach (var data in batchViewList)
            {
                DataRow dtr = dtb.NewRow();
                dtr["BusinessCode"] = data.BusinessCode;
                dtr["ContractID"] = data.ContractID;
                dtr["CustomerName"] = data.CustomerName;
                dtr["DeductDate"] = data.DeductDate;
                dtr["DeductType"] = data.DeductType;
                dtr["ContractAmount"] = data.ContractAmount;
                dtr["InterestAmount"] = data.InterestAmount;
                dtr["ServiceFee"] = data.ServiceFee;
                dtr["ServiceFeedeductcharge"] = data.ServiceFeedeductcharge;
                dtr["deductcharge"] = data.deductcharge;
                dtr["guaranteecharge"] = data.guaranteecharge;
                dtr["CleanCreditCharge"] = data.CleanCreditCharge;
                dtr["PunitiveInterestAmount"] = data.PunitiveInterestAmount;
                dtr["RepayDuration"] = data.RepayDuration;
                dtr["ContractTerminatedDate"] = data.ContractTerminatedDate;
                dtr["RepaySeqNo"] = data.RepaySeqNo;
                dtr["ThirdPartyCode"] = data.ThirdPartyCode;
                dtr["RecieveDate"] = data.RecieveDate;
                dtb.Rows.Add(dtr);
            }
            return dtb;
        }
    }
}

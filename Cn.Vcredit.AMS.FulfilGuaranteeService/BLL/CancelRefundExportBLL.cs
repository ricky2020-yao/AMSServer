using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.FulfilGuaranteeService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Cn.Vcredit.Common.Constants;

namespace Cn.Vcredit.AMS.FulfilGuaranteeService.BLL
{
    /// <summary>
    /// Author:Ricky
    /// CreateTime:2014-12-9
    /// Description:合同解约退款明细导出逻辑处理层
    /// </summary>
    public class CancelRefundExportBLL : BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void ExportData(CancelRefundExportFilter baseFilter, ResponseEntity responseEntity)
        {
            if (baseFilter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            var reportDetailList
                = Singleton<CancelRefundExportDAL<CancelRefundViewData>>.Instance.SearchData(baseFilter);

            if (reportDetailList == null || reportDetailList.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                DataTable dt = FormatToExport(reportDetailList, baseFilter);
                // 设置输出文件
                SetExportFile(baseFilter, dt, responseEntity);
            }
        }

        /// <summary>
        /// 格式化成导出的格式
        /// </summary>
        /// <param name="batchViewList">原始数据</param>
        /// <returns>格式化后结果</returns>
        private DataTable FormatToExport(List<CancelRefundViewData> batchViewList, CancelRefundExportFilter filter)
        {
            DataTable dtb = new DataTable();

            string[] columns = filter.Fields.Split(WebServiceConst.Separater_Comma.ToArray());
            foreach (string col in columns)
                dtb.Columns.Add(col);

            foreach (var refund in batchViewList)
            {
                DataRow dtr = dtb.NewRow();
                dtr["BusinessCode"] = "023";
                dtr["DeductType"] = string.Format("0{0}", refund.PayType.ToString());
                dtr["ContractID"] = refund.ContractNo;
                dtr["CustomerName"] = refund.CustomerName;
                dtr["ContractAmount"] = refund.RefundAmt;
                dtr["DeductDate"] = refund.PayDate.ToDateString();
                dtr["ThirdPartyCode"] = string.Empty;
                dtr["RecieveDate"] = refund.ReceivedDate.ToDateTimeString();

                //补解约数据
                dtr["InterestAmount"] = 0;
                dtr["deductcharge"] = 0;
                dtr["PunitiveInterestAmount"] = 0;
                dtr["RepayDuration"] = 0;
                dtr["ContractTerminatedDate"] = refund.CancelTime.HasValue ? refund.CancelTime.ToDateTimeString() : refund.ReceivedDate.ToDateTimeString();
                dtb.Rows.Add(dtr);
            }
            return dtb;
        }
    }
}

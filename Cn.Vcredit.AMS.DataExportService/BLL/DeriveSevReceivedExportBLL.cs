using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataExportService.Common;
using Cn.Vcredit.AMS.DataExportService.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.BLL
{
    /// <summary>
    /// Author:Ricky
    /// CreateTime:2014-12-12
    /// Description:现收数据导出逻辑处理类
    /// </summary>
    public class DeriveSevReceivedExportBLL : BaseBLL
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
                = Singleton<DeriveSevReceivedExportDAL<DeriveSevExportReceiveViewData>>.Instance.SearchData(filter);

            if (reportDetailList == null || reportDetailList.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("无数据可导出！。");
            }
            else
            {
                //string[] titles = new string[] { 
                //     "类型","合同号","姓名"};
                //string[] fileds = new string[] {
                //    "StrReviceType", "ContactNo", "CustomerName"};
                //filter.Titles = string.Join(",", titles);
                //filter.Fields = string.Join(",", fileds);

                SetExportData(reportDetailList);
                // 设置输出文件
                SetExportFile(filter, reportDetailList, responseEntity);
            }
        }

        /// <summary>
        /// 设置输出数据的格式
        /// </summary>
        /// <param name="lstData"></param>
        private void SetExportData(List<DeriveSevExportReceiveViewData> lstData)
        {
            foreach (var data in lstData)
            {
                data.StrReviceType = Utility.GetReviceTypeName(
                    data.ReceivedType.ValueToEnum<EnumAdjustKind>());
                data.PayDate = data.RecTime.ToString("yyyy-MM-dd hh:mm:ss");
                data.ProductKind = Utility.GetProductKindDesc(data.ProductKind);
            }
        }
    }
}

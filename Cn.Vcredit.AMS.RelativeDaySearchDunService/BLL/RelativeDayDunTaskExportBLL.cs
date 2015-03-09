using Aspose.Cells;
using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.RelativeDaySearchDunService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using AsposeXls = Aspose.Cells;

namespace Cn.Vcredit.AMS.RelativeDaySearchDunService.BLL
{
    /// <summary>
    /// Author:ricky
    /// Date:2014-12-15
    /// Desc:催收任务导出业务处理类
    /// </summary>
    public class RelativeDayDunTaskExportBLL : BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void ExportData(BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            var filter = baseFilter as RelativeDayExportDunFilter;
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            DataTable dt
                = Singleton<RelativeDayDunTaskExportDAL<RelativeDaySearchDunExportViewData>>.Instance.SearchDataToDataTable(filter);
            if (dt == null || dt.Rows.Count <= 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult, "无催收单导出");
                m_Logger.Info("无催收单导出。");
                return;
            }

            AsposeXls.Workbook workbook = new AsposeXls.Workbook();
            AsposeXls.Worksheet sheet = workbook.Worksheets[0];

            Aspose.Cells.Style sc1 = workbook.Styles[workbook.Styles.Add()];
            sc1.ShrinkToFit = true;
            sc1.Number = 14;
            sc1.HorizontalAlignment = AsposeXls.TextAlignmentType.Center;
            sc1.VerticalAlignment = AsposeXls.TextAlignmentType.Center;
            Aspose.Cells.StyleFlag scf1 = new Aspose.Cells.StyleFlag();
            scf1.ShrinkToFit = true;
            scf1.NumberFormat = true;
            scf1.HorizontalAlignment = true;
            scf1.VerticalAlignment = true;
            Aspose.Cells.Column colomn1 = sheet.Cells.Columns[9];
            colomn1.ApplyStyle(sc1, scf1);

            sheet.FreezePanes(1, 1, 1, 0); //冻结第一行

            DataTable dtb = dt.Clone();
            foreach (DataRow item in dt.Rows)
            {
                DataRow NewRow = dtb.NewRow();
                for (int i = 0; i < NewRow.ItemArray.Length; i++)
                {
                    NewRow[i] = item[i];
                }
                NewRow["客户类型"] = Convert.ToByte(item["客户类型"]).ValueToDesc<EnumBusinessStatus>();
                NewRow["逾期月数"] = "M" + item["逾期月数"].ToString();
                NewRow["客户状态"] = Convert.ToByte(item["客户状态"]).ValueToDesc<EnumLawsuitStatus>();
                //NewRow["产品类型"] = Convert.ToByte(item["产品类型"]).ValueToDesc<EnumLawsuitStatus>();
                NewRow["产品种类"] = Convert.ToByte(item["产品种类"]).ValueToDesc<EnumProductKind>();
                dtb.Rows.Add(NewRow);
            }

            sheet.Cells.ImportDataTable(dtb, true, 0, 0);
            sheet.AutoFitColumns();

            byte[] result = null;
            using (MemoryStream stream = new MemoryStream())
            {
                workbook.Save(stream, SaveFormat.Xlsx);
                result = stream.ToArray();
            }

            if (result == null || result.Length == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("无催收单导出。");
            }
            else
            {
                // 设置输出文件
                ResponseFileResult responseResult = new ResponseFileResult();
                responseResult.Result = result;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }
    }
}

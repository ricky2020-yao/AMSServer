using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.EveryDueReportService.DAL;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.EveryDueReportService.BLL
{
    /// <summary>
    /// Author:wichell
    /// CreateTime:2014年10月9日
    /// Description:每日逾期静态报表导出
    /// </summary>
    public class EveryDueReportExportBLL : BaseBLL
    {
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public virtual void Search(EveryDueReportFilter filter
            , ResponseEntity responseEntity)
        {
            if (filter == null)
                return;

            var result = Singleton<EveryDueReportExportDAL<EveryDueReportViewData>>
                .Instance.SearchDataToDataTable(filter);

            if (result == null || result.Rows.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                //DataTable dtb = result.Clone();
                //foreach (DataRow item in result.Rows)
                //{
                //    DataRow NewRow = dtb.NewRow();
                //    for (int i = 0; i < 38; i++)
                //    {
                //        NewRow[i] = item[i];
                //    }
                //    NewRow["日期"] = Convert.ToDateTime(item["日期"]).AddDays(-1).ToString("yyyy-MM-dd");
                //    dtb.Rows.Add(NewRow);
                //}

                // 设置输出文件
                SetExportFile(filter, result, responseEntity);
            }
        }
    }
}

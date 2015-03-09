using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.EveryDueRepayReportService.DAL;
using Cn.Vcredit.AMS.EveryDueRepayReportService.Common;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Cn.Vcredit.AMS.EveryDueRepayReportService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月18日
    /// Description:每日逾期客户还款明细报表导出逻辑处理类
    /// </summary>
    public class EveryDueRepayReportExportBLL : BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(EveryDueRepayReportSearchFilter baseFilter, ResponseEntity responseEntity)
        {
            if (baseFilter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            var reportDetailList
                = Singleton<EveryDueRepayReportSearchDAL<EveryDueRepayDetailReportViewData>>
                .Instance.SearchData(baseFilter);

            if (reportDetailList == null || reportDetailList.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                ConvertUtil.ConvertEveryDueRepayDetailReportViewData(reportDetailList);
                // 设置输出文件
                SetExportFile(baseFilter, reportDetailList, responseEntity);
            }
        }
    }
}

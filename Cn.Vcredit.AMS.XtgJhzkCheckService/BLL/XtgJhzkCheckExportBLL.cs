using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.XtgJhzkCheckService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.XtgJhzkCheckService.BLL
{
    /// <summary>
    /// Author:Ricky
    /// CreateTime:2014-12-9
    /// Description:信托归集户账款核对导出逻辑处理层
    /// </summary>
    public class XtgJhzkCheckExportBLL:BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(XtgJhzkCheckExportFilter baseFilter, ResponseEntity responseEntity)
        {
            if (baseFilter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            var reportDetailList
                = Singleton<XtgJhzkCheckExportDAL<XtgJhzkViewData>>.Instance.SearchData(baseFilter);

            if (reportDetailList == null || reportDetailList.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                // 设置输出文件
                SetExportFile(baseFilter, reportDetailList, responseEntity);
            }
        }
    }
}

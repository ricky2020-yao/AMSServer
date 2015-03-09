using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.ExamineIMP;
using Cn.Vcredit.AMS.Entity.ViewData.ExamineIMP;
using Cn.Vcredit.AMS.PostLoanOverDueReportService.DAL;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.PostLoanOverDueReportService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月24日
    /// Description:根据订单号获取订单的逾期信息逻辑处理层
    /// </summary>
    public class GetOverDueReportDetailByBusinessIdsBLL : BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            if (baseFilter == null)
                return;

            OverDueDetailFilter filter = baseFilter as OverDueDetailFilter;
            if (filter == null)
                return;

            string guid = Guid.NewGuid().ToString();

            // 保存业务号列表到数据库
            SaveBusinessIDsListToDataBase(guid, filter.BusinessIds);

            filter.Guid = guid;
            var reportDetailList = Singleton<GetOverDueReportDetailByBusinessIdsDAL<OverDueReportDetailViewData>>
                .Instance.SearchData(filter);

            if (reportDetailList == null || reportDetailList.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                var responseResult = new ResponseListResult<OverDueReportDetailViewData>();
                responseResult.TotalCount = reportDetailList.Count;
                responseResult.LstResult = reportDetailList;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }

        /// <summary>
        /// 保存业务号列表到数据库
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="businessIds"></param>
        private void SaveBusinessIDsListToDataBase(string guid, string businessIds)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("BusinessId", typeof(string)));
            dt.Columns.Add(new DataColumn("GuiId", typeof(string)));

            string[] businessIdArray = businessIds.Split(
                WebServiceConst.Separater_Comma.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (businessIdArray == null || businessIdArray.Length == 0)
                return;

            DataRow dr = null;
            foreach (string mobile in businessIdArray)
            {
                dr = dt.NewRow();
                dr["BusinessId"] = mobile;
                dr["GuiId"] = guid;
                dt.Rows.Add(dr);
            }

            dt.TableName = "SaveBusinessId";

            try
            {
                SqlBulkHelper.ExecuteBulkImport("SaveBusinessId", dt, "PostLoanDB", businessIdArray.Length);
            }
            catch (Exception ex)
            {
                m_Logger.Error("审核员贷后客户逾期情况日报表查询异常：" + ex.Message);
                m_Logger.Error("审核员贷后客户逾期情况日报表查询异常：" + ex.StackTrace);
            }
            finally
            {
            }
        }
    }
}

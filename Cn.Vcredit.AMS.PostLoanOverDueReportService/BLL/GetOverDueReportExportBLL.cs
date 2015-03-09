using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.ExamineIMP;
using Cn.Vcredit.AMS.Entity.ViewData.ExamineIMP;
using Cn.Vcredit.AMS.PostLoanOverDueReportService.DAL;
using Cn.Vcredit.AMS.PostLoanOverDueReportService.Data;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.PostLoanOverDueReportService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月14日
    /// Description:导出审核员贷后客户逾期情况日报表逻辑处理层
    /// </summary>
    public class GetOverDueReportExportBLL : BaseBLL
    {
        #region 常量
        private const string ExamineStatusName_First = "初审";
        private const string ExamineStatusName_Second = "复审";
        private const int ExamineStatus_None = 0;
        private const int ExamineStatus_First = 1;
        private const int ExamineStatus_Second = 2;
        #endregion

        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            if (baseFilter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            OverDueReportFilter filter = baseFilter as OverDueReportFilter;
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            var reportDetailList = Singleton<GetOverDueReportSearchResultDAL<OverDueDetailReportData>>
                .Instance.SearchData(baseFilter);

            List<OverDueReportViewData> listOverDueReportViewData = new List<OverDueReportViewData>();
            IDictionary<int, Dictionary<int, List<OverDueDetailReportData>>> dicReport
                = new Dictionary<int, Dictionary<int, List<OverDueDetailReportData>>>();
            Dictionary<int, List<OverDueDetailReportData>> dicTmp;
            List<OverDueDetailReportData> lstTmp;

            foreach (var detail in reportDetailList)
            {
                if (!dicReport.TryGetValue(detail.Examiner, out dicTmp))
                    dicTmp = new Dictionary<int, List<OverDueDetailReportData>>();

                if (!dicTmp.TryGetValue(detail.ExamineStatus, out lstTmp))
                    lstTmp = new List<OverDueDetailReportData>();

                lstTmp.Add(detail);
                dicTmp[detail.ExamineStatus] = lstTmp;
                dicReport[detail.Examiner] = dicTmp;
            }

            int index = 0;
            int loanCount = 0;
            int firstOverDueCount = 0;
            int fourM2Count = 0;
            int threeNonePayCount = 0;
            int m1Count = 0;
            int m2Count = 0;
            int m3Count = 0;
            int m4Count = 0;
            int m5PlusCount = 0;
            OverDueReportViewData report;
            foreach (var key in dicReport.Keys)
            {
                foreach (var keyInner in dicReport[key].Keys)
                {
                    lstTmp = dicReport[key][keyInner];
                    report = new OverDueReportViewData();
                    report.FirstOverDueBusinessIDs = "";
                    report.FourM2BusinessIDs = "";
                    report.ThreeNonePayBusinessIDs = "";
                    report.M1BusinessIDs = "";
                    report.M2BusinessIDs = "";
                    report.M3BusinessIDs = "";
                    report.M4BusinessIDs = "";
                    report.M5PlusBusinessIDs = "";

                    // 序号
                    report.No = ++index;
                    // 工号
                    report.Examiner = key;
                    // 审核属性
                    report.ExamineStatus = GetExamineStatusName(keyInner);

                    loanCount = 0;
                    firstOverDueCount = 0;
                    fourM2Count = 0;
                    threeNonePayCount = 0;
                    m1Count = 0;
                    m2Count = 0;
                    m3Count = 0;
                    m4Count = 0;
                    m5PlusCount = 0;

                    if (lstTmp == null || lstTmp.Count == 0)
                        continue;

                    // 已放款数
                    loanCount = lstTmp.Count;
                    report.LoanCount = loanCount;
                    foreach (var detail in lstTmp)
                    {
                        // 审核人
                        report.ExaminerName = detail.Name;

                        // 首逾数
                        if (detail.DifferCountFirst > 0
                        && !report.FirstOverDueBusinessIDs.Contains(detail.Bid.ToString()))
                        {
                            firstOverDueCount++;
                            report.FirstOverDueBusinessIDs += WebServiceConst.Separater_Comma + detail.Bid;
                        }
                        // 前4期账单出现M2的数量
                        if (detail.DifferCountFour > 0
                        && !report.FourM2BusinessIDs.Contains(detail.Bid.ToString()))
                        {
                            fourM2Count++;
                            report.FourM2BusinessIDs += WebServiceConst.Separater_Comma + detail.Bid;
                        }
                        // 前3期账单0次还款
                        if (detail.DifferCountThree == 0
                        && !report.ThreeNonePayBusinessIDs.Contains(detail.Bid.ToString()))
                        {
                            threeNonePayCount++;
                            report.ThreeNonePayBusinessIDs += WebServiceConst.Separater_Comma + detail.Bid;
                        }

                        int mCount = detail.OverMonth;
                        switch (mCount)
                        {
                            case 0:
                                break;
                            case 1:
                                if (!report.M1BusinessIDs.Contains(detail.Bid.ToString()))
                                {
                                    m1Count++;
                                    report.M1BusinessIDs += WebServiceConst.Separater_Comma + detail.Bid;
                                }
                                break;
                            case 2:
                                if (!report.M2BusinessIDs.Contains(detail.Bid.ToString()))
                                {
                                    m2Count++;
                                    report.M2BusinessIDs += WebServiceConst.Separater_Comma + detail.Bid;
                                }
                                break;
                            case 3:
                                if (!report.M3BusinessIDs.Contains(detail.Bid.ToString()))
                                {
                                    m3Count++;
                                    report.M3BusinessIDs += WebServiceConst.Separater_Comma + detail.Bid;
                                }
                                break;
                            case 4:
                                if (!report.M4BusinessIDs.Contains(detail.Bid.ToString()))
                                {
                                    m4Count++;
                                    report.M4BusinessIDs += WebServiceConst.Separater_Comma + detail.Bid;
                                }
                                break;
                            default:
                                if (!report.M5PlusBusinessIDs.Contains(detail.Bid.ToString()))
                                {
                                    m5PlusCount++;
                                    report.M5PlusBusinessIDs += WebServiceConst.Separater_Comma + detail.Bid;
                                }
                                break;
                        }
                    }

                    report.FirstOverDueCount = firstOverDueCount;
                    report.FourM2Count = fourM2Count;
                    report.ThreeNonePayCount = threeNonePayCount;
                    // M1逾期率
                    report.M1Rate
                        = string.Format("{0}%", Math.Round((double)m1Count / (double)loanCount * 100, 1).ToString("#0.0"));
                    // M2逾期率
                    report.M2Rate
                        = string.Format("{0}%", Math.Round((double)m2Count / (double)loanCount * 100, 1).ToString("#0.0"));
                    // M3逾期率
                    report.M3Rate
                        = string.Format("{0}%", Math.Round((double)m3Count / (double)loanCount * 100, 1).ToString("#0.0"));
                    // M4逾期率
                    report.M4Rate
                        = string.Format("{0}%", Math.Round((double)m4Count / (double)loanCount * 100, 1).ToString("#0.0"));
                    // M5+逾期率
                    report.M5PlusRate
                        = string.Format("{0}%", Math.Round((double)m5PlusCount / (double)loanCount * 100, 1).ToString("#0.0"));
                    listOverDueReportViewData.Add(report);
                }
            }

            if (listOverDueReportViewData == null || listOverDueReportViewData.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {
                // 设置输出文件
                SetExportFile(filter, listOverDueReportViewData, responseEntity);
            }
        }

        /// <summary>
        /// 获取审核属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetExamineStatusName(int type)
        {
            if (type == ExamineStatus_First)
            {
                return ExamineStatusName_First;
            }
            else if (type == ExamineStatus_Second)
            {
                return ExamineStatusName_Second;
            }

            return "";
        }
    }
}

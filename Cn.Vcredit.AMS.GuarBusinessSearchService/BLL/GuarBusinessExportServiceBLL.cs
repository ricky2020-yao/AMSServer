using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage.BillDun;
using Cn.Vcredit.AMS.GuarBusinessSearchService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.GuarBusinessSearchService.BLL
{
    public class GuarBusinessExportServiceBLL : BaseBLL
    {
        IDictionary<string, string> dicEng2ChnColumn = new Dictionary<string, string>()
        {
            {"BusinessId","业务号"},
            {"CustomerName","客户姓名"},
            {"RegionKey","地区"},
            {"BranchKey","门店"},
            {"AgentName","理财顾问"},
            {"ProductKey","产品"},
            {"LoanPeriod","期限"},
            {"EntryDate","交单时间"},
            {"LoanDate","放款时间"},
            {"LoanCapital","放款金额"},
            {"OverdueStatus","逾期状态"},
            {"GuaranteeAmt","担保金额"},
            {"OverdueMoney","逾期金额"},
            {"GuaranteeDate","担保日期"},
            {"ResidualCapital","剩余本金"},
        };
        public void SearchData(BaseExportFilter baseFilter, ResponseEntity responseEntity)
        {
            if (baseFilter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            baseFilter.ExportFileType = (int)EnumExportFileType.Xlsx;

            var tbReport = Singleton<GuarBusinessSearchDAL>.Instance.SearchDataToDataTable(baseFilter);

            if (tbReport == null || tbReport.Rows.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                //转义枚举
                foreach (DataRow item in tbReport.Rows)
                {
                    item["RegionKey"] = Singleton<RedisEnumOperatorBLL>.Instance.
                                     GetRedisData(RedisEnumOperatorBLL.HashId_Region_9, item["RegionKey"].ToString()).Name;

                    item["BranchKey"] = Singleton<RedisEnumOperatorBLL>.Instance.
                                    GetRedisData(RedisEnumOperatorBLL.HashId_Store_12, item["BranchKey"].ToString()).Name;

                    item["ProductKey"] = Singleton<RedisEnumOperatorBLL>.Instance.
                                    GetRedisData(RedisEnumOperatorBLL.HashId_LoanKind_6, item["ProductKey"].ToString()).Name;
                }

                List<DataColumn> removeColumns = new List<DataColumn>();

                //表头转换为中文,可以写在配置中,暂时在Code中指定
                foreach (DataColumn col in tbReport.Columns)
                {
                    if (dicEng2ChnColumn.ContainsKey(col.ColumnName))
                        col.ColumnName = dicEng2ChnColumn[col.ColumnName];
                    else
                        removeColumns.Add(col);
                }
                //删除不需要显示的列
                removeColumns.ForEach(e=>tbReport.Columns.Remove(e.ColumnName));
                //导出
                SetExportFile(baseFilter, tbReport, responseEntity);
            }
        }
    }
}

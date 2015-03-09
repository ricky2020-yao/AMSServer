using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.CustomerService;
using Cn.Vcredit.AMS.Entity.ViewData.CustomerService;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cn.Vcredit.AMS.DeductRemindService.DAL;
using System.Data;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;

namespace Cn.Vcredit.AMS.DeductRemindService.BLL
{

    public class DeductRemindExportBLL : BaseBLL
    {
        IDictionary<string, string> dicEng2ChnColumn = new Dictionary<string, string>()
        {
            {"ContractNo","合同编号"},
            {"CustomerName","客户姓名"},
            {"LoanCapital","贷款金额"},
            {"LoanPeriod","贷款期限"},
            {"MonthPay","月还款额"},
            {"LoanTime","放贷日期"},
            {"SaleMode","业务渠道"},
            {"Region","地区"},
            {"BranchKey","营业所"},
            {"SalesTeam","销售团队"},
            {"SalesManName","销售人员"},
        };
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(DeductRemindExportFilter baseFilter, ResponseEntity responseEntity)
        {
            if (baseFilter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            baseFilter.ExportFileType = (int)EnumExportFileType.Xlsx;

            var reportDetailList = Singleton<DeductRemindExportDAL>.Instance.SearchDataToDataTable(baseFilter);

            if (reportDetailList == null || reportDetailList.Rows.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                //转义枚举
                foreach(DataRow item in reportDetailList.Rows)
                {
                    item["Region"] = GetEnumValue(RedisEnumOperatorBLL.HashId_Region_9, item["Region"].ToString());

                    item["BranchKey"] = GetEnumValue(RedisEnumOperatorBLL.HashId_Store_12, item["BranchKey"].ToString());

                    item["SaleMode"] = GetEnumValue(RedisEnumOperatorBLL.HashId_SaleMode_13, item["SaleMode"].ToString());
                 }

                //表头转换为中文,可以写在配置中,暂时在Code中指定
                foreach(DataColumn col in reportDetailList.Columns)
                {
                    if (dicEng2ChnColumn.ContainsKey(col.ColumnName))
                        col.ColumnName = dicEng2ChnColumn[col.ColumnName];
                }

                SetExportFile(baseFilter, reportDetailList, responseEntity);
            }
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="EnumKey"></param>
        /// <returns></returns>
        private string GetEnumValue(string id,string EnumKey)
        {
            var redisEnum = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                        id, EnumKey);
           return redisEnum == null ? string.Empty : redisEnum.Name;
        }
    }
}

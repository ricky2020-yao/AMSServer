using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Data.DB.MongoData;
using Cn.Vcredit.AMS.DataAccess.Caches;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.TimeLineService.DAL;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.TimeLineService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月19日
    /// Description:时间轴设置关帐日初始化逻辑处理层
    /// </summary>
    public class TimeLineCloseDayInitBLL:BaseBLL
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

            TimeLineInitFilter filter = baseFilter as TimeLineInitFilter;
            if (filter == null)
                return;

            var lstCloseDays = Singleton<TimeLineCloseDayInitDAL>.Instance.SearchData(baseFilter);
            List<CloseBillTimeViewData> listTime = new List<CloseBillTimeViewData>();

            if (lstCloseDays != null 
                && lstCloseDays.Count > 0)
            {
                //var userPermissionDataList = Singleton<CompanyCache>.Instance.GetPermission(baseFilter.UserId);
                //List<int> CompanyIds = Singleton<CompanyCache>.Instance.CompanyIds(baseFilter.UserId);
                //foreach (var permission in userPermissionDataList)
                //{
                //    CompanyKeys.Add(permission.FullKey);
                //}
                List<string> companyKeys
                    = filter.CompanyKeys.Split(WebServiceConst.Separater_Comma.ToArray()).ToList();
                int order = 0;

                //判断是否有权修改
                foreach (var item in lstCloseDays)
                {
                    BankAccount company = Singleton<BankAccountsCache>
                        .Instance.BankAccounts.FirstOrDefault(x => x.CompanyKey == item.CompanyKey);

                    if (company == null)
                        continue;

                    if (!companyKeys.Contains(company.CompanyKey))
                        continue;

                    listTime.Add(new CloseBillTimeViewData
                    {
                        CloseBillDayID = item.CloseBillDayID,
                        CompanyName = company.AccountNumber,
                        LatestTime = item.LatestTime.ToDateString(),
                        OriginalTime = item.OriginalTime.ToDateString(),
                        OperatorID = baseFilter.UserId,
                        Order = ++order,
                        DataType = WebServiceConst.TimeLineType_CloseDay
                    });
                }
            }

            if (listTime == null || listTime.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {
                var responseResult = new ResponseListResult<CloseBillTimeViewData>();
                responseResult.LstResult = listTime;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }

        }
    }
}

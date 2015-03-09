using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Data.DB.MongoData;
using Cn.Vcredit.AMS.DataAccess.Caches;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
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
    /// Description:时间轴设置数据初始化逻辑处理层
    /// </summary>
    public class TimeLineInitBLL : BaseBLL
    {
        /// <summary>
        /// 获取关账日信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        /// <returns></returns>
        public void GetCloseBillTime(TimeLineInitFilter filter, ResponseEntity responseEntity)
        {
            List<string> companyKeys =new List<string>();
            if (!string.IsNullOrEmpty(filter.CompanyKeys))
            {
                companyKeys = filter.CompanyKeys.Split(WebServiceConst.Separater_Comma.ToArray()).ToList();
            }

            // 获取关账日信息
            List<CloseBillTimeViewData> listTime = GetCloseBillTime(filter, companyKeys);

            // 获取序列（21/28/12）
            for (int i = 2; i > -10; i--)
            {
                listTime.AddRange(GetTimeLine(DateTime.Now.AddMonths(i), filter.UserId, companyKeys));
            }

            var responseResult = new ResponseListResult<CloseBillTimeViewData>();
            responseResult.LstResult = listTime;

            ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
            responseEntity.Results = responseResult;
        }

        /// <summary>
        /// 获取关账日信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="companyKeys"></param>
        /// <returns></returns>
        private List<CloseBillTimeViewData> GetCloseBillTime(TimeLineInitFilter filter, List<string> companyKeys)
        {
            var lstCloseDays = Singleton<TimeLineCloseDayInitDAL>.Instance.SearchData(filter);
            List<CloseBillTimeViewData> listTime = new List<CloseBillTimeViewData>();

            if (lstCloseDays == null 
                || lstCloseDays.Count > 0)
            {
                //List<int> CompanyIds = Singleton<CompanyCache>.Instance.CompanyIds(filter.UserId);
                //List<MongoUserPermission> userPermissionDataList = Singleton<CompanyCache>.Instance.GetPermission(filter.UserId);
                //foreach (var permission in userPermissionDataList)
                //{
                //    CompanyKeys.Add(permission.FullKey);
                //}

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
                        OperatorID = filter.UserId,
                        Order = ++order,
                        DataType = WebServiceConst.TimeLineType_CloseDay
                    });
                }
            }

            return listTime;
        }

        /// <summary>
        /// 根据月份查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="companyKeys"></param>
        /// <returns></returns>
        private List<DeductSequence> GetTimeLine(TimeLineInitFilter filter, List<string> companyKeys)
        {
            //List<string> companyKeys = Singleton<CompanyCache>.Instance.CompanyIds(filter.UserId);

            if (companyKeys == null)
                return null;

            string key = companyKeys[0];
            filter.CompanyKey = key;
            filter.Kind = (byte)EnumDeductSeqKind.DS21_28_12;
            return Singleton<TimeLineInitDAL>.Instance.SearchData(filter);
        }

        /// <summary>
        /// 获取返回的扣款序列
        /// </summary>
        /// <param name="setDate"></param>
        /// <param name="userId"></param>
        /// <param name="companyKeys"></param>
        /// <returns></returns>
        private List<CloseBillTimeViewData> GetTimeLine(DateTime setDate, int userId, List<string> companyKeys)
        {
            string nowMonth = setDate.ToBillMonthString();

            TimeLineInitFilter filter = new TimeLineInitFilter();
            filter.UserId = userId;
            filter.DeductMonth = nowMonth;
            List<DeductSequence> lstDeductSequence = GetTimeLine(filter, companyKeys);

            List<CloseBillTimeViewData> listTime = new List<CloseBillTimeViewData>();
            if (lstDeductSequence != null && lstDeductSequence.Count > 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    listTime.Add(new CloseBillTimeViewData
                    {
                        DeductYear = setDate.Year,
                        DeductMonth = setDate.Month,
                        DeductTime = lstDeductSequence[i].DeductTime.ToDateString(),
                        Order = i + 1,
                        DataType = WebServiceConst.TimeLineType_Sequence
                    });
                }
            }
            else
            {
                listTime.Add(new CloseBillTimeViewData
                {
                    DeductYear = setDate.Year,
                    DeductMonth = setDate.Month,
                    DeductTime = setDate.Year.ToString() + "-" + setDate.ToString("MM") + "-21",
                    Order = 1,
                    DataType = WebServiceConst.TimeLineType_Sequence
                });
                listTime.Add(new CloseBillTimeViewData
                {
                    DeductYear = setDate.Year,
                    DeductMonth = setDate.Month,
                    DeductTime = setDate.Year.ToString() + "-" + setDate.ToString("MM") + "-28",
                    Order = 2,
                    DataType = WebServiceConst.TimeLineType_Sequence
                });
                listTime.Add(new CloseBillTimeViewData
                {
                    DeductYear = setDate.Year,
                    DeductMonth = setDate.Month,
                    DeductTime = setDate.AddMonths(1).Year.ToString() + "-" + setDate.AddMonths(1).ToString("MM") + "-12",
                    Order = 3,
                    DataType = WebServiceConst.TimeLineType_Sequence
                });
            }

            return listTime;
        }
    }
}

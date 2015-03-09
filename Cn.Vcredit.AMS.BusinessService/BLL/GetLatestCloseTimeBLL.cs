using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Data.DB.RedisData;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BusinessService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-11-24
    /// Description:订单详情信息逻辑处理类
    /// </summary>
    public class GetLatestCloseTimeBLL : BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public void GetLatestCloseTime(SearchBusinessListFilter baseFilter, ResponseEntity responseEntity)
        {
            if (baseFilter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            var viewData = new LatestCloseTimeViewData();

            // 银行账户
            var bankAccounts = Singleton<RedisEnumOperatorBLL>
                .Instance.GetEnumRedisDataEntitys<BankAccountRedisEntity>();
            if (bankAccounts == null || bankAccounts.Count == 0)
            {
                viewData.LatestTime = DateTime.MinValue;
                SetResult(viewData, responseEntity);
                return;
            }

            var bankAccount = bankAccounts.FirstOrDefault(p => p.BankAccountID == baseFilter.BankAccountID);
            if (bankAccount == null)
            {
                viewData.LatestTime = DateTime.MinValue;
                SetResult(viewData, responseEntity);
                return;
            }

            // 关帐日
            var closeBillDays = Singleton<RedisEnumOperatorBLL>
                .Instance.GetEnumRedisDataEntitys<CloseBillDayRedisEntity>();
            if (closeBillDays == null || closeBillDays.Count == 0)
            {
                viewData.LatestTime = DateTime.MinValue;
                SetResult(viewData, responseEntity);
                return;
            }

            var closeBillDay = closeBillDays.FirstOrDefault(p => p.CompanyKey == bankAccount.CompanyKey);
            if (closeBillDay == null)
                viewData.LatestTime = DateTime.MinValue;
            else
                viewData.LatestTime = closeBillDay.LatestTime;

            SetResult(viewData, responseEntity);
        }

        /// <summary>
        /// 获取最近的一次关帐时间
        /// </summary>
        /// <param name="bankAccountID"></param>
        /// <returns></returns>
        public LatestCloseTimeViewData GetLatestCloseTimeViewData(int bankAccountID)
        {
            var viewData = new LatestCloseTimeViewData();

            // 银行账户
            var bankAccounts = Singleton<RedisEnumOperatorBLL>
                .Instance.GetEnumRedisDataEntitys<BankAccountRedisEntity>();
            if (bankAccounts == null || bankAccounts.Count == 0)
            {
                viewData.LatestTime = DateTime.MinValue;
                return viewData;
            }

            var bankAccount = bankAccounts.FirstOrDefault(p => p.BankAccountID == bankAccountID);
            if (bankAccount == null)
            {
                viewData.LatestTime = DateTime.MinValue;
                return viewData;
            }

            // 关帐日
            var closeBillDays = Singleton<RedisEnumOperatorBLL>
                .Instance.GetEnumRedisDataEntitys<CloseBillDayRedisEntity>();
            if (closeBillDays == null || closeBillDays.Count == 0)
            {
                viewData.LatestTime = DateTime.MinValue;
                return viewData;
            }

            var closeBillDay = closeBillDays.FirstOrDefault(p => p.CompanyKey == bankAccount.CompanyKey);
            if (closeBillDay == null)
                viewData.LatestTime = DateTime.MinValue;
            else
                viewData.LatestTime = closeBillDay.LatestTime;

            return viewData;
        }

        /// <summary>
        /// 设置返回的结果
        /// </summary>
        /// <param name="result"></param>
        /// <param name="responseEntity"></param>
        private void SetResult(LatestCloseTimeViewData result, ResponseEntity responseEntity)
        {
            // 返回数据
            var responseResult = new ResponseListResult<LatestCloseTimeViewData>();
            responseResult.TotalCount = 1;
            responseResult.LstResult = new List<LatestCloseTimeViewData> { result };

            ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
            responseEntity.Results = responseResult;
        }
    }
}

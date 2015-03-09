using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.BLL.Products;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Data.DB.RedisData;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using Cn.Vcredit.AMS.InterestFitService.DAL;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.InterestFitService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年8月13日
    /// Description:代偿款支付设置业务处理类
    /// </summary>
    public class InterestFitBLL:BaseBLL
    {
        /// <summary>
        /// 检索数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public void SearchData(InterestFitFilter filter, ResponseEntity responseEntity)
        {
            if (filter == null)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                return;
            }

            List<string> companys = Singleton<RedisEnumOperatorBLL>.Instance.GetUserOwnCompanyKeys(responseEntity.UserId);
            filter.BranchKey = string.Join("','", companys.ToArray());

            // 获取件数
            int totalCount = Singleton<InterestFitDAL<Business>>.Instance.GetCount(filter);

            if (totalCount <= 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
                return;
            }

            if (filter.PageNo < 1)
                filter.PageNo = 1;

            int fromIndex = (filter.PageNo - 1) * filter.PageSize;
            int toIndex = filter.PageNo * filter.PageSize;
            if (toIndex > totalCount)
                toIndex = totalCount;

            filter.FromIndex = fromIndex;
            filter.ToIndex = toIndex;
            var lstBusinessDatas = Singleton<InterestFitDAL<Business>>.Instance.SearchData(filter);

            if (lstBusinessDatas == null || lstBusinessDatas.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {
                var lstBusinessIds = lstBusinessDatas.Select(x => x.BusinessID);
                filter.BusinessIDs = string.Join(",", lstBusinessIds.ToArray());
                // 银行账户
                var bankAccounts = Singleton<RedisEnumOperatorBLL>
                     .Instance.GetEnumRedisDataEntitys<BankAccountRedisEntity>();

                var lstBills = Singleton<BillDAL<Bill>>.Instance.SearchData(filter);
                var lstInterestFiltViewData = new List<InterestFiltViewData>();
                foreach (var business in lstBusinessDatas)
                {
                    business.Bills = lstBills.FindAll(x => x.BusinessID == business.BusinessID).ToList();

                    decimal sum = GetSum(business) + GetInter(business);
                    var bankser = bankAccounts.FirstOrDefault(x => x.BankAccountID == business.LendingSideID);
                    lstInterestFiltViewData.Add(new InterestFiltViewData
                    {
                        ContractNO = business.ContractNo,
                        CustomerName = business.CustomerName,
                        idenNO = business.IdentityNo,
                        GuaranteeSideDate = business.ToGuaranteeTime.ToDateString(),
                        SurplusInterest = sum.ToAmtString(),
                        Loaner = Singleton<RedisEnumOperatorBLL>.Instance.GetRedisData(
                                RedisEnumOperatorBLL.HashId_LendingGroup_4, bankser.CompanyKey).Name,
                        PayDate = business.PaymentDate.ToString()
                    });
                }

                var responseResult = new ResponseListResult<InterestFiltViewData>();
                responseResult.TotalCount = totalCount;
                responseResult.LstResult = lstInterestFiltViewData;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }

        /// <summary>
        /// 剩余利息
        /// </summary>
        /// <param name="business"></param>
        /// <returns></returns>
        private decimal GetInter(Business business)
        {
            //月利息
            decimal monthacoumt = business.LoanCapital * business.InterestRate;
            //剩余期数
            int hadPeriod = BillDAL.GetNormalBills(business.Bills).Count;
            int qs = business.LoanPeriod - hadPeriod;
            //剩余总利息
            decimal sum = monthacoumt * qs;
            return sum;
        }

        /// <summary>
        /// 剩余本金
        /// </summary>
        /// <param name="business"></param>
        /// <returns></returns>
        private decimal GetSum(Business business)
        {
            //未出帐本金
            decimal wczlxamout = 0;

            //已出期数
            string HadPeriod = business == null ? string.Empty : BillDAL.GetNormalBills(business.Bills).Count.ToString();

            var product = ProductFactory.Instance.GetProduct(business);
            var subjectItems = product.GetBaseItemsByMonth();
            //var dirItems = product.GetProductItems();

            // 未到期的未收
            var subjectItem = subjectItems.FirstOrDefault(p => p.Subject == (byte)EnumCostSubject.Capital);

            if (subjectItem != null && int.Parse(HadPeriod) < business.LoanPeriod
                && business.CLoanStatus == (byte)EnumCLoanStatus.Refunding)
            {
                wczlxamout = business.LoanCapital - (subjectItem.Amount * int.Parse(HadPeriod));
            }

            return wczlxamout;
        }
    }
}

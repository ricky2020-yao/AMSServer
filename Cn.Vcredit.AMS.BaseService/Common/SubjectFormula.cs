using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.Common
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年5月30日
    /// Description:款项公式
    /// </summary>
    public class SubjectFormula
    {
        /// <summary>
        /// 计算扣失(合同号等于7位的20，其他都为50元)
        /// </summary>
        /// <param name="contractNo"></param>
        /// <returns></returns>
        public static decimal CalculatBuckleFail(string contractNo)
        {
            return contractNo == null ? 0 : (contractNo.Length == 7 ? 20 : 50);
        }

        /// <summary>
        /// 计算担保违约金（贷款金额 * 3% = 担保违约金）
        /// </summary>
        /// <param name="capital"></param>
        /// <returns></returns>
        public static decimal CalculatGuaranteePenalty(decimal capital)
        {
            return Round(capital * 0.03m);
        }

        /// <summary>
        /// 计算剩余本金（贷款金额 - 已还本金 = 剩余本金）
        /// </summary>
        public static decimal CalculatCapitalOver(decimal loanAmount, int loanPeriod)
        {
            return Round(loanAmount - (CalculatCapital(loanAmount, loanPeriod) * (loanPeriod - 1)));
        }

        /// <summary>
        /// 计算本金(贷款金额 / 期数 = 月本金)
        /// </summary>
        public static decimal CalculatCapital(decimal loanAmount, int loanPeriod)
        {
            return Round(loanAmount / loanPeriod);
        }

        /// <summary>
        /// 计算服务费或担保费（贷款金额 * 月服务费率 = 月服务费）
        /// </summary>
        public static decimal CalculatServiceFee(decimal capital, decimal serviceRate, bool isHalf)
        {
            var serviceFee = capital * serviceRate;
            return Round(isHalf ? serviceFee / 2 : serviceFee);
        }

        /// <summary>
        /// 计算利息（贷款金额 * 月利率 = 月利息）
        /// </summary>
        public static decimal CalculatInterest(decimal capital, decimal interestRate)
        {
            return Round(capital * interestRate);
        }

        /// <summary>
        /// 计算罚息（所欠本息 * 月罚息率 = 月罚息）
        /// </summary>
        public static decimal CalculatPunitiveInterest(decimal debtsPrincipal, decimal pInterestRate)
        {
            return Round(debtsPrincipal * pInterestRate);
        }

        /// <summary>
        /// 计算利息（贷款金额 * 日利率 * 天数 = 利息费用）
        /// </summary>
        public static decimal CalculatInterest(decimal capital, decimal intRateDay, int days)
        {
            return Round(capital * intRateDay * days);
        }

        /// <summary>
        /// 计算月预缴保费（首期保费 / 12 = 月预缴保费）
        /// </summary>
        public static decimal CalculatAdvanceFee(decimal advanceFee)
        {
            return Round(advanceFee / 12);
        }

        /// <summary>
        /// 计算租金（本金 + 利息 = 租金）
        /// </summary>
        public static decimal CalculatRent(decimal capital, decimal interest)
        {
            return Round(capital + interest);
        }

        /// <summary>
        /// 计算罚息（所欠本息 * 日罚息率 * 罚息天数 = 罚息费用）
        /// </summary>
        public static decimal CalculatPunitiveInterest(decimal debtsPrincipal, decimal interestRate, int days)
        {
            return Round(debtsPrincipal * interestRate * days);
        }

        /// <summary>
        /// 计算输入年月剩余天数
        /// </summary>
        public static int CalculatDaysInMonth(DateTime dateTime, int curDay = 0)
        {
            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            return daysInMonth - curDay;
        }

        /// <summary>
        /// 四舍五入，保留三位小数
        /// </summary>
        /// <returns></returns>
        public static decimal Round(decimal amount)
        {
            return Math.Round(amount, 2);
        }

        // 2014年4月16日 Add By Baker
        /// <summary>
        /// 计算剩余本金（贷款本金-已出本金=剩余本金）
        /// </summary>
        /// <param name="loanAmount">放贷本金</param>
        /// <param name="rate">本金费率</param>
        /// <param name="loanPeriod">放贷期数</param>
        /// <returns>剩余本金</returns>
        public static decimal CalcuateCapitalOver(decimal loanAmount, decimal rate, int loanPeriod)
        {
            return Round(loanAmount - (CalcuLateCapitalByRate(loanAmount, rate) * (loanPeriod - 1)));
        }

        /// <summary>
        /// 计算本金（贷款本金*本金利率）
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static decimal CalcuLateCapitalByRate(decimal loanAmount, decimal rate)
        {
            return Round(loanAmount * rate);
        }
    }
}

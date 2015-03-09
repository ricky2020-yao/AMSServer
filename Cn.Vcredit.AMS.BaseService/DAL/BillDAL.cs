using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.DAL
{
    /// <summary>
    /// 账单操作类
    /// </summary>
    public static class BillDAL
    {
        #region- 为内存中帐单提供筛选 -
        /// <summary>
        /// Author:王正吉
        /// Description:获取普通帐单集合
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public static List<Bill> GetNormalBills(List<Bill> bills)
        {
            return bills.Where(o => (o.BillType == (byte)EnumBillKind.Normal
                || o.BillType == (byte)EnumBillKind.Guarantee) && !o.IsShelve).ToList();
        }

        /// <summary>
        /// Author:王正吉
        /// Description:获取非注销帐单集合
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public static List<Bill> GetValidBills(List<Bill> bills)
        {
            return bills.Where(o => o.BillType != (byte)EnumBillKind.Annul
                && !o.IsShelve).ToList();
        }

        /// <summary>
        /// Author:王正吉
        /// Description:获取欠费帐单集合
        /// </summary>
        /// <param name="bills"></param>
        /// <param name="isnormal">是否是普通账单{true:普通账单 false:全选}</param>
        /// <returns></returns>
        public static List<Bill> GetArrearsBills(List<Bill> bills, bool isnormal = false)
        {
            if (isnormal)
                bills = GetNormalBills(bills);
            return bills.Where(p => p.BillStatus == (byte)EnumBillStatus.NoPay
                        || p.BillStatus == (byte)EnumBillStatus.PartPay)
                        .Where(p => p.BillType != (byte)EnumBillKind.Annul
                        && !p.IsShelve).ToList();
        }

        /// <summary>
        /// Author:李光明
        /// Description:获取欠费帐单集合并添加外贸权限条件
        /// </summary>
        /// <param name="bills"></param>
        /// <param name="isnormal">是否是普通账单{true:普通账单 false:全选}</param>
        /// <returns></returns>
        public static List<Bill> GetArrearsBills(List<Bill> bills, string lendKey, bool isnormal = false)
        {
            if (isnormal)
                bills = GetNormalBills(bills);
            return bills.Where(p => p.BillStatus == (byte)EnumBillStatus.NoPay
                        || p.BillStatus == (byte)EnumBillStatus.PartPay)
                        .Where(p => p.BillType != (byte)EnumBillKind.Annul
                        && !p.IsShelve && p.Business.LendingSideKey == lendKey).ToList();
        }

        /// <summary>
        /// Author:王正吉
        /// Description:获取全额支付帐单集合
        /// </summary>
        /// <param name="bills"></param>
        /// <param name="isnormal">是否是普通账单{true:普通账单 false:全选}</param>
        /// <returns></returns>
        public static List<Bill> GetFullPayBills(List<Bill> bills, bool isnormal = false)
        {
            if (isnormal)
                bills = GetNormalBills(bills);
            return bills.Where(p => p.BillStatus == (byte)EnumBillStatus.FullPay).ToList();
        }

        /// <summary>
        /// Author:王正吉
        /// Description:获取当期帐单
        /// </summary>
        public static Bill GetCurrentBill(List<Bill> bills)
        {
            return GetNormalBills(bills).FirstOrDefault(o => o.IsCurrent);
        }

        /// <summary>
        /// Author:王正吉
        /// Description:获取逾期帐单
        /// </summary>
        public static List<Bill> GetOverdueBills(List<Bill> bills)
        {
            return GetNormalBills(bills).Where(o => !o.IsCurrent)
                .OrderByDescending(o => o.LimitTime).ToList();
        }

        /// <summary>
        /// Author:王正吉
        /// Description:获取上期帐单
        /// </summary>
        public static Bill GetPrevBill(List<Bill> bills)
        {
            return GetOverdueBills(bills).FirstOrDefault();
        }
        /// <summary>
        /// Author:陈伟
        /// Description:判断是否是小m
        /// </summary>
        public static bool IsSmallm(List<Bill> bills)
        {
            return bills.Sum(p => p.BillItems.Where(o => o.Subject == (byte)EnumCostSubject.Capital ||
                o.Subject == (byte)EnumCostSubject.Interest ||
                o.Subject == (byte)EnumCostSubject.ServiceFee ||
                o.Subject == (byte)EnumCostSubject.GuaranteeFee).Sum(x => x.DueAmt - x.ReceivedAmt)) == 0;
        }
        #endregion
    }
}

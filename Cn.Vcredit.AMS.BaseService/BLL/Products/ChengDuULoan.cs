using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Dictionary;

namespace Cn.Vcredit.AMS.BaseService.BLL.Products
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年6月8日
    /// Description:成都小额贷款
    /// </summary>
    public class ChengDuULoan : VcreditProduct
    {
		#region- 字段属性 -
		/// <summary>
		/// 科目优先级
		/// </summary>
        private List<List<EnumCostSubject>> _Subjects;
        public List<List<EnumCostSubject>> Subjects
		{
			get
			{
				if (_Subjects == null)
				{
                    _Subjects = new List<List<EnumCostSubject>>
					{ 
					    new	List<EnumCostSubject>{EnumCostSubject.ServiceFee},
						new	List<EnumCostSubject>{EnumCostSubject.ServiceBuckleFail},
						new	List<EnumCostSubject>{EnumCostSubject.InterestBuckleFail},
						new	List<EnumCostSubject>{EnumCostSubject.PunitiveInterest},
						new	List<EnumCostSubject>{EnumCostSubject.Interest,EnumCostSubject.Capital}
					};
				}
				return _Subjects;
			}
		}
		#endregion

        #region- 构造函数 -
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="business"></param>
        public ChengDuULoan(Business business) : base(business) { }
        #endregion

        #region- 功能函数 -
		/// <summary>
		/// 创建基本科目（包括本金、利息、管理费）
		/// </summary>
		/// <param name="business">业务对象</param>
		/// <returns>返回款项集合</returns>
		public override List<BillItem> CreateBaseItems(bool isLast = false)
		{
			List<BillItem> billItem = new List<BillItem>();

			// 添加信托方扣款项目
			if (Business.LendingSideID > 0)
			{
				var capital = SubjectFormula.CalculatCapital(Business.LoanCapital, Business.LoanPeriod);
				var interest = SubjectFormula.CalculatInterest(Business.LoanCapital, Business.InterestRate);

				// 判断当期帐单属于第几期帐单,最后一期普通帐单（本金按余额计）
				if (isLast)
				{
					capital = SubjectFormula.CalculatCapitalOver(Business.LoanCapital, Business.LoanPeriod);
				}
				billItem.Add(CreateBillItem(EnumCostSubject.Capital, capital));
				billItem.Add(CreateBillItem(EnumCostSubject.Interest, interest));
			}

			// 添加管理方扣款项目
			if (Business.ServiceSideID > 0)
			{
				var mangeFee = SubjectFormula.CalculatServiceFee(Business.LoanCapital, Business.ServiceRate, false);
				billItem.Add(CreateBillItem(EnumCostSubject.ServiceFee, mangeFee));
			}

			return billItem;
		}

		/// <summary>
		/// 创建扣失科目（因逾期而产生的本息扣失、管理费扣失）
		/// </summary>
		public override List<BillItem> CreateBuckleFail(DateTime limitTime)
		{
			List<BillItem> items = new List<BillItem>();

			// 获取当期帐单
			var curBill = BillDAL.GetNormalBills(Business.Bills).FirstOrDefault(p => p.BillMonth == _CurMonth);

			// 无当期帐单 或者 有全额支付时间且全额支付时间是在扣失日之前（包括当天）的则不生成扣失
			if (curBill == null || (limitTime >= curBill.FullPaidTime && curBill.FullPaidTime != null))
				return items;

			// 判断当期帐单是否应生成本息扣失
			List<byte> subjects = new List<byte> { (byte)EnumCostSubject.Capital, (byte)EnumCostSubject.Interest };
			if (IsCreateBuckleFail(subjects, curBill))
				items.Add(CreateBillItem(EnumCostSubject.InterestBuckleFail,
					SubjectFormula.CalculatBuckleFail(Business.ContractNo), true, curBill.BillID));

			// 判断当期帐单是否应生成服务费担保费扣失
			subjects = new List<byte> { (byte)EnumCostSubject.ServiceFee, (byte)EnumCostSubject.GuaranteeFee };
			if (IsCreateBuckleFail(subjects, curBill))
				items.Add(CreateBillItem(EnumCostSubject.ServiceBuckleFail,
					SubjectFormula.CalculatBuckleFail(Business.ContractNo), true, curBill.BillID));

			return items;
		}

		/// <summary>
		/// 创建罚息科目（因逾期而产生的上期帐单罚息）
		/// 费用算法：一次性为上期帐单生成上月20日至今的罚息合计
		/// </summary>
		/// <param name="business">订单对象</param>
		/// <returns>返回罚息款项</returns>
		public override BillItem CreatePunitiveInterest(DateTime limitTime, List<PenaltyInt> penaltyInts)
		{
			// 获取当期帐单，为NULL或已在第三次扣失日前全额付清则继续循环
			var nbills = BillDAL.GetNormalBills(Business.Bills);
			var prevBill = nbills.FirstOrDefault(p => p.BillMonth == _PrevMonth);
			var curBill = nbills.FirstOrDefault(p => p.BillMonth == _CurMonth);

			// 判断是否存在原因帐单或当期帐单，不存在则返回NULL
			if (prevBill == null || curBill == null || Business.Bills.Count == 1
				|| (limitTime >= prevBill.FullPaidTime && prevBill.FullPaidTime != null))
				return null;

			// 获取本金、利息、管理费等参与科目，计算上期所欠本息或租金
			var subjects = new List<byte> { (byte)EnumCostSubject.Capital, (byte)EnumCostSubject.Interest };

			// 获取上期帐单的本息欠费
			decimal debtsPrincipal = prevBill.BillItems.Where(p => subjects.Contains(p.Subject))
				.Sum(s => s.DueAmt - s.ReceivedAmt);

			// 判断上期帐单是否存在未归还所欠本息或租金，无则不生成罚息
			if (debtsPrincipal <= 0)
				return null;

			var beginDay = DateTime.Now.AddMonths(-1).SetDay(21);
			// 在罚息表中计算罚息金额
			CalculatPenaltyInt(penaltyInts, debtsPrincipal, prevBill.BillID, curBill.BillID, beginDay);

			// 当所欠本息大于零时，计算从上月21日至今天数的罚息
			var totalDays = DateTime.Now - beginDay;
			debtsPrincipal = SubjectFormula.CalculatPunitiveInterest(debtsPrincipal,
				Business.PenaltyRate, totalDays.Days);


			// 判断欠费帐单是否存在未归还所欠本息或租金，无则不生成罚息
			if (debtsPrincipal <= 0)
				return null;

			// 创建罚息科目（科目名称、金额）
			return CreateBillItem(EnumCostSubject.PunitiveInterest, debtsPrincipal, false, curBill.BillID);
		}

        /// <summary>
        /// 获取信用贷款所有基本款项
        /// </summary>
        /// <param name="business">业务对象</param>
        /// <returns>返回款项集合</returns>
        public override List<BillItem> GetBaseItemsByMonth()
        {
            List<BillItem> billItem = new List<BillItem>();

            // 添加信托方扣款项目（本金、利息、管理费）
            if (Business.LendingSideID > 0)
            {
                var capital = SubjectFormula.CalculatCapital(Business.LoanCapital, Business.LoanPeriod);
                billItem.Add(CreateBillItem(EnumCostSubject.Capital, capital));
                var interest = SubjectFormula.CalculatInterest(Business.LoanCapital, Business.InterestRate);
                billItem.Add(CreateBillItem(EnumCostSubject.Interest, interest));
                var mangeFee = SubjectFormula.CalculatServiceFee(Business.LoanCapital, Business.ServiceRate, false);
                billItem.Add(CreateBillItem(EnumCostSubject.ServiceFee, mangeFee));
            }

            return billItem;
        }

        /// <summary>
        /// 获取合同参与方
        /// </summary>
        /// <returns></returns>
        public override Dictionary<EnumContractSide, string> GetProductCompanys()
        {
            Dictionary<EnumContractSide, string> dic = new Dictionary<EnumContractSide, string>();
            dic.Add(EnumContractSide.Manage, Business.LendingSideKey);
            return dic;
        }

        /// <summary>
        /// 获取合同科目字典
        /// </summary>
        /// <returns>款项字典</returns>
        public override Dictionary<byte, string> GetProductItems()
        {
            return Singleton<SubjectDictionary>.Instance.ChengDuUnsecuredDic;
        }

		/// <summary>
		/// 获取可足科填充科目（手工扣款规则）
		/// </summary>
		/// <param name="amount"></param>
		/// <returns></returns>
		public override List<BillItem> GetFullPaymentItems(decimal amount)
		{
            List<Bill> bills = BillDAL.GetArrearsBills(Business.Bills).OrderBy(o => o.BillMonth).ToList();
            List<BillItem> items = new List<BillItem>();
            foreach (Bill bill in bills)
            {
                foreach (var subject in Subjects)
                {
                    var item = bill.BillItems.Where(p =>
                        subject.Contains(p.Subject.ValueToEnum<EnumCostSubject>()) && !p.IsShelve);
                    var amt = item.Sum(p => p.DueAmt - p.ReceivedAmt);
                    if (amt == 0)
                        continue;
                    if (amount >= amt)
                    {
                        amount -= amt;
                        items.AddRange(item);
                    }
                    else
                        return items;//手工扣款规则若发现无法填帐则直接停止遍历并返回
                    if (amount == 0)
                        break;
                }
                if (amount == 0)
                    break;
            }
            return items;
		}

		/// <summary>
		/// 获取可足科填充科目（自动填帐规则）
		/// </summary>
		/// <param name="amount"></param>
		/// <returns></returns>
        public override List<BillItem> GetFillItems(decimal amount)
        {
            List<Bill> bills = BillDAL.GetArrearsBills(Business.Bills).OrderBy(o => o.CreateTime).ToList();
            List<BillItem> items = new List<BillItem>();
            foreach (Bill bill in bills)
            {
                foreach (var subject in Subjects)
                {
                    var item = bill.BillItems.Where(p =>
                        subject.Contains(p.Subject.ValueToEnum<EnumCostSubject>()) && !p.IsShelve);
                    var amt = item.Sum(p => p.DueAmt - p.ReceivedAmt);
                    if (amt <= 0)
                        continue;
                    if (amount >= amt)
                    {
                        amount -= amt;
                        items.AddRange(item);
                    }
                    if (amount == 0)
                        break;
                }
                if (amount == 0)
                    break;
            }
            return items;
        }

        /// <summary>
        /// 获取单笔扣款收款帐户编号
        /// 规则：
        /// 1、收款归放贷方
        /// </summary>
        /// <returns></returns>
        public override int GetAccountPayeeID(EnumCostSubject subject)
        {
            return Business.LendingSideID;
        }
        #endregion

        /// <summary>
        /// 生成本息扣失的科目
        /// </summary>
        public override List<byte> InterestBuckleFailSubjects
        {
            get
            {
                // 判断当期帐单是否应生成本息扣失
                List<byte> subjects = new List<byte> { (byte)EnumCostSubject.Capital, (byte)EnumCostSubject.Interest };

                return subjects;
            }
        }
        /// <summary>
        /// 生成服务费扣失的科目
        /// </summary>
        public override List<byte> ServiceBuckleFailSubjects
        {
            get
            {
                // 判断当期帐单是否应生成本息扣失
                List<byte> subjects = new List<byte> { (byte)EnumCostSubject.ServiceFee, (byte)EnumCostSubject.GuaranteeFee };

                return subjects;
            }
        }
        /// <summary>
        /// 生成罚息的科目
        /// </summary>
        public override List<byte> PunitiveInterestSubjects
        {
            get
            {
                // 获取本金、利息或租金等参与科目，计算上期所欠本息或租金
                var subjects = new List<byte> { (byte)EnumCostSubject.Capital, (byte)EnumCostSubject.Interest, (byte)EnumCostSubject.ServiceFee };
                return subjects;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.BaseService.Common;

namespace Cn.Vcredit.AMS.BaseService.BLL.Products
{
    public abstract class VcreditProduct
    {
        #region- 字段属性 -
        /// <summary>
        /// 业务对象
        /// </summary>
        protected Business Business { get; set; }

        #region 还款计算添加(wichell 2014年8月7日 13:56:29)
        /// <summary>
        /// 生成本息扣失的科目集合
        /// </summary>
        public abstract List<byte> InterestBuckleFailSubjects { get; }

        /// <summary>
        /// 生成服务费扣失的科目集合
        /// </summary>
        public abstract List<byte> ServiceBuckleFailSubjects { get; }

        /// <summary>
        /// 生成罚息的科目集合
        /// </summary>
        public abstract List<byte> PunitiveInterestSubjects { get; }
        #endregion

        /// <summary>
        /// 上月
        /// </summary>
        protected string _PrevMonth = DateTime.Now.AddMonths(-1).ToBillMonthString();

        /// <summary>
        /// 当月
        /// </summary>
        protected string _CurMonth = DateTime.Now.ToBillMonthString();
        #endregion

        #region- 构造函数 -
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="business"></param>
        protected VcreditProduct(Business business)
        {
            Business = business;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="business"></param>
        protected VcreditProduct()
        {
        }
        #endregion

        #region- 抽象函数 -
        /// <summary>
        /// 创建基本款项
        /// </summary>
        /// <param name="business">业务对象</param>
        /// <returns>返回款项集合</returns>
        public abstract List<BillItem> CreateBaseItems(bool isLast = false);

        /// <summary>
        /// 获取所有产品款项
        /// </summary>
        /// <returns>返回款项字典</returns>
        public abstract Dictionary<byte, string> GetProductItems();

        /// <summary>
        /// 获取所有特定产品基本款项
        /// </summary>
        /// <param name="business">业务对象</param>
        /// <returns>返回款项集合</returns>
        public abstract List<BillItem> GetBaseItemsByMonth();

        /// <summary>
        /// 创建扣失科目
        /// </summary>
        /// <returns></returns>
        public abstract List<BillItem> CreateBuckleFail(DateTime limitTime);

        /// <summary>
        /// 创建合同约定的违约金
        /// </summary>
        /// <returns></returns>
        public abstract BillItem CreatePunitiveInterest(DateTime limitTime, List<PenaltyInt> penaltyInts);

		/// <summary>
		/// 设置填充金额，计算可填充科目（手工扣款填充规则）
		/// </summary>
		/// <param name="amount">自定义金额</param>
		/// <returns></returns>
		public abstract List<BillItem> GetFullPaymentItems(decimal amount);

		/// <summary>
		/// 设置填充金额，计算可填充科目（自动填充规则）
		/// </summary>
		public abstract List<BillItem> GetFillItems(decimal amount);

        /// <summary>
        /// 获得收款帐户编号
        /// </summary>
        /// <returns></returns>
        public abstract int GetAccountPayeeID(EnumCostSubject subject);
        #endregion

        #region- 功能函数 -
        /// <summary>
        /// 计算日罚息区间
        /// </summary>
        protected void CalculatPenaltyInt(List<PenaltyInt> penaltyInts, decimal debtsPrincipal,
            long reasonId, long toBillId, DateTime beginDay)
        {
            var interval = DateTime.Now.SetDay(1);

            // 罚息从上月21日开始起算，分隔日为上月月末，计算罚息金额
            var prevDays = interval - beginDay;
            var prevAmount = SubjectFormula.CalculatPunitiveInterest(debtsPrincipal,
                Business.PenaltyRate, prevDays.Days);

            // 罚息从当月月头开始起算，分隔日为今天，计算罚息金额
            var curDays = DateTime.Now - interval;
            var curAmount = SubjectFormula.CalculatPunitiveInterest(debtsPrincipal,
                Business.PenaltyRate, curDays.Days);

            // 在罚息表中插入上月罚息金额和当月罚息金额
            penaltyInts.Add(NewPenaltyInt(Business.BusinessID, reasonId, toBillId, prevAmount, false, interval.AddDays(-1)));
            penaltyInts.Add(NewPenaltyInt(Business.BusinessID, reasonId, toBillId, curAmount));
        }

        /// <summary>
        /// 创建罚息实体
        /// </summary>
        private PenaltyInt NewPenaltyInt(int businessId, long reasonId, long toBillId,
            decimal amount, bool isShelve = false, DateTime? time = null)
        {
            return new PenaltyInt
            {
                BusinessID = businessId,
                ReasonID = reasonId,
                ToBillID = toBillId,
                Amount = amount,
                IsShelve = isShelve,
                CreateTime = time.HasValue ? time.Value : DateTime.Now
            };
        }

        /// <summary>
        /// 是否创建扣失费用
        /// </summary>
        /// <param name="subjects">科目类型</param>
        /// <returns>返回布尔值</returns>
        protected bool IsCreateBuckleFail(List<byte> subjects, Bill currentBill)
        {
            return currentBill.BillItems.Where(o => subjects.Contains(o.Subject))
                .Sum(p => p.DueAmt - p.ReceivedAmt) > 0;
        }

        /// <summary>
        /// 创建帐单款项
        /// </summary>
        /// <param name="subject">款项科目</param>
        /// <param name="amount">款项金额</param>
        /// <returns>返回帐单款项</returns>
        protected static BillItem CreateBillItem(EnumCostSubject subject, decimal amount,
            bool isCurrent = true, long billID = 0)
        {
            return CreateBillItem(subject, EnumSubjectKind.Normal, amount, isCurrent, billID);
        }

        /// <summary>
        /// 创建帐单款项
        /// </summary>
        /// <param name="subject">款项科目</param>
        /// <param name="kind">款项性质</param>
        /// <param name="amount"></param>
        /// <returns>返回帐单款项</returns>
        public static BillItem CreateBillItem(EnumCostSubject subject,
            EnumSubjectKind kind, decimal amount, bool isCurrent, long billID)
        {
            return new BillItem
            {
                Subject = (byte)subject,
                Amount = amount,
                DueAmt = amount,
                CreateTime = DateTime.Now,
                SubjectType = (byte)kind,
                BillID = billID,
                IsCurrent = isCurrent,
                IsShelve = false,
                //DunLevel = (byte)level
            };
        }

        /// <summary>
        /// 获取所有特定产品相应的合同方
        /// </summary>
        /// <param name="business">业务对象</param>
        /// <returns>返回合同方集合</returns>
        public virtual Dictionary<EnumContractSide, string> GetProductCompanys()
        {
            Dictionary<EnumContractSide, string> dic = new Dictionary<EnumContractSide, string>();
            if (Business.LendingSideID > 0)
                dic.Add(EnumContractSide.Lend, Business.LendingSideKey);
            if (Business.ServiceSideID > 0)
                dic.Add(EnumContractSide.Service, Business.ServiceSideKey);
            if (Business.GuaranteeSideID > 0)
                dic.Add(EnumContractSide.Guarantee, Business.GuaranteeSideKey);
            return dic;
        }

        /// <summary>
        /// 获取所有款项统计信息
        /// isCurrent: true:当期欠费科目集合 false:逾期欠费集合
        /// </summary>
        /// <returns></returns>
        public virtual List<BillItem> GetStatisticsItems(bool? isCurrent = false)
        {
            var bills = BillDAL.GetNormalBills(Business.Bills);

            List<BillItem> billItems = new List<BillItem>();
            bills.ForEach(o =>
            {
                if (o.BillItems != null)
                    billItems.AddRange(o.BillItems);
            });

            List<BillItem> tempItems = new List<BillItem>();
            var pdtItems = GetProductItems();
            foreach (var pdtItem in pdtItems)
            {
                BillItem item = new BillItem();
                var filterItems = billItems.Where(p => p.Subject == pdtItem.Key).ToList();
                if (isCurrent != null)
                    filterItems = filterItems.Where(p => p.IsCurrent == isCurrent.Value).ToList();
                item.Amount = filterItems.Sum(p => p.Amount);
                item.DueAmt = filterItems.Sum(p => p.DueAmt);
                item.ReceivedAmt = filterItems.Sum(p => p.ReceivedAmt);
                item.StrSubject = pdtItem.Value.Replace("月", string.Empty);
                item.Subject = pdtItem.Key;
                tempItems.Add(item);
            }

            return tempItems;
        }

        /// <summary>
        /// 获取其他款项（担保金、担保违约金、诉讼费、诉讼违约金）
        /// </summary>
        /// <param name="business"></param>
        /// <returns></returns>
        public virtual List<BillItem> GetOtherItems()
        {
            List<EnumBillKind> billKinds = new List<EnumBillKind>{
				//EnumBillKind.AddGuarantee,
				EnumBillKind.AddLitigation
			};

            List<BillItem> billItems = new List<BillItem>();
            foreach (var bill in Business.Bills)
            {
                if (billKinds.Contains(bill.BillType.ValueToEnum<EnumBillKind>()))
                {
                    billItems.AddRange(bill.BillItems);
                }
            }

            return billItems;
        }
        #endregion
    }
}

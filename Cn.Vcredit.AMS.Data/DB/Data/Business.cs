using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:自动创建（陈伟、王正吉）
    /// CreateTime:2012-6-6 13:58:02
    /// Description:业务信息（由贷前提供，贷后维护）
    /// </summary>
    public class Business
    {
        #region 老版的数据结构

        #region- 基本属性 -
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 扣款序列类型（[关联DeductSequence表]：1、21/28/12 2、21/30/5/15 3、浮动）
        /// </summary>
        public byte DSeqType { get; set; }
        /// <summary>
        /// 帐单周期类型（1、自然月型 12、12日型 32、放贷日隔月型）
        /// </summary>
        public byte PeriodType { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public int SalesManID { get; set; }
        /// <summary>
        /// 产品类型（[Net枚举] 1、融资租赁 2、车辆抵押贷款  3、房屋抵押贷款 4、小额贷款 5、成都小额贷款 6、陆金所贷款）
        /// </summary>
        public byte ProductType { get; set; }
        /// <summary>
        /// 贷款本金
        /// </summary>
        public decimal LoanCapital { get; set; }
        /// <summary>
        /// 剩余本金
        /// </summary>
        public decimal ResidualCapital { get; set; }
        /// <summary>
        /// 贷款期数
        /// </summary>
        public int LoanPeriod { get; set; }
        /// <summary>
        /// 放贷日
        /// </summary>
        public DateTime LoanTime { get; set; }
        /// <summary>
        /// 月利率
        /// </summary>
        public decimal InterestRate { get; set; }
        /// <summary>
        /// 月服务费率
        /// </summary>
        public decimal ServiceRate { get; set; }
        /// <summary>
        /// 日罚息率
        /// </summary>
        public decimal PenaltyRate { get; set; }
        /// <summary>
        /// 首期保费（适用车贷）
        /// </summary>
        public decimal AdvanceFee { get; set; }
        /// <summary>
        /// 保证金
        /// </summary>
        public decimal EarnestAmt { get; set; }
        /// <summary>
        /// 保险费(适用融资租赁)
        /// </summary>
        public decimal Premium { get; set; }
        /// <summary>
        /// 是否是二次营销
        /// </summary>
        public string SecondSales { get; set; }
        /// <summary>
        /// 业务状态（[Net枚举]：1、正常 2、担保 3、诉讼）
        /// </summary>
        public byte BusinessStatus { get; set; }
        /// <summary>
        /// 清贷状态（[Net枚举]：1、偿还中  2、满约清贷 3、提前清贷 4、坏帐清贷）
        /// </summary>
        public byte CLoanStatus { get; set; }
        /// <summary>
        /// 诉讼执行状态([Net枚举]：1、未诉讼 2、诉讼中 3、诉讼完成 4、申请执行 5、执行一次 6、执行二次)。
        /// </summary>
        public byte LawsuitStatus { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 银行键名
        /// </summary>
        public string BankKey { get; set; }
        /// <summary>
        /// 储蓄卡号
        /// </summary>
        public string SavingCard { get; set; }
        /// <summary>
        /// 储蓄用户
        /// </summary>
        public string SavingUser { get; set; }

        /// <summary>
        /// 信托方公司键名
        /// </summary>
        public string LendingSideKey { get; set; }

        /// <summary>
        /// 信托方银行编号
        /// </summary>
        public int LendingSideID { get; set; }

        /// <summary>
        /// 服务方公司键名
        /// </summary>
        public string ServiceSideKey { get; set; }

        /// <summary>
        /// 服务方银行编号
        /// </summary>
        public int ServiceSideID { get; set; }

        /// <summary>
        /// 担保方键名
        /// </summary>
        public string GuaranteeSideKey { get; set; }

        /// <summary>
        /// 担保方编号
        /// </summary>
        public int GuaranteeSideID { get; set; }

        /// <summary>
        /// 建行号
        /// </summary>
        public string ConstructionBankNo { get; set; }

        /// <summary>
        /// 借款借据版本号
        /// </summary>
        public string ReceiptVersion { get; set; }

        /// <summary>
        /// 分公司
        /// </summary>
        public string BranchKey { get; set; }

        /// <summary>
        /// 欠费金额（不含当期）
        /// </summary>
        public decimal OverAmount { get; set; }

        /// <summary>
        /// 当期欠费总金额
        /// </summary>
        public decimal CurrentOverAmount { get; set; }

        /// <summary>
        /// 其他金额(担保-诉讼金额)
        /// </summary>
        public decimal OtherAmount { get; set; }

        /// <summary>
        /// 逾期数
        /// </summary>
        public byte OverMonth { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 产品种类
        /// </summary>
        public string LoanKind { get; set; }

        /// <summary>
        /// 支付日期
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// 转担保时间
        /// </summary>
        public DateTime? ToGuaranteeTime { get; set; }

        /// <summary>
        /// 转诉讼时间
        /// </summary>
        public DateTime? ToLitigationTime { get; set; }

        /// <summary>
        /// 清贷时间
        /// </summary>
        public DateTime? ClearLoanTime { get; set; }

        /// <summary>
        /// 是否在偿还中
        /// </summary>
        public bool IsRepayment { get; set; }

        /// <summary>
        /// 判决案号
        /// </summary>
        public string LawsuitCode { get; set; }

        /// <summary>
        /// 手续费率
        /// </summary>
        public decimal ProceduresRate { set; get; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal ProceduresAmout { set; get; }

        /// <summary>
        /// 是否全额开票
        /// </summary>
        public bool IsFullInvoice { set; get; }

        /// <summary>
        /// 转担保批次号
        /// </summary>
        public string GuaranteeNum { set; get; }

        /// <summary>
        /// 冻结批次号
        /// </summary>
        public string FrozenNo { get; set; }

        /// <summary>
        /// 建行号(二)
        /// </summary>
        public string ConstructSedNo { get; set; }

        /// <summary>
        /// 销售团队
        /// </summary>
        public string SalesTeam { get; set; }

        /// <summary>
        /// 保证金率
        /// </summary>
        public decimal DepositRate { get; set; }

        /// <summary>
        /// 签约时间
        /// </summary>
        public DateTime SignTime { get; set; }

        /// <summary>
        /// 签约照片
        /// </summary>
        public string DucImgPath { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string FromSource { get; set; }

        /// <summary>
        /// 银行支行
        /// </summary>
        public string SubBranch { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductKind { get; set; }

        /// <summary>
        /// 订单交单时间
        /// </summary>
        public DateTime? SubmitTime { get; set; }

        /// <summary>
        /// 本息扣失
        /// </summary>
        public decimal? PrincipalPunish { get; set; }

        /// <summary>
        ///服务费扣失
        /// </summary>
        public decimal? ServicePunish { get; set; }

        /// <summary>
        /// 下一扣款日
        /// </summary>
        public DateTime? NextBillDate { get; set; }

        /// <summary>
        /// 销售模式
        /// </summary>
        public string SaleMode { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 是否可操作
        /// </summary>
        public byte Operable { get; set; }

        /// <summary>
        /// 清贷原因
        /// </summary>
        public string ClearLoanType { get; set; }

        /// <summary>
        /// 清贷原因备注
        /// </summary>
        public string ClearLoanRemark { get; set; }

        /// <summary>
        /// 档案袋编号
        /// </summary>
        //public string FileNum { get; set; }

        /// <summary>
        /// 特殊政策
        /// </summary>
        public string SpecailPolicy { get; set; }

        /// <summary>
        /// 月本金率
        /// </summary>
        public decimal? CapitalRate { get; set; }

        /// <summary>
        /// 转担保金额
        /// </summary>
        public decimal? ToGuaranteeAmt { get; set; }

        /// <summary>
        /// 资金来源
        /// </summary>
        public string FundSource { get; set; }

        /// <summary>
        /// 月管理费率
        /// </summary>
        public decimal? ManagementRate { get; set; }
        #endregion

        #region- 扩展属性 -

        /// <summary>
        /// 业务对应的客户实体
        /// </summary>
        public virtual Customer Customer { get; set; }

        ///// <summary>
        ///// 预减免集合
        ///// </summary>
        //public virtual List<AdvRelief> AdvReliefs { get; set; }

        /// <summary>
        /// 帐单集合
        /// </summary>
        public virtual List<Bill> Bills { get; set; }

        ///// <summary>
        ///// 订单存款
        ///// </summary>
        //public virtual AdvReceived AdvReceived { get; set; }

        /// <summary>
        /// 扣款相对日日期
        /// </summary>
        public virtual DateTime RelativeDate { get; set; }

        ///// <summary>
        ///// 代款卡集合
        ///// </summary>
        //public virtual List<AdaptationCard> AdaptationCards { get; set; }

        /// <summary>
        /// 订单状态（客户类型）
        /// </summary>
        public string StrBusinessStatus
        {
            get
            {
                return BusinessStatus.ValueToDesc<EnumBusinessStatus>();
            }
        }
        /// <summary>
        /// 诉讼状态（客户状态）
        /// </summary>
        public string StrLawsuitStatus
        {
            get
            {
                return LawsuitStatus.ValueToDesc<EnumLawsuitStatus>();
            }
        }
        /// <summary>
        /// 清贷状态
        /// </summary>
        public string StrCLoanStatus
        {
            get
            {
                return CLoanStatus.ValueToDesc<EnumCLoanStatus>();
            }
        }
        /// <summary>
        /// 逾期月状态显示
        /// </summary>
        public string StrOverMonth
        {
            get { return OverMonth.ValueToDesc<EnumDunMark>(); }
        }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 地区名称
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 产品种类名称
        /// </summary>
        public string LoanKindName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityNo { get; set; }
        /// <summary>
        /// 最近的一次关帐时间
        /// </summary>
        public DateTime LatestTime { get; set; }
        #endregion
        #endregion

        #region 新版数据结构（暂停使用）
        //#region- 基本属性 -
        ///// <summary>
        ///// 业务编号
        ///// </summary>
        //public int BusinessID { get; set; }

        ///// <summary>
        ///// 订单基础表
        ///// </summary>
        //public BusinessBasic Basic { get; set; }

        ///// <summary>
        ///// 订单扩展表
        ///// </summary>
        //public BusinessExtend Extend { get; set; }

        ///// <summary>
        ///// 订单当前状态表
        ///// </summary>
        //public BusinessCurrentStaus CurrentStaus { get; set; }
        //#endregion

        //#region 扩展属性
        ///// <summary>
        ///// 帐单集合
        ///// </summary>
        //public virtual List<Bill> Bills { get; set; }
        //#endregion
        #endregion
    }
}

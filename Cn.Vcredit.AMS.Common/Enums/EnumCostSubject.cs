using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年4月17日
    /// Description:费用科目（扣款项目）
    /// </summary>
    public enum EnumCostSubject : byte
    {
        /// <summary>
        /// 全选
        /// </summary>
        [Description("")]
        Default = 0,

        #region- 信用贷款(上海、杭州、苏州) -
        /// <summary>
        /// 月本金
        /// </summary>
        [Description("月本金")]
        Capital = 1,

        /// <summary>
        /// 月利息
        /// </summary>
        [Description("月利息")]
        Interest = 2,

        /// <summary>
        /// 月服务费
        /// </summary>
        [Description("月服务费")]
        ServiceFee = 3,

        /// <summary>
        /// 月担保费
        /// </summary>
        [Description("月担保管理费")]
        GuaranteeFee = 4,

        /// <summary>
        /// 月本息扣失
        /// </summary>
        [Description("本息扣失")]
        InterestBuckleFail = 21,

        /// <summary>
        /// 月服务费扣失
        /// </summary>
        [Description("服务费扣失")]
        ServiceBuckleFail = 22,

        /// <summary>
        /// 罚息
        /// </summary>
        [Description("罚息")]
        PunitiveInterest = 23,
        #endregion

        #region- 信用贷款(成都) -
        /// <summary>
        /// 月管理费（仅在成都中使用）
        /// </summary>
        [Description("月管理费")]
        Manage = 9,

        /// <summary>
        /// 延迟支付违约金
        /// </summary>
        //[Description("延迟支付违约金")]
        //Penalbond = 28,
        #endregion

        #region- 暂未使用 -
        /// <summary>
        /// 月预缴保费（仅在车贷中使用）
        /// </summary>
        [Description("预缴保费")]
        AdvanceFee = 6,

        /// <summary>
        /// 月租金（仅在融资租赁中使用）
        /// </summary>
        [Description("月租金")]
        Rent = 7,

        /// <summary>
        /// 月保险费（仅在融资租赁中使用）
        /// </summary>
        [Description("月保险费")]
        Premium = 8,
        #endregion

        #region- 提前清贷款项 -
        /// <summary>
        /// 加收利息	
        /// </summary>
        [Description("加收利息")]
        AddInterest = 28,

        /// <summary>
        /// 加收服务费
        /// </summary>
        [Description("加收服务费")]
        AddServiceFee = 29,

        /// <summary>
        /// 加收担保费
        /// </summary>
        [Description("加收担保管理费")]
        AddGuaranteeFee = 36,

        /// <summary>
        /// 清贷服务费
        /// </summary>
        [Description("清贷服务费")]
        AdvCleanLoanServiceFee = 30,

        /// <summary>
        /// 提前清贷服务费
        /// </summary>
        [Description("提前清贷服务费")]
        AdvServiceFee = 35,

        /// <summary>
        /// 剩余本金
        /// </summary>
        [Description("剩余本金")]
        ResidualCapital = 31,

        /// <summary>
        /// 清贷减免金额
        /// </summary>
        [Description("清贷减免金额")]
        ReduceAmount = 32,
        #endregion

        #region -坏账款项-
        /// <summary>
        /// 剩余利息
        /// </summary>
        [Description("剩余利息")]
        ResidualInterest = 33,

        /// <summary>
        /// 坏账实收金额
        /// </summary>
        [Description("坏账实收金额")]
        BadReceviedAmount = 34,
        #endregion

        #region- 担保/诉讼款项 -
        ///// <summary>
        ///// 担保金
        ///// </summary>
        //[Description("担保金")]
        //Guarantee = 24,

        /// <summary>
        /// 担保违约金
        /// </summary>
        [Description("担保违约金")]
        GuaranteeLateFee = 25,

        /// <summary>
        /// 诉讼费
        /// </summary>
        [Description("诉讼费")]
        Litigation = 26,

        /// <summary>
        /// 诉讼违约金
        /// </summary>
        [Description("诉讼违约金")]
        LitigationLateFee = 27,
        #endregion

        #region- 抵押贷款 -
        /// <summary>
        /// 保费
        /// </summary>
        [Description("保费")]
        Insure = 37,
        #endregion

        #region 手续费、保证金
        [Description("手续费")]
        Procedures = 10,
        [Description("保证金")]
        Earnest = 11
        #endregion
    }
}

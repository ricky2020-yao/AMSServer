using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:订单当前状态表
    /// </summary>
    public class BusinessCurrentStaus
    {
        #region- 基本属性 -
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 总本金
        /// </summary>
        public decimal Capital { get; set; }

        /// <summary>
        /// 未还本金
        /// </summary>
        public decimal ResidualCapital { get; set; }

        /// <summary>
        /// 未还本金
        /// </summary>
        public decimal NoOutOfCapital { get; set; }

        /// <summary>
        /// 业务状态（[Net枚举]：1、正常 2、担保 3、诉讼）4、解约
        /// </summary>
        public byte BusinessStatus { get; set; }

        /// <summary>
        /// 清贷状态（[Net枚举]：1、偿还中  2、满约清贷 3、提前清贷 4、坏帐清贷 
        /// 5、提前清贷申请 6、坏账申请 7、提前清贷中 8、注销订单）
        /// </summary>
        public byte CLoanStatus { get; set; }

        /// <summary>
        /// 逾期月数
        /// </summary>
        public int OverMonth { get; set; }

        /// <summary>
        /// 逾期天数
        /// </summary>
        public int OverDay { get; set; }

        /// <summary>
        /// 本金逾期月数
        /// </summary>
        public int CapitalOverMonth { get; set; }

        /// <summary>
        /// 本息逾期月数
        /// </summary>
        public decimal CapitalInterestOverMonth { get; set; }

        /// <summary>
        /// 历史逾期欠费(统计逾期的普通帐单、担保帐单的欠费总计)
        /// </summary>
        public decimal OverAmount { get; set; }

        /// <summary>
        /// 当期欠费（固定日下使用，相对日下为零）
        /// </summary>
        public decimal CurrentOverAmount { get; set; }

        /// <summary>
        /// 其他欠费（除普通、担保帐单以外的帐单欠费合计）
        /// </summary>
        public decimal OtherAmount { get; set; }

        /// <summary>
        /// 冻结码
        /// </summary>
        public string FrozenNo { get; set; }

        /// <summary>
        /// 当前账单日
        /// </summary>
        public DateTime CurrentBillDate { get; set; }

        /// <summary>
        /// 下一账单日
        /// </summary>
        public DateTime NextBillDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否已开发票（清贷后使用）
        /// </summary>
        public bool IsFullInvoice { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Updatetime { get; set; }

        #endregion

        #region- 扩展属性 -
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
        #endregion
    }
}

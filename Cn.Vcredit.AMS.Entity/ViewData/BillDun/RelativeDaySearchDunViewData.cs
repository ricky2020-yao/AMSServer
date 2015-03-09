using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:催收单查询结果实体类
    /// </summary>
    public class RelativeDaySearchDunViewData
    {
        /// <summary>
        /// 催收单编号
        /// </summary>
        public long DunID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int DunNumber { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public string HouseholdAddress { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 放贷日期
        /// </summary>
        public DateTime LoanTime { get; set; }

        /// <summary>
        /// 贷款金额
        /// </summary>
        public decimal LoanCapital { get; set; }

        /// <summary>
        /// 欠费金额
        /// </summary>
        public decimal DebtsAmt { get; set; }

        /// <summary>
        /// 逾期数
        /// </summary>
        public string OverdueNumber { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 单位地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 催收员
        /// </summary>
        public string Duner { get; set; }

        /// <summary>
        /// 是否冻结
        /// </summary>
        public string IsFreeze { get; set; }

        /// <summary>
        /// 罚息
        /// </summary>
        public string PunInterest { get; set; }

        /// <summary>
        /// 相对日
        /// </summary>
        public int RelativeDay { get; set; }

        /// <summary>
        /// 指标金额
        /// </summary>
        public string DebtsIndexAmt { get; set; }

        /// <summary>
        /// 日期类型放贷日期
        /// </summary>
        public DateTime LoanDateTime { get; set; }

        /// <summary>
        /// Decimal类型贷款金额
        /// </summary>
        public decimal LoanCapitalDecimal { get; set; }

        /// <summary>
        /// 催收金额
        /// </summary>
        public decimal DunAmount { get; set; }

        /// <summary>
        /// 罚息金额
        /// </summary>
        public decimal PenaltyAmt { get; set; }

        /// <summary>
        /// Decimal类型欠费金额
        /// </summary>
        public decimal DebtsAmtDecimal { get; set; }

        /// <summary>
        /// 逾期月数
        /// </summary>
        public byte OverMonth { get; set; }

        /// <summary>
        /// 冻结号
        /// </summary>
        public string FrozenNo { get; set; }

        /// <summary>
        /// 相对日日期类型
        /// </summary>
        public DateTime RelativeDayDatetime { get; set; }

        /// <summary>
        /// 客群
        /// </summary>
        public string KeQun { get; set; }

        // **********************************************
        // CR181 催收单信息中增加剩余本金和逾期天数
        // 2014年8月5日  wangzhangming
        // **********************************************
        /// <summary>
        /// 剩余本金
        /// </summary>
        public decimal ResidualCapital { get; set; }

        /// <summary>
        /// 逾期天数
        /// </summary>
        public string OverDueDays { get; set; }

        /// <summary>
        /// 银行户名
        /// </summary>
        public string SavingUser { get; set; }

        #region PCR012 wichell 2014年10月13日 10:35:34
        /// <summary>
        /// 客户号
        /// </summary>
        public int PersonID { get; set; }
        /// <summary>
        /// 结果代码
        /// </summary>
        public string DunCodeName { get; set; }
        /// <summary>
        /// 上次跟进时间
        /// </summary>
        public DateTime LastTrackTime { get; set; }
        /// <summary>
        /// 催收单逾期天数
        /// </summary>
        public int DueDays { get; set; }
        /// <summary>
        /// 委外时间
        /// </summary>
        public DateTime OutSourceTime { get; set; }
        #endregion
    }
}

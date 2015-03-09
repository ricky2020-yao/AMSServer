using System;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// 入担保查询的返回视图对象
    /// </summary>
    public class GuarBusinessViewData
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string RegionKey { get; set; }
        /// <summary>
        /// 门店
        /// </summary>
        public string BranchKey { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string AgentName { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductKey { get; set; }
        /// <summary>
        /// 放贷期数
        /// </summary>
        public int LoanPeriod { get; set; }
        /// <summary>
        /// 进入表的日期
        /// </summary>
        public DateTime EntryDate { get; set; }
        /// <summary>
        /// 放贷日期
        /// </summary>
        public DateTime LoanDate { get; set; }
        /// <summary>
        /// 放贷金额
        /// </summary>
        public double LoanCapital { get; set; }
        /// <summary>
        /// 逾期标记
        /// </summary>
        public string OverdueStatus { get; set; }
        /// <summary>
        /// 担保金额
        /// </summary>
        public double GuaranteeAmt { get; set; }
        /// <summary>
        /// 逾期金额
        /// </summary>
        public double OverdueMoney { get; set; }
        /// <summary>
        /// 担保日期
        /// </summary>
        public DateTime GuaranteeDate { get; set; }
        /// <summary>
        /// 剩余本金
        /// </summary>
        public double ResidualCapital { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
    }
}

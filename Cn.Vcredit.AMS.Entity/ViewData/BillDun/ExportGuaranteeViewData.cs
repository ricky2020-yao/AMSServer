using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-18
    /// Description:担保客户数据导出结果
    /// </summary>
    public class ExportGuaranteeViewData
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string BusinessStatus { get; set; }
        /// <summary>
        /// 催收详细
        /// </summary>
        public string OtherConditions { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string AgentName { get; set; }
        /// <summary>
        /// 所属门店
        /// </summary>
        public string BranchName { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string BusinessId { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 担保金额
        /// </summary>
        public string GuaranteeAmount { get; set; }
        /// <summary>
        /// 担保日期
        /// </summary>
        public string GuaranteeDate { get; set; }
        /// <summary>
        /// 贷款金额
        /// </summary>
        public string LoanCapital { get; set; }
        /// <summary>
        /// 贷款日期
        /// </summary>
        public string LoanDate { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        public string LoanPeriod { get; set; }
        /// <summary>
        /// 贷款种类
        /// </summary>
        public string LoanProduct { get; set; }
        /// <summary>
        /// 逾期状态
        /// </summary>
        public string Overdue { get; set; }
        /// <summary>
        /// 统计期内还款
        /// </summary>
        public string PeriodPayment { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 统计周期
        /// </summary>
        public string StatMonth { get; set; }
        /// <summary>
        /// 销售团队
        /// </summary>
        public string Team { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public string TrackType { get; set; }
        /// <summary>
        /// 信托方
        /// </summary>
        public string LendingSide { get; set; }
        /// <summary>
        /// 催讨详情及目前结果（如资料作假，人为包装等明确列出）
        /// </summary>
        public string DetailAndResult { get; set; }
        /// <summary>
        /// 催收详细
        /// </summary>
        public string DunDetail { get; set; }
    }
}

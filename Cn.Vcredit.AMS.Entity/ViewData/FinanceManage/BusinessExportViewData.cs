using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月5日
    /// Description:订单筛选导出结果类
    /// </summary>
    public class BusinessExportViewData
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityCard { get; set; }
        /// <summary>
        /// 逾期欠费
        /// </summary>
        public string OverdueAmt { get; set; }
        /// <summary>
        /// 当期欠费
        /// </summary>
        public string CurrentDueAmt { get; set; }
        /// <summary>
        /// 业务状态
        /// </summary>
        public string BusinessStatus { get; set; }
        /// <summary>
        /// 是否冻结
        /// </summary>
        public string IsFreeze { get; set; }
        /// <summary>
        /// 清贷状态
        /// </summary>
        public string CLoanStatus { get; set; }
        /// <summary>
        /// 操作类编号
        /// </summary>
        public string ProductKind { get; set; }
        /// <summary>
        /// 放贷方
        /// </summary>
        public string LendingSideKey { get; set; }
        /// <summary>
        /// 服务方
        /// </summary>
        public string ServiceSideKey { get; set; }
        /// <summary>
        /// 担保方
        /// </summary>
        public string GuaranteeSideKey { get; set; }
        /// <summary>
        /// 贷款产品（
        /// </summary>
        public string LoanKind { get; set; }
        /// <summary>
        /// 逾期月数
        /// </summary>
        public string OverMonth { get; set; }
        /// <summary>
        /// 销售团队
        /// </summary>
        public string SalesTeam { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SalesManID { get; set; }
        /// <summary>
        /// 放贷日期
        /// </summary>
        public DateTime LoanTime { get; set; }
        /// <summary>
        /// 清贷日期
        /// </summary>
        public DateTime? ClearLoanTime { get; set; }
        /// <summary>
        /// 满约日期
        /// </summary>
        public DateTime? ZClearLoanTime { get; set; }
        /// <summary>
        /// 转诉讼日期
        /// </summary>
        public DateTime? ToLitigationTime { get; set; }
        /// <summary>
        /// 转担保日期
        /// </summary>
        public DateTime? ToGuaranteeTime { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string SavingCard { get; set; }
        /// <summary>
        /// 银行ID
        /// </summary>
        public string BankKey { get; set; }
        /// <summary>
        /// 贷款期数
        /// </summary>
        public int LoanPeriod { get; set; }
        /// <summary>
        /// 未还本金
        /// </summary>
        public decimal ResidualCapital { get; set; }
        /// <summary>
        /// 银行ID
        /// </summary>
        public string BranchKey { get; set; }
        /// <summary>
        /// 贷款本金
        /// </summary>
        public decimal LoanCapital { get; set; }
        /// <summary>
        /// 服务费率
        /// </summary>
        public decimal ServiceRate { get; set; }
        /// <summary>
        /// 手续费率
        /// </summary>
        public decimal ProceduresRate { set; get; }
        /// <summary>
        /// 管理费率
        /// </summary>
        public decimal? ManagementRate { set; get; }
    }
}

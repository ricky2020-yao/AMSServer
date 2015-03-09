using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.Data
{
    /// <summary>
    /// Author:李光明
    /// CreateTime:2013年5月20日
    /// Description：绑定订单科目
    /// </summary>
    public class ViewBusinessExtPivot
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [Description("科目明细-业务号")]
        public int BusinessID { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        [Description("科目明细-合同号")]
        public string ContractNo { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        [Description("科目明细-客户姓名")]
        public string CustomerName { get; set; }

        /// <summary>
        /// 储蓄卡号
        /// </summary>
        [Description("科目明细-储蓄卡号")]
        public string SavingCard { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        [Description("科目明细-开户银行")]
        public string BankName { get; set; }

        /// <summary>
        /// 贷款期数
        /// </summary>
        [Description("科目明细-贷款期数")]
        public int LoanPeriod { get; set; }

        /// <summary>
        /// 贷款种类
        /// </summary>
        [Description("科目明细-贷款种类")]
        public string LoanKind { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        [Description("科目明细-订单类型")]
        public string ProductType { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [Description("科目明细-订单状态")]
        public string BusinessStatus { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        //[Description("科目明细-订单状态")]
        public string strBusinessStatus
        {
            get
            {
                return BusinessStatus.ValueToDesc<EnumBusinessStatus>();
            }
        }

        /// <summary>
        /// 提前清贷状态
        /// </summary>
        [Description("科目明细-清贷状态")]
        public string CLoanStatus { get; set; }

        /// <summary>
        /// 提前清贷状态
        /// </summary>
        //[Description("科目明细-清贷状态")]
        public string strCLoanStatus
        {
            get
            {
                return CLoanStatus.ValueToDesc<EnumCLoanStatus>();
            }
        }

        /// <summary>
        /// 剩余本金
        /// </summary>
        [Description("科目明细-贷款余额")]
        public decimal ResidualCapital { get; set; }

        /// <summary>
        /// 逾期数
        /// </summary>
        [Description("科目明细-逾期期数")]
        public string OverMonth { get; set; }

        /// <summary>
        /// 服务方
        /// </summary>
        [Description("科目明细-服务方")]
        public string ServiceSideKey { get; set; }

        /// <summary>
        /// 担保方
        /// </summary>
        [Description("科目明细-担保方")]
        public string GuaranteeSideKey { get; set; }

        /// <summary>
        /// 信托方
        /// </summary>
        [Description("科目明细-信托方")]
        public string LendingSideKey { get; set; }

        /// <summary>
        /// 门店键名
        /// </summary>
        //public string BranchKey { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        [Description("科目明细-门店")]
        public String BranchKey { get; set; }

        /// <summary>
        /// 销售团队
        /// </summary>
        [Description("科目明细-团队")]
        public String SalesTeam { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        [Description("科目明细-销售员")]
        public string SalesManID { get; set; }

        /// <summary>
        /// 放贷日期
        /// </summary>
        [Description("科目明细-放贷日期")]
        public DateTime LoanTime { get; set; }

        /// <summary>
        /// 自然清贷日期，可扩展
        /// </summary>
        [Description("科目明细-满约日期")]
        public DateTime? ZClearLoanTime { get; set; }

        /// <summary>
        /// 实际清贷日期
        /// </summary>
        [Description("科目明细-清贷日期")]
        public DateTime? ClearLoanTime { get; set; }

        /// <summary>
        /// 诉讼日期
        /// </summary>
        [Description("科目明细-诉讼日期")]
        public DateTime? ToLitigationTime { get; set; }

        /// <summary>
        /// 担保日期
        /// </summary>
        [Description("科目明细-担保日期")]
        public DateTime? ToGuaranteeTime { get; set; }

        /// <summary>
        /// 贷款本金
        /// </summary>
        [Description("科目明细-放贷金额")]
        public decimal LoanCapital { get; set; }

        /// <summary>
        /// 月服务费率
        /// </summary>
        [Description("科目明细-服务费率")]
        public decimal ServiceRate { get; set; }

        /// <summary>
        /// 手续费率
        /// </summary>
        [Description("科目明细-手续费率")]
        public decimal ProceduresRate { set; get; }

        /// <summary>
        /// 管理费率
        /// </summary>
        [Description("科目明细-管理费率")]
        public decimal ManagementRate { set; get; }
    }
}

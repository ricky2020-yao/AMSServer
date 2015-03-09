using Cn.Vcredit.AMS.Entity.Filter;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage.BillDun
{
    /// <summary>
    /// 入担保查询的Filter
    /// </summary>
    public class QueryGuarBusinessFilter:BaseExportFilter
    {
        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 门店
        /// </summary>
        public string Branch { get; set; }
        /// <summary>
        /// 团队
        /// </summary>
        public string Team { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string BusinessKind { get; set; }
        /// <summary>
        /// 客户性质
        /// </summary>
        public string CustomerType { get; set; }
        /// <summary>
        /// 逾期状态
        /// </summary>
        public string OverDueStatus { get; set; }
        /// <summary>
        /// 回款状态
        /// </summary>
        public string ReceiveStauts { get; set; }

        /// <summary>
        /// 放款开始时间
        /// </summary>
        public string LoanStartTime { get; set; }

        /// <summary>
        /// 放款结束时间
        /// </summary>
        public string LoanEndTime { get; set; }

        /// <summary>
        /// 入担保开始时间
        /// </summary>
        public string GuarStartTime { get; set; }

        /// <summary>
        /// 入担保结束时间
        /// </summary>
        public string GuarEndTime { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.CustomerService
{
    /// <summary>
    /// 首月还款提醒导出筛选条件
    /// </summary>
    public class DeductRemindExportFilter : BaseExportFilter
    {
        /// <summary>
        /// 放款开始时间
        /// </summary>
        public DateTime LoanTimeBegin { get; set; }
        /// <summary>
        /// 放款结束时间
        /// </summary>
        public DateTime LoanTimeEnd { get; set; }
        /// <summary>
        /// 销售渠道
        /// </summary>
        public string SaleModel { get; set; }
        /// <summary>
        /// 贷款产品种类
        /// </summary>
        public string ProductKind { get; set; }

        /// <summary>
        /// 有权限的门店List
        /// </summary>
        public string BranchKeyList { get; set; }
    }
}

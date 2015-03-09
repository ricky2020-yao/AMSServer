using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:清贷原因设置检索条件类
    /// </summary>
    public class ClearLoanReasonSearchFilter:BaseFilter
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public int BusinessId { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string BusinessIds { get; set; }
    }
}

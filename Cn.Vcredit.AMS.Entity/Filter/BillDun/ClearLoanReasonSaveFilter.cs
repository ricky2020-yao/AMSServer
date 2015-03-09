using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:保存清贷原因设置条件类
    /// </summary>
    public class ClearLoanReasonSaveFilter:BaseFilter
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
        /// 清贷原因
        /// </summary>
        public string ClearLoanType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ClearLoanRemark { get; set; }        
    }
}

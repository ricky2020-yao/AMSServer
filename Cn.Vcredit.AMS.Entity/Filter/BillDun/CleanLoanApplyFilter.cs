using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// Desc:提前清贷过滤条件
    /// </summary>
    public class CleanLoanApplyFilter
    {
        /// <summary>
        /// 是否已申请
        /// </summary>
        public bool IsApplyed { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdenNo { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 逾期月数
        /// </summary>
        public byte OverMonth { get; set; }

        /// <summary>
        /// 担保key
        /// </summary>
        public string LendingSideKey { get; set; }
    }
}

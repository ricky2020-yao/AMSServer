using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// 催收处理-》订单查询过滤类
    /// </summary>
    public class DBusinessSearchFilter:BaseFilter
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityCard { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public byte BusinessStatus { get; set; }
        /// <summary>
        /// 清贷状态
        /// </summary>
        public byte CLoanStatus { get; set; }
        /// <summary>
        /// 借贷方
        /// </summary>
        public string LendingSideKey { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:坏账清贷条件类
    /// </summary>
    public class BadTransferFilter : BaseFilter
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdenNo { get; set; }

        /// <summary>
        /// 清贷申请编号
        /// </summary>
        public int CloanApplyID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
    }
}

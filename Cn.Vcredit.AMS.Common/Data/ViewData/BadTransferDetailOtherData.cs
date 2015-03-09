using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Data.ViewData
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:坏账清贷检索详情用数据(其它费用)
    /// </summary>
    public class BadTransferDetailOtherData
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 担保违约金
        /// </summary>
        public decimal GuaranteeLateFee { get; set; }

        /// <summary>
        /// 诉讼费
        /// </summary>
        public decimal Litigation { get; set; }

        /// <summary>
        /// 诉讼违约金
        /// </summary>
        public decimal LitigationLateFee { get; set; }
    }
}

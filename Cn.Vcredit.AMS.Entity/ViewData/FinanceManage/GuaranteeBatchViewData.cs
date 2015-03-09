using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// Author:shwang
    /// CreateTime:2014年06月11日
    /// Description:履行担保批次透视表
    /// </summary>
    public class GuaranteeBatchViewData
    {
        /// <summary>
        /// 担保批次号
        /// </summary>
        public string GuaranteeNum { get; set; }

        /// <summary>
        /// 担保批次流水号
        /// </summary>
        public int GuaranteeIndex { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 子公司
        /// </summary>
        public string ChildCompany { get; set; }
        /// <summary>
        /// 担保月份
        /// </summary>
        public string GuaranteeMonth { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 担保金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}

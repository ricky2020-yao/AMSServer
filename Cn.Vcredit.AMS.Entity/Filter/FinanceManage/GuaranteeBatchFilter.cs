using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// Author:shwang
    /// CreateTime:2014年6月11日
    /// Description:履行担保批次查询条件
    /// </summary>
    public class GuaranteeBatchFilter : BaseFilter
    {
        /// <summary>
        /// 担保批次号
        /// </summary>
        public string GuaranteeNo { get; set; }

        /// <summary>
        /// 担保批次索引
        /// </summary>
        public int GuaranteeIndex { get; set; }

        /// <summary>
        /// 担保批次月
        /// </summary>
        public string GuaranteeMonth { get; set; }

        /// <summary>
        /// 子公司
        /// </summary>
        public string ChildCompany { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }
        
        /// <summary>
        /// 是否有付款日期
        /// </summary>
        public bool HasPayDate { get; set; }
    }
}

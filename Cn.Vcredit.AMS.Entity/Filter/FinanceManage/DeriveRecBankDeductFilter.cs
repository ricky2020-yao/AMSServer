using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-12
    /// Description:每日扣导出银扣过滤条件类
    /// </summary>
    public class DeriveRecBankDeductFilter:BaseExportFilter
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 调整科目
        /// </summary>
        public string AdjustKinds { get; set; }
    }
}

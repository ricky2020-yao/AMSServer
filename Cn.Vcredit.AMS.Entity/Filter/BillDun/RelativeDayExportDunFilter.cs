using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:催收单查询检索条件类
    /// </summary>
    public class RelativeDayExportDunFilter : BaseExportFilter
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 业务号s
        /// </summary>
        public string BusinessIds { get; set; }
        /// <summary>
        /// 配置催收单位权限
        /// </summary>
        public string PermissionsUnitKeys { get; set; }
        /// <summary>
        /// 客户权限
        /// </summary>
        public string BranchKey { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-19
    /// Description:订单筛选导出数据处理类
    /// </summary>
    public class ExportBusinessListFilter:BaseExportFilter
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
        /// 是否过滤服务方
        /// </summary>
        public bool IsFilterServiceSideKey { get; set; }
        /// <summary>
        /// 地区键值
        /// </summary>
        public string BranchKeys { get; set; }
        /// <summary>
        /// 是否过滤地区
        /// </summary>
        public bool IsFilterBranchKeys { get; set; }

        /// <summary>
        /// 是否检索罚息信息
        /// </summary>
        public bool IsSearchPenaltyInt { get; set; }

        /// <summary>
        /// 是否检索代偿卡信息
        /// </summary>
        public bool IsSearchAdaptationCard { get; set; }
    }
}

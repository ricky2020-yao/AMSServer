using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Data.Filter
{
    /// <summary>
    /// Author:王书行
    /// CreateTime:2014年8月13日
    /// Description:订单查询条件类
    /// </summary>
    public class SearchBusinessListFilter : BaseFilter
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
        /// 业务状态（[Net枚举]：1、正常 2、担保 3、诉讼）
        /// </summary>
        public byte BusinessStatus { get; set; }

        /// <summary>
        /// 清贷状态（[Net枚举]：1、偿还中  2、满约清贷 3、提前清贷 4、坏帐清贷）
        /// </summary>
        public byte CLoanStatus { get; set; }

        /// <summary>
        /// 储蓄卡号
        /// </summary>
        public string SavingCard { get; set; }

        /// <summary>
        /// 放贷方Id
        /// </summary>
        public int LendingSideId { get; set; }

        /// <summary>
        /// 公司编号字符串
        /// </summary>
        public string CompanyIds { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdenNo { get; set; }
    }
}

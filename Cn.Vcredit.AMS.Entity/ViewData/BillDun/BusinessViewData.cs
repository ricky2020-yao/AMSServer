using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// 业务数据
    /// </summary>
    public class BusinessViewData
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 业务状态（[Net枚举]：1、正常 2、担保 3、诉讼）
        /// </summary>
        public byte BusinessStatus { get; set; }

        /// <summary>
        /// 诉讼执行状态([Net枚举]：1、未诉讼 2、诉讼中 3、诉讼完成 4、申请执行 5、执行一次 6、执行二次)。
        /// </summary>
        public byte LawsuitStatus { get; set; }

        /// <summary>
        /// 转诉讼时间
        /// </summary>
        public DateTime? ToLitigationTime { get; set; }

        /// <summary>
        /// 是否在偿还中
        /// </summary>
        public bool IsRepayment { get; set; }

        /// <summary>
        /// 判决案号
        /// </summary>
        public string LawsuitCode { get; set; }

        /// <summary>
        /// 贷款本金
        /// </summary>
        public decimal LoanCapital { get; set; }

        /// <summary>
        /// 贷款期数
        /// </summary>
        public int LoanPeriod { get; set; }

        /// <summary>
        /// 月利率
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        /// 月服务费率
        /// </summary>
        public decimal ServiceRate { get; set; }

        /// <summary>
        /// 担保方键名
        /// </summary>
        public string GuaranteeSideKey { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// 信托方公司键名
        /// </summary>
        public string LendingSideKey { get; set; }

        /// <summary>
        /// 关停日
        /// </summary>
        public DateTime StopDate { get; set; }
    }
}

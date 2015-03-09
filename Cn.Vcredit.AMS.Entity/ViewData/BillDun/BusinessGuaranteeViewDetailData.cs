using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年10月21日
    /// Description:担保明细
    /// </summary>
    public class BusinessGuaranteeViewDetailData
    {
        /// <summary>
        /// 批次号
        /// </summary>
        public string GuaranteeNum { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdenNo { get; set; }

        /// <summary>
        /// 放贷本金
        /// </summary>
        public decimal LoanCapital { get; set; }

        /// <summary>
        /// 起始年月至结束年月
        /// </summary>
        public string DateRegion { get; set; }

        /// <summary>
        /// 转担保时间（取当前时间）
        /// </summary>
        public string StopDate { get; set; }

        /// <summary>
        /// 拖欠本金
        /// </summary>
        public decimal DueCapital { get; set; }

        /// <summary>
        /// 拖欠利息
        /// </summary>
        public decimal DueInterest { get; set; }

        /// <summary>
        /// 拖欠服务费担保费
        /// </summary>
        public decimal DueService { get; set; }

        /// <summary>
        /// 拖欠罚息
        /// </summary>
        public decimal DuePenltyInt { get; set; }

        /// <summary>
        /// 拖欠总计
        /// </summary>
        public decimal DueTotal { get; set; }

        /// <summary>
        /// 还款月份
        /// </summary>
        public string ReceivedMonth { get; set; }
    }
}

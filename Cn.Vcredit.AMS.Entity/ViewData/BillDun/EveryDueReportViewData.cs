using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cn.Vcredit.Common.TypeConvert;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// Author:wichell
    /// CreateTime:2014年10月9日
    /// Description:静态逾期报表显示实体
    /// </summary>
    public class EveryDueReportViewData
    {
        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatisticsDate { get; set; }
        /// <summary>
        /// 统计日期
        /// </summary>
        public string StatisticsDateStr
        {
            get
            {
                return StatisticsDate.ToDateString();
            }
            set
            {
            }
        }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 贷款金额
        /// </summary>
        public decimal LoanCapital { get; set; }
        /// <summary>
        /// 签约地区
        /// </summary>
        public string SigningCity { get; set; }
        /// <summary>
        /// 逾期天数
        /// </summary>
        public int OverdueDays { get; set; }
        /// <summary>
        /// 当日逾期标记
        /// </summary>
        public string TodayOverdueMark { get; set; }
        /// <summary>
        /// 期初逾期标记
        /// </summary>
        public string BeginningOverdueMark { get; set; }
        /// <summary>
        /// 催收单编号
        /// </summary>
        public int DunID { get; set; }
    }
}

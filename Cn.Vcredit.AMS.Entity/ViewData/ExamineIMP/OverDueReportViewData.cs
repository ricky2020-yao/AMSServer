using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.ExamineIMP
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月23日
    /// Description:客户的逾期情况日报表
    /// </summary>
    public class OverDueReportViewData
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// 统计的月份
        /// </summary>
        public string StaticMonth { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public int Examiner { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string ExaminerName { get; set; }
        /// <summary>
        /// 审核属性
        /// </summary>
        public string ExamineStatus { get; set; }
        /// <summary>
        /// 已放款数
        /// </summary>
        public int LoanCount { get; set; }
        /// <summary>
        /// 首逾数
        /// </summary>
        public int FirstOverDueCount { get; set; }
        /// <summary>
        /// 前4期账单出现M2的数量
        /// </summary>
        public int FourM2Count { get; set; }
        /// <summary>
        /// 前3期账单0次还款的数量
        /// </summary>
        public int ThreeNonePayCount { get; set; }
        /// <summary>
        /// M1逾期率
        /// </summary>
        public string M1Rate { get; set; }
        /// <summary>
        /// M2逾期率
        /// </summary>
        public string M2Rate { get; set; }
        /// <summary>
        /// M3逾期率
        /// </summary>
        public string M3Rate { get; set; }
        /// <summary>
        /// M4逾期率
        /// </summary>
        public string M4Rate { get; set; }
        /// <summary>
        /// M5+逾期率
        /// </summary>
        public string M5PlusRate { get; set; }
        /// <summary>
        /// 首逾数
        /// </summary>
        public string FirstOverDueBusinessIDs { get; set; }
        /// <summary>
        /// 前4期账单出现M2的数量
        /// </summary>
        public string FourM2BusinessIDs { get; set; }
        /// <summary>
        /// 前3期账单0次还款的数量
        /// </summary>
        public string ThreeNonePayBusinessIDs { get; set; }
        /// <summary>
        /// M1逾期率
        /// </summary>
        public string M1BusinessIDs { get; set; }
        /// <summary>
        /// M2逾期率
        /// </summary>
        public string M2BusinessIDs { get; set; }
        /// <summary>
        /// M3逾期率
        /// </summary>
        public string M3BusinessIDs { get; set; }
        /// <summary>
        /// M4逾期率
        /// </summary>
        public string M4BusinessIDs { get; set; }
        /// <summary>
        /// M5+逾期率
        /// </summary>
        public string M5PlusBusinessIDs { get; set; }
    }
}

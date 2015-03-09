using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.PostLoanOverDueReportService.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月23日
    /// Description:客户的逾期情况详细报表
    /// </summary>
    public class OverDueDetailReportData
    {
        /// <summary>
        /// 统计的月份
        /// </summary>
        public string OverTime { get; set; }

        /// <summary>
        /// 业务ID
        /// </summary>
        public int Bid { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public int Examiner { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 审核属性
        /// </summary>
        public int ExamineStatus { get; set; }

        /// <summary>
        /// 逾期月数
        /// </summary>
        public int OverMonth { get; set; }

        /// <summary>
        /// 首逾订单
        /// </summary>
        public int DifferCountFirst { get; set; }

        /// <summary>
        /// 前4期账单出现M2
        /// </summary>
        public int DifferCountFour { get; set; }

        /// <summary>
        /// 前3期账单0次还款
        /// </summary>
        public int DifferCountThree { get; set; }
    }
}

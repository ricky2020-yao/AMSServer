using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月10日
    /// Description:每日逾期客户还款明细报表检索条件
    /// </summary>
    public class EveryDueRepayReportSearchFilter:BaseExportFilter
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public string BusinessNo { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// 订单所属地区
        /// </summary>
        public string OwnRegion { get; set; }
        /// <summary>
        /// 入账开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 入账结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 当日还款前逾期开始天数
        /// </summary>
        public string StartDays { get; set; }
        /// <summary>
        /// 当日还款前逾期结束天数
        /// </summary>
        public string EndDays { get; set; }
    }
}

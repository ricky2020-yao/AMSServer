using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月17日
    /// Description:每日逾期静态报表条件
    /// </summary>
    public class EveryDueReportFilter:BaseExportFilter
    {
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 催收单编号
        /// </summary>
        public int DunId { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdenNo { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 当前逾期标记
        /// </summary>
        public string CurDueSign { get; set; }
        /// <summary>
        /// 期初逾期标记
        /// </summary>
        public string FirstDueSign { get; set; }
        /// <summary>
        /// 销售渠道
        /// </summary>
        public string SaleWay { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string BusStatus { get; set; }
        /// <summary>
        /// 诉讼状态
        /// </summary>
        public string LawitStatus { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// 签约地区
        /// </summary>
        public string SignArean { get; set; }
        /// <summary>
        /// 委外状态
        /// </summary>
        public string ExternalStatus { get; set; }
        /// <summary>
        /// 逾期天数开始
        /// </summary>
        public string MinDueDays { get; set; }
        /// <summary>
        /// 逾期天数结束
        /// </summary>
        public string MaxDueDays { get; set; }
        /// <summary>
        /// 统计开始日期
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 统计结束日期
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 是否欠费
        /// </summary>
        public bool? IsDue { get; set; }
    }
}

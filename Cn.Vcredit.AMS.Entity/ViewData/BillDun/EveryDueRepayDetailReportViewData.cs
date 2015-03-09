using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月10日
    /// Description:每日逾期客户还款明细报表显示实体
    /// </summary>
    public class EveryDueRepayDetailReportViewData
    {
        /// <summary>
        /// 总件数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string BusinessNo { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 贷款总额
        /// </summary>
        public string LoanTotalAmount { get; set; }
        /// <summary>
        /// 当日还款前剩余本金
        /// </summary>
        public string NowDayOddCorpus { get; set; }
        /// <summary>
        /// 每期还款额
        /// </summary>
        public string EachRepayAmount { get; set; }
        /// <summary>
        /// 当日还款前逾期总额
        /// </summary>
        public string NowDayOverDueAmount { get; set; }
        /// <summary>
        /// 当日还款前逾期天数
        /// </summary>
        public string NowDayOverDueDays { get; set; }
        /// <summary>
        /// 还款金额
        /// </summary>
        public string RepayAmount { get; set; }
        /// <summary>
        /// 还款类型
        /// </summary>
        public string RepaymentType { get; set; }
        /// <summary>
        /// 应收/实收
        /// </summary>
        public string ReceivedType { get; set; }
        /// <summary>
        /// 还款方式
        /// </summary>
        public string RepaymentMode { get; set; }
        /// <summary>
        /// 入账时间
        /// </summary>
        public string StrRecordingDate { get; set; }
        /// <summary>
        /// 入账时间
        /// </summary>
        public DateTime RecordingDate { get; set; }
        /// <summary>
        /// 交易日期
        /// </summary>
        public string StrExchangeDate { get; set; }
        /// <summary>
        /// 交易日期
        /// </summary>
        public DateTime ExchangeDate { get; set; }
        /// <summary>
        /// 催收人员
        /// </summary>
        public string CollectionOfficer { get; set; }
        /// <summary>
        /// 订单所属地区
        /// </summary>
        public string OrderOwnRegion { get; set; }
        /// <summary>
        /// 签约门店
        /// </summary>
        public string SingedStore { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductKind { get; set; }
        /// <summary>
        /// 客户号
        /// </summary>
        public string CustomerID { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 催收队列
        /// </summary>
        public string DunQueue { get; set; }
        /// <summary>
        /// 催收机构
        /// </summary>
        public string DunAgency { get; set; }
    }
}

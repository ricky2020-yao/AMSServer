using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 报表系统逾期报表
    /// </summary>
    [MongoTableNameAtrr("Mongo.Rpt_OverDueStatic")]
    public class MongoRptOverDueStatic : MongoDataEntity
    {
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户属性
        /// </summary>
        public string CustomerProperty { get; set; }
        /// <summary>
        /// 借款借据版本
        /// </summary>
        public string ReceiptVersion { get; set; }
        /// <summary>
        /// 贷款金额
        /// </summary>
        public decimal LoanCapital { get; set; }
        /// <summary>
        /// 剩余本金
        /// </summary>
        public decimal? ResidualCapital { get; set; }
        /// <summary>
        /// 欠款金额
        /// </summary>
        public decimal OverdueAmount { get; set; }
        /// <summary>
        /// 本金
        /// </summary>
        public decimal Capital { get; set; }
        /// <summary>
        /// 利息
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// 管理费
        /// </summary>
        public decimal? ManagementFee { get; set; }
        /// <summary>
        /// 本息扣失
        /// </summary>
        public decimal CapitalInterestLoss { get; set; }
        /// <summary>
        /// 服务费
        /// </summary>
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 担保费
        /// </summary>
        public decimal GuaranteeFee { get; set; }
        /// <summary>
        /// 服务费扣失
        /// </summary>
        public decimal ServiceFeeLoss { get; set; }
        /// <summary>
        /// 罚息
        /// </summary>
        public decimal Penalty { get; set; }
        /// <summary>
        /// 账单金额
        /// </summary>
        public decimal BillFee { get; set; }
        /// <summary>
        /// 期限
        /// </summary>
        public int LoanPeriod { get; set; }
        /// <summary>
        /// 偿返期限
        /// </summary>
        public int RepayPeriod { get; set; }
        /// <summary>
        /// 逾期日期
        /// </summary>
        public string OverdueDate { get; set; }
        /// <summary>
        /// 逾期天数
        /// </summary>
        public int OverdueDays { get; set; }
        /// <summary>
        /// 逾期标记
        /// </summary>
        public string TodayOverdueMark { get; set; }
        /// <summary>
        /// 转担保
        /// </summary>
        public string Guarantee { get; set; }
        /// <summary>
        /// 转担保日期
        /// </summary>
        public string GuaranteeDate { get; set; }
        /// <summary>
        /// 转担保金额
        /// </summary>
        public decimal GuaranteeAmount { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string OutStatus { get; set; }
        /// <summary>
        /// 诉讼状态
        /// </summary>
        public string LitigationStatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string CLoanStatus { get; set; }
        /// <summary>
        /// 特殊政策
        /// </summary>
        public string SpecialPolicy { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// 放贷方
        /// </summary>
        public string LendingSide { get; set; }
        /// <summary>
        /// 服务方
        /// </summary>
        public string ServiceSide { get; set; }
        /// <summary>
        /// 放款时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LoanTime { get; set; }
        /// <summary>
        /// 销售渠道
        /// </summary>
        public string SalesChannels { get; set; }
        /// <summary>
        /// 签约城市
        /// </summary>
        public string SigningCity { get; set; }
        /// <summary>
        /// 签约门店
        /// </summary>
        public string SigningShop { get; set; }
        /// <summary>
        /// 销售团队
        /// </summary>
        public string SalesTeam { get; set; }
        /// <summary>
        /// 销售员工
        /// </summary>
        public string SalesStaff { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 二次营销
        /// </summary>
        public string SecondSales { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 统计日期
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StatisticsDate { get; set; }

    }
}

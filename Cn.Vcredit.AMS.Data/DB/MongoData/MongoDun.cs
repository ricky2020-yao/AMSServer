using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 催收信息
    /// </summary>
    [MongoTableNameAtrr("Mongo.Suf_Dun")]
    public class MongoDun : MongoDataEntity
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int? CustomerID { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 欠费总额
        /// </summary>
        public decimal DebtsAmt { get; set; }
        /// <summary>
        /// 放款金额
        /// </summary>
        public decimal LoanCapital { get; set; }
        /// <summary>
        /// 放款时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LoanTime { get; set; }
        /// <summary>
        /// 逾期月数
        /// </summary>
        public string OverdueNumber { get; set; }
        /// <summary>
        /// 催收员
        /// </summary>
        public string Duner { get; set; }
        /// <summary>
        /// 冻结码
        /// </summary>
        public string IsFreeze { get; set; }
        /// <summary>
        /// 未还本金
        /// </summary>
        public decimal ResidualCapital { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int? PersonId { get; set; }
        /// <summary>
        /// 催收编号
        /// </summary>
        public int DunID { get; set; }
        /// <summary>
        /// 期初金额
        /// </summary>
        public decimal BeginningAmt { get; set; }
        /// <summary>
        /// 期末金额
        /// </summary>
        public decimal EndAmt { get; set; }
        /// <summary>
        /// 催收单结束时间
        /// </summary>
        public DateTime CancelTime { get; set; }
        /// <summary>
        /// 期末逾期月数
        /// </summary>
        public int EndOverMonth { get; set; }
        /// <summary>
        /// 催收单序号
        /// </summary>
        public int DunNumber { get; set; }
        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool IsClosed { get; set; }
        /// <summary>
        /// 任务Code
        /// </summary>
        public string TaskCode { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 开始逾期天数
        /// </summary>
        public int? DueDays { get; set; }
        /// <summary>
        /// 结束逾期天数
        /// </summary>
        public int? EndDueDays { get; set; }
        /// <summary>
        /// 催收单位
        /// </summary>
        public int? DunUnit { get; set; }
        /// <summary>
        /// 导出时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime OutSourceTime { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Updatetime { get; set; }

    }
}

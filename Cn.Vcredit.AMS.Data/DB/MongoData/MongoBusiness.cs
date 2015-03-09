using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 包含客户姓名、身份证，BusinessBasic、BusinessExtend、BusinessCurrentStaus信息
    /// </summary>
    [MongoTableNameAtrr("Mongo.Suf_Business")]
    public class MongoBusiness : MongoDataEntity
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        public int? BusinessID { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int? PersonID { get; set; }
        /// <summary>
        /// 贷款本金
        /// </summary>
        public decimal LoanCapital { get; set; }
        /// <summary>
        /// 贷款期数
        /// </summary>
        public int? LoanPeriod { get; set; }
        /// <summary>
        /// 放贷日期
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? LoanTime { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 资金来源
        /// </summary>
        public int? FundSource { get; set; }
        /// <summary>
        /// 放贷方
        /// </summary>
        public int? LendingSide { get; set; }
        /// <summary>
        /// 服务方
        /// </summary>
        public int? ServiceSide { get; set; }
        /// <summary>
        /// 担保方
        /// </summary>
        public int? GuaranteeSide { get; set; }
        /// <summary>
        /// 放贷方收款账号编号
        /// </summary>
        public int? LendingSideAccountID { get; set; }
        /// <summary>
        /// 服务方收款账号编号
        /// </summary>
        public int? ServiceSideAccountID { get; set; }
        /// <summary>
        /// 担保方收款账号编号
        /// </summary>
        public int? GuaranteeSideAccountID { get; set; }
        /// <summary>
        /// 借款借据版本
        /// </summary>
        public int? ReceiptVersion { get; set; }
        /// <summary>
        /// 分店编号
        /// </summary>
        public int? BranchID { get; set; }
        /// <summary>
        /// 贷款产品
        /// </summary>
        public int? LoanKind { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 扣款相对日
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? RelativeDate { get; set; }
        /// <summary>
        /// 贷后业务操作标志：0、不操作1、生成账单
        /// </summary>
        public byte Operable { get; set; }
        /// <summary>
        /// 地区编号
        /// </summary>
        public int? Region { get; set; }
        /// <summary>
        /// 业务逻辑编号
        /// </summary>
        public byte BusinessLogicID { get; set; }
        /// <summary>
        /// 建行号（银扣使用)
        /// </summary>
        public string ConstructionBankNo { get; set; }
        /// <summary>
        /// 建行号
        /// </summary>
        public string ConstructSedNo { get; set; }
        /// <summary>
        /// 签约照片
        /// </summary>
        public string DucImgPath { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public int? SalesManID { get; set; }
        /// <summary>
        /// 二次营销方式
        /// </summary>
        public int? SecondSales { get; set; }
        /// <summary>
        /// 订单来源
        /// </summary>
        public int? FromSource { get; set; }
        /// <summary>
        /// 销售渠道
        /// </summary>
        public int? SaleMode { get; set; }
        /// <summary>
        /// 保证金
        /// </summary>
        public decimal EarnestAmt { get; set; }
        /// <summary>
        /// 保险费(适用融资租赁)
        /// </summary>
        public decimal Premium { get; set; }
        /// <summary>
        /// 手续费
        /// </summary>
        public decimal ProceduresAmout { get; set; }
        /// <summary>
        /// 保证金率
        /// </summary>
        public decimal DepositRate { get; set; }
        /// <summary>
        /// 手续费率
        /// </summary>
        public decimal ProceduresRate { get; set; }
        
        /// <summary>
        /// 总本金
        /// </summary>
        public decimal Capital { get; set; }
        /// <summary>
        /// 未还本金
        /// </summary>
        public decimal ResidualCapital { get; set; }
        /// <summary>
        /// 未出本金
        /// </summary>
        public decimal NoOutOfCapital { get; set; }
        /// <summary>
        /// 业务状态
        /// </summary>
        public byte BusinessStatus { get; set; }
        /// <summary>
        /// 清贷状态
        /// </summary>
        public byte CLoanStatus { get; set; }
        /// <summary>
        /// 逾期月数
        /// </summary>
        public int? OverMonth { get; set; }
        /// <summary>
        /// 逾期天数
        /// </summary>
        public int? OverDay { get; set; }
        /// <summary>
        /// 本金逾期月数
        /// </summary>
        public int? CapitalOverMonth { get; set; }
        /// <summary>
        /// 本息逾期月数
        /// </summary>
        public int? CapitalInterestOverMonth { get; set; }
        /// <summary>
        /// 历史逾期欠费
        /// </summary>
        public decimal OverAmount { get; set; }
        /// <summary>
        /// 当期欠费
        /// </summary>
        public decimal CurrentOverAmount { get; set; }
        /// <summary>
        /// 其他欠费
        /// </summary>
        public decimal OtherAmount { get; set; }
        /// <summary>
        /// 冻结码
        /// </summary>
        public string FrozenNo { get; set; }
        /// <summary>
        /// 当前账单日
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? CurrentBillDate { get; set; }
        /// <summary>
        /// 下一账单日
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? NextBillDate { get; set; }
        /// <summary>
        /// 是否已开发票
        /// </summary>
        public bool IsFullInvoice { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdenNo { get; set; }

    }
}

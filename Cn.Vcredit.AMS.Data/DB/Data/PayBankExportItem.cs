using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012-6-20
    /// Description:银扣导出数据源实体
    /// </summary>
    [Serializable]
    public class PayBankExportItem
    {
        /// <summary>
        /// 欠费总额
        /// </summary>
        public decimal DunAmount { get; set; }
        /// <summary>
        /// 外贸建行号
        /// </summary>
        public string ConstructionBankNo { get; set; }
        /// <summary>
        /// 本金
        /// </summary>
        public decimal BJ { get; set; }
        /// <summary>
        /// 利息
        /// </summary>
        public decimal LX { get; set; }
        /// <summary>
        /// 本息扣失
        /// </summary>
        public decimal BXKS { get; set; }
        /// <summary>
        /// 罚息
        /// </summary>
        public decimal FX { get; set; }
        /// <summary>
        /// 服务费
        /// </summary>
        public decimal FWF { get; set; }
        /// <summary>
        /// 担保费
        /// </summary>
        public decimal DBF { get; set; }
        /// <summary>
        /// 服务费扣失
        /// </summary>
        public decimal FWFKS { get; set; }
        /// <summary>
        /// 账单ID
        /// </summary>
        public long BillID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 是否是当期账单
        /// </summary>
        public bool IsCurrent { get; set; }
        /// <summary>
        /// 储蓄卡号
        /// </summary>
        public string SavingCard { get; set; }
        /// <summary>
        /// 银行户
        /// </summary>
        public string SavingUser { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 贷款本金
        /// </summary>
        public decimal LoanCapital { get; set; }
        /// <summary>
        /// 服务方编号
        /// </summary>
        public int ServiceSideID { get; set; }
        /// <summary>
        /// 信托方编号
        /// </summary>
        public int LendingSideID { get; set; }
        /// <summary>
        /// 贷款期数
        /// </summary>
        public int LoanPeriod { get; set; }
        /// <summary>
        /// 月利率
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        /// 月服务费率
        /// </summary>
        public decimal ServiceRate { get; set; }

        /// <summary>
        /// 日罚息率
        /// </summary>
        public decimal PenaltyRate { get; set; }
        /// <summary>
        /// 放贷日期
        /// </summary>
        public DateTime LoanTime { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public byte BusinessStatus { get; set; }

        /// <summary>
        /// 维信建行号
        /// </summary>
        public string ConstructSedNo { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdenNo { get; set; }

        /// <summary>
        /// 账单月
        /// </summary>
        public string BillMonth { get; set; }

        /// <summary>
        /// 扣款优先级
        /// </summary>
        public byte DunLevel { get; set; }
    } 
}

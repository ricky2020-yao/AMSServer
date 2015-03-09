using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:订单基础表
    /// </summary>
    public class BusinessBasic
    {
        #region- 基本属性 -
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public int PersonID{ get; set; }

        /// <summary>
        /// 贷款本金
        /// </summary>
        public decimal LoanCapital { get; set; }

        /// <summary>
        /// 贷款期数
        /// </summary>
        public byte LoanPeriod { get; set; }

        /// <summary>
        /// 放贷日期
        /// </summary>
        public DateTime LoanTime { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 资金来源
        /// </summary>
        public int FundSource { get; set; }

        /// <summary>
        /// 放贷方
        /// </summary>
        public int LendingSide { get; set; }

        /// <summary>
        /// 服务方
        /// </summary>
        public int ServiceSide { get; set; }

        /// <summary>
        /// 担保方
        /// </summary>
        public int GuaranteeSide { get; set; }

        /// <summary>
        /// 放贷方收款账号编号
        /// </summary>
        public int LendingSideAccountID { get; set; }

        /// <summary>
        /// 服务方收款账号编号
        /// </summary>
        public int ServiceSideAccountID { get; set; }

        /// <summary>
        /// 担保方收款账号编号
        /// </summary>
        public int? GuaranteeSideAccountID { get; set; }

        /// <summary>
        /// 借款借据版本
        /// </summary>
        public int ReceiptVersion { get; set; }

        /// <summary>
        /// 分店编号
        /// </summary>
        public int BranchID { get; set; }

        /// <summary>
        /// 贷款产品（系统枚举）
        /// </summary>
        public int LoanKind { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 扣款相对日
        /// </summary>
        public DateTime RelativeDate { get; set; }

        /// <summary>
        /// 贷后业务操作标志0、	不操作 1、生成账单 
        /// </summary>
        public byte Operable { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public int Region { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Updatetime { get; set; }

        #endregion

        #region- 扩展属性 -
        #endregion
    }
}

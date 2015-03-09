using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月1日
    /// Description:信托归集户账款核对结果类
    /// </summary>
    public class XtgJhzkViewData
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public int Bid { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 账单月
        /// </summary>
        public string BillMonth { get; set; }
        /// <summary>
        /// 科目
        /// </summary>
        public byte Subject { get; set; }
        /// <summary>
        /// 储蓄卡号
        /// </summary>
        public string SavingCard { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string SavingUser { get; set; }
        /// <summary>
        /// 实收
        /// </summary>
        public Decimal Amount { get; set; }
        /// <summary>
        /// 来源/调整
        /// </summary>
        public byte ReceivedType { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        public string ReceivedTime { get; set; }
        /// <summary>
        /// 到账日期
        /// </summary>
        public string ToAcountTime { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 接收ID
        /// </summary>
        public int ReceivedID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTimes
        {
            get
            {
                return this.CreateTime.ToString();
            }
            set
            {
            }
        }
        /// <summary>
        /// 收款时间
        /// </summary>
        public string ReceivedTimes
        {
            get
            {
                return this.ReceivedTime.NullToString();
            }
            set
            {
            }
        }
        /// <summary>
        /// 到账时间
        /// </summary>
        public string ToAcountTimes
        {
            get
            {
                return this.ToAcountTime.NullToString();
            }
            set
            {
            }
        }
        /// <summary>
        /// 科目名称
        /// </summary>
        public string SubjectName
        {
            get
            {
                return this.Subject.ValueToDesc<EnumCostSubject>();

            }
            set
            {
            }
        }
        /// <summary>
        /// 收款类型
        /// </summary>
        public string ReceivedTypeName
        {
            get
            {
                return this.ReceivedType.ValueToDesc<EnumAdjustKind>();
            }
            set
            {
            }
        }
    }
}

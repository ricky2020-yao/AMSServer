using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:帐单
    /// </summary>
    public class Bill
    {
        #region 老版的数据结构
        #region- 基本属性 -
        /// <summary>
        /// 帐单编号（自增列）
        /// </summary>
        public long BillID { get; set; }

        /// <summary>
        /// 业务编号[关联Business表]
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 客户编号[关联Customer表]
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// 帐单类型（1、普通帐单 2、调整帐单 3、提前清贷帐单 4、坏帐帐单）
        /// </summary>
        public byte BillType { get; set; }

        /// <summary>
        /// 帐单状态（1、未付款 2、部分付款 3、全额付款）
        /// </summary>
        public byte BillStatus { get; set; }

        /// <summary>
        /// 帐单月名称
        /// </summary>
        public string BillMonth { get; set; }

        /// <summary>
        /// 帐单所属公司
        /// </summary>
        public string CompanyKey { get; set; }

        /// <summary>
        /// 结算起始日
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结算结束日
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 最后付款日期
        /// </summary>
        public DateTime LimitTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 账单状态全额支付修改事件
        /// </summary>
        public DateTime? FullPaidTime { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// 是否为当期帐单
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 是否搁置帐单
        /// </summary>
        public bool IsShelve { get; set; }

        /// <summary>
        /// 原数据库编号
        /// </summary>
        public int DeductionID { get; set; }

        /// <summary>
        /// 生成账单的时间(在生成账单时,从Business表的NextBillDate获取值,用来判断逾期期数)
        /// </summary>
        public DateTime? DueDate { get; set; }
        #endregion

        #region- 扩展属性 -
        /// <summary>
        /// 帐单款项集合
        /// </summary>
        public virtual List<BillItem> BillItems { get; set; }

        /// <summary>
        /// 业务集合
        /// </summary>
        public virtual Business Business { get; set; }

        /// <summary>
        /// 帐单状态名称
        /// </summary>
        public string StrBillStatus
        {
            get
            {
                return BillStatus.ValueToDesc<EnumBillStatus>();
            }
        }

        /// <summary>
        /// 帐单类型名称
        /// </summary>
        public string StrBillType
        {
            get
            {
                return BillType.ValueToDesc<EnumBillKind>();
            }
        }

        /// <summary>
        /// 帐单状态Html显示
        /// </summary>
        public string StrBillStatusHtml
        {
            get
            {
                switch (BillStatus)
                {
                    case (byte)EnumBillStatus.NoPay:
                        return string.Format("<span style='color:red'>{0}</span>", StrBillStatus);
                    case (byte)EnumBillStatus.PartPay:
                        return string.Format("<span style='color:red'>{0}</span>", StrBillStatus);
                    case (byte)EnumBillStatus.FullPay:
                        return string.Format("<span style='color:green'>{0}</span>", StrBillStatus);
                    default:
                        return string.Format("<span style=''>{0}</span>", StrBillStatus);
                }
            }
        }

        /// <summary>
        /// 应收合计
        /// </summary>
        public string StrTotalDue
        {
            get
            {
                if (this.BillItems == null)
                    return ObjectExtension.ToAmtString(0);
                return this.BillItems.Where(p => !p.IsShelve).Sum(o => o.DueAmt).ToAmtString();
            }
        }

        /// <summary>
        /// 实收合计
        /// </summary>
        public string StrTotalReceived
        {
            get
            {
                if (this.BillItems == null)
                    return ObjectExtension.ToAmtString(0);
                return this.BillItems.Where(p => !p.IsShelve).Sum(o => o.ReceivedAmt).ToAmtString();
            }
        }

        /// <summary>
        /// 帐单的背景底色
        /// </summary>
        public string Background
        {
            get
            {
                switch (BillType)
                {
                    case (byte)EnumBillKind.Normal:
                        return string.Empty;
                    case (byte)EnumBillKind.Guarantee:
                        return "#fff8ff";
                    case (byte)EnumBillKind.Litigation:
                        return "#CBCBCB";
                    case (byte)EnumBillKind.Advance:
                        return "#E8E8FC";
                    case (byte)EnumBillKind.Procedures:
                        return "#8EC4FA";
                    default:
                        return "#FFDBE3";
                }
            }
        }
        #endregion
        #endregion

        #region 新版数据结构(暂时屏蔽)
        //#region- 基本属性 -
        ///// <summary>
        ///// 帐单编号
        ///// </summary>
        //public long BillID { get; set; }

        ///// <summary>
        ///// 帐单基础信息表
        ///// </summary>
        //public BillBasic Basic { get; set; }

        ///// <summary>
        ///// 账单当前状态表
        ///// </summary>
        //public BillCurrentStatus CurrentStatus { get; set; }
        //#endregion

        //#region- 扩展属性 -
        ///// <summary>
        ///// 帐单款项集合
        ///// </summary>
        //public virtual List<BillItem> BillItems { get; set; }

        ///// <summary>
        ///// 业务集合
        ///// </summary>
        //public virtual Business Business { get; set; }

        ///// <summary>
        ///// 应收合计
        ///// </summary>
        //public string StrTotalDue
        //{
        //    get
        //    {
        //        if (this.BillItems == null)
        //            return ObjectExtension.ToAmtString(0);
        //        return this.BillItems.Where(p => !p.Basic.IsShelve).Sum(o => o.Basic.Amount).ToAmtString();
        //    }
        //}

        ///// <summary>
        ///// 实收合计
        ///// </summary>
        //public string StrTotalReceived
        //{
        //    get
        //    {
        //        if (this.BillItems == null)
        //            return ObjectExtension.ToAmtString(0);
        //        return this.BillItems.Where(p => !p.Basic.IsShelve).Sum(o => o.CurrentStatus.ReceivedAmt).ToAmtString();
        //    }
        //}
        //#endregion
        #endregion
    } 
}

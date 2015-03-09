using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:账单当前状态表
    /// </summary>
    public class BillCurrentStatus
    {
        #region- 基本属性 -
        /// <summary>
        /// 帐单编号
        /// </summary>
        public long BillID { get; set; }

        /// <summary>
        /// 业务编号[关联Business表]
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 帐单状态（0、默认值 1、未付款 2、部分付款 3、全额付款 4、欠费）
        /// </summary>
        public byte BillStatus { get; set; }

        /// <summary>
        /// 是否为当期帐单
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 账单状态全额支付修改事件
        /// </summary>
        public DateTime? FullPaidTime { get; set; }

        /// <summary>
        /// 总欠费金额
        /// </summary>
        public decimal OverAmount { get; set; }

        /// <summary>
        /// 本息欠费金额
        /// </summary>
        public decimal CapitalInterestOverAmount { get; set; }

        /// <summary>
        /// 本金欠费金额
        /// </summary>
        public decimal CapitalOverAmount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否搁置帐单
        /// </summary>
        public bool IsShelve { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Updatetime { get; set; }
        #endregion

        #region- 扩展属性 -

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
        #endregion
    }
}

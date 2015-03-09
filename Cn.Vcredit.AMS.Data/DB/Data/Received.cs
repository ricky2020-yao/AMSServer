using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.Common.Enums;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:自动创建（陈伟、王正吉）
    /// CreateTime:2012/6/1 10:34:26
    /// Description:实收表格
    /// </summary>
    public class Received
    {
        #region- 基本属性 -
        /// <summary>
        /// 实收编号
        /// </summary>
        public int ReceivedID { get; set; }

        /// <summary>
        /// 帐单编号[关联Bill表]
        /// </summary>
        public long BillID { get; set; }

        /// <summary>
        /// 扣款科目编号[关联BilllTem表]
        /// </summary>
        public long BillItemID { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 实收/调整类型(1、普通 2、冲销 3、反冲销 4、补生成 11、转帐 12、银扣 13、坏帐 14、补确认 15、预减免 16、预收款 17、退回)
        /// </summary>
        public byte ReceivedType { get; set; }

        /// <summary>
        /// 支付编号（银扣与转帐的收款都将添加PayID,其他虚实收则为零）
        /// </summary>
        public int PayID { get; set; }

        /// <summary>
        /// 收款时间
        /// </summary>
        public DateTime? ReceivedTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? ToAcountTime { get; set; }

        /// <summary>
        /// 收款帐户
        /// </summary>
        public int? ToAccountID { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Explain { get; set; }

        /// <summary>
        /// 原数据库编号
        /// </summary>
        public int DeductionID { get; set; }
        #endregion

        #region- 扩展属性 -
        /// <summary>
        /// 实收类型名称
        /// </summary>
        public string strReceivedType
        {
            get
            {
                return ReceivedType.ValueToDesc<EnumAdjustKind>();
            }
        }

        /// <summary>
        /// 帐单款项
        /// </summary>
        public virtual BillItem BillItem { get; set; }

        /// <summary>
        /// 帐单
        /// </summary>
        public virtual Bill Bill { get; set; }

        /// <summary>
        /// 临时帐单款项编号
        /// </summary>
        public string TempBillItemID { get; set; }

        #endregion
    } 
}

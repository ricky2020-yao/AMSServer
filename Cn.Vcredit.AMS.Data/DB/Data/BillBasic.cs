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
    /// Description:帐单基础信息表
    /// </summary>
    public class BillBasic
    {
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
        /// 帐单类型（1、普通帐单 2、担保帐单 3、提前清贷帐单 4、坏帐帐单 
        /// 5、注销帐单 6、调整帐单 7、担保费用 8、诉讼帐单 9、诉讼费用）
        /// </summary>
        public byte BillType { get; set; }

        /// <summary>
        /// 帐单月名称
        /// </summary>
        public int BillMonth { get; set; }

        /// <summary>
        /// 结算起始日
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结算结束日
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 罚息起计日
        /// </summary>
        public DateTime LimitTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// 是否是固定日帐单
        /// </summary>
        public bool IsFixed { get; set; }

        /// <summary>
        /// 生成账单的时间(在生成账单时,从Business表的NextBillDate获取值,用来判断逾期期数)
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// 是否可扣款
        /// </summary>
        public bool IsCanDeduct { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Updatetime { get; set; }
        #endregion

        #region- 扩展属性 -
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
                    default:
                        return "#FFDBE3";
                }
            }
        }
        #endregion
    }
}

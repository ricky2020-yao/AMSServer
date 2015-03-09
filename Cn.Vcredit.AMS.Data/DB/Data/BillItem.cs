using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:自动创建（陈伟、王正吉）
    /// CreateTime:2012/6/1 10:34:26
    /// Description:保存贷款客户应收的各类扣款项目
    /// </summary>
    public class BillItem
    {
        #region 老版版数据结构
        #region- 基本属性 -
        /// <summary>
        /// 帐单款项编号（自增列）
        /// </summary>
        public long BillItemID { get; set; }

        /// <summary>
        /// 帐单编号[关联Bill表格]
        /// </summary>
        public long BillID { get; set; }

        /// <summary>
        /// 款项科目（1、本金 2、利息 3、本金扣失 4、服务费 5、服务费扣失 6、担保费 7、罚息）
        /// </summary>
        public byte Subject { get; set; }

        /// <summary>
        /// 款项类型（1、普通 2、补生成）
        /// </summary>
        public byte SubjectType { get; set; }

        /// <summary>
        /// 欠费金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 实际应收金额
        /// </summary>
        public decimal DueAmt { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal ReceivedAmt { get; set; }

        /// <summary>
        /// 罚息金额
        /// </summary>
        public decimal PenaltyIntAmt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 足额还款时间
        /// </summary>
        public DateTime? FullPaidTime { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// 是否当期
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 是否搁置
        /// </summary>
        public bool IsShelve { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public int BusinessID { get; set; }

        ///// <summary>
        ///// 扣款优先级
        ///// </summary>
        //public byte DunLevel { get; set; }
        #endregion

        #region- 扩展属性 -
        /// <summary>
        /// 关联Bill表（一对一）
        /// </summary>
        public virtual Bill Bill { set; get; }

        /// <summary>
        /// 关联Receiveds表（一对多）
        /// </summary>
        public virtual List<Received> Receiveds { get; set; }

        /// <summary>
        /// 科目名称
        /// </summary>
        private string _StrSubject;
        public string StrSubject
        {
            get
            {
                if (_StrSubject == null)
                    _StrSubject = Subject.ValueToDesc<EnumCostSubject>();
                return _StrSubject;
            }
            set
            {
                _StrSubject = value;
            }
        }

        /// <summary>
        /// 应收金额
        /// </summary>
        public string StrAmount
        {
            get
            {
                return Amount.ToAmtString();
            }
        }

        /// <summary>
        /// 实际应收
        /// </summary>
        public string StrDueAmt
        {
            get
            {
                return DueAmt.ToAmtString();
            }
        }

        /// <summary>
        /// 实收金额
        /// </summary>
        public string StrReceivedAmt
        {
            get
            {
                return ReceivedAmt.ToAmtString();
            }
        }

        /// <summary>
        /// 实收项调整公式HTML代码
        /// </summary>
        public string StrReceived { get; set; }

        /// <summary>
        /// 应收项调整公式HTML代码
        /// </summary>
        public string StrReceivable { get; set; }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string StrCreateTime
        {
            get
            {
                return CreateTime.ToDateTimeString();
            }
        }
        #endregion
        #endregion

        #region 新版数据结构（暂停使用）
        //#region- 基本属性 -
        ///// <summary>
        ///// 帐单款项编号
        ///// </summary>
        //public long BillItemID { get; set; }

        ///// <summary>
        ///// 科目基础表
        ///// </summary>
        //public BillItemBasic Basic { get; set; }

        ///// <summary>
        ///// 科目当前状态表
        ///// </summary>
        //public BillItemCurrentStatus CurrentStatus { get; set; }
        //#endregion
        #endregion
    }
}

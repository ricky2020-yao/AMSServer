using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:科目基础表
    /// </summary>
    public class BillItemBasic
    {
        #region- 基本属性 -
        /// <summary>
        /// 帐单款项编号
        /// </summary>
        public long BillItemID { get; set; }

        /// <summary>
        /// 帐单编号[关联Bill表格]
        /// </summary>
        public long BillID { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 款项科目科目类型（1、月本金 2、月利息 3、月服务费 4、月担保费 6、预缴保费 
        /// 7、月租金 8、月保险费 9、月管理费 21、本息扣失 22、服务费扣失 23、罚息 
        /// 24、担保金25、担保违约金 26、诉讼费 27、诉讼违约金 28、加收利息 29、加收服务费
        /// 30、清贷服务费 31、剩余本金 32、清贷减免金额 33、剩余利息 34、坏账实收金额
        /// 35、提前清贷服务费 36、加收担保费 37、保费）
        /// </summary>
        public byte Subject { get; set; }

        /// <summary>
        /// DueDate
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 款项类型（1、普通 2、补生成）
        /// </summary>
        public byte SubjectType { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// 是否搁置
        /// </summary>
        public bool IsShelve { get; set; }

        /// <summary>
        /// 是否可扣款
        /// </summary>
        public bool IsCanDeduct { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Updatetime { get; set; }
        #endregion
    }
}

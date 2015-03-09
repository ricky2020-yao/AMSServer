using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月1日
    /// Description:信托归集户账款核对条件类
    /// </summary>
    public class XtgJhzkCheckExportFilter : BaseExportFilter
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 储蓄卡号
        /// </summary>
        public string CashCardNo { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 到账开始日期
        /// </summary>
        public string ToAccountBeginDate { get; set; }

        /// <summary>
        /// 到账结束日期
        /// </summary>
        public string ToAccountEndDate { get; set; }

        /// <summary>
        /// 信托方
        /// </summary>
        public string LendingSide { get; set; }

        /// <summary>
        /// 服务方
        /// </summary>
        public string ServiceSide { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string BillType { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string BillStatus { get; set; }

        /// <summary>
        /// 收款账户
        /// </summary>
        public string ReceiveAccount { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 收款来源
        /// </summary>
        public string ReceiveType { get; set; }

        /// <summary>
        /// 科目明细
        /// </summary>
        public string ReptColSubject { get; set; }
    }
}

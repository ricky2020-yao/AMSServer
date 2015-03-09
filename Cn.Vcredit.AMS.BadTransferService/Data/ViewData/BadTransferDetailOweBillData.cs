using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BadTransferService.Data.ViewData
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:坏账清贷检索详情用数据(欠费账单(当期+逾期))
    /// </summary>
    public class BadTransferDetailOweBillData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BadTransferDetailOweBillData() { }

        /// <summary>
        /// 申请编号
        /// </summary>
        public int CloanApplyID { get; set; }

        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 业务逻辑编号
        /// </summary>
        public byte BusinessLogicID { get; set; }

        /// <summary>
        /// 科目类型
        /// </summary>
        public byte Subject { get; set; }

        /// <summary>
        /// 实际应收
        /// </summary>
        public decimal DueAmt { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public decimal ReceivedAmt { get; set; }

        /// <summary>
        /// 凭证文件保存路径
        /// </summary>
        public string Path { get; set; }
    }
}

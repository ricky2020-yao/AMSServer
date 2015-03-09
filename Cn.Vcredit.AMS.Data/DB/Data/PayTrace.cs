using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:自动创建（陈伟、王正吉）
    /// CreateTime:2012-8-29 15:29:39
    /// Description:支付请求跟踪
    /// </summary>
    public class PayTrace
    {
        #region- 基本属性 -
        /// <summary>
        /// 支付跟踪编号（流水号）
        /// </summary>
        public int PayTraceID { get; set; }

        /// <summary>
        /// 支付类型(第三方网关名称、银扣)
        /// </summary>
        public byte PayKind { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 当日编号
        /// </summary>
        public int DayNo { get; set; }

        /// <summary>
        /// 请求字符串内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 记录目的
        /// </summary>
        public byte CallDirection { get; set; }

        /// <summary>
        /// 传参方式（1、get 2、post）
        /// </summary>
        public byte CallMethod { get; set; }

        /// <summary>
        /// 请求状态
        /// </summary>
        public byte RequestState { get; set; }

        /// <summary>
        /// 跟踪总金额
        /// </summary>
        public decimal PayTraceAmount { get; set; }

        /// <summary>
        /// 操作者ID（仅仅针对于银扣，每日扣款不需要触发者）
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// 公司键名（服务方）
        /// </summary>
        public string CompanyKey { get; set; }

        /// <summary>
        /// 服务方收款账户
        /// </summary>
        public int ServiceSideID { get; set; }

        /// <summary>
        /// 放贷方收款账户
        /// </summary>
        public int LendingSideID { get; set; }

        /// <summary>
        /// 子公司ID
        /// </summary>
        public int AccountID { get; set; }

        /// <summary>
        /// 收益方类型
        /// </summary>
        public byte IncomeType { get; set; }

        /// <summary>
        /// 发送时间(创建时间)
        /// </summary>
        public DateTime TraceTime { get; set; }

        /// <summary>
        /// 响应时间
        /// </summary>
        public DateTime? ResponseTime { get; set; }

        /// <summary>
        /// 响应代码
        /// </summary>
        public string ResponseNum { get; set; }

        /// <summary>
        /// 响应代码描述
        /// </summary>
        public string ResponseDesc { get; set; }

        /// <summary>
        /// 加锁码
        /// </summary>
        public string LockKey { get; set; }

        /// <summary>
        /// 响应文件个数
        /// </summary>
        public int FileNum { get; set; }

        /// <summary>
        /// 成功扣款金额
        /// </summary>
        public decimal RequestAmount { get; set; }

        /// <summary>
        /// 帐单开帐周期
        /// </summary>
        public byte PeriodType { get; set; }

        /// <summary>
        /// 计划任务Key
        /// </summary>
        public string TaskKey { get; set; }

        /// <summary>
        /// 支付原因
        /// </summary>
        public byte? PayReason { get; set; }
        #endregion

    }
}

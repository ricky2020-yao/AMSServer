using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Consts
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月14日
    /// Description:WebService服务定义ID
    /// </summary>
    public class WebServiceConst
    {
        #region WebService服务ID定义
        /// <summary>
        /// 坏账清贷审核通过服务-1
        /// </summary>
        [Description("坏账清贷审核通过服务")]
        public const string WebService_PassBadTransfer = "1";
        /// <summary>
        /// 坏账清贷检索服务-2
        /// </summary>
        [Description("坏账清贷检索服务")]
        public const string WebService_BadTransferSearch = "2";
        /// <summary>
        /// 坏账清贷详细信息服务-3
        /// </summary>
        [Description("坏账清贷详细信息服务")]
        public const string WebService_BadTransferDetail = "3";
        /// <summary>
        /// 坏账清贷拒绝服务-4
        /// </summary>
        [Description("坏账清贷拒绝服务")]
        public const string WebService_BadTransferReject = "4";
        /// <summary>
        /// 时间轴设置数据初始化服务-5
        /// </summary>
        [Description("时间轴设置数据初始化服务")]
        public const string WebService_TimeLineInitService = "5";
        /// <summary>
        /// 时间轴设置更新序列服务-6
        /// </summary>
        [Description("时间轴设置更新序列服务")]
        public const string WebService_TimeLineUpdateSeqService = "6";
        /// <summary>
        /// 时间轴设置更新关帐日服务-7
        /// </summary>
        [Description("时间轴设置更新关帐日服务")]
        public const string WebService_TimeLineUpdateCloseDayService = "7";
        /// <summary>
        /// 时间轴设置关帐日初始化服务-8
        /// </summary>
        [Description("时间轴设置关帐日初始化服务")]
        public const string WebService_TimeLineCloseDayInitService = "8";
        /// <summary>
        /// 手动扣款服务-9
        /// </summary>
        [Description("手动扣款服务")]
        public const string WebService_HandDeductService = "9";
        /// <summary>
        /// 手动扣款查询服务-10
        /// </summary>
        [Description("手动扣款查询服务")]
        public const string WebService_HandDeductQueryService = "10";
        /// <summary>
        /// 信托归集户账款核对查询服务-11
        /// </summary>
        [Description("信托归集户账款核对查询服务")]
        public const string WebService_XtgJhzkCheckSearchService = "11";
        /// <summary>
        /// 信托归集户账款核对更新到账日期服务-12
        /// </summary>
        [Description("信托归集户账款核对更新到账日期服务")]
        public const string WebService_UpdateXtgJhzkToAcountTimeService = "12";
        /// <summary>
        /// 信托归集户账款核对清除到账日期服务-13
        /// </summary>
        [Description("信托归集户账款核对清除到账日期服务")]
        public const string WebService_ClearXtgJhzkToAcountTimeService = "13";
        /// <summary>
        /// 订单筛选导出服务-14
        /// </summary>
        [Description("订单筛选导出服务")]
        public const string WebService_BusinesssDataExportService = "14";
        /// <summary>
        /// 审核员贷后客户逾期情况日报表查询服务-20
        /// </summary>
        [Description("审核员贷后客户逾期情况日报表查询服务")]
        public const string WebService_GetOverDueReportSearchResultService = "20";
        /// <summary>
        /// 审核员贷后客户逾期情况汇总情况表查询服务-21
        /// </summary>
        [Description("审核员贷后客户逾期情况汇总情况表查询服务")]
        public const string WebService_GetOverDueTotalSearchResultService = "21";
        #endregion

        #region  坏账清贷费用类型
        /// <summary>
        /// 欠费账单(当期+逾期)--1
        /// </summary>
        [Description("欠费账单(当期+逾期)")]
        public const int BadTransferFeeType_OweBill = 1;
        /// <summary>
        /// 其它费用--2
        /// </summary>
        [Description("其它费用")]
        public const int BadTransferFeeType_Other = 2;
        #endregion

        #region 超时时间

        /// <summary>
        /// 超时时间较少
        /// </summary>
        [Description("超时时间较少")]
        public const int TimeOut_Less = 5000;

        /// <summary>
        /// 超时时间适中
        /// </summary>
        [Description("超时时间适中")]
        public const int TimeOut_Middle = 20000;

        /// <summary>
        /// 超时时间多
        /// </summary>
        [Description("超时时间多")]
        public const int TimeOut_More = 60000;
        #endregion

        #region  时间轴设置类型
        /// <summary>
        /// 序列(21/28/12)--1
        /// </summary>
        [Description("序列(21/28/12)")]
        public const int TimeLineType_Sequence = 1;
        /// <summary>
        /// 关账日--2
        /// </summary>
        [Description("关账日")]
        public const int TimeLineType_CloseDay = 2;
        #endregion
    }
}

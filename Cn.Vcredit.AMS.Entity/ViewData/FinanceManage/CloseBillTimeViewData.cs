using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月18日
    /// Description:时间轴设置关账日初始化
    /// </summary>
    public class CloseBillTimeViewData
    {
        /// <summary>
        /// 关帐日编号
        /// </summary>
        public int CloseBillDayID { set; get; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { set; get; }
        /// <summary>
        /// 原始时间
        /// </summary>
        public string OriginalTime { set; get; }
        /// <summary>
        /// 最新的时间
        /// </summary>
        public string LatestTime { set; get; }
        /// <summary>
        /// 操作者ID
        /// </summary>
        public int OperatorID { set; get; }

        /// <summary>
        /// 时间轴年份
        /// </summary>
        public int DeductYear { get; set; }

        /// <summary>
        /// 时间轴月份
        /// </summary>
        public int DeductMonth { get; set; }

        /// <summary>
        /// 扣款时间
        /// </summary>
        public string DeductTime { get; set; }

        /// <summary>
        /// 数据类型。
        /// 1：序列（21/28/12）
        /// 2：关账日
        /// </summary>
        public int DataType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
    }
}

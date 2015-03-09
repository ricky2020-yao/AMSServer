using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月19日
    /// Description:时间轴设置更新条件类
    /// </summary>
    public class TimeLineUpdateFilter : BaseFilter
    {
        /// <summary>
        /// 当月字符串（显示格式：1985/01）
        /// </summary>
        public string CurrentMonthString { get; set; }

        /// <summary>
        /// 当月第一次扣款时间
        /// </summary>
        public DateTime CurrentMonthFirst { get; set; }

        /// <summary>
        /// 当月第二次扣款时间
        /// </summary>
        public DateTime CurrentMonthSecond { get; set; }

        /// <summary>
        /// 当月第三次扣款时间
        /// </summary>
        public DateTime CurrentMonthThird { get; set; }

        /// <summary>
        /// 下月字符串（显示格式：1985/01）
        /// </summary>
        public string NextMonthString { get; set; }

        /// <summary>
        /// 下月第一次扣款时间
        /// </summary>
        public DateTime NextMonthFirst { get; set; }

        /// <summary>
        /// 下月第二次扣款时间
        /// </summary>
        public DateTime NextMonthSecond { get; set; }

        /// <summary>
        /// 下月第三次扣款时间
        /// </summary>
        public DateTime NextMonthThird { get; set; }

        /// <summary>
        /// 下下月字符串（显示格式：1985/01）
        /// </summary>
        public string MonthAfterNextString { get; set; }

        /// <summary>
        /// 下下月第一次扣款时间
        /// </summary>
        public DateTime MonthAfterNextFirst { get; set; }

        /// <summary>
        /// 下下月第二次扣款时间
        /// </summary>
        public DateTime MonthAfterNextSecond { get; set; }

        /// <summary>
        /// 下下月第三次扣款时间
        /// </summary>
        public DateTime MonthAfterNextThird { get; set; }

        /// <summary>
        /// 上月字符串（显示格式：1985/01）
        /// </summary>
        public string LastMonthString { get; set; }

        /// <summary>
        /// 上月第一次扣款时间
        /// </summary>
        public DateTime LastMonthFirst { get; set; }

        /// <summary>
        /// 上月第二次扣款时间
        /// </summary>
        public DateTime LastMonthSecond { get; set; }

        /// <summary>
        /// 上月第三次扣款时间
        /// </summary>
        public DateTime LastMonthThird { get; set; }

        /// <summary>
        /// 关帐日ID
        /// </summary>
        public int CloseBillDayID { get; set; }

        /// <summary>
        /// 历史关帐日期
        /// </summary>
        public DateTime OriginalTime { get; set; }

        /// <summary>
        /// 关帐日期
        /// </summary>
        public DateTime LatestTime { get; set; }

        /// <summary>
        /// 公司Key
        /// </summary>
        public string CompanyKey { get; set; }

        /// <summary>
        /// 公司Keys
        /// </summary>
        public string CompanyKeys { get; set; }
    }
}

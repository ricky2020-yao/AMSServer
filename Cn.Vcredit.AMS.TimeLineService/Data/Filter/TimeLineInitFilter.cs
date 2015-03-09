using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.TimeLineService.Data.Filter
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月19日
    /// Description:时间轴设置初始化条件类
    /// </summary>
    public class TimeLineInitFilter:BaseFilter
    {
        /// <summary>
        /// 扣款序列类型
        /// </summary>
        public EnumDeductSeqKind Kind { get; set; }

        /// <summary>
        /// 扣款月份
        /// </summary>
        public string DeductMonth { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyId { get; set; }
    }
}

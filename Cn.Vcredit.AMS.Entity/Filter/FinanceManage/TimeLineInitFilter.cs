using Cn.Vcredit.AMS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:时间抽初始化过滤类
    /// </summary>
    public class TimeLineInitFilter:BaseFilter
    {
        /// <summary>
        /// 扣款序列类型
        /// </summary>
        public byte Kind { get; set; }

        /// <summary>
        /// 扣款月份
        /// </summary>
        public string DeductMonth { get; set; }

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

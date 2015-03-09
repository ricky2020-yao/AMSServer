using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.RedisData
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-11-24
    /// Description:关帐日
    /// </summary>
    public class CloseBillDayRedisEntity
    {
        /// <summary>
        /// 关帐日编号
        /// </summary>
        public int CloseBillDayID { get; set; }
        /// <summary>
        /// 子公司编号
        /// </summary>
        public string CompanyKey { get; set; }
        /// <summary>
        /// 原始时间
        /// </summary>
        public DateTime OriginalTime { get; set; }
        /// <summary>
        /// 最新的时间
        /// </summary>
        public DateTime LatestTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 操作者ID
        /// </summary>
        public int OperatorID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.RedisData
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-11-24
    /// Description:账户关帐日
    /// </summary>
    public class AccountingCloseBillDayRedisEntity
    {
        /// <summary>
        /// 关帐日编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 子公司编号
        /// </summary>
        public DateTime CloseDate { get; set; }
        /// <summary>
        /// 原始时间
        /// </summary>
        public DateTime StopDate { get; set; }
        /// <summary>
        /// 最新的时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }
}

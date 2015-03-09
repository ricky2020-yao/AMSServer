using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:自动创建
    /// CreateTime:2014年8月18号
    /// Description:关账日
    /// </summary>
    public class CloseBillDay
    {
        #region- 基本属性 -
        /// <summary>
        /// 关帐日编号
        /// </summary>
        public int CloseBillDayID { get; set; }

        /// <summary>
        /// 子公司Key
        /// </summary>
        public string CompanyKey { get; set; }

        /// <summary>
        /// 历史关帐日期
        /// </summary>
        public DateTime OriginalTime { get; set; }

        /// <summary>
        /// 关帐日期
        /// </summary>
        public DateTime LatestTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///  操作者ID
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        //public DateTime Updatetime { get; set; }
        #endregion
    }
}

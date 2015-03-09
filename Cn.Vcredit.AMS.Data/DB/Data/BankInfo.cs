using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月5日
    /// Description:银行类型
    /// </summary>
    public class BankInfo
    {
        /// <summary>
        /// 银行ID
        /// </summary>
        public int BankId { get; set; }
        /// <summary>
        /// 银行代码
        /// </summary>
        public string BankCode { get; set; }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankType { get; set; }
        /// <summary>
        /// 交易代码
        /// </summary>
        public string ExangeOfficeCode { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime {get;set; }
    }
}

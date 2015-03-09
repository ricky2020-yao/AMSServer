using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:公用导入字段
    /// </summary>
    [Serializable]
    public class BaseImportItem
    {
        /// <summary>
        /// 储蓄卡号
        /// </summary>
        public string SavingCard { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 扣款科目优先级
        /// </summary>
        public byte DunLevel { get; set; }
    } 
}

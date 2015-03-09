using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// 履行担保更新条件
    /// </summary>
    public class GuaranteeBatchUpdateFilter : BaseFilter
    {
        public string GuaranteeNums { get; set; }
        public DateTime PayDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public byte PayType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年12月3日
    /// Description:代偿款支付设置返回数据
    /// </summary>
    public class InterestFiltViewData
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNO { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string idenNO { get; set; }

        /// <summary>
        /// 转担保日期
        /// </summary>
        public string GuaranteeSideDate { get; set; }

        /// <summary>
        ///  支付日期
        /// </summary>
        public string PayDate { get; set; }

        /// <summary>
        /// 剩余本息
        /// </summary>
        public string SurplusInterest { get; set; }

        /// <summary>
        /// 贷款方
        /// </summary>
        public string Loaner { get; set; }
    }
}

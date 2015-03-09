using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.CustomerService
{
    public class DeductRemindExportViewData
    {
        ///<summary>
        /// 合同号
        /// </summary>		
        public string ContractNo { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>		
        public string CustomerName { get; set; }
        /// <summary>
        /// 贷款金额
        /// </summary>		
        public decimal LoanCapital { get; set; }
        /// <summary>
        /// 贷款期限
        /// </summary>		
        public int LoanPeriod { get; set; }
        /// <summary>
        /// 每月付款金额
        /// </summary>		
        public decimal MonthPay { get; set; }
        /// <summary>
        /// 销售渠道
        /// </summary>		
        public string SaleMode { get; set; }
        /// <summary>
        /// 地区(城市)
        /// </summary>		
        public string Region { get; set; }
        /// <summary>
        /// 门店
        /// </summary>		
        public string BranchKey { get; set; }
        /// <summary>
        /// 销售团队
        /// </summary>		
        public string SalesTeam { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>		
        public string SalesManName { get; set; }    
    }
}

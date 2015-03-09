using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    public class CleanLoanApplyViewData
    {
        /// <summary>
        /// 序号
        /// </summary>		
        public int num { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>		
        public int BusinessId { get; set; }
        /// <summary>
        /// 逾期金额
        /// </summary>		
        public decimal OverAmount { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>		
        public string ContenNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>		
        public string CustomerName { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>		
        public string IndeNo { get; set; }
        /// <summary>
        /// 电话
        /// </summary>		
        public string Phone { get; set; }
        /// <summary>
        /// 放贷金额
        /// </summary>		
        public decimal LoanAmount { get; set; }
        /// <summary>
        /// 清贷状态
        /// </summary>		
        public int CLoanStatus { get; set; }
        /// <summary>
        /// 逾期月
        /// </summary>		
        public int OverMonth { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>		
        public int TotalCount { get; set; }    
    }
}

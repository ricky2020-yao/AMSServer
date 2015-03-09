using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.CustomerService
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月9日
    /// Description:客户检索条件类
    /// </summary>
    public class CustomerSearchFilter:BaseFilter
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNO { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public int BusinessId { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdenNO { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string DropSources { get; set; }
        /// <summary>
        /// 分公司
        /// </summary>
        public string CompanyKey { get; set; }
        /// <summary>
        /// 放款方
        /// </summary>
        public string LendingKey { get; set; }
    }
}

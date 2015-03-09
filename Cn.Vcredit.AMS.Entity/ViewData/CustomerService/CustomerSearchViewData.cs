using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.CustomerService
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月9日
    /// Description:客户查询结果数据定义
    /// </summary>
    public class CustomerSearchViewData
    {
        /// <summary>
        /// 业务编号  
        /// </summary>
        public int BusinessID { set; get; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int CustomerID { set; get; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { set; get; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string CustomerName { set; get; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdenNo { set; get; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string SavingCard { set; get; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Mobile { set; get; }

        /// <summary>
        /// 户籍类型（1、本地籍 2、非本地籍）
        /// </summary>
        public string HouseholdType { get; set; }

        /// <summary>
        /// 是否是二次营销
        /// </summary>
        public string IsLoanSecond { get; set; }

        /// <summary>
        /// 是否发送短信
        /// </summary>
        public bool IsSendMsg { get; set; }
    }
}

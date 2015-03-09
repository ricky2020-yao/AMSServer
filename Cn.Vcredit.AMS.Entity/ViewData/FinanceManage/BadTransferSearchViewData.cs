using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月12日
    /// Description:坏账清贷检索结果用数据
    /// </summary>
    public class BadTransferSearchViewData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BadTransferSearchViewData() { }

        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 申请编号
        /// </summary>
        public int CloanApplyID { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// 已出账欠费
        /// </summary>
        public decimal OverAmount { get; set; }

        /// <summary>
        /// 坏账清贷金额
        /// </summary>
        public decimal AdvAmount { get; set; }

        /// <summary>
        /// 其他金额
        /// </summary>
        public decimal OtherAmount { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int BusinessStatus { get; set; }

        /// <summary>
        /// 清贷状态
        /// </summary>
        public int CLoanStatus { get; set; }

        ///// <summary>
        ///// 订单状态（客户类型）
        ///// </summary>
        //public string StrBusinessStatus
        //{
        //    get
        //    {
        //        return BusinessStatus.ValueToDesc<EnumBusinessStatus>();
        //    }
        //}
        ///// <summary>
        ///// 清贷状态
        ///// </summary>
        //public string StrCLoanStatus
        //{
        //    get
        //    {
        //        return CLoanStatus.ValueToDesc<EnumCLoanStatus>();
        //    }
        //}
    }
}

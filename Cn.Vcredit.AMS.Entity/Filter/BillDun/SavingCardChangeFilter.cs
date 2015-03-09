using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// 储蓄卡变更过滤条件
    /// </summary>
    public class SavingCardChangeFilter:BaseFilter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SavingCardChangeFilter()
        {
            CustomerName = "";
            IdenNO = "";
            ContractNo = "";
            BusinessId = "";
            AuditType = 0;
            BranchKeyList = "";
            RegionKey = "";
        }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdenNO { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string BusinessId { get; set; }
        /// <summary>
        /// 待复核类型
        /// 1、待复核 0、变更
        /// </summary>
        public int AuditType { get; set; }
        /// <summary>
        /// 地区权限
        /// </summary>
        public string BranchKeyList { get; set; }

        /// <summary>
        /// 地区权限
        /// </summary>
        public string RegionKey { get; set; }
    }
}

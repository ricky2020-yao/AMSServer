using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月5日
    /// Description:订单筛选查询条件类
    /// </summary>
    public class BusinessExportFilter : BaseExportFilter
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public byte BusinessStatus { get; set; }

        /// <summary>
        /// 清贷状态
        /// </summary>
        public byte CLoanStatus { get; set; }

        /// <summary>
        /// 贷款种类
        /// </summary>
        public string LoanKind { get; set; }

        /// <summary>
        /// 逾期期数
        /// </summary>
        public byte OverMonth { get; set; }

        /// <summary>
        /// 信托方
        /// </summary>
        public string LendingSideKey { get; set; }

        /// <summary>
        /// 服务方
        /// </summary>
        public string ServiceSideKey { get; set; }

        /// <summary>
        /// 担保方
        /// </summary>
        public string GuaranteeSideKey { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        public string BranchKey { get; set; }

        /// <summary>
        /// 团队
        /// </summary>
        public string SalesTeam { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public int SalesManId { get; set; }

        /// <summary>
        /// 放贷日起始日期
        /// </summary>
        public DateTime? LoanDateBegin { get; set; }

        /// <summary>
        /// 放贷日结束日期
        /// </summary>
        public DateTime? LoanDateEnd { get; set; }

        /// <summary>
        /// 自然清贷日起始日期
        /// </summary>
        public DateTime? ZLoanDateBegin { get; set; }

        /// <summary>
        /// 自然清贷日结束日期
        /// </summary>
        public DateTime? ZLoanDateEnd { get; set; }

        /// <summary>
        /// 实际清贷日起始日期
        /// </summary>
        public DateTime? CLoanDateBegin { get; set; }

        /// <summary>
        /// 实际清贷日结束日期
        /// </summary>
        public DateTime? CLoanDateEnd { get; set; }

        /// <summary>
        /// 诉讼日开始日期
        /// </summary>
        public DateTime? LawsuitDateBegin { get; set; }

        /// <summary>
        /// 诉讼日结束日期
        /// </summary>
        public DateTime? LawsuitDateEnd { get; set; }

        /// <summary>
        /// 担保日开始日期
        /// </summary>
        public DateTime? GuarteeDateBegin { get; set; }

        /// <summary>
        /// 担保日结束日期
        /// </summary>
        public DateTime? GuarteeDateEnd { get; set; }
    }
}

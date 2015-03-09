using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.FinanceManage
{
    /// <summary>
    /// Author:王书行
    /// CreateTime:2014年8月13日
    /// Description:订单查询条件类
    /// </summary>
    public class SearchBusinessListFilter : BaseFilter
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 业务状态（[Net枚举]：1、正常 2、担保 3、诉讼）
        /// </summary>
        public byte BusinessStatus { get; set; }

        /// <summary>
        /// 清贷状态（[Net枚举]：1、偿还中  2、满约清贷 3、提前清贷 4、坏帐清贷）
        /// </summary>
        public byte CLoanStatus { get; set; }

        /// <summary>
        /// 诉讼状态
        /// </summary>
        public byte LawsuitStatus { get; set; }

        /// <summary>
        /// 储蓄卡号
        /// </summary>
        public string SavingCard { get; set; }

        /// <summary>
        /// 放贷方Id
        /// </summary>
        public int LendingSideId { get; set; }

        /// <summary>
        /// 公司编号字符串
        /// </summary>
        public string ServiceSideIds { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 放贷方的key
        /// </summary>
        public string LendingSideKey { get; set; }

        /// <summary>
        /// 服务方的key
        /// </summary>
        public string ServiceSideKey { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string BranchKey { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public int BankAccountID { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        public DateTime RecTime { get; set; }

        /// <summary>
        /// 账单科目IDS
        /// </summary>
        public string BillItemIds { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 是否过滤服务方
        /// </summary>
        public bool IsFilterServiceSideKey { get; set; }
        /// <summary>
        /// 地区键值
        /// </summary>
        public string BranchKeys { get; set; }
        /// <summary>
        /// 是否过滤地区
        /// </summary>
        public bool IsFilterBranchKeys { get; set; }

        /// <summary>
        /// 是否检索罚息信息
        /// </summary>
        public bool IsSearchPenaltyInt { get; set; }

        /// <summary>
        /// 是否检索代偿卡信息
        /// </summary>
        public bool IsSearchAdaptationCard { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// Desc:担保和诉讼设置过滤条件
    /// </summary>
    public class BusinessGuaranteeFilter:BaseExportFilter
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPage { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdenNo { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 逾期标记
        /// </summary>
        public string OverdueTag { get; set; }
        /// <summary>
        /// 担保方
        /// </summary>
        public string GuaranteeSideKey { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public byte BusinessStatus { get; set; }
        /// <summary>
        /// 服务方
        /// </summary>
        public string ServiceSideKey { get; set; }
        /// <summary>
        /// 信托方
        /// </summary>
        public string LendingSideKey { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string GuaranteeNum { get; set; }
        /// <summary>
        /// 本息逾期标记
        /// </summary>
        public int CapitalCnt { get; set; }
        /// <summary>
        /// 转担保日期
        /// </summary>
        public string ToGuaranteeFromTime { get; set; }
        /// <summary>
        /// 转担保日期
        /// </summary>
        public string ToGuaranteeToTime { get; set; }
        /// <summary>
        /// 公司键值
        /// </summary>
        public string CompanyKeys { get; set; }        
        /// <summary>
        /// 放贷方名称
        /// </summary>
        public string LendingSideKeyName { get; set; }
        /// <summary>
        /// 选择的业务号
        /// </summary>
        public string BusinessIDs { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }
    }
}

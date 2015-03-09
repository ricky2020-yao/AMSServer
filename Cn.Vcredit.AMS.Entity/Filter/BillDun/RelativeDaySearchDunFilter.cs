using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-15
    /// Description:催收单查询检索条件类
    /// </summary>
    public class RelativeDaySearchDunFilter:BaseFilter
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 催收员姓名
        /// </summary>
        public string Duner { get; set; }
        /// <summary>
        /// 催收单编号
        /// </summary>
        public long DunID { get; set; }
        /// <summary>
        /// 催收单序号
        /// </summary>
        public int DunNumber { get; set; }
        /// <summary>
        /// 是否当期
        /// </summary>
        public bool IsCurrent { get; set; }
        /// <summary>
        /// 期初逾期标记
        /// </summary>
        public int OverMonth { get; set; }
        /// <summary>
        /// 信托方
        /// </summary>
        public string LendingSideKey { get; set; }
        /// <summary>
        /// 最后跟进时间
        /// </summary>
        public string LastTrackTime { get; set; }
        /// <summary>
        /// 门店
        /// </summary>
        public string Store { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 催收单位
        /// </summary>
        public string DunUnit { get; set; }
        /// <summary>
        /// 逾期天数开始
        /// </summary>
        public string StartDays { get; set; }
        /// <summary>
        /// 逾期天数结束
        /// </summary>
        public string EndDays { get; set; }
        /// <summary>
        /// 结果代码
        /// </summary>
        public string DunCode { get; set; }
        /// <summary>
        /// 客户号
        /// </summary>
        public string PersonID { get; set; }
        /// <summary>
        /// 标签类型
        /// </summary>
        public string LabelType { get; set; }
        /// <summary>
        /// 委外时间
        /// </summary>
        public string OutSourceTime { get; set; }
        /// <summary>
        /// 诉讼状态
        /// </summary>
        public byte LawsuitStatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public byte BusinessStatus { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductKind { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 门店催收
        /// </summary>
        public bool IsDunShop { get; set; }
        /// <summary>
        /// 客维催收
        /// </summary>
        public bool IsDunCustomer { get; set; }
        /// <summary>
        /// 客户权限
        /// </summary>
        public string BranchKey { get; set; }
        /// <summary>
        /// 业务号s
        /// </summary>
        public string BusinessIds { get; set; }
        /// <summary>
        /// 获取登录用户所属地区子公司集合
        /// </summary>
        public string MyCompanyIds { get; set; }
        /// <summary>
        /// 配置催收单位权限
        /// </summary>
        public string PermissionsUnitKeys { get; set; }
    }
}

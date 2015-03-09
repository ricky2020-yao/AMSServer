using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// 催收查询条件
    /// </summary>
    public class DunSearchFilter : BaseFilter
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 催收编号
        /// </summary>
        public long DunID { get; set; }
        /// <summary>
        /// 分支键值
        /// </summary>
        public string BranchKey { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 催收员
        /// </summary>
        public string Duner { get; set; }
        /// <summary>
        /// 催收员对应催收单序号
        /// </summary>
        public int DunNumber { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public byte BusinessStatus { get; set; }
        /// <summary>
        /// 诉讼状态
        /// </summary>
        public byte LawsuitStatus { get; set; }
        /// <summary>
        /// 放贷方
        /// </summary>
        public string LendingSideKey { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string PersonName { get; set; }
        /// <summary>
        /// 委外时间
        /// </summary>
        public DateTime OutSourceTime { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductKind { get; set; }
        /// <summary>
        /// 是否是当前催收单
        /// </summary>
        public int IsCurrent { get; set; }
        /// <summary>
        /// 最后跟进时间
        /// </summary>
        public DateTime LastTrackTime { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 催收单位
        /// </summary>
        public string VBSDunUnitCode { get; set; }
        /// <summary>
        /// 结果代码
        /// </summary>
        public string DunCode { get; set; }
        /// <summary>
        /// 开始日期数
        /// </summary>
        public int StartDays { get; set; }
        /// <summary>
        /// 结束日期数
        /// </summary>
        public int EndDays { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int PersonID { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string LabelType { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:固定日扣款顺序表
    /// </summary>
    public class DeductSequence
    {
        #region- 基本属性 -
        /// <summary>
        /// 扣款序列编号
        /// </summary>
        public int DeductSeqID { get; set; }

        /// <summary>
        /// 扣款序列类型（[关联DeductSequence表]：1、21/28/12 2、21/30/5/15 3、浮动）
        /// </summary>
        public byte DSeqType { get; set; }

        /// <summary>
        /// 扣款月份
        /// </summary>
        public string DeductMonth { get; set; }

        /// <summary>
        /// 扣款时间
        /// </summary>
        public DateTime DeductTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 所属公司子公司（服务方）
        /// </summary>
        public int CompanyId{ get; set; }

        /// <summary>
        /// 时间点作用（[Net枚举]：1、扣失 2、罚息 3、扣失加罚息 4、无）
        /// </summary>
        public byte OverdueOperation { get; set; }

        /// <summary>
        /// 扣款区间（[Net枚举]：1、当期帐单 2、逾期帐单 3、当期加逾期 4、无）
        /// </summary>
        public byte BillRegion { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        #endregion
    }
}

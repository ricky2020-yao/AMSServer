using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// 代偿卡更新添加条件
    /// </summary>
    public class AdaptationCardFilter:BaseFilter
    {
        /// <summary>
        /// ID
        /// </summary>
        public int AdaptationCardID { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string CardUser { get; set; }

        /// <summary>
        /// 银行
        /// </summary>
        public string AdaBankName { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string ValidPath { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? ValidEndTime { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string StrValidEndTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string AdaDesc { get; set; }

        /// <summary>
        /// 附件名
        /// </summary>
        public string ValidName { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string AdaName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}

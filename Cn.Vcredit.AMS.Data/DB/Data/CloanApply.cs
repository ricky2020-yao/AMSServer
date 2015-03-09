using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:清贷申请记录表
    /// </summary>
    /// </summary>
    public class CloanApply
    {
        #region- 基本属性 -
        /// <summary>
        /// 主键
        /// </summary>
        public int CloanApplyID { get; set; }

        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 未到期月份
        /// </summary>
        public string UnexpiredMonth { get; set; }

        /// <summary>
        /// 欠费月份
        /// </summary>
        public string OverMonth { get; set; }

        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 清贷类型(提前清贷、坏账清贷)
        /// </summary>
        public byte CloanApplyKind { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public byte CloanApplyStatus { get; set; }

        /// <summary>
        /// 申请人ID
        /// </summary>
        public int ApplyerID { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        public int CheckerID { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? CheckTime { get; set; }
        #endregion

        #region- 扩展属性 -
        /// <summary>
        /// 申请清贷类型
        /// </summary>
        public string StrCloanApplyKind
        {
            get { return CloanApplyKind.ValueToDesc<EnumCloanApplyKind>(); }
        }
        /// <summary>
        /// 业务实体
        /// </summary>
        public virtual Business Business { get; set; }

        /// <summary>
        /// 款项列表
        /// </summary>
        public virtual List<CloanApplyItem> CloanApplyItems { get; set; }
        #endregion
    }
}

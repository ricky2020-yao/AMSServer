using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:产品类型
    /// </summary>
    public enum EnumProductKind : byte
    {
        /// <summary>
        /// 车辆抵押贷款
        /// </summary>
        [Description("沪苏车贷")]
        CarMortgageLoan = 2,

        /// <summary>
        /// 小额贷款
        /// </summary>
        [Description("外贸小贷")]
        UnsecuredLoan = 4,

        /// <summary>
        /// 成都小额贷款
        /// </summary>
        [Description("成都小贷")]
        ChengDuULoan = 5,

        /// <summary>
        /// 外贸楼一贷 2014年4月16日 Add by Baker
        /// </summary>
        [Description("外贸楼一贷")]
        FoticBuildingLoan = 6,

        /// <summary>
        /// 资金来源：国开行
        /// </summary>
        [Description("成都小贷国开行模式")]
        FundFromGKHChengDuULoan = 7,
        /// <summary>
        /// 静安小贷抵押（房贷）
        /// </summary>
        [Description("静安小贷抵押（房贷）")]
        JingAnMortgageLoan = 8,
        /// <summary>
        /// 静安小贷（无抵押）
        /// </summary>
        [Description("静安小贷（无抵押）")]
        JingAnUnMortgageLoan = 9
    }
}

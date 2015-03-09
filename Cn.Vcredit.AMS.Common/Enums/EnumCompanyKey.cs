using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年9月13日
    /// Description:公司键名
    /// </summary>
    public enum EnumCompanyKey : byte
    {
        /// <summary>
        /// 上海维视
        /// </summary>
        [Description("上海维视")]
        WX_SHWS_SERVICE = 1,

        /// <summary>
        /// 上海维信
        /// </summary>
        [Description("上海维信")]
        WX_SHWX_SERVICE = 2,

        /// <summary>
        /// 杭州维仕
        /// </summary>
        [Description("杭州维仕")]
        WX_HZWS_SERVICE = 3,

        /// <summary>
        /// 苏州维信
        /// </summary>
        [Description("苏州维信")]
        WX_SZWX_LEASE = 4,

        /// <summary>
        /// 维仕担保
        /// </summary>
        [Description("维仕担保")]
        WX_HZWS_GUARANTEE = 6,

        /// <summary>
        /// 成都维仕
        /// </summary>
        [Description("成都维仕")]
        WX_CDWS_LENDING = 10,

        /// <summary>
        /// 渤海信托
        /// </summary>
        [Description("上海渤海")]
        BHXT_LENDING = 7,

        /// <summary>
        /// 外贸信托
        /// </summary>
        [Description("上海外贸")]
        DWJM_LENDING = 11,
    }
}

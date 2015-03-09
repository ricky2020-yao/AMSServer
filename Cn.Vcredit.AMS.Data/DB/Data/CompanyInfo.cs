using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月29日
    /// Description:公司信息定义（服务方，放款方，担保方）
    /// </summary>
    public class CompanyInfo
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 父公司编号
        /// </summary>
        public int? SuperCompanyId { get; set; }
        /// <summary>
        /// 公司简称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 地区编号
        /// </summary>
        public int? RegionId { get; set; }
        /// <summary>
        /// 是否服务方
        /// </summary>
        public bool IsServiceSide { get; set; }
        /// <summary>
        /// 是否放贷方
        /// </summary>
        public bool IsLendingSide { get; set; }
        /// <summary>
        /// 是否担保方
        /// </summary>
        public bool IsGuaranteeSide { get; set; }
    }
}

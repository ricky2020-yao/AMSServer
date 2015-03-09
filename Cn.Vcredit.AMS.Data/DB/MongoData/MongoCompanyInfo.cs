using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 公司信息
    /// </summary>
    [MongoTableNameAtrr("Mongo.Sys_CompanyInfo")]
    public class MongoCompanyInfo : MongoDataEntity
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
        /// 简称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public int? RegionId { get; set; }
    }
}

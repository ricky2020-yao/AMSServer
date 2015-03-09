using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.Caches
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月5日
    /// Description:公司信公司信息缓存（服务方，放款方，担保方）
    /// </summary>
    public class CompanyInfoCache
    {
        /// <summary>
        /// 公司信公司信息缓存（服务方，放款方，担保方）
        /// </summary>
        public List<CompanyInfo> CompanyInfos { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public CompanyInfoCache()
        {
            CompanyInfos = Singleton<CompanyInfoDal>.Instance.GetCompanyInfo();
        }
    }
}

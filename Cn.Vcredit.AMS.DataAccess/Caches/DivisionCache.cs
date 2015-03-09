using Cn.Vcredit.AMS.Data.Cache.Data;
using Cn.Vcredit.AMS.DataAccess.BLL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.Caches
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月18日
    /// Description:获取分部操作类
    /// </summary>
    public class DivisionCache
    {
        /// <summary>
        /// 分部缓存
        /// </summary>
        public IList<DivisionV> DivisionVCache { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public DivisionCache()
        {
            DivisionVCache = Singleton<DivisionBLL>.Instance.GetDivisionListAll();
        }
    }
}

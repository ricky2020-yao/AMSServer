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
    /// Description:地区缓存
    /// </summary>
    public class RegionCache
    {
        /// <summary>
        /// 地区缓存
        /// </summary>
        public IList<RegionV> RegionVCache { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public RegionCache()
        {
            RegionVCache = Singleton<RegionBLL>.Instance.GetRegionListAll();
        }

        /// <summary>
        /// 获取所有城市列表
        /// </summary>
        /// <returns></returns>
        public IList<RegionV> GetRegionListAll()
        {
            try
            {
                var RegionList = RegionVCache;
                RegionList = (from q in RegionList
                              join o in GetDivisionListAll()
                              on q.ParentId equals o.SerialId
                              select q).ToList();
                return RegionList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取所有分部
        /// </summary>
        /// <returns></returns>
        private IList<DivisionV> GetDivisionListAll()
        {
            return Singleton<DivisionCache>.Instance.DivisionVCache;
        }
    }
}

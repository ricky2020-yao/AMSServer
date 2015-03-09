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
    /// CreateTime:2014年8月18日
    /// Description:枚举缓存
    /// </summary>
    public class EnumerationCache
    {
        /// <summary>
        /// 地区缓存
        /// </summary>
        public IList<Enumeration> EnumerationAllCache { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public EnumerationCache()
        {
            EnumerationAllCache = Singleton<EnumerationDal>.Instance.GetAllEnumerations();
            List<Enumeration> allChild = new List<Enumeration>();

            foreach (Enumeration e in EnumerationAllCache)
            {
                e.AllChild = RecurAllChild(e).OrderBy(a => a.DisplayOrder).ToList();
            }
        }

        /// <summary>
        /// 递归获取所有下级
        /// </summary>
        /// <param name="enu">枚举对象</param>
        /// <returns></returns>
        public IEnumerable<Enumeration> RecurAllChild(Enumeration enu)
        {
            List<Enumeration> enus = EnumerationAllCache.Where(e => e.Super == enu.Id).ToList();
            if (enus.Count == 0)
            {
                return enus;
            }
            List<Enumeration> tempChild = new List<Enumeration>();
            tempChild.AddRange(enus);
            tempChild.AddRange(enus.SelectMany(c => RecurAllChild(c)));
            return tempChild;
        }

        /// <summary>
        /// 根据指定的Key，获取Enumeration
        /// </summary>
        /// <param name="enumKey"></param>
        /// <returns></returns>
        public Enumeration GetEnumerationByKey(string enumKey)
        {
            Enumeration e = EnumerationAllCache.First(x => x.EnumKey == enumKey);
            if(e!=null)
            {
                e.AllChild = RecurAllChild(e).OrderBy(a => a.DisplayOrder).ToList();
            }

            return e;
        }
    }
}

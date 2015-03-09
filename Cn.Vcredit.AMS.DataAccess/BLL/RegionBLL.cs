using Cn.Vcredit.AMS.Data.Cache.Data;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月18日
    /// Description:获取地区权限操作类
    /// </summary>
    public class RegionBLL
    {
        #region 获取城市
        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<RegionV> GetRegionList(int pageindex, int pagesize, 
            string orderby, int pid, string isactive, ref int recordcount)
        {
            return Singleton<DivisionDal>.Instance.GetRegionList(pageindex, pagesize, 
                orderby, pid, isactive, ref recordcount);
        }

        /// <summary>
        /// 获取在用城市列表(isActive=1)
        /// </summary>
        /// <returns></returns>
        public IList<RegionV> GetRegionList()
        {
            return Singleton<DivisionDal>.Instance.GetRegionList();
        }

        /// <summary>
        /// 获取所有城市列表
        /// </summary>
        /// <returns></returns>
        public IList<RegionV> GetRegionListAll()
        {
            //return Singleton<DivisionDal>.Instance.GetRegionListAll();
            return Singleton<DivisionDalFromMongo>.Instance.GetRegionList();
        }

        /// <summary>
        /// 获取城市单体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RegionV GetRegionbyId(int id)
        {
            return Singleton<DivisionDal>.Instance.GetRegionbyId(id);
        }

        /// <summary>
        /// 根据地区名称获取地区对象
        /// </summary>
        /// <returns></returns>
        public RegionV GetRegionByRegionName(string RegionName)
        {
            return Singleton<DivisionDal>.Instance.GetRegionByRegionName(RegionName);
        }

        /// <summary>
        /// 根据地区fullkey获取地区对象
        /// </summary>
        /// <param name="RegionKey">地区fullkey</param>
        /// <returns></returns>
        public RegionV GetRegionByRegionKey(string RegionKey)
        {
            return Singleton<DivisionDal>.Instance.GetRegionByRegionKey(RegionKey);
        }

        /// <summary>
        /// 获取是否有相同名称或键值的城市
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int HasSameRegion(int id, string name, string key)
        {
            return Singleton<DivisionDal>.Instance.HasSameRegion(id, name, key);
        }
        #endregion

        #region 添加，修改，删除 城市
        /// <summary>
        /// 添加城市
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddRegion(Region entity)
        {
            return Singleton<DivisionDal>.Instance.AddRegion(entity);
        }

        /// <summary>
        /// 修改城市
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateRegion(Region entity)
        {
            return Singleton<DivisionDal>.Instance.UpdateRegion(entity);
        }

        /// <summary>
        /// 改变城市状态
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="isactive">1启用，0停用</param>
        /// <returns></returns>
        public int ChangeStatusRegion(string ids, int isactive)
        {

            try
            {
                string[] idattr = ids.Split(',');
                int[] idattrint = new int[idattr.Length];
                if (idattr.Length > 0)
                {
                    for (int i = 0; i < idattr.Length; i++)
                    {
                        idattrint[i] = Convert.ToInt32(idattr[i]);
                    }
                }
                return Singleton<DivisionDal>.Instance.ChangeStatusRegion(string.Join(",", idattrint), isactive);
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        /// <summary>
        /// 改变城市上级
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="pid">上级ID</param>
        /// <returns></returns>
        public int ChangeParentRegion(string ids, int pid)
        {
            try
            {
                string[] idattr = ids.Split(',');
                int[] idattrint = new int[idattr.Length];
                if (idattr.Length > 0)
                {
                    for (int i = 0; i < idattr.Length; i++)
                    {
                        idattrint[i] = Convert.ToInt32(idattr[i]);
                    }
                }
                return Singleton<DivisionDal>.Instance.ChangeParentRegion(string.Join(",", idattrint), pid);
            }
            catch (Exception ex)
            {
            }
            return 0;
        }
        #endregion
    }
}

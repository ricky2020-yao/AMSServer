using Cn.Vcredit.AMS.Common.Enums;
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
    /// Description:获取分部操作类
    /// </summary>
    public class DivisionBLL
    {
        #region 获取分部
        /// <summary>
        /// 获取分部列表
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<DivisionV> GetDivisionList(int pageindex, int pagesize, string orderby, string isactive, ref int recordcount)
        {
            return Singleton<DivisionDal>.Instance.GetDivisionList(pageindex, pagesize, orderby, isactive, ref recordcount);
        }
        /// <summary>
        /// 获取分部单体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DivisionV GetDivisionbyId(int id)
        {
            return Singleton<DivisionDal>.Instance.GetDivisionbyId(id);

        }
        /// <summary>
        /// 获取是否有相同名称或键值的分部
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int HasSameDivision(int id, string name, string key)
        {
            return Singleton<DivisionDal>.Instance.HasSameDivision(id, name, key);

        }
        /// <summary>
        /// 获取所有分部
        /// </summary>
        /// <returns></returns>
        public IList<DivisionV> GetDivisionListAll()
        {
            //return Singleton<DivisionDal>.Instance.GetDivisionListAll();
            return Singleton<DivisionDalFromMongo>.Instance.GetDivisionList();
        }
        #endregion
        #region 添加，修改，删除 分部
        /// <summary>
        /// 添加分部
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddDivision(Division entity)
        {
            return Singleton<DivisionDal>.Instance.AddDivision(entity);

        }
        /// <summary>
        /// 修改分部
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateDivision(Division entity)
        {
            return Singleton<DivisionDal>.Instance.UpdateDivision(entity);

        }
        /// <summary>
        /// 改变分部状态
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="isactive">1启用，0停用</param>
        /// <returns></returns>
        public int ChangeStatusDivision(string ids, int isactive)
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
                return Singleton<DivisionDal>.Instance.ChangeStatusDivision(string.Join(",", idattrint), isactive);
            }
            catch (Exception ex)
            {
                //ILogger logger = LogFactory.CreateLogger("ChangeStatusDivision");
                //logger.Error(ex.Message, ex);
            }
            return 0;
        }

        #endregion

        #region 获取整棵树
        /// <summary>
        /// 获取组织架构树简易版
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<OrgTreeEntity> GetOrgTreeAllList(int userid)
        {
            return Singleton<DivisionDal>.Instance.GetOrgTreeAllList(userid);
        }
        /// <summary>
        /// 获取组织架构树子集
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<OrgTreeEntity> GetOrgTreeListByPid(string treepid, int userid)
        {

            return Singleton<DivisionDal>.Instance.GetOrgTreeListByPid(treepid, userid);
        }
        /// <summary>
        /// 获取组织架构树子集
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<OrgTreeEntity> GetOrgTreeListByProc(string treepid, int userid)
        {

            return Singleton<DivisionDal>.Instance.GetOrgTreeListByProc(treepid, userid);
        }
        #endregion
        #region 其他
        /// <summary>
        /// 根据级别获取当前组织类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public string GetTypeName(int level)
        {
            if (level == (int)OrgType.Disvison)
            {
                return "分部";
            }
            else if (level == (int)OrgType.Region)
            {
                return "城市";
            }
            else if (level == (int)OrgType.Store)
            {
                return "门店";
            }
            else if (level == (int)OrgType.Team)
            {
                return "团队";
            }
            else if (level == (int)OrgType.Agent)
            {
                return "经办人";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 修改上级
        /// </summary>
        /// <param name="orgtypeid"></param>
        /// <param name="pid"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int ChangeOrgParentByProc(int orgtypeid, int pid, string ids, int memberid)
        {
            return Singleton<DivisionDal>.Instance.ChangeOrgParentByProc(orgtypeid, pid, ids, memberid);
        }
        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="isactive">1启用，0停用</param>
        /// <returns></returns>
        public int ChangeStatusOrg(string ids, int isactive, int memberid)
        {
            return Singleton<DivisionDal>.Instance.ChangeStatusOrg(ids, isactive, memberid);
        }
        /// <summary>
        /// 获取列表项
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="pid"></param>
        /// <param name="orgtypeid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IList<OrgTableEntity> GetOrgChildTableByProc(int pageindex, int pagesize, int pid, int orgtypeid, int userid)
        {
            return Singleton<DivisionDal>.Instance.GetOrgChildTableByProc(pageindex, pagesize, pid, orgtypeid, userid);
        }
        #endregion
    }
}

using Cn.Vcredit.AMS.Data.Cache.Data;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月18日
    /// Description:分布组织结构数据层
    /// </summary>
    public class DivisionDal:BaseDao
    {
        /// <summary>
        /// 系统连接参数
        /// </summary>
        private string Connction = "SysDB";

        #region 获取分部
        /// <summary>
        /// 根据查询条件、排序、参数集，查询城市对象集
        /// </summary>
        /// <param name="sqlWhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="param">参数集</param>
        /// <returns></returns>
        private IList<DivisionV> GetDivisionListByWhere(string sqlWhere, string orderby, IDictionary<string, object> param)
        {
            try
            {
                IList<DivisionV> list = new List<DivisionV>();
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = " order by DisplayOrder";
                }
                sqlWhere = " where FullKey is not null and FullKey!='' " + (String.IsNullOrEmpty(sqlWhere) ? "" : " and " + sqlWhere);

                string sql = @"select  *
                           from [org].[DivisionV] with(nolock) " + sqlWhere + orderby;
                list = Query<DivisionV>(sql, param, Connction);
                return list;
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
        public IList<DivisionV> GetDivisionListAll()
        {
            return GetDivisionListByWhere(null, null, null);
        }

        /// <summary>
        /// 获取分部列表
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<DivisionV> GetDivisionList(int pageindex, int pagesize, string orderby, string isactive, ref int recordcount)
        {
            IList<DivisionV> list = new List<DivisionV>();
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = " divisionName asc";
            }
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(isactive))
            {
                where = "isActive=@isActive";
            }
            string sql = @"select  [serialId],
                                    [divisionName],
                                    [cityId],
                                    [DivisionKey],
                                    [FullKey],
                                    [PermissionKey],
                                    [isActive],
                                    [operatorId],
                                    [updateTime],ChildCount,OrgTypeId from
(select ROW_NUMBER() OVER (ORDER BY  " + orderby + @"  ) AS ROWNUMBER, 
                        [serialId],
                        [divisionName],
                        [cityId],     
                        [DivisionKey],
                        [FullKey],
                        [PermissionKey],
                        [isActive],
                        [operatorId],
                        [updateTime],ChildCount  ,OrgTypeId
						FROM [org].[divisionV] with(nolock)   where " + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize;";

            string sql2 = "select count(1) from [org].[divisionV] with(nolock)   where  " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(isactive))
            {
                param.Add("@isActive", isactive);
            }
            recordcount = Convert.ToInt32(QueryScalar(sql2, param, Connction));
            param.Add("@PageIndex", pageindex);
            param.Add("@PageSize", pagesize);
            list = Query<DivisionV>(sql, param, Connction);
            return list;
        }
        /// <summary>
        /// 获取分部单体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DivisionV GetDivisionbyId(int id)
        {
            string strSql = @"select [serialId],
                                    [divisionName],
                                    [cityId],     
                                    [DivisionKey],
                                    [FullKey],
                                    [PermissionKey],
                                    [isActive],
                                    [operatorId],
                                    [updateTime],ChildCount 
from [org].[DivisionV] with(nolock)  where serialId = @serialId  ";
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@serialId", id);
            var resultList = Query<DivisionV>(strSql, para, Connction, CommandType.Text);
            if (resultList != null && resultList.Count > 0)
                return resultList[0];
            else
                return null;
        }
        /// <summary>
        /// 获取是否有相同名称或键值的分部
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int HasSameDivision(int id, string name, string key)
        {
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(name))
            {
                where += " and divisionName=@divisionName ";
            }
            if (!string.IsNullOrEmpty(key))
            {
                where += " and DivisionKey=@DivisionKey ";
            }
            if (id > 0)
            {
                where += " and serialId<>@serialId ";
            }
            string strSql = @"select count(1) from [org].[division]  where    " + where;
            IDictionary<string, object> para = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(name))
            {
                para.Add("@divisionName", name);
            }
            if (!string.IsNullOrEmpty(key))
            {
                para.Add("@DivisionKey", key);
            }
            if (id > 0)
            {
                para.Add("@serialId", id);
            }
            var result = QueryScalar(strSql, para, Connction, CommandType.Text);
            return Convert.ToInt32(result);
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
            string strSql = @"INSERT  INTO [org].[division]
                                ( [divisionName]
                                  ,[cityId],
                                    [DivisionKey],
                                    [FullKey],
                                    [PermissionKey]
                                  ,[isActive]
                                  ,[operatorId]
                                  ,[updateTime]
                                  ,[CreaTime] 
                                )
                        VALUES  ( @divisionName ,
                                  @cityId ,
                                @DivisionKey,
                                @FullKey,
                                @PermissionKey,
                                  @isActive ,
                                  @operatorId ,
                                  @updateTime  
                                  ,@CreaTime
                                ); SELECT isnull(@@identity,0) ";
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@divisionName", entity.DivisionName);
            param.Add("@cityId", entity.CityId);
            param.Add("@DivisionKey", entity.DivisionKey);
            param.Add("@FullKey", entity.FullKey);
            param.Add("@PermissionKey", entity.PermissionKey);
            param.Add("@isActive", entity.IsActive);
            param.Add("@operatorId", entity.OperatorId);
            param.Add("@updateTime", entity.updateTime);
            param.Add("@CreaTime", entity.CreaTime);
            int result = Convert.ToInt32(QueryScalar(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 修改分部
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateDivision(Division entity)
        {
            string strSql = @" INSERT  INTO [org].[Division_Log]
                        ( [serialId] ,
                          [divisionName] ,
                          [cityId] ,
                          [isActive] ,
                          [DivisionKey] ,
                          [FullKey] ,
                          [PermissionKey] ,
                          [operatorId] ,
                          [DisplayOrder] ,
                          [CreaTime] ,
                          [updateTime] ,
                          [updateuser] ,
                          [updateReason]
                        )
                        SELECT  [serialId] ,
                                [divisionName] ,
                                [cityId] ,
                                [isActive] ,
                                [DivisionKey] ,
                                [FullKey] ,
                                [PermissionKey] ,
                                [operatorId] ,
                                [DisplayOrder] ,
                                [CreaTime] ,
                                GETDATE() ,
                                @updateuser ,
                                '修改分部信息'
                        FROM    [org].[division] WHERE [serialId]=@serialId ;
Update  [org].[division] SET
                                [divisionName]=@divisionName
                                  ,[cityId]=@cityId
                                  ,[DivisionKey]=@DivisionKey
                                  ,[FullKey]=@FullKey
                                  ,[PermissionKey]=@PermissionKey
                                  ,[isActive]=@isActive
                                  ,[updateuser]=@updateuser
                                  ,[updateTime]=@updateTime
                                WHERE [serialId]=@serialId ";
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@serialId", entity.SerialId);
            param.Add("@divisionName", entity.DivisionName);
            param.Add("@cityId", entity.CityId);
            param.Add("@DivisionKey", entity.DivisionKey);
            param.Add("@FullKey", entity.FullKey);
            param.Add("@PermissionKey", entity.PermissionKey);
            param.Add("@isActive", entity.IsActive);
            param.Add("@updateuser", entity.Updateuser);
            param.Add("@updateTime", entity.updateTime);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 改变分部状态
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="isactive">1启用，0停用</param>
        /// <returns></returns>
        public int ChangeStatusDivision(string ids, int isactive)
        {
            string where = " 1=1 ";
            where += " and  serialId in (" + ids + ")";
            string strSql = @"Update  [org].[division] SET [isActive]=@isActive
                                WHERE   " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@isActive", isactive);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }

        #endregion

        #region 获取城市
        /// <summary>
        /// 根据查询条件、排序、参数集，查询城市对象集
        /// </summary>
        /// <param name="sqlWhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="param">参数集</param>
        /// <returns></returns>
        private IList<RegionV> GetRegionListByWhere(string sqlWhere, string orderby, IDictionary<string, object> param)
        {
            try
            {
                IList<RegionV> list = new List<RegionV>();
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = " order by DisplayOrder";
                }
                sqlWhere = " where FullKey is not null and FullKey!='' " + (String.IsNullOrEmpty(sqlWhere) ? "" : " and " + sqlWhere);

                string sql = @"select  [serialId],
                                    [cityId],
                                    [cityName], 
                                    [RegionKey],
                                    [FullKey],
                                    [PermissionKey],
                                    [parentId],
                                    [isActive],
                                    [operatorId],
                                    [updateTime]
                                 ,[parentname],ChildCount
                           from [org].[RegionV] with(nolock) " + sqlWhere + orderby;
                list = Query<RegionV>(sql, param, Connction);
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取所有城市列表(过滤不显示的)
        /// </summary>
        /// <returns></returns>
        public IList<RegionV> GetRegionList()
        {
            string sqlWhere = " isActive=1";
            return GetRegionListByWhere(sqlWhere, null, null);
        }
        /// <summary>
        /// 获取所有城市列表(过滤不显示的)
        /// </summary>
        /// <returns></returns>
        public IList<RegionV> GetRegionListAll()
        {
            return GetRegionListByWhere(null, null, null);
        }
        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<RegionV> GetRegionList(int pageindex, int pagesize, string orderby, int pid, string isactive, ref int recordcount)
        {
            IList<RegionV> list = new List<RegionV>();
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = " cityName asc";
            }
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(isactive))
            {
                where += " and isActive=@isActive";
            }
            if (pid > 0)
            {
                where += " and parentId=@parentId";
            }
            string sql = @"select  [serialId],
                                    [cityId],
                                    [cityName], 
                                    [RegionKey],
                                    [FullKey],
                                    [PermissionKey],
                                    [parentId],
                                    [isActive],
                                    [operatorId],
                                    [updateTime]
                                 ,[parentname],ChildCount,OrgTypeId
                        from (select ROW_NUMBER() OVER (ORDER BY  " + orderby + @"  ) AS ROWNUMBER,
                                [serialId],
                                [cityId],
                                [cityName],    
                                [RegionKey],
                                [FullKey],
                                [PermissionKey],
                                [parentId],
                                [isActive],
                                [operatorId],
                                [updateTime],
                                [parentname] ,ChildCount,OrgTypeId
                        FROM [org].[RegionV] with(nolock)   where " + where + @" ) as temp 
                        where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize;";
            string sql2 = "select count(1) from [org].[RegionV] with(nolock)   where  " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(isactive))
            {
                param.Add("@isActive", isactive);
            }
            if (pid > 0)
            {
                param.Add("@parentId", pid);
            }
            recordcount = Convert.ToInt32(QueryScalar(sql2, param, Connction));
            param.Add("@PageIndex", pageindex);
            param.Add("@PageSize", pagesize);
            list = Query<RegionV>(sql, param, Connction);
            return list;
        }
        /// <summary>
        /// 获取城市单体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RegionV GetRegionbyId(int id)
        {
            string strSql = @"select [serialId],
                                    [cityId],
                                    [cityName],    
                                    [RegionKey],
                                    [FullKey],
                                    [PermissionKey],
                                    [parentId],
                                    [isActive],
                                    [operatorId],
                                    [updateTime],
                                    [parentname],ChildCount,OrgTypeId
from [org].[RegionV] with(nolock) where serialId = @serialId  ";
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@serialId", id);
            var resultList = Query<RegionV>(strSql, para, Connction, CommandType.Text);
            if (resultList != null && resultList.Count > 0)
                return resultList[0];
            else
                return null;
        }
        /// <summary>
        /// 根据地区名称获取地区对象
        /// </summary>
        /// <returns></returns>
        public RegionV GetRegionByRegionName(string RegionName)
        {
            string sqlWhere = " cityName=@cityName";
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@cityName", RegionName);

            return GetRegionListByWhere(sqlWhere, null, para).FirstOrDefault();
        }
        /// <summary>
        /// 根据地区fullkey获取地区对象
        /// </summary>
        /// <param name="RegionKey">地区fullkey</param>
        /// <returns></returns>
        public RegionV GetRegionByRegionKey(string RegionKey)
        {
            string sqlWhere = " fullkey=@RegionKey";
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@RegionKey", RegionKey);

            return GetRegionListByWhere(sqlWhere, null, para).FirstOrDefault();
        }
        /// <summary>
        /// 获取是否有相同名称或键值的城市
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int HasSameRegion(int id, string name, string key)
        {
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(name))
            {
                where += " and cityName=@cityName ";
            }
            if (!string.IsNullOrEmpty(key))
            {
                where += " and RegionKey=@RegionKey ";
            }
            if (id > 0)
            {
                where += " and serialId<>@serialId ";
            }
            string strSql = @"select count(1) from [org].[Region]  where    " + where;
            IDictionary<string, object> para = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(name))
            {
                para.Add("@cityName", name);
            }
            if (!string.IsNullOrEmpty(key))
            {
                para.Add("@RegionKey", key);
            }
            if (id > 0)
            {
                para.Add("@serialId", id);
            }
            var result = QueryScalar(strSql, para, Connction, CommandType.Text);
            return Convert.ToInt32(result);
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
            string strSql = @"INSERT  INTO [org].[Region]
                                ( [cityId]
                                ,[cityName]
                                ,[RegionKey]
                                ,[FullKey]
                                ,[PermissionKey]
                                ,[parentId]
                                ,[isActive]
                                ,[operatorId]
                                ,[updateTime]
                                ,[CreaTime]
                                )
                        VALUES  ( @cityId
      ,@cityName
      ,@RegionKey
      ,@FullKey
      ,@PermissionKey
      ,@parentId
      ,@isActive
      ,@operatorId
      ,@updateTime
      ,@CreaTime
                                ); SELECT isnull(@@identity,0) ";
            IDictionary<string, object> param = new Dictionary<string, object>();

            param.Add("@cityId", entity.CityId);
            param.Add("@cityName", entity.CityName);
            param.Add("@RegionKey", entity.RegionKey);
            param.Add("@FullKey", entity.FullKey);
            param.Add("@PermissionKey", entity.PermissionKey);
            param.Add("@parentId", entity.ParentId);
            param.Add("@isActive", entity.IsActive);
            param.Add("@operatorId", entity.OperatorId);
            param.Add("@updateTime", entity.updateTime);
            param.Add("@CreaTime", entity.CreaTime);
            int result = Convert.ToInt32(QueryScalar(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 修改城市
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateRegion(Region entity)
        {
            string strSql = @" INSERT  INTO [org].[Region_Log]
                        ( [serialId] ,
                          [cityId] ,
                          [cityName] ,
                          [parentId] ,
                          [isActive] ,
                          [RegionKey] ,
                          [FullKey] ,
                          [PermissionKey] ,
                          [operatorId] ,
                          [DisplayOrder] ,
                          [CreaTime] ,
                          [updateTime] ,
                          [updateuser] ,
                          [updateReason]
                        )
                        SELECT  [serialId] ,
                                [cityId] ,
                                [cityName] ,
                                [parentId] ,
                                [isActive] ,
                                [RegionKey] ,
                                [FullKey] ,
                                [PermissionKey] ,
                                [operatorId] ,
                                [DisplayOrder] ,
                                [CreaTime] ,
                                GETDATE() ,
                                @updateuser ,
                                '修改城市信息'
                        FROM    [org].[Region] WHERE [serialId]=@serialId ;
Update  [org].[Region] SET
                                [cityId]=@cityId
      ,[cityName]=@cityName
      ,[RegionKey]=@RegionKey
      ,[FullKey]=@FullKey
      ,[PermissionKey]=@PermissionKey
      ,[parentId]=@parentId
      ,[isActive]=@isActive
      ,[updateuser]=@updateuser
      ,[updateTime]=@updateTime
                                WHERE [serialId]=@serialId ";
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@serialId", entity.SerialId);
            param.Add("@cityId", entity.CityId);
            param.Add("@cityName", entity.CityName);
            param.Add("@RegionKey", entity.RegionKey);
            param.Add("@FullKey", entity.FullKey);
            param.Add("@PermissionKey", entity.PermissionKey);
            param.Add("@parentId", entity.ParentId);
            param.Add("@isActive", entity.IsActive);
            param.Add("@updateuser", entity.Updateuser);
            param.Add("@updateTime", entity.updateTime);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 改变城市状态
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="isactive">1启用，0停用</param>
        /// <returns></returns>
        public int ChangeStatusRegion(string ids, int isactive)
        {
            string where = " 1=1 ";
            string[] idattr = ids.Split(',');
            where += " and  serialId in (" + ids + ")";

            string strSql = @"Update  [org].[Region] SET [isActive]=@isActive
                                WHERE   " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@isActive", isactive);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 改变城市上级
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="pid">上级ID</param>
        /// <returns></returns>
        public int ChangeParentRegion(string ids, int pid)
        {
            string where = " 1=1 ";
            string[] idattr = ids.Split(',');
            where += " and  serialId in (" + ids + ")";

            string strSql = @"Update  [org].[Region] SET [parentid]=@parentid
                                WHERE   " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@parentid", pid);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }
        #endregion

        #region 获取门店
        private IList<StoreV> GetStoreListByWhere(string sqlWhere, string orderby, IDictionary<string, object> param)
        {
            try
            {
                IList<StoreV> list = new List<StoreV>();
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = " order by DisplayOrder";
                }
                sqlWhere = " where FullKey is not null and FullKey!='' " + (String.IsNullOrEmpty(sqlWhere) ? "" : " and " + sqlWhere);
                string sql = @"select [serialId],[storeName],[attributeId], [StoreKey],
                                    [FullKey],
                                    [PermissionKey],[parentId],[isActive],[operatorId]
      ,[updateTime],[parentname],ChildCount from [org].[StoreV] with(nolock) " + sqlWhere + orderby;
                list = Query<StoreV>(sql, param, Connction);
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 获取主门店列表
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<StoreV> GetStoreList(int pageindex, int pagesize, string orderby, int pid, string isactive, ref int recordcount)
        {
            IList<StoreV> list = new List<StoreV>();
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = " storeName asc";
            }
            string where = " 1=1 and StoreType=0 ";
            if (!string.IsNullOrEmpty(isactive))
            {
                where += " and isActive=@isActive";
            }
            if (pid > 0)
            {
                where += " and parentId=@parentId";
            }
            string sql = @"select  [serialId],
                                    [storeName],
                                    [attributeId] ,
                                    [StoreKey],
                                    [FullKey],
                                    [PermissionKey],
                                    [parentId],
                                    [isActive],
                                    [operatorId],
                                    [updateTime],
                                    [parentname],ChildCount,StoreType,OrgTypeId,
    [parentStoreId]
from (select ROW_NUMBER() OVER (ORDER BY  " + orderby + @"  ) AS ROWNUMBER,
                                        [serialId],
                                    [storeName],
                                    [attributeId],
                                    [StoreKey],
                                    [FullKey],
                                    [PermissionKey],
                                    [parentId],
                                    [isActive],
                                    [operatorId],
                                    [updateTime],
[parentname],ChildCount,StoreType,OrgTypeId,
    [parentStoreId] FROM [org].[StoreV] with(nolock)   where " + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize;";
            string sql2 = "select count(1) from [org].[StoreV]   where  " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(isactive))
            {
                param.Add("@isActive", isactive);
            }
            if (pid > 0)
            {
                param.Add("@parentId", pid);
            }
            recordcount = Convert.ToInt32(QueryScalar(sql2, param, Connction));
            param.Add("@PageIndex", pageindex);
            param.Add("@PageSize", pagesize);
            list = Query<StoreV>(sql, param, Connction);
            return list;
        }
        /// <summary>
        /// 获取门店单体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StoreV GetStorebyId(int id)
        {
            string strSql = @"select [serialId],[storeName],[attributeId], [StoreKey],
                                    [FullKey],
                                    [PermissionKey],[parentId],[isActive],[operatorId]
      ,[updateTime],[parentname],ChildCount,StoreType,OrgTypeId,[parentStoreId] from [org].[StoreV] with(nolock)  where serialId = @serialId  ";
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@serialId", id);
            var resultList = Query<StoreV>(strSql, para, Connction, CommandType.Text);
            if (resultList != null && resultList.Count > 0)
                return resultList[0];
            else
                return null;
        }
        /// <summary>
        /// 根据门店fullkey获取门店
        /// </summary>
        /// <param name="StoreKey">门店fullkey</param>
        /// <returns></returns>
        public StoreV GetStoreByStoreKey(string StoreKey)
        {
            string sqlWhere = " fullkey=@StoreKey";
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@StoreKey", StoreKey);

            return GetStoreListByWhere(sqlWhere, null, para).FirstOrDefault();
        }
        /// <summary>
        /// 根据门店名称获取门店
        /// </summary>
        /// <param name="StoreKey">门店名称</param>
        /// <returns></returns>
        public StoreV GetStoreByStoreName(string StoreName)
        {
            string sqlWhere = " StoreName=@StoreName";
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@StoreName", StoreName);

            return GetStoreListByWhere(sqlWhere, null, para).FirstOrDefault();
        }
        /// <summary>
        /// 根据地区fullkey获取门店列表
        /// </summary>
        /// <param name="RegionKey">地区fullkey</param>
        /// <param name="isAll">是否全部</param>
        /// <returns></returns>
        public IList<StoreV> GetStoreListByRegionKey(string RegionKey, bool isAll)
        {
            string sqlWhere = " FullKey like @FullKey+'%'";
            if (isAll != true)
            {
                sqlWhere += " and isActive=1";
            }
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@FullKey", RegionKey);
            return GetStoreListByWhere(sqlWhere, null, para);
        }
        /// <summary>
        /// 获取门店列表
        /// </summary>
        /// <returns></returns>
        public IList<StoreV> GetStoreList()
        {
            string sqlWhere = " isActive=1";
            return GetStoreListByWhere(sqlWhere, null, null);
        }
        /// <summary>
        /// 获取全部门店列表
        /// </summary>
        /// <returns></returns>
        public IList<StoreV> GetStoreListAll()
        {
            return GetStoreListByWhere(null, null, null);
        }
        /// <summary>
        /// 获取是否有相同名称或键值的门店
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int HasSameStore(int id, string name, string key)
        {
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(name))
            {
                where += " and storeName=@storeName ";
            }
            if (!string.IsNullOrEmpty(key))
            {
                where += " and StoreKey=@StoreKey ";
            }
            if (id > 0)
            {
                where += " and serialId<>@serialId ";
            }
            string strSql = @"select count(1) from [org].[Store]  where    " + where;
            IDictionary<string, object> para = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(name))
            {
                para.Add("@storeName", name);
            }
            if (!string.IsNullOrEmpty(key))
            {
                para.Add("@StoreKey", key);
            }
            if (id > 0)
            {
                para.Add("@serialId", id);
            }
            var result = QueryScalar(strSql, para, Connction, CommandType.Text);
            return Convert.ToInt32(result);
        }
        #endregion

        #region 添加，修改，删除 门店
        /// <summary>
        /// 添加门店
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddStore(Store entity)
        {
            string strSql = @"INSERT  INTO [org].[Store]
                                (  [storeName]
                                    ,[attributeId]
                                    ,[parentId]
                                    , [StoreKey],
                                    [FullKey],
                                    [PermissionKey],[isActive]
                                    ,[operatorId]
                                    ,[updateTime],StoreType
                                    ,[parentStoreId]
                                    ,[CreaTime]

                                )
                        VALUES  ( @storeName
                                  ,@attributeId
                                  ,@parentId
                                  ,@StoreKey,
                                    @FullKey,
                                    @PermissionKey,@isActive
                                  ,@operatorId
                                  ,@updateTime,@StoreType
                                    ,@parentStoreId
                                    ,@CreaTime
                                ); SELECT isnull(@@identity,0) ";
            IDictionary<string, object> param = new Dictionary<string, object>();

            param.Add("@storeName", entity.StoreName);
            param.Add("@attributeId", entity.AttributeId);
            param.Add("@parentId", entity.ParentId);
            param.Add("@StoreKey", entity.StoreKey);
            param.Add("@FullKey", entity.FullKey);
            param.Add("@PermissionKey", entity.PermissionKey);
            param.Add("@isActive", entity.IsActive);
            param.Add("@operatorId", entity.OperatorId);
            param.Add("@updateTime", entity.updateTime);
            param.Add("@StoreType", entity.StoreType);
            param.Add("@parentStoreId", entity.ParentStoreId);
            param.Add("@CreaTime", entity.CreaTime);
            int result = Convert.ToInt32(QueryScalar(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 修改门店
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateStore(Store entity)
        {
            string strSql = @" INSERT  INTO [org].[Store_Log]
                        ( [serialId] ,
                          [storeName] ,
                          [attributeId] ,
                          [parentId] ,
                          [isActive] ,
                          [StoreKey] ,
                          [FullKey] ,
                          [PermissionKey] ,
                          [operatorId] ,
                          [DisplayOrder] ,
                          [StoreType] ,
                          [parentStoreId] ,
                          [openedDate] ,
                          [address] ,
                          [CreaTime] ,
                          [updateTime] ,
                          [updateuser] ,
                          [updateReason]
                        )
                        SELECT  [serialId] ,
                                [storeName] ,
                                [attributeId] ,
                                [parentId] ,
                                [isActive] ,
                                [StoreKey] ,
                                [FullKey] ,
                                [PermissionKey] ,
                                [operatorId] ,
                                [DisplayOrder] ,
                                [StoreType] ,
                                [parentStoreId] ,
                                [openedDate] ,
                                [address] ,
                                [CreaTime] ,
                                GETDATE() ,
                                @updateuser ,
                                '修改门店信息'
                        FROM    [org].[Store]  WHERE [serialId]=@serialId ;
Update  [org].[Store] SET
                               [storeName]=@storeName
                                    ,[attributeId]=@attributeId
                                    ,[StoreKey]=@StoreKey
                                    ,[FullKey]=@FullKey
                                    ,[PermissionKey]=@PermissionKey
                                    ,[parentId]=@parentId
                                    ,[isActive]=@isActive
                                    ,[updateuser]=@updateuser
                                    ,[updateTime]=@updateTime
                                    ,[StoreType]=@StoreType
                                    ,[parentStoreId]=@parentStoreId
                                WHERE [serialId]=@serialId ";
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@serialId", entity.SerialId);
            param.Add("@storeName", entity.StoreName);
            param.Add("@attributeId", entity.AttributeId);
            param.Add("@StoreKey", entity.StoreKey);
            param.Add("@FullKey", entity.FullKey);
            param.Add("@PermissionKey", entity.PermissionKey);
            param.Add("@parentId", entity.ParentId);
            param.Add("@isActive", entity.IsActive);
            param.Add("@updateuser", entity.Updateuser);
            param.Add("@updateTime", entity.updateTime);
            param.Add("@StoreType", entity.StoreType);
            param.Add("@parentStoreId", entity.ParentStoreId);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 改变门店状态
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="isactive">1启用，0停用</param>
        /// <returns></returns>
        public int ChangeStatusStore(string ids, int isactive)
        {
            string where = " 1=1 ";
            string[] idattr = ids.Split(',');
            where += " and  serialId in (" + ids + ")";

            string strSql = @"Update  [org].[Store] SET [isActive]=@isActive
                                WHERE   " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@isActive", isactive);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 改变门店上级
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="pid">上级ID</param>
        /// <returns></returns>
        public int ChangeParentStore(string ids, int pid)
        {
            string where = " 1=1 ";
            string[] idattr = ids.Split(',');
            where += " and  serialId in (" + ids + ")";

            string strSql = @"Update  [org].[Store] SET [parentid]=@parentid
                                WHERE   " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@parentid", pid);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }
        #endregion



        #region 获取团队
        private IList<TeamV> GetTeamListByWhere(string sqlWhere, string orderby, IDictionary<string, object> param)
        {
            try
            {
                IList<TeamV> list = new List<TeamV>();
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = " order by DisplayOrder";
                }
                sqlWhere = " where FullKey is not null and FullKey!='' " + (String.IsNullOrEmpty(sqlWhere) ? "" : " and " + sqlWhere);

                string sql = @"select [serialId]
                                      ,[teamName]
                                      ,[parentId]
                                      ,[teamLeader]
                                      ,[isActive]
                                      ,[TeamKey]
                                      ,[FullKey]
                                      ,[operatorId]
                                      ,[updateTime]
                                      ,[DisplayOrder]
                                      ,[ParentName]
                                      ,[ChildCount]
                                      ,[OrgTypeId]
                                from [org].[TeamV] with(nolock) " + sqlWhere + orderby;
                list = Query<TeamV>(sql, param, Connction);
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 根据门店fullkey获取团队列表
        /// </summary>
        /// <param name="StoreKey">门店fullkey</param>
        /// <param name="isAll">是否全部</param>
        /// <returns></returns>
        public IList<TeamV> GetTeamListByStoreKey(string StoreKey, bool isAll)
        {
            string sqlWhere = " FullKey like @FullKey+'%'";
            if (isAll != true)
            {
                sqlWhere += " and isActive=1";
            }
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@FullKey", StoreKey);
            return GetTeamListByWhere(sqlWhere, null, para);
        }
        /// <summary>
        /// 查询可用团队列表
        /// </summary>
        /// <returns></returns>
        public IList<TeamV> GetTeamList()
        {
            string sqlWhere = " isActive=1";
            return GetTeamListByWhere(sqlWhere, null, null);
        }
        /// <summary>
        /// 查询所有团队列表
        /// </summary>
        /// <returns></returns>
        public IList<TeamV> GetTeamListAll()
        {
            return GetTeamListByWhere(null, null, null);
        }
        /// <summary>
        /// 获取团队和子门店列表
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<TeamV> GetTeamAndSubStoreList(int pageindex, int pagesize, string orderby, int pid, string isactive, ref int recordcount)
        {
            IList<TeamV> list = new List<TeamV>();
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = " teamName asc";
            }
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(isactive))
            {
                where += " and isActive=@isActive";
            }
            if (pid > 0)
            {
                where += " and parentId=@parentId";
            }
            string sql = @"select  [serialId]
      ,[teamName]
      ,[parentId]
      ,[teamLeader] ,[teamLeaderName]
      ,[isActive]
      ,[TeamKey]
      ,[FullKey]
      ,[operatorId]
      ,[updateTime]
      ,[ParentName],ChildCount,OrgTypeId
from (select ROW_NUMBER() OVER (ORDER BY  " + orderby + @"  ) AS ROWNUMBER,
                                        [serialId]
      ,[teamName]
      ,[parentId]
      ,[teamLeader],[teamLeaderName]
      ,[isActive]
      ,[TeamKey]
      ,[FullKey]
      ,[operatorId]
      ,[updateTime]
      ,[ParentName],ChildCount,OrgTypeId FROM [org].[vw_TeamAndSubStore]  with(nolock)   where " + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize;";
            string sql2 = "select count(1) from [org].[vw_TeamAndSubStore]   where  " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(isactive))
            {
                param.Add("@isActive", isactive);
            }
            if (pid > 0)
            {
                param.Add("@parentId", pid);
            }
            recordcount = Convert.ToInt32(QueryScalar(sql2, param, Connction));
            param.Add("@PageIndex", pageindex);
            param.Add("@PageSize", pagesize);
            list = Query<TeamV>(sql, param, Connction);
            return list;
        }
        /// <summary>
        /// 获取团队列表
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<TeamV> GetTeamList(int pageindex, int pagesize, string orderby, int pid, string isactive, ref int recordcount)
        {
            IList<TeamV> list = new List<TeamV>();
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = " teamName asc";
            }
            string where = " 1=1 and OrgLevel=4 ";
            if (!string.IsNullOrEmpty(isactive))
            {
                where += " and isActive=@isActive";
            }
            if (pid > 0)
            {
                where += " and parentId=@parentId";
            }
            string sql = @"select  [serialId]
      ,[teamName]
      ,[parentId]
      ,[teamLeader]
      ,[isActive]
      ,[TeamKey]
      ,[FullKey]
      ,[operatorId]
      ,[updateTime]
      ,[ParentName],ChildCount,OrgTypeId
from (select ROW_NUMBER() OVER (ORDER BY  " + orderby + @"  ) AS ROWNUMBER,
                                        [serialId]
      ,[teamName]
      ,[parentId]
      ,[teamLeader]
      ,[isActive]
      ,[TeamKey]
      ,[FullKey]
      ,[operatorId]
      ,[updateTime]
      ,[ParentName],ChildCount,OrgTypeId FROM [org].[TeamV]  with(nolock)   where " + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize;";
            string sql2 = "select count(1) from [org].[TeamV]   where  " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(isactive))
            {
                param.Add("@isActive", isactive);
            }
            if (pid > 0)
            {
                param.Add("@parentId", pid);
            }
            recordcount = Convert.ToInt32(QueryScalar(sql2, param, Connction));
            param.Add("@PageIndex", pageindex);
            param.Add("@PageSize", pagesize);
            list = Query<TeamV>(sql, param, Connction);
            return list;
        }
        /// <summary>
        /// 获取团队单体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TeamV GetTeambyId(int id)
        {
            string strSql = @"select [serialId]
      ,[teamName]
      ,[parentId]
      ,[teamLeader]
      ,[isActive]
      ,[TeamKey]
      ,[FullKey]
      ,[operatorId]
      ,[updateTime]
      ,[ParentName],ChildCount,OrgTypeId from [org].[TeamV]  with(nolock)  where serialId = @serialId  ";
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@serialId", id);
            var resultList = Query<TeamV>(strSql, para, Connction, CommandType.Text);
            if (resultList != null && resultList.Count > 0)
                return resultList[0];
            else
                return null;
        }
        /// <summary>
        /// 根据经办人编号，查询所属团队
        /// </summary>
        /// <param name="AgentID">经办人编号</param>
        /// <returns></returns>
        public TeamV GetTeamByAgentID(int AgentID)
        {
            string sqlWhere = " serialId=(select parentId from org.agent where userId=" + AgentID + ")";
            return GetTeamListByWhere(sqlWhere, null, null).FirstOrDefault();
        }
        /// <summary>
        /// 获取是否有相同名称或键值的团队
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int HasSameTeam(int id, string name, string key)
        {
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(name))
            {
                where += " and teamName=@teamName ";
            }
            if (!string.IsNullOrEmpty(key))
            {
                where += " and TeamKey=@TeamKey ";
            }
            if (id > 0)
            {
                where += " and serialId<>@serialId ";
            }
            string strSql = @"select count(1) from [org].[Team]  where    " + where;
            IDictionary<string, object> para = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(name))
            {
                para.Add("@teamName", name);
            }
            if (!string.IsNullOrEmpty(key))
            {
                para.Add("@TeamKey", key);
            }
            if (id > 0)
            {
                para.Add("@serialId", id);
            }
            var result = QueryScalar(strSql, para, Connction, CommandType.Text);
            return Convert.ToInt32(result);
        }
        #endregion

        #region 添加，修改，删除 团队
        /// <summary>
        /// 添加团队
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddTeam(Team entity)
        {
            string strSql = @"INSERT INTO [org].[Team]
                                (  [teamName]
                                  ,[parentId]
                                  ,[teamLeader]
                                  ,[isActive]
                                  ,[TeamKey]
                                  ,[FullKey]
                                  ,[operatorId]
                                  ,[updateTime] 
                                  ,[CreaTime] 
                                )
                        VALUES  (  @teamName
                                  ,@parentId
                                  ,@teamLeader
                                  ,@isActive
                                  ,@TeamKey
                                  ,@FullKey
                                  ,@operatorId
                                  ,@updateTime
                                  ,@CreaTime
                                ); SELECT isnull(@@identity,0) ";
            IDictionary<string, object> param = new Dictionary<string, object>();

            param.Add("@teamName", entity.TeamName);
            param.Add("@parentId", entity.ParentId);
            param.Add("@teamLeader", entity.TeamLeader);
            param.Add("@isActive", entity.IsActive);
            param.Add("@TeamKey", entity.TeamKey);
            param.Add("@FullKey", entity.FullKey);
            param.Add("@operatorId", entity.OperatorId);
            param.Add("@updateTime", entity.updateTime);
            param.Add("@CreaTime", entity.CreaTime);
            int result = Convert.ToInt32(QueryScalar(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 修改团队
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateTeam(Team entity)
        {
            string strSql = @"INSERT  INTO [org].[Team_Log]
                        ( [serialId] ,
                          [teamName] ,
                          [parentId] ,
                          [teamLeader] ,
                          [isActive] ,
                          [TeamKey] ,
                          [FullKey] ,
                          [operatorId] ,
                          [CreaTime] ,
                          [DisplayOrder] ,
                          [updateTime] ,
                          [updateuser] ,
                          [updateReason]
                        )
                        SELECT  [serialId] ,
                                [teamName] ,
                                [parentId] ,
                                [teamLeader] ,
                                [isActive] ,
                                [TeamKey] ,
                                [FullKey] ,
                                [operatorId] ,
                                [CreaTime] ,
                                [DisplayOrder] ,
                                GETDATE() ,
                                @updateuser ,
                                '修改团队信息'
                        FROM    [org].[Team]   WHERE [serialId]=@serialId ;
Update  [org].[Team] SET
                               [teamName]=@teamName
                                  ,[parentId]=@parentId
                                  ,[teamLeader]=@teamLeader
                                  ,[isActive]=@isActive
                                  ,[TeamKey]=@TeamKey
                                  ,[FullKey]=@FullKey
                                  ,[updateuser]=@updateuser
                                  ,[updateTime]=@updateTime
                                WHERE [serialId]=@serialId ;
";
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@serialId", entity.SerialId);
            param.Add("@teamName", entity.TeamName);
            param.Add("@parentId", entity.ParentId);
            param.Add("@teamLeader", entity.TeamLeader);
            param.Add("@isActive", entity.IsActive);
            param.Add("@TeamKey", entity.TeamKey);
            param.Add("@FullKey", entity.FullKey);
            param.Add("@updateuser", entity.Updateuser);
            param.Add("@updateTime", entity.updateTime);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 改变团队状态
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="isactive">1启用，0停用</param>
        /// <returns></returns>
        public int ChangeStatusTeam(string ids, int isactive)
        {
            string where = " 1=1 ";
            string[] idattr = ids.Split(',');
            where += " and  serialId in (" + ids + ")";

            string strSql = @"Update  [org].[Team] SET [isActive]=@isActive
                                WHERE   " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@isActive", isactive);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 改变团队上级
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="pid">上级ID</param>
        /// <returns></returns>
        public int ChangeParentTeam(string ids, int pid)
        {
            string where = " 1=1 ";
            string[] idattr = ids.Split(',');
            where += " and  serialId in (" + ids + ")";

            string strSql = @"Update  [org].[Team] SET [parentid]=@parentid
                                WHERE   " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@parentid", pid);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }
        #endregion


        #region 获取经办人
        private IList<AgentV> GetAgentListByWhere(string sqlWhere, string orderby, IDictionary<string, object> param)
        {
            try
            {
                IList<AgentV> list = new List<AgentV>();
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = " order by DisplayOrder";
                }
                sqlWhere = " where FullKey is not null " + (String.IsNullOrEmpty(sqlWhere) ? "" : " and " + sqlWhere);

                string sql = @"select *
                                from [org].[AgentV] with(nolock) " + sqlWhere + orderby;
                list = Query<AgentV>(sql, param, Connction);
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取所有可用经办人
        /// </summary>
        /// <returns></returns>
        public IList<AgentV> GetAgentList()
        {
            string sqlWhere = " and isActive=1";
            return GetAgentListByWhere(sqlWhere, null, null);
        }
        /// <summary>
        /// 获取所有经办人
        /// </summary>
        /// <returns></returns>
        public IList<AgentV> GetAgentListAll()
        {
            return GetAgentListByWhere(null, null, null);
        }
        /// <summary>
        /// 根据团队fullkey获取经办人列表
        /// </summary>
        /// <param name="TeamKey">团队fullkey</param>
        /// <returns></returns>
        public IList<AgentV> GetAgentListByTeamKey(string TeamKey)
        {
            string sqlWhere = " FullKey like @FullKey+'%'";
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@FullKey", TeamKey);
            return GetAgentListByWhere(sqlWhere, null, para);
        }
        /// <summary>
        /// 根据团队名称获取经办人列表
        /// </summary>
        /// <param name="TeamName">团队名称</param>
        /// <param name="isAll">是否全部</param>
        /// <returns></returns>
        public IList<AgentV> GetAgentListByTeamName(string TeamName, bool isAll)
        {
            string sqlWhere = " ParentName=@TeamName";
            if (isAll != true)
            {
                sqlWhere += " and isActive=1";
            }
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@TeamName", TeamName);
            return GetAgentListByWhere(sqlWhere, null, para);
        }
        /// <summary>
        /// 获取经办人列表
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<AgentV> GetAgentList(int pageindex, int pagesize, string orderby, int pid, string isactive, ref int recordcount)
        {
            IList<AgentV> list = new List<AgentV>();
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = " UserName asc";
            }
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(isactive))
            {
                where += " and isActive=@isActive";
            }
            if (pid > 0)
            {
                where += " and parentId=@parentId";
            }
            string sql = @"select  [serialId]
                                  ,[UserId]
                                  ,[UserName]
                                  ,[parentId]
                                  ,[isActive]
                                  ,[agentKey]
                                  ,[FullKey]
                                  ,[operatorId]
                                  ,[updateTime]
                                  ,[ParentName] ,OrgTypeId
from (select ROW_NUMBER() OVER (ORDER BY  " + orderby + @"  ) AS ROWNUMBER,
                                     [serialId]
                                      ,[UserId]
                                      ,[UserName]
                                      ,[parentId]
                                      ,[isActive]
                                      ,[agentKey]
                                      ,[FullKey]
                                      ,[operatorId]
                                      ,[updateTime]
      ,[ParentName],OrgTypeId FROM [org].[AgentV] with(nolock)   where " + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize;";
            string sql2 = "select count(1) from [org].[AgentV] with(nolock)   where  " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(isactive))
            {
                param.Add("@isActive", isactive);
            }
            if (pid > 0)
            {
                param.Add("@parentId", pid);
            }
            recordcount = Convert.ToInt32(QueryScalar(sql2, param, Connction));
            param.Add("@PageIndex", pageindex);
            param.Add("@PageSize", pagesize);
            list = Query<AgentV>(sql, param, Connction);
            return list;
        }
        /// <summary>
        /// 获取经办人单体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AgentV GetAgentbyId(int id)
        {
            string strSql = @"select [serialId]
                                  ,[UserId]
                                  ,[UserName]
                                  ,[parentId]
                                  ,[isActive]
                                  ,[agentKey]
                                  ,[FullKey]
                                  ,[operatorId]
                                  ,[updateTime]
                                  ,[ParentName],OrgTypeId  from [org].[AgentV] with(nolock)  where serialId = @serialId  ";
            IDictionary<string, object> para = new Dictionary<string, object>();
            para.Add("@serialId", id);
            var resultList = Query<AgentV>(strSql, para, Connction, CommandType.Text);
            if (resultList != null && resultList.Count > 0)
                return resultList[0];
            else
                return null;
        }
        /// <summary>
        ///根据经办人编号，获取经办人单体
        /// </summary>
        /// <param name="AgentID">经办人编号</param>
        /// <returns></returns>
        public AgentV GetAgentbyAgentID(int AgentID)
        {
            string sqlWhere = " UserId=" + AgentID;
            return GetAgentListByWhere(sqlWhere, null, null).FirstOrDefault();
        }
        /// <summary>
        /// 获取是否有相同名称或键值的经办人
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int HasSameAgent(int id, string name, string key)
        {
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(name))
            {
                where += " and UserName=@UserName ";
            }
            if (!string.IsNullOrEmpty(key))
            {
                where += " and agentKey=@agentKey ";
            }
            if (id > 0)
            {
                where += " and serialId<>@serialId ";
            }
            string strSql = @"select count(1) from [org].[Agent]  where    " + where;
            IDictionary<string, object> para = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(name))
            {
                para.Add("@UserName", name);
            }
            if (!string.IsNullOrEmpty(key))
            {
                para.Add("@agentKey", key);
            }
            if (id > 0)
            {
                para.Add("@serialId", id);
            }
            var result = QueryScalar(strSql, para, Connction, CommandType.Text);
            return Convert.ToInt32(result);
        }
        #endregion

        #region 添加，修改，删除 经办人
        /// <summary>
        /// 添加经办人
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddAgent(Agent entity)
        {
            string strSql = @"INSERT  INTO [org].[Agent]
                                ( [UserId]
                                  ,[UserName]
                                  ,[parentId]
                                  ,[isActive]
                                  ,[agentKey]
                                  ,[FullKey]
                                  ,[operatorId]
                                  ,[updateTime] 
                                  ,[CreaTime] 
                                )
                        VALUES  (  @UserId
                                  ,@UserName
                                  ,@parentId
                                  ,@isActive
                                  ,@agentKey
                                  ,@FullKey
                                  ,@operatorId
                                  ,@updateTime
                                  ,@CreaTime
                                ); SELECT isnull(@@identity,0) ";
            IDictionary<string, object> param = new Dictionary<string, object>();

            param.Add("@UserId", entity.UserId);
            param.Add("@UserName", entity.UserName);
            param.Add("@parentId", entity.ParentId);
            param.Add("@agentKey", entity.agentKey);
            param.Add("@FullKey", entity.FullKey);
            param.Add("@isActive", entity.IsActive);
            param.Add("@operatorId", entity.OperatorId);
            param.Add("@updateTime", entity.updateTime);
            param.Add("@CreaTime", entity.CreaTime);
            int result = Convert.ToInt32(QueryScalar(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 修改经办人
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateAgent(Agent entity)
        {
            string strSql = @" INSERT INTO [org].[Agent_log](
                                [serialId],
                                [UserId],
                                [UserName],
                                [parentId],
                                [isActive],
                                [agentKey],
                                [FullKey],
                                [operatorId],
                                [DisplayOrder],
                                [updateTime],
                                updateUser,
                                updateReason)
		              SELECT [serialId], 
                                [UserId],
                                [UserName],
                                [parentId],
                                [isActive],
                                [agentKey],
                                [FullKey],
                                [operatorId],
                                [DisplayOrder],
                                GETDATE(),
                                @updateuser,'修改经办人' FROM [org].[Agent]  
				  	 WHERE [serialId]=@serialId;
            Update  [org].[Agent] SET
                             [UserId]=@UserId
                                  ,[UserName]=@UserName
                                  ,[parentId]=@parentId
                                  ,[isActive]=@isActive
                                  ,[agentKey]=@agentKey
                                  ,[FullKey]=@FullKey
                                  ,[updateuser]=@updateuser
                                  ,[updateTime]=@updateTime
                                WHERE [serialId]=@serialId ;
		    
		
";
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@serialId", entity.SerialId);
            param.Add("@UserId", entity.UserId);
            param.Add("@UserName", entity.UserName);
            param.Add("@agentKey", entity.agentKey);
            param.Add("@FullKey", entity.FullKey);
            param.Add("@parentId", entity.ParentId);
            param.Add("@isActive", entity.IsActive);
            param.Add("@updateuser", entity.Updateuser);
            param.Add("@updateTime", entity.updateTime);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 改变经办人状态
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="isactive">1启用，0停用</param>
        /// <returns></returns>
        public int ChangeStatusAgent(string ids, int isactive)
        {
            string where = " 1=1 ";
            string[] idattr = ids.Split(',');
            where += " and  serialId in (" + ids + ")";

            string strSql = @"Update  [org].[Agent] SET [isActive]=@isActive
                                WHERE   " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@isActive", isactive);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;
        }
        /// <summary>
        /// 改变经办人上级
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="pid">上级ID</param>
        /// <returns></returns>
        public int ChangeParentAgent(string ids, int pid)
        {
            string where = " 1=1 ";
            string[] idattr = ids.Split(',');
            where += " and  serialId in (" + ids + ")";

            string strSql = @"Update  [org].[Agent] SET [parentid]=@parentid
                                WHERE   " + where;
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@parentid", pid);
            int result = Convert.ToInt32(Execute(strSql, param, Connction, CommandType.Text));
            return result;

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
            IList<OrgTreeEntity> list = new List<OrgTreeEntity>();
            //            string sql = @"select  [id],treeid,treepid
            //      ,[name]
            //      ,[orgtypeid]
            //      ,[pId],childcount,issub
            //  FROM  [org].[vw_OrgTree] "; 
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@userid", userid);
            list = Query<OrgTreeEntity>("[org].[proc_org_GetOrgTree]", param, Connction, CommandType.StoredProcedure);
            return list;

        }
        /// <summary>
        /// 获取组织架构树子集
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<OrgTreeEntity> GetOrgTreeListByPid(string treepid, int userid)
        {
            IList<OrgTreeEntity> list = new List<OrgTreeEntity>();
            string sql = "";
            if (userid > 0)
            {
                sql = @"select  [id],treeid,treepid
                                ,[name]
                                ,[orgtypeid]
                                ,[pId],childcount,issub,fullkey,teamleader,teamleaderName
                                FROM  [org].[vw_OrgTree] a  with(NOLOCK)   INNER  JOIN 
                                [user].[AccountOrgRelation] b with(NOLOCK) 
                                ON b.[orgLevel]=a.orgtypeid 
                                AND b.orgId=a.id  where a.treepid=@treepid and b.UserId=@userid";
            }
            else
            {
                sql = @"select  [id],treeid,treepid
                                ,[name]
                                ,[orgtypeid]
                                ,[pId],childcount,issub,fullkey,teamleader,teamleaderName
                                FROM  [org].[vw_OrgTree] a where a.treepid=@treepid ";
            }
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@treepid", treepid);
            if (userid > 0)
            {
                param.Add("@userid", userid);
            }
            list = Query<OrgTreeEntity>(sql, param, Connction);
            return list;
        }
        /// <summary>
        /// 获取组织架构树子集
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<OrgTreeEntity> GetOrgTreeListByProc(string treepid, int userid)
        {
            IList<OrgTreeEntity> list = new List<OrgTreeEntity>();

            IDictionary<string, object> param = new Dictionary<string, object>();

            param.Add("@treepid", treepid);
            param.Add("@userid", userid);
            list = Query<OrgTreeEntity>("[org].[proc_org_GetOrgTreeForM]", param, Connction, CommandType.StoredProcedure);
            return list;
        }

        /// <summary>
        /// 修改上级
        /// </summary>
        /// <param name="orgtypeid"></param>
        /// <param name="pid"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int ChangeOrgParentByProc(int orgtypeid, int pid, string treeids, int memberid)
        {
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@pid", pid);
            param.Add("@treeids", treeids);
            param.Add("@memberid", memberid);
            List<int> list = Query<int>("[org].[proc_org_ChangeOrgParent]", param, Connction, CommandType.StoredProcedure);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return 0;
        }
        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="ids">id字符串，以“，”隔开</param>
        /// <param name="isactive">1启用，0停用</param>
        /// <returns></returns>
        public int ChangeStatusOrg(string ids, int isactive, int memberid)
        {
            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@status", isactive);
            param.Add("@treeids", ids);
            param.Add("@memberid", memberid);
            List<int> list = Query<int>("[org].[proc_org_ChangeOrgStatus]", param, Connction, CommandType.StoredProcedure);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return 0;
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
            IList<OrgTableEntity> list = new List<OrgTableEntity>();

            IDictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@pageindex", pageindex);
            param.Add("@pagesize", pagesize);
            param.Add("@pid", pid);
            param.Add("@orgtypeid", orgtypeid);
            param.Add("@userid", userid);
            list = Query<OrgTableEntity>("[org].[proc_org_GetOrgTableByPRI]", param, Connction, CommandType.StoredProcedure);
            return list;
        }
        #endregion
    }
}

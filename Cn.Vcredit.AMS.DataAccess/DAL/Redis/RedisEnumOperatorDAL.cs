using Cn.Vcredit.AMS.Data.DB.RedisData;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.DAL.Redis
{
    /// <summary>
    /// Redis枚举类型数据操作类
    /// </summary>
    public class RedisEnumOperatorDAL:BaseDao
    {
        /// <summary>
        /// 获取枚举类列表
        /// </summary>
        /// <returns></returns>
        public List<RedisDataEntity> GetEnumData()
        {
            string sql = "SELECT FullKey AS [Key], Name FROM [common].[EnumerationAll] WITH (NOLOCK)";
            return Query<RedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取枚举类列表
        /// </summary>
        /// <returns></returns>
        public List<RedisDataEntity> GetUserData()
        {
            string sql = "SELECT Id AS [Key], Name FROM [user].[User] WITH (NOLOCK)";
            return Query<RedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取指定类型枚举
        /// </summary>
        /// <param name="typeFullKey"></param>
        /// <returns></returns>
        public List<EnumRedisDataEntity> GetTypeEnumData(string typeFullKey)
        {
            string sql = @"SELECT FullKey AS [Key], Name, Value FROM [common].[EnumerationAll] WITH (NOLOCK)
                            WHERE Super = (SELECT Id FROM [common].[EnumerationAll] WITH (NOLOCK) WHERE FullKey = '" + typeFullKey + "')"
                         + " ORDER BY DisplayOrder asc";
            return Query<EnumRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取指定类型枚举
        /// </summary>
        /// <param name="typeFullKey"></param>
        /// <returns></returns>
        public List<EnumRedisDataEntity> GetTypeEnumDataForValue(string typeFullKey)
        {
            string sql = @"SELECT Value AS [Key], Name, FullKey AS Value FROM [common].[EnumerationAll] WITH (NOLOCK)
                             WHERE Super = (SELECT Id FROM [common].[EnumerationAll] WITH (NOLOCK) WHERE FullKey = '" + typeFullKey + "')";
            sql += "         ORDER BY DisplayOrder asc";
            return Query<EnumRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取地区枚举类型
        /// </summary>
        /// <returns></returns>
        public List<EnumRedisDataEntity> GetRegionEnumData()
        {
            string sql = "SELECT FullKey AS [Key], cityName AS Name, cityId AS Value FROM [org].[Region] WITH (NOLOCK)";
            return Query<EnumRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取门店枚举类型
        /// </summary>
        /// <returns></returns>
        public List<EnumRedisDataEntity> GetStoreEnumData()
        {
            string sql = "SELECT FullKey AS [Key], storeName AS Name, serialId AS Value FROM [org].[Store] WITH (NOLOCK)";
            return Query<EnumRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取指定类型枚举
        /// </summary>
        /// <param name="typeFullKeys">多个枚举值的拼接,用","分割</param>
        /// <returns></returns>
        public List<EnumRedisDataEntity> GetTypeEnumListData(string typeFullKeys)
        {
            string sql = @"SELECT a.Id AS EnumID, a.Super, a.FullKey AS [Key], a.Name, a.Value
                       , a.DisplayOrder, a.IsDisable, a.IsDelete, b.FullKey AS SuperFullKey, a.Description, a.Region
                        FROM [common].[EnumerationAll] a WITH (NOLOCK)
                        JOIN [common].[EnumerationAll] b WITH (NOLOCK)
                        ON a.Super = b.Id 
                        WHERE b. FullKey IN ('" + typeFullKeys + "')";
                     sql += " ORDER BY a.DisplayOrder asc";
            return Query<EnumRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取门店的枚举
        /// </summary>
        /// <returns></returns>
        public List<EnumRedisDataEntity> GetBranchEnumData()
        {
            string sql = @"SELECT  FullKey AS [Key] ,
                                    Name ,
                                    Value
                            FROM    [common].[EnumerationAll] WITH ( NOLOCK )
                            WHERE   Super IN ( SELECT   Id
                                               FROM     [common].[EnumerationAll] WITH ( NOLOCK )
                                               WHERE    FullKey LIKE 'SUBSIDIARY%' )
                            ORDER BY DisplayOrder ASC";
            return Query<EnumRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取地区枚举类型列表
        /// </summary>
        /// <returns></returns>
        public List<RegionRedisDataEntity> GetRegionEnumListData()
        {
            string sql = @"SELECT  serialId AS EnumID ,
                                    parentId AS Super ,
                                    FullKey AS [Key] ,
                                    cityName AS Name ,
                                    cityId AS Value ,
                                    DisplayOrder ,
                                    isActive AS IsDisable ,
                                    'REGION' AS SuperFullKey
                            FROM    [org].[Region] WITH ( NOLOCK )
                            ORDER BY cityName";

            return Query<RegionRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取门店枚举类型列表
        /// </summary>
        /// <returns></returns>
        public List<StoreRedisDataEntity> GetStoreEnumListData()
        {
            string sql = @"SELECT  serialId AS EnumID ,
                                    parentId AS Super ,
                                    FullKey AS [Key] ,
                                    storeName AS Name ,
                                    serialId AS Value ,
                                    DisplayOrder ,
                                    isActive AS IsDisable ,
                                    'STORE' AS SuperFullKey
                            FROM    [org].[Store] WITH ( NOLOCK )
                            ORDER BY storeName";
            return Query<StoreRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }
        
        /// <summary>
        /// 获取团队信息
        /// </summary>
        /// <returns></returns>
        public List<TeamRedisDataEntity> GetTeamRedisListData()
        {
            string sql = @"SELECT  t.serialId AS Id ,
                                    t.teamName AS Name ,
                                    t.parentId AS ParentId ,
                                    t.FullKey AS [Key] ,
                                    t.DisplayOrder AS DisplayOrder ,
                                    s.FullKey AS ParentFullKey ,
                                    t.isActive AS IsActive
                            FROM    [org].[Team] t WITH ( NOLOCK )
                            JOIN    [org].[Store] s WITH ( NOLOCK ) ON t.parentId = s.serialId";

            return Query<TeamRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取地区门店关系信息
        /// </summary>
        /// <returns></returns>
        public List<RegionStoreRedisEntity> GetRegionStoreRedisListData()
        {
            string sql = @"SELECT  s.serialId AS Id ,
                                    s.storeName AS Name ,
                                    s.parentId AS ParentId ,
                                    s.FullKey AS [Key] ,
                                    s.DisplayOrder AS DisplayOrder ,
                                    r.FullKey AS ParentFullKey ,
                                    s.isActive AS IsActive
                            FROM    [org].[Region] r WITH ( NOLOCK )
                            JOIN    [org].[Store] s WITH ( NOLOCK ) ON r.serialId = s.parentId";

            return Query<RegionStoreRedisEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取权限数据
        /// </summary>
        /// <returns></returns>
        public List<PermissionRedisDataEntity> GetPermissionRedisListData()
        {
            string sql = @"SELECT  r.Id ,
                                u.UserId ,
                                p.FullKey AS [Key] ,
                                p.Name
                        FROM    [user].RolePermission AS r WITH ( NOLOCK )
                                INNER JOIN [user].RoleAccount AS a WITH ( NOLOCK ) ON r.RoleId = a.RoleId
                                INNER JOIN [user].Permission AS p WITH ( NOLOCK ) ON r.PermissionKey = p.FullKey
                                INNER JOIN [user].Account AS u WITH ( NOLOCK ) ON a.AccountId = u.Id";

            return Query<PermissionRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取地区权限
        /// </summary>
        /// <returns></returns>
        public List<PermissionRedisDataEntity> GetRegionPermissionRedisListData()
        {
            string sql = @"SELECT DISTINCT
                            Id ,
                            UserId ,
                            [Key] ,
                            Name
                    FROM    ( SELECT    r.serialId AS Id ,
                                        a.UserId ,
                                        r.FullKey AS [Key] ,
                                        r.cityName AS Name
                              FROM      [user].[AccountOrgRelation] a WITH ( NOLOCK )
                                        JOIN [org].[RegionV] r WITH ( NOLOCK ) ON a.orgId = r.serialId
                              WHERE     orgLevel = 2
                              UNION ALL
                              SELECT    r.serialId AS Id ,
                                        a.UserId ,
                                        r.FullKey AS [Key] ,
                                        r.cityName AS Name
                              FROM      [user].[AccountOrgRelation] a WITH ( NOLOCK )
                                        JOIN [org].[DivisionV] d WITH ( NOLOCK ) ON a.orgId = d.serialId
                                        JOIN [org].[RegionV] r WITH ( NOLOCK ) ON d.serialId = r.parentId
                              WHERE     orgLevel = 1
                            ) r
                    ORDER BY r.UserId ";
            return Query<PermissionRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取地区权限
        /// </summary>
        /// <returns></returns>
        public List<PermissionRedisDataEntity> GetDivisionPermissionRedisListData()
        {
            string sql = @"SELECT  d.serialId AS Id ,
                                    a.UserId ,
                                    d.FullKey AS [Key] ,
                                    d.divisionName AS Name
                            FROM    [user].[AccountOrgRelation] a WITH ( NOLOCK )
                            JOIN [org].[DivisionV] d WITH ( NOLOCK ) ON a.orgId = d.serialId
                            WHERE   orgLevel = 1";
            return Query<PermissionRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取银行账户信息
        /// </summary>
        /// <returns></returns>
        public List<BankAccountRedisEntity> GetBankAccountRedisEntity()
        {
            string sql = @"SELECT  BankAccountID,
                                   CompanyKey,
                                   BankKey,
                                   BankName,
                                   AccountNumber,
                                   SavingCard,
                                   ProtocolNo,
                                   ProvinceName,
                                   AreaName,
                                   Region,
                                   AreaCode,
                                   ApplicationNo,
                                   CreateTime,
                                   PaymentAccount,
                                   CompanyType,
                                   IsOpen
                            FROM   [dbo].[BankAccount] a WITH ( NOLOCK )";
            return Query<BankAccountRedisEntity>(sql, null, "PostLoanDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取关帐日信息
        /// </summary>
        /// <returns></returns>
        public List<CloseBillDayRedisEntity> GetCloseBillDayRedisEntity()
        {
            string sql = @"SELECT  CloseBillDayID,
                                   CompanyKey,
                                   OriginalTime,
                                   LatestTime,
                                   CreateTime,
                                   OperatorID
                            FROM   [dbo].[CloseBillDay] a WITH ( NOLOCK )";
            return Query<CloseBillDayRedisEntity>(sql, null, "PostLoanDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取账户关帐日信息
        /// </summary>
        /// <returns></returns>
        public List<AccountingCloseBillDayRedisEntity> GetAccountingCloseBillDayRedisEntity()
        {
            string sql = @"SELECT  ID,
                                   CloseDate,
                                   StopDate,
                                   LastUpdateTime
                            FROM   [Accounting].[CloseBillDate] a WITH ( NOLOCK )";
            return Query<AccountingCloseBillDayRedisEntity>(sql, null, "PostLoanDB", CommandType.Text, 60000);
        }

        /// <summary>
        /// 获取门店权限
        /// </summary>
        /// <returns></returns>
        public List<PermissionRedisDataEntity> GetStorePermissionRedisListData()
        {
            string sql = @"SELECT DISTINCT
                            Id ,
                            UserId ,
                            [Key] ,
                            Name
                    FROM    ( SELECT    s.serialId AS Id ,
                                        a.UserId ,
                                        s.FullKey AS [Key] ,
                                        s.storeName AS Name
                              FROM      [user].[AccountOrgRelation] a WITH ( NOLOCK )
                                        JOIN [org].[StoreV] s WITH ( NOLOCK ) ON a.orgId = s.serialId
                              WHERE     orgLevel = 3
                              UNION ALL
                              SELECT    
                                        s.serialId AS Id,
                                        r.UserId,
                                        s.FullKey AS [Key] ,
                                        s.storeName AS Name
                              FROM      ( SELECT    r.serialId AS Id ,
                                                            a.UserId ,
                                                            r.FullKey AS [Key] ,
                                                            r.cityName AS Name
                                                  FROM      [user].[AccountOrgRelation] a WITH ( NOLOCK )
                                                            JOIN [org].[RegionV] r WITH ( NOLOCK ) ON a.orgId = r.serialId
                                                  WHERE     orgLevel = 2
                                                  UNION ALL
                                                  SELECT    r.serialId AS Id ,
                                                            a.UserId ,
                                                            r.FullKey AS [Key] ,
                                                            r.cityName AS Name
                                                  FROM      [user].[AccountOrgRelation] a WITH ( NOLOCK )
                                                            JOIN [org].[DivisionV] d WITH ( NOLOCK ) ON a.orgId = d.serialId
                                                            JOIN [org].[RegionV] r WITH ( NOLOCK ) ON d.serialId = r.parentId
                                                  WHERE     orgLevel = 1
                                              ) r
                                        JOIN [org].[StoreV] s WITH ( NOLOCK ) ON r.Id = s.parentId
                            ) r";
            return Query<PermissionRedisDataEntity>(sql, null, "SysDB", CommandType.Text, 60000);
        }
    }
}

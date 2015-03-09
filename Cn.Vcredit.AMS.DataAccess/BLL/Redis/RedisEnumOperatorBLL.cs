using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Data.DB.RedisData;
using Cn.Vcredit.AMS.DataAccess.DAL.Redis;
using Cn.Vcredit.AMS.DataAccess.Redis;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.BLL.Redis
{
    /// <summary>
    /// Redis枚举类型逻辑操作类
    /// </summary>
    public class RedisEnumOperatorBLL
    {
        #region HashId定义
        /// <summary>
        /// 存放枚举的HashID
        /// </summary>
        [Description("存放枚举的HashID")]
        public const string HashId_Enum_1 = "HashId_Enum_1";
        /// <summary>
        /// 存放用户的HashID
        /// </summary>
        [Description("存放用户的HashID")]
        public const string HashId_User_2 = "HashId_User_2";
        /// <summary>
        /// 存放服务方的HashID
        /// </summary>
        [Description("存放服务方的HashID")]
        public const string HashId_ServiceGroup_3 = "HashId_ServiceGroup_3";
        /// <summary>
        /// 存放放贷方的HashID
        /// </summary>
        [Description("存放放贷方的HashID")]
        public const string HashId_LendingGroup_4 = "HashId_LendingGroup_4";
        /// <summary>
        /// 存放担保方的HashID
        /// </summary>
        [Description("存放担保方的HashID")]
        public const string HashId_GuaranteeGroup_5 = "HashId_GuaranteeGroup_5";
        /// <summary>
        /// 存放订单类型的HashID
        /// </summary>
        [Description("存放订单类型的HashID")]
        public const string HashId_LoanKind_6 = "HashId_LoanKind_6";
        /// <summary>
        /// 存放贷款产品类型的HashID
        /// </summary>
        [Description("存放贷款产品类型的HashID")]
        public const string HashId_ProductKind_7 = "HashId_ProductKind_7";
        /// <summary>
        /// 存放银行列表的HashID
        /// </summary>
        [Description("存放银行列表的HashID")]
        public const string HashId_BankList_8 = "HashId_BankList_8";
        /// <summary>
        /// 存放地区的HashID
        /// </summary>
        [Description("存放地区的HashID")]
        public const string HashId_Region_9 = "HashId_Region_9";
        /// <summary>
        /// 存放门店的HashID
        /// </summary>
        [Description("存放门店的HashID")]
        public const string HashId_Store_12 = "HashId_Store_12";
        /// <summary>
        /// 存放订单状态的HashID
        /// </summary>
        [Description("存放订单状态的HashID")]
        public const string HashId_BusinessStatus_10 = "HashId_BusinessStatus_10";
        /// <summary>
        /// 存放清贷状态的HashID
        /// </summary>
        [Description("存放清贷状态的HashID")]
        public const string HashId_CLoanStatus_11 = "HashId_CLoanStatus_11";
        /// <summary>
        /// 存放销售模式的HashID
        /// </summary>
        [Description("存放销售模式的HashID")]
        public const string HashId_SaleMode_13 = "HashId_SaleMode_13";
        /// <summary>
        /// 存放工商注册类型的HashID
        /// </summary>
        [Description("存放工商注册类型的HashID")]
        public const string HashId_Entregist_14 = "HashId_Entregist_14";
        /// <summary>
        /// 存放18位合同号适用地区的HashID
        /// </summary>
        [Description("存放18位合同号适用地区的HashID")]
        public const string HashId_18Region_15 = "HashId_18Region_15";
        /// <summary>
        /// 存放分公司的HashID
        /// </summary>
        [Description("存放分公司的HashID")]
        public const string HashId_SubCompany_16 = "HashId_SubCompany_16";
        #endregion

        #region 常量
        public const string Pre_User = "User_";
        public const string Pre_User_Menu = "User_Menu_";
        public const string Pre_User_Division = "User_Division_";
        public const string Pre_User_Region = "User_Region_";
        public const string Pre_User_Store = "User_Store_";
        public const string Pre_User_Company = "User_Company_";
        #endregion

        #region Redis共通处理
        /// <summary>
        /// 清空Redis数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public bool ClearRedisData(string hashId)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                foreach (var key in redisClient.GetHashKeys(hashId))
                    redisClient.RemoveEntryFromHash(hashId, key);
            }
            return true;
        }

        /// <summary>
        /// 获取Redis数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public RedisDataEntity GetRedisData(string hashId, string key)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                RedisDataEntity data = new RedisDataEntity();
                data.Key = key;
                data.Name = redisClient.GetValueFromHash(hashId, key);
                return data;
            }
        }

        /// <summary>
        /// 获取所有的HashKeys
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public List<string> GetHashKeys(string hashId)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var items = redisClient.GetAllEntriesFromHash(hashId);
                if (items == null || items.Count == 0)
                    return new List<string>();

                return items.Keys.ToList();
            }
        }

        /// <summary>
        /// 删除Redis数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteRedisData(string hashId, string key)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                redisClient.RemoveEntryFromHash(hashId, key);
            }
            return true;
        }

        /// <summary>
        /// 更新Redis数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateRedisData(string hashId, RedisDataEntity data)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                redisClient.SetEntryInHash(hashId, data.Key, data.Name);
            }
            return true;
        }

        /// <summary>
        /// 插入Redis数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool InsertRedisData(string hashId, RedisDataEntity data)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                redisClient.SetEntryInHashIfNotExists(hashId, data.Key, data.Name);
            }
            return true;
        }

        /// <summary>
        /// 插入Redis数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool InsertRedisData(string hashId, string key, string name)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                redisClient.SetEntryInHashIfNotExists(hashId, key, name);
            }
            return true;
        }

        /// <summary>
        /// 批量插入Redis数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="lstRedisEntities"></param>
        /// <returns></returns>
        public bool BatchInsertRedisData(string hashId, List<RedisDataEntity> lstRedisEntities)
        {
            if (lstRedisEntities == null || lstRedisEntities.Count == 0)
                return false;

            using (var redisClient = RedisManager.GetClient())
            {
                foreach (var data in lstRedisEntities)
                {
                    redisClient.SetEntryInHashIfNotExists(hashId, data.Key, data.Name);
                }
            }
            return true;
        }

        /// <summary>
        /// 批量插入Redis数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="lstRedisEntities"></param>
        /// <returns></returns>
        public bool BatchInsertRedisData(string hashId, List<EnumRedisDataEntity> lstRedisEntities)
        {
            if (lstRedisEntities == null || lstRedisEntities.Count == 0)
                return false;

            using (var redisClient = RedisManager.GetClient())
            {
                foreach (var data in lstRedisEntities)
                {
                    redisClient.SetEntryInHashIfNotExists(hashId, data.Key, data.Name);
                }
            }
            return true;
        }

        /// <summary>
        /// 清空Redis数据
        /// </summary>
        /// <returns></returns>
        public bool ClearRedisData<T>()
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var dropListData = redisClient.GetTypedClient<T>();
                dropListData.DeleteAll();
                dropListData.Save();
            }

            return true;
        }

        /// <summary>
        /// 清空下拉框枚举Redis数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullKey"></param>
        /// <returns></returns>
        public bool ClearEnumDropDownListRedisData(string fullKey)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var dropListData = redisClient.GetTypedClient<EnumRedisDataEntity>();
                var EnumRedisDataEntity = dropListData.GetAll();
                for(int i = EnumRedisDataEntity.Count - 1; i >= 0; i--)
                {
                    if (EnumRedisDataEntity[i].SuperFullKey == fullKey)
                        EnumRedisDataEntity.RemoveAt(i);
                }

                dropListData.DeleteAll();
                dropListData.StoreAll(EnumRedisDataEntity);
            }
            return true;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="lstRedisEntities"></param>
        /// <returns></returns>x
        public bool BatchInsertRedisData<T>(List<T> lstRedisEntities)
        {
            if (lstRedisEntities == null || lstRedisEntities.Count == 0)
                return false;

            using (var redisClient = RedisManager.GetClient())
            {
                var dropListData = redisClient.GetTypedClient<T>();
                dropListData.StoreAll(lstRedisEntities);
                return true;
            }
        }

        /// <summary>
        /// 获取所有的下拉框列表数据
        /// </summary>
        /// <returns></returns>
        public List<T> GetEnumRedisDataEntitys<T>()
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var dropListData = redisClient.GetTypedClient<T>();
                return dropListData.GetAll().ToList();
            }
        }

        #endregion

        #region 获取各种权限信息
        /// <summary>
        /// 获取用户所拥有的公司键值
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<string> GetUserOwnCompanyKeys(int userId)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            var lstEnumRedisDataEntity = GetEnumRedisDataEntitys<EnumRedisDataEntity>();

            if (lstEnumRedisDataEntity == null || lstEnumRedisDataEntity.Count == 0)
                return new List<string>();

            var lstCompany = lstEnumRedisDataEntity.Where(x => x.SuperFullKey == SysConst.COMPANY).ToList();
            if (lstCompany == null || lstCompany.Count == 0)
                return new List<string>();

            string hashId = string.Format("{0}{1}", Pre_User_Region, userId);
            List<string> regionKeys = new List<string>();
            GetHashKeys(hashId).ForEach(p => { regionKeys.Add(p.Replace("SUBSIDIARY", "REGION").ToUpper()); });

            List<string> lstCompanyKeys = new List<string>();
            lstCompanyKeys = lstCompany.Where(p => regionKeys
                .Contains(p.Region.ToUpper())).Select(p => p.Key).ToList();
            sw.Stop();

            return lstCompanyKeys;
        }

        /// <summary>
        /// 获取用户拥有的所有分部的Key
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<string> GetUserOwnDivisionKeys(int userId)
        {
            string hashId = string.Format("{0}{1}", Pre_User_Division, userId);
            return GetHashKeys(hashId);
        }

        /// <summary>
        /// 获取用户拥有的所有地区Key
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<string> GetUserOwnRegionKeys(int userId)
        {
            string hashId = string.Format("{0}{1}", Pre_User_Region, userId);
            return GetHashKeys(hashId);
        }

        /// <summary>
        /// 获取用户拥有的所有门店Key
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<string> GetUserOwnStoreKeys(int userId)
        {
            string hashId = string.Format("{0}{1}", Pre_User_Store, userId);
            return GetHashKeys(hashId);
        }
        
        #endregion

        /// <summary>
        /// 全量同步页面下拉框数据
        /// </summary>
        /// <returns></returns>
        public bool FullSyncDropDownListData()
        {
            try
            {
                // 清空数据
                ClearRedisData<EnumRedisDataEntity>();
                ClearRedisData<RegionRedisDataEntity>();
                ClearRedisData<StoreRedisDataEntity>();
                ClearRedisData<TeamRedisDataEntity>();
                ClearRedisData<RegionStoreRedisEntity>();

                // 插入下拉框枚举值
                List<string> lstFullKeys = new List<string>();
                lstFullKeys.Add(SysConst.COMPANY);
                lstFullKeys.Add(SysConst.LOANKIND);
                lstFullKeys.Add(SysConst.PRODUCTKIND);
                lstFullKeys.Add(Const.BANKLIST);
                lstFullKeys.Add(SysConst.BUSINESSSTATUS);
                lstFullKeys.Add(SysConst.CLOANSTATUS);
                lstFullKeys.Add(SysConst.BILLSTATUS);
                lstFullKeys.Add(SysConst.LAWSUITSTATUS);
                lstFullKeys.Add(SysConst.ENTREGIST);
                lstFullKeys.Add(SysConst.CODE_FITAREA);
                lstFullKeys.Add(SysConst.SUBCOMPANY);

                string typeFullKeys = string.Join(WebServiceConst.Separater_Comma_Quote, lstFullKeys.ToArray());
                BatchInsertRedisData<EnumRedisDataEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumListData(typeFullKeys));

                // 加载地区信息
                BatchInsertRedisData<RegionRedisDataEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetRegionEnumListData());
                // 加载门店信息
                BatchInsertRedisData<StoreRedisDataEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetStoreEnumListData());

                // 加载团队信息
                BatchInsertRedisData<TeamRedisDataEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetTeamRedisListData());
                
                // 加载门店地区关联信息
                BatchInsertRedisData<RegionStoreRedisEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetRegionStoreRedisListData());

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 同步用户权限数据
        /// </summary>
        /// <returns></returns>
        public bool FullSyncPermission()
        {
            // 清除用户权限
            ClearUserPerssion();

            // 同步菜单权限类
            FullSyncMenuPermission();

            // 同步用户地区权限
            FullSyncRegionPermission();

            // 同步用户门店权限
             FullSynsStroePermission();

            //  同步分部权限数据
            FullSynsDivisionPermission();

            return true;
        }

        /// <summary>
        /// 清除用户权限
        /// </summary>
        /// <returns></returns>
        public bool ClearUserPerssion()
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var keys = redisClient.GetAllKeys();

                if (keys == null || keys.Count == 0)
                    return false;

                var lstRemoveKeys = new List<string>();
                foreach (var key in keys)
                {
                    if (key.StartsWith(Pre_User))
                        lstRemoveKeys.Add(key);
                }

                redisClient.RemoveAll(lstRemoveKeys);
            }

            return true;
        }

        /// <summary>
        /// 清除用户权限
        /// </summary>
        /// <returns></returns>
        public bool ClearUserPerssion(string prePerssion)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var keys = redisClient.GetAllKeys();
                if (keys == null || keys.Count == 0)
                    return false;

                var lstRemoveKeys = new List<string>();
                foreach (var key in keys)
                {
                    if (key.StartsWith(prePerssion))
                        lstRemoveKeys.Add(key);
                }

                redisClient.RemoveAll(lstRemoveKeys);
            }

            return true;
        }

        /// <summary>
        /// 插入权限数据
        /// </summary>
        /// <param name="lstPermissionData"></param>
        /// <param name="preHashId"></param>
        private void InsertPermission(List<PermissionRedisDataEntity> lstPermissionData, string preHashId)
        {
            var dicPermission = lstPermissionData.ToLookup(x => x.UserId);

            string hashId = "";
            foreach (var userRecord in dicPermission)
            {
                hashId = string.Format("{0}{1}", preHashId, userRecord.Key);
                var values = lstPermissionData.FindAll(x => x.UserId == userRecord.Key);
                if (values == null || values.Count == 0)
                    continue;

                foreach (var value in values)
                {
                    InsertRedisData(hashId, value.Key, value.Name);
                }
            }
        }

        /// <summary>
        /// 同步用户地区权限
        /// </summary>
        /// <returns></returns>
        public bool FullSyncRegionPermission()
        {
            // 查询权限数据
            var lstPermissionData = Singleton<RedisEnumOperatorDAL>.Instance.GetRegionPermissionRedisListData();
            InsertPermission(lstPermissionData, Pre_User_Region);

            return true;
        }

        /// <summary>
        /// 同步用户分部权限
        /// </summary>
        /// <returns></returns>
        public bool FullSynsDivisionPermission()
        {
            // 查询权限数据
            var lstPermissionData = Singleton<RedisEnumOperatorDAL>.Instance.GetDivisionPermissionRedisListData();
            InsertPermission(lstPermissionData, Pre_User_Division);

            return true;
        }

        /// <summary>
        /// 同步用户门店权限
        /// </summary>
        /// <returns></returns>
        public bool FullSynsStroePermission()
        {
            // 查询权限数据
            var lstPermissionData = Singleton<RedisEnumOperatorDAL>.Instance.GetStorePermissionRedisListData();
            InsertPermission(lstPermissionData, Pre_User_Store);

            return true;
        }

        /// <summary>
        /// 全量同步菜单权限
        /// </summary>
        /// <returns></returns>
        public bool FullSyncMenuPermission()
        {
            // 查询权限数据
            var lstPermissionData = Singleton<RedisEnumOperatorDAL>.Instance.GetPermissionRedisListData();
            InsertPermission(lstPermissionData, Pre_User_Menu);

            return true;
        }

        /// <summary>
        /// 全量加载公司权限
        /// </summary>
        /// <returns></returns>
        public bool FullSyncCompanyPerssion()
        {
            return true;
        }

        /// <summary>
        /// 全量同步Key_Value
        /// </summary>
        /// <returns></returns>
        public bool FullSyncKeyValue()
        {
            // 清空Redis缓存
            ClearRedisData(HashId_Enum_1);
            ClearRedisData(HashId_User_2);
            ClearRedisData(HashId_ServiceGroup_3);
            ClearRedisData(HashId_LendingGroup_4);
            ClearRedisData(HashId_GuaranteeGroup_5);
            ClearRedisData(HashId_LoanKind_6);
            ClearRedisData(HashId_ProductKind_7);
            ClearRedisData(HashId_BankList_8);
            ClearRedisData(HashId_Region_9);
            ClearRedisData(HashId_BusinessStatus_10);
            ClearRedisData(HashId_CLoanStatus_11);
            ClearRedisData(HashId_Store_12);
            ClearRedisData(HashId_SaleMode_13);
            ClearRedisData(HashId_Entregist_14);
            ClearRedisData(HashId_18Region_15);
            ClearRedisData(HashId_SubCompany_16);

            // 查询公司数据
            var lstCompanyData = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumData(Const.COMPANY);
            if (lstCompanyData != null
                && lstCompanyData.Count > 0)
            {
                // 服务方
                var lstServiceData = lstCompanyData.Where(e => e.Value.Contains("IsService=1")).OrderBy(e => e.Name).ToList();
                BatchInsertRedisData(HashId_ServiceGroup_3, lstServiceData);
                // 放贷方
                var lstLendingData = lstCompanyData.Where(e => e.Value.Contains("IsLending=1")).OrderBy(e => e.Name).ToList();
                BatchInsertRedisData(HashId_LendingGroup_4, lstLendingData);
                // 担保方
                var lstGuaranteeData = lstCompanyData.Where(e => e.Value.Contains("IsGuarantee=1")).OrderBy(e => e.Name).ToList();
                BatchInsertRedisData(HashId_GuaranteeGroup_5, lstGuaranteeData);
            }

            // 查询订单类型
            var lstLoanKindata = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumData(Const.LOANKIND);
            BatchInsertRedisData(HashId_LoanKind_6, lstLoanKindata);
            // 查询贷款产品类型
            var lstProductKindData = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumData(Const.PRODUCTKIND);
            BatchInsertRedisData(HashId_ProductKind_7, lstProductKindData);
            // 查询银行数据
            var lstBankData = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumData(Const.BANKLIST);
            BatchInsertRedisData(HashId_BankList_8, lstBankData);
            // 查询销售模式数据
            var lstSaleModeData = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumData(Const.SALEMODE);
            BatchInsertRedisData(HashId_SaleMode_13, lstSaleModeData);
            // 查询订单状态
            var lstBusinessData = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumDataForValue(Const.BUSINESSSTATUS);
            BatchInsertRedisData(HashId_BusinessStatus_10, lstBusinessData);
            // 查询清贷状态
            var lstCloanStatusData = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumDataForValue(Const.CLOANSTATUS);
            BatchInsertRedisData(HashId_CLoanStatus_11, lstCloanStatusData);

            // 查询地区数据
            var lstRegionData = Singleton<RedisEnumOperatorDAL>.Instance.GetRegionEnumData();
            BatchInsertRedisData(HashId_Region_9, lstRegionData);
            // 查询门店数据
            var lstStoreData = Singleton<RedisEnumOperatorDAL>.Instance.GetStoreEnumData();
            BatchInsertRedisData(HashId_Store_12, lstStoreData);

            // 工商注册类型
            var lstEntregistData = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumData(Const.ENTREGIST);
            BatchInsertRedisData(HashId_Entregist_14, lstEntregistData);

            // 18位合同号适用地区
            var lstCodeFitareaData = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumData(Const.CODE_FITAREA);
            BatchInsertRedisData(HashId_18Region_15, lstCodeFitareaData);
            // 分公司
            var lstSubCompanyData = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumData(Const.SUBCOMPANY);
            BatchInsertRedisData(HashId_SubCompany_16, lstSubCompanyData);

            // 查询用户数据
            var lstUserData = Singleton<RedisEnumOperatorDAL>.Instance.GetUserData();
            if (lstUserData != null && lstUserData.Count > 0)
                BatchInsertRedisData(HashId_User_2, lstUserData);

            return true;
        }

        /// <summary>
        /// 同步基础数据
        /// </summary>
        /// <returns></returns>
        public bool FullSyncBaseInfo()
        {
            try
            {
                // 清空数据
                ClearRedisData<BankAccountRedisEntity>();
                ClearRedisData<CloseBillDayRedisEntity>();
                ClearRedisData<AccountingCloseBillDayRedisEntity>();

                // 同步银行账户信息
                BatchInsertRedisData<BankAccountRedisEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetBankAccountRedisEntity());
                // 同步关帐日信息
                BatchInsertRedisData<CloseBillDayRedisEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetCloseBillDayRedisEntity());
                // 同步账户关帐日信息
                BatchInsertRedisData<AccountingCloseBillDayRedisEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetAccountingCloseBillDayRedisEntity());

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 全量同步枚举类
        /// </summary>
        /// <returns></returns>
        public bool FullSyncRedisEnumData()
        {
            // 全量同步Key_Value
            FullSyncKeyValue();

            // 全量同步页面下拉框数据
            FullSyncDropDownListData();

            // 同步用户权限数据
            FullSyncPermission();

            // 同步基础数据
            FullSyncBaseInfo();

            //List<string> keys = GetUserOwnCompanyKeys(20200);

            return true;
        }

        #region Key_Value
        /// <summary>
        /// 同步用户数据
        /// </summary>
        /// <returns></returns>
        public void SyncUserData()
        {
            // 清空Redis缓存
            ClearRedisData(HashId_User_2);

            // 查询用户数据
            var lstUserData = Singleton<RedisEnumOperatorDAL>.Instance.GetUserData();
            if (lstUserData != null && lstUserData.Count > 0)
                BatchInsertRedisData(HashId_User_2, lstUserData);
        }

        /// <summary>
        /// 同步服务方数据
        /// </summary>
        /// <returns></returns>
        public void SyncServiceGroup()
        {
            ClearRedisData(HashId_ServiceGroup_3);

            // 查询公司数据
            var lstCompanyData = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumData(Const.COMPANY);
            if (lstCompanyData != null
                && lstCompanyData.Count > 0)
            {
                // 服务方
                var lstServiceData = lstCompanyData.Where(e => e.Value.Contains("IsService=1")).OrderBy(e => e.Name).ToList();
                BatchInsertRedisData(HashId_ServiceGroup_3, lstServiceData);
            }
        }

        /// <summary>
        /// 同步放贷方数据
        /// </summary>
        /// <returns></returns>
        public void SyncLendingGroup()
        {
            ClearRedisData(HashId_LendingGroup_4);

            // 查询公司数据
            var lstCompanyData = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumData(Const.COMPANY);
            if (lstCompanyData != null
                && lstCompanyData.Count > 0)
            {
                // 放贷方
                var lstLendingData = lstCompanyData.Where(e => e.Value.Contains("IsLending=1")).OrderBy(e => e.Name).ToList();
                BatchInsertRedisData(HashId_LendingGroup_4, lstLendingData);
            }
        }

        /// <summary>
        /// 同步担保方数据
        /// </summary>
        /// <returns></returns>
        public void SyncGuaranteeGroup()
        {
            // 清空数据
            ClearRedisData(HashId_GuaranteeGroup_5);

            // 查询公司数据
            var lstCompanyData = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumData(Const.COMPANY);
            if (lstCompanyData != null
                && lstCompanyData.Count > 0)
            {
                // 担保方
                var lstGuaranteeData = lstCompanyData.Where(e => e.Value.Contains("IsGuarantee=1")).OrderBy(e => e.Name).ToList();
                BatchInsertRedisData(HashId_GuaranteeGroup_5, lstGuaranteeData);
            }
        }

        /// <summary>
        /// 同步特定类型的数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="typeFullKey"></param>
        public void SyncTypeEnumData(string hashId, string typeFullKey)
        {
            // 清空数据
            ClearRedisData(hashId);

            // 查询数据
            var lstEnumdata = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumData(typeFullKey);
            BatchInsertRedisData(hashId, lstEnumdata);
        }

        /// <summary>
        /// 同步特定类型的数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="typeFullKey"></param>
        public void SyncTypeEnumDataForValue(string hashId, string typeFullKey)
        {
            // 清空数据
            ClearRedisData(hashId);

            // 查询数据
            var lstEnumdata = Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumDataForValue(typeFullKey);
            BatchInsertRedisData(hashId, lstEnumdata);
        }

        /// <summary>
        /// 同步地区权限
        /// </summary>
        public void SyncRegionEnumData()
        {
            // 清空数据
            ClearRedisData(HashId_Region_9);

            // 查询地区数据
            var lstRegionData = Singleton<RedisEnumOperatorDAL>.Instance.GetRegionEnumData();
            BatchInsertRedisData(HashId_Region_9, lstRegionData);
        }

        /// <summary>
        /// 同步门店权限
        /// </summary>
        public void SyncStoreEnumData()
        {
            // 清空数据
            ClearRedisData(HashId_Store_12);

            // 查询门店数据
            var lstStoreData = Singleton<RedisEnumOperatorDAL>.Instance.GetStoreEnumData();
            BatchInsertRedisData(HashId_Store_12, lstStoreData);
        }
        #endregion

        #region 下拉框
        /// <summary>
        /// 同步下拉框枚举值数据
        /// </summary>
        /// <param name="typeFullKeys"></param>
        public void SyncDropListEnumData(string typeFullKeys)
        {
            // 清空数据
            ClearEnumDropDownListRedisData(typeFullKeys);            
            // 查询数据
            BatchInsertRedisData<EnumRedisDataEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetTypeEnumListData(typeFullKeys));
        }

        /// <summary>
        /// 同步下拉框地区数据
        /// </summary>
        public void SyncRegionData()
        {
            // 清空数据
            ClearRedisData<RegionRedisDataEntity>();
            // 加载地区信息
            BatchInsertRedisData<RegionRedisDataEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetRegionEnumListData());
        }

        /// <summary>
        /// 同步下拉框门店数据
        /// </summary>
        public void SyncStoreData()
        {
            // 清空数据
            ClearRedisData<StoreRedisDataEntity>();
            // 加载门店信息
            BatchInsertRedisData<StoreRedisDataEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetStoreEnumListData());
        }

        /// <summary>
        /// 同步下拉框团队数据
        /// </summary>
        public void SyncTeamData()
        {
            // 清空数据
            ClearRedisData<TeamRedisDataEntity>();
            // 加载团队信息
            BatchInsertRedisData<TeamRedisDataEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetTeamRedisListData());
        }

        /// <summary>
        /// 同步下拉框团队数据
        /// </summary>
        public void SyncRegionStoreData()
        {
            // 清空数据
            ClearRedisData<RegionStoreRedisEntity>();
            // 加载门店地区关联信息
            BatchInsertRedisData<RegionStoreRedisEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetRegionStoreRedisListData());
        }

        #endregion

        #region 权限

        /// <summary>
        /// 同步用户菜单权限
        /// </summary>
        public void SyncMenuPermission()
        {
            // 清空缓存
            ClearUserPerssion(Pre_User_Menu);
            // 同步菜单权限类
            FullSyncMenuPermission();
        }

        /// <summary>
        /// 同步地区权限数据
        /// </summary>
        public void SyncRegionPermission()
        {
            // 清空缓存
            ClearUserPerssion(Pre_User_Region);
            // 同步用户地区权限
            FullSyncRegionPermission();
        }

        /// <summary>
        /// 同步门店权限
        /// </summary>
        public void SyncStroePermission()
        {
            // 清空缓存
            ClearUserPerssion(Pre_User_Store);
            // 同步用户门店权限
            FullSynsStroePermission();
        }

        /// <summary>
        /// 同步分部权限数据
        /// </summary>
        public void SynsDivisionPermission()
        {
            // 清空缓存
            ClearUserPerssion(Pre_User_Division);
            //  同步分部权限数据
            FullSynsDivisionPermission();
        }

        #endregion

        #region 基础数据

        /// <summary>
        /// 同步银行账户信息
        /// </summary>
        public void SyncBankAccount()
        {
            // 清空数据
            ClearRedisData<BankAccountRedisEntity>();
            // 同步银行账户信息
            BatchInsertRedisData<BankAccountRedisEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetBankAccountRedisEntity());
        }

        /// <summary>
        /// 同步关帐日信息
        /// </summary>
        public void SyncCloseBillDay()
        {
            // 清空数据
            ClearRedisData<CloseBillDayRedisEntity>();
            // 同步关帐日信息
            BatchInsertRedisData<CloseBillDayRedisEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetCloseBillDayRedisEntity());
        }

        /// <summary>
        /// 同步账户关帐日信息
        /// </summary>
        public void SyncAccountingCloseBillDay()
        {
            // 清空数据
            ClearRedisData<AccountingCloseBillDayRedisEntity>();
            // 同步关帐日信息
            BatchInsertRedisData<AccountingCloseBillDayRedisEntity>(Singleton<RedisEnumOperatorDAL>.Instance.GetAccountingCloseBillDayRedisEntity());
        }

        #endregion
    }
}

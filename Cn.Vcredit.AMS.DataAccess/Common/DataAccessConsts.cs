using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.Common
{
    /// <summary>
    /// 常量定义
    /// </summary>
    public class DataAccessConsts
    {
        public const string OrderbyStr = "OrderbyStr";
        public const string PageNo = "PageNo";
        public const string PageSize = "PageSize";
        public const string RecordCount = "RecordCount";
        public const int OneBatchCount = 50000;

        public static string[] BaseFilterProperty = new string[] { 
            DataAccessConsts.OrderbyStr,
            DataAccessConsts.PageNo,
            DataAccessConsts.PageSize,
            DataAccessConsts.RecordCount};
        #region 表名定义
        public const string MongoTable_AccountOrgRelation = "Mongo.Sys_AccountOrgRelation";
        public const string MongoTable_UserPermission = "Mongo.Sys_UserPermission";
        public const string MongoTable_DivisionV = "Mongo.Sys_DivisionV";
        public const string MongoTable_RegionV = "Mongo.Sys_RegionV";
        public const string MongoTable_EnumerationAll = "Mongo.Sys_EnumerationAll";
        #endregion

        public const string ConnectDB_PostLoan = "PostLoanDB";

    }
}

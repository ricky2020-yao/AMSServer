using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.SyncFromSql
{
    public class SyncConsts
    {
        /// <summary>
        /// 同步类型到MonogDB
        /// </summary>
        public const int SyncType_MonogDB = 1;

        /// <summary>
        /// 数据库脏读
        /// </summary>
        public const string ReadUncommittedSet = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ";

        /// <summary>
        /// 日期格式化
        /// </summary>
        public const string DatetimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 点
        /// </summary>
        public const char Point_Char = '.';

        /// <summary>
        /// 超时120秒
        /// </summary>
        public const int ExecuteTimeOut_120 = 120;
    }
}

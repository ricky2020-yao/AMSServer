using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月18日
    /// Description:更新数据数据访问基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class BaseUpdateDAL: BaseDao
    {
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        protected virtual string GetConnectKey()
        {
            return "PostLoanDB";
        }

        /// <summary>
        /// 获取指定如何解释命令字符串
        /// </summary>
        /// <returns></returns>
        protected virtual CommandType GetCommandType()
        {
            return CommandType.Text;
        }

        /// <summary>
        /// 获取更新的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected virtual string GetUpdateSql(BaseFilter baseFilter)
        {
            return "";
        }

        /// <summary>
        /// 获取检索数据的存储过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected virtual string GetUpdateSpName(BaseFilter baseFilter)
        {
            return "";
        }

        /// <summary>
        /// 获取更新数据的存储过程参数列表
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected virtual IDictionary<string, object> GetUpdateSpInParams(BaseFilter baseFilter)
        {
            return null;
        }

        /// <summary>
        /// 获取更新数据的件数
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        public virtual int Update(BaseFilter baseFilter)
        {
            string sql = "";
            IDictionary<string, object> dicParams = null;
            CommandType type = GetCommandType();

            if (type == CommandType.StoredProcedure)
            {
                sql = GetUpdateSpName(baseFilter);
                dicParams = GetUpdateSpInParams(baseFilter);
            }
            else
            {
                sql = GetUpdateSql(baseFilter);
            }
            if (string.IsNullOrEmpty(sql))
                return 0;

            // 获取更新的件数
            return Execute(sql, dicParams, GetConnectKey(), type);
        }
    }
}

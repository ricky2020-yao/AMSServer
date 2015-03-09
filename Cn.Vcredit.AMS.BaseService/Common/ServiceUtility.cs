using Cn.Vcredit.AMS.Entity.Filter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cn.Vcredit.Common.DataTableExtensions;
using Cn.Vcredit.Data;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Tools;
using Cn.Vcredit.Common.Log;
using ExportLogHandler = Cn.Vcredit.AMS.Common.Define.DelegataDefine.ExportLogHandler;
using System.ComponentModel;

namespace Cn.Vcredit.AMS.BaseService.Common
{
    /// <summary>
    /// Author:王书行
    /// CreateTime:2014年8月13日
    /// Description:服务共通处理类
    /// </summary>
    public class ServiceUtility
    {
        /// <summary>
        /// 过滤条件字典返回类
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="dict">字典</param>
        /// <returns>类实例</returns>
        public static T ConvertToFilterFromDict<T>(IDictionary<string, string> dict) where T : new()
        {
            if (dict == null)
                return default(T);

            Type t = typeof(T);
            PropertyInfo[] propertyInfoArray = t.GetProperties();
            T newInstance = new T();
            //object defaultValue;
            foreach (PropertyInfo info in propertyInfoArray)
            {
                string propertyName = info.Name;
                if (!dict.ContainsKey(propertyName))
                    continue;

                string val = dict[propertyName];
                if (info.PropertyType.IsGenericType
                    && info.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    info.SetValue(newInstance, string.IsNullOrEmpty(val)
                        ? null : Convert.ChangeType(val, Nullable.GetUnderlyingType(info.PropertyType)), null);
                }
                else
                {
                    info.SetValue(newInstance,
                        val == null
                        ? null : Convert.ChangeType(val, info.PropertyType), null);
                }

                //defaultValue = DefaultForType(info.GetType());
                //info.SetValue(newInstance,
                //    val == null
                //    ? null : Convert.ChangeType(val, info.PropertyType), null);

                //info.SetValue(newInstance, val, null);
            }
            
            return newInstance;
        }

        /// <summary>
        /// 获取某个类型的默认值
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private static object DefaultForType(Type targetType)
        {
            if (targetType.Name.Contains(""))
                return "";

            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }

        /// <summary>
        /// 查询出分页数据
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="filter">过滤条件，得到结果后会设置RecordCount值</param>
        /// <param name="sqlStr">查询语句，要求包含2个sql，第一个为分页数据，第二个为总数</param>
        /// <returns>分页数据</returns>
        public static List<T> GetSearchDataByPageNo<T>(BaseFilter filter,string sqlStr) where T:new()
        {
            BaseDao baseDao = new BaseDao();
            DataSet dataSet = baseDao.QuerySet(sqlStr, null, "PostLoanDB");
            if (dataSet == null || dataSet.Tables.Count <= 1)
                return null;

            DataTable dtbData = dataSet.Tables[0];
            filter.RecordCount = Convert.ToInt32(dataSet.Tables[1].Rows[0][0]);
            return dtbData.ConvertToList<T>();
        }

        /// <summary>
        /// 设置返回状态
        /// </summary>
        /// <param name="responseEntity"></param>
        /// <param name="state"></param>
        /// <param name="responseMessage"></param>
        public static void SetResponseStatus(ResponseEntity responseEntity
            , EnumResponseState state, string responseMessage = "")
        {
            if (responseEntity == null) return;

            responseEntity.ResponseStatus = (int)state;
            if (string.IsNullOrEmpty(responseMessage))
                responseEntity.ResponseMessage = state.ValueToDesc<EnumResponseState>();
            else
                responseEntity.ResponseMessage = responseMessage;
        }

        /// <summary>
        /// 日志输出
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logHandler"></param>
        /// <param name="logMessage"></param>
        public static void ExportLog(ILogger log, ExportLogHandler logHandler, string logMessage)
        {
            if (log != null)
                log.Debug(logMessage);

            //if (logHandler != null)
            //    logHandler(logMessage);
        }

        /// <summary>
        /// 扩展方法 获取任意对象的Description属性
        /// </summary>
        public static string ToDescription(Type type)
        {
            object[] da = type.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (da == null || da.Length == 0)
                return string.Empty;

            DescriptionAttribute descriptionAttribute = da[0] as DescriptionAttribute;
            if (descriptionAttribute != null)
                return descriptionAttribute.Description;

            return string.Empty;
        }
    }
}

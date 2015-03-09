using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.Common.Tools
{
    /// <summary>
    /// DataTable 的扩展方法
    /// 2013年12月6日
    /// 曹贝
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// 获取DataRow的列的值
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static T GetValue<T>(this DataRow dr, string columnName)
        {
            T returnVal = default(T);
            try
            {
                if (dr.Table.Columns.Contains(columnName))
                {
                    object value = dr[columnName];

                    if (GetValue(value, ref returnVal))
                        return returnVal;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Get value of {0} failed,Exception Information is 【{1}】", columnName, ex.Message));
            }

            return returnVal;
        }

        private static bool GetValue<T>(object value, ref T outValue)
        {
            if (null == value)
            {
                return false;
            }

            if (typeof(T) == typeof(string))
            {
                value = value.ToString();
                outValue = (T)value;
            }
            else if (typeof(T) == typeof(Int64) || typeof(T) == typeof(long) ||
                     typeof(T) == typeof(int) || typeof(T) == typeof(Int32) ||
                     typeof(T) == typeof(Int16) || typeof(T) == typeof(double) ||
                     typeof(T) == typeof(decimal) || typeof(T) == typeof(DateTime) ||
                     typeof(T) == typeof(bool) || typeof(T) == typeof(byte)
                    )
            {
                outValue = GetValue<T>(value);
            }
            else if (typeof(T) == typeof(object))
            {
                outValue = GetValue<T>(value);
            }
            else
            {
                outValue = default(T);
            }

            return true;
        }

        private static T GetValue<T>(object value)
        {
            Type type = typeof(T);

            var method = type.GetMethod("TryParse", new Type[] { typeof(string), typeof(T).MakeByRefType() });

            if (null == method)
                return default(T);

            T val = default(T);

            var instance = Activator.CreateInstance(type);

            object[] parameters = new object[] { value.ToString(), val };

            bool flag = (bool)method.Invoke(instance, parameters);

            if (flag)
                return (T)parameters[1];
            return val;
        }

        /// <summary>
        /// table转成其他类型datatable
        /// 20140326
        /// add by shwang
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="table">datatable</param>
        /// <returns>list/></returns>
        public static List<T> ConvertToList<T>(this DataTable table) where T : new()
        {
            if (table == null)
                return null;

            List<T> list = new List<T>();
            if (table.Rows.Count == 0)
                return list;

            DataTableEntityBuilder<T> dte;
            foreach (DataRow dtr in table.Rows)
            {
                dte = DataTableEntityBuilder<T>.CreateBuilder(dtr);
                T t = dte.Build(dtr);
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// function:判断dataset是否空
        /// date:20140328
        /// author:shwang
        /// </summary>
        /// <param name="dataset">dataset</param>
        /// <returns>是否空</returns>
        public static bool IsNullOrEmpty(this DataSet dataset)
        {
            if (dataset == null || dataset.Tables.Count == 0)
                return true;
            else
                return false;
        }
    }
}

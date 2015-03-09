using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cn.Vcredit.Common.Tools;

namespace Cn.Vcredit.AMS.Logic.Common
{
    public class LogicUtility
    {
        /// <summary>
        /// 过滤条件字典返回类
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="dict">字典</param>
        /// <returns>类实例</returns>
        public static T ConvertToFilterFromDict<T>(IDictionary<string,string> dict) where T:new()
        {
            if(dict==null)
                return default(T);

            Type t = typeof(T);
            PropertyInfo[] propertyInfoArray = t.GetProperties();
            T newInstance = new T();
            foreach(PropertyInfo info in propertyInfoArray)
            {
                string propertyName = info.Name;
                if (!dict.ContainsKey(propertyName))
                    continue;

                string val = dict[propertyName];
                info.SetValue(newInstance,
                    string.IsNullOrEmpty(val)
                    ? null : Convert.ChangeType(val, info.PropertyType), null);

                //info.SetValue(newInstance, val, null);
            }
            return newInstance;
        }
    }
}

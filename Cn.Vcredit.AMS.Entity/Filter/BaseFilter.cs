using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace Cn.Vcredit.AMS.Entity.Filter
{
    /// <summary>
    /// Author:王书行
    /// CreateTime:2014年8月13日
    /// Description:条件基础类
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// 一页数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页编号，从1开始
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// 排序字段，多个逗号隔开，例如 field1 asc,field2 desc
        /// </summary>
        public string OrderbyStr { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// 记录开始的下标
        /// </summary>
        public int FromIndex { get; set; }
        /// <summary>
        /// 记录结束的下标
        /// </summary>
        public int ToIndex { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Proc输出参数集合
        /// </summary>
        public IDictionary<string, object> outParams { get; set; }

        /// <summary>
        /// 类转换成dict
        /// </summary>
        /// <returns>转换成key、value</returns>
        public virtual Dictionary<string,string> ToDictory()
        {
            PropertyInfo[] propertyInfoArray = this.GetType().GetProperties();
            Dictionary<string, string> dict = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
            foreach(PropertyInfo propertyInfo in propertyInfoArray)
            {
                if(dict.ContainsKey(propertyInfo.Name))
                    continue;

                object obValue = propertyInfo.GetValue(this,null);
                if(obValue == null)
                    continue;

                if (obValue is DateTime)
                {
                    dict.Add(propertyInfo.Name, Convert.ToDateTime(obValue).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else if (obValue is DateTime?)
                {
                    DateTime? dt = obValue as DateTime?;
                    if (dt.HasValue)
                        dict.Add(propertyInfo.Name, Convert.ToDateTime(obValue).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    dict.Add(propertyInfo.Name, obValue.ToString());
                }
            }
            return dict;
        }
    }
}

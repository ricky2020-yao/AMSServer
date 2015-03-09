using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.AMS.Entity.ViewData.FinanceManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cn.Vcredit.Common.Tools;
using Cn.Vcredit.AMS.Entity.Filter;
using System.Reflection;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.DataAccess.Common;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// 处理business相关信息
    /// </summary>
    public class BusinessDal
    {
        /// <summary>
        /// 得到business信息
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="filter">过滤条件，字段名需要和数据库列名一致</param>
        /// <returns>订单信息包括 客户姓名、身份证号</returns>
        public List<T> GetBusinessInfo<T>(BaseFilter filter) where T:new()
        {
            Type filterType = filter.GetType();
            PropertyInfo[] propertyInfoArray = filterType.GetProperties();
            StringBuilder conditionSbl = new StringBuilder();
            foreach(PropertyInfo propertyInfo in propertyInfoArray)
            {
                object propertyInfoV = propertyInfo.GetValue(filter, null);
                if (propertyInfoV.IsDefaultValue())
                    continue;

                if (DataAccessConsts.BaseFilterProperty.Contains(propertyInfo.Name))
                    continue;

                if(propertyInfoV is string)
                {
                    conditionSbl.AppendFormat("AND {0}='{1}' ", propertyInfo.Name, propertyInfoV);
                }
                else if(propertyInfoV is DateTime)
                {
                    conditionSbl.AppendFormat("AND {0}='{1}' ", propertyInfo.Name,Convert.ToDateTime(propertyInfoV).DateTimeToString2());
                }
                else
                {
                    conditionSbl.AppendFormat("AND {0}={1} ", propertyInfo.Name, propertyInfoV);
                }
            }

            string pageConditionStr = string.Empty;
            if (filter.PageNo > 0)
            {
                int startNo = (filter.PageNo - 1) * filter.PageSize + 1;
                int endNo = startNo + filter.PageSize - 1;
                pageConditionStr = string.Format("WHERE t.num BETWEEN {0} AND {1}", startNo, endNo);
            }

            string sql = "SQL\\BusinessService\\BusinessSearch.sql".ToFileContent(false, conditionSbl.ToString(), pageConditionStr);
            return DataAccessUtility.GetSearchDataByPageNo<T>(filter, sql);

        }
    }
}

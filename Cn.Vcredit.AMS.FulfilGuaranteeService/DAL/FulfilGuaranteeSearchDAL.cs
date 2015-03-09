using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.FulfilGuaranteeService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-10
    /// Description:履行担保批次查询服务数据访问层
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FulfilGuaranteeSearchDAL<T>
        : BaseSearchDAL<T> where T : class, new()
    {
        /// <summary>
        /// 获取指定如何解释命令字符串
        /// </summary>
        /// <returns></returns>
        protected override CommandType GetCommandType()
        {
            return CommandType.Text;
        }

        /// <summary>
        /// 根据过滤条件，返回检索件数的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetCountSql(BaseFilter baseFilter)
        {
            GuaranteeBatchFilter filter = baseFilter as GuaranteeBatchFilter;

            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(@" SELECT COUNT(1) FROM 
                (SELECT DISTINCT
                        g.GuaranteeNum ,
                        c.NAME ,
                        s.MappingValue ,
                        g.CreateDate ,
                        g.GuaranteeIndex
                   FROM dun.GuaranteeBatchPay g WITH ( NOLOCK )
             INNER JOIN dbo.Business b WITH ( NOLOCK ) 
                     ON b.GuaranteeNum = g.GuaranteeNum
              LEFT JOIN common.MappingConfig s WITH ( NOLOCK )
                     ON b.ServiceSideKey = s.MappingKey
                    AND MappingType = 'SUBCOMPANY'
              LEFT JOIN dbo.ConstSysEnum c WITH ( NOLOCK ) 
                     ON g.Region = c.fullkey ");

            string condition = CombineCondition(filter);
            if (!string.IsNullOrEmpty(condition))
                sb.Append(condition);

            sb.Append(" ) t");

            return sb.ToString();
        }

        /// <summary>
        /// 根据过滤条件，返回检索数据的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSql(BaseFilter baseFilter)
        {
            GuaranteeBatchFilter filter = baseFilter as GuaranteeBatchFilter;

            if (filter == null)
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(@" SELECT * FROM 
                (SELECT  g.GuaranteeNum AS GuaranteeNum ,
                        c.NAME AS Region ,
                        m.name AS ChildCompany ,
                        g.CreateDate ,
                        SUM(b.ToGuaranteeAmt) AS Amount ,
                        g.GuaranteeIndex ,
                        CAST(g.GuaranteeMonth AS VARCHAR) AS GuaranteeMonth ,
                        ROW_NUMBER() OVER ( ORDER BY c.fullkey, m.name, g.GuaranteeIndex ) AS num
                   FROM dun.GuaranteeBatchPay g WITH ( NOLOCK )
             INNER JOIN dbo.Business b WITH ( NOLOCK ) 
                     ON b.GuaranteeNum = g.GuaranteeNum
              LEFT JOIN common.MappingConfig s WITH ( NOLOCK ) 
                     ON b.ServiceSideKey = s.MappingKey
                    AND MappingType = 'SUBCOMPANY'
              LEFT JOIN dbo.ConstSysEnum m WITH ( NOLOCK ) ON s.MappingValue = m.fullkey
              LEFT JOIN dbo.ConstSysEnum c WITH ( NOLOCK ) ON g.Region = c.fullkey ");

            string condition = CombineCondition(filter);
            if (!string.IsNullOrEmpty(condition))
                sb.Append(condition);
            sb.Append(" GROUP BY g.GuaranteeNum,c.NAME,m.name,g.CreateDate,g.GuaranteeIndex,g.GuaranteeMonth,c.fullkey");
            sb.Append(" ) t");
            sb.AppendFormat(" WHERE t.num > {0} AND t.num <= {1}", filter.FromIndex, filter.ToIndex);
            sb.Append(" ORDER BY t.num");

            return sb.ToString();
        }

        /// <summary>
        /// 生成检索条件
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string CombineCondition(GuaranteeBatchFilter filter)
        {
            StringBuilder sb = new StringBuilder();

            if (filter.HasPayDate)
                sb.AppendFormat(" WHERE g.PayDate IS NOT NULL ");
            else
                sb.AppendFormat(" WHERE g.PayDate IS NULL ");

            if (!string.IsNullOrEmpty(filter.GuaranteeNo))
                sb.AppendFormat(" AND g.GuaranteeNum='{0}'", filter.GuaranteeNo);

            if (!string.IsNullOrEmpty(filter.Region))
                sb.AppendFormat(" AND c.fullKey IN({0})", filter.Region);

            if (!string.IsNullOrEmpty(filter.GuaranteeMonth))
                sb.AppendFormat(" AND g.GuaranteeMonth={0}", filter.GuaranteeMonth.ToInt());

            if (!string.IsNullOrEmpty(filter.ChildCompany))
                sb.AppendFormat(" AND s.MappingValue='{0}'", filter.ChildCompany);

            if (filter.GuaranteeIndex > 0)
                sb.AppendFormat(" AND g.GuaranteeIndex={0}", filter.GuaranteeIndex);

            return sb.ToString();
        }
    }
}

using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.FinanceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-12
    /// Description:每日扣导出服务数据处理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeriveRecExportDAL<T>
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
        /// 返回存贮过程名
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetSearchSql(BaseFilter baseFilter)
        {
            var filter = baseFilter as DeriveRecExportFilter;
            if (filter == null)
                return "";

            string sqlFormat 
                = @"SELECT bs.ContractNo
                         , p.PersonName AS CustomerName
                         , bl.BillMonth
                         , ISNULL(SUM(bi1.Amount),0) AS CapitalAmt
                         , ISNULL(SUM(bi2.Amount),0) AS InterestAmt
                         , ISNULL(SUM(bi9.Amount),0) AS ManagementAmt
                         , ISNULL(SUM(bi3.Amount),0) AS ServiceAmt
                         , ISNULL(SUM(bi4.Amount),0) AS GuaranteeAmt
                         , ISNULL(SUM(bi21.Amount),0) AS CapitalIntBAmt
                         , ISNULL(SUM(bi22.Amount),0) AS ServiceBAmt
                         , ISNULL(SUM(bi23.Amount),0) AS PenaltyIntAmt
                         , ISNULL(SUM(bi.Amount), 0) AS Total
                    FROM Received r WITH (NOLOCK)
                    JOIN Bill bl WITH (NOLOCK) ON r.BillID = bl.BillID
                    JOIN Business bs WITH (NOLOCK) ON bl.BusinessID = bs.BusinessID
                    JOIN customer.CustomerInfo c WITH (NOLOCK) ON c.Custid = bs.CustomerID
                    JOIN customer.Person p WITH (NOLOCK) ON p.PersonId=c.PersonId
                    LEFT OUTER JOIN BillItem bi1 WITH (NOLOCK) ON r.BillItemID = bi1.BillItemID AND bi1.Subject = 1
                    LEFT OUTER JOIN BillItem bi2 WITH (NOLOCK) ON r.BillItemID = bi2.BillItemID AND bi2.Subject = 2
                    LEFT OUTER JOIN BillItem bi9 WITH (NOLOCK) ON r.BillItemID = bi9.BillItemID AND bi9.Subject = 9
                    LEFT OUTER JOIN BillItem bi3 WITH (NOLOCK) ON r.BillItemID = bi3.BillItemID AND bi3.Subject = 3
                    LEFT OUTER JOIN BillItem bi4 WITH (NOLOCK) ON r.BillItemID = bi4.BillItemID AND bi4.Subject = 4
                    LEFT OUTER JOIN BillItem bi21 WITH (NOLOCK) ON r.BillItemID = bi21.BillItemID AND bi21.Subject = 21
                    LEFT OUTER JOIN BillItem bi22 WITH (NOLOCK) ON r.BillItemID = bi22.BillItemID AND bi22.Subject = 22
                    LEFT OUTER JOIN BillItem bi23 WITH (NOLOCK) ON r.BillItemID = bi23.BillItemID AND bi23.Subject = 23
                    LEFT OUTER JOIN BillItem bi WITH (NOLOCK) ON r.BillItemID = bi.BillItemID AND bi.Subject IN (1,2,9,3,4,21,22,23)
                    WHERE r.CreateTime >= '{0}' 
                    AND r.CreateTime < '{1}' 
                    AND bs.ServiceSideKey IN ('COMPANY/WX_SHWX_SERVICE', 'COMPANY/WX_SHWS_SERVICE')
                    AND r.ReceivedType in ({2})
                    GROUP BY  bs.ContractNo, p.PersonName, bl.BillMonth";

            return string.Format(sqlFormat, filter.StartDate, filter.EndDate, filter.AdjustKinds);
        }
    }
}

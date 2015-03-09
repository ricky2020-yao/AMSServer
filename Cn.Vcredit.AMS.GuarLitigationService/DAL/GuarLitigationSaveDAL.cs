using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Cn.Vcredit.Data;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Data.DB.Data;

namespace Cn.Vcredit.AMS.GuarLitigationService.DAL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年10月21日
    /// Description:担保和诉讼设置状态修改保存数据存取
    /// </summary>
    public class GuarLitigationSaveDAL:BaseDao
    {
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        protected string GetConnectKey()
        {
            return "PostLoanDB";
        }

        /// <summary>
        /// 获取正常订单的件数
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public int GetNormalBillsCount(int businessId)
        {            
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT COUNT(*) FROM [dbo].[Bill] WITH (NOLOCK)");
            sb.Append(" WHERE BusinessID = " + businessId);
            sb.Append("   AND BillType IN (1, 2)");
            sb.Append("   AND IsShelve = 0");

            // 获取正常订单的件数
            return (int)QueryScalar(sb.ToString(), null, GetConnectKey(), CommandType.Text);
        }

        /// <summary>
        /// 获取业务号下的账单列表欠费金额
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public decimal GetBillItemsAmount(int businessId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ISNULL(SUM(Amount), 0)");
            sb.Append(" FROM [dbo].[BillItem] WITH (NOLOCK)");
            sb.Append(" WHERE BusinessID = " + businessId);
            sb.Append(" AND Subject = 1");

            // 获取业务号下的账单列表欠费金额的总额
            return (decimal)QueryScalar(sb.ToString(), null, GetConnectKey(), CommandType.Text);
        }

        /// <summary>
        /// 更新订单信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int UpdateBusiness(string guid, BusinessGuaranteeSaveFilter filter)
        {
            IDictionary<string, object> inParams = new Dictionary<string, object>();
            inParams.Add("@Guid", guid);
            inParams.Add("@BusinessID", filter.BusinessID);
            inParams.Add("@CustomerID", filter.CustomerID);
            inParams.Add("@BusinessStatus", filter.BusinessStatus);
            inParams.Add("@LawsuitStatus", filter.LawsuitStatus);
            inParams.Add("@LawsuitCode", filter.LawsuitCode);
            inParams.Add("@IsAddNewBill", filter.IsAddNewBill);
            inParams.Add("@UserId", filter.UserId);
            if (!string.IsNullOrEmpty(filter.ToLitigationTime))
                inParams.Add("@ToLitigationTime", filter.ToLitigationTime);

            // 获取更新的件数
            return Execute("proc_BillDun_BusinessGuaranteeSave", inParams, GetConnectKey(), CommandType.StoredProcedure);
        }
    }
}

using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.EveryDueRepayReportService.Common
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月10日
    /// Description:转换共同处理
    /// </summary>
    public static class ConvertUtil
    {
        /// <summary>
        /// 转换每日逾期客户还款明细
        /// </summary>
        /// <param name="lstViewData">每日逾期客户还款明细</param>
        public static void ConvertEveryDueRepayDetailReportViewData(List<EveryDueRepayDetailReportViewData> lstViewData)
        {
            int index = 0;
            foreach (var data in lstViewData)
            {
                data.No = ++index;
                data.RepaymentMode = GetRepaymentModeName(data.RepaymentMode);	// 还款方式
                data.ProductKind = GetProductKindName(data.ProductKind);        // 产品类型
                data.StrExchangeDate = data.ExchangeDate.ToDateString();        // 入账时间
                data.StrRecordingDate = data.RecordingDate.ToDateString();      // 交易日期
            }
        }

        /// <summary>
        /// 获取还款方式名称
        /// </summary>
        /// <param name="repaymentMode"></param>
        /// <returns></returns>
        public static string GetRepaymentModeName(string repaymentMode)
        {
            try
            {
                EnumAdjustKind kind = (EnumAdjustKind)Enum.Parse(typeof(EnumAdjustKind), repaymentMode);
                return kind.ToDescription();
            }
            catch (Exception)
            {
            }

            return "";
        }

        /// <summary>
        /// 获取产品类型
        /// </summary>
        /// <param name="productKind"></param>
        /// <returns></returns>
        public static string GetProductKindName(string productKind)
        {
            switch (productKind)
            {
                case "无抵押贷款":
                case "快速加贷":
                    return "无抵押";
                case "抵押贷款":
                case "房贷":
                case "购车融资":
                case "原车融资":
                    return "抵押";
                default:
                    return productKind;
            }
        }
    }
}

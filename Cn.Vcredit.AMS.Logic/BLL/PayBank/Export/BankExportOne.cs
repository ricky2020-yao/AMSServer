using Cn.Vcredit.AMS.Data.DB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.BLL.PayBank.Export
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年6月8日
    /// 导出模版一（适用 上海维信外贸担保方）
    /// 格式为：建行号|储蓄卡号|姓名|金额
    /// </summary>
    public class BankExportOne : ExportTemplate
    {
		#region- 功能函数 -
		/// <summary>
		/// 模版格式
		/// </summary>
		/// <param name="bill"></param>
		/// <param name="Amount"></param>
		/// <returns></returns>
        public override byte[] FomartTemplate(PayBankExportItem item, decimal amount,
            List<BankAccount> banks, BankAccount account, bool IsServ = false)
        {
            return Encoding.GetEncoding("GBK").GetBytes(string.Format("{0}|{1}|{2}|{3}\r\n"
                , IsServ ? item.ConstructSedNo.Trim() ?? string.Empty
                : item.ConstructionBankNo.Trim() ?? string.Empty
                , item.SavingCard.Trim()
                , item.SavingUser.Trim()
                , amount
                ));
        }
		#endregion
    }
}

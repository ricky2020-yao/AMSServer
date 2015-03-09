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
    /// 导出模版一（适用 杭州光大TXT）
    /// 格式为：储蓄卡号|金额|姓名
    /// </summary>
    public class BankExportSeven : ExportTemplate
    {
        #region- 功能函数 -
        /// <summary>
        /// 模版格式
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public override byte[] FomartTemplate(PayBankExportItem item, decimal Amount,
            List<BankAccount> banks, BankAccount account, bool IsServ = false)
        {
            return Encoding.GetEncoding("GBK").GetBytes(string.Format("{0}|{1}|{2}|\r\n"
               , item.SavingCard.Trim()
			   , Amount
               , item.SavingUser.Trim()));
        }
        #endregion
    }
}

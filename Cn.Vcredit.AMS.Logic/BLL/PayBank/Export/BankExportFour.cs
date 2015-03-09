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
    /// 导出模版四（适用成都小额贷款）
    /// 格式为：储蓄卡号|姓名|金额|合同号
    /// </summary>
    public class BankExportFour : ExportTemplate
    {
        #region- 实现抽象方法 -
        /// <summary>
        /// 实现返回文本字节流的抽象方法
        /// </summary>
        /// <param name="billitem">款项列表</param>
        /// <param name="customers">客户列表</param>
        /// <returns>文本字节流</returns>
        public override byte[] FomartTemplate(PayBankExportItem item, decimal amount,
            List<BankAccount> banks, BankAccount account, bool IsServ = false)
        {
            return Encoding.GetEncoding("GBK").GetBytes(string.Format("{0}|{1}|{2}|{3}|\r\n"
                    , item.SavingCard.Trim()
                    , item.SavingUser.Trim()
                    , amount
                    , item.ContractNo));
        }
        #endregion
    }
}

using Cn.Vcredit.AMS.Common.DBData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.BLL.PayBank.Export
{
    public class ExportTempNine : ExportTemplate
    {
        #region- 功能函数 -
        public override byte[] FomartTemplate(PayBankExportItem item, decimal amount
                    , List<BankAccount> banks, List<Customer> customers, BankAccount account, bool IsServ = false)
        {
            string mobile = customers.FirstOrDefault(o => o.IdenNo == item.IdenNo) == null ?
                "" : customers.FirstOrDefault(o => o.IdenNo == item.IdenNo).Mobile;
            return Encoding.GetEncoding("GBK").GetBytes(
                string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}" + "\r\n"
               , "RMB"
               , ""
               , account.ApplicationNo.Trim()
               , account.AccountNumber.Trim()
               , amount
               , item.ContractNo.Trim()
               , ""
               , mobile.Trim()
               , ""
               , account.BankKey == "BANKLIST/GONGHANG" ? "是" : "否"
               , item.SavingCard));
        }
        #endregion
    }
}

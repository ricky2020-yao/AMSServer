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
    /// 导出模版二（适用 上海维视渤海，上海维视外贸、上海维信外贸服务\信托）
    /// 格式为：储蓄卡号|姓名|金额|合同号|款项名称|本金
    /// </summary>
    public class BankExportTwo : ExportTemplate
    {
        #region- 功能函数 -
        public override byte[] FomartTemplate(PayBankExportItem item, decimal amount
                    , List<BankAccount> banks,BankAccount account, bool IsServ = false)
        {
            return Encoding.GetEncoding("GBK").GetBytes(string.Format("{0}|{1}|{2}|{3}|{4}|{5}\r\n"
               , item.SavingCard.Trim()
               , item.SavingUser.Trim()
               , amount
               , item.ContractNo.Trim()
               , GetSpouse(item.DunLevel)
               , item.LoanCapital
               ));
        }
        #endregion
    }
}

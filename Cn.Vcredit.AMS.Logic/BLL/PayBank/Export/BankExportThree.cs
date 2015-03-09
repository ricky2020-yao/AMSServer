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
    /// 导出模版三（适用 苏州维视）
    /// 格式为：储蓄卡号|姓名|身份证|金额|款项
	/// 622700200172000000|黄海鹰|322019880000000|10000|合同号
    /// </summary>
    public class BankExportThree : ExportTemplate
    {
        #region- 功能函数 -
        public override byte[] FomartTemplate(PayBankExportItem item, decimal amount,
            List<BankAccount> banks,BankAccount account, bool IsServ = false)
        {
            return Encoding.GetEncoding("GBK").GetBytes(string.Format("{0}|{1}|{2}|{3}|{4}\r\n"
                    , item.SavingCard.Trim()
                    , item.SavingUser.Trim()
                    , account.PaymentAccount.Trim()
                    , amount
                    , item.ContractNo.Trim()
                    ));
        }
        #endregion
    }
}

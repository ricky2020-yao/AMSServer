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
    /// 导出模版抽象类
    /// </summary>
    public abstract class ExportTemplate
    {
        #region- 功能函数 -
        /// <summary>
        /// 设置导出格式
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public abstract byte[] FomartTemplate(PayBankExportItem item, decimal Amount,
            List<BankAccount> banks,BankAccount account, bool IsServ = false);

        protected string GetSpouse(byte Subject)
        {
            switch (Subject)
            {
                case 10:
                    return "本息";
                case 20:
                    return "扣失罚息";
                case 21:
                    return "本息扣失汇总";
                case 22:
                    return "当期本息扣失";
                case 23:
                    return "罚息汇总";
                case 30:
                    return "服务担保管理费";
                case 40:
                    return "服务费扣失";
                case 50:
                    return "订单汇总";
                default:
                    return "";
            }
        }
        #endregion
    }
}

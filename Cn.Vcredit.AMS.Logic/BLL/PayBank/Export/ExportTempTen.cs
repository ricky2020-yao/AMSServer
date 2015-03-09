using Cn.Vcredit.AMS.Common.DBData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.BLL.PayBank.Export
{
    public class ExportTempTen : ExportTemplate2
    {
        #region- 功能函数 -

        #region 返回字节数
        public override byte[] RetByte(List<string[]> list, int operateid,
            List<Customer> customer, string title)
        {
            string[] titles = {"币种","顺序号","协议编号","收款单位名称","付款金额","用途","备注"
              ,"付款账户短信通知手机号码","自定义序号","是否是工行账号","付方账号开户行行号"};

            XlsDocument xls = new XlsDocument();
            xls.FileName = title;
            Worksheet wks = xls.Workbook.Worksheets.Add(title);
            Cells cell = wks.Cells;

            XF xf = ExportExtend.SetCommonStyle(xls);
            ExportExtend.SetColumnWidth(xls, wks, 1, 25);
            ExportExtend.SetColumnWidth(xls, wks, 3, 25);
            ExportExtend.SetColumnWidth(xls, wks, 4, 15);
            ExportExtend.SetColumnWidth(xls, wks, 7, 25);
            ExportExtend.SetColumnWidth(xls, wks, 12, 12);
            ExportExtend.SetColumnWidth(xls, wks, 15, 25);
            ExportExtend.SetColumnWidth(xls, wks, 17, 20);
            ExportExtend.SetColumnWidth(xls, wks, 18, 20);

            for (int i = 1; i <= list.Count; i++)
            {
                if (i == 1)
                {
                    for (int j = 1; j <= list[i - 1].Length; j++)
                    {
                        Cell cel = cell.Add(i, j, titles[j - 1], xf);
                    }
                }
                for (int j = 1; j <= list[i - 1].Length; j++)
                {
                    if (j == 1)
                    {
                        Cell cel = cell.Add(i + 1, 1, i, xf);
                    }

                    else if ((j >= 5 && j <= 13))
                    {
                        Cell cel = cell.Add(i + 1, j, list[i - 1][j - 1].ToDecimal(), xf);
                    }
                    else if (j == 14)
                    {
                        Cell cel = cell.Add(i + 1, j, list[i - 1][j - 1].ToInt(), xf);
                    }
                    else
                    {
                        Cell cel = cell.Add(i + 1, j, list[i - 1][j - 1].Trim(), xf);
                    }
                }
            }
            return xls.Bytes.ByteArray;
        }
        #endregion

        #region 导出内容转换
        /// <summary>
        /// 转换导出的内容
        /// </summary>
        /// <param name="list"></param>
        /// <param name="num"></param>
        /// <param name="customer"></param>
        /// <param name="enums"></param>
        /// <returns></returns>
        public override string[] GetImportExcelItem(PayBankExportItem item, decimal amount,
           List<Customer> customer, List<BankAccount> bankaccounts, List<Enumeration> enums,
           BankAccount bka, bool isgurante = false)
        {
            string mobile = customer.FirstOrDefault(o => o.IdenNo == item.IdenNo) == null ?
               "" : customer.FirstOrDefault(o => o.IdenNo == item.IdenNo).Mobile;
            string[] strlist = 
			{
				"RMB",
				"",
				bka.ApplicationNo.Trim(),
                bka.AccountNumber.Trim(),
				amount.ToAmtString(),
				item.ContractNo.Trim(),
				"",
                mobile.Trim(),
                "",
		        bka.BankKey=="BANKLIST/GONGHANG"?"是":"否",
                item.SavingCard.Trim()
			};
            return strlist;
        }
        #endregion

        #endregion
    }
}

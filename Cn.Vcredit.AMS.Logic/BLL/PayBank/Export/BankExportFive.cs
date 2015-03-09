using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Cn.Vcredit.Common.Tools;
using Cn.Vcredit.Common.ExcelExport;
using Cn.Vcredit.AMS.Data.DB.Data;

namespace Cn.Vcredit.AMS.Logic.BLL.PayBank.Export
{
	/// <summary>
	/// Author:陈伟
	/// CreateTime:2012年6月8日
	/// 导出模版五（杭州工商Excel）服务方+信托方（格式相同）
	/// 格式为：
	/// </summary>
	public class BankExportFive : ExportTemplate2
	{
		#region- 功能函数 -

		#region 返回字节数
		public override byte[] RetByte(List<string[]> list, int operationid,string title)
		{
			string[] titles = {"币种","顺序号","	付方开户行名","付方卡号","缴费编号","付方户名"
              ,"收款单位开户行名","省份","收款方地区名","收款方地区代码","收款账号","协议编号",
              "收款单位名称","付款金额","用途","备注","付款账户短信通知手机号码",
              "自定义序号","是否工行账号","付方账号开户行行号"};

			XlsDocument xls = new XlsDocument();
			xls.FileName = title;

            Worksheet wks = xls.Workbook.Worksheets.Add("Sheet1");
			Cells cell = wks.Cells;

			XF xf = ExportExtend.SetCommonStyle(xls);
			ExportExtend.SetColumnWidth(xls, wks, 2, 15);
			ExportExtend.SetColumnWidth(xls, wks, 3, 23);
			ExportExtend.SetColumnWidth(xls, wks, 4, 22);
			ExportExtend.SetColumnWidth(xls, wks, 5, 15);
			ExportExtend.SetColumnWidth(xls, wks, 6, 18);
			ExportExtend.SetColumnWidth(xls, wks, 7, 10);
			ExportExtend.SetColumnWidth(xls, wks, 8, 15);
			ExportExtend.SetColumnWidth(xls, wks, 9, 15);
			ExportExtend.SetColumnWidth(xls, wks, 10, 22);
			ExportExtend.SetColumnWidth(xls, wks, 11, 22);
			ExportExtend.SetColumnWidth(xls, wks, 12, 25);
			ExportExtend.SetColumnWidth(xls, wks, 13, 15);
			ExportExtend.SetColumnWidth(xls, wks, 14, 20);
			ExportExtend.SetColumnWidth(xls, wks, 16, 25);
			ExportExtend.SetColumnWidth(xls, wks, 17, 15);
			ExportExtend.SetColumnWidth(xls, wks, 18, 15);
			ExportExtend.SetColumnWidth(xls, wks, 19, 20);
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
					if (j == 2)
					{
						Cell cel = cell.Add(i + 1, 2, i, xf);
					}
                    else if (j == 10)
                    {
                        Cell cel = cell.Add(i + 1, j, list[i - 1][j - 1].Trim().ToInt(), xf);
                    }
                    else if (j == 14)
                    {
                        Cell cel = cell.Add(i + 1, j, list[i - 1][j - 1].Trim().ToDecimal(), xf);
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
          List<BankAccount> bankaccounts, List<Enumeration> enums, BankAccount bka, bool isgurante = false)
        {
            string[] strlist = 
			{
				"RMB",
				"",
				"工行",
                item.SavingCard.Trim(),
                item.IdenNo.Trim(),
                item.SavingUser.Trim(),
                bka.BankName.Trim(),
                bka.ProvinceName.Trim(),
                bka.AreaName.Trim(),
                bka.AreaCode.Trim(),
                bka.SavingCard.Trim(),
                bka.ApplicationNo.Trim(),
                enums.FirstOrDefault(p => p.FullKey == bka.CompanyKey).Name,
                amount.ToString(),
				//GetSpouse(item.DunLevel),
                item.ContractNo,"","","","",""
			};
            return strlist;
        }
		#endregion

		#endregion
	}
}

using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.Common.ExcelExport;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.BLL.PayBank.Export
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年6月8日
    /// 导出模版五 服务方明细
    /// 格式为：
    /// </summary>
    public class BankExportSix : ExportTemplate2
    {
        #region- 功能函数 -

        #region 返回字节数
        public override byte[] RetByte(List<string[]> list, int operateid,string title)
        {
            string[] titles = {"序号","合同号","	姓名","身份证号","名义金额","服务费","担保管理费"
              ,"服务费扣款失败手续费","本金","利息"
              ,"本息扣失","罚息","合计","贷款期限","服务费率","账号","开户省市",
              "放贷月份","扣款月份"};

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
            List<BankAccount> bankaccounts, List<Enumeration> enums,
           BankAccount bka, bool isgurante = false)
        {
            string[] strlist = 
			{
				"",
				item.ContractNo.Trim(),
				item.SavingUser.Trim(),
                item.IdenNo.Trim(),
				item.LoanCapital.ToString(),
				item.FWF.ToString(),
				item.DBF.ToString(),
                item.FWFKS.ToString(),
                item.BJ.ToString(),
				item.LX.ToString(),
                item.BXKS.ToString(),
                item.FX.ToString(),
                amount.ToString(),
                item.LoanPeriod.ToString(),
				item.ServiceRate.ToPercent(),
                item.SavingCard.Trim(),
                bka.AreaName.Trim(),
                item.LoanTime.ToString("yyyy年MM月"),
                item.BillMonth==null ?
                DateTime.Now.Day<21?DateTime.Now.AddMonths(-1).ToString("yyyy年MM月"): DateTime.Now.ToString("yyyy年MM月")
                :item.BillMonth.Replace("/","年")+"月"
			};
            return strlist;
        }
        #endregion

        #endregion
    }
}

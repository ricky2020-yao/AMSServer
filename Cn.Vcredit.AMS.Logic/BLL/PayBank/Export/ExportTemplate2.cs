using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cn.Vcredit.Common.ExcelExport;
using Cn.Vcredit.AMS.Data.DB.Data;

namespace Cn.Vcredit.AMS.Logic.BLL.PayBank.Export
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年6月8日
    /// 导出Excel基类
    /// </summary>
    public abstract class ExportTemplate2
    {
        #region 抽象方法
        public abstract byte[] RetByte(List<string[]> list, int operationid,string title);

        public abstract string[] GetImportExcelItem(PayBankExportItem item, decimal amount,
          List<BankAccount> bankaccounts, List<Enumeration> enums, BankAccount bankaccount, bool isgurante = false);
                                                                    
        #endregion

        #region 辅助方法
        /// <summary>
        /// 返回需要导出的Excel字节
        /// </summary>
        /// <param name="filetitle">Excel文件名</param>
        /// <param name="titles">Excel标题</param>
        /// <param name="dt">需要导出的Table数据</param>
        /// <returns>Excel字节流</returns>
        protected byte[] GetExcelBytes(string filetitle, List<string> titles, DataTable dt)
        {
            XlsDocument xls = new XlsDocument();
            xls.FileName = filetitle;

            Worksheet wks = xls.Workbook.Worksheets.Add(filetitle);
            Cells cell = wks.Cells;
            XF xf = xls.NewXF();
            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                for (int j = 1; j <= titles.Count; j++)
                {
                    if (i == 1)
                        cell.Add(i, j, titles[j - 1], xf);
                    else
                        cell.Add(i + 1, j, dt.Rows[i - 1][j - 1], xf);
                }
            }
            return xls.Bytes.ByteArray;
        }

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
                    return "其他费用";
            }
        }
        #endregion
    }
}

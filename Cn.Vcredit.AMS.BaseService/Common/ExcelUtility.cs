using Aspose.Cells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using AsposeXls = Aspose.Cells;

namespace Cn.Vcredit.AMS.BaseService.Common
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月16日
    /// Description:Excel共同操作类
    /// </summary>
    public static class ExcelUtility
    {
        /// <summary>
        /// 生成Excel文件流
        /// </summary>
        /// <param name="titles"></param>
        /// <param name="fields"></param>
        /// <param name="list"></param>
        /// <param name="saveFormat"></param>
        /// <param name="isPropertyNameShown"></param>
        /// <param name="firstRow"></param>
        /// <param name="firstColumn"></param>
        /// <param name="insertRows"></param>
        /// <param name="dateFormatString"></param>
        /// <param name="convertStringToNumber"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static byte[] GenerateExcelFileStream(string[] titles, string[] fields, ICollection list
            , SaveFormat saveFormat, bool isPropertyNameShown, int firstRow, int firstColumn
            , bool insertRows, string dateFormatString, bool convertStringToNumber, string sheetName = "Sheet1")
        {
            AsposeXls.Workbook workbook = new AsposeXls.Workbook();
            AsposeXls.Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = sheetName;

            sheet.FreezePanes(1, 1, 1, 0); //冻结第一行

            for (int i = 0; i < titles.Length; i++)
            {
                sheet.Cells[0, i].PutValue(titles[i]);
            }

            sheet.Cells.ImportCustomObjects(list, fields, isPropertyNameShown, firstRow, firstColumn,
                list.Count, insertRows, dateFormatString, convertStringToNumber);

            sheet.AutoFitColumns();//让各列自适应宽度，这个很有用
            using (MemoryStream stream = new MemoryStream())
            {
                workbook.Save(stream, saveFormat);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 生成Excel文件流
        /// </summary>
        /// <param name="result"></param>
        /// <param name="isFieldNameShown"></param>
        /// <param name="firstRow"></param>
        /// <param name="firstColumn"></param>
        /// <param name="saveFormat"></param>
        /// <param name="rowNumber"></param>
        /// <param name="colNumber"></param>
        /// <param name="insertRows"></param>
        /// <param name="dateFormatString"></param>
        /// <param name="convertStringToNumber"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static byte[] GenerateExcelFileStream(DataTable result, bool isFieldNameShown
            , int firstRow, int firstColumn, SaveFormat saveFormat, int rowNumber, int colNumber, bool insertRows, string dateFormatString
            , bool convertStringToNumber, string sheetName = "Sheet1")
        {
            AsposeXls.Workbook workbook = new AsposeXls.Workbook();
            AsposeXls.Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = sheetName;

            sheet.FreezePanes(1, 1, 1, 0);  // 冻结第一行
            //sheet.Cells.ImportDataTable(result, isFieldNameShown, firstRow, firstColumn
            //    , rowNumber, colNumber, insertRows, dateFormatString, true);
            sheet.Cells.ImportDataTable(result, true, 0, 0);
            sheet.AutoFitColumns();         // 让各列自适应宽度，这个很有用

            using (MemoryStream stream = new MemoryStream())
            {
                workbook.Save(stream, saveFormat);
                return stream.ToArray();
            }
        }
    }
}

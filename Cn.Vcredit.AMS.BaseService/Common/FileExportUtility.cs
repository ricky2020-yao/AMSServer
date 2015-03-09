using Aspose.Cells;
using Cn.Vcredit.Common.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.Common
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年10月23日
    /// Description:文件导出共同类
    /// </summary>
    public class FileExportUtility
    {
        /// <summary>
        /// 生成文件流
        /// </summary>
        /// <param name="titles"></param>
        /// <param name="fields"></param>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static byte[] GenerateFileStream(string[] titles, string[] fields, ICollection list, EnumExportFileType type, string sheetName = "Sheet1")
        {
            byte[] result = null;
            switch (type)
            {
                case EnumExportFileType.Xls:
                    result = ExcelUtility.GenerateExcelFileStream(titles, fields,
                        list, SaveFormat.Excel97To2003, false, 1, 0, true, "yyyy-MM-dd", false, sheetName);
                    break;
                case EnumExportFileType.Xlsx:
                    result = ExcelUtility.GenerateExcelFileStream(titles, fields,
                        list, SaveFormat.Xlsx, false, 1, 0, true, "yyyy-MM-dd", false, sheetName);
                    break;
                case EnumExportFileType.Txt:
                case EnumExportFileType.Pdf:
                    break;
            }

            return result;
        }

        /// <summary>
        /// 生成Excel文件流
        /// </summary>
        /// <param name="result"></param>
        /// <param name="type"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static byte[] GenerateFileStream(DataTable dtResult, EnumExportFileType type, string sheetName = "Sheet1")
        {
            if (dtResult == null || dtResult.Rows.Count == 0 || dtResult.Columns.Count == 0)
                return null;

            byte[] result = null;
            switch (type)
            {
                case EnumExportFileType.Xls:
                    result = ExcelUtility.GenerateExcelFileStream(dtResult, true
                        , 0, 0, SaveFormat.Excel97To2003, 1, 1, false, "yyyy-MM-dd", false, sheetName);
                    break;
                case EnumExportFileType.Xlsx:
                    result = ExcelUtility.GenerateExcelFileStream(dtResult, true
                        , 0, 0, SaveFormat.Xlsx, dtResult.Rows.Count, dtResult.Columns.Count, false, "yyyy-MM-dd", false, sheetName);
                    break;
                case EnumExportFileType.Txt:
                    result = TxtUtility.GetExportBytes(dtResult);
                    break;
                case EnumExportFileType.Pdf:
                    break;
            }

            return result;
        }
    }
}

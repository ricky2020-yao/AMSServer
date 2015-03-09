using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.Common
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-12-11
    /// Description:Text共同操作类
    /// </summary>
    /// </summary>
    public class TxtUtility
    {
        /// <summary>
        /// Author:shwang
        /// Date:20140617
        /// Desc:拼接导出的文本内容格式
        /// </summary>
        /// <param name="dtb">DataTable</param>
        /// <returns>文本内容</returns>
        public static string GetExportStr(DataTable dtb)
        {
            StringBuilder sbl = new StringBuilder();
            foreach (DataColumn dcl in dtb.Columns)
            {
                sbl.AppendFormat("{0}\t", dcl.ColumnName);
            }
            sbl.Append(System.Environment.NewLine);

            foreach (DataRow dtr in dtb.Rows)
            {
                for (int i = 0; i < dtb.Columns.Count; i++)
                {
                    sbl.AppendFormat("{0}\t", dtr[i]);
                }
                sbl.Append(System.Environment.NewLine);
            }
            sbl.AppendFormat("END|{0}", dtb.Rows.Count);
            return sbl.ToString();
        }

        /// <summary>
        /// Author:shwang
        /// Date:20140617
        /// Desc:拼接导出的文本内容格式
        /// </summary>
        /// <param name="dtb">DataTable</param>
        /// <returns>文本内容</returns>
        public static byte[] GetExportBytes(DataTable dtb)
        {
            StringBuilder sbl = new StringBuilder();
            foreach (DataColumn dcl in dtb.Columns)
            {
                sbl.AppendFormat("{0}\t", dcl.ColumnName);
            }
            sbl.Append(System.Environment.NewLine);

            foreach (DataRow dtr in dtb.Rows)
            {
                for (int i = 0; i < dtb.Columns.Count; i++)
                {
                    sbl.AppendFormat("{0}\t", dtr[i]);
                }
                sbl.Append(System.Environment.NewLine);
            }
            sbl.AppendFormat("END|{0}", dtb.Rows.Count);
            return ConvertUtility.CodingToByte(sbl.ToString(), 2);
        }
    }
}

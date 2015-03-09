using System;
using System.Collections.Generic;
using System.Text;
using Cn.Vcredit.Common.Excel.ByteUtil;

namespace Cn.Vcredit.Common.Excel
{

    /// <summary>
	/// ��ΰ
    /// Excel������չ
    /// </summary>
    public static class ExportExtend 
    {
        /// <summary>
        /// ����ĳһ��ֵ�Ŀ��
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="wks"></param>
        /// <param name="columnindex"></param>
        /// <param name="widthline"></param>
        public static void SetColumnWidth(XlsDocument xls, Worksheet wks, ushort columnindex, ushort widthline)
        {
            ColumnInfo colinfo = new ColumnInfo(xls, wks);
            colinfo.ColumnIndexStart = columnindex;
            colinfo.ColumnIndexEnd = columnindex;
            colinfo.Width = (ushort)(widthline * 256);
            wks.AddColumnInfo(colinfo);
        }
		/// <summary>
		/// �Żع�����ʽ
		/// </summary>
		/// <param name="xls"></param>
		/// <returns></returns>
		public static XF SetCommonStyle(XlsDocument xls)
		{
			XF xf = xls.NewXF();
			xf.HorizontalAlignment = HorizontalAlignments.Centered;
			xf.VerticalAlignment = VerticalAlignments.Centered;
			return xf;
		}
    }
}

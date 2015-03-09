using Cn.Vcredit.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-11-25
    /// Description:实收数据
    /// </summary>
    public class ReceivedCutViewData
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 科目类型
        /// </summary>
        public byte Subject { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 收款类型
        /// </summary>
        public byte ReceivedType { get; set; }


        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}{1}", BusinessID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", Subject.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", Amount.ToString(), WebServiceConst.Separater_1);
            sb.Append(ReceivedType.ToString());

            return sb.ToString();
        }

        /// <summary>
        /// 解析成实体类
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static ReceivedCutViewData ToEntity(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return null;

            string[] lines = strValue.Split(
                WebServiceConst.Separater_1.ToArray(), StringSplitOptions.None);

            PropertyInfo[] proInfos = typeof(ReceivedCutViewData).GetProperties();

            if (lines == null || proInfos == null || lines.Length != proInfos.Length)
                return null;

            int index = 0;
            var data = new ReceivedCutViewData();
            int businessID = 0;
            byte subject = 0;
            byte receivedType = 0;
            decimal amount = 0;

            int.TryParse(lines[index++], out businessID);
            data.BusinessID = businessID;

            byte.TryParse(lines[index++], out subject);
            data.Subject = subject;

            decimal.TryParse(lines[index++], out amount);
            data.Amount = amount;

            byte.TryParse(lines[index++], out receivedType);
            data.ReceivedType = receivedType;

            return data;
        }
    }
}

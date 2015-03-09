using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// Author:shwang
    /// Date:20140616
    /// Desc:解约退款
    /// </summary>
    public class CancelRefundViewData
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public int BusinessId { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 实际发放贷款金额
        /// </summary>
        public decimal RealLoanCapital { get; set; }

        /// <summary>
        /// 实退金额
        /// </summary>
        public decimal RefundAmt { get; set; }

        /// <summary>
        /// 付款日期
        /// </summary>
        public DateTime? PayDate { get; set; }

        /// <summary>
        /// 到账日期
        /// </summary>
        public DateTime? ReceivedDate { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public byte PayType { get; set; }

        /// <summary>
        /// 解约日期
        /// </summary>
        public DateTime? CancelTime { get; set; }

        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}{1}", BusinessId.ToString(), WebServiceConst.Separater_1);
            if (string.IsNullOrEmpty(ContractNo))
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", ContractNo.ToString(), WebServiceConst.Separater_1);

            if (string.IsNullOrEmpty(CustomerName))
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", CustomerName.ToString(), WebServiceConst.Separater_1);

            sb.AppendFormat("{0}{1}", RealLoanCapital.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", RefundAmt.ToString(), WebServiceConst.Separater_1);

            if (PayDate.HasValue)
                sb.AppendFormat("{0}{1}", PayDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);

            if (ReceivedDate.HasValue)
                sb.AppendFormat("{0}{1}", ReceivedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);

            sb.AppendFormat("{0}{1}", PayType.ToString(), WebServiceConst.Separater_1);

            if (CancelTime.HasValue)
                sb.Append(ReceivedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            else
                sb.Append( "");

            return sb.ToString();
        }

        /// <summary>
        /// 解析成实体类
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static CancelRefundViewData ToEntity(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return null;

            string[] lines = strValue.Split(
                WebServiceConst.Separater_1.ToArray(), StringSplitOptions.None);

            PropertyInfo[] proInfos = typeof(CancelRefundViewData).GetProperties();

            if (lines == null || proInfos == null || lines.Length != proInfos.Length)
                return null;

            int index = 0;
            var data = new CancelRefundViewData();
            int businessID = 0;
            byte payType = 0;
            decimal realLoanCapital = 0;
            decimal refundAmt = 0;
            DateTime payDate = DateTime.Now;
            DateTime receivedDate = DateTime.Now;
            DateTime cancelTime = DateTime.Now;

            int.TryParse(lines[index++], out businessID);
            data.BusinessId = businessID;
            data.ContractNo = lines[index++];
            data.CustomerName = lines[index++];

            decimal.TryParse(lines[index++], out realLoanCapital);
            data.RealLoanCapital = realLoanCapital;
            decimal.TryParse(lines[index++], out refundAmt);
            data.RefundAmt = refundAmt;

            DateTime.TryParse(lines[index++], out payDate);
            data.PayDate = payDate;

            DateTime.TryParse(lines[index++], out receivedDate);
            data.ReceivedDate = receivedDate;

            byte.TryParse(lines[index++], out payType);
            data.PayType = payType;

            DateTime.TryParse(lines[index++], out cancelTime);
            data.CancelTime = cancelTime;

            return data;
        }
    }
}

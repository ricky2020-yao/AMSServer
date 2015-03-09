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
    public class ReceiveDetailViewData
    {
        /// <summary>
        /// 实收编号
        /// </summary>
        public int ReceivedID { get; set; }

        /// <summary>
        /// 帐单编号[关联Bill表]
        /// </summary>
        public long BillID { get; set; }

        /// <summary>
        /// 扣款科目编号[关联BilllTem表]
        /// </summary>
        public long BillItemID { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 实收/调整类型(1、普通 2、冲销 3、反冲销 4、补生成 11、转帐 12、银扣 13、坏帐 14、补确认 15、预减免 16、预收款 17、退回)
        /// </summary>
        public byte ReceivedType { get; set; }

        /// <summary>
        /// 支付编号（银扣与转帐的收款都将添加PayID,其他虚实收则为零）
        /// </summary>
        public int PayID { get; set; }

        /// <summary>
        /// 收款时间
        /// </summary>
        public DateTime? ReceivedTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? ToAcountTime { get; set; }

        /// <summary>
        /// 收款帐户
        /// </summary>
        public int? ToAccountID { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Explain { get; set; }

        /// <summary>
        /// 原数据库编号
        /// </summary>
        public int DeductionID { get; set; }

        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}{1}", ReceivedID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BillID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BillItemID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", Amount.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ReceivedType.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", PayID.ToString(), WebServiceConst.Separater_1);

            if (ReceivedTime.HasValue)
                sb.AppendFormat("{0}{1}", ReceivedTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            if (ToAcountTime.HasValue)
                sb.AppendFormat("{0}{1}", ToAcountTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);
            if (ToAccountID.HasValue)
                sb.AppendFormat("{0}{1}", ToAccountID.Value.ToString(), WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", OperatorID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", Explain, WebServiceConst.Separater_1);
            sb.Append(DeductionID.ToString());

            return sb.ToString();
        }

        /// <summary>
        /// 解析成实体类
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static ReceiveDetailViewData ToEntity(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return null;

            string[] lines = strValue.Split(WebServiceConst.Separater_1.ToArray(), StringSplitOptions.None);

            PropertyInfo[] proInfos = typeof(ReceiveDetailViewData).GetProperties();

            if (lines == null || proInfos == null || lines.Length != proInfos.Length)
                return null;

            int index = 0;
            var data = new ReceiveDetailViewData();
            int receiveID = 0;
            long billID = 0;
            long billItemID = 0;
            decimal amount = 0;
            byte receivedType = 0;
            int payId = 0;
            DateTime receivedTime = DateTime.Now;
            DateTime createTime = DateTime.Now;
            DateTime toAcountTime = DateTime.Now;
            int toAccountID = 0;
            int operatorID = 0;
            int deductionID = 0;

            int.TryParse(lines[index++], out receiveID);
            data.ReceivedID = receiveID;
            long.TryParse(lines[index++], out billID);
            data.BillID = billID;
            long.TryParse(lines[index++], out billItemID);
            data.BillItemID = billItemID;

            decimal.TryParse(lines[index++], out amount);
            data.Amount = amount;

            byte.TryParse(lines[index++], out receivedType);
            data.ReceivedType = receivedType;

            int.TryParse(lines[index++], out payId);
            data.PayID = payId;

            if (DateTime.TryParse(lines[index++], out receivedTime))
                data.ReceivedTime = receivedTime;

            DateTime.TryParse(lines[index++], out createTime);
            data.CreateTime = createTime;

            if (DateTime.TryParse(lines[index++], out toAcountTime))
                data.ToAcountTime = toAcountTime;

            if (int.TryParse(lines[index++], out toAccountID))
                data.ToAccountID = toAccountID;

            int.TryParse(lines[index++], out operatorID);
            data.OperatorID = operatorID;

            data.Explain = lines[index++];

            int.TryParse(lines[index++], out deductionID);
            data.DeductionID = deductionID;

            return data;
        }
    }
}

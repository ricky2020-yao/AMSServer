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
    /// CreateTime:2014年11月21日
    /// Description:账单科目详情的返回结果类
    /// </summary>
    public class BillItemDetailViewData
    {
        /// <summary>
        /// 帐单款项编号（自增列）
        /// </summary>
        public long BillItemID { get; set; }

        /// <summary>
        /// 帐单编号[关联Bill表格]
        /// </summary>
        public long BillID { get; set; }

        /// <summary>
        /// 款项科目（1、本金 2、利息 3、本金扣失 4、服务费 5、服务费扣失 6、担保费 7、罚息）
        /// </summary>
        public byte Subject { get; set; }

        /// <summary>
        /// 科目名称
        /// </summary>
        public string StrSubject { get; set; }

        /// <summary>
        /// 款项类型（1、普通 2、补生成）
        /// </summary>
        public byte SubjectType { get; set; }

        /// <summary>
        /// 欠费金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 实际应收金额
        /// </summary>
        public decimal DueAmt { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal ReceivedAmt { get; set; }

        /// <summary>
        /// 罚息金额
        /// </summary>
        public decimal PenaltyIntAmt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 足额还款时间
        /// </summary>
        public DateTime? FullPaidTime { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperatorID { get; set; }

        /// <summary>
        /// 是否当期
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 是否搁置
        /// </summary>
        public bool IsShelve { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public string StrAmount { get; set; }

        /// <summary>
        /// 实际应收
        /// </summary>
        public string StrDueAmt { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public string StrReceivedAmt { get; set; }

        /// <summary>
        /// 实收项调整公式HTML代码
        /// </summary>
        public string StrReceived { get; set; }

        /// <summary>
        /// 应收项调整公式HTML代码
        /// </summary>
        public string StrReceivable { get; set; }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string StrCreateTime { get; set; }

        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}{1}", BillItemID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BillID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", Subject.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", StrSubject, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", SubjectType.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", Amount.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", DueAmt.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ReceivedAmt.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", PenaltyIntAmt.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);

            if (FullPaidTime.HasValue)
                sb.AppendFormat("{0}{1}", FullPaidTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);

            sb.AppendFormat("{0}{1}", OperatorID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", IsCurrent.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", IsShelve.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BusinessID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", StrAmount, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", StrDueAmt, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", StrReceivedAmt, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", StrReceived, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", StrReceivable, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", Display, WebServiceConst.Separater_1);
            sb.Append(StrCreateTime);

            return sb.ToString();
        }

        /// <summary>
        /// 解析成实体类
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static BillItemDetailViewData ToEntity(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return null;

            string[] lines = strValue.Split(WebServiceConst.Separater_1.ToArray(), StringSplitOptions.None);

            PropertyInfo[] proInfos = typeof(BillItemDetailViewData).GetProperties();

            if (lines == null || proInfos == null || lines.Length != proInfos.Length)
                return null;

            int index = 0;
            var data = new BillItemDetailViewData();
            long billItemID = 0;
            long billID = 0;
            byte subject = 0;
            byte subjectType = 0;
            decimal amount = 0;
            decimal dueAmt = 0;
            decimal receivedAmt = 0;
            decimal penaltyIntAmt = 0;
            DateTime createTime = DateTime.Now;
            DateTime fullPaidTime = DateTime.Now;
            int operatorID = 0;
            bool isCurrent = false;
            bool isShelve = false;
            int businessID = 0;

            long.TryParse(lines[index++], out billItemID);
            data.BillItemID = billItemID;

            long.TryParse(lines[index++], out billID);
            data.BillID = billID;

            byte.TryParse(lines[index++], out subject);
            data.Subject = subject;

            data.StrSubject = lines[index++];

            byte.TryParse(lines[index++], out subjectType);
            data.SubjectType = subjectType;

            decimal.TryParse(lines[index++], out amount);
            data.Amount = amount;

            decimal.TryParse(lines[index++], out dueAmt);
            data.DueAmt = dueAmt;

            decimal.TryParse(lines[index++], out receivedAmt);
            data.ReceivedAmt = receivedAmt;

            decimal.TryParse(lines[index++], out penaltyIntAmt);
            data.PenaltyIntAmt = penaltyIntAmt;

            DateTime.TryParse(lines[index++], out createTime);
            data.CreateTime = createTime;

            if (DateTime.TryParse(lines[index++], out fullPaidTime))
                data.FullPaidTime = fullPaidTime;

            int.TryParse(lines[index++], out operatorID);
            data.OperatorID = operatorID;

            bool.TryParse(lines[index++], out isCurrent);
            data.IsCurrent = isCurrent;

            bool.TryParse(lines[index++], out isShelve);
            data.IsShelve = isShelve;

            int.TryParse(lines[index++], out businessID);
            data.BusinessID = businessID;

            data.StrAmount = lines[index++];
            data.StrDueAmt = lines[index++];
            data.StrReceivedAmt = lines[index++];
            data.StrReceived = lines[index++];
            data.StrReceivable = lines[index++];
            data.Display = lines[index++];
            data.StrCreateTime = lines[index++];

            return data;
        }
    }
}

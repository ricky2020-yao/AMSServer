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
    /// CreateTime:2014年11月13日
    /// Description:账单详情的返回结果类
    /// </summary>
    public class BillDetailViewData
    {
        /// <summary>
        /// 帐单编号（自增列）
        /// </summary>
        public long BillID { get; set; }

        /// <summary>
        /// 业务编号[关联Business表]
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 帐单类型（1、普通帐单 2、调整帐单 3、提前清贷帐单 4、坏帐帐单）
        /// </summary>
        public byte BillType { get; set; }

        /// <summary>
        /// 帐单类型名称
        /// </summary>
        public string StrBillType { get; set; }

        /// <summary>
        /// 帐单状态（1、未付款 2、部分付款 3、全额付款）
        /// </summary>
        public byte BillStatus { get; set; }

        /// <summary>
        /// 帐单状态名称
        /// </summary>
        public string StrBillStatus { get; set; }

        /// <summary>
        /// 帐单月名称
        /// </summary>
        public string BillMonth { get; set; }

        /// <summary>
        /// 结算起始日
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结算结束日
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 最后付款日期
        /// </summary>
        public DateTime LimitTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 账单状态全额支付修改事件
        /// </summary>
        public DateTime? FullPaidTime { get; set; }

        /// <summary>
        /// 是否为当期帐单
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 是否搁置帐单
        /// </summary>
        public bool IsShelve { get; set; }

        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}{1}", BillID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BusinessID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BillType.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", StrBillType, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BillStatus.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", StrBillStatus, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BillMonth, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BeginTime.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", EndTime.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", LimitTime.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);

            if (FullPaidTime.HasValue)
                sb.AppendFormat("{0}{1}", FullPaidTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);

            sb.AppendFormat("{0}{1}", IsCurrent.ToString(), WebServiceConst.Separater_1);
            sb.Append(IsShelve.ToString());

            return sb.ToString();
        }

        /// <summary>
        /// 解析成实体类
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static BillDetailViewData ToEntity(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return null;

            string[] lines = strValue.Split(
                WebServiceConst.Separater_1.ToArray(), StringSplitOptions.None);

            PropertyInfo[] proInfos = typeof(BillDetailViewData).GetProperties();

            if (lines == null || proInfos == null || lines.Length != proInfos.Length)
                return null;

            int index = 0;
            var data = new BillDetailViewData();
            long billID = 0;
            int businessID = 0;
            byte billType = 0;
            byte billStatus = 0;
            DateTime beginTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            DateTime limitTime = DateTime.Now;
            DateTime createTime = DateTime.Now;
            DateTime fullPaidTime = DateTime.Now;
            bool isCurrent = false;
            bool isShelve = false;

            long.TryParse(lines[index++], out billID);
            data.BillID = billID;

            int.TryParse(lines[index++], out businessID);
            data.BusinessID = businessID;

            byte.TryParse(lines[index++], out billType);
            data.BillType = billType;

            data.StrBillType = lines[index++];

            byte.TryParse(lines[index++], out billStatus);
            data.BillStatus = billStatus;

            data.StrBillStatus = lines[index++];
            data.BillMonth = lines[index++];

            DateTime.TryParse(lines[index++], out beginTime);
            data.BeginTime = beginTime;
            DateTime.TryParse(lines[index++], out endTime);
            data.EndTime = endTime;
            DateTime.TryParse(lines[index++], out limitTime);
            data.LimitTime = limitTime;
            DateTime.TryParse(lines[index++], out createTime);
            data.CreateTime = createTime;

            if (DateTime.TryParse(lines[index++], out fullPaidTime))
                data.FullPaidTime = fullPaidTime;

            bool.TryParse(lines[index++], out isCurrent);
            data.IsCurrent = isCurrent;
            bool.TryParse(lines[index++], out isShelve);
            data.IsShelve = isShelve;

            return data;
        }
    }
}

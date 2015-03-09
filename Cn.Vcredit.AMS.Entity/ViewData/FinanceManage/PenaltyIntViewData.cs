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
    /// Description:罚息详情的返回结果类
    /// </summary>
    public class PenaltyIntViewData
    {
        /// <summary>
        /// 主键列
        /// </summary>
        public long PenaltyIntID { get; set; }

        /// <summary>
        /// 业务号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 原因帐单
        /// </summary>
        public long ReasonID { get; set; }

        /// <summary>
        /// 原因科目
        /// </summary>
        public long ReasonItemID { get; set; }

        /// <summary>
        /// 所属帐单
        /// </summary>
        public long ToBillID { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 是否搁置
        /// </summary>
        public bool IsShelve { get; set; }

        /// <summary>
        /// 创建帐单
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}{1}", PenaltyIntID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BusinessID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ReasonID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ReasonItemID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ToBillID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", Amount.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", IsShelve.ToString(), WebServiceConst.Separater_1);
            sb.Append(CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));

            return sb.ToString();
        }

        /// <summary>
        /// 解析成实体类
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static PenaltyIntViewData ToEntity(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return null;

            string[] lines = strValue.Split(
                WebServiceConst.Separater_1.ToArray(), StringSplitOptions.None);

            PropertyInfo[] proInfos = typeof(PenaltyIntViewData).GetProperties();

            if (lines == null || proInfos == null || lines.Length != proInfos.Length)
                return null;

            int index = 0;
            var data = new PenaltyIntViewData();
            long penaltyIntID = 0;
            int businessID = 0;
            long reasonID = 0;
            long reasonItemID = 0;
            long toBillID = 0;
            decimal amount = 0;
            bool isShelve = false;
            DateTime createTime = DateTime.Now;

            long.TryParse(lines[index++], out penaltyIntID);
            data.PenaltyIntID = penaltyIntID;

            int.TryParse(lines[index++], out businessID);
            data.BusinessID = businessID;

            long.TryParse(lines[index++], out reasonID);
            data.ReasonID = reasonID;

            long.TryParse(lines[index++], out reasonItemID);
            data.ReasonItemID = reasonItemID;

            long.TryParse(lines[index++], out toBillID);
            data.ToBillID = toBillID;

            decimal.TryParse(lines[index++], out amount);
            data.Amount = amount;

            bool.TryParse(lines[index++], out isShelve);
            data.IsShelve = isShelve;

            DateTime.TryParse(lines[index++], out createTime);
            data.CreateTime = createTime;

            return data;
        }
    }
}

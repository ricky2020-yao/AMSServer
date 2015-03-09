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
    /// Description:代偿卡详情的返回结果类
    /// </summary>
    public class AdaptationCardDetailData
    {
        /// <summary>
        /// ID
        /// </summary>
        public int AdaptationCardID { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string CardUser { get; set; }

        /// <summary>
        /// 银行
        /// </summary>
        public string AdaBankName { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string ValidPath { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? ValidEndTime { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string StrValidEndTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string AdaDesc { get; set; }

        /// <summary>
        /// 附件名
        /// </summary>
        public string ValidName { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string AdaName { get; set; }

        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}{1}", AdaptationCardID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BusinessID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", AdaName, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", CardUser, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", AdaBankName, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", CardNo, WebServiceConst.Separater_1);

            if (ValidEndTime.HasValue)
                sb.AppendFormat("{0}{1}", ValidEndTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);

            sb.AppendFormat("{0}{1}", StrValidEndTime, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ValidName, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ValidPath, WebServiceConst.Separater_1);
            sb.Append(AdaDesc);

            return sb.ToString();
        }

        /// <summary>
        /// 解析成实体类
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static AdaptationCardDetailData ToEntity(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return null;

            string[] lines = strValue.Split(
                WebServiceConst.Separater_1.ToArray(), StringSplitOptions.None);

            PropertyInfo[] proInfos = typeof(AdaptationCardDetailData).GetProperties();

            if (lines == null || proInfos == null || lines.Length != proInfos.Length)
                return null;

            int index = 0;
            var data = new AdaptationCardDetailData();
            int adaCardID = 0;
            int businessID = 0;
            DateTime validEndTime = DateTime.Now;

            int.TryParse(lines[index++], out adaCardID);
            data.AdaptationCardID = adaCardID;

            int.TryParse(lines[index++], out businessID);
            data.BusinessID = businessID;

            data.AdaName = lines[index++];
            data.CardUser = lines[index++];
            data.AdaBankName = lines[index++];
            data.CardNo = lines[index++];

            if (DateTime.TryParse(lines[index++], out validEndTime))
                data.ValidEndTime = validEndTime;

            data.StrValidEndTime = lines[index++];
            data.ValidName = lines[index++];
            data.ValidPath = lines[index++];
            data.AdaDesc = lines[index++];

            return data;
        }
    }
}

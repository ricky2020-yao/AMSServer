using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Cn.Vcredit.AMS.WebService.Service
{
    /// <summary>
    /// BadTransferService 的摘要说明
    /// Author:姚海凡
    /// CreateTime:2014年8月12日
    /// Description:维信WebService服务
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class VcreditService : BaseWebService
    {
        /// <summary>
        /// 维信WebService服务入口
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [WebMethod]
        public string DealClientRequest(string condition)
        {
            // 处理客户端请求
            return DealRequest(condition);
        }

        /// <summary>
        /// 维信WebService服务入口
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] DealClientRequestByte(string condition)
        {
            string result = DealRequest(condition);

            byte[] bytResult = ConvertUtility.CodingToByte(result, 2);
            byte[] returnResult = null;

            // 长度大于2048，要进行压缩。
            if (bytResult != null || bytResult.Length > WebServiceConst.Need_Compress_MinLength)
            {
                byte[] temp
                    = CompressHelper.Compress(EnumCompressType.MemCompress, bytResult);

                returnResult = new byte[temp.Length + 1];
                returnResult[0] = 1;
                Array.Copy(temp, 0, returnResult, 1, temp.Length);
                temp = null;
            }
            else
            {
                returnResult = new byte[bytResult.Length + 1];
                returnResult[0] = 0;
                Array.Copy(bytResult, 0, returnResult, 1, bytResult.Length);
            }

            bytResult = null;
            // 处理客户端请求
            return returnResult;
        }
    }
}

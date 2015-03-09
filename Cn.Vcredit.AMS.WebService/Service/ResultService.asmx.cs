using Cn.Vcredit.AMS.BaseService.Command;
using Cn.Vcredit.AMS.BaseService.Manager;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Cn.Vcredit.AMS.WebService.Service
{
    /// <summary>
    /// ResultService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ResultService : BaseWebService
    {
        /// <summary>
        /// 处理结果回送WebService
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SendResult(string result)
        {
            result = result.Replace("\n", "\r\n");
            if(string.IsNullOrEmpty(result))
                return false;

            ServiceCommand command = new ServiceCommand();
            command.CommandStringToClass(result);

            if (command == null || string.IsNullOrEmpty(command.Guid))
                return false;

            return 
                Singleton<ResultCacheManager<string>>.Instance.AddResult(command.Guid, command.EntityXmlContent);
        }

        /// <summary>
        /// 处理结果回送WebService
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SendByteResult(byte[] bytResult)
        {
            byte[] temp
                   = CompressHelper.DeCompress(EnumCompressType.MemCompress, bytResult);

            string result = ConvertUtility.CodingToString(temp, 2);
            //result = result.Replace("\n", "\r\n");
            if (string.IsNullOrEmpty(result))
                return false;

            ServiceCommand command = new ServiceCommand();
            command.CommandStringToClass(result, false);

            if (command == null || string.IsNullOrEmpty(command.Guid))
                return false;

            return
                Singleton<ResultCacheManager<string>>.Instance.AddResult(command.Guid, command.EntityXmlContent);
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}

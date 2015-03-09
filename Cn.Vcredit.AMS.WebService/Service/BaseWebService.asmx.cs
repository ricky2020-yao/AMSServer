using Cn.Vcredit.AMS.BaseService.Command;
using Cn.Vcredit.AMS.BaseService.Communication;
using Cn.Vcredit.AMS.BaseService.Communication.MSMQ;
using Cn.Vcredit.AMS.BaseService.Manager;
using Cn.Vcredit.AMS.BaseService.Service.Data;
using Cn.Vcredit.AMS.Common.Data;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Common.XmlConfigData;
using Cn.Vcredit.AMS.Controller.Manager;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Cn.Vcredit.AMS.WebService.Service
{
    /// <summary>
    /// BaseWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class BaseWebService : System.Web.Services.WebService
    {
        // 日志记录
        private ILogger m_Logger;

        /// <summary>
        /// 初始化
        /// </summary>
        public BaseWebService()
        {
            m_Logger = LogFactory.CreateLogger(this.GetType());
        }

        /// <summary>
        /// 生成请求命令格式错误的响应消息
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <returns></returns>
        public ResponseEntity GenerateRequestCommandError(RequestEntity requestEntity)
        {
            ResponseEntity responseEntity = new ResponseEntity();

            responseEntity.RequestId = requestEntity.RequestId;
            responseEntity.UserId = requestEntity.UserId;
            responseEntity.CompressType = (int)EnumCompressType.None;
            responseEntity.EncyptionType = (int)EnumEncyptionType.None;
            responseEntity.ResponseStatus = (int)EnumResponseState.RequestCommandError;

            return responseEntity;
        }

        /// <summary>
        /// 处理客户端请求
        /// </summary>
        /// <param name="xmlCondition"></param>
        /// <returns></returns>
        public string DealRequest(string xmlCondition)
        {
            // 请求实体类初始化
            try
            {
                m_Logger.Debug("开始处理客户端请求。");
                m_Logger.Debug("接收的消息。" + xmlCondition);

                RequestEntity requestEntity = new RequestEntity(xmlCondition);

                if (requestEntity == null)
                    return "请求命令格式出错。";

                string resultId = Guid.NewGuid().ToString();
                ServiceCommand command = new ServiceCommand();

                m_Logger.Debug("开始处理客户端请求。" + resultId);
                command.Guid = resultId;
                command.Entity = requestEntity;
                //command.ReceiveId = Singleton<CommunicationControl>.Instance.ServerId;

                m_Logger.Debug("发送命令。");
                //Singleton<MSMQ>.Instance.MessageReceived += Instance_MessageReceived;
                // 发送命令
                Singleton<MSMQClient>.Instance.Send(command);

                m_Logger.Debug("获取计算结果。");
                // 获取计算结果
                string responseEntityString = GetResponseEntity(command);
                m_Logger.Debug("获取计算结果:" + responseEntityString);
                m_Logger.Debug("获取计算结果结束。");

                m_Logger.Debug("处理客户端请求结束。" + resultId);
                return responseEntityString;
            }
            catch (Exception ex)
            {
                m_Logger.Error("处理客户端请求:" + ex.Message);
                m_Logger.Error("处理客户端请求:" + ex.StackTrace);
                return ex.Message;
            }
        }

        /// <summary>
        /// 接收到消息
        /// </summary>
        /// <param name="message"></param>
        void Instance_MessageReceived(ServiceCommand message)
        {
            if (message == null || string.IsNullOrEmpty(message.Guid))
                return;

            Singleton<ResultCacheManager<string>>.Instance.AddResult(message.Guid, message.EntityXmlContent);
        }

        /// <summary>
        /// 获取计算结果
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string GetResponseEntity(ServiceCommand command)
        {
            // 获取计算结果
            ResponseEntity responseEntity = null;

            string resultId = command.Guid;
            RequestEntity requestEntity = command.Entity as RequestEntity;

            bool isTimeOut = false;
            string result
                = Singleton<ResultCacheManager<string>>.Instance
                .GetResponseEntity(resultId, requestEntity.TimeOut, out isTimeOut);

            // 超时
            if (isTimeOut)
            {
                responseEntity = new ResponseEntity();
                responseEntity.RequestId = requestEntity.RequestId;
                responseEntity.UserId = requestEntity.UserId;
                responseEntity.CompressType = (int)EnumCompressType.None;
                responseEntity.EncyptionType = (int)EnumEncyptionType.None;
                responseEntity.ResponseStatus = (int)EnumResponseState.TimeOut;
                result = responseEntity.ClassToCommandString();
            }

            return result;
        }
    }
}

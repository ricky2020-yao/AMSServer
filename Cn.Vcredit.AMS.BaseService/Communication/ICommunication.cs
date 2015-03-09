using Cn.Vcredit.AMS.BaseService.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.Communication
{
    /// <summary>
    /// 处理收到得消息
    /// </summary>
    /// <param name="message">收到的字节消息</param>
    public delegate void MessageReceivedHandler(ServiceCommand message);

    /// <summary>
    /// 断开与服务器的连接
    /// </summary>
    public delegate void DisconnectedHandler();

    /// <summary>
    /// 
    /// </summary>
    public interface ICommunication
    {
        /// <summary>
        /// 事件触发：当从客户端收到消息
        /// </summary>
        event MessageReceivedHandler MessageReceived;

        /// <summary>
        /// 事件触发：断开与客户端的连接
        /// </summary>
        event DisconnectedHandler Disconnected;

        /// <summary>
        /// 获取服务端ID
        /// </summary>
        string HostID { get; }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        void SendMessage(ServiceCommand message);

        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();
    }
}

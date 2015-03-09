using Cn.Vcredit.AMS.BaseService.Command;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.Common.Log;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;

namespace Cn.Vcredit.AMS.BaseService.Communication.MSMQ
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月20日
    /// Description:微软消息服务服务端
    /// </summary>
    public class MSMQServer
    {        
        // 微软消息服务客户端
        private MessageQueue m_MSMQServer;
        // 可接收标识
        private bool m_IsReceived;
        // 接收消息队列
        private ConcurrentQueue<string> m_ReceiveQueue;
        // 日志记录
        private ILogger m_Logger = LogFactory.CreateLogger(typeof(MSMQServer));

        /// <summary>
        /// 事件触发：收到一条消息来自服务器的管道消息
        /// </summary>
        public event MessageReceivedHandler MessageReceived;

        /// <summary>
        /// 初始化
        /// </summary>
        public MSMQServer()
        {
            // 初始化接收消息队列
            m_ReceiveQueue = new ConcurrentQueue<string>();

            m_Logger.Debug("初始化微软消息服务客户端");
            // 初始化微软消息服务客户端
            if (MessageQueue.Exists(Const.MessageQueue_Send_Name))
                m_MSMQServer = new MessageQueue(Const.MessageQueue_Send_Name);
            else
                m_MSMQServer = MessageQueue.Create(Const.MessageQueue_Send_Name);
            m_Logger.Debug("初始化微软消息服务客户端成功");

            m_MSMQServer.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            m_MSMQServer.ReceiveCompleted += MSMQServer_ReceiveCompleted;
            m_MSMQServer.BeginReceive();

            m_IsReceived = true;

            // 接收进程
            Thread threadSend = new Thread(new ThreadStart(DealReceive));
            threadSend.IsBackground = true;
            threadSend.Start();           
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="messageQueueName"></param>
        public MSMQServer(string messageQueueName)
        {
            // 初始化接收消息队列
            m_ReceiveQueue = new ConcurrentQueue<string>();

            m_Logger.Debug("初始化微软消息服务客户端");
            // 初始化微软消息服务客户端
            if (MessageQueue.Exists(messageQueueName))
                m_MSMQServer = new MessageQueue(messageQueueName);
            else
                m_MSMQServer = MessageQueue.Create(messageQueueName);
            m_Logger.Debug("初始化微软消息服务客户端成功");

            m_MSMQServer.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            m_MSMQServer.ReceiveCompleted += MSMQServer_ReceiveCompleted;
            m_MSMQServer.BeginReceive();

            m_IsReceived = true;

            // 接收进程
            Thread threadSend = new Thread(new ThreadStart(DealReceive));
            threadSend.IsBackground = true;
            threadSend.Start();
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MSMQServer_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                m_Logger.Debug("开始接收数据。");
                MessageQueue messageQueue = sender as MessageQueue;
                Message message = messageQueue.EndReceive(e.AsyncResult);
                m_Logger.Debug("接收数据成功。");
                //处理消息
                string str = message.Body.ToString();
                m_Logger.Debug("接收数据加入队列。");
                m_ReceiveQueue.Enqueue(str);

                //继续下一条消息
                messageQueue.BeginReceive();
            }
            catch (Exception ex)
            {
                m_Logger.Error("接收命令" + ex.Message);
                m_Logger.Error("接收命令" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 处理接收的数据
        /// </summary>
        void DealReceive()
        {
            while (m_IsReceived)
            {
                if (m_ReceiveQueue.IsEmpty)
                {
                    Thread.Sleep(50);
                    continue;
                }

                string message = null;
                m_Logger.Debug("开始处理接收的数据。");
                if (m_ReceiveQueue.TryDequeue(out message))
                {
                    ServiceCommand requestMessage = new ServiceCommand();
                    requestMessage.CommandStringToClass(message);

                    m_Logger.Debug("解析接收数据成功。");
                    if (MessageReceived != null
                        && requestMessage != null
                        && !string.IsNullOrEmpty(requestMessage.Guid))
                        MessageReceived(requestMessage);
                }
                m_Logger.Debug("处理接收的数据结束。");

                Thread.Sleep(50);
            }
        }
    }
}

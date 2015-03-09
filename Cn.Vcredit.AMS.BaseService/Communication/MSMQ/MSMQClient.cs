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
    /// Description:微软消息服务客户端
    /// </summary>
    public class MSMQClient
    {
        // 微软消息服务客户端
        private MessageQueue m_MSMQClient;
        // 发送消息队列
        private ConcurrentQueue<ServiceCommand> m_SendQueue;
        // 日志记录
        private ILogger m_Logger = LogFactory.CreateLogger(typeof(MSMQClient));
        // 可发送标识
        private bool m_IsSend;

        /// <summary>
        /// 初始化
        /// </summary>
        public MSMQClient()
        {
            m_Logger.Debug("初始化微软消息服务客户端初始化");
            // 初始化微软消息服务客户端
            if (MessageQueue.Exists(Const.MessageQueue_Send_Name))
                m_MSMQClient = new MessageQueue(Const.MessageQueue_Send_Name);
            else
                m_MSMQClient = MessageQueue.Create(Const.MessageQueue_Send_Name);
            m_Logger.Debug("初始化微软消息服务客户端初始化结束");

            m_MSMQClient.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            // 初始化队列
            m_SendQueue = new ConcurrentQueue<ServiceCommand>();

            m_IsSend = true;

            // 发送进程
            Thread threadSend = new Thread(new ThreadStart(QueueSend));
            threadSend.IsBackground = true;
            threadSend.Start();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="messageQueueName"></param>
        public MSMQClient(string messageQueueName)
        {
            m_Logger.Debug("初始化微软消息服务客户端初始化");
            // 初始化微软消息服务客户端
            if (MessageQueue.Exists(messageQueueName))
                m_MSMQClient = new MessageQueue(messageQueueName);
            else
                m_MSMQClient = MessageQueue.Create(messageQueueName);
            m_Logger.Debug("初始化微软消息服务客户端初始化结束");

            m_MSMQClient.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            // 初始化队列
            m_SendQueue = new ConcurrentQueue<ServiceCommand>();

            m_IsSend = true;

            // 发送进程
            Thread threadSend = new Thread(new ThreadStart(QueueSend));
            threadSend.IsBackground = true;
            threadSend.Start();
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="db">请求内容</param>
        public void Send(ServiceCommand db)
        {
            m_Logger.Debug("发送请求");
            m_SendQueue.Enqueue(db);
            m_Logger.Debug("发送请求结束");
        }

        /// <summary>
        /// 发送队列中的消息
        /// </summary>
        protected void QueueSend()
        {
            while (m_IsSend)
            {
                if (m_SendQueue.IsEmpty)
                {
                    Thread.Sleep(50);
                    continue;
                }

                m_Logger.Debug("开始发送请求");
                ServiceCommand msg = null;
                if (m_SendQueue.TryPeek(out msg))
                {
                    if (!m_MSMQClient.CanWrite)
                        continue;

                    m_MSMQClient.Send(msg.ClassToCommandString());
                    m_SendQueue.TryDequeue(out msg);
                }
                m_Logger.Debug("发送请求结束");

                Thread.Sleep(50);
            }
        }
    }
}

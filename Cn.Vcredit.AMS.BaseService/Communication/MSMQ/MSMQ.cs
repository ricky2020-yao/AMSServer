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
    /// CreateTime:2014年10月15日
    /// Description:微软消息服务
    /// </summary>
    public class MSMQ
    {
        // 微软消息服务客户端
        private MessageQueue m_MSMQ;
        // 可接收标识
        private bool m_IsReceived;
        // 可发送标识
        private bool m_IsSend;
        // 接收消息队列
        private ConcurrentQueue<string> m_ReceiveQueue;
        // 发送消息队列
        private ConcurrentQueue<ServiceCommand> m_SendQueue;
        // 日志记录
        private ILogger m_Logger = LogFactory.CreateLogger(typeof(MSMQ));

        /// <summary>
        /// 事件触发：收到一条消息来自服务器的管道消息
        /// </summary>
        public event MessageReceivedHandler MessageReceived;
        
        /// <summary>
        /// 初始化
        /// </summary>
        public MSMQ()
        {
            // 初始化接收消息队列
            m_ReceiveQueue = new ConcurrentQueue<string>();
            // 初始化消息发送队列
            m_SendQueue = new ConcurrentQueue<ServiceCommand>();

            m_Logger.Debug("初始化微软消息服务");
            // 初始化微软消息服务
            if (MessageQueue.Exists(Const.MessageQueue_Receive_Name))
                m_MSMQ = new MessageQueue(Const.MessageQueue_Receive_Name);
            else
                m_MSMQ = MessageQueue.Create(Const.MessageQueue_Receive_Name);
            m_Logger.Debug("初始化微软消息服务成功");

            m_MSMQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            m_MSMQ.ReceiveCompleted += MSMQServer_ReceiveCompleted;
            m_MSMQ.BeginReceive();

            m_IsReceived = true;
            m_IsSend = true;

            // 发送进程
            Thread threadSend = new Thread(new ThreadStart(QueueSend));
            threadSend.IsBackground = true;
            threadSend.Start();

            // 接收进程
            Thread threadReceive = new Thread(new ThreadStart(DealReceive));
            threadReceive.IsBackground = true;
            threadReceive.Start();           
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="messageQueueName"></param>
        public MSMQ(string messageQueueName)
        {
            // 初始化接收消息队列
            m_ReceiveQueue = new ConcurrentQueue<string>();
            // 初始化消息发送队列
            m_SendQueue = new ConcurrentQueue<ServiceCommand>();

            m_Logger.Debug("初始化微软消息服务");
            // 初始化微软消息服务
            if (MessageQueue.Exists(messageQueueName))
                m_MSMQ = new MessageQueue(messageQueueName);
            else
                m_MSMQ = MessageQueue.Create(messageQueueName);
            m_Logger.Debug("初始化微软消息服务成功");

            m_MSMQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            m_MSMQ.ReceiveCompleted += MSMQServer_ReceiveCompleted;
            m_MSMQ.BeginReceive();

            m_IsReceived = true;
            m_IsSend = true;

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
                    if (!m_MSMQ.CanWrite)
                        continue;

                    m_MSMQ.Send(msg.ClassToCommandString());
                    m_SendQueue.TryDequeue(out msg);
                }
                m_Logger.Debug("发送请求结束");

                Thread.Sleep(50);
            }
        }
    }
}

using Cn.Vcredit.AMS.BaseService.Command;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cn.Vcredit.AMS.BaseService.Communication
{
    /// Author:姚海凡
    /// CreateTime:2014年8月20日
    /// Description:通信控制类
    public class CommunicationControlServer:IDisposable
    {
        // 自己的ID
        private string m_HostId = "";

        // 发送消息队列
        private ConcurrentQueue<ServiceCommand> m_SendQueue;
        // 接收消息类
        private ConcurrentQueue<ServiceCommand> m_ReceiveQueue;
        // 通信窗体
        private ICommunication m_CommunicationForm;

        private bool m_IsSend;
        private bool m_IsReceived;

        /// <summary>
        /// 事件触发：收到一条消息来自服务器的管道消息
        /// </summary>
        public event MessageReceivedHandler MessageReceived;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommunicationControlServer(string hostId)
        {
            m_HostId = hostId;

            m_SendQueue = new ConcurrentQueue<ServiceCommand>();
            m_ReceiveQueue = new ConcurrentQueue<ServiceCommand>();

            m_CommunicationForm = new CommunicationForm(m_HostId);
            m_CommunicationForm.MessageReceived += new MessageReceivedHandler(Received);

            m_IsSend = true;
            m_IsReceived = true;

            Thread tdSend = new Thread(new ThreadStart(QueueSend));
            tdSend.IsBackground = true;
            tdSend.Start();
            tdSend = null;

            Thread tdReceive = new Thread(new ThreadStart(QueueReceive));
            tdReceive.IsBackground = true;
            tdReceive.Start();
            tdReceive = null;
        }

        /// <summary>
        /// 发送队列中的消息
        /// </summary>
        protected void QueueSend()
        {
            while (m_IsSend)
            {
                if (!m_SendQueue.IsEmpty)
                {
                    ServiceCommand msg = null;
                    if (m_SendQueue.TryPeek(out msg))
                    {
                        m_CommunicationForm.SendMessage(msg);
                        m_SendQueue.TryDequeue(out msg);
                    }
                }

                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 接收队列中的消息
        /// </summary>
        protected void QueueReceive()
        {
            while (m_IsReceived)
            {
                if (!m_ReceiveQueue.IsEmpty)
                {
                    ServiceCommand msg = null;
                    // 事件不为为空时
                    if (m_ReceiveQueue.TryDequeue(out msg) && MessageReceived != null)
                        MessageReceived(msg);
                }

                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 状态回调
        /// </summary>
        /// <param name="rs">验证结果</param>
        private void Received(ServiceCommand rs)
        {
            if (rs != null)
            {
                m_ReceiveQueue.Enqueue(rs);
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="db">请求内容</param>
        public void Send(ServiceCommand db)
        {
            m_SendQueue.Enqueue(db);
        }

        /// <summary>
        /// 开始服务
        /// </summary>
        public void Start(){}

        #region 垃圾清理
        private bool IsDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool Disposing)
        {
            if (!IsDisposed)
            {
                if (Disposing)
                {
                    //清理托管资源
                }

                m_SendQueue = null;
                m_ReceiveQueue = null;

                //清理非托管资源
                m_IsSend = false;
                m_IsReceived = false;
                m_CommunicationForm.Disconnect();
            }
            IsDisposed = true;
        }
        #endregion
    }
}

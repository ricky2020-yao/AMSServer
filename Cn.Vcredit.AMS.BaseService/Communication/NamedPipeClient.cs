using Cn.Vcredit.AMS.BaseService.Command;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Cn.Vcredit.AMS.BaseService.Communication
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月20日
    /// Description:命名管道通信客户端控制类
    /// </summary>
    public class NamedPipeClient:IDisposable
    {
        // 命名管道的名称
        private string m_NamedPipeName = "0123456789";
        // 命名管道客户端
        private NamedPipeClientStream m_PipeClient;
        /// <summary>
        /// 事件触发：收到一条消息来自服务器的管道消息
        /// </summary>
        public event MessageReceivedHandler MessageReceived;
        // 发送消息队列
        private ConcurrentQueue<ServiceCommand> m_SendQueue;
        // 接收消息类
        //private ConcurrentQueue<ServiceCommand> m_ReceiveQueue;
        // 发送命令缓存
        //private ResultCacheManager<ServiceCommand> m_SendCommandCaches = null;
        // 结果缓存
        //private ResultCacheManager<ServiceCommand> m_ReceiveCommandCaches = null;

        private bool m_IsSend;
        private bool m_IsReceived;

        /// <summary>
        /// 初始化
        /// </summary>
        public NamedPipeClient()
        {
            m_PipeClient = new NamedPipeClientStream(".", m_NamedPipeName, PipeDirection.InOut
                , PipeOptions.None, TokenImpersonationLevel.Impersonation);
            
            // 初始化队列
            m_SendQueue = new ConcurrentQueue<ServiceCommand>();
            //m_ReceiveQueue = new ConcurrentQueue<ServiceCommand>();

            // 初始化发送缓存
            //m_SendCommandCaches = new ResultCacheManager<ServiceCommand>();
            // 结果缓存
            //m_ReceiveCommandCaches = new ResultCacheManager<ServiceCommand>();

            // 发送进程
            Thread threadSend = new Thread(new ThreadStart(QueueSend));
            threadSend.IsBackground = true;
            threadSend.Start();

            // 接收进程
            //Thread threadReceive = new Thread(new ThreadStart(QueueReceive));
            //threadReceive.IsBackground = true;
            //threadReceive.Start();

            m_IsSend = true;
            m_IsReceived = true;
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
        /// 接收队列中的消息
        /// </summary>
        protected void QueueReceive()
        {
            while (m_IsReceived)
            {
                if (!m_PipeClient.IsConnected)
                {
                    m_PipeClient.Connect();
                    Thread.Sleep(50);
                    continue;
                }

                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(m_PipeClient))
                {
                    int length = 0;
                    if (int.TryParse(sr.ReadLine(), out length))
                    {
                        for (int i = 0; i < length; i++)
                        {
                            sb.Append(sr.ReadLine());
                        }
                    }

                    if (sb.Length > 0)
                    {
                        ServiceCommand msg = new ServiceCommand();
                        msg.CommandStringToClass(sb.ToString());

                        //m_ReceiveCommandCaches.AddResult(msg.Guid, msg);
                        //m_SendCommandCaches.RemoveResult(msg.Guid);

                        // 事件不为为空时
                        //if (MessageReceived != null && msg != null)
                        //    MessageReceived(msg);
                    }
                }

                Thread.Sleep(50);
            }
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

                ServiceCommand msg = null;
                if (m_SendQueue.TryPeek(out msg))
                {
                    if (!m_PipeClient.IsConnected)
                        m_PipeClient.Connect();

                    using (StreamWriter sw = new StreamWriter(m_PipeClient))
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine(msg.ClassToCommandString());
                    }
                    m_SendQueue.TryDequeue(out msg);
                }

                Thread.Sleep(50);
            }
        }

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
                //m_ReceiveQueue = null;

                //清理非托管资源
                m_IsSend = false;
                m_IsReceived = false;

                m_PipeClient.Close();
                m_PipeClient.Dispose();
                m_PipeClient = null;
            }
            IsDisposed = true;
        }
        #endregion
    }
}

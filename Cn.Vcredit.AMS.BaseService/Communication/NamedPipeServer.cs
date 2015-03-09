using Cn.Vcredit.AMS.BaseService.Command;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BaseService.Manager;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.Common.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cn.Vcredit.AMS.BaseService.Communication
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月20日
    /// Description:命名管道通信服务端控制类
    /// </summary>
    public class NamedPipeServer : IDisposable
    {
        // 命名管道的名称
        private string m_NamedPipeName = "0123456789";
        // 命名管道服务端
        private NamedPipeServerStream m_PipeServer;
        /// <summary>
        /// 事件触发：收到一条消息来自服务器的管道消息
        /// </summary>
        public event MessageReceivedHandler MessageReceived;
        // 发送消息队列
        private ConcurrentQueue<ServiceCommand> m_SendQueue;
        // 接收消息类
        //private ConcurrentQueue<ServiceCommand> m_ReceiveQueue;
        // 结果缓存
        private ResultCacheManager<ServiceCommand> m_ReceiveCommandCaches = null;

        private bool m_IsSend;
        private bool m_IsReceived;

        /// <summary>
        /// 初始化
        /// </summary>
        public NamedPipeServer()
        {
            m_PipeServer = new NamedPipeServerStream(m_NamedPipeName, PipeDirection.InOut, 20);
            
            // 初始化队列
            m_SendQueue = new ConcurrentQueue<ServiceCommand>();
            //m_ReceiveQueue = new ConcurrentQueue<ServiceCommand>();
            // 结果缓存
            m_ReceiveCommandCaches = new ResultCacheManager<ServiceCommand>();

            // 发送进程
            //Thread threadSend = new Thread(new ThreadStart(QueueSend));
            //threadSend.IsBackground = true;
            //threadSend.Start();

            // 接收进程
            Thread threadReceive = new Thread(new ThreadStart(QueueReceive));
            threadReceive.IsBackground = true;
            threadReceive.Start();

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
        /// 获取计算结果
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ResponseEntity GetResponseEntity(ServiceCommand command)
        {
            // 获取计算结果
            ResponseEntity responseEntity = null;

            string resultId = command.Guid;
            RequestEntity requestEntity = command.Entity as RequestEntity;

            bool isTimeOut = false;
            ServiceCommand result
                = m_ReceiveCommandCaches.GetResponseEntity(resultId, requestEntity.TimeOut, out isTimeOut);

            // 超时
            if (isTimeOut)
            {
                responseEntity = new ResponseEntity();
                responseEntity.RequestId = requestEntity.RequestId;
                responseEntity.UserId = requestEntity.UserId;
                responseEntity.CompressType = (int)EnumCompressType.None;
                responseEntity.EncyptionType = (int)EnumEncyptionType.None;
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.TimeOut);
            }
            else
            {
                responseEntity = result.Entity as ResponseEntity;
            }

            return responseEntity;
        }

        /// <summary>
        /// 接收队列中的消息
        /// </summary>
        protected void QueueReceive()
        {
            m_PipeServer.WaitForConnection();

            while (m_IsReceived)
            {
                if (!m_PipeServer.IsConnected)
                {
                    Thread.Sleep(50);
                    continue;
                }

                ServiceCommand msg = new ServiceCommand();
                using (StreamReader sr = new StreamReader(m_PipeServer))
                {
                    msg.CommandStringToClass(sr.ReadLine());
                }
                if (msg == null || string.IsNullOrEmpty(msg.Guid))
                    continue;

                m_ReceiveCommandCaches.AddResult(msg.Guid, msg);
                if (MessageReceived != null)
                {
                    // 事件不为为空时
                    if (MessageReceived != null)
                        MessageReceived(msg);                    
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
                    string[] lines = msg.ClassToCommandString()
                        .Split(Const.Command_Field_Separater_ReturnLine.ToCharArray(), StringSplitOptions.None);

                    m_SendQueue.TryDequeue(out msg);

                    if (lines == null
                        || lines.Length == 0)
                        continue;

                    using (StreamWriter sw = new StreamWriter(m_PipeServer))
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine(lines.Length);
                        foreach (string line in lines)
                        {
                            sw.WriteLine(line);
                        }
                    }
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

                m_PipeServer.Disconnect();
                m_PipeServer.Close();
                m_PipeServer.Dispose();
                m_PipeServer = null;
            }
            IsDisposed = true;
        }
        #endregion
    }
}

using Cn.Vcredit.AMS.BaseService.Command;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BaseService.Manager;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cn.Vcredit.AMS.BaseService.Communication
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月20日
    /// Description:通信控制类
    /// </summary>
    public class CommunicationControl : IDisposable
    {
        // 自己的ID
        private string m_HostId = "";
        // 服务器ID 
        private string m_ServerId = "1234567890";

        /// <summary>
        /// 服务器ID 
        /// </summary>
        public string ServerId
        {
            get { return m_ServerId; }
            set { m_ServerId = value; }
        }
        /// <summary>
        /// 日志记录
        /// </summary>
        private ILogger m_Logger = LogFactory.CreateLogger(typeof(CommunicationControl));

        // 发送消息队列
        private ConcurrentQueue<ServiceCommand> m_SendQueue;
        // 接收消息类
        private ConcurrentQueue<ServiceCommand> m_ReceiveQueue;
        // 通信窗体
        private ICommunication m_CommunicationForm;
        // 发送命令缓存
        private ResultCacheManager<ServiceCommand> m_SendCommandCaches = null;
        // 结果缓存
        private ResultCacheManager<ServiceCommand> m_ReceiveCommandCaches = null;

        private bool m_IsSend;
        private bool m_IsReceived;

        /// <summary>
        /// 事件触发：收到一条消息来自服务器的管道消息
        /// </summary>
        public event MessageReceivedHandler MessageReceived;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommunicationControl()
        {
            try
            {
                m_HostId = Guid.NewGuid().ToString();

                m_SendQueue = new ConcurrentQueue<ServiceCommand>();
                m_ReceiveQueue = new ConcurrentQueue<ServiceCommand>();

                m_CommunicationForm = new CommunicationForm(m_HostId);
                m_CommunicationForm.MessageReceived += new MessageReceivedHandler(Received);

                // 初始化发送缓存
                m_SendCommandCaches = new ResultCacheManager<ServiceCommand>();
                // 结果缓存
                m_ReceiveCommandCaches = new ResultCacheManager<ServiceCommand>();

                m_IsSend = true;
                m_IsReceived = true;

                Thread tdSend = new Thread(new ThreadStart(QueueSend));
                tdSend.IsBackground = true;
                tdSend.Start();
                tdSend = null;
            }
            catch (Exception ex)
            {
                m_Logger.Error("初始化CommunicationControl" + ex.Message);
                m_Logger.Error("初始化CommunicationControl" + ex.StackTrace);
            }

            //Thread tdReceive = new Thread(new ThreadStart(QueueReceive));
            //tdReceive.IsBackground = true;
            //tdReceive.Start();
            //tdReceive = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommunicationControl(string hostId)
        {
            try
            {
                m_HostId = hostId;

                m_SendQueue = new ConcurrentQueue<ServiceCommand>();
                m_ReceiveQueue = new ConcurrentQueue<ServiceCommand>();

                m_CommunicationForm = new CommunicationForm(m_HostId);
                m_CommunicationForm.MessageReceived += new MessageReceivedHandler(Received);

                // 初始化发送缓存
                m_SendCommandCaches = new ResultCacheManager<ServiceCommand>();
                // 结果缓存
                m_ReceiveCommandCaches = new ResultCacheManager<ServiceCommand>();

                m_IsSend = true;
                m_IsReceived = true;

                Thread tdSend = new Thread(new ThreadStart(QueueSend));
                tdSend.IsBackground = true;
                tdSend.Start();
                tdSend = null;
            }
            catch (Exception ex)
            {
                m_Logger.Error("初始化CommunicationControl" + ex.Message);
                m_Logger.Error("初始化CommunicationControl" + ex.StackTrace);
            }

            //Thread tdReceive = new Thread(new ThreadStart(QueueReceive));
            //tdReceive.IsBackground = true;
            //tdReceive.Start();
            //tdReceive = null;
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
                        m_Logger.Debug("入队发送请求（QueueSend）,Guid：" + msg.Guid);
                        m_CommunicationForm.SendMessage(msg);
                        m_Logger.Debug("入队发送请求（QueueSend）,Guid：" + msg.Guid);
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
                    if (m_ReceiveQueue.TryDequeue(out msg))
                        MessageReceived(msg);

                    // 事件不为为空时
                    if (MessageReceived != null)
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
                m_ReceiveCommandCaches.AddResult(rs.Guid, rs);
                //m_ReceiveQueue.Enqueue(rs);
                m_SendCommandCaches.RemoveResult(rs.Guid);
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="db">请求内容</param>
        public void Send(ServiceCommand db)
        {
            m_Logger.Debug("入队发送请求（Send）,Guid：" + db.Guid);
            m_SendQueue.Enqueue(db);
            m_Logger.Debug("入队发送请求结束（Send）,Guid：" + db.Guid);
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

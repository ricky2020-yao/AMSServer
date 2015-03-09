using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BaseService.Manager;
using Cn.Vcredit.AMS.BaseService.Service.Data;
using Cn.Vcredit.AMS.BaseService.Service.Interface;
using Cn.Vcredit.AMS.Common;
using Cn.Vcredit.AMS.Common.Data;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using ExportLogHandler = Cn.Vcredit.AMS.Common.Define.DelegataDefine.ExportLogHandler;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cn.Vcredit.AMS.Controller.Manager
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description:优先级队列管理类
    /// </summary>
    public class QueueManager
    {
        #region 内部变量
        /// <summary>
        /// 定义的优先级队列
        /// </summary>
        private static ConcurrentDictionary<int, RequestQueue<ServiceData>>  m_PriorityQueue = null;

        /// <summary>
        /// 日志记录
        /// </summary>
        private ILogger m_Logger = null;
        #endregion

        /// <summary>
        /// 日志信息画面输出触发
        /// </summary>
        public event ExportLogHandler LogExport;
        /// <summary>
        /// 日志输出
        /// </summary>
        /// <param name="logMessage"></param>
        private void ExportLog(string logMessage)
        {
            ServiceUtility.ExportLog(m_Logger, LogExport, logMessage);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueueManager()
        {
            // 日志初始化
            m_Logger = LogFactory.CreateLogger(typeof(QueueManager));

            // 初始化队列
            m_PriorityQueue = new ConcurrentDictionary<int, RequestQueue<ServiceData>>();
            for (int i = 1; i <= 5; i++)
            {
                m_PriorityQueue.TryAdd(i, new RequestQueue<ServiceData>(i));
            }

            // 启动监听队列
            Thread thread = new Thread(new ThreadStart(Run));
            thread.IsBackground = true;
            thread.Start();
        }

        #region 对外方法
        /// <summary>
        /// 添加到队列
        /// </summary>
        /// <param name="command"></param>
        public bool Enqueue(ServiceData command)
        {
            m_Logger.Debug(string.Format("服务入队。\r\nGuid：{0}。\r\n服务名称：{1}"
                , command.Command.Guid, command.Command.ServiceFullName));

            try
            {
                RequestQueue<ServiceData> queue = null;
                if(m_PriorityQueue.TryGetValue(command.Command.Priority, out queue))
                {
                    queue.Enqueue(command);
                    m_Logger.Debug("服务入队结束。");
                    return true;
                }
            }
            catch (Exception ex)
            {
                m_Logger.Debug("服务入队异常。");
                m_Logger.Fatal(string.Concat("服务入队 异常: ", ex.Message));
                m_Logger.Fatal(string.Concat("服务入队 异常: ", ex.StackTrace));
            }

            m_Logger.Debug("服务入队失败。");
            return false;
        }

        /// <summary>
        /// 判断队列是否空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            foreach (var queue in m_PriorityQueue.Values)
            {
                if (!queue.IsEmpty())
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <returns></returns>
        public ServiceData Dequeue()
        {
            ServiceData command = null;

            try
            {
                if (!IsEmpty())
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        RequestQueue<ServiceData> queue = null;
                        if (m_PriorityQueue.TryGetValue(i, out queue))
                        {
                            if (!queue.IsEmpty())
                                command = queue.Dequeue();

                            if (command != null)
                            {
                                m_Logger.Debug(string.Format("服务出队。\r\nGuid：{0}。\r\n服务名称：{1}"
                                    , command.Command.Guid, command.Command.ServiceFullName));
                                return command;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m_Logger.Debug("服务出队异常。");
                m_Logger.Fatal(string.Concat("服务出队: ", ex.Message));
                m_Logger.Fatal(string.Concat("服务出队: ", ex.StackTrace));
            }

            return command;
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Run()
        {
            while (true)
            {
                Thread.Sleep(200);

                // 请求出队
                ServiceData data = Singleton<QueueManager>.Instance.Dequeue();
                if (data == null)
                    continue;

                // 获取服务
                IService service = Singleton<ServiceManager>.Instance.GetService(data.Command);
                if (service == null)
                {
                    ServiceUtility.ExportLog(m_Logger, LogExport
                        , "服务不存在，服务名称为：" + data.Command.ServiceFullName);

                    RequestEntity requestEntity = data.Command.Entity as RequestEntity;
                    ResponseEntity responseEntity = new ResponseEntity();

                    responseEntity.RequestId = requestEntity.RequestId;
                    responseEntity.UserId = requestEntity.UserId;
                    responseEntity.CompressType = (int)EnumCompressType.None;
                    responseEntity.EncyptionType = (int)EnumEncyptionType.None;

                    ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoService);

                    //放入字典
                    Singleton<ResultCacheManager<ResponseEntity>>.Instance
                        .AddResult(data.Command.Guid, responseEntity);
                    continue;
                }

                if (!service.IsAddLogExport)
                {
                    service.LogExport += ExportLog;
                    service.IsAddLogExport = true;
                }
                // 调用命令
                Singleton<ThreadPoolManager>.Instance.DoWorkInThread(service.Execute, (object)data);
            }
        }
        #endregion
    }
}

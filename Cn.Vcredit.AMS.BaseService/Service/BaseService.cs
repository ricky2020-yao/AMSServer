using Cn.Vcredit.AMS.BaseService.Command;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BaseService.Manager;
using Cn.Vcredit.AMS.BaseService.Service.Data;
using Cn.Vcredit.AMS.BaseService.Service.Interface;
using Cn.Vcredit.AMS.Common.Data;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using ExportLogHandler = Cn.Vcredit.AMS.Common.Define.DelegataDefine.ExportLogHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cn.Vcredit.Common.Tools;

namespace Cn.Vcredit.AMS.BaseService.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月12日
    /// Description:服务基类
    /// </summary>
    public abstract class BaseService : IService
    {
        #region 内部变量
        /// <summary>
        /// 日志记录
        /// </summary>
        protected ILogger m_Logger;
        #endregion

        /// <summary>
        /// 日志信息画面输出触发
        /// </summary>
        public event ExportLogHandler LogExport;

        /// <summary>
        /// 是否已经添加日志输出委托
        /// </summary>
        public bool IsAddLogExport { get; set; }

        /// <summary>
        /// 各自处理
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected virtual void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity) { }

        /// <summary>
        /// 日志输出
        /// </summary>
        /// <param name="message"></param>
        private void ExportLog(string message)
        {
            //m_Logger.Debug(message);

            //if (LogExport != null)
            //    LogExport(message);
        }

        /// <summary>
        /// 输出服务类信息
        /// </summary>
        private void ExportServiceInfo()
        {
            ExportLog(ServiceUtility.ToDescription(this.GetType()));
        }

        /// <summary>
        /// 输出异常信息
        /// </summary>
        /// <param name="responseEntity"></param>
        /// <param name="ex"></param>
        private void ExportExceptionLog(ResponseEntity responseEntity, Exception ex)
        {
            if (responseEntity == null) return;
            if (ex == null) return;

            string description = ServiceUtility.ToDescription(this.GetType()) + "异常:";
            string errorMessage = string.Format("{0}{1}", description, ex.Message.ToString());

            ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, errorMessage);
            ExportLog(errorMessage);
            m_Logger.Error(description + ex.StackTrace.ToString());
            ExportLog(description + ex.StackTrace.ToString());
        }

        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="serviceData"></param>
        public void Execute(object serviceData)
        {
            m_Logger = LogFactory.CreateLogger(this.GetType());

            try
            {
                ResponseEntity responseEntity = new ResponseEntity();
                ServiceData data = serviceData as ServiceData;
                if (data == null)
                {
                    ExportLog("服务端传输数据错误。");
                    return;
                }

                ServiceCommand command = data.Command;
                if (command == null)
                {
                    ExportLog("请求命令格式出错。");
                    return;
                }

                RequestEntity requestEntity = command.Entity as RequestEntity;
                if (requestEntity == null)
                {
                    ExportLog("请求命令格式出错。");
                    ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);
                    return;
                }

                // 请求ID
                responseEntity.RequestId = requestEntity.RequestId;
                // 用户名
                responseEntity.UserId = requestEntity.UserId;

                // 各自处理
                ExportLog("开始处理请求。");
                ExportServiceInfo();
                // 记录执行时间类
                using (StopWatcherAuto auto = new StopWatcherAuto())
                {
                    try
                    {
                        DoExecute(requestEntity, responseEntity);
                    }
                    catch (Exception ex)
                    {
                        // 输出异常日志
                        ExportExceptionLog(responseEntity, ex);
                    }
                }
                ExportLog("处理请求结束。");

                //放入字典
                Singleton<ResultCacheManager<ResponseEntity>>.Instance.AddResult(command.Guid, responseEntity);
            }
            catch (Exception ex)
            {
                m_Logger.Fatal(this.GetType().Name, ex);
            }
        }
    }
}

using Cn.Vcredit.AMS.Common.Command;
using Cn.Vcredit.AMS.Common.Data;
using Cn.Vcredit.AMS.Common.Entity;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Common.Service.Data;
using Cn.Vcredit.AMS.Common.Service.Interface;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.Service
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
        /// 各自处理
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected virtual void DoExecute(RequestEntity requestEntity,ResponseEntity responseEntity){}

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
                    return;

                ServiceCommand command = data.Command;
                if (command == null)
                    return;

                RequestEntity requestEntity = command.Entity;
                if (requestEntity == null)
                {
                    responseEntity.ResponseStatus = (int)EnumResponseState.RequestCommandError;
                    return;
                }

                // 请求ID
                responseEntity.RequestId = requestEntity.RequestId;
                // 用户名
                responseEntity.UserId = requestEntity.UserId;

                // 各自处理
                DoExecute(requestEntity, responseEntity);

                //放入字典
                Singleton<ResultCacheManager>.Instance.AddResult(command.Guid, responseEntity);
            }
            catch (Exception ex)
            {
                m_Logger.Fatal(this.GetType().Name, ex);
            }
        }
    }
}

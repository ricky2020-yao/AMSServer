using Cn.Vcredit.AMS.Common;
using Cn.Vcredit.AMS.Common.Command;
using Cn.Vcredit.AMS.Common.Data;
using Cn.Vcredit.AMS.Common.Entity;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Common.Service.Data;
using Cn.Vcredit.AMS.Common.Service.Interface;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cn.Vcredit.AMS.Logic.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description:测试用服务
    /// </summary>
    public class NoneService : IService
    {
        #region 内部变量
        /// <summary>
        /// 日志记录
        /// </summary>
        private ILogger m_Logger = LogFactory.CreateLogger(typeof(NoneService));
        #endregion

        /// <summary>
        /// 服务执行入口
        /// </summary>
        /// <param name="serviceData">服务传递的数据</param>
        /// <returns></returns>
        public void Execute(object serviceData)
        {
            ServiceData data = serviceData as ServiceData;
            if (data == null)
                return;

            ServiceCommand baseCommand = data.Command;
            RequestEntity entity = baseCommand.Entity;

            ResponseEntity responseEntity = new ResponseEntity();
            responseEntity.RequestId = entity.RequestId;
            responseEntity.UserId = entity.UserId;
            responseEntity.CompressType = (int)EnumCompressType.None;
            responseEntity.EncyptionType = (int)EnumEncyptionType.None;
            responseEntity.ResponseStatus = (int)EnumResponseState.Success;

            m_Logger.Info("NoneService Executed Begin:" + baseCommand.Guid);
            Thread.Sleep(2000);
            m_Logger.Info("NoneService Executed End:" + baseCommand.Guid);

            Singleton<ResultCacheManager>.Instance.AddResult(baseCommand.Guid, responseEntity);
        }
    }
}

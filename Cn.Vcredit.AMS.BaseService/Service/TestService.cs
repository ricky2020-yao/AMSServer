using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.Service
{
    public class TestService:BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            string errorMessage = "";

            // 记录执行时间类
            using (StopWatcherAuto auto = new StopWatcherAuto())
            {
                try
                {
                    ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                    responseEntity.Results = null;
                }
                catch (Exception ex)
                {
                    ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others, "时间轴设置数据初始化失败。" + ex.Message.ToString());
                    m_Logger.Error("时间轴设置数据初始化失败:" + ex.Message.ToString());
                    m_Logger.Error("时间轴设置数据初始化失败:" + ex.StackTrace.ToString());
                }
                finally
                {
                }
            }
        }
    }
}

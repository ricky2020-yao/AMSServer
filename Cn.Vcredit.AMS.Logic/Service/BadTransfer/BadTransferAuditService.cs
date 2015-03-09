using Cn.Vcredit.AMS.Common.Data.Filter;
using Cn.Vcredit.AMS.Common.Entity;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Logic.Common;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.Service.BadTransfer
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:坏账清贷审核通过服务
    /// </summary>
    public class BadTransferAuditService:BaseService
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
                    // 定义接收客户端参数的变量
                    IDictionary<string, string> paraDict = requestEntity.Parameters;
                    BadTransferFilter filter = LogicUtility.ConvertToFilterFromDict<BadTransferFilter>(paraDict);
                    
                    //var responseResult = new BadTransferResponseResult<BadTransferDetailResultData>();
                    //responseResult.LstResult = detailResults;

                    responseEntity.ResponseStatus = (int)EnumResponseState.Success;
                    //responseEntity.Results = responseResult;
                }
                catch (Exception ex)
                {
                    errorMessage = "";
                    responseEntity.ResponseStatus = (int)EnumResponseState.Others;
                    responseEntity.ResponseMessage = "坏账清贷审核通过失败。" + ex.Message.ToString();
                    m_Logger.Error("坏账清贷审核通过失败:" + ex.Message.ToString());
                    m_Logger.Error("坏账清贷审核通过失败:" + ex.StackTrace.ToString());
                }
                finally
                {
                    if (errorMessage.Length > 0)
                    {
                        responseEntity.ResponseStatus = (int)EnumResponseState.Others;
                        responseEntity.ResponseMessage = errorMessage;
                    }
                }
            }
        }
    }
}

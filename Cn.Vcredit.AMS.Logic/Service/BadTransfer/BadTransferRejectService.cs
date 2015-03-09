using Cn.Vcredit.AMS.Common.Data.Filter;
using Cn.Vcredit.AMS.Common.Entity;
using Cn.Vcredit.AMS.Common.Entity.ReponseResult;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Logic.Common;
using Cn.Vcredit.Common.Patterns;
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
    /// Description:坏账清贷拒绝服务
    /// </summary>
    public class BadTransferRejectService:BaseService
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

                    // 坏账清贷拒绝
                    int count = Singleton<BadTransferDal>.Instance.RejectBadTransfer(
                        filter.CloanApplyID, requestEntity.UserId);

                    //var responseResult = new BadTransferResponseResult<BadTransferDetailResultData>();
                    //responseResult.LstResult = detailResults;

                    if (count > 0)
                        responseEntity.ResponseStatus = (int)EnumResponseState.Success;
                    else
                        responseEntity.ResponseStatus = (int)EnumResponseState.Others;
                }
                catch (Exception ex)
                {
                    errorMessage = "";
                    responseEntity.ResponseStatus = (int)EnumResponseState.Others;
                    responseEntity.ResponseMessage = "坏账清贷拒绝失败。" + ex.Message.ToString();
                    m_Logger.Error("坏账清贷拒绝失败:" + ex.Message.ToString());
                    m_Logger.Error("坏账清贷拒绝失败:" + ex.StackTrace.ToString());
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

using Cn.Vcredit.AMS.Common.Data;
using Cn.Vcredit.AMS.Common.Data.Filter;
using Cn.Vcredit.AMS.Common.Data.ViewData;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Logic.Common;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月12日
    /// Description:坏账清贷检索服务
    /// </summary>
    public class BadTransferSearchService : BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            //传递的参数
            IDictionary<string, string> dicParamteters = requestEntity.Parameters;

            string errorMessage = "";
            // 记录执行时间类
            using (StopWatcherAuto auto = new StopWatcherAuto())
            {
                try
                {
                    // 定义接收客户端参数的变量
                    IDictionary<string, string> paraDict = requestEntity.Parameters;
                    BadTransferFilter filter = LogicUtility.ConvertToFilterFromDict<BadTransferFilter>(paraDict);

                    // 根据检索条件获取坏账清贷信息
                    var result = Singleton<BadTransferDal>.Instance.GetBadTransferInfor(
                        filter.BusinessID, filter.ContractNo, filter.CustomerName, filter.IdenNo);

                    if (result == null || result.Count == 0)
                    {
                        responseEntity.ResponseStatus = (int)EnumResponseState.NoResult;
                        responseEntity.ResponseMessage = "无申请记录";
                        m_Logger.Info("无申请记录。");
                    }
                    else
                    {
                        var responseResult = new ResponseListResult<BadTransferSearchData>();
                        responseResult.LstResult = result;

                        responseEntity.ResponseStatus = (int)EnumResponseState.Success;
                        responseEntity.Results = responseResult;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = "";
                    responseEntity.ResponseStatus = (int)EnumResponseState.Others;
                    responseEntity.ResponseMessage = "检索申请记录失败。" + ex.Message.ToString();
                    m_Logger.Error("检索申请记录失败:" + ex.Message.ToString());
                    m_Logger.Error("检索申请记录失败:" + ex.StackTrace.ToString());
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

using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.SavingCardChangeService.BLL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.SavingCardChangeService.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月29日
    /// Description:储蓄卡修改提交修改服务
    /// </summary>
    [Description("储蓄卡修改提交修改服务")]
    public class SavingCardChangeSubmitService:BaseService.Service.BaseService
    {
        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <param name="responseEntity"></param>
        protected override void DoExecute(RequestEntity requestEntity, ResponseEntity responseEntity)
        {
            // 定义接收客户端参数的变量
            SavingCardChangeUpdateFilter filter
                = ServiceUtility.ConvertToFilterFromDict<SavingCardChangeUpdateFilter>(requestEntity.Parameters);

            // 储蓄卡修改提交修改
            Singleton<SavingCardChangeSubmitBLL>.Instance.UpdateData(filter, responseEntity);
        }
    }
}

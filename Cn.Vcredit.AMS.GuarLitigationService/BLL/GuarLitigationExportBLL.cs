﻿using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.AMS.Entity.ViewData.BillDun;
using Cn.Vcredit.AMS.GuarLitigationService.DAL;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.GuarLitigationService.BLL
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年10月21日
    /// Description:担保和诉讼设置导出逻辑操作类
    /// </summary>
    public class GuarLitigationExportBLL : BaseBLL
    {
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="responseEntity"></param>
        public virtual void Search(BusinessGuaranteeFilter filter, ResponseEntity responseEntity)
        {
            var reportDetailList = Singleton<GuarLitigationExportDAL<BusinessGuaranteeViewData>>
                .Instance.SearchData(filter);

            if (reportDetailList == null || reportDetailList.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据！。");
            }
            else
            {
                foreach (var guarantee in reportDetailList)
                {
                    guarantee.LendingSideKeyName = Singleton<RedisEnumOperatorBLL>.Instance
                        .GetRedisData(RedisEnumOperatorBLL.HashId_LendingGroup_4, guarantee.LendingSideKey).Name;
                }

                // 设置输出文件
                SetExportFile(filter, reportDetailList, responseEntity, "客户列表");
            }
        }
    }
}

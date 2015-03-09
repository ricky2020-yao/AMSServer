using Cn.Vcredit.AMS.BadTransferService.DAL;
using Cn.Vcredit.AMS.BaseService.BLL;
using Cn.Vcredit.AMS.BaseService.Entity;
using Cn.Vcredit.AMS.Common.Data.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BadTransferService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月19日
    /// Description:坏账清贷审核通过逻辑处理层
    /// </summary>
    public class BadTransferAuditBLL : BaseUpdateBLL<BadTransferAuditDAL>
    {
        /// <summary>
        /// 获取更新数据的件数
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        public override void Update(BaseFilter baseFilter, ResponseEntity responseEntity)
        {

        }
    }
}

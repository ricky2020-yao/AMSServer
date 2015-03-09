using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月18日
    /// Description:更新数据处理基类
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public class BaseUpdateBLL<T> : BaseBLL
        where T : BaseUpdateDAL, new()
    {
        /// <summary>
        /// 根据条件更新数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        public virtual void UpdateData(BaseFilter baseFilter
            , ResponseEntity responseEntity)
        {
            T dal = new T();    
            // 更新数据
            int count = dal.Update(baseFilter);

            if (count > 0)
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
            else
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Others);
        }
    }
}

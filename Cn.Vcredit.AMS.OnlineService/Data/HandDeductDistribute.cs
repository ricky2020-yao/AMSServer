using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cn.Vcredit.AMS.OnlineService.Data
{
    public class HandDeductDistribute
    {
        /// <summary>
        /// 分配的Guid
        /// </summary>
        public Guid DistributeGuid { get; set; }

        /// <summary>
        /// 结果：1、成功 2、失败
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// 结果描述
        /// </summary>
        public string ResultDesc { get; set; }

        /// <summary>
        /// 状态，0：正在处理，1：处理结束
        /// </summary>
        public int Status { get; set; }
    }
}
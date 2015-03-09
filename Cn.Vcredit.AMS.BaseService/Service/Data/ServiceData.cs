using Cn.Vcredit.AMS.BaseService.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.Service.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description:调用服务，传递的数据结构
    /// </summary>
    public class ServiceData
    {
        /// <summary>
        /// 客户端传递的命令
        /// </summary>
        public ServiceCommand Command { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExportLogHandler = Cn.Vcredit.AMS.Common.Define.DelegataDefine.ExportLogHandler;

namespace Cn.Vcredit.AMS.BaseService.Service.Interface
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description:所有服务的基接口
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// 日志信息画面输出触发
        /// </summary>
        event ExportLogHandler LogExport;

        /// <summary>
        /// 是否已经添加日志输出委托
        /// </summary>
        bool IsAddLogExport { get; set; }

        /// <summary>
        /// 命令执行的入口
        /// </summary>
        /// <param name="serviceData">服务传递的数据</param>
        /// <returns></returns>
        void Execute(object serviceData);
    }
}

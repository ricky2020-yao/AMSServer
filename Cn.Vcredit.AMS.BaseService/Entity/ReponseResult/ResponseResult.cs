using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.Entity.ReponseResult
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月30日
    /// Description:请求响应结果的实体基类
    /// </summary>
    public abstract class ResponseResult
    {
        /// <summary>
        /// 命令实体类转换成字符串
        /// </summary>
        /// <returns></returns>
        public abstract string ClassToCommandString();

        /// <summary>
        /// 命令字符串转换实体类
        /// </summary>
        /// <param name="command"></param>
        public abstract void CommandStringToClass(string command);
    }
}

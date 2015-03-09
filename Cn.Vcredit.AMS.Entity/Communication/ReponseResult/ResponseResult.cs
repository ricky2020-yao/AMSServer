using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Communication.ReponseResult
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月30日
    /// Description:请求响应结果的实体基类
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 头部信息
        /// </summary>
        public ResponseHead ResponseHead { get; set; }

        /// <summary>
        /// 命令实体类转换成字符串
        /// </summary>
        /// <returns></returns>
        public virtual string ClassToCommandString()
        {
            return "";
        }

        /// <summary>
        /// 命令字符串转换实体类
        /// </summary>
        /// <param name="command"></param>
        public virtual void CommandStringToClass(string command)
        {
        }
    }
}

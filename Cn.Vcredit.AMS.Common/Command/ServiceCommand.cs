using Cn.Vcredit.AMS.Common.Entity;
using Cn.Vcredit.AMS.Common.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Command
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description:命令格式基类，根据命令的格式，解析到实体类
    /// </summary>
    public class ServiceCommand
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ServiceCommand()
        { }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="command"></param>
        public ServiceCommand(string command)
        {
            // 命令字符串转换实体类
            CommandStringToClass(command);
        }

        /// <summary>
        /// 服务端分配的请求标识，生存周期内唯一
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 服务类型。用户调用的具体服务
        /// 路由服务使用
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 命令的优先级，值越小优先级越高
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 命令到达服务器端的时间，精确到毫秒，格式yyyyMMddHHmmssfff。
        /// 如果Priority优先级相同，则根据此值进行比较，先到先处理。
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 客户端请求的实体类
        /// </summary>
        public RequestEntity Entity { get; set; }

        /// <summary>
        /// 命令实体类转换成字符串
        /// </summary>
        /// <returns></returns>
        public virtual string ClassToCommandString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{4}{1}{4}{2}{4}{3}{4}"
                 , Guid, ServiceName, Priority, CreateTime, Const.Command_Field_Separater_ReturnLine);

            if (Entity != null)
            {
                sb.Append(Entity.EntityToCommandString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// 命令字符串转换实体类
        /// </summary>
        /// <param name="command"></param>
        public virtual int CommandStringToClass(string command)
        {
            try
            {
                int index = 0;
                int length = 0;
                int startIndex = 0;
                int separaterLength = Const.Command_Field_Separater_ReturnLine.Length;

                // Guid
                index = command.IndexOf(Const.Command_Field_Separater_ReturnLine);
                if (index < 0)
                    return -1;
                length = index - startIndex;
                Guid = command.Substring(startIndex, length);;

                // ServiceName
                startIndex = index + separaterLength;
                index = command.IndexOf(Const.Command_Field_Separater_ReturnLine, startIndex);
                if (index < 0)
                    return -1;
                length = index - startIndex;
                ServiceName = command.Substring(startIndex, length);

                string strPriority = "";
                int priority = 0;
                // Priority
                startIndex = index + separaterLength;
                index = command.IndexOf(Const.Command_Field_Separater_ReturnLine, startIndex);
                if (index < 0)
                    return -1;
                length = index - startIndex;
                strPriority = command.Substring(startIndex, length);
                if (int.TryParse(strPriority, out priority))
                    Priority = priority;

                // CreateTime
                startIndex = index + separaterLength;
                index = command.IndexOf(Const.Command_Field_Separater_ReturnLine, startIndex);
                if (index < 0)
                    return -1;
                length = index - startIndex;
                CreateTime = command.Substring(startIndex, length);

                // Entity
                startIndex = index + separaterLength;
                Entity = new RequestEntity(startIndex, command);

                return 0;
            }
            catch (Exception ex)
            {
            }

            return -1;
        }
    }
}

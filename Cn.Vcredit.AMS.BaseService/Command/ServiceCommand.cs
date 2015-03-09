using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Entity.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.Command
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description:命令格式基类，根据命令的格式，解析到实体类
    /// </summary>
    public class ServiceCommand
    {
        #region 公共属性
        /// <summary>
        /// 发送端ID
        /// </summary>
        public string SendId { get; set; }

        /// <summary>
        /// 接收端ID
        /// </summary>
        public string ReceiveId { get; set; }

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
        /// 服务类型。用户调用的具体服务
        /// 路由服务使用
        /// </summary>
        public string ServiceFullName { get; set; }

        /// <summary>
        /// 服务的DLL名称
        /// </summary>
        public string ServiceDLLName { get; set; }

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
        /// 客户端请求的实体类名称，字符串解析用
        /// </summary>
        public string CommunicationEntityName { get; set; }

        /// <summary>
        /// 请求响应结果的实体基类名称，字符串解析用
        /// 请求的场合下，为空
        /// </summary>
        public string ResponseResultName { get; set; }

        /// <summary>
        /// 客户端请求的实体类
        /// </summary>
        public CommunicationEntity Entity { get; set; }

        /// <summary>
        /// 客户端请求的实体类的Xml格式内容
        /// </summary>
        public string EntityXmlContent { get;set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ServiceCommand(){}

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="command"></param>
        public ServiceCommand(string command)
        {
            // 命令字符串转换实体类
            CommandStringToClass(command);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 命令实体类转换成字符串
        /// </summary>
        /// <returns></returns>
        public virtual string ClassToCommandString()
        {
            CommunicationEntityName = Entity.GetType().FullName;
            if (Entity is ResponseEntity)
            {
                ResponseEntity responseEntity = Entity as ResponseEntity;
                if (responseEntity != null && responseEntity.Results != null)
                    ResponseResultName = responseEntity.Results.GetType().FullName;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{4}{1}{4}{2}{4}{3}{4}{5}{4}{6}{4}{7}{4}{8}{4}{9}{4}{10}{4}"
                 , Guid, ServiceName, ServiceFullName, ServiceDLLName, Const.Command_Field_Separater_ReturnLine
                 , Priority, CreateTime
                 , ReceiveId, SendId, CommunicationEntityName, ResponseResultName);

            if (Entity != null)
            {
                sb.Append(Entity.ClassToCommandString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// 命令字符串转换实体类

        /// <param name="command"></param>
        /// <param name="isNeedDeEntity">是否发序列化实体类</param>
        public virtual int CommandStringToClass(string command, bool isNeedDeEntity = true)
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
            Guid = command.Substring(startIndex, length);

            // ServiceName
            startIndex = index + separaterLength;
            index = command.IndexOf(Const.Command_Field_Separater_ReturnLine, startIndex);
            if (index < 0)
                return -1;
            length = index - startIndex;
            ServiceName = command.Substring(startIndex, length);

            // ServiceFullName
            startIndex = index + separaterLength;
            index = command.IndexOf(Const.Command_Field_Separater_ReturnLine, startIndex);
            if (index < 0)
                return -1;
            length = index - startIndex;
            ServiceFullName = command.Substring(startIndex, length);

            // ServiceDLLName
            startIndex = index + separaterLength;
            index = command.IndexOf(Const.Command_Field_Separater_ReturnLine, startIndex);
            if (index < 0)
                return -1;
            length = index - startIndex;
            ServiceDLLName = command.Substring(startIndex, length);

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

            // ReceiveId
            startIndex = index + separaterLength;
            index = command.IndexOf(Const.Command_Field_Separater_ReturnLine, startIndex);
            if (index < 0)
                return -1;
            length = index - startIndex;
            ReceiveId = command.Substring(startIndex, length);

            // SendId
            startIndex = index + separaterLength;
            index = command.IndexOf(Const.Command_Field_Separater_ReturnLine, startIndex);
            if (index < 0)
                return -1;
            length = index - startIndex;
            SendId = command.Substring(startIndex, length);

            // CommunicationEntityName
            startIndex = index + separaterLength;
            index = command.IndexOf(Const.Command_Field_Separater_ReturnLine, startIndex);
            if (index < 0)
                return -1;
            length = index - startIndex;
            CommunicationEntityName = command.Substring(startIndex, length);

            // ResponseResultName
            startIndex = index + separaterLength;
            index = command.IndexOf(Const.Command_Field_Separater_ReturnLine, startIndex);
            if (index < 0)
                return -1;
            length = index - startIndex;
            ResponseResultName = command.Substring(startIndex, length);

            // Entity
            startIndex = index + separaterLength;
            EntityXmlContent = command.Substring(startIndex);

            // 序列化实体类
            if (isNeedDeEntity && !string.IsNullOrEmpty(CommunicationEntityName))
            {
                Assembly assm = Assembly.Load("Cn.Vcredit.AMS.Entity");
                Entity = assm.CreateInstance(CommunicationEntityName) as CommunicationEntity;
                if (Entity != null)
                    Entity.CommandStringToClass(startIndex, command, ResponseResultName);
            }

            return 0;
        }
        #endregion
    }
}

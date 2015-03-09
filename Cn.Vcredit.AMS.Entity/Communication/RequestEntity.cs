using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Cn.Vcredit.AMS.Entity.Communication
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:客户端请求实体类基类
    /// </summary>
    [Serializable]
    public class RequestEntity : CommunicationEntity
    {
        #region 属性

        /// <summary>
        /// 调用服务的标识
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 设置的超时时间，单位毫秒
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// 条件字典
        /// </summary>
        public IDictionary<string, string> Parameters { get; set; }

        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化对象
        /// </summary>
        public RequestEntity()
        {
            TimeOut = 5000;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="command"></param>
        public RequestEntity(string command)
        {
            TimeOut = 5000;
            // 命令字符串转换实体类
            CommandStringToClass(command);
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="command"></param>
        public RequestEntity(int startIndex, string command)
        {
            TimeOut = 5000;
            // 命令字符串转换实体类
            CommandStringToClass(startIndex, command);
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="serviceId"></param>
        /// <param name="userId"></param>
        /// <param name="timeOut"></param>
        /// <param name="compressType"></param>
        /// <param name="encyptionType"></param>
        public RequestEntity(string requestId,
            string serviceId, int userId, int timeOut, EnumCompressType compressType, EnumEncyptionType encyptionType)
        {
            RequestId = requestId;
            ServiceId = serviceId;
            UserId = userId;
            TimeOut = timeOut;
            CompressType = (int)compressType;
            EncyptionType = (int)encyptionType;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="serviceId"></param>
        /// <param name="userId"></param>
        public RequestEntity(string requestId,
            string serviceId, int userId)
            : this(requestId, serviceId, userId, 5000, EnumCompressType.None, EnumEncyptionType.None) { }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="serviceId"></param>
        /// <param name="userId"></param>
        /// <param name="timeOut"></param>
        public RequestEntity(string requestId,
            string serviceId, int userId, int timeOut)
            : this(requestId, serviceId, userId, timeOut, EnumCompressType.None, EnumEncyptionType.None) { }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="serviceId"></param>
        /// <param name="userId"></param>
        /// <param name="timeOut"></param>
        /// <param name="compressType"></param>
        public RequestEntity(string requestId,
            string serviceId, int userId, int timeOut, EnumCompressType compressType)
            : this(requestId, serviceId, userId, timeOut, compressType, EnumEncyptionType.None) { }
        #endregion

        #region 方法
        /// <summary>
        /// 命令实体类转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ClassToCommandString()
        {
            StringBuilder sb = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings();
            //要求缩进 
            settings.Indent = true;
            //省略 XML 声明
            settings.OmitXmlDeclaration = true;
            //设置换行符 
            settings.NewLineChars = Environment.NewLine;

            //初始化XML文档操作类
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteRaw(CommunicationConsts.XMLNode_Encoding);
            writer.WriteRaw(Environment.NewLine);
            writer.WriteStartElement(CommunicationConsts.XMLNode_RequestEntity);
            writer.WriteElementString(CommunicationConsts.XMLNode_RequestId, RequestId);
            writer.WriteElementString(CommunicationConsts.XMLNode_UserId, UserId.ToString());
            writer.WriteElementString(CommunicationConsts.XMLNode_ServiceId, ServiceId);
            writer.WriteElementString(CommunicationConsts.XMLNode_TimeOut, TimeOut.ToString());
            writer.WriteElementString(CommunicationConsts.XMLNode_CompressType, ((int)CompressType).ToString());
            writer.WriteElementString(CommunicationConsts.XMLNode_EncyptionType, ((int)EncyptionType).ToString());

            if (Parameters != null && Parameters.Count > 0)
            {
                writer.WriteStartElement(CommunicationConsts.XMLNode_Parameters);
                foreach (var para in Parameters)
                {
                    writer.WriteElementString(para.Key, para.Value);
                }
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();

            string returnResult = sb.ToString();
            return returnResult;
        }

        /// <summary>
        /// 命令字符串转换实体类
        /// </summary>
        /// <param name="index"></param>
        /// <param name="command"></param>
        public override void CommandStringToClass(string command)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(command);

            XmlNodeList lstNodes = xmlDocument.SelectNodes(CommunicationConsts.XMLNode_RequestEntity);
            XmlNode xmlNode = null;

            Parameters = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

            Type type = this.GetType();
            PropertyInfo[] proInfos = type.GetProperties();

            foreach (XmlNode node in lstNodes)
            {
                foreach (PropertyInfo info in proInfos)
                {
                    if (info.Name.Equals(CommunicationConsts.XMLNode_Parameters))
                    {
                        xmlNode = node.SelectSingleNode(info.Name);
                        if (xmlNode == null || !xmlNode.HasChildNodes)
                            continue;

                        foreach (XmlNode coditionNode in xmlNode.ChildNodes)
                        {
                            Parameters.Add(coditionNode.Name, coditionNode.InnerText);
                        }
                    }
                    else
                    {
                        xmlNode = node.SelectSingleNode(info.Name);
                        if (xmlNode != null)
                        {
                            info.SetValue(this,
                                string.IsNullOrEmpty(xmlNode.InnerText)
                                ? null : Convert.ChangeType(xmlNode.InnerText, info.PropertyType), null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 命令字符串转换实体类
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="command"></param>
        public override void CommandStringToClass(int index, string command)
        {
            if (command.Length <= index)
                return;

            string xmlContent = command.Substring(index);
            CommandStringToClass(xmlContent);
        }
        
        /// <summary>
        /// 命令字符串转换实体类
        /// </summary>
        /// <param name="index"></param>
        /// <param name="command"></param>
        /// <param name="responseResultName"></param>
        public override void CommandStringToClass(int index, string command, string responseResultName)
        {
            if (command.Length <= index)
                return;

            string xmlContent = command.Substring(index);
            CommandStringToClass(xmlContent);
        }
        #endregion
    }
}

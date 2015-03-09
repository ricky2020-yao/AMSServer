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


namespace Cn.Vcredit.AMS.Entity.Communication.RequestSpecialEntity
{
    
    /// <summary>
    /// 增加请求类，以便能传批量数据
    /// </summary>
    [Serializable]
    public class RequestDataEntity<T> : RequestEntity
    {
        #region 属性
        /// <summary>
        /// 需要处理的数据
        /// </summary>
        public List<T> OperationDatas { get; set; }
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

            if(OperationDatas !=null && OperationDatas.Count>0)
            {
                writer.WriteStartElement(CommunicationConsts.XMLNode_OperationDatas);
                foreach(T t in OperationDatas)
                {
                    writer.WriteStartElement(CommunicationConsts.XMLNode_OperationData);

                    writer.WriteEndElement();
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

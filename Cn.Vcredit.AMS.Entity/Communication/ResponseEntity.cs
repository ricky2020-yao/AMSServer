using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Tools;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Cn.Vcredit.AMS.Entity.Communication
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:服务端响应消息实体类
    /// </summary>
    [Serializable]
    public class ResponseEntity : CommunicationEntity
    {
        #region 属性
        /// <summary>
        /// 响应返回的状态
        /// </summary>
        public int ResponseStatus { get; set; }

        /// <summary>
        /// 响应返回的消息
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// 响应返回的结果
        /// </summary>
        public ResponseResult Results { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ResponseEntity() { }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="command"></param>
        public ResponseEntity(string command)
        {
            // 命令字符串转换实体类
            CommandStringToClass(command);
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="command"></param>
        public ResponseEntity(int startIndex, string command)
        {
            // 命令字符串转换实体类
            CommandStringToClass(startIndex, command);
        }
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
            writer.WriteRaw("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            writer.WriteRaw(Environment.NewLine);
            writer.WriteStartElement("ResponseEntity");
            writer.WriteElementString("RequestId", RequestId);
            writer.WriteElementString("UserId", UserId.ToString());
            writer.WriteElementString("CompressType", ((int)CompressType).ToString());
            writer.WriteElementString("EncyptionType", ((int)EncyptionType).ToString());
            writer.WriteElementString("ResponseStatus", ((int)ResponseStatus).ToString());
            writer.WriteElementString("ResponseMessage", ResponseMessage);

            //if (ResponseStatus == (int)EnumResponseState.Others)
            //    writer.WriteElementString("ResponseMessage", ResponseMessage);
            //else
            //    writer.WriteElementString("ResponseMessage", ResponseStatus.ValueToDesc<EnumResponseState>());

            if (Results != null)
            {
                string entityXmlContent = Results.ClassToCommandString();
                if (!string.IsNullOrEmpty(entityXmlContent))
                {
                    using (StringReader stringReader = new StringReader(entityXmlContent))
                    {
                        XmlReader xmlReader = XmlReader.Create(stringReader);
                        writer.WriteNode(xmlReader, false);
                    }
                }
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
        /// <param name="command"></param>
        public override void CommandStringToClass(string command)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(command);

            XmlNodeList lstNodes = xmlDocument.SelectNodes("ResponseEntity");
            XmlNode xmlNode = null;

            // 获取返回的头部信息
            Type type = this.GetType();
            PropertyInfo[] proInfos = type.GetProperties();
            foreach (XmlNode node in lstNodes)
            {
                foreach (PropertyInfo info in proInfos)
                {
                    if (info.Name.Equals("Results"))
                        continue;

                    xmlNode = node.SelectSingleNode(info.Name);
                    if (xmlNode != null)
                    {
                        info.SetValue(this,
                            string.IsNullOrEmpty(xmlNode.InnerText)
                            ? null : Convert.ChangeType(xmlNode.InnerText, info.PropertyType), null);
                    }
                }
            }

            if (Results != null)
                Results.CommandStringToClass(command);
        }

        /// <summary>
        /// 命令字符串转换实体类
        /// </summary>
        /// <param name="index"></param>
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
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlContent);

            XmlNodeList lstNodes = xmlDocument.SelectNodes("ResponseEntity");
            XmlNode xmlNode = null;

            // 获取返回的头部信息
            Type type = this.GetType();
            PropertyInfo[] proInfos = type.GetProperties();
            foreach (XmlNode node in lstNodes)
            {
                foreach (PropertyInfo info in proInfos)
                {
                    if (info.Name.Equals("Results"))
                        continue;

                    xmlNode = node.SelectSingleNode(info.Name);
                    if (xmlNode != null)
                    {
                        info.SetValue(this,
                            string.IsNullOrEmpty(xmlNode.InnerText)
                            ? null : Convert.ChangeType(xmlNode.InnerText, info.PropertyType), null);
                    }
                }
            }

            if (!string.IsNullOrEmpty(responseResultName))
            {
                Assembly assm = Assembly.Load("Cn.Vcredit.AMS.Entity");
                Results = assm.CreateInstance(responseResultName) as ResponseResult;
                if (Results != null)
                    Results.CommandStringToClass(xmlContent);
            }
        }
        #endregion
    }
}

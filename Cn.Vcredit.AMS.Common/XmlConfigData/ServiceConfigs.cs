using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Cn.Vcredit.AMS.Common.XmlConfigData
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:服务类配置对象列表类
    /// </summary>
    [XmlRoot("ServiceMaps")]
    public class ServiceConfigs
    {
        /// <summary>
        /// 配置对象列表
        /// </summary>
        [XmlArray("ServiceMaps"),XmlArrayItem("ServiceMap")]
        public List<ServiceMap> ServiceMaps{ get; set; } 

        /// <summary>
        /// 序列化成Xml
        /// </summary>
        /// <returns></returns>
        public string ToXmlSerialization()
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
            writer.WriteStartElement("ServiceMaps");

            if (ServiceMaps != null && ServiceMaps.Count > 0)
            {
                string serviceMap = "";
                foreach (var map in ServiceMaps)
                {
                    serviceMap = map.ToXmlSerialization();
                    if (!string.IsNullOrEmpty(serviceMap))
                    {
                        using (StringReader stringReader = new StringReader(serviceMap))
                        {
                            XmlReader xmlReader = XmlReader.Create(stringReader);
                            writer.WriteNode(xmlReader, false);
                        }
                    }
                }
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();

            return sb.ToString();
        }

        /// <summary>
        /// Xml反序列
        /// </summary>
        /// <param name="xmlContent"></param>
        public static ServiceConfigs XmlDeSerialization(string xmlContent)
        {
            ServiceConfigs config = new ServiceConfigs();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlContent);

            XmlNodeList lstNodes = xmlDocument.SelectNodes("ServiceMaps");
            if (lstNodes == null || lstNodes.Count == 0)
                return config;

            // 获取返回的头部信息
            config.ServiceMaps = new List<ServiceMap>();
            foreach (XmlNode node in lstNodes[0].ChildNodes)
            {
                config.ServiceMaps.Add(ServiceMap.XmlDeSerialization(node));
            }

            return config;
        }
    }
}

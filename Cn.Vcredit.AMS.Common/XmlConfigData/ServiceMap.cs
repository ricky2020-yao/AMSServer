using System;
using System.Collections.Generic;
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
    /// Description:服务类配置对象类
    /// </summary>
    [XmlRoot("ServiceMap")]
    [Serializable]
    public class ServiceMap
    {
        /// <summary>
        /// ID标识
        /// </summary>
        [XmlIgnore]
        public int ID { get; set; }
        /// <summary>
        /// 模块类型
        /// </summary>
        [XmlElement("ModuleType")]
        public string ModuleType { get; set; }
        /// <summary>
        /// 服务ID
        /// </summary>
        [XmlAttribute("ServiceId")]
        public string ServiceId { get; set; }
        /// <summary>
        /// 服务端服务名称
        /// </summary>
        [XmlElement("ServiceName")]
        public string ServiceName { get; set; }
        /// <summary>
        /// 服务端服务名称全程，包括命名空间
        /// </summary>
        [XmlElement("ServiceFullName")]
        public string ServiceFullName { get; set; }
        /// <summary>
        /// 服务所在的DLL的名称
        /// </summary>
        [XmlElement("ServiceDLLName")]
        public string ServiceDLLName { get; set; }
        /// <summary>
        /// 服务优先级
        /// </summary>
        [XmlElement("Priority")]
        public int Priority { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [XmlElement("Description")]
        public string Description { get; set; }

        /// <summary>
        /// 是否延迟加载
        /// </summary>
        [XmlElement("IsDelayLoad")]
        public bool IsDelayLoad { get; set; }

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
            writer.WriteStartElement("ServiceMap");

            PropertyInfo[] pInfos = typeof(ServiceMap).GetProperties();
            object value = null;
            for (int j = 0; j < pInfos.Length; j++)
            {
                value = pInfos[j].GetValue(this, null);
                if (value != null)
                {
                    writer.WriteElementString(pInfos[j].Name, value.ToString());
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
        /// <param name="mapNode"></param>
        public static ServiceMap XmlDeSerialization(XmlNode mapNode)
        {
            ServiceMap map = new ServiceMap();

            PropertyInfo proInfoT = null;
            foreach (XmlNode resultNode in mapNode.ChildNodes)
            {
                proInfoT = map.GetType().GetProperty(resultNode.Name);
                if (proInfoT != null)
                {
                    proInfoT.SetValue(map,
                        string.IsNullOrEmpty(resultNode.InnerText)
                        ? null : Convert.ChangeType(resultNode.InnerText, proInfoT.PropertyType), null);
                }
            }

            return map;
        }
    }
}

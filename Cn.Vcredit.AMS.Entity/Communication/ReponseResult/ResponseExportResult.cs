using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Cn.Vcredit.AMS.Entity.Communication.ReponseResult
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月12日
    /// Description:报表类型请求响应结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseExportResult<T>
        : ResponseResult where T : class,new()
    {
        /// <summary>
        /// 返回的结果
        /// </summary>
        public List<T> LstResult = new List<T>();

        /// <summary>
        /// 总的件数
        /// </summary>
        public int TotalCount { get; set; }

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

            writer.WriteStartElement("Results");
            writer.WriteElementString("TotalCount", TotalCount.ToString());

            PropertyInfo[] pInfos = typeof(T).GetProperties();
            object value = null;
            if (LstResult != null)
            {
                writer.WriteStartElement("Result");
                for (int i = 0; i < LstResult.Count; i++)
                {
                    for (int j = 0; j < pInfos.Length; j++)
                    {
                        value = pInfos[j].GetValue(LstResult[i], null);
                        if (value != null)
                        {
                            writer.WriteElementString(pInfos[j].Name, value.ToString());
                        }
                    }
                }

                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();

            return sb.ToString();
        }

        /// <summary>
        /// 命令字符串转换实体类
        /// </summary>
        /// <param name="command"></param>
        public override void CommandStringToClass(string command)
        {
            // 获取返回的头部信息
            ResponseHead = ResponseHead.CommandStringToEntity(command);

            // 明细数据
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(command);

            XmlNodeList lstNodes = xmlDocument.SelectNodes("ResponseEntity");
            XmlNode xmlNode = null;

            foreach (XmlNode node in lstNodes)
            {
                xmlNode = node.SelectSingleNode("Results");
                if (xmlNode == null || !xmlNode.HasChildNodes)
                    return;

                LstResult = new List<T>();

                PropertyInfo proInfoT = null;
                foreach (XmlNode resultsNode in xmlNode.ChildNodes)
                {
                    if (resultsNode.Name == "TotalCount")
                    {
                        int count = 0;
                        if (int.TryParse(resultsNode.InnerText, out count))
                            TotalCount = count;
                    }
                    else if (resultsNode.Name == "Result")
                    {
                        T t = new T();
                        foreach (XmlNode resultNode in resultsNode.ChildNodes)
                        {
                            proInfoT = t.GetType().GetProperty(resultNode.Name);
                            if (proInfoT != null)
                            {
                                proInfoT.SetValue(t,
                                    string.IsNullOrEmpty(resultNode.InnerText)
                                    ? null : Convert.ChangeType(resultNode.InnerText, proInfoT.PropertyType), null);
                            }
                        }

                        LstResult.Add(t);
                    }
                }
            }
        }
    }
}

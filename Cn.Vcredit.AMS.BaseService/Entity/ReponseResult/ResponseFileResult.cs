using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Cn.Vcredit.AMS.BaseService.Entity.ReponseResult
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月30日
    /// Description:请求响应结果的实体文件流类
    /// </summary>
    public class ResponseFileResult:ResponseResult
    {
        /// <summary>
        /// 返回的文件流
        /// </summary>
        public byte[] Result{get;set;}

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
            //writer.WriteStartElement("Results");

            if (Result != null)
            {
                writer.WriteElementString("Result", ConvertHelper.CodingToString(Result as byte[], 1));                
            }

            //writer.WriteEndElement();
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
        }
    }
}

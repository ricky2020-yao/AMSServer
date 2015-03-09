using System;
using System.Collections.Generic;
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
    public class ResponseHead
    {
        #region 属性
		/// <summary>
		/// 客户端请求标识，如有回调，需要用该编号进行匹配。
		/// 此标识对于某个客户端唯一
		/// </summary>
		public string RequestId { get; set; }

		/// <summary>
		/// 调用用户标识，可供验证使用
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// 压缩类型
		/// </summary>
		public int CompressType { get; set; }

		/// <summary>
		/// 加密解密类型
		/// </summary>
		public int EncyptionType { get; set; }

		/// <summary>
		/// 响应返回的状态
		/// </summary>
		public int ResponseStatus { get; set; }

		/// <summary>
		/// 响应返回的消息
		/// </summary>
		public string ResponseMessage { get; set; }
		#endregion

		/// <summary>
		/// 命令字符串转换实体类
		/// </summary>
		/// <param name="command"></param>
		public static ResponseHead CommandStringToEntity(string command)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(command);

			XmlNodeList lstNodes = xmlDocument.SelectNodes("ResponseEntity");
			XmlNode xmlNode = null;

			// 获取返回的头部信息
			ResponseHead head = new ResponseHead();

			Type type = head.GetType();
			PropertyInfo[] proInfos = type.GetProperties();
			foreach (XmlNode node in lstNodes)
			{
				foreach (PropertyInfo info in proInfos)
				{
					xmlNode = node.SelectSingleNode(info.Name);
					if (xmlNode != null)
					{
						info.SetValue(head,
							string.IsNullOrEmpty(xmlNode.InnerText)
							? null : Convert.ChangeType(xmlNode.InnerText, info.PropertyType), null);
					}
				}
			}

			return head;
		}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace Cn.Vcredit.Common.Tools
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月30日
    /// Description:转换帮助类
    /// </summary>
    public static class ConvertHelper
    {
        #region 字符串和Byte数组互转
        /// <summary>
        /// 获取字节值，将字符型转换成字节型【
        /// strData：原字符；
        /// TypeId编码类型1：Base64编码， 2：UTF8编码， 3：ASCII编码】
        /// </summary>
        public static byte[] CodingToByte(string strData, int TypeId)
        {
            byte[] byteData = null ;
            try
            {
                switch (TypeId)
                {
                    case 1:
                        byteData = Convert.FromBase64String(strData);
                        break;
                    case 2:
                        byteData = System.Text.Encoding.UTF8.GetBytes(strData);
                        break;
                    default:
                        System.Text.ASCIIEncoding ByteConverter = new ASCIIEncoding();
                        byteData = ByteConverter.GetBytes(strData);
                        break;
                }
                return byteData;
            }
            catch
            {
                return byteData;
            }
        }

        /// <summary>
        /// 获取字符值， 将字节型转换成字符型【
        /// byteData：原字节；
        /// TypeId编码类型1：Base64编码， 2：UTF8编码， 3：ASCII编码】
        /// </summary>
        public static string CodingToString(byte[] byteData, int TypeId)
        {
            string stringData = "";
            try
            {
                switch (TypeId)
                {
                    case 1:
                        stringData = System.Convert.ToBase64String(byteData);
                        break;
                    case 2:
                        stringData = System.Text.Encoding.UTF8.GetString(byteData);
                        break;
                    default:
                        System.Text.ASCIIEncoding ByteConverter = new ASCIIEncoding();
                        stringData = ByteConverter.GetString(byteData);
                        break;
                }
                return stringData;
            }
            catch
            {
                return stringData;
            }
        }
        #endregion

        #region 序列化和反序列化

        #region XML序列化和反序列化
        /// <summary>
        /// XML序列化
        /// </summary>
        public static string XmlSerializer<T>(T t)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream())
            {
                ser.Serialize(ms, t);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// XML反序列化
        /// </summary>
        public static T XmlDeserialize<T>(string jsonString)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                return (T)xmlSerializer.Deserialize(ms);
            }
        }
        #endregion

        #region JSON序列化和反序列化
        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream())
            {
                ser.WriteObject(ms, t);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(obj.GetType());
                return (T)jsonSerializer.ReadObject(ms);
            }
        }

        /// <summary>  
        /// 将DataTable转换为JSON字符串  
        /// </summary>  
        /// <param name="dt">数据表</param>  
        /// <returns>JSON字符串</returns>  
        public static string GetJsonFromDataTable(DataTable dt)
        {
            StringBuilder sbJsonString = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                sbJsonString.Append("{ ");
                sbJsonString.Append("\"TableInfo\":[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sbJsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            sbJsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() 
                                + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            sbJsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() 
                                + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        sbJsonString.Append("} ");
                    }
                    else
                    {
                        sbJsonString.Append("}, ");
                    }
                }
                sbJsonString.Append("]}");
                return sbJsonString.ToString();
            }
            else
            {
                return null;
            }
        }  

        #region 时间特殊处理的JSON序列化和反序列化
        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializerSpecialDateTime<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            string jsonString = "";
            using (MemoryStream ms = new MemoryStream())
            {
                ser.WriteObject(ms, t);
                jsonString = Encoding.UTF8.GetString(ms.ToArray());
            }

            //替换Json的Date字符串
            string p = @"\\/Date\((\d+)\+\d+\)\\/";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            return jsonString;
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserializeSpecialDateTime<T>(string jsonString)
        {
            //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式
            string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        /// 将时间字符串转为Json时间
        /// </summary>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }
        #endregion
        #endregion

        #region 二进制序列化和反序列化
        /// <summary>
        /// 二进制序列化
        /// </summary>
        public static string BinarySerializer<T>(T t)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, t);
                ms.Position = 0;
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// 二进制反序列化
        /// </summary>
        public static T BinaryDeserialize<T>(string binaryString)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(binaryString)))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
        #endregion

        #region Soap序列化和反序列化
        /// <summary>
        /// SOAP序列化
        /// </summary>
        public static string SoapSerializer<T>(T t)
        {
            SoapFormatter formatter = new SoapFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, t);
                ms.Position = 0;
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// SOAP反序列化
        /// </summary>
        public static T SoapDeserialize<T>(string soapString)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(soapString)))
            {
                SoapFormatter formatter = new SoapFormatter();
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
        #endregion

        #endregion
    }
}

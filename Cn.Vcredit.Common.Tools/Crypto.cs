using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cn.Vcredit.Common.Tools
{
    /// <summary>
    /// Crypto 的摘要说明。
    /// </summary>
    public sealed class Crypto
    {
        // 0-9,a-z,A-Z 字符16进制 ASCII
        static readonly byte[] chars = new byte[]{
													 0x30,0x31,0x32,0x33,0x34,0x35,0x36,0x37,0x38,0x39,
													 0x41,0x42,0x43,0x44,0x45,0x46,0x47,0x48,0x49,0x4A,
													 0x4B,0x4C,0x4D,0x4E,0x4F,0x50,0x51,0x52,0x53,0x54,
													 0x55,0x56,0x57,0x58,0x59,0x5A,0x61,0x62,0x63,0x64,
													 0x65,0x66,0x67,0x68,0x69,0x6A,0x6B,0x6C,0x6D,0x6E,
													 0x6F,0x70,0x71,0x72,0x73,0x74,0x75,0x76,0x77,0x78,
													 0x79,0x7A
												 };

        // 0-9,a-z 字符16进制 ASCII
        static readonly byte[] chars2 = new byte[]{
													  0x30,0x31,0x32,0x33,0x34,0x35,0x36,0x37,0x38,0x39,
													  0x61,0x62,0x63,0x64,0x65,0x66,0x67,0x68,0x69,0x6A,
													  0x6B,0x6C,0x6D,0x6E,0x6F,0x70,0x71,0x72,0x73,0x74,
													  0x75,0x76,0x77,0x78,0x79,0x7A
												  };

        // a-z,A-Z 字符16进制 ASCII
        static readonly byte[] chars3 = new byte[]{
													 0x41,0x42,0x43,0x44,0x45,0x46,0x47,0x48,0x49,0x4A,
													 0x4B,0x4C,0x4D,0x4E,0x4F,0x50,0x51,0x52,0x53,0x54,
													 0x55,0x56,0x57,0x58,0x59,0x5A,0x61,0x62,0x63,0x64,
													 0x65,0x66,0x67,0x68,0x69,0x6A,0x6B,0x6C,0x6D,0x6E,
													 0x6F,0x70,0x71,0x72,0x73,0x74,0x75,0x76,0x77,0x78,
													 0x79,0x7A
												 };

        /// <summary>
        /// MD5算法散列数据
        /// </summary>
        /// <param name="Text"> 需散列文本 </param>
        /// <returns> 定长32位16进制字符(大写) </returns>
        public static string ToMD5Hash(string text, string encode)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] md5re = md5.ComputeHash(Encoding.GetEncoding(encode).GetBytes(text));

            StringBuilder sre = new StringBuilder(32);
            for (int i = 0; i < md5re.Length; i++)
            {
                if (md5re[i] < 16) sre.Append("0");
                sre.Append(md5re[i].ToString("x"));
            }
            md5.Clear();
            md5 = null;

            return sre.ToString();	//定长32位16进制数据
        }

        public static string ToMD5Hash(string text)
        {
            return ToMD5Hash(text, "GB2312");
        }

        /// <summary>
        /// byte 数组转换成可显示的16进制表示的字符串
        /// </summary>
        public static string BytesToHexString(byte[] data)
        {
            StringBuilder temp = new StringBuilder(data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] < 16) temp.Append("0");
                temp.Append(data[i].ToString("x"));
            }
            return temp.ToString();
        }

        /// <summary>
        /// BytesToHexString 的逆向转换
        /// </summary>
        public static byte[] HexStringToBytes(string data)
        {
            byte[] temp = new Byte[data.Length / 2];
            int j = 0;
            for (int i = 0; i < data.Length; i += 2)
            {
                temp[j++] = Convert.ToByte("0x" + data[i] + data[i + 1], 16);
            }
            return temp;
        }

        /// <summary>
        /// 产生随机字符序列（仅包含字母和数字序列）
        /// </summary>
        /// <param name="length"> 随机字符长度 </param>
        /// <returns> 随机字符串 </returns>
        public static string Rnd(int length)
        {
            StringBuilder sb = new StringBuilder();
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] data = new byte[length];
            rng.GetNonZeroBytes(data);
            rng = null;

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = chars[data[i] %= 62];
            }
            return Encoding.ASCII.GetString(data);
        }

        /// <summary>
        /// 产生随机字符序列（仅包含小写字母和数字序列）
        /// </summary>
        /// <param name="length"> 随机字符长度 </param>
        /// <returns> 随机字符串 </returns>
        public static string RndLowerCase(int length)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] data = new byte[length];
            rng.GetNonZeroBytes(data);
            rng = null;

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = chars2[data[i] %= 36];
            }

            return Encoding.ASCII.GetString(data);
        }

        /// <summary>
        /// 产生随机字符序列（仅包含大小写字母）
        /// </summary>
        /// <param name="length"> 随机字符长度 </param>
        /// <returns> 随机字符串 </returns>
        public static string RndCase(int length)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] data = new byte[length];
            rng.GetNonZeroBytes(data);
            rng = null;

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = chars3[data[i] %= 52];
            }

            return Encoding.ASCII.GetString(data);
        }

        /// <summary>
        /// 获取订单冻结编号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetFrozenNo(byte kind)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff")
                .Insert(0, kind.ToString());
        }
        /// <summary>
        /// 每日扣款创建列表
        /// </summary>
        public static object CreatPayDayList = new object();
        /// <summary>
        /// 每日扣款实收列表
        /// </summary>
        public static object ReceviedPayDayList = new object();
        /// <summary>
        /// 每日扣款获取扣款列表
        /// </summary>
        public static object GetPayDayList = new object();

        /// <summary>
        /// 富友通讯层生成文件名加锁码
        /// </summary>
        public static object CreateFileName_Fuiou = new object();

        /// <summary>
        /// 为更新订单、帐单冗余字段操作加锁
        /// </summary>
        public static object BusinessListUpdate = new object();
    }
}

using Cn.Vcredit.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cn.Vcredit.Common.Tools
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月1日
    /// Description:加密解密帮助类
    /// </summary>
    public class EncyptionHelper
    {
        #region 内部变量
        /// <summary>
        /// 编码方式
        /// </summary>
        private static Encoding m_Encoding = Encoding.UTF8;
        /// <summary>
        /// 编码方式
        /// </summary>
        public static Encoding Encoding
        {
            get
            {
                return m_Encoding;
            }
            set
            {
                m_Encoding = value;
            }
        }

        /// <summary>
        /// 默认密钥向量 AES
        /// </summary>
        private static byte[] m_IV_AES = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// 默认密钥向量 DES
        /// </summary>
        private static byte[] m_IV_DES = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// 默认密钥向量 TripleDES
        /// </summary>
        private static byte[] m_IV_TripleDES = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        #endregion

        #region 对外方法
        /// <summary>
        /// 加密算法
        /// </summary>
        /// <param name="encyptionType"></param>
        /// <param name="plainText"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string Encrypt(EnumEncyptionType encyptionType, string plainText, string strKey)
        {
            switch (encyptionType)
            {
                case EnumEncyptionType.None:
                    return plainText;
                case EnumEncyptionType.AES:
                    return AESEncrypt(plainText, strKey);
                case EnumEncyptionType.DES:
                    return DESEncrypt(plainText, strKey);
                case EnumEncyptionType.TripleDES:
                    return TripleDESEncrypt(plainText, strKey);
                default:
                    return plainText;
            }
        }

        /// <summary>
        /// 解密算法
        /// </summary>
        /// <param name="encyptionType"></param>
        /// <param name="OriginalData"></param>
        /// <returns></returns>
        public static string Decrypt(EnumEncyptionType encyptionType, string cipherText, string strKey)
        {
            switch (encyptionType)
            {
                case EnumEncyptionType.None:
                    return cipherText;
                case EnumEncyptionType.AES:
                    return AESDecrypt(cipherText, strKey);
                case EnumEncyptionType.DES:
                    return DESDecrypt(cipherText, strKey);
                case EnumEncyptionType.TripleDES:
                    return TripleDESDecrypt(cipherText, strKey);
                default:
                    return cipherText;
            }
        }
        #endregion

        #region AES 对称加密/解密算法
        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回加密后的密文字节数组</returns>
        public static string AESEncrypt(string plainText, string strKey)
        {
            //分组加密算法
            Rijndael aes = Rijndael.Create();

            aes.Key = Encoding.UTF8.GetBytes(GetPassword(strKey, aes.Key.Length));
            aes.IV = m_IV_AES;

            //得到需要加密的字节数组
            byte[] inputByteArray = Encoding.GetBytes(plainText);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms,
                    aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组

                    //将加密后的结果转换为字符串
                    return Convert.ToBase64String(cipherBytes);
                }
            }
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">密文字符串</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回解密后的字符串</returns>
        public static string AESDecrypt(string cipherText, string strKey)
        {
            //分组加密算法
            SymmetricAlgorithm aes = Rijndael.Create();

            aes.Key = Encoding.UTF8.GetBytes(GetPassword(strKey, aes.Key.Length));
            aes.IV = m_IV_AES;

            //得到需要解密的字节数组	
            byte[] inputByteArray = Convert.FromBase64String(cipherText);

            byte[] decryptBytes = new byte[inputByteArray.Length];
            using (MemoryStream ms = new MemoryStream(inputByteArray))
            {
                using (CryptoStream cs = new CryptoStream(ms,
                    aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);

                    //将解密后的结果转换为字符串
                    return Encoding.GetString(decryptBytes);
                }
            }
        }
        #endregion

        #region DES 对称加密/解密算法
        /// <summary>
        /// DES 对称加密算法
        /// <param name="plainText">明文字符串</param>
        /// <param name="strKey">密钥</param>
        /// 【DataToEncrypt：原文；ekey：随机密码，8个字符】【原文有中文传入的原文字符用utf-8编码转换成字节】
        /// </summary>
        public static string DESEncrypt(string plainText, string strKey)
        {
            //DES 对称加密算法
            SymmetricAlgorithm des = new DESCryptoServiceProvider();

            des.Key = Encoding.UTF8.GetBytes(GetPassword(strKey, des.Key.Length));
            des.IV = m_IV_DES;

            //得到需要加密的字节数组
            byte[] inputByteArray = Encoding.GetBytes(plainText);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms,
                    des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组

                    //将加密后的结果转换为字符串
                    return Convert.ToBase64String(cipherBytes);
                }
            }
        }

        /// <summary>
        /// Des 对称解密算法
        /// <param name="cipherText">密文字符串</param>
        /// <param name="strKey">密钥</param>
        /// 【DataToDecrypt：密文；ekey：随机密码，8个字符】【原文有中文返回字节用utf-8编码转换成字符】
        /// </summary>
        public static string DESDecrypt(string cipherText, string strKey)
        {
            // DES 对称解密算法
            SymmetricAlgorithm des = new DESCryptoServiceProvider();

            des.Key = Encoding.UTF8.GetBytes(GetPassword(strKey, des.Key.Length));
            des.IV = m_IV_DES;

            //得到需要解密的字节数组	
            byte[] inputByteArray = Convert.FromBase64String(cipherText);

            byte[] decryptBytes = new byte[inputByteArray.Length];

            using (MemoryStream ms = new MemoryStream(inputByteArray))
            {
                using (CryptoStream cs = new CryptoStream(ms,
                    des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    //将解密后的结果转换为字符串
                    return Encoding.GetString(decryptBytes);
                }
            }
        }
        #endregion

        #region TripleDES 对称加密/解密算法
        /// <summary>
        /// TripleDES 对称加密算法
        /// <param name="plainText">明文字符串</param>
        /// <param name="strKey">密钥</param>
        /// 算法加密字串【DataToEncrypt：原文；priKkey：随机密码，十六个字符】【原文有中文传入的原文字符用utf-8编码转换成字节】
        /// </summary>
        public static string TripleDESEncrypt(string plainText, string strKey)
        {
            // TripleDES 对称加密算法
            SymmetricAlgorithm tripleDES = new TripleDESCryptoServiceProvider();

            tripleDES.Key = Encoding.UTF8.GetBytes(GetPassword(strKey, tripleDES.Key.Length));
            tripleDES.IV = m_IV_TripleDES;

            //得到需要加密的字节数组
            byte[] inputByteArray = Encoding.GetBytes(plainText);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms,
                    tripleDES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组

                    //将加密后的结果转换为字符串
                    return Convert.ToBase64String(cipherBytes);
                }
            }
        }

        /// <summary>
        /// TripleDES 对称解密算法
        /// <param name="cipherText">密文字符串</param>
        /// <param name="strKey">密钥</param>
        /// 【DataToDecrypt：密文；priKkey：随机密码，十六个字符】【原文有中文返回字节用utf-8编码转换成字符】
        /// </summary>
        public static string TripleDESDecrypt(string cipherText, string strKey)
        {
            // TripleDES 对称加密算法
            SymmetricAlgorithm tripleDES = new TripleDESCryptoServiceProvider();

            tripleDES.Key = Encoding.UTF8.GetBytes(GetPassword(strKey, tripleDES.Key.Length));
            tripleDES.IV = m_IV_TripleDES;

            //得到需要解密的字节数组	
            byte[] inputByteArray = Convert.FromBase64String(cipherText);

            byte[] decryptBytes = new byte[inputByteArray.Length];

            using (MemoryStream ms = new MemoryStream(inputByteArray))
            {
                using (CryptoStream cs = new CryptoStream(ms,
                    tripleDES.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    //将解密后的结果转换为字符串
                    return Encoding.GetString(decryptBytes);
                }
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 根据对称算法的密钥长度获取密码
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="keyLength"></param>
        /// <returns></returns>
        private static string GetPassword(string keyValue, int keyLength)
        {
            string afterKey = keyValue;
            if (keyValue.Length < keyLength)
                afterKey = keyValue.PadRight(keyLength, '$');
            else if (keyValue.Length > keyLength)
                afterKey = keyValue.Substring(0, keyLength);

            return afterKey;
        }
        #endregion
    }
}
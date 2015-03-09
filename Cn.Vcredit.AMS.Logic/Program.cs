using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.Logic
{
    class Program
    {
        static void Main(string[] args)
        {

            string originalText = "dsagrewt3265342tgfdh5ye76&AD89sad0as892340423908*&*&*)(@!*$!@#)$*)!#$#*(!@$&#!$#!";
            string key = "@#!@3s#$324dffds@!$3322sdffds@!@$!@@#@";
            Console.WriteLine("原始记录：" + originalText);
            Console.WriteLine("密码" + key);

            Console.WriteLine("AES加密");
            string cipherText = EncyptionHelper.AESEncrypt(originalText, key);
            Console.WriteLine("加密后的字符串：" + cipherText);
            string plainText = EncyptionHelper.AESDecrypt(cipherText, key);
            Console.WriteLine("解密后的字符串：" + plainText);
            Console.WriteLine("解密和原始字符串比较：" + (originalText.CompareTo(plainText) == 0));

            Console.WriteLine("DES加密");
            cipherText = EncyptionHelper.DESEncrypt(originalText, key);
            Console.WriteLine("加密后的字符串：" + cipherText);
            plainText = EncyptionHelper.DESDecrypt(cipherText, key);
            Console.WriteLine("解密后的字符串：" + plainText);
            Console.WriteLine("解密和原始字符串比较：" + (originalText.CompareTo(plainText) == 0));

            Console.WriteLine("TripleDES加密");
            cipherText = EncyptionHelper.TripleDESEncrypt(originalText, key);
            Console.WriteLine("加密后的字符串：" + cipherText);
            plainText = EncyptionHelper.TripleDESDecrypt(cipherText, key);
            Console.WriteLine("解密后的字符串：" + plainText);
            Console.WriteLine("解密和原始字符串比较：" + (originalText.CompareTo(plainText) == 0));

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}

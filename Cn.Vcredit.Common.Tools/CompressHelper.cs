using Aced.Compression;
using Cn.Vcredit.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.Common.Tools
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description:压缩解压缩帮助类
    /// </summary>
    public class CompressHelper
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="compressType"></param>
        /// <param name="OriginalData"></param>
        /// <returns></returns>
        public static byte[] Compress(EnumCompressType compressType, byte[] OriginalData)
        {
            switch (compressType)
            {
                case EnumCompressType.None:
                    return OriginalData;
                case EnumCompressType.MemCompress:
                    return MemCompress(OriginalData);
                case EnumCompressType.GZIP:
                    return GZipCompress(OriginalData);
                default:
                    return OriginalData;
            }
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="compressType"></param>
        /// <param name="OriginalData"></param>
        /// <returns></returns>
        public static byte[] DeCompress(EnumCompressType compressType, byte[] OriginalData)
        {
            switch (compressType)
            {
                case EnumCompressType.None:
                    return OriginalData;
                case EnumCompressType.MemCompress:
                    return MemDeCopress(OriginalData);
                case EnumCompressType.GZIP:
                    return GZipDecompress(OriginalData);
                default:
                    return OriginalData;
            }
        }

        #region GZip
        /// <summary>
        /// GZip方式压缩
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] GZipCompress(byte[] data)
        {
            byte[] buffer;
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress))
                {
                    stream2.Write(data, 0, data.Length);
                    buffer = stream.ToArray();
                }
            }
            return buffer;
        }

        /// <summary>
        /// GZip方式解压缩
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] GZipDecompress(byte[] data)
        {
            byte[] buffer2;
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream stream2 = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
                {
                    int num;
                    byte[] buffer = new byte[0xa000];
                    while ((num = stream2.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        stream.Write(buffer, 0, num);
                    }
                    buffer2 = stream.ToArray();
                }
            }
            return buffer2;
        }
        #endregion

        #region Memory
        /// <summary>
        /// 内存方式压缩
        /// </summary>
        /// <param name="bytesToCompressBytes"></param>
        /// <returns></returns>
        public static byte[] MemCompress(byte[] bytesToCompressBytes)
        {
            byte[] buffer2;
            try
            {
                buffer2 = new AcedDeflator().Compress(
                    bytesToCompressBytes, 0, bytesToCompressBytes.Length, AcedCompressionLevel.Fast, 0, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return buffer2;
        }

        /// <summary>
        /// 内存方式解压缩
        /// </summary>
        /// <param name="bytesToDecompress"></param>
        /// <returns></returns>
        public static byte[] MemDeCopress(byte[] bytesToDecompress)
        {
            byte[] buffer2;
            try
            {
                buffer2 = new AcedInflator().Decompress(bytesToDecompress, 0, 0, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return buffer2;
        }
        #endregion
    }
}

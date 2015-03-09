using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.BLL.PayBank
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年6月8日
    /// 字节流打包扩展类
    /// </summary>
    public class StringDataSource : IStaticDataSource
    {
        public StringDataSource(byte[] data)
        {
            data_ = data;
        }
        public Stream GetSource()
        {
            return new MemoryStream(data_);
        }
        readonly byte[] data_;
    }
}

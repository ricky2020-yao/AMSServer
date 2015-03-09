using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.SyncFromSql
{
    public class Program
    {
        /// <summary>
        /// 数据同步
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SyncToMongoDB sync = new SyncToMongoDB();
            sync.SyncFromSql();
        }
    }
}

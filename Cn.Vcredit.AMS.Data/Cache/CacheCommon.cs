using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.Cache
{
    public class CacheCommon
    {
        public enum CacheState { Unloaded, Loading, Loaded };

        public enum CachePriority { Prerequisite, Normal };

        public enum RecordChangeType { None, Append, Update, Delete };

        public enum SortType { ASC, DESC };


    }
}

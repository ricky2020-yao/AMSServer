using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.Common
{
    public class EnumerationViewData
    {
        /// <summary>
        /// 枚举ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 上级目录
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// FullKey
        /// </summary>
        public string FullKey { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupbyName { get; set; }
        /// <summary>
        /// 枚举名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ParentFullKey
        /// </summary>
        public string ParentFullKey { get; set; }

        /// <summary>
        /// 启用标识 1:启用 0:禁用
        /// </summary>
        public int IsEnable { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}

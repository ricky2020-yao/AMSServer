using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.Cache.Data
{
    [Serializable]
    public class RegionV
    {
        /// <summary>
        /// 递增序列号
        /// </summary>
        public int SerialId
        {
            get;
            set;
        }
        /// <summary>
        /// 分部ID
        /// </summary>
        public int CityId
        {
            get;
            set;
        }
        /// <summary>
        /// 分部名称
        /// </summary>
        public string CityName
        {
            get;
            set;
        }
        /// <summary>
        /// 分部ID
        /// </summary>
        public int ParentId
        {
            get;
            set;
        }
        /// <summary>
        /// 记录子节点数量
        /// </summary>
        public int ChildCount
        {
            get;
            set;
        }
        /// <summary>
        /// 分部名称
        /// </summary>
        public string ParentName
        {
            get;
            set;
        }
        /// <summary>
        /// 城市主键
        /// </summary>
        public string RegionKey
        {
            get;
            set;
        }
        /// <summary>
        /// 城市全主键
        /// </summary>
        public string FullKey
        {
            get;
            set;
        }
        /// <summary>
        /// 城市权限键-过度
        /// </summary>
        public string PermissionKey
        {
            get;
            set;
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public int IsActive
        {
            get;
            set;
        }
        /// <summary>
        /// 最后修改人Id
        /// </summary>
        public int OperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? updateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 组织类型ID
        /// </summary>
        public int OrgTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? CreaTime
        {
            get;
            set;
        }
        /// <summary>
        /// 最后修改人员 
        /// </summary>
        public int Updateuser
        {
            get;
            set;
        }
    }
}

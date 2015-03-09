using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.Cache.Data
{
    [Serializable]
    public class TeamV
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
        /// 团队名称
        /// </summary>
        public string TeamName
        {
            get;
            set;
        }
        /// <summary>
        /// 团队领导人id
        /// </summary>
        public int TeamLeader
        {
            get;
            set;
        }
        /// <summary>
        /// 团队领导人名称
        /// </summary>
        public string TeamLeaderName
        {
            get;
            set;
        }
        /// <summary>
        /// 团队主键
        /// </summary>
        public string TeamKey
        {
            get;
            set;
        }
        /// <summary>
        /// 团队全主键
        /// </summary>
        public string FullKey
        {
            get;
            set;
        }
        /// <summary>
        /// 团队权限键-过度
        /// </summary>
        public string PermissionKey
        {
            get;
            set;
        }
        /// <summary>
        /// 门店ID
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
        /// 门店名称
        /// </summary>
        public string ParentName
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.Cache.Data
{
    [Serializable]
    public class Agent
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
        /// ？？？
        /// </summary>
        public int UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 经办人名称
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 经办人主键
        /// </summary>
        public string agentKey
        {
            get;
            set;
        }
        /// <summary>
        /// 经办全主键
        /// </summary>
        public string FullKey
        {
            get;
            set;
        }
        /// <summary>
        /// 经办权限键-过度
        /// </summary>
        public string PermissionKey
        {
            get;
            set;
        }
        /// <summary>
        /// 团队ID
        /// </summary>
        public int ParentId
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

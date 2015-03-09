using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.Cache.Data
{
    [Serializable]
    public class PermissionV
    {
        /// <summary>
        ///  id 
        /// </summary> 
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 账号 id 
        /// </summary> 
        public int AccountId
        {
            get;
            set;
        }
        /// <summary>
        /// 用户 id 
        /// </summary> 
        public int UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 组织 id 
        /// </summary> 
        public int OrgId
        {
            get;
            set;
        }
        /// <summary>
        /// 组织名称 
        /// </summary> 
        public string OrgName
        {
            get;
            set;
        }
        /// <summary>
        /// 组织是否子组织
        /// </summary> 
        public int OrgIsSub
        {
            get;
            set;
        }
        /// <summary>
        /// 组织类型Id
        /// </summary> 
        public int OrgTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 授权人ID
        /// </summary> 
        public int OperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 最后修改时间
        /// </summary> 
        public DateTime UpdateTime
        {
            get;
            set;
        }
    }
}

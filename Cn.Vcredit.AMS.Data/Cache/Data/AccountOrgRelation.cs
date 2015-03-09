using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.Cache.Data
{
    /// <summary>
    /// AccountOrgRelationEntity object for table 'AccountOrgRelation'.
    /// </summary>
    [Serializable]
    public class AccountOrgRelation
    {
        public int Id { get; set; }

        /// <summary>
        /// 账户表用户id
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 用户表的关联id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 组织结构ID
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 1:BRANCH,2:CITY,3:STORE
        /// </summary>
        public int OrgLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OperatorId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}

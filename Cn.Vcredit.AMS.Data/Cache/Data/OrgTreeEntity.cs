using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.Cache.Data
{
    [Serializable]
    public class OrgTreeEntity
    {
        /// <summary>
        ///  键
        /// </summary>
        public int id
        {
            get;
            set;
        }
        /// <summary>
        /// 上级ID
        /// </summary>
        public int pId
        {
            get;
            set;
        }
        /// <summary>
        ///  树主键
        /// </summary>
        public string treeid
        {
            get;
            set;
        }
        /// <summary>
        /// 树上级键
        /// </summary>
        public string treepId
        {
            get;
            set;
        }
        /// <summary>
        ///  名称
        /// </summary>
        public string name
        {
            get;
            set;
        }
        /// <summary>
        ///  全文键
        /// </summary>
        public string fullkey
        {
            get;
            set;
        }
        /// <summary>
        /// 是否包含下级true是false否
        /// </summary>
        public string isParent
        {
            get;
            set;
        }
        /// <summary>
        /// 扩展名称（分类）
        /// </summary>
        public string type
        {
            get;
            set;
        }
        /// <summary>
        /// 是否本级的子节点
        /// </summary>
        public int issub
        {
            get;
            set;
        }
        /// <summary>
        /// 树级别类型
        /// </summary>
        public int orgtypeid
        {
            get;
            set;
        }
        /// <summary>
        /// 树级别类型
        /// </summary>
        public int teamleader
        {
            get;
            set;
        }
        /// <summary>
        /// 树级别类型
        /// </summary>
        public string teamleaderName
        {
            get;
            set;
        }
        /// <summary>
        /// 孩子节点数
        /// </summary>
        public int ChildCount
        {
            get;
            set;
        }
        private string _open = "false";
        /// <summary>
        /// 是否展开"true"是false否
        /// </summary>
        public string open
        {
            get { return _open; }
            set { _open = value; }
        }
        /// <summary>
        /// 是否展开"true"是false否
        /// </summary>
        public IList<OrgTreeEntity> children
        {
            get;
            set;
        }
        /// <summary>
        ///  是否有编辑权限
        /// </summary>
        public int ishasper
        {
            get;
            set;
        }
    }

    [Serializable]
    public class OrgTableEntity
    {
        /// <summary>
        ///  键
        /// </summary>
        public int id
        {
            get;
            set;
        }
        /// <summary>
        ///  名称
        /// </summary>
        public string name
        {
            get;
            set;
        }
        /// <summary>
        ///   键
        /// </summary>
        public string key
        {
            get;
            set;
        }
        /// <summary>
        ///  全文键
        /// </summary>
        public string fullkey
        {
            get;
            set;
        }
        /// <summary>
        ///  权限键
        /// </summary>
        public string permissionkey
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string cityid
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int active
        {
            get;
            set;
        }
        /// <summary>
        /// 是否本级的子节点
        /// </summary>
        public int orgtypeid
        {
            get;
            set;
        }
        /// <summary>
        /// 树级别类型
        /// </summary>
        public int storetype
        {
            get;
            set;
        }
        /// <summary>
        ///  
        /// </summary>
        public int leader
        {
            get;
            set;
        }
        /// <summary>
        ///  
        /// </summary>
        public string leadername
        {
            get;
            set;
        }
        /// <summary>
        ///  
        /// </summary>
        public int teamleader
        {
            get;
            set;
        }
        /// <summary>
        ///  
        /// </summary>
        public string teamleadername
        {
            get;
            set;
        }
        /// <summary>
        ///  
        /// </summary>
        public int userid
        {
            get;
            set;
        }

    }
}

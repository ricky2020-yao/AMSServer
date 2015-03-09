using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.Cache.Data
{
    [Serializable]
    public class Store
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
        /// 门店名称
        /// </summary>
        public string StoreName
        {
            get;
            set;
        }
        /// <summary>
        /// ???
        /// </summary>
        public int AttributeId
        {
            get;
            set;
        }
        /// <summary>
        /// 门店主键
        /// </summary>
        public string StoreKey
        {
            get;
            set;
        }
        /// <summary>
        /// 门店全主键
        /// </summary>
        public string FullKey
        {
            get;
            set;
        }
        /// <summary>
        /// 门店权限键-过度
        /// </summary>
        public string PermissionKey
        {
            get;
            set;
        }
        /// <summary>
        /// 城市ID
        /// </summary>
        public int ParentId
        {
            get;
            set;
        }
        /// <summary>
        /// 上级门店id
        /// </summary>
        public int ParentStoreId
        {
            get;
            set;
        }
        /// <summary>
        /// 门店类型0主门店1子门店
        /// </summary>
        public int StoreType
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

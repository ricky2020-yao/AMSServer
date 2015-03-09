using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.Cache.Data
{
    [Serializable]
    public class Division
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
        /// 分部所属城市ID
        /// </summary>
        public int CityId
        {
            get;
            set;
        }
        /// <summary>
        /// 分部名称
        /// </summary>
        public string DivisionName
        {
            get;
            set;
        }
        /// <summary>
        /// 分部主键
        /// </summary>
        public string DivisionKey
        {
            get;
            set;
        }
        /// <summary>
        /// 分部全主键
        /// </summary>
        public string FullKey
        {
            get;
            set;
        }
        /// <summary>
        /// 分部权限键-过度
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

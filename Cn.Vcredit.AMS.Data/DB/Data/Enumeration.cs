using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:枚举
    /// </summary>
    public class Enumeration
    {
        /// <summary>
        /// 枚举表主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 所属上级，自连接主键
        /// </summary>
        public int Super { get; set; }

        /// <summary>
        /// 枚举对应的同层级的唯一键。系统进行判断时使用，内部进行递归获取下级节点时使用，每一层之间使用逗号隔开
        /// </summary>
        public string EnumKey { get; set; }

        /// <summary>
        /// 枚举名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 系统设置类型的枚举对应值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 枚举类型（0选择项，1系统设置）默认0
        /// </summary>
        public Int16 EnumType { get; set; }

        /// <summary>
        /// 在同一节点下的显示顺序。
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 描述，系统设置类型是必须填写
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否停用,1使用，0停用。默认1使用。
        /// </summary>
        public bool IsDisable { get; set; }

        /// <summary>
        /// 是否删除(0否，1是)默认0
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 枚举的地区权限。引用权限表，以完全限定路径的权限键做链接。空字符串就是无区域之分
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 旧的ID
        /// </summary>
        public int OldID { get; set; }

        /// <summary>
        /// FullKey
        /// </summary>
        public string FullKey { get; set; }

        /// <summary>
        /// EnumBusType
        /// </summary>
        public int EnumBusType { get; set; }

        /// <summary>
        /// FlowType
        /// </summary>
        public int FlowType { get; set; }

         //<summary>
         //当前枚举的所有下级（包含子集）
         //</summary>
        public List<Enumeration> AllChild = new List<Enumeration>();
    }
}

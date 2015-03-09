using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:自动创建（陈伟、王正吉）
    /// CreateTime:2012-7-21 14:08:32
    /// Description:客户的个人资料（由贷前提供，贷后维护）
    /// </summary>
    public class Customer
    {
        #region- 基本属性 -
        /// <summary>
        /// 客户编号
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 移动电话（手机）
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 住宅电话（固定电话）
        /// </summary>
        public string ResidenceTel { get; set; }

        /// <summary>
        /// 住宅地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 户口所在地
        /// </summary>
        public string HouseholdAddress { get; set; }

        /// <summary>
        /// 户籍类型（1、本地籍 2、非本地籍）
        /// </summary>
        public string HouseholdType { get; set; }

        /// <summary>
        /// 受薪日
        /// </summary>
        public string SalariedDay { get; set; }

        /// <summary>
        /// 月收入
        /// </summary>
        public string MonthIncome { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 配偶姓名
        /// </summary>
        public string SpouseName { get; set; }

        /// <summary>
        /// 配偶电话
        /// </summary>
        public string SpouseTEL { get; set; }

        /// <summary>
        /// 直属关系
        /// </summary>
        public string DirectRelation { get; set; }

        /// <summary>
        /// 直系姓名
        /// </summary>
        public string DirectRelName { get; set; }

        /// <summary>
        /// 直系电话
        /// </summary>
        public string DirectRelTEL { get; set; }

        /// <summary>
        /// 联系人关系
        /// </summary>
        public string ContacterRelation { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContacterName { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContacterTEL { get; set; }

        /// <summary>
        /// 担保人姓名
        /// </summary>
        public string GuarantorName { get; set; }

        /// <summary>
        /// 担保人电话
        /// </summary>
        public string GuarantorTEL { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Educational { get; set; }

        /// <summary>
        /// 婚姻状态  0.未婚  1.已婚无子女 2.已婚有子女 3.其他
        /// </summary>
        public string Marriage { get; set; }

        /// <summary>
        /// 邮局编码
        /// </summary>
        public string PostalNO { get; set; }

        /// <summary>
        /// 单位电话
        /// </summary>
        public string CompanyTel { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string CompPosi { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 即时通讯  QQ/MSN
        /// </summary>
        public string Communication { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdenNo { get; set; }

        /// <summary>
        /// 客户性质
        /// </summary>
        public string EntRegist { get; set; }

        /// <summary>
        /// 户籍所在地电话
        /// </summary>
        public string HouseholdPhone { get; set; }

        /// <summary>
        /// 产权房地址
        /// </summary>
        public string EstateAddr { get; set; }

        /// <summary>
        /// 单位性质
        /// </summary>
        public string CompType { get; set; }

        /// <summary>
        /// 企业规模
        /// </summary>
        public string CompSize { get; set; }

        /// <summary>
        /// 所属行业
        /// </summary>
        public string Industry { get; set; }

        /// <summary>
        /// 现任职部门
        /// </summary>
        public string CompDepart { get; set; }

        /// <summary>
        /// 岗位级别
        /// </summary>
        public string CompPosiLevel { get; set; }

        /// <summary>
        /// 是否发送短信
        /// </summary>
        public bool IsSendMsg { get; set; }
        #endregion
    }
}

using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.MongoData
{
    /// <summary>
    /// 客户信息数据类
    /// </summary>
    [MongoTableNameAtrr("Mongo.Pre_Customer")]
    public class MongoCustomer : MongoDataEntity
    {
        /// <summary>
        /// 业务号 
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 客户号
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 住宅电话
        /// </summary>
        public string ResidenceTel { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 户口所在地
        /// </summary>
        public string HouseholdAddress { get; set; }
        /// <summary>
        /// 户籍类型
        /// </summary>
        public string HouseholdType { get; set; }
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
        /// 直系关系
        /// </summary>
        public string DirectRelation { get; set; }
        /// <summary>
        /// 直系关系人姓名
        /// </summary>
        public string DirectRelName { get; set; }
        /// <summary>
        /// 直系关系联系方式
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
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 教育程度
        /// </summary>
        public string Educational { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string Marriage { get; set; }
        /// <summary>
        /// 邮政号码
        /// </summary>
        public string PostalNO { get; set; }
        /// <summary>
        /// 公司联系电话
        /// </summary>
        public string CompanyTel { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Communication { get; set; }
        /// <summary>
        /// 身份号码
        /// </summary>
        public string IdenNo { get; set; }
        /// <summary>
        /// 工商注册类型
        /// </summary>
        public string EntRegist { get; set; }
        /// <summary>
        /// 住宅电话
        /// </summary>
        public string HouseholdPhone { get; set; }
        /// <summary>
        /// 住宅地址
        /// </summary>
        public string EstateAddr { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public string CompType { get; set; }
        /// <summary>
        /// 公司规模
        /// </summary>
        public string CompSize { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        public string Industry { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        public string CompDepart { get; set; }
        /// <summary>
        /// 职位级别
        /// </summary>
        public string CompPosiLevel { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string CompPosi { get; set; }
        /// <summary>
        /// 是否发送消息
        /// </summary>
        public bool IsSendMsg { get; set; }
        /// <summary>
        /// 销售团队
        /// </summary>
        public int SalesTeam { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public int PersonId { get; set; }

    }
}

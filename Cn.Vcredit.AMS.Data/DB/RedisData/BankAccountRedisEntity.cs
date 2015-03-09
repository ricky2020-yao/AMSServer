using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.RedisData
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014-11-24
    /// Description:银行账户（由贷后创建和维护）
    /// </summary>
    public class BankAccountRedisEntity
    { /// <summary>
        /// 公司编号
        /// </summary>
        public int BankAccountID { get; set; }

        /// <summary>
        /// 子公司Key
        /// </summary>
        public string CompanyKey { get; set; }

        /// <summary>
        /// 银行Key
        /// </summary>
        public string BankKey { get; set; }

        /// <summary>
        /// 银行支行名
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 银行户名
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// 储蓄卡号
        /// </summary>
        public string SavingCard { get; set; }

        /// <summary>
        /// 协议编号
        /// </summary>
        public string ProtocolNo { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 地区Key
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 地区代码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplicationNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 付款账号
        /// </summary>
        public string PaymentAccount { get; set; }

        /// <summary>
        ///是否开启
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 公司类型
        /// </summary>
        public byte CompanyType { get; set; }
    }
}

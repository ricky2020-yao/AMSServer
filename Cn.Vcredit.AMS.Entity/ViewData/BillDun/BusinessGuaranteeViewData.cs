using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.BillDun
{
    /// <summary>
    /// 担保诉讼设置
    /// </summary>
    public class BusinessGuaranteeViewData
    {/// <summary>
        /// 订单号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdenNo { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 拖欠金额
        /// </summary>
        public decimal OverAmount { get; set; }

        /// <summary>
        /// 逾期月数
        /// </summary>
        public string OverdueTag { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public byte BusinessStatus { get; set; }

        /// <summary>
        /// 转担保日期
        /// </summary>
        public DateTime? ToGuaranteeTime { get; set; }

        /// <summary>
        /// 诉讼状态
        /// </summary>
        public byte LawsuitStatus { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 担保方
        /// </summary>
        public string GuaranteeSideKey { get; set; }

        /// <summary>
        /// 服务方
        /// </summary>
        public string ServiceSideKey { get; set; }

        /// <summary>
        /// 放贷方
        /// </summary>
        public string LendingSideKey { get; set; }

        /// <summary>
        /// 放贷方名称
        /// </summary>
        public string LendingSideKeyName { get; set; }

        /// <summary>
        /// 担保批次
        /// </summary>
        public string GuaranteeNum { get; set; }

        /// <summary>
        /// 本息欠费次数
        /// </summary>
        public int CapitalCnt { get; set; }

        /// <summary>
        /// 欠费最小月
        /// </summary>
        public string MinMonth { get; set; }

        /// <summary>
        /// 欠费最大月
        /// </summary>
        public string MaxMonth { get; set; }

        /// <summary>
        /// 自然清贷日
        /// </summary>
        public string ExpireDate { get; set; }

        /// <summary>
        /// 欠费月份
        /// </summary>
        public string DueMonth
        {
            get
            {
                if (string.IsNullOrEmpty(MinMonth) || string.IsNullOrEmpty(MaxMonth))
                    return string.Empty;
                return MinMonth + "-" + MaxMonth;
            }
            set
            {
            }
        }

        /// <summary>
        /// 订单状态名称
        /// </summary>
        public string StrBusinessStatus
        {
            get
            {
                return BusinessStatus.ValueToDesc<EnumBusinessStatus>();
            }
            set
            {
            }
        }

        /// <summary>
        /// 担保日期
        /// </summary>
        public string StrToGuaranteeTime
        {
            get
            {
                if (ToGuaranteeTime == null)
                    return string.Empty;
                return ToGuaranteeTime.ToDateString();
            }
            set
            {
            }
        }

        /// <summary>
        /// 诉讼状态名称
        /// </summary>
        public string StrLawsuitStatus
        {
            get
            {
                return LawsuitStatus.ValueToDesc<EnumLawsuitStatus>();
            }
            set
            {
            }
        }
    }
}

using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Dictionary
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:科目字典目录
    /// </summary>
    public class SubjectDictionary
    {
        #region- 字段属性 -
        /// <summary>
        /// 无抵押贷款款项字典
        /// </summary>
        public Dictionary<byte, string> UnsecuredDic = new Dictionary<byte, string>
		{
			{(byte)EnumCostSubject.Capital, EnumCostSubject.Capital.ToDescription()},
			{(byte)EnumCostSubject.Interest, EnumCostSubject.Interest.ToDescription()},
			{(byte)EnumCostSubject.ServiceFee, EnumCostSubject.ServiceFee.ToDescription()},
			{(byte)EnumCostSubject.GuaranteeFee, EnumCostSubject.GuaranteeFee.ToDescription()},
			{(byte)EnumCostSubject.InterestBuckleFail, EnumCostSubject.InterestBuckleFail.ToDescription()},
			{(byte)EnumCostSubject.ServiceBuckleFail, EnumCostSubject.ServiceBuckleFail.ToDescription()},
			{(byte)EnumCostSubject.PunitiveInterest, EnumCostSubject.PunitiveInterest.ToDescription()}
		};

        /// <summary>
        /// 成都无抵押贷款款项字典
        /// </summary>
        public Dictionary<byte, string> ChengDuUnsecuredDic = new Dictionary<byte, string>
		{
			{(byte)EnumCostSubject.Capital, EnumCostSubject.Capital.ToDescription()},
			{(byte)EnumCostSubject.Interest, EnumCostSubject.Interest.ToDescription()},
			{(byte)EnumCostSubject.ServiceFee, "月服务费"},
			{(byte)EnumCostSubject.InterestBuckleFail, EnumCostSubject.InterestBuckleFail.ToDescription()},
			{(byte)EnumCostSubject.ServiceBuckleFail, "服务费扣失"},
			{(byte)EnumCostSubject.PunitiveInterest, "延迟支付违约金"}
		};

        public Dictionary<byte, string> FundFromGKHChengDuULoanDic = new Dictionary<byte, string>
        {
            {(byte)EnumCostSubject.Capital, EnumCostSubject.Capital.ToDescription()},
			{(byte)EnumCostSubject.Interest, EnumCostSubject.Interest.ToDescription()},
            {(byte)EnumCostSubject.Manage, EnumCostSubject.Manage.ToDescription()},
			{(byte)EnumCostSubject.ServiceFee, "月服务费"},
			{(byte)EnumCostSubject.InterestBuckleFail, EnumCostSubject.InterestBuckleFail.ToDescription()},
			{(byte)EnumCostSubject.ServiceBuckleFail, "服务费扣失"},
			{(byte)EnumCostSubject.PunitiveInterest, EnumCostSubject.PunitiveInterest.ToDescription()}
        };

        public Dictionary<byte, string> JingAnUnMortgageLoanDic = new Dictionary<byte, string>
        {
            {(byte)EnumCostSubject.Capital, EnumCostSubject.Capital.ToDescription()},
			{(byte)EnumCostSubject.Interest, EnumCostSubject.Interest.ToDescription()},
            {(byte)EnumCostSubject.Manage, EnumCostSubject.Manage.ToDescription()},
			{(byte)EnumCostSubject.ServiceFee, "月服务费"},
			{(byte)EnumCostSubject.InterestBuckleFail, EnumCostSubject.InterestBuckleFail.ToDescription()},
			{(byte)EnumCostSubject.ServiceBuckleFail, "服务费扣失"},
			{(byte)EnumCostSubject.PunitiveInterest, EnumCostSubject.PunitiveInterest.ToDescription()},
            {(byte)EnumCostSubject.Procedures, "手续费"}
        };

        public Dictionary<byte, string> JingAnMortgageLoanDic = new Dictionary<byte, string>
        {
            {(byte)EnumCostSubject.Capital, EnumCostSubject.Capital.ToDescription()},
			{(byte)EnumCostSubject.Interest, EnumCostSubject.Interest.ToDescription()},
			{(byte)EnumCostSubject.ServiceFee, "月服务费"},
			{(byte)EnumCostSubject.InterestBuckleFail, EnumCostSubject.InterestBuckleFail.ToDescription()},
			{(byte)EnumCostSubject.ServiceBuckleFail, "服务费扣失"},
			{(byte)EnumCostSubject.PunitiveInterest, EnumCostSubject.PunitiveInterest.ToDescription()},
            {(byte)EnumCostSubject.Procedures, "手续费"},
            {(byte)EnumCostSubject.Earnest, "保证金"}
        };

        /// <summary>
        /// 成都无抵押贷款款项字典
        /// </summary>
        public Dictionary<byte, string> CarMortgageLoanDic = new Dictionary<byte, string>
		{
			{(byte)EnumCostSubject.Capital, "融资金额"},
			{(byte)EnumCostSubject.Interest, "租金成本"},
			{(byte)EnumCostSubject.InterestBuckleFail, "扣失"},
			{(byte)EnumCostSubject.PunitiveInterest, "违约金"}
		};

        #endregion
    }
}

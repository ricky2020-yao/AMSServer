using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.Components.MyOle2.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.ViewData.FinanceManage
{
    /// <summary>
    /// Author:ricky
    /// CreateTime:2014年11月13日
    /// Description:订单详情的返回结果类
    /// </summary>
    public class BusinessDetailViewData
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 身份号码
        /// </summary>
        public string IdentityNo { get; set; }
        /// <summary>
        /// 产品类型（[Net枚举] 1、融资租赁 2、车辆抵押贷款  
        /// 3、房屋抵押贷款 4、小额贷款 5、成都小额贷款 6、陆金所贷款）
        /// </summary>
        public byte ProductType { get; set; }
        /// <summary>
        /// 贷款本金
        /// </summary>
        public decimal LoanCapital { get; set; }
        /// <summary>
        /// 贷款期数
        /// </summary>
        public int LoanPeriod { get; set; }
        /// <summary>
        /// 放贷日
        /// </summary>
        public DateTime LoanTime { get; set; }
        /// <summary>
        /// 业务状态（[Net枚举]：1、正常 2、担保 3、诉讼）
        /// </summary>
        public byte BusinessStatus { get; set; }
        /// <summary>
        /// 订单状态（客户类型）
        /// </summary>
        public string StrBusinessStatus { get; set; }
        /// <summary>
        /// 清贷状态（[Net枚举]：1、偿还中  2、满约清贷 3、提前清贷 4、坏帐清贷）
        /// </summary>
        public byte CLoanStatus { get; set; }
        /// <summary>
        /// 清贷状态
        /// </summary>
        public string StrCLoanStatus { get; set; }
        /// <summary>
        /// 诉讼执行状态([Net枚举]：1、未诉讼 2、诉讼中 3、诉讼完成 
        /// 4、申请执行 5、执行一次 6、执行二次)。
        /// </summary>
        public byte LawsuitStatus { get; set; }
        /// <summary>
        /// 诉讼状态（客户状态）
        /// </summary>
        public string StrLawsuitStatus { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 储蓄卡号
        /// </summary>
        public string SavingCard { get; set; }
        /// <summary>
        /// 储蓄用户
        /// </summary>
        public string SavingUser { get; set; }
        /// <summary>
        /// 信托方公司键名
        /// </summary>
        public string LendingSideKey { get; set; }
        /// <summary>
        /// 放贷方收款帐户编号
        /// </summary>
        public int LendingSideID { get; set; }
        /// <summary>
        /// 服务方公司键名
        /// </summary>
        public string ServiceSideKey { get; set; }
        /// <summary>
        /// 服务方收款帐户编号
        /// </summary>
        public int ServiceSideID { get; set; }
        /// <summary>
        /// 担保方公司键名
        /// </summary>
        public string GuaranteeSideKey { get; set; }
        /// <summary>
        /// 担保方收款帐户编号
        /// </summary>
        public int GuaranteeSideID { get; set; }
        /// <summary>
        /// 欠费金额（不含当期）
        /// </summary>
        public decimal OverAmount { get; set; }
        /// <summary>
        /// 当期欠费总金额
        /// </summary>
        public decimal CurrentOverAmount { get; set; }
        /// <summary>
        /// 其他金额(担保-诉讼金额)
        /// </summary>
        public decimal OtherAmount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 产品种类
        /// </summary>
        public string LoanKind { get; set; }
        /// <summary>
        /// 产品种类
        /// </summary>
        public string LoanKindName { get; set; }
        /// <summary>
        /// 转担保时间
        /// </summary>
        public DateTime? ToGuaranteeTime { get; set; }

        /// <summary>
        /// 转诉讼时间
        /// </summary>
        public DateTime? ToLitigationTime { get; set; }        
        /// <summary>
        /// 清贷时间
        /// </summary>
        public DateTime? ClearLoanTime { get; set; }
        /// <summary>
        /// 是否在偿还中
        /// </summary>
        public bool IsRepayment { get; set; }
        /// <summary>
        /// 冻结批次号
        /// </summary>
        public string FrozenNo { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductKind { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 地区名称
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 本金利率
        /// </summary>
        public decimal CapitalRate { get; set; }
        /// <summary>
        /// 保证金率
        /// </summary>
        public decimal DepositRate { get; set; }
        /// <summary>
        /// 手续费率
        /// </summary>
        public decimal ProceduresRate { get; set; }
        /// <summary>
        /// 管理费费率
        /// </summary>
        public decimal ManagementRate { get; set; }
        /// <summary>
        /// 月利率
        /// </summary>
        public decimal InterestRate { get; set; }
        /// <summary>
        /// 月服务费率
        /// </summary>
        public decimal ServiceRate { get; set; }

        /// <summary>
        /// 分公司
        /// </summary>
        public string BranchKey { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string BranchKeyName { get; set; }
        /// <summary>
        /// 最近的一次关帐时间
        /// </summary>
        public DateTime LatestTime { get; set; }


        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}{1}", BusinessID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", CustomerID.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", CustomerName, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", IdentityNo, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ProductType.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", LoanCapital.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", LoanPeriod.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", LoanTime.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BusinessStatus.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", StrBusinessStatus, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", CLoanStatus.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", StrCLoanStatus, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", LawsuitStatus.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", StrLawsuitStatus, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ContractNo, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", SavingCard, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", SavingUser, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", LendingSideKey, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", LendingSideID, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ServiceSideKey, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ServiceSideID, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", GuaranteeSideKey, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", GuaranteeSideID, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", OverAmount.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", CurrentOverAmount.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", OtherAmount.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", LoanKind, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", LoanKindName, WebServiceConst.Separater_1);            
            if (ToGuaranteeTime.HasValue)
                sb.AppendFormat("{0}{1}", ToGuaranteeTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);

            if (ToLitigationTime.HasValue)
                sb.AppendFormat("{0}{1}", ToLitigationTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);

            if (ClearLoanTime.HasValue)
                sb.AppendFormat("{0}{1}", ClearLoanTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            else
                sb.AppendFormat("{0}{1}", "", WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", IsRepayment.ToString(), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", FrozenNo, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ProductKind, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", LatestTime.ToString("yyyy-MM-dd HH:mm:ss"), WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", CapitalRate, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", DepositRate, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ProceduresRate, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ManagementRate, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", InterestRate, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", ServiceRate, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BranchKey, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", BranchKeyName, WebServiceConst.Separater_1);
            sb.AppendFormat("{0}{1}", Region, WebServiceConst.Separater_1);
            sb.Append(RegionName);

            return sb.ToString();
        }

        /// <summary>
        /// 解析成实体类
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static BusinessDetailViewData  ToEntity(string strValue)
        {
            if(string.IsNullOrEmpty(strValue))
                return null;

            string[] lines = strValue.Split(WebServiceConst.Separater_1.ToArray(), StringSplitOptions.None);

            PropertyInfo[] proInfos = typeof(BusinessDetailViewData).GetProperties();

            if (lines == null || proInfos == null || lines.Length != proInfos.Length)
                return null;

            int index = 0;
            var data = new BusinessDetailViewData();
            int businessID = 0;
            int customerID = 0;
            byte productType = 0;
            decimal loanCapital = 0;
            int loanPeriod = 0;
            byte businessStatus = 0;
            byte cLoanStatus = 0;
            byte lawsuitStatus = 0;
            decimal overAmount = 0;
            decimal currentOverAmount = 0;
            decimal otherAmount = 0;
            decimal capitalRate = 0;
            decimal depositRate = 0;
            decimal proceduresRate = 0;
            decimal managementRate = 0;
            decimal interestRate = 0;
            decimal serviceRate = 0;
            int serviceSideID = 0;
            int lendingSideID = 0;
            int guaranteeSideID = 0;
            DateTime createTime = DateTime.Now;
            DateTime loanTime = DateTime.Now;
            DateTime toGuaranteeTime = DateTime.Now;
            DateTime toLitigationTime = DateTime.Now;
            DateTime clearLoanTime = DateTime.Now;
            DateTime latestTime = DateTime.Now;
            bool isRepayment = false;

            int.TryParse(lines[index++], out businessID);
            data.BusinessID = businessID;

            int.TryParse(lines[index++], out customerID);
            data.CustomerID = customerID;

            data.CustomerName = lines[index++];
            data.IdentityNo = lines[index++];

            byte.TryParse(lines[index++], out productType);
            data.ProductType = productType;

            decimal.TryParse(lines[index++], out loanCapital);
            data.LoanCapital = loanCapital;

            int.TryParse(lines[index++], out loanPeriod);
            data.LoanPeriod = loanPeriod;

            DateTime.TryParse(lines[index++], out loanTime);
            data.LoanTime = loanTime;

            byte.TryParse(lines[index++], out businessStatus);
            data.BusinessStatus = businessStatus;
            data.StrBusinessStatus = lines[index++];

            byte.TryParse(lines[index++], out cLoanStatus);
            data.CLoanStatus = cLoanStatus;
            data.StrCLoanStatus = lines[index++];

            byte.TryParse(lines[index++], out lawsuitStatus);
            data.LawsuitStatus = lawsuitStatus;
            data.StrLawsuitStatus = lines[index++];
            data.ContractNo = lines[index++];
            data.SavingCard = lines[index++];
            data.SavingUser = lines[index++];

            data.LendingSideKey = lines[index++];
            int.TryParse(lines[index++], out lendingSideID);
            data.LendingSideID = lendingSideID;

            data.ServiceSideKey = lines[index++];
            int.TryParse(lines[index++], out serviceSideID);
            data.ServiceSideID = serviceSideID;

            data.GuaranteeSideKey = lines[index++];
            int.TryParse(lines[index++], out guaranteeSideID);
            data.GuaranteeSideID = guaranteeSideID;

            decimal.TryParse(lines[index++], out overAmount);
            data.OverAmount = overAmount;

            decimal.TryParse(lines[index++], out currentOverAmount);
            data.CurrentOverAmount = currentOverAmount;

            decimal.TryParse(lines[index++], out otherAmount);
            data.OtherAmount = otherAmount;

            DateTime.TryParse(lines[index++], out createTime);
            data.CreateTime = createTime;

            data.LoanKind = lines[index++];
            data.LoanKindName = lines[index++];

            if (DateTime.TryParse(lines[index++], out toGuaranteeTime))
                data.ToGuaranteeTime = toGuaranteeTime;

            if (DateTime.TryParse(lines[index++], out toLitigationTime))
                data.ToLitigationTime = toLitigationTime;

            if (DateTime.TryParse(lines[index++], out clearLoanTime))
                data.ClearLoanTime = clearLoanTime;

            bool.TryParse(lines[index++], out isRepayment);
            data.IsRepayment = isRepayment;

            data.FrozenNo = lines[index++];
            data.ProductKind = lines[index++];

            DateTime.TryParse(lines[index++], out latestTime);
            data.LatestTime = latestTime;

            decimal.TryParse(lines[index++], out capitalRate);
            data.CapitalRate = capitalRate;
            decimal.TryParse(lines[index++], out depositRate);
            data.DepositRate = depositRate;
            decimal.TryParse(lines[index++], out proceduresRate);
            data.ProceduresRate = proceduresRate;
            decimal.TryParse(lines[index++], out managementRate);
            data.ManagementRate = managementRate;
            decimal.TryParse(lines[index++], out interestRate);
            data.InterestRate = interestRate;
            decimal.TryParse(lines[index++], out serviceRate);
            data.ServiceRate = serviceRate;

            data.BranchKey = lines[index++];
            data.BranchKeyName = lines[index++];
            data.Region = lines[index++];
            data.RegionName = lines[index++];

            return data;            
        }
    }
}

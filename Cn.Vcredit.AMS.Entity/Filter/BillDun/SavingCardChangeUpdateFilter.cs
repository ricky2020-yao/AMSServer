using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Filter.BillDun
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月29日
    /// Description:银行卡号更改
    /// </summary>
    public class SavingCardChangeUpdateFilter:BaseFilter
    {
        /// <summary>
        /// 变更编号
        /// </summary>
        public int BCChangeID { get; set; }
        /// <summary>
        /// 隐藏的变更编号
        /// </summary>
        public int HidBCChangeID { get; set; }
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// 原银行卡用户
        /// </summary>
        public string OriginalUser { get; set; }

        /// <summary>
        /// 原银行卡号
        /// </summary>
        public string OriginalCard { get; set; }

        /// <summary>
        /// 更变后银行卡用户
        /// </summary>
        public string ChangeUser { get; set; }

        /// <summary>
        /// 变更后的银行卡号
        /// </summary>
        public string ChangeCard { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更变后银行名
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 更变后银行键值
        /// </summary>
        public string BankKey { get; set; }

        /// <summary>
        /// 状态（[Net枚举]：1、新建 2、失败 3、成功 4、其他）
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// 附件名
        /// </summary>
        public string MeanName { get; set; }

        /// <summary>
        /// 附件地址
        /// </summary>
        public string MeanPath { get; set; }

        /// <summary>
        /// 老银行支行
        /// </summary>
        public string OldSubBranch { get; set; }
        /// <summary>
        /// 银行支行
        /// </summary>
        public string SubBranch { get; set; }

        /// <summary>
        /// 老银行名称
        /// </summary>
        public string OldBankName { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public int OperatorUser { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
    }
}

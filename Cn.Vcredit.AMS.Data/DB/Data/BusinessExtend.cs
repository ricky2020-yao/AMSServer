using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:订单扩展表
    /// </summary>
    public class BusinessExtend
    {
        #region- 基本属性 -
        /// <summary>
        /// 业务编号
        /// </summary>
        public int BusinessID { get; set; }

        /// <summary>
        /// 业务逻辑编号（1、融资租赁2、沪苏车贷 3、房屋抵押贷款 4、外贸小贷  5、成都小贷 6、陆金所贷款）
        /// </summary>
        public byte BusinessLogicID { get; set; }

        /// <summary>
        /// 建行号（停用——银扣使用）
        /// </summary>
        public string ConstructionBankNo { get; set; }

        /// <summary>
        /// 建行号（已废弃）
        /// </summary>
        public string ConstructSedNo { get; set; }

        /// <summary>
        /// 签约照片（催收单使用）
        /// </summary>
        public string DucImgPath { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public int SalesManID { get; set; }

        /// <summary>
        /// 二次营销方式
        /// </summary>
        public int SecondSales { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        public int FromSource { get; set; }

        /// <summary>
        /// 销售渠道
        /// </summary>
        public int SaleMode { get; set; }

        /// <summary>
        /// 保证金
        /// </summary>
        public decimal EarnestAmt { get; set; }

        /// <summary>
        /// 保险费(适用融资租赁)
        /// </summary>
        public decimal Premium { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal ProceduresAmout { get; set; }

        /// <summary>
        /// 保证金率
        /// </summary>
        public decimal DepositRate { get; set; }

        /// <summary>
        /// 手续费率
        /// </summary>
        public decimal ProceduresRate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Updatetime { get; set; }

        #endregion

        #region- 扩展属性 -
        #endregion
    }
}

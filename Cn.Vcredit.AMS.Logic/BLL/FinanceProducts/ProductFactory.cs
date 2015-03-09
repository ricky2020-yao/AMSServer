using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.BLL.FinanceProducts
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description：维信产品工厂
    /// </summary>
    public class ProductFactory
    {
        #region- 构造函数 -
        private ProductFactory() { }
        #endregion

        #region- 字段属性 -
        public static readonly ProductFactory Instance = new ProductFactory();
        #endregion

        #region- 功能函数 -
        /// <summary>
        /// 设置业务信息，获取产品操作类
        /// </summary>
        /// <param name="businessLogicID">业务对象</param>
        /// <returns>返回产品操作类</returns>
        public VcreditProduct GetProduct(byte businessLogicID)
        {
            switch (businessLogicID.ValueToEnum<EnumProductKind>())
            {
                // 外贸小贷
                case EnumProductKind.UnsecuredLoan:
                    return new UnsecuredLoan();
                // 成都小额贷款
                case EnumProductKind.ChengDuULoan:
                    return new ChengDuULoan();
                // 沪苏车贷
                case EnumProductKind.CarMortgageLoan:
                    return new CarMortgageLoan();
                // 外贸楼一贷
                case EnumProductKind.FoticBuildingLoan:
                    return new FoticBuildingMortgage();
                // 成都小贷国开行模式
                case EnumProductKind.FundFromGKHChengDuULoan:
                    return new FundFromGKHChengDuULoan();
                default:
                    return null;
            }
        }
        #endregion
    }
}

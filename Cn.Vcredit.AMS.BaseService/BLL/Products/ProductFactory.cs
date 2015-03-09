using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cn.Vcredit.Common.TypeConvert;

namespace Cn.Vcredit.AMS.BaseService.BLL.Products
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年6月5日
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
        /// <param name="business">业务对象</param>
        /// <returns>返回产品操作类</returns>
        public VcreditProduct GetProduct(Business business)
        {
            switch (business.ProductType.ValueToEnum<EnumProductKind>())
            {
                case EnumProductKind.UnsecuredLoan:
                    return new UnsecuredLoan(business);
                case EnumProductKind.ChengDuULoan:
                    return new ChengDuULoan(business);
                case EnumProductKind.CarMortgageLoan:
                    return new CarMortgageLoan(business);
                case EnumProductKind.FoticBuildingLoan:
                    return new FoticBuildingMortgage(business);

                case EnumProductKind.FundFromGKHChengDuULoan:
                    return new FundFromGKHChengDuULoan(business);

                case EnumProductKind.JingAnMortgageLoan:
                    return new JingAnMortgageLoan(business);
                case EnumProductKind.JingAnUnMortgageLoan:
                    return new JingAnUnMortgageLoan(business);

                default:
                    return null;
            }
        }
        #endregion
    }
}

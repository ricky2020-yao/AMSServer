using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cn.Vcredit.AMS.Data.DB.Data;

namespace Cn.Vcredit.AMS.BaseService.BLL.Products
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2012年6月4日
    /// Description:陆金所贷款款项计算类
    /// </summary>
    public class LuJinSuoLoan : VcreditProduct
    {
        #region- 构造函数 -
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="business"></param>
        public LuJinSuoLoan(Business business) : base(business) { }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public LuJinSuoLoan() { }
        #endregion

        #region- 功能函数 -
        /// <summary>
        /// 创建基本款项
        /// </summary>
        /// <param name="business">业务对象</param>
        /// <returns>返回款项集合</returns>
        public override List<BillItem> CreateBaseItems(Business business = null)
        {
            return null;
        }

        /// <summary>
        /// 获取该产品所有款项编号、款项名称
        /// </summary>
        /// <returns>返回款项字典</returns>
        public override Dictionary<byte, string> GetProductItems()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

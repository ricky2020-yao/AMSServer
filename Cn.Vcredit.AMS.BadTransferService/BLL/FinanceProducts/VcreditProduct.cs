using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BadTransferService.BLL.FinanceProducts
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:产品类的基类
    /// </summary>
    public abstract class VcreditProduct
    {
        #region- 字段属性 -
        /// <summary>
        /// 业务对象
        /// </summary>
        //protected Business Business { get; set; }

        /// <summary>
        /// 上月
        /// </summary>
        protected string PrevMonth = DateTime.Now.AddMonths(-1).ToBillMonthString();

        /// <summary>
        /// 当月
        /// </summary>
        protected string CurMonth = DateTime.Now.ToBillMonthString();
        #endregion

        #region- 构造函数 -
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="business"></param>
        //protected VcreditProduct(Business business)
        //{
        //    Business = business;
        //}
        #endregion

        #region- 抽象函数 -
        /// <summary>
        /// 获取所有产品款项
        /// </summary>
        /// <returns>返回款项字典</returns>
        public abstract Dictionary<byte, string> GetProductItems();
        #endregion
    }
}

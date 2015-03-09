using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Data.DB.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月5日
    /// Description:客户信息类
    /// </summary>
    public class CustomerData
    {
        /// <summary>
        /// 客户编号
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public int PersonId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdenNo { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Mobile { get; set; }
    }
}

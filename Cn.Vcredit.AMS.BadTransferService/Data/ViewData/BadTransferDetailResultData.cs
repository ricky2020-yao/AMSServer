using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BadTransferService.Data.ViewData
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:坏账清贷检索详情返回结果用数据
    /// </summary>
    public class BadTransferDetailResultData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BadTransferDetailResultData() { }

        /// <summary>
        /// 款项ID
        /// </summary>
        public byte SubjectId { get; set; }

        /// <summary>
        /// 款项名称
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// 款项欠费
        /// </summary>
        public decimal SubjectValue { get; set; }

        /// <summary>
        /// 款项类型
        /// 1：欠费账单(当期+逾期)
        /// 2：其它费用
        /// </summary>
        public byte SubjectType { get; set; }

        /// <summary>
        /// 排列的顺序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 凭证文件保存路径
        /// </summary>
        public string Path { get; set; }

    }
}

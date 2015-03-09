using Cn.Vcredit.AMS.Common.Data;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:银行扣收相关数据操作类
    /// </summary>
    public class PayBankDal:BaseDao
    {
        /// <summary>
        /// 获取银行扣收数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<PayBankExportItem> GetPayBankExportItem(string sql)
        {
            return Query<PayBankExportItem>(sql, null, "PostLoanDB", System.Data.CommandType.Text, 60000);
        }

        /// <summary>
        /// 根据ServiceSideID获取扣款指令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<DeductCommand> GetDeductCommand(string sql)
        {
            return Query<DeductCommand>(sql, null, "PostLoanDB", System.Data.CommandType.Text, 60000);
        }
    }
}

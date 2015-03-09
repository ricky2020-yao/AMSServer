using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.Common.Tools;
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
    /// Description:固定日扣款顺序表相关数据操作类
    /// </summary>
    public class DeductSequenceDal:BaseDao
    {
        /// <summary>
        /// 根据扣款序列类型获取固定日扣款顺序
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public List<DeductSequence> GetDeductSequenceList(EnumDeductSeqKind kind)
        {
            string sql = "SELECT * FROM [dbo].[DeductSequence] WHERE DSeqType = {0} ORDER BY DeductTime DESC".StringFormat((byte)kind);

            return Query<DeductSequence>(sql, null, "PostLoanDB", System.Data.CommandType.Text, 60000);
        }
    }
}

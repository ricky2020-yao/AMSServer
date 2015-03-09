using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataAccess.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月29日
    /// Description:交易记录表数据操作类
    /// </summary>
    public class PayTraceDal:BaseDao
    {
        /// <summary>
        /// 插入交易记录
        /// </summary>
        /// <param name="payTrace"></param>
        /// <returns></returns>
        public bool InsertPayTrace(PayTrace payTrace)
        {
            string sql = "SQL\\PayTrace\\INSERT_PAYTRACE.sql".ToFileContent(true
                , payTrace.PayKind, payTrace.FileName, payTrace.DayNo
                , payTrace.Content, payTrace.CallDirection, payTrace.CallMethod
                , payTrace.RequestState, payTrace.PayTraceAmount, payTrace.OperatorID
                , payTrace.CompanyKey, payTrace.ServiceSideID, payTrace.LendingSideID
                , payTrace.TraceTime, payTrace.ResponseTime, payTrace.ResponseNum
                , payTrace.ResponseDesc, payTrace.LockKey, payTrace.FileNum
                , payTrace.RequestAmount, payTrace.AccountID, payTrace.IncomeType
                , payTrace.PeriodType, payTrace.TaskKey, payTrace.PayReason);

            return Execute(sql, null, "PostLoanDB", System.Data.CommandType.Text) > 0 ? true:false;
        }
    }
}

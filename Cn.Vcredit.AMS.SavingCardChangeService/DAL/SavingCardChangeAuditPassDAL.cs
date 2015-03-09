using Cn.Vcredit.AMS.BaseService.DAL;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.AMS.Entity.Filter.BillDun;
using Cn.Vcredit.Common.TypeConvert;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.SavingCardChangeService.DAL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月21日
    /// Description:储蓄卡变更审批通过数据层操作类
    /// </summary>
    public class SavingCardChangeAuditPassDAL:BaseUpdateDAL
    {
        /// <summary>
        /// 获取指定如何解释命令字符串
        /// </summary>
        /// <returns></returns>
        protected override CommandType GetCommandType()
        {
            return CommandType.Text;
        }

        /// <summary>
        /// 获取检索数据的Sql文
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        protected override string GetUpdateSql(BaseFilter baseFilter)
        {
            SavingCardChangeUpdateFilter filter = baseFilter as SavingCardChangeUpdateFilter;
            if (filter == null)
                return "";

            string sql = "Services\\SQL\\SavingCardChange\\INSERT_SAVINGCARDCHANGE_HISTORY_AUDIT.sql";

            sql = sql.ToFileContent(true
                , filter.BusinessID
                , filter.CustomerID
                , filter.OriginalCard
                , filter.ChangeCard
                , filter.Status
                , filter.MeanName
                , filter.MeanPath
                , filter.OriginalUser
                , filter.ChangeUser
                , filter.BankName
                , filter.SubBranch
                , filter.OldSubBranch
                , filter.OldBankName
                , filter.OperatorUser
                , filter.BankKey);

            return sql;
        }
    }
}

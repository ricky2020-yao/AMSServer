using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Consts
{
    /// <summary>
    /// SQL文件
    /// </summary>
    public class SqlFile
    {
        public const string Sql_Update_Business_Due = "SQL\\Update_Business_Due.sql";

        public const string PayBankSqlServer = "SQL\\BankExport\\PayBankService.sql";

        public const string PayBankSqlDWJM = "SQL\\BankExport\\PayBankDWJM.sql";

        public const string PayBankSqlBH = "SQL\\BankExport\\PayBankBH.sql";

        public const string PayBankSqlChengdu = "SQL\\BankExport\\PayBankChengdu.sql";

        public const string PayBankSqlBusinessLock = "SQL\\BankExport\\PayBankBusinessLock.sql";

        public const string BusinessAndCustomerSQL = "SQL\\NewDataExchange.sql";

        public const string Sql_Select_Bill_BillItem_Received = "SQL\\SELECT_BILL_BILLITEM_RECEIVED.sql";

        public const string AppointedCustomerSQL = "SQL\\AppointedCustomer.sql";
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Cn.Vcredit.Common.Excel.ByteUtil;

namespace Cn.Vcredit.Common.Excel
{
    internal class FormulaRecord : Record
    {
        internal Record StringRecord = null;

        internal FormulaRecord(Record formulaRecord, Record stringRecord)
            : base()
        {
            _rid = formulaRecord.RID;
            _data = formulaRecord.Data;
            _continues = formulaRecord.Continues;

            StringRecord = stringRecord;
        }
    }
}

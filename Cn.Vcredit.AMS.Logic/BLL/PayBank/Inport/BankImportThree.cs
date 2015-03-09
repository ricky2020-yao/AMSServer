using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Cn.Vcredit.AMS.Data.DB.Data;

namespace Cn.Vcredit.AMS.Logic.BLL.PayBank.Inport
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年6月8日
    /// 导入模版三（适用 成都维仕）
    /// 格式为：1|6227003813340139977{1}|陈永兵{2}|1093.33{3}|成功{4}|交易成功|其它|
    /// </summary>
    public class BankImportThree : ImportTemplate
    {
        #region- 构造函数 -
        public BankImportThree(DeductCommand deductcommand)
            :base(deductcommand)
        { 
            
        }
        #endregion

        #region- 功能函数 -
        public override List<BaseImportItem> MatchBillItems
                (Stream stream, out string ErrorMessage)
        {
            return base.GetSourceFomart(stream, "|", "成功", out ErrorMessage, 1, 2, 3, 4, 6, 0, 7, 1);
        }
        #endregion
    }
}

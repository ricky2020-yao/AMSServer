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
    /// CreateTime:2012年11月9日
    /// 导入模版一（适用 杭州光大）
    /// 格式为：储蓄卡号{0}|姓名{1}|金额{2}|成功{3}
    /// </summary>
    public class BankImportFive : ImportTemplate
    {
        #region- 构造函数 -
        public BankImportFive(DeductCommand deductcommande)
            : base(deductcommande)
        {

        }
        #endregion

        #region- 功能函数 -
        public override List<BaseImportItem> MatchBillItems(Stream stream, out string errormessage)
        {
            return GetSourceFomartForHZ(stream, out errormessage, 0, 2, 1, 3, 4, 5, "成功", 6);
        }
        #endregion
    }
}

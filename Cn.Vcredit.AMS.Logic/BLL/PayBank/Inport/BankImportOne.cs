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
    /// 导入模版一（适用 上海维信外贸服务方）
    /// 格式为：储蓄卡号{0}|姓名{1}|金额{2}|成功{3}|合同号{4}
    /// </summary>
    public class BankImportOne : ImportTemplate
    {
        #region- 构造函数 -
        public BankImportOne(DeductCommand deductcommande)
            : base(deductcommande)
        {

        }
        #endregion

        #region- 功能函数 -
        public override List<BaseImportItem> MatchBillItems(Stream stream, out string errormessage)
        {
            return GetSourceFomart(stream, "|", "成功", out errormessage, 0, 1, 2, 3, 4, 0, 5, 0);
        }
        #endregion
    }
}

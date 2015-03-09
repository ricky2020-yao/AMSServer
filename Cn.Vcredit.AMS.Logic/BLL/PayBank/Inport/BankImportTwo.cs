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
    /// 导入模版二（适用 上海维视渤海、上海维视外贸、上海维信外贸信托方）
    /// 格式为：序号|付款账号{1}|付款人姓名{2}|金额{3}|摘要|交易结果{5}|失败原因|备注一|备注二|
    ///         序号|付款账号{1}|付款人姓名{2}|金额{3}|合同号|交易结果{5}|失败原因|备注一|备注二|
    /// </summary>
    public class BankImportTwo : ImportTemplate
    {
        #region- 构造函数 -
        public BankImportTwo(DeductCommand deductcommand)
            :base(deductcommand)
        { 
            
        }
        #endregion

        #region- 功能函数 -
        public override List<BaseImportItem> MatchBillItems(Stream stream, out string errormessage)
        {
            return base.GetSourceFomart(stream, "|", "成功", out errormessage, 1, 2, 3, 5, 4, 7, 10, 1);
        }
        #endregion
    }
}

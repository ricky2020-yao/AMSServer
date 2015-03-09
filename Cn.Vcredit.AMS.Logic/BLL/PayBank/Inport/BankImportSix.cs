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
    /// 导出模版四（适用杭州工行外贸）
    /// 格式为：指令序号|缴费编号|缴费种类  |协议编号 | 币种 | 收费账号  | 收费单位名称 |付方卡/账号  | 付方户名 |应扣金额 |实扣金额 |用途  | 指令状态 银行反馈  | 自定义序号  | 付款手机通知号码  | 是否发送短信 
    /// HQG70623732-1|330102198507182722|消费信托贷款利息|BDP300078624|RMB|1202050619900014129|杭州维仕金融服务有限公司|6222021202036232917【７】|徐傅怡【８】|600.00元【９】|【１０】|服务担保费|银行拒绝[4102]余额不足|||否
    /// </summary>
    public class BankImportSix : ImportTemplate
    {
        #region- 构造函数 -
        public BankImportSix(DeductCommand deductcommand)
            :base(deductcommand)
        { 
            
        }
        #endregion 

        #region- 功能函数 -
        public override List<BaseImportItem> MatchBillItems(Stream stream, out string ErrorMessage)
        {
            return base.GetSourceFomart(stream, "|", "处理成功", out ErrorMessage, 7, 8, 10, 12, 11, 0, 16, 5, "元");
        }
        #endregion 
    }
}

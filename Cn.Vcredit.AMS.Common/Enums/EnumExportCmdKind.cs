using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Enums
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年6月8日
    /// Description:扣款指令导出格式类型
    /// </summary>
    public enum EnumExportCmdKind : byte
    {
        /// <summary>
        /// 建行号|储蓄卡号|姓名|金额
        /// </summary>
        [Description("建行号|储蓄卡号|姓名|金额")]
        TemplateOne = 10,

        /// <summary>
        /// 储蓄卡号|姓名|金额|合同号|款项名称|本金
        /// </summary>
        [Description("储蓄卡号|姓名|金额|合同号|款项名称|本金")]
        TemplateTwo = 20,

        /// <summary>
        /// 储蓄卡号|姓名|身份证|金额|款项
        /// </summary>
        [Description("储蓄卡号|姓名|身份证|金额|款项")]
        TemplateThree = 30,

        /// <summary>
        /// 储蓄卡号|姓名|金额|
        /// </summary>
        [Description("储蓄卡号|姓名|金额|")]
        TemplateFour = 40,

        /// <summary>
        /// 杭州维仕工行Excel格式\渤海工行Excel格式
        /// </summary>
        [Description("杭州维仕工行Excel格式|渤海工行Excel格式")]
        TemplateFive = 50,

        /// <summary>
        /// Excel服务方明细格式
        /// </summary>
        [Description("Excel服务方明细格式")]
        TemplateSix = 60,

        /// <summary>
        /// 杭州维仕光大TxT格式
        /// </summary>
        [Description("杭州维仕光大TxT格式")]
        TemplateSeven = 70,

        /// <summary>
        /// Excel信托方明细格式
        /// </summary>
        [Description("Excel信托方明细格式")]
        TemplateEight = 80,

        /// <summary>
        /// 币种|顺序号|协议编号|收款单位名称|付款金额|用途|备注|付款账户短信通知手机号|自定义序号|是否工行账号|付方账号开户行行号
        /// </summary>
        [Description("币种|顺序号|协议编号|收款单位名称|付款金额|用途|备注|付款账户短信通知手机号|自定义序号|是否工行账号|付方账号开户行行号")]
        TemplateNine = 90,

        /// <summary>
        /// 杭州外贸Excel服务方明细
        /// </summary>
        [Description("杭州外贸Excel服务方明细")]
        TemplateTen = 100,
    }
}

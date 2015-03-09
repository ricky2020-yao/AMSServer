using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.DataExportService.Common
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// 获取调整类型名称
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static string GetReviceTypeName(EnumAdjustKind kind)
        {
            string Exite = "";
            switch (kind)
            {
                case EnumAdjustKind.WriteOff://冲销(正数/负数) 减免(正数/负数)
                case EnumAdjustKind.Mitigation://
                    Exite = "追讨";
                    break;
                case EnumAdjustKind.BadDebts://坏账
                    Exite = "坏账";
                    break;
                case EnumAdjustKind.Transfer: //转账
                case EnumAdjustKind.Fuiou://富友
                case EnumAdjustKind.TenPay://财付通
                case EnumAdjustKind.Correct://冲销(负数)
                case EnumAdjustKind.ReturnBack://退回
                    Exite = "补交";
                    break;
                case EnumAdjustKind.BankSupport://银扣
                    Exite = "银扣";
                    break;
                default:
                    break;
            }
            return Exite;
        }

        /// <summary>
        /// 获取产品类型的描述
        /// </summary>
        /// <param name="productKind">产品类型枚举值</param>
        /// <returns></returns>
        public static string GetProductKindDesc(string productKind)
        {
            string type = "";
            switch (productKind)
            {
                case FinanceConstants.BUSINESS_PRODUCTKIND_CHEDAI:
                    type = "车辆抵押贷款";
                    break;
                case FinanceConstants.BUSINESS_PRODUCTKIND_WUDIYADAIKUAN:
                    type = "无抵押贷款";
                    break;
                default:
                    type = "";
                    break;
            }
            return type;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Cn.Vcredit.Common.Tools
{
    /// <summary>
    /// Author:王正吉
    /// CreateTime:2011年11月15日15:24:02
    /// Description:对象扩展类
    /// </summary>
    public static class ObjectExtension
    {

        #region(EnumExtension)
        /// <summary>
        /// 扩展方法 获取任意对象的Description属性
        /// </summary>
        public static string ToDescription(this object source)
        {
            FieldInfo fieldInfo = source.GetType().GetField(source.ToString());
            if (fieldInfo == null)
                return string.Empty;

            object[] da = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (da == null || da.Length == 0)
                return string.Empty;

            DescriptionAttribute descriptionAttribute = da[0] as DescriptionAttribute;
            if (descriptionAttribute != null)
                return descriptionAttribute.Description;

            return string.Empty;
        }

        /// <summary>
        /// 扩展方法 将枚举值转换成枚举名称
        /// </summary>
        public static string ToEnumName(this object source)
        {
            return Enum.GetName(source.GetType(), source);
        }

        /// <summary>
        /// 扩展方法 将枚举值转换成T枚举元素
        /// </summary>
        public static T ValueToEnum<T>(this object kind)
        {
            return (T)Enum.Parse(typeof(T), kind.ToString());
            //return (T)Enum.Parse(typeof(T), kind);
        }

        /// <summary>
        /// 扩展方法 将枚举值转换成描述内容
        /// </summary>
        public static string ValueToDesc<T>(this object val)
        {
            return val.ValueToEnum<T>().ToDescription();
        }

        /// <summary>
        /// 扩展方法 将枚举值转换成元素名称
        /// </summary>
        public static string ValueToName<T>(this object val)
        {
            return Enum.GetName(typeof(T), val);
        }

        /// <summary>
        /// Enum类型转换Dictionary<int, string>类型
        /// </summary>
        public static Dictionary<T, string> EnumToDictionary<T>(this Type type, bool isName = false)
        {
            Dictionary<T, string> dictionary = new Dictionary<T, string>();
            foreach (string name in Enum.GetNames(type))
            {
                object obj = Enum.Parse(type, name);
                if (isName)
                    dictionary.Add((T)obj, Enum.GetName(type, obj));
                else
                    dictionary.Add((T)obj, obj.ToDescription());
            }
            return dictionary;
        }

        /// <summary>
        /// Enum类型转换List<string>类型
        /// </summary>
        public static List<string> EnumToDescriptions(this Type type)
        {
            List<string> list = new List<string>();
            foreach (string name in Enum.GetNames(type))
            {
                object obj = Enum.Parse(type, name);
                list.Add(obj.ToDescription());
            }
            return list;
        }

        #endregion

        #region(StringExtension)
        /// <summary>
        /// 判断可空类型
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns></returns>
        public static string NullToString(this string o)
        {
            return o == null ? "" : o;
        }
        /// <summary>
        /// 验证是否空字符串
        /// </summary>
        /// <param name="str">待验证字符串</param>
        /// <returns>是空返回true,非空返回false</returns>
        public static Boolean IsNullString(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// String类型转换成Int类型
        /// </summary>
        public static int ToInt(this string str)
        {
            int intParm;
            int.TryParse(str, out intParm);
            return intParm;
        }

        /// <summary>
        /// String类型转换成Short类型
        /// </summary>
        public static short ToShort(this string str)
        {
            short shortParm;
            short.TryParse(str, out shortParm);
            return shortParm;
        }

        /// <summary>
        /// String类型转换成decimal类型
        /// </summary>
        public static decimal ToDecimal(this string str)
        {
            decimal decimalParm;
            decimal.TryParse(str, out decimalParm);
            return decimalParm;
        }

        /// <summary>
        /// String类型转换成decimal类型
        /// </summary>
        public static long ToLong(this string str)
        {
            long longParm;
            long.TryParse(str, out longParm);
            return longParm;
        }

        /// <summary>
        /// String类型转换成byte类型
        /// </summary>
        public static byte ToByte(this string str)
        {
            byte byteParm;
            byte.TryParse(str, out byteParm);
            return byteParm;
        }

        /// <summary>
        /// String类型转换成Bool类型
        /// </summary>
        public static bool ToBoolean(this string str)
        {
            bool boolParm;
            bool.TryParse(str, out boolParm);
            return boolParm;
        }

        /// <summary>
        /// String类型转换成DateTime类型
        /// </summary>
        public static DateTime ToDateTime(this string str)
        {
            DateTime timeParm;
            DateTime.TryParse(str, out timeParm);
            return timeParm;
        }

        /// <summary>
        /// String类型转换成DateTime类型
        /// </summary>
        public static DateTime? ToDateTime2(this string str)
        {
            DateTime dateTime = str.ToDateTime();
            if (dateTime == DateTime.MinValue)
                return null;
            return dateTime;
        }

        /// <summary>
        /// 为筛选条件中结束日期增加23个小时59分钟59秒
        /// </summary>
        public static DateTime? ToEndDateTime2(this string str)
        {
            var datetime = str.ToDateTime2();
            if (datetime.HasValue)
                return datetime.Value.Date.AddDays(1).AddSeconds(-1);
            return datetime;
        }

        #region 增加有默认值的string扩展方法 20140325 addby shwang

        /// <summary>
        /// 字符串转成int
        /// </summary>
        /// <param name="str">原始值</param>
        /// <param name="defaultValue">转换失败默认值</param>
        /// <returns>int</returns>
        public static int ConvertToInt(this string str, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            int result;
            if (int.TryParse(str, out result))
                return result;
            else
                return defaultValue;
        }

        /// <summary>
        /// 字符串转成bool
        /// </summary>
        /// <param name="str">原始值</param>
        /// <param name="defaultValue">转换失败默认值</param>
        /// <returns>bool</returns>
        public static bool? ConvertToBool(this string str, bool? defaultValue = null)
        {
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            bool result;
            if (bool.TryParse(str, out result))
                return result;
            else
                return defaultValue;
        }

        /// <summary>
        /// 字符串转成datetime
        /// </summary>
        /// <param name="str">原始值</param>
        /// <param name="defaultValue">转换失败默认值</param>
        /// <returns>datetime</returns>
        public static DateTime? ConvertToDatetime(this string str, DateTime? defaultValue = null)
        {
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            DateTime result;
            if (DateTime.TryParse(str, out result))
                return result;
            else
                return defaultValue;
        }

        /// <summary>
        /// 字符串转成byte
        /// </summary>
        /// <param name="str">原始值</param>
        /// <param name="defaultValue">转换失败默认值</param>
        /// <returns>byte</returns>
        public static Byte ConverToByte(this string str, byte defaultValue = 0)
        {
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            Byte result;
            if (Byte.TryParse(str, out result))
                return result;
            else
                return defaultValue;
        }

        /// <summary>
        /// 字符串拼sql时单引号替换成2个单引号
        /// </summary>
        /// <param name="str">原始值</param>
        /// <returns>替换成2个单引号</returns>
        public static string FormatToSqlStr(this string str)
        {
            return str.Replace("'", "''");
        }

        /// <summary>
        /// 设置参数，替换模板占位符
        /// </summary>
        public static string StringFormat(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        #endregion

        /// <summary>
        /// 设置身份证号，返回生日
        /// </summary>
        public static string ToBirthday(this string identityCard)
        {
            if (identityCard == null)
                return string.Empty;
            switch (identityCard.Length)
            {
                case 18: return string.Format("{0}-{1}-{2}", identityCard.Substring(6, 4),
                    identityCard.Substring(10, 2), identityCard.Substring(12, 2));
                case 15: return string.Format("19{0}-{1}-{2}", identityCard.Substring(6, 2),
                    identityCard.Substring(8, 2), identityCard.Substring(10, 2));
                default:
                    return string.Empty;
            }
        }
        /// <summary>
        /// 设置身份证号，返回性别
        /// </summary>
        public static string ToSex(this string identityCard)
        {
            if (identityCard == null)
                return string.Empty;
            int intSex;
            switch (identityCard.Length)
            {
                case 18: intSex = identityCard.Substring(14, 3).ToInt(); break;
                case 15: intSex = identityCard.Substring(12, 3).ToInt(); break;
                default: return string.Empty;
            }
            return intSex % 2 == 0 ? "女" : "男";
        }
        /// <summary>
        /// 正则验证
        /// </summary>
        public static bool IsVaild(this string Txt, string vaildstr)
        {
            Regex regex = new Regex(vaildstr);
            return regex.IsMatch(Txt.Trim());
        }

        /// <summary>
        /// 设置文件名称，获取文件内容
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="parameters">替换参数</param>
        /// <returns>返回文件内容</returns>
        public static string ToFileContent(this string fileName, bool isweb = true, params object[] parameters)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + fileName;
            if (!isweb)
                path = path.Replace("bin\\Release\\", "");
            if (!File.Exists(path))
                return string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                var content = reader.ReadToEnd();
                return parameters.Length > 0 ? string.Format(content, parameters) : content;
            }
        }
        #endregion

        #region(DateTimeExtension)
        /// <summary>
        /// DateTime类型转换成String类型（显示格式：1985-01-10）
        /// </summary>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime == DateTime.MinValue ?
                string.Empty : dateTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// DateTime?类型转换成String类型（显示格式：1989-02-12）
        /// </summary>
        public static string ToDateString(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToDateString() : string.Empty;
        }

        /// <summary>
        /// DateTime类型转换成String类型（显示格式：1985/01）
        /// </summary>
        public static string ToBillMonthString(this DateTime dateTime, string tag = "/")
        {
            return dateTime == DateTime.MinValue ?
                string.Empty : dateTime.ToString("yyyy" + tag + "MM", DateTimeFormatInfo.InvariantInfo).Trim();
        }

        /// <summary>
        /// DateTime类型转换成String类型（显示格式：1985-01-10 12:30:30）
        /// </summary>
        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime == DateTime.MinValue ?
                string.Empty : dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// DateTime类型转换成String类型（显示格式：1985-01-10 12:30:30）
        /// </summary>
        public static string ToDateTimeString(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToDateTimeString() : string.Empty;
        }

        /// <summary>
        /// DateTime类型转换成当前日期的第day天
        /// </summary>
        public static DateTime SetDay(this DateTime dateTime, int day = 1)
        {
            return dateTime.AddDays(-dateTime.Day + day);
        }
        #endregion

        #region(DecimalExtension)
        /// <summary>
        /// 将decimal类型转换成金额字符串（显示格式：￥125.35）
        /// </summary>
        public static string ToAmtString(this decimal amount)
        {
            return amount.ToString("N", CultureInfo.CreateSpecificCulture("zh-CN"));
        }

        /// <summary>
        /// 用颜色区分将decimal类型转换成金额字符串（显示格式：￥125.35，颜色区分：负数为红色、零为黑色、整数为蓝色）
        /// </summary>
        public static string ToAmtHtmlString(this decimal amount)
        {
            if (amount < 0)
                return string.Format("<font color=red>{0}</font>", amount.ToAmtString());
            else if (amount == 0)
                return amount.ToAmtString();
            else
                return string.Format("<font color=blue>{0}</font>", amount.ToAmtString());
        }

        /// <summary>
        /// 将decimal类型转换成百分比字符串（显示格式：0.1%）
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string ToPercent(this decimal amount)
        {
            return amount.ToString("p");
        }

        #region 转成大写金额
        /// <summary>
        /// 数字字符串 
        /// 转换成中文大写后的字符串或者出错信息提示字符串 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToChineseUpper(this string str)
        {

            if (!IsPositveDecimal(str))

                return "输入的不是正数字！";

            if (Double.Parse(str) > 999999999999.99)

                return "数字太大，无法换算，请输入一万亿元以下的金额";

            char[] ch = new char[1];

            ch[0] = '.'; //小数点 

            string[] splitstr = null; //定义按小数点分割后的字符串数组 

            splitstr = str.Split(ch[0]);//按小数点分割字符串 

            if (splitstr.Length == 1) //只有整数部分 

                return ConvertData(str) + "圆整";

            else //有小数部分 
            {

                string rstr;

                rstr = ConvertData(splitstr[0]) + "圆";//转换整数部分 

                rstr += ConvertXiaoShu(splitstr[1]);//转换小数部分 

                return rstr;

            }

        }

        /// <summary>
        /// 数字字符串 
        /// 转换成中文大写后的字符串或者出错信息提示字符串 
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string ConvertChineseUpper(this decimal money)
        {
            return money.ToString().ConvertToChineseUpper();
        }

        /// <summary>
        /// 判断是否是正数字字符串 
        /// 判断字符串 
        /// 如果是数字，返回true，否则返回false 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool IsPositveDecimal(string str)
        {

            Decimal d;

            try
            {

                d = Decimal.Parse(str);

            }

            catch (Exception)
            {

                return false;

            }

            if (d > 0)

                return true;

            else

                return false;

        }

        /// <summary>
        /// 转换数字（整数） 
        /// 需要转换的整数数字字符串 
        /// 转换成中文大写后的字符串 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string ConvertData(string str)
        {

            string tmpstr = "";

            string rstr = "";

            int strlen = str.Length;

            if (strlen <= 4)//数字长度小于四位 
            {

                rstr = ConvertDigit(str);

            }

            else
            {

                if (strlen <= 8)//数字长度大于四位，小于八位 
                {

                    tmpstr = str.Substring(strlen - 4, 4);//先截取最后四位数字 

                    rstr = ConvertDigit(tmpstr);//转换最后四位数字 

                    tmpstr = str.Substring(0, strlen - 4);//截取其余数字 

                    //将两次转换的数字加上万后相连接 

                    rstr = String.Concat(ConvertDigit(tmpstr) + "万", rstr);

                    rstr = rstr.Replace("零零", "零");

                }

                else

                    if (strlen <= 12)//数字长度大于八位，小于十二位 
                    {

                        tmpstr = str.Substring(strlen - 4, 4);//先截取最后四位数字 

                        rstr = ConvertDigit(tmpstr);//转换最后四位数字 

                        tmpstr = str.Substring(strlen - 8, 4);//再截取四位数字 

                        rstr = String.Concat(ConvertDigit(tmpstr) + "万", rstr);

                        tmpstr = str.Substring(0, strlen - 8);

                        rstr = String.Concat(ConvertDigit(tmpstr) + "亿", rstr);

                        rstr = rstr.Replace("零亿", "亿");

                        rstr = rstr.Replace("零万", "零");

                        rstr = rstr.Replace("零零", "零");

                        rstr = rstr.Replace("零零", "零");

                    }

            }

            strlen = rstr.Length;

            if (strlen >= 2)
            {

                switch (rstr.Substring(strlen - 2, 2))
                {

                    case "佰零": rstr = rstr.Substring(0, strlen - 2) + "佰"; break;

                    case "仟零": rstr = rstr.Substring(0, strlen - 2) + "仟"; break;

                    case "万零": rstr = rstr.Substring(0, strlen - 2) + "万"; break;

                    case "亿零": rstr = rstr.Substring(0, strlen - 2) + "亿"; break;

                }

            }

            return rstr;

        }

        /// <summary>
        /// 转换数字（小数部分） 
        /// 需要转换的小数部分数字字符串 
        /// 转换成中文大写后的字符串 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string ConvertXiaoShu(string str)
        {

            int strlen = str.Length;

            string rstr;

            if (strlen == 1)
            {

                rstr = ConvertChinese(str) + "角";

                return rstr;

            }

            else
            {

                string tmpstr = str.Substring(0, 1);

                rstr = ConvertChinese(tmpstr) + "角";

                tmpstr = str.Substring(1, 1);

                rstr += ConvertChinese(tmpstr) + "分";

                rstr = rstr.Replace("零分", "");

                rstr = rstr.Replace("零角", "");

                return rstr;

            }

        }

        /// <summary>
        /// 转换数字 
        /// 转换的字符串（四位以内） 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string ConvertDigit(string str)
        {

            int strlen = str.Length;

            string rstr = "";

            switch (strlen)
            {

                case 1: rstr = ConvertChinese(str); break;

                case 2: rstr = Convert2Digit(str); break;

                case 3: rstr = Convert3Digit(str); break;

                case 4: rstr = Convert4Digit(str); break;

            }

            rstr = rstr.Replace("拾零", "拾");

            strlen = rstr.Length;

            return rstr;

        }

        /// <summary>
        /// 转换四位数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Convert4Digit(string str)
        {

            string str1 = str.Substring(0, 1);

            string str2 = str.Substring(1, 1);

            string str3 = str.Substring(2, 1);

            string str4 = str.Substring(3, 1);

            string rstring = "";

            rstring += ConvertChinese(str1) + "仟";

            rstring += ConvertChinese(str2) + "佰";

            rstring += ConvertChinese(str3) + "拾";

            rstring += ConvertChinese(str4);

            rstring = rstring.Replace("零仟", "零");

            rstring = rstring.Replace("零佰", "零");

            rstring = rstring.Replace("零拾", "零");

            rstring = rstring.Replace("零零", "零");

            rstring = rstring.Replace("零零", "零");

            rstring = rstring.Replace("零零", "零");

            return rstring;

        }

        /// <summary>
        /// 转换三位数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Convert3Digit(string str)
        {

            string str1 = str.Substring(0, 1);

            string str2 = str.Substring(1, 1);

            string str3 = str.Substring(2, 1);

            string rstring = "";

            rstring += ConvertChinese(str1) + "佰";

            rstring += ConvertChinese(str2) + "拾";

            rstring += ConvertChinese(str3);

            rstring = rstring.Replace("零佰", "零");

            rstring = rstring.Replace("零拾", "零");

            rstring = rstring.Replace("零零", "零");

            rstring = rstring.Replace("零零", "零");

            return rstring;

        }

        /// <summary>
        /// 转换二位数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Convert2Digit(string str)
        {

            string str1 = str.Substring(0, 1);

            string str2 = str.Substring(1, 1);

            string rstring = "";

            rstring += ConvertChinese(str1) + "拾";

            rstring += ConvertChinese(str2);

            rstring = rstring.Replace("零拾", "零");

            rstring = rstring.Replace("零零", "零");

            return rstring;

        }

        /// <summary>
        /// 将一位数字转换成中文大写数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string ConvertChinese(string str)
        {

            //"零壹贰叁肆伍陆柒捌玖拾佰仟万亿圆整角分" 

            string cstr = "";

            switch (str)
            {

                case "0": cstr = "零"; break;

                case "1": cstr = "壹"; break;

                case "2": cstr = "贰"; break;

                case "3": cstr = "叁"; break;

                case "4": cstr = "肆"; break;

                case "5": cstr = "伍"; break;

                case "6": cstr = "陆"; break;

                case "7": cstr = "柒"; break;

                case "8": cstr = "捌"; break;

                case "9": cstr = "玖"; break;

            }

            return (cstr);

        }
        #endregion
        #endregion

        #region(List<String>Extension)
        /// <summary>
        /// 将{1,2,3,4}转换成'1','2','3','4'之类的格式
        /// </summary>
        public static string ToCharDecorate(this List<string> list, string character = "")
        {
            var split = character + ',' + character;
            return character + string.Join(split, list) + character;
        }
        #endregion

        #region
        /// <summary>
        /// Distinct扩展
        /// </summary>
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }
        #endregion

        #region 绑定的Object,add by shwang
        /// <summary>
        /// 绑定日期数据转成显示数据
        /// </summary>
        /// <param name="val">绑定数据</param>
        /// <returns>日期</returns>
        public static string BindDateToStr(this object val)
        {
            if (val == null || val == DBNull.Value)
                return string.Empty;

            return Convert.ToDateTime(val).ToDateString();
        }

        /// <summary>
        /// 绑定日期数据转成显示成时间数据
        /// </summary>
        /// <param name="val">绑定数据</param>
        /// <returns>日期</returns>
        public static string BindDateToTimeStr(this object val)
        {
            if (val == null || val == DBNull.Value)
                return string.Empty;

            return Convert.ToDateTime(val).ToDateTimeString();
        }

        /// <summary>
        /// 绑定数字转成显示数据
        /// </summary>
        /// <param name="val">绑定数据</param>
        /// <returns>数字</returns>
        public static string BindDecimalToStr(this object val)
        {
            if (val == null || val == DBNull.Value)
                return string.Empty;

            return Convert.ToDecimal(val).ToAmtString();
        }


        #endregion

        #region 判断函数

        /// <summary>
        /// 是否为无符号Int型
        /// </summary>
        /// <param name="str">检查的字符串</param>
        /// <returns></returns>
        public static bool IsUnSignInt(this string str)
        {
            //检查是否是正整数
            if (!Regex.IsMatch(str, "^[0-9]*[1-9][0-9]*$"))
                return false;
            int value = 0;
            //检查是否超出了int的边界
            if (!int.TryParse(str, out value))
                return false;
            return true;
        }

        #endregion
    }
}

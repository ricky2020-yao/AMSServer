using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Cn.Vcredit.Common.Tools;
using Cn.Vcredit.AMS.Data.DB.Data;

namespace Cn.Vcredit.AMS.Logic.BLL.PayBank.Inport
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年6月8日
    /// 导入模版抽象类
    /// </summary>
    public abstract class ImportTemplate
    {
        #region - 属性字段 -

        public DeductCommand _DeductCommand { get; set; }
        #endregion

        #region - 构造函数 -
        public ImportTemplate(DeductCommand deductcommand)
        {
            this._DeductCommand = deductcommand;
        }
        #endregion

        #region- 抽象类 -
        public abstract List<BaseImportItem> MatchBillItems(Stream stream, out string errormessage);                    
        #endregion

        #region- 功能函数 -

        #region 普通导入格式解析
        /// <summary>
        /// 组合数据和格式
        /// </summary>
        /// <param name="bills"></param>
        /// <param name="fromat"></param>
        /// <returns></returns>
        protected List<BaseImportItem>GetSourceFomart(Stream stream,
            string fh, string successtext, out string errormsg,
            int card,int name,int amount,int status,int contractno,int subj,int count,int hl,string zf=null)
        {
            if (stream is MemoryStream)
                stream.Position = 0;
            errormsg = string.Empty; 
            List<BaseImportItem> list = new List<BaseImportItem>();
            StreamReader sdr = new StreamReader(stream, Encoding.GetEncoding("GBK"));
            if (sdr.BaseStream.Length <= 0)
            {
                errormsg = "无任何导入内容";
                return null;
            }
            string ret = string.Empty;
            if (hl > 0)
            {
                for (int i = 0; i < hl; i++)
                {
                    ret = sdr.ReadLine();
                    if (string.IsNullOrEmpty(ret))
                        ret = sdr.ReadLine();
                }
            }
            while (sdr.Peek() > -1)
            {
                ret = sdr.ReadLine();
                if (!string.IsNullOrEmpty(ret))
                {
                    try
                    {
                        string[] splite = FomartTemplate(ret, fh);
                        splite[amount] = zf == null ? splite[amount] : splite[amount].Replace(zf, "");
                        if (splite.Length != count)
                        {
                            errormsg = ret + "格式不匹配";
                            break;
                        }
                        if (!splite[card].IsVaild("^[0-9]*$"))
                        {
                            errormsg = ret + "对应位置的内容不正确";
                            break;
                        }
                        if (splite[status].Trim() != successtext.Trim())
                            continue;
                        list.Add(new BaseImportItem
                        {
                            Amount = splite[amount].ToDecimal(),
                            CustomerName = splite[name].Trim(),
                            SavingCard = splite[card].Trim(),
                            ContractNo = splite[contractno].Trim(),
                            DunLevel = GetLevel(splite[subj].Trim())
                        });
                    }
                    catch
                    {
                        errormsg = ret;
                        break;
                    }
                }
            }
            return list;
        }
        #endregion

        #region 杭州光大导入格式解析
        /// <summary>
        /// 组合数据和格式
        /// </summary>
        /// <param name="bills"></param>
        /// <param name="fromat"></param>
        /// <returns></returns>
        protected List<BaseImportItem> GetSourceFomartForHZ(Stream stream, out string errormsg,
            int card, int name, int amount, int status,int contractno,int subj, string suctext, int count)
        {
            List<BaseImportItem> check = new List<BaseImportItem>();
            errormsg = string.Empty;
            List<BaseImportItem> list = new List<BaseImportItem>();
            string ret = string.Empty;
            try
            {
                StreamReader sdr = new StreamReader(stream, Encoding.GetEncoding("GBK"));
                if (sdr.BaseStream.Length <= 0)
                {
                    errormsg = "无任何导入内容";
                    return null;
                }
                ret = sdr.ReadLine();
                string[] title = ret.Split('|');
                if (title.Length != 2)
                {
                    errormsg = ret + "首行格式不匹配";
                    return null;
                }
                while (sdr.Peek() > -1)
                {
                    ret = sdr.ReadLine();
                    if (!string.IsNullOrEmpty(ret))
                    {
                        string[] splite = FomartTemplate(ret, "|");
                        if (splite.Length != count)
                        {
                            errormsg = ret + "格式不匹配";
                            break;
                        }
                        if (!splite[card].IsVaild("^[0-9]*$"))
                        {
                            errormsg = ret + "对应位置的内容不正确";
                            break;
                        }
                        check.Add(new BaseImportItem
                        {
                            Amount = splite[amount].ToDecimal(),
                            CustomerName = splite[name].Trim(),
                            SavingCard = splite[card].Trim(),
                            DunLevel = GetLevel(splite[subj].Trim())
                        });
                        if (splite[status] != suctext.Trim())
                            continue;
                        list.Add(new BaseImportItem
                        {
                            Amount = splite[amount].ToDecimal(),
                            CustomerName = splite[name].Trim(),
                            SavingCard = splite[card].Trim(),
                            ContractNo = splite[contractno].Trim(),
                            DunLevel = GetLevel(splite[subj].Trim())
                        });
                    }
                }
                if (check.Count > 0)
                {
                    if (check.Sum(o => o.Amount) != title[1].ToDecimal() ||
                        check.Count != title[0].ToInt())
                    {
                        errormsg = "导入金额和笔数与导出不符";
                        list.Clear();
                    }
                }
            }
            catch(Exception ex)
            {
                errormsg = ret + ex.ToString();
                list.Clear();
                check.Clear();
            }
            return list;
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 模版格式
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        protected string[] FomartTemplate(string input, string fh)
        {
            if (!input.Contains(fh))
                return null;
            return input.Split(fh.ToArray());
        }
        protected byte GetLevel(string Subject)
        {
            Subject = Subject.Trim();
            switch (Subject)
            {
                case "本息":
                    return 10;
                case "本息扣失汇总":
                    return 21;
                case "当期本息扣失":
                    return 22;
                case "罚息汇总":
                    return 23;
                default:
                    return 0;
            }
        }
        #endregion

        #endregion
    }
}

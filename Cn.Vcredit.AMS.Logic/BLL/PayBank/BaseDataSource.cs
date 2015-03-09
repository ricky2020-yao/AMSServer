using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Logic.BLL.PayBank.Export;
using Cn.Vcredit.AMS.Logic.BLL.PayBank.Inport;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.BLL.PayBank
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年6月8日
    /// 导入、导出模版数据基类
    /// </summary>
    public class BaseDataSource
    {
        #region 变量
        /// <summary>
        /// 固定日扣款顺序表相关数据操作类
        /// </summary>
        private DeductSequenceDal m_DeductSequenceDal = new DeductSequenceDal();
        /// <summary>
        /// 交易记录表数据操作类
        /// </summary>
        private PayTraceDal m_PayTraceDal = new PayTraceDal();
        #endregion

        #region - 属性字段 -
        public Dictionary<int, DeductCommand> DeductCommandList { get; set; }

        #endregion

        #region - 构造函数 -
        public BaseDataSource(Dictionary<int, DeductCommand> cmdlist)
        {
            this.DeductCommandList = cmdlist;
        }
        #endregion

        #region- 功能函数 -

        #region- 导出 -

        #region 获取导出结果集(TXT+Excel 外贸信托方)
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int, List<byte>> GetBillListDWJM(out List<string[]> retstring,
            out decimal totalamount, List<BankAccount> banks, List<PayBankExportItem> data,
            List<Enumeration> enums, int serid, int lengidngid)
        {
            #region 基础信息
            totalamount = 0;
            retstring = new List<string[]>();
            Dictionary<int, List<byte>> retbyte = new Dictionary<int, List<byte>>();
            Dictionary<int, List<PayBankExportItem>> dicdata = new Dictionary<int, List<PayBankExportItem>>();
            if (DeductCommandList.Count < 0)
                return null;
            if (data.Count <= 0)
                return retbyte;
            int count = data.Count / 5000;
            int num = 0;
            int i = 0;
            for (i = 0; i <= count; i++)
            {
                retbyte.Add(i, new List<byte>());

                var last = data.Skip(num).Take(5000).ToList();
                var first = data.Skip(num + (last.Count)).Take(5000).ToList();
                var co1 = last.LastOrDefault();
                var co2 = first.FirstOrDefault();
                if (co1 != null && co2 != null && co1.ContractNo == co2.ContractNo)
                {
                    last = last.Where(p => p.ContractNo != co1.ContractNo).ToList();
                }
                num += last.Count;
                dicdata.Add(i, last);
            }
            if (data.Count - num > 0)
                dicdata.Add(i + 1, data.Skip(num).Take(5000).ToList());
            BankAccount lengidngbank = banks.FirstOrDefault(p => p.BankAccountID == lengidngid);
            decimal lendingamount = 0;
            #endregion
            foreach (var dic in dicdata)
            {
                foreach (var item in dic.Value)
                {
                    #region 导出扣款指令逻辑
                    totalamount += item.DunAmount;
                    lendingamount = item.BJ + item.LX + item.BXKS + item.FX;
                    //信托方收益
                    if (totalamount > 0)
                    {
                        //信托TXT扣款指令
                        retbyte[dic.Key].AddRange(GetBankExportTemplate(DeductCommandList[item.LendingSideID])
                                .FomartTemplate(item, lendingamount, banks, lengidngbank));
                        //信托Excel对账单
                        retstring.Add(GetBankExportTemplate2(DeductCommandList[item.LendingSideID])
                                .GetImportExcelItem(item, lendingamount, banks, enums, lengidngbank));
                    }
                    #endregion
                }
            }
            return retbyte;
        }
        #endregion

        #region 获取导出结果集(TXT+Excel 外贸服务方)
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int, List<byte>> GetBillListServiceDWJM(out List<string[]> retstring,
           List<BankAccount> banks, List<PayBankExportItem> data, List<Enumeration> enums, int serid, int lengidngid)
        {
            #region 基础信息
            retstring = new List<string[]>();
            Dictionary<int, List<byte>> retbyte = new Dictionary<int, List<byte>>();
            Dictionary<int, List<PayBankExportItem>> dicdata = new Dictionary<int, List<PayBankExportItem>>();
            if (DeductCommandList.Count < 0)
                return null;
            if (data.Count <= 0)
                return retbyte;
            int count = data.Count / 5000;
            int num = 0;
            int i = 0;
            for (i = 0; i <= count; i++)
            {
                retbyte.Add(i, new List<byte>());

                var last = data.Skip(num).Take(5000).ToList();
                var first = data.Skip(num + (last.Count)).Take(5000).ToList();
                var co1 = last.LastOrDefault();
                var co2 = first.FirstOrDefault();
                if (co1 != null && co2 != null && co1.ContractNo == co2.ContractNo)
                {
                    last = last.Where(p => p.ContractNo != co1.ContractNo).ToList();
                }
                num += last.Count;
                dicdata.Add(i, last);
            }
            if (data.Count - num > 0)
                dicdata.Add(i + 1, data.Skip(num).Take(5000).ToList());
            BankAccount servicebank = banks.FirstOrDefault(p => p.BankAccountID == serid);
            BankAccount lengidngbank = banks.FirstOrDefault(p => p.BankAccountID == lengidngid);
            #endregion
            foreach (var dic in dicdata)
            {
                foreach (var item in dic.Value)
                {
                    #region 导出扣款指令逻辑
                    if (item.DunAmount > 0)
                    {
                        //担保客户TXT扣款指令
                        retbyte[dic.Key].AddRange(GetBankExportTemplate(DeductCommandList[serid])
                                .FomartTemplate(item, item.DunAmount, banks, servicebank, true));
                        //担保客户Excel对账单
                        retstring.Add(GetBankExportTemplate2(DeductCommandList[serid])
                                .GetImportExcelItem(item, item.DunAmount, banks, enums, servicebank, true));
                    }
                    #endregion
                }
            }
            return retbyte;
        }
        #endregion

        #region 获取导出结果集(TXT+Excel 外贸担保方)
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int, List<byte>> GetBillListGuarantee(out List<string[]> retstring,
            List<Customer> customers, List<BankAccount> banks, List<PayBankExportItem> data,
            List<Enumeration> enums, int serid, int lengidngid)
        {
            #region 基础信息
            retstring = new List<string[]>();
            Dictionary<int, List<byte>> retbyte = new Dictionary<int, List<byte>>();
            Dictionary<int, List<PayBankExportItem>> dicdata = new Dictionary<int, List<PayBankExportItem>>();
            if (DeductCommandList.Count < 0)
                return null;
            if (data.Count <= 0)
                return retbyte;
            int count = data.Count / 5000;
            int num = 0;
            int i = 0;
            for (i = 0; i <= count; i++)
            {
                retbyte.Add(i, new List<byte>());
                var last = data.Skip(num).Take(5000).ToList();
                var first = data.Skip(num + (last.Count)).Take(5000).ToList();
                var co1 = last.LastOrDefault();
                var co2 = first.FirstOrDefault();
                if (co1 != null && co2 != null && co1.ContractNo == co2.ContractNo)
                {
                    last = last.Where(p => p.ContractNo != co1.ContractNo).ToList();
                }
                num += last.Count;
                dicdata.Add(i, last);
            }
            if (data.Count - num > 0)
                dicdata.Add(i + 1, data.Skip(num).Take(5000).ToList());
            BankAccount servicebank = banks.FirstOrDefault(p => p.BankAccountID == serid);
            #endregion
            foreach (var dic in dicdata)
            {
                foreach (var item in dic.Value)
                {
                    #region 导出扣款指令逻辑
                    if (item.DunAmount > 0)
                    {
                        //担保客户TXT扣款指令
                        retbyte[dic.Key].AddRange(GetBankExportTemplate(DeductCommandList[6])
                                .FomartTemplate(item, item.DunAmount, banks, servicebank, true));
                        //担保客户Excel对账单
                        retstring.Add(GetBankExportTemplate2(DeductCommandList[6])
                                .GetImportExcelItem(item, item.DunAmount, banks, enums, servicebank, true));
                    }
                    #endregion
                }
            }
            return retbyte;
        }
        #endregion

        #region 获取导出结果集（Excel+Excel 杭州工商外贸正常客户、担保客户）
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int, List<string[]>> GetBillListExcelDWJM(out List<string[]> outstring, List<PayBankExportItem> data,
            List<Customer> customers, List<BankAccount> banks, List<Enumeration> enums, int serid, int lengdingid)
        {
            outstring = new List<string[]>();
            Dictionary<int, List<string[]>> retstring = new Dictionary<int, List<string[]>>();
            Dictionary<int, List<PayBankExportItem>> dicdata = new Dictionary<int, List<PayBankExportItem>>();
            if (DeductCommandList.Count < 0)
                return null;
            if (data.Count <= 0)
                return retstring;
            int count = data.Count / 2000;
            int num = 0;
            int i = 0;
            for (i = 0; i <= count; i++)
            {
                retstring.Add(i, new List<string[]>());
                var last = data.Skip(num).Take(2000).ToList();
                var first = data.Skip(num + (last.Count)).Take(2000).ToList();
                var co1 = last.LastOrDefault();
                var co2 = first.FirstOrDefault();
                if (co1 != null && co2 != null && co1.ContractNo == co2.ContractNo)
                {
                    last = last.Where(p => p.ContractNo != co1.ContractNo).ToList();
                }
                num += last.Count;
                dicdata.Add(i, last);
            }
            if (data.Count - num > 0)
                dicdata.Add(i + 1, data.Skip(num).Take(2000).ToList());
            BankAccount lengidngbank = banks.FirstOrDefault(p => p.BankAccountID == lengdingid);
            foreach (var dat in dicdata)
            {
                foreach (var item in dat.Value)
                {
                    #region 导出扣款指令逻辑
                    if (item.DunAmount > 0)
                    {
                        //信托TXT扣款指令
                        retstring[dat.Key].Add(GetBankExportTemplate2(DeductCommandList[item.LendingSideID], true)
                            .GetImportExcelItem(item, item.DunAmount, banks, enums, lengidngbank));
                        //信托Excel对账单
                        outstring.Add(GetBankExportTemplate2(DeductCommandList[item.LendingSideID])
                                .GetImportExcelItem(item, item.DunAmount, banks, enums, lengidngbank));
                    }
                    #endregion
                }
            }
            return retstring;
        }
        #endregion

        #region 获取导出结果集(TXT+Excel 渤海信托方)
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int, List<byte>> GetBillListBHXT(out Dictionary<int, List<string[]>> retstring,
            out decimal totalamount, List<BankAccount> banks, List<PayBankExportItem> data,
            List<Enumeration> enums, int serid, int lengidngid)
        {
            #region 基础信息
            totalamount = 0;
            retstring = new Dictionary<int, List<string[]>>();
            Dictionary<int, List<byte>> retbyte = new Dictionary<int, List<byte>>();
            Dictionary<int, decimal> dunamount = new Dictionary<int, decimal>();
            if (DeductCommandList.Count < 0)
                return null;
            foreach (var a in DeductCommandList)
            {
                retbyte.Add(a.Key, new List<byte>());
                retstring.Add(a.Key, new List<string[]>());
                dunamount.Add(a.Key, 0);
            }
            if (data.Count <= 0)
                return retbyte;
            BankAccount lengidngbank = banks.FirstOrDefault(p => p.BankAccountID == lengidngid);
            #endregion

            foreach (var item in data)
            {
                #region 导出扣款指令逻辑
                totalamount += item.DunAmount;
                //信托方收益
                if (item.DunAmount > 0)
                {
                    //信托TXT扣款指令
                    retbyte[item.LendingSideID].AddRange(GetBankExportTemplate(DeductCommandList[item.LendingSideID])
                            .FomartTemplate(item, item.DunAmount, banks, lengidngbank));
                    //信托Excel对账单
                    retstring[item.LendingSideID].Add(GetBankExportTemplate2(DeductCommandList[item.LendingSideID])
                            .GetImportExcelItem(item, item.DunAmount, banks, enums, lengidngbank));
                    dunamount[item.LendingSideID] += item.DunAmount;
                }
                #endregion
            }

            #region 为杭州光大导出的扣款指令加上表头
            if (serid == 5)
                foreach (var a in DeductCommandList)
                {
                    totalamount += dunamount[a.Key];
                    if (retbyte[a.Key].Count > 0)
                    {
                        List<byte> bytes = new List<byte>();
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("JIAMBZ:\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("DANWBH:00000000\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes(string.Format("YWZLBH:{0}\r\n",
                            a.Key == 5 ? "0022" : "0018")));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("KHZHLX:0\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("KEHUZH:\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("HUOBDH:01\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("CHUIBZ:\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("ZHHUXZ:0001\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("SHOFBZ:2\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes(string.Format("ZONGJE:{0}\r\n",
                            dunamount[a.Key])));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes(string.Format("BISHUU:{0}\r\n",
                            retstring[a.Key].Count)));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes(string.Format("RUZHRQ:{0}\r\n",
                            DateTime.Now.ToString("yyyyMMdd"))));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes(("--------------------------" +
                            "--------------------------------------------\r\n")));
                        retbyte[a.Key].InsertRange(0, bytes);
                    }
                }
            #endregion
            return retbyte;
        }
        #endregion

        #region 获取导出结果集(TXT+Excel 渤海服务方)
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int, List<byte>> GetBillListService(out Dictionary<int, List<string[]>> retstring,
            out decimal totalamount, List<BankAccount> banks, List<PayBankExportItem> data,
            List<Enumeration> enums, int serid, int lengidngid)
        {
            #region 基础信息
            totalamount = 0;
            retstring = new Dictionary<int, List<string[]>>();
            Dictionary<int, List<byte>> retbyte = new Dictionary<int, List<byte>>();
            Dictionary<int, decimal> dunamount = new Dictionary<int, decimal>();
            if (DeductCommandList.Count < 0)
                return null;
            foreach (var a in DeductCommandList)
            {
                retbyte.Add(a.Key, new List<byte>());
                retstring.Add(a.Key, new List<string[]>());
                dunamount.Add(a.Key, 0);
            }
            if (data.Count <= 0)
                return retbyte;
            BankAccount servicebank = banks.FirstOrDefault(p => p.BankAccountID == serid);
            BankAccount lengidngbank = banks.FirstOrDefault(p => p.BankAccountID == lengidngid);
            #endregion

            foreach (var item in data)
            {
                #region 导出扣款指令逻辑
                totalamount += item.DunAmount;
                if (item.DunAmount > 0)
                {
                    //担保客户TXT扣款指令
                    retbyte[item.ServiceSideID].AddRange(GetBankExportTemplate(DeductCommandList[item.ServiceSideID])
                            .FomartTemplate(item, item.DunAmount, banks, servicebank, true));
                    //担保客户Excel对账单
                    retstring[item.ServiceSideID].Add(GetBankExportTemplate2(DeductCommandList[item.ServiceSideID])
                            .GetImportExcelItem(item, item.DunAmount, banks, enums, servicebank, true));
                    dunamount[item.ServiceSideID] += item.DunAmount;
                }
                #endregion
            }

            #region 为杭州光大导出的扣款指令加上表头
            if (serid == 5)
                foreach (var a in DeductCommandList)
                {
                    totalamount += dunamount[a.Key];
                    if (retbyte[a.Key].Count > 0)
                    {
                        List<byte> bytes = new List<byte>();
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("JIAMBZ:\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("DANWBH:00000000\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes(string.Format("YWZLBH:{0}\r\n",
                            a.Key == 5 ? "0022" : "0018")));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("KHZHLX:0\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("KEHUZH:\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("HUOBDH:01\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("CHUIBZ:\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("ZHHUXZ:0001\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes("SHOFBZ:2\r\n"));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes(string.Format("ZONGJE:{0}\r\n",
                            dunamount[a.Key])));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes(string.Format("BISHUU:{0}\r\n",
                            retstring[a.Key].Count)));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes(string.Format("RUZHRQ:{0}\r\n",
                            DateTime.Now.ToString("yyyyMMdd"))));
                        bytes.AddRange(Encoding.GetEncoding("GBK").GetBytes(("--------------------------" +
                            "--------------------------------------------\r\n")));
                        retbyte[a.Key].InsertRange(0, bytes);
                    }
                }
            #endregion

            return retbyte;
        }
        #endregion

        #region 获取导出结果集（Excel+Excel 杭州工商渤海信托方）
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int, List<string[]>> GetBillListExcelBHXT(out Dictionary<int, List<string[]>> outstring,
            out decimal totalamount, List<PayBankExportItem> data, List<BankAccount> banks, List<Enumeration> enums, int serid, int lengdingid)
        {
            totalamount = 0;
            Dictionary<int, int> num = new Dictionary<int, int>();
            outstring = new Dictionary<int, List<string[]>>();
            Dictionary<int, List<string[]>> retstring = new Dictionary<int, List<string[]>>();
            if (DeductCommandList.Count < 0)
                return null;
            foreach (var a in DeductCommandList)
            {
                retstring.Add(a.Key, new List<string[]>());
                outstring.Add(a.Key, new List<string[]>());
            }
            BankAccount lengidngbank = banks.FirstOrDefault(p => p.BankAccountID == lengdingid);
            foreach (var item in data)
            {
                #region 导出扣款指令逻辑
                totalamount += item.DunAmount;
                if (item.DunAmount > 0)
                {
                    //信托TXT扣款指令
                    retstring[item.LendingSideID].Add(GetBankExportTemplate2(DeductCommandList[item.LendingSideID], true)
                        .GetImportExcelItem(item, item.DunAmount, banks, enums, lengidngbank));
                    //信托Excel对账单
                    outstring[item.LendingSideID].Add(GetBankExportTemplate2(DeductCommandList[item.LendingSideID])
                            .GetImportExcelItem(item, item.DunAmount, banks, enums, lengidngbank));
                }
                #endregion
            }
            return retstring;
        }
        #endregion

        #region 获取导出结果集（Excel+Excel 杭州工商渤海服务方）
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int, List<string[]>> GetBillListExcelService(out Dictionary<int, List<string[]>> outstring,
            out decimal totalamount, List<PayBankExportItem> data, List<BankAccount> banks, List<Enumeration> enums, int serid, int lengdingid)
        {
            totalamount = 0;
            outstring = new Dictionary<int, List<string[]>>();
            Dictionary<int, List<string[]>> retstring = new Dictionary<int, List<string[]>>();
            if (DeductCommandList.Count < 0)
                return null;
            foreach (var a in DeductCommandList)
            {
                retstring.Add(a.Key, new List<string[]>());
                outstring.Add(a.Key, new List<string[]>());
            }
            BankAccount servicebank = banks.FirstOrDefault(p => p.BankAccountID == serid);
            foreach (var item in data)
            {
                #region 导出扣款指令逻辑
                totalamount += item.DunAmount;
                if (item.DunAmount > 0)
                {
                    //担保客户TXT扣款指令
                    retstring[item.ServiceSideID].Add(GetBankExportTemplate2(DeductCommandList[item.ServiceSideID], true)
                            .GetImportExcelItem(item, item.DunAmount, banks, enums, servicebank, true));
                    //担保客户Excel对账单
                    outstring[item.ServiceSideID].Add(GetBankExportTemplate2(DeductCommandList[item.ServiceSideID])
                            .GetImportExcelItem(item, item.DunAmount, banks, enums, servicebank, true));
                }
                #endregion
            }
            return retstring;
        }
        #endregion

        #region 获取导出结果集(TXT+Excel 成都)
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int, List<byte>> GetBillListChengdu(out Dictionary<int, List<string[]>> retstring,
            out decimal totalamount, List<Customer> customers, List<BankAccount> banks, List<PayBankExportItem> data,
            List<Enumeration> enums, int serid, int lengidngid)
        {
            #region 基础信息
            totalamount = 0;
            retstring = new Dictionary<int, List<string[]>>();
            Dictionary<int, List<byte>> retbyte = new Dictionary<int, List<byte>>();
            if (DeductCommandList.Count < 0)
                return null;
            foreach (var a in DeductCommandList)
            {
                retbyte.Add(a.Key, new List<byte>());
                retstring.Add(a.Key, new List<string[]>());
            }
            if (data.Count <= 0)
                return retbyte;
            BankAccount servicebank = banks.FirstOrDefault(p => p.BankAccountID == serid);
            BankAccount lengidngbank = banks.FirstOrDefault(p => p.BankAccountID == lengidngid);
            #endregion

            foreach (var item in data)
            {
                #region 导出扣款指令逻辑
                totalamount += item.DunAmount;
                if (item.DunAmount > 0)
                {
                    //担保客户TXT扣款指令
                    retbyte[item.ServiceSideID].AddRange(GetBankExportTemplate(DeductCommandList[item.ServiceSideID])
                            .FomartTemplate(item, item.DunAmount, banks, servicebank, true));
                    //担保客户Excel对账单
                    retstring[item.ServiceSideID].Add(GetBankExportTemplate2(DeductCommandList[item.ServiceSideID])
                            .GetImportExcelItem(item, item.DunAmount, banks, enums, servicebank, true));
                }
                #endregion
            }
            return retbyte;
        }
        #endregion
        #endregion

        #region- 导入 -

        #region 导入扣款逻辑指令（服务方【包括所有子公司服务方】）
        /// <summary>
        /// 【适用所有服务方】
        /// </summary>
        protected virtual bool BankImportForService(List<BaseImportItem> list, int operateid,
            int serid, int lendid, string compankey, string recviedtime,
            string luckkey, out string message, out string missmsg, string filename)
        {
            message = string.Empty;
            missmsg = string.Empty;

            //FinanceContext context = new FinanceContext();
            //PaymentContext payment = new PaymentContext();
            //PaymentFactory payfac = new PaymentFactory(payment);
            //FinanceFactory fac = new FinanceFactory(context);

            //DbTransaction tran = null;
            //DbConnection dbConnection = ((IObjectContextAdapter)context).ObjectContext.Connection;
            //try
            //{
            //    dbConnection.Open();
            //    ReceivedDAL dal = new ReceivedDAL(context);

            //    var businesslist = context.BusinessSet
            //        .Where(p => p.ServiceSideID == serid && p.IsRepayment).ToList();
            //    string level = string.Empty;
            //    List<DunModel> models = null;
            //    decimal Amt = 0;
            //    string selectsql = "SQL\\ReceivedForBank\\SELECT_RECEIVED_Service.sql"
            //                    .ToFileContent(true, "{0}", "{1}", "{2}", "{3}");

            //    #region- 扣款指令导入日志 -
            //    PayTrace log = new PayTrace
            //    {
            //        AccountID = serid,
            //        IncomeType = (byte)EnumIncomeType.BankIncomeServ,
            //        OperatorID = operateid,
            //        TraceTime = DateTime.Now,
            //        PayKind = (byte)EnumPayKind.Payment_Bank,
            //        CallDirection = (byte)EnumCallDirectionKind.Import_Pay,
            //        PayTraceAmount = -list.Sum(p => p.Amount),
            //        RequestState = (byte)EnumRequestState.Success,
            //        ResponseTime = DateTime.Now,
            //        Content = "导入银扣指令",
            //        LockKey = luckkey,
            //        FileName = filename
            //    };

            //    m_PayTraceDal.InsertPayTrace(log);

            //    //payfac.CreatePayTraceDAL().Insert(log);
            //    //payment.SaveChanges();
            //    #endregion

            //    #region- 收款逻辑 -
            //    foreach (var item in list)
            //    {
            //        try
            //        {
            //            tran = dbConnection.BeginTransaction();
            //            var bus = businesslist.FirstOrDefault(p => p.ContractNo == item.ContractNo);
            //            if (bus == null)
            //            {
            //                tran.Rollback();
            //                Amt += item.Amount;
            //                missmsg += item.ContractNo + "_" + item.Amount + "\r\n";
            //                LogFactory.CreateLogger(compankey + "银扣")
            //                            .Info("储蓄卡号为:" + item.SavingCard + "的订单不存在");
            //                continue;
            //            }
            //            level = bus.BusinessStatus == 2 ? "1,2,3,4,21,22,23" : "3,4,22";
            //            models = context.Database.SqlQuery<DunModel>(
            //                string.Format(selectsql, bus.BusinessID, level, item.Amount, level)).ToList();
            //            if (models.Count() == 0)
            //            {
            //                tran.Rollback();
            //                Amt += item.Amount;
            //                missmsg += item.ContractNo + "_" + item.Amount + "\r\n";
            //                LogFactory.CreateLogger(compankey + "银扣")
            //                                .Info("成功收款金额不匹配：" + item.ContractNo + ":" + item.Amount);
            //                continue;
            //            }
            //            foreach (var model in models)
            //            {
            //                dal.ReceviedForBankBillitems(model.BillID, model.BillItemID, model.Amt, recviedtime
            //                    , operateid, log.PayTraceID);
            //            }
            //            tran.Commit();
            //        }
            //        catch
            //        {
            //            tran.Rollback();
            //            Amt += item.Amount;
            //            missmsg += item.ContractNo + "_" + item.Amount + "\r\n";
            //            LogFactory.CreateLogger(compankey + "银扣")
            //                            .Info("成功收款填帐失败：" + item.ContractNo + ":" + item.Amount);
            //            continue;
            //        }
            //    }
            //    #endregion

            //    #region 更新收款后字段
            //    log.RequestAmount = log.PayTraceAmount + Amt;
            //    if (log.RequestAmount == 0)
            //    {
            //        message = "回盘导入文件有成功记录但系统发现所有填帐金额不匹配,可能是因为回盘文件文件名填写错误";
            //        new FinanceContext().Database.ExecuteSqlCommand("delete from PayTrace where PayTraceID=" +
            //            log.PayTraceID);
            //        return false;
            //    }
            //    else
            //    {
            //        dal.BillDown();//沉底操作
            //        fac.CreateBusinessDAL().UpdateAllChange(new List<string> { compankey });
            //        new FinanceContext().Database.ExecuteSqlCommand("update PayTrace set RequestAmount=" +
            //            log.RequestAmount + " where PayTraceID=" + log.PayTraceID);
            //    }
            //    #endregion
            //}
            //catch (Exception ex)
            //{
            //    message = ex.Message;
            //    LogFactory.CreateLogger("导入回盘").Error(ex.ToString());
            //    tran.Rollback();
            //    return false;
            //}
            return true;
        }
        #endregion

        #region 导入扣款逻辑指令（担保方【包括所有子公司担保方】）
        /// <summary>
        /// 【适用所有担保方】
        /// </summary>
        protected virtual bool BankImportForGuarantee(List<BaseImportItem> list, int operateid,
            int serid, int lendid, string compankey, string recviedtime,
            string luckkey, out string message, out string missmsg, string filename)
        {
            message = string.Empty;
            missmsg = string.Empty;

            //FinanceContext context = new FinanceContext();
            //PaymentContext paymentContext = new PaymentContext();
            //PaymentFactory payfac = new PaymentFactory(paymentContext);
            //FinanceFactory fac = new FinanceFactory(context);
            //DbTransaction tran = null;
            //DbConnection dbConnection = ((IObjectContextAdapter)context).ObjectContext.Connection;
            //try
            //{
            //    dbConnection.Open();
            //    ReceivedDAL dal = new ReceivedDAL(context);
            //    var businesslist = context.BusinessSet
            //        .Where(p => p.ServiceSideID == serid && p.IsRepayment).ToList();
            //    string level = string.Empty;
            //    List<DunModel> models = null;
            //    decimal Amt = 0;
            //    string selectsql = "SQL\\ReceivedForBank\\SELECT_RECEIVED_Service.sql"
            //                    .ToFileContent(true, "{0}", "{1}", "{2}", "{3}");

            //    #region- 扣款指令导入日志 -
            //    PayTrace log = new PayTrace
            //    {
            //        AccountID = lendid,
            //        IncomeType = (byte)EnumIncomeType.BankIncomeGuarant,
            //        OperatorID = operateid,
            //        TraceTime = DateTime.Now,
            //        PayKind = (byte)EnumPayKind.Payment_Bank,
            //        CallDirection = (byte)EnumCallDirectionKind.Import_Pay,
            //        PayTraceAmount = -list.Sum(p => p.Amount),
            //        RequestState = (byte)EnumRequestState.Success,
            //        ResponseTime = DateTime.Now,
            //        Content = "导入银扣指令",
            //        LockKey = luckkey,
            //        FileName = filename
            //    };
            //    payfac.CreatePayTraceDAL().Insert(log);
            //    paymentContext.SaveChanges();
            //    #endregion

            //    #region- 收款逻辑 -
            //    foreach (var item in list)
            //    {
            //        try
            //        {
            //            tran = dbConnection.BeginTransaction();
            //            var bus = businesslist.FirstOrDefault(p => p.ContractNo == item.ContractNo);
            //            if (bus == null)
            //            {
            //                tran.Rollback();
            //                Amt += item.Amount;
            //                missmsg += item.ContractNo + "_" + item.Amount + "\r\n";
            //                LogFactory.CreateLogger(compankey + "银扣")
            //                            .Info("储蓄卡号为:" + item.SavingCard + "的订单不存在");
            //                continue;
            //            }
            //            level = bus.BusinessStatus == 2 ? "1,2,3,4,21,22,23" : "3,4,22";
            //            models = context.Database.SqlQuery<DunModel>(
            //                string.Format(selectsql, bus.BusinessID, level, item.Amount, level)).ToList();
            //            if (models.Count() == 0)
            //            {
            //                tran.Rollback();
            //                Amt += item.Amount;
            //                missmsg += item.ContractNo + "_" + item.Amount + "\r\n";
            //                LogFactory.CreateLogger(compankey + "银扣")
            //                                .Info("成功收款金额不匹配：" + item.ContractNo + ":" + item.Amount);
            //                continue;
            //            }
            //            foreach (var model in models)
            //            {
            //                dal.ReceviedForBankBillitems(model.BillID, model.BillItemID, model.Amt, recviedtime
            //                    , operateid, log.PayTraceID);
            //            }
            //            tran.Commit();
            //        }
            //        catch
            //        {
            //            tran.Rollback();
            //            Amt += item.Amount;
            //            missmsg += item.ContractNo + "_" + item.Amount + "\r\n";
            //            LogFactory.CreateLogger(compankey + "银扣")
            //                            .Info("成功收款填帐失败：" + item.ContractNo + ":" + item.Amount);
            //            continue;
            //        }
            //    }
            //    #endregion

            //    #region 更新收款后字段
            //    log.RequestAmount = log.PayTraceAmount + Amt;
            //    if (log.RequestAmount == 0)
            //    {
            //        message = "回盘导入文件有成功记录但系统发现所有填帐金额不匹配,可能是因为回盘文件文件名填写错误";
            //        new FinanceContext().Database.ExecuteSqlCommand("delete from PayTrace where PayTraceID=" +
            //            log.PayTraceID);
            //        return false;
            //    }
            //    else
            //    {
            //        dal.BillDown();//沉底操作
            //        fac.CreateBusinessDAL().UpdateAllChange(new List<string> { compankey });
            //        new FinanceContext().Database.ExecuteSqlCommand("update PayTrace set RequestAmount=" +
            //            log.RequestAmount + " where PayTraceID=" + log.PayTraceID);
            //    }
            //    #endregion
            //}
            //catch (Exception ex)
            //{
            //    message = ex.Message;
            //    LogFactory.CreateLogger("导入回盘").Error(ex.ToString());
            //    tran.Rollback();
            //    return false;
            //}
            return true;
        }
        #endregion

        #region 导入扣款逻辑指令（成都指令回盘）
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual bool BankImportForChengdu(List<BaseImportItem> list,
            int operateid, int serid, int lendid, string companykey, string recviedtime,
            string luckkey, out string message, out string missmsg, string filename)
        {
            message = missmsg = string.Empty;
            string selectsql = "SQL\\ReceivedForBank\\SELECT_RECEIVED_Chengdu.sql"
            .ToFileContent(true, "{0}", "{1}");

            //FinanceContext context = new FinanceContext();
            //FinanceFactory fac = new FinanceFactory(context);
            //PaymentContext paymentContext = new PaymentContext();

            //PaymentFactory payfac = new PaymentFactory(paymentContext);
            //DbTransaction tran = null;
            //DbConnection dbConnection = ((IObjectContextAdapter)context).ObjectContext.Connection;
            //try
            //{
            //    dbConnection.Open();
            //    var dal = new ReceivedDAL(context);
            //    var businesslist = context.BusinessSet
            //        .Where(p => p.ServiceSideID == lendid && p.IsRepayment).ToList();
            //    List<DunModel> models = null;
            //    decimal Amt = 0;
            //    #region 扣款指令导入日志
            //    PayTrace log = new PayTrace
            //    {
            //        AccountID = serid,
            //        IncomeType = (byte)EnumIncomeType.BankIncomeLend,
            //        OperatorID = operateid,
            //        TraceTime = DateTime.Now,
            //        PayKind = (byte)EnumPayKind.Payment_Bank,
            //        CallDirection = (byte)EnumCallDirectionKind.Import_Pay,
            //        PayTraceAmount = -list.Sum(p => p.Amount),
            //        RequestState = (byte)EnumRequestState.Success,
            //        Content = "导入银扣指令",
            //        ResponseTime = DateTime.Now,
            //        LockKey = luckkey,
            //        FileName = filename
            //    };
            //    payfac.CreatePayTraceDAL().Insert(log);
            //    paymentContext.SaveChanges();
            //    #endregion

            //    #region- 收款逻辑 -
            //    foreach (var item in list)
            //    {
            //        try
            //        {
            //            tran = dbConnection.BeginTransaction();
            //            var bus = businesslist.FirstOrDefault(p => p.ContractNo == item.ContractNo);
            //            if (bus == null)
            //            {
            //                tran.Rollback();
            //                Amt += item.Amount;
            //                LogFactory.CreateLogger("成都银扣指令")
            //                            .Info("储蓄卡号为:" + item.SavingCard + "的订单不存在");
            //                continue;
            //            }
            //            models = context.Database.SqlQuery<DunModel>(
            //                string.Format(selectsql, bus.BusinessID, item.Amount)).ToList();
            //            if (models.Count() == 0)
            //            {
            //                tran.Rollback();
            //                Amt += item.Amount;
            //                missmsg += item.ContractNo + "_" + item.Amount;
            //                LogFactory.CreateLogger("成都银扣")
            //                                .Info("成功收款金额不匹配：" + item.ContractNo + ":" + item.Amount);
            //                continue;
            //            }
            //            foreach (var model in models)
            //            {
            //                dal.ReceviedForBankBillitems(model.BillID, model.BillItemID, model.Amt, recviedtime
            //                    , operateid, log.PayTraceID);
            //            }
            //            tran.Commit();
            //        }
            //        catch
            //        {
            //            tran.Rollback();
            //            Amt += item.Amount;
            //            missmsg += item.ContractNo + "_" + item.Amount;
            //            LogFactory.CreateLogger("成都银扣")
            //                            .Info("成功收款填帐失败：" + item.ContractNo + ":" + item.Amount);
            //            continue;
            //        }
            //    }
            //    #endregion

            //    #region 更新收款后字段
            //    log.RequestAmount = log.PayTraceAmount + Amt;
            //    if (log.RequestAmount == 0)
            //    {
            //        message = "回盘导入文件有成功记录但系统发现所有填帐金额不匹配,可能是因为回盘文件文件名填写错误";
            //        new FinanceContext().Database.ExecuteSqlCommand("delete from PayTrace where PayTraceID=" +
            //            log.PayTraceID);
            //        return false;
            //    }
            //    else
            //    {
            //        dal.BillDown();//沉底操作
            //        fac.CreateBusinessDAL().UpdateAllChange(new List<string> { companykey });
            //        new FinanceContext().Database.ExecuteSqlCommand("update PayTrace set RequestAmount=" +
            //            log.RequestAmount + " where PayTraceID=" + log.PayTraceID);
            //    }
            //    #endregion
            //}
            //catch (Exception ex)
            //{
            //    message = ex.Message;
            //    tran.Rollback();
            //    return false;
            //}
            return true;
        }
        #endregion

        #region 导入扣款逻辑指令（信托方【包括所有渤海信托方】）
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual bool BankImportForBHXT(List<BaseImportItem> list,
            int operateid, int serid, int lendid, string companykey, string recviedtime, string luckkey,
            out string message, out string mismsg, string filename)
        {
            message = mismsg = string.Empty;
            string selectsqlbill = "SQL\\ReceivedForBank\\SELECT_RECEIVED_BHXT_Bill.sql"
                                                                .ToFileContent(true, "{0}", "{1}");
            string selectsqlcurrent = "SQL\\ReceivedForBank\\SELECT_RECEIVED_BHXT_Current.sql"
                                                                .ToFileContent(true, "{0}", "{1}");
            string selectsqlbus = "SQL\\ReceivedForBank\\SELECT_RECEIVED_BHXT_Business.sql"
                                                     .ToFileContent(true, "{0}", "{1}", "{2}", "{3}");
            //FinanceContext context = new FinanceContext();
            //FinanceFactory fac = new FinanceFactory(context);
            //PaymentContext paymentContext = new PaymentContext();
            //PaymentFactory payfac = new PaymentFactory(paymentContext);
            //DbTransaction tran = null;
            //DbConnection dbConnection = ((IObjectContextAdapter)context).ObjectContext.Connection;
            //var dal = new ReceivedDAL(context);
            //dbConnection.Open();
            //try
            //{
            //    List<int> bids = new List<int>();
            //    List<DunModel> models = new List<DunModel>();
            //    decimal Amt = 0;

            //    #region 扣款指令导入日志
            //    PayTrace log = new PayTrace
            //    {
            //        AccountID = lendid,
            //        IncomeType = (byte)EnumIncomeType.BankIncomeLend,
            //        OperatorID = operateid,
            //        TraceTime = DateTime.Now,
            //        PayKind = (byte)EnumPayKind.Payment_Bank,
            //        CallDirection = (byte)EnumCallDirectionKind.Import_Pay,
            //        PayTraceAmount = -list.Sum(p => p.Amount),
            //        RequestState = (byte)EnumRequestState.Success,
            //        Content = "导入银扣指令",
            //        ResponseTime = DateTime.Now,
            //        LockKey = luckkey,
            //        FileName = filename
            //    };
            //    payfac.CreatePayTraceDAL().Insert(log);
            //    paymentContext.SaveChanges();
            //    #endregion

            //    #region- 收款逻辑 -
            //    foreach (var item in list)
            //    {
            //        try
            //        {
            //            tran = dbConnection.BeginTransaction();
            //            if (item.DunLevel == 0)
            //            {
            //                tran.Rollback();
            //                Amt += item.Amount;
            //                mismsg += item.ContractNo + "_" + item.Amount;
            //                LogFactory.CreateLogger(companykey + "银扣")
            //                            .Info("科目名称填写错误：" + item.ContractNo + "**" + item.Amount);
            //                continue;
            //            }
            //            else if (item.DunLevel == 10)
            //            {
            //                models = context.Database.SqlQuery<DunModel>(
            //                        string.Format(selectsqlbill, item.ContractNo, item.Amount)).ToList();
            //            }
            //            else if (item.DunLevel == 21)
            //            {
            //                models = context.Database.SqlQuery<DunModel>(
            //                        string.Format(selectsqlbus, item.ContractNo, 21, item.Amount, 21)).ToList();
            //            }
            //            else if (item.DunLevel == 22)
            //            {
            //                models = context.Database.SqlQuery<DunModel>(
            //                        string.Format(selectsqlcurrent, item.ContractNo, item.Amount)).ToList();
            //            }
            //            else if (item.DunLevel == 23)
            //            {
            //                models = context.Database.SqlQuery<DunModel>(
            //                        string.Format(selectsqlbus, item.ContractNo, 23, item.Amount, 23)).ToList();
            //            }
            //            if (models.Count() == 0)
            //            {
            //                tran.Rollback();
            //                Amt += item.Amount;
            //                mismsg += item.ContractNo + "_" + item.Amount;
            //                LogFactory.CreateLogger(companykey + "银扣")
            //                            .Info("成功收款金额不匹配：" + item.ContractNo + ":" + item.Amount);
            //                continue;
            //            }
            //            foreach (var model in models)
            //            {
            //                dal.ReceviedForBankBillitems(model.BillID, model.BillItemID, model.Amt, recviedtime
            //                    , operateid, log.PayTraceID);
            //            }
            //            tran.Commit();
            //        }
            //        catch
            //        {
            //            tran.Rollback();
            //            Amt += item.Amount;
            //            mismsg += item.ContractNo + "_" + item.Amount;
            //            LogFactory.CreateLogger(companykey + "银扣")
            //                            .Info("成功收款填帐失败：" + item.ContractNo + ":" + item.Amount);
            //            continue;
            //        }
            //    }
            //    #endregion

            //    #region 更新收款后字段
            //    log.RequestAmount = log.PayTraceAmount + Amt;
            //    if (log.RequestAmount == 0)
            //    {
            //        message = "回盘导入文件有成功记录但系统发现所有填帐金额不匹配,可能是因为回盘文件文件名填写错误";
            //        new FinanceContext().Database.ExecuteSqlCommand("delete from PayTrace where PayTraceID=" +
            //            log.PayTraceID);
            //        return false;
            //    }
            //    else
            //    {
            //        dal.BillDown();//沉底操作
            //        fac.CreateBusinessDAL().UpdateAllChange(new List<string> { companykey });
            //        new FinanceContext().Database.ExecuteSqlCommand("update PayTrace set RequestAmount=" +
            //            log.RequestAmount + " where PayTraceID=" + log.PayTraceID);
            //    }
            //    #endregion
            //}
            //catch (Exception e)
            //{
            //    message = e.ToString();
            //    LogFactory.CreateLogger("导入回盘").Error(e.ToString());
            //    tran.Rollback();
            //    return false;
            //}
            return true; ;
        }
        #endregion

        #region 导入扣款逻辑指令（信托方【包括维信外贸和维视外贸】）
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual bool BankImportForDWJM(List<BaseImportItem> list,
            int operateid, int serid, int lendid, string companykey, string recviedtime, string luckkey,
            out string message, out string missmsg, string filename, bool isall = false)
        {
            message = missmsg = string.Empty;
            string selectsql = "SQL\\ReceivedForBank\\SELECT_RECEIVED_DWJM.sql"
                .ToFileContent(true, "{0}", "{1}", "{2}");

            //FinanceContext context = new FinanceContext();
            //FinanceFactory fac = new FinanceFactory(context);
            //PaymentContext paymentContext = new PaymentContext();
            //PaymentFactory payfac = new PaymentFactory(paymentContext);
            //DbTransaction tran = null;
            //DbConnection dbConnection = ((IObjectContextAdapter)context).ObjectContext.Connection;
            //PayTrace log = null;
            //try
            //{
            //    dbConnection.Open();
            //    var dal = new ReceivedDAL(context);
            //    List<DunModel> models = null;
            //    decimal Amt = 0;

            //    #region 扣款指令导入日志
            //    log = new PayTrace
            //    {
            //        AccountID = lendid,
            //        IncomeType = (byte)EnumIncomeType.BankIncomeLend,
            //        OperatorID = operateid,
            //        TraceTime = DateTime.Now,
            //        PayKind = (byte)EnumPayKind.Payment_Bank,
            //        CallDirection = (byte)EnumCallDirectionKind.Import_Pay,
            //        PayTraceAmount = -list.Sum(p => p.Amount),
            //        RequestState = (byte)EnumRequestState.Success,
            //        Content = "导入银扣指令",
            //        ResponseTime = DateTime.Now,
            //        LockKey = luckkey,
            //        FileName = filename
            //    };
            //    payfac.CreatePayTraceDAL().Insert(log);
            //    paymentContext.SaveChanges();
            //    #endregion

            //    string level = isall ? "10,20,30" : "10,20";

            //    #region- 收款逻辑 -
            //    foreach (var item in list)
            //    {
            //        try
            //        {
            //            tran = dbConnection.BeginTransaction();
            //            models = context.Database.SqlQuery<DunModel>(
            //                    string.Format(selectsql, item.ContractNo, item.Amount, level)).ToList();
            //            if (models.Count() == 0)
            //            {
            //                tran.Rollback();
            //                Amt += item.Amount;
            //                missmsg += item.ContractNo + "_" + item.Amount + "\r\n";
            //                LogFactory.CreateLogger(companykey + "银扣")
            //                            .Info("成功收款金额不匹配：" + item.ContractNo + ":" + item.Amount);
            //                continue;
            //            }
            //            foreach (var model in models)
            //            {
            //                dal.ReceviedForBankBillitems(model.BillID, model.BillItemID, model.Amt, recviedtime
            //                    , operateid, log.PayTraceID);
            //            }
            //            tran.Commit();
            //        }
            //        catch
            //        {
            //            tran.Rollback();
            //            Amt += item.Amount;
            //            missmsg += item.ContractNo + "_" + item.Amount + "\r\n";
            //            LogFactory.CreateLogger(companykey + "银扣")
            //                            .Info("成功收款填帐失败：" + item.ContractNo + ":" + item.Amount);
            //            continue;
            //        }
            //    }
            //    #endregion

            //    #region 更新收款后字段
            //    log.RequestAmount = log.PayTraceAmount + Amt;
            //    //if (context.Database.Connection.State == ConnectionState.Closed)
            //    //    context.Database.Connection.Open();
            //    if (log.RequestAmount == 0)
            //    {
            //        message = "回盘导入文件有成功记录但系统发现所有填帐金额不匹配,可能是因为回盘文件文件名填写错误";
            //        new FinanceContext().Database.ExecuteSqlCommand("delete from PayTrace where PayTraceID=" +
            //            log.PayTraceID);
            //        return false;
            //    }
            //    else
            //    {
            //        dal.BillDown();//沉底操作
            //        fac.CreateBusinessDAL().UpdateAllChange(new List<string> { companykey });
            //        new FinanceContext().Database.ExecuteSqlCommand("update PayTrace set RequestAmount=" +
            //            log.RequestAmount + " where PayTraceID=" + log.PayTraceID);
            //    }
            //    #endregion
            //}
            //catch (Exception e)
            //{
            //    message = "回盘成功，更新收款字段出错";
            //    LogFactory.CreateLogger("导入回盘").Error(e.ToString());
            //    return false;
            //}
            return true;
        }
        #endregion

        #endregion

        #region- 导出模版配置 -

        /// <summary>
        /// 通过模版配置，获取模版的格式处理方式TXT（导出）
        /// </summary>
        /// <param name="deductcommand"></param>
        /// <returns></returns>
        public ExportTemplate GetBankExportTemplate(DeductCommand deductcommand)
        {
            switch (deductcommand.ExportCmdType.ValueToEnum<EnumExportCmdKind>())
            {
                case EnumExportCmdKind.TemplateOne:
                    return new BankExportOne();
                case EnumExportCmdKind.TemplateTwo:
                    return new BankExportTwo();
                case EnumExportCmdKind.TemplateThree:
                    return new BankExportThree();
                case EnumExportCmdKind.TemplateFour:
                    return new BankExportFour();
                case EnumExportCmdKind.TemplateSeven:
                    return new BankExportSeven();
                default:
                    return null;
            }
        }

        /// <summary>
        /// 通过模版配置，获取模版的格式处理方式EXCEL（导出）
        /// </summary>
        /// <param name="deductcommand"></param>
        /// <returns></returns>
        public ExportTemplate2 GetBankExportTemplate2(DeductCommand deductcommand, bool isbankexport = false)
        {
            EnumExportCmdKind cmd = deductcommand.ExportDetailCmdType.ValueToEnum<EnumExportCmdKind>();
            if (isbankexport)
                cmd = deductcommand.ExportCmdType.ValueToEnum<EnumExportCmdKind>();
            switch (cmd)
            {
                case EnumExportCmdKind.TemplateFive://工行格式
                    return new BankExportFive();
                case EnumExportCmdKind.TemplateSix://服务方明细格式
                    return new BankExportSix();
                case EnumExportCmdKind.TemplateEight://信托方明细格式
                    return new BankExportEight();
                default:
                    return null;
            }
        }

        #endregion

        #region- 导入模版配置 -
        /// <summary>
        /// 通过模版配置，获取模版的格式处理方式
        /// </summary>
        /// <param name="deductcommand"></param>
        /// <returns></returns>
        public ImportTemplate GetBankImportTemplate(DeductCommand deductcommand)
        {
            switch (deductcommand.AnalysisCmdType.ValueToEnum<EnumImportCmdKind>())
            {
                case EnumImportCmdKind.Template1:
                    return new BankImportOne(deductcommand);
                case EnumImportCmdKind.Template2:
                    return new BankImportTwo(deductcommand);
                case EnumImportCmdKind.Template3:
                    return new BankImportThree(deductcommand);
                case EnumImportCmdKind.Template4:
                    return new BankImportFour(deductcommand);
                case EnumImportCmdKind.Template5:
                    return new BankImportFive(deductcommand);
                case EnumImportCmdKind.Template6:
                    return new BankImportSix(deductcommand);
                default:
                    return null;
            }
        }
        #endregion

        #region- 执行导出数据操作 -
        /// <summary>
        /// 执行SQL记录
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="dcd"></param>
        /// <param name="operatorid"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        protected bool ExportToSql(int lendid, EnumIncomeType incometype, int operatorid, decimal Amount, string lockkey, int filecount, string filename = "")
        {
            PayTrace log = new PayTrace
            {
                AccountID = lendid,
                IncomeType = (byte)incometype,
                OperatorID = operatorid,
                TraceTime = DateTime.Now,
                PayTraceAmount = Amount,
                PayKind = (byte)EnumPayKind.Payment_Bank,
                CallDirection = (byte)EnumCallDirectionKind.Export_Pay,
                RequestState = (byte)EnumRequestState.Success,
                ResponseTime = DateTime.Now,
                Content = "导出银扣指令",
                LockKey = lockkey,
                FileNum = filecount,
                FileName = filename
            };

            return m_PayTraceDal.InsertPayTrace(log);

            //_PaymentFactory.CreatePayTraceDAL().Insert(log);
            //return _PaymentContext.SaveChanges() > 0 ? true : false;
        }
        #endregion

        #region- 计算款项 -
        /// <summary>
        /// 获取未足额支付本息账单款项
        /// </summary>
        /// <returns></returns>
        protected List<BillItem> GetNoFullPayBillItem(List<BillItem> list
            , List<byte> subjs, bool iscurrent = false)
        {
            return list.Where(p => p.CurrentStatus.FullPaidTime == null && p.CurrentStatus.IsCurrent == iscurrent &&
                    subjs.Contains(p.Basic.Subject)).ToList();
        }

        /// <summary>
        /// 获取未足额支付本息账单款项
        /// </summary>
        /// <returns></returns>
        protected List<BillItem> GetNoFullPayBX(List<BillItem> list)
        {
            return list.Where(p => p.CurrentStatus.FullPaidTime == null &&
                    (p.Basic.Subject == (byte)EnumCostSubject.Capital
                    || p.Basic.Subject == (byte)EnumCostSubject.Interest)).ToList();
        }

        /// <summary>
        /// 获取未足额支付服务费、担保费款项
        /// </summary>
        /// <returns></returns>
        protected virtual List<BillItem> GetNoFullPayFD(List<BillItem> list)
        {
            return list.Where(p => p.CurrentStatus.FullPaidTime == null &&
                    (p.Basic.Subject == (byte)EnumCostSubject.GuaranteeFee
                    || p.Basic.Subject == (byte)EnumCostSubject.ServiceFee)).ToList();
        }
        /// <summary>
        /// 获取未足额支付管理费款项
        /// </summary>
        /// <returns></returns>
        protected virtual List<BillItem> GetNoFullPayGLF(List<BillItem> list)
        {
            return list.Where(p => p.CurrentStatus.FullPaidTime == null &&
                    p.Basic.Subject == (byte)EnumCostSubject.ServiceFee).ToList();
        }
        /// <summary>
        /// 获取未足额支付本息扣失账单款项
        /// </summary>
        /// <returns></returns>
        protected BillItem GetNoFullPayBXKS(List<BillItem> list)
        {
            return list.FirstOrDefault(p => p.CurrentStatus.FullPaidTime == null &&
                p.Basic.Subject == (byte)EnumCostSubject.InterestBuckleFail);
        }

        /// <summary>
        /// 获取未足额支付罚息账单款项
        /// </summary>
        /// <returns></returns>
        protected BillItem GetNoFullPayFX(List<BillItem> list)
        {
            return list.FirstOrDefault(p => p.CurrentStatus.FullPaidTime == null &&
                    p.Basic.Subject == (byte)EnumCostSubject.PunitiveInterest);
        }

        /// <summary>
        /// 获取未足额支付服务费扣失账单款项
        /// </summary>
        /// <returns></returns>
        public virtual BillItem GetNoFullPayFWFKS(List<BillItem> list)
        {
            return list.FirstOrDefault(p => p.CurrentStatus.FullPaidTime == null &&
                    p.Basic.Subject == (byte)EnumCostSubject.ServiceBuckleFail);
        }

        /// <summary>
        /// 判断当前账单是否需要导出当前账单款项
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public bool IsExporeDQ(Bill bill)
        {
            //List<DeductSequence> dedes = m_DeductSequenceDal
            //         .GetDeductSequenceList(bill.Business.DSeqType.ValueToEnum<EnumDeductSeqKind>());

            //var dede = dedes.FirstOrDefault(p => p.DeductTime < DateTime.Now);
            //if (dede == null)
            //    return false;

            //if (dede.DeductMonth != GetDate() && dede.BillRegion == (byte)EnumBillRegion.Overdue)
            //    return false;
            return true;
        }

        /// <summary>
        /// 获取账单的本息扣失
        /// </summary>
        /// <param name="bills"></param>
        /// <param name="IsCotainsDQ">是否包含当期</param>
        /// <returns></returns>
        protected List<BillItem> GetTotalBxks(List<Bill> bills, bool IsCotainsDQ)
        {
            var list = bills.Select(p => new
            {
                Memb = p.BillItems
                    .Where(o => o.CurrentStatus.FullPaidTime == null).ToList()
            });
            return null;
        }
        #endregion

        #region- 辅助方法 -
        /// <summary>
        /// 判断是否是当期账单
        /// </summary>
        /// <returns></returns>
        public string GetDate()
        {
            int month = DateTime.Now.Month;
            string months = string.Empty;
            if (month < 10)
                months = "0" + month;
            else
                months = month.ToString();
            return DateTime.Now.Year + "/" + months;
        }
        /// <summary>
        /// 获取款项名称
        /// </summary>
        /// <param name="billitem"></param>
        /// <returns></returns>
        protected string GetPurpose(BillItem billitem)
        {
            if (billitem.Basic.Subject == (byte)EnumCostSubject.Capital
                || billitem.Basic.Subject == (byte)EnumCostSubject.Interest)
            {
                return "本息";
            }
            else if (billitem.Basic.Subject == (byte)EnumCostSubject.GuaranteeFee
                || billitem.Basic.Subject == (byte)EnumCostSubject.ServiceFee)
            {
                return "服务担保管理费";
            }
            else
            {
                return billitem.Basic.Subject.ValueToDesc<EnumCostSubject>();
            }
        }

        /// <summary>
        /// 判断IF条件
        /// </summary>
        /// <param name="billitem"></param>
        /// <param name="businessid"></param>
        /// <returns></returns>
        protected bool GetIfResult(List<BillItem> billitem, Bill bill)
        {
            if (billitem.Count < 1)
                return false;
            return (bill.Basic.BillMonth.ToString().Trim() == GetDate()
                    && billitem.Count() > 0);
        }
        /// <summary>
        /// 获取导出模版配置中为TXT的配置
        /// </summary>
        /// <param name="cmdlist"></param>
        /// <param name="isTXT"></param>
        /// <returns></returns>
        public DeductCommand GetCmd(List<DeductCommand> cmdlist, bool isTXT)
        {
            return cmdlist.FirstOrDefault(p => p.AnalysisCmdType == 0 != isTXT);
        }
        public List<DeductCommand> GetCmdList(List<DeductCommand> cmdlist, bool isTXT)
        {
            return cmdlist.Where(p => p.AnalysisCmdType == 0 != isTXT).ToList();
        }
        /// <summary>
        ///匹配回盘结果实体
        /// </summary>
        public class DunModel
        {
            public long BillItemID { get; set; }
            public decimal Amt { get; set; }
            public long BillID { get; set; }
        }
        #endregion
        #endregion
    }
}

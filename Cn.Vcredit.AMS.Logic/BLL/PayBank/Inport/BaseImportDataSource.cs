using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cn.Vcredit.AMS.Common.DBData;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Logic.BLL.PayBank.Export;

namespace Cn.Vcredit.AMS.Logic.BLL.PayBank.Inport
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年7月3日
    /// 导入模版基类
    /// </summary>
    public class BaseImportDataSource
    {
        #region - 属性字段 -
        //protected static FinanceContext _FinanceContext = new FinanceContext();

        public List<DeductCommand> DeductCommandList { get; set; }

        public List<DeductCommand> LendingDc
        {
            get
            {
                if (DeductCommandList.Count() > 0)
                {
                    return DeductCommandList.Where(p => p.LendingSideID != p.ServiceSideID).ToList();
                }
                return null;
            }
        }
        public DeductCommand SerDc
        {
            get
            {
                if (DeductCommandList.Count() > 0)
                {
                    return DeductCommandList.FirstOrDefault(p => p.LendingSideID == p.ServiceSideID);
                }
                return null;
            }
        }

        #endregion

        #region - 构造函数 -
        public BaseImportDataSource(List<DeductCommand> cmdlist)
        {
            this.DeductCommandList = cmdlist;
        }
        #endregion

        #region- 功能函数 -

        #region 获取导出结果集(TXT)
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int,List<byte>> GetBillList(out decimal TotalAmount, List<Customer> customers)
        {
            TotalAmount = 0;
            Dictionary<int, List<byte>> retbyte = new Dictionary<int, List<byte>>();
            if (DeductCommandList.Count < 0)//如果没有此子公司的配置，则直接返回null
                return null;
            var query = _FinanceFactory.CreateBillDAL().GetBillList(EnumBillStatus.Debts)
               .Where(p => p.Business.ServicerSideID == DeductCommandList.FirstOrDefault().ServiceSideID)
               .GroupBy(p => p.BusinessID).Select(z => new
               {
                   ListBill = z.ToList()
               });
            if (query.Count() < 0)
                return null;
            List<BillItem> fwfksbillitems = new List<BillItem>();
            retbyte.Add(SerDc.ServiceSideID, new List<byte>());
            Dictionary<int, List<BillItem>> bxksbillitems = new Dictionary<int, List<BillItem>>();
            Dictionary<int, List<BillItem>> fxbillitems = new Dictionary<int, List<BillItem>>();
            foreach (var a in LendingDc)
            {
                retbyte.Add(a.LendingSideID, new List<byte>());
                bxksbillitems.Add(a.LendingSideID, new List<BillItem>());
                fxbillitems.Add(a.LendingSideID, new List<BillItem>());
            }
            foreach (var list in query)
            {
                List<Bill> bills = list.ListBill;
                for (int i = 0; i < bills.Count(); i++)
                {
                    if (bills[i].BillMonth.Trim() == GetDate())//判断是否是当期账单（若能，把存在逾期的本息扣失和罚息添加）
                    {
                        if (!IsExporeDQ(bills[i]))
                            continue;
                    }

                    if (GetIfResult(fwfksbillitems))//导出当期服务、担保费前，先把逾期服务费扣失添加上
                    {
                        TotalAmount += fwfksbillitems.Sum(p=>p.Amount-p.ReceivedAmt);
                        retbyte[SerDc.ServiceSideID].AddRange(GetBankExportTemplate(SerDc).FomartTemplate(fwfksbillitems, customers));
                        fwfksbillitems = new List<BillItem>();
                    }
                    var fd = GetNoFullPayFD(bills[i].BillItems);//服务费、担保费
                    if (fd.Count > 0)
                    {
                        TotalAmount += fd.Sum(p => p.Amount - p.ReceivedAmt);
                        retbyte[SerDc.ServiceSideID].AddRange(GetBankExportTemplate(SerDc).FomartTemplate(fd, customers));
                    }
                    var fwfks = GetNoFullPayFWFKS(bills[i].BillItems);//服务费扣失
                    if ( fwfks!= null)
                    {
                        fwfksbillitems.Add(fwfks);
                    }

                    for (int j = 0; j < LendingDc.Count; j++)
                    {
                        if (GetIfResult(bxksbillitems[LendingDc[j].LendingSideID]))//本息扣失
                        {
                            TotalAmount += bxksbillitems[LendingDc[j].LendingSideID].Sum(p => p.Amount - p.ReceivedAmt);
                            retbyte[LendingDc[j].LendingSideID].AddRange(GetBankExportTemplate(LendingDc[j]).FomartTemplate(bxksbillitems[LendingDc[j].LendingSideID], customers));
                            bxksbillitems[LendingDc[j].LendingSideID] = new List<BillItem>();
                        }
                        if (GetIfResult(fxbillitems[LendingDc[j].LendingSideID]))//罚息
                        {
                            TotalAmount += fxbillitems[LendingDc[j].LendingSideID].Sum(p => p.Amount - p.ReceivedAmt);
                            retbyte[LendingDc[j].LendingSideID].AddRange(GetBankExportTemplate(LendingDc[j]).FomartTemplate(fxbillitems[LendingDc[j].LendingSideID], customers));
                            fxbillitems[LendingDc[j].LendingSideID] = new List<BillItem>();
                        }
                        if (bills[i].Business.LendingSideID == LendingDc[j].LendingSideID)
                        {
                            var bx = GetNoFullPayBX(bills[i].BillItems);//本息
                            if (bx.Count > 0)
                            {
                                TotalAmount += bx.Sum(p => p.Amount - p.ReceivedAmt);
                                retbyte[LendingDc[j].LendingSideID].AddRange(GetBankExportTemplate(LendingDc[j]).FomartTemplate(bx, customers));
                            }
                            var bxks = GetNoFullPayBXKS(bills[i].BillItems);//本息扣失
                            if ( bxks != null)
                            {
                                bxksbillitems[LendingDc[j].LendingSideID].Add(bxks);
                            }
                            var fx = GetNoFullPayFX(bills[i].BillItems);//罚息
                            if ( fx!= null)
                            {
                                fxbillitems[LendingDc[j].LendingSideID].Add(fx);
                            }
                        }
                    }
                }
                for (int k = 0; k < LendingDc.Count; k++)
                {
                    if (bxksbillitems[LendingDc[k].LendingSideID].Count() > 0)
                    {
                        TotalAmount += bxksbillitems[LendingDc[k].LendingSideID].Sum(p => p.Amount - p.ReceivedAmt);
                        retbyte[LendingDc[k].LendingSideID].AddRange(GetBankExportTemplate(LendingDc[k]).FomartTemplate(bxksbillitems[LendingDc[k].LendingSideID], customers));
                        bxksbillitems[LendingDc[k].LendingSideID] = new List<BillItem>();
                    }
                    if (fxbillitems[LendingDc[k].LendingSideID].Count() > 0)
                    {
                        TotalAmount += fxbillitems[LendingDc[k].LendingSideID].Sum(p => p.Amount - p.ReceivedAmt);
                        retbyte[LendingDc[k].LendingSideID].AddRange(GetBankExportTemplate(LendingDc[k]).FomartTemplate(fxbillitems[LendingDc[k].LendingSideID], customers));
                        fxbillitems[LendingDc[k].LendingSideID] = new List<BillItem>();
                    }
                }
                if (fwfksbillitems.Count() > 0)
                {
                    TotalAmount += fwfksbillitems.Sum(p => p.Amount - p.ReceivedAmt);
                    retbyte[SerDc.ServiceSideID].AddRange(GetBankExportTemplate(SerDc).FomartTemplate(fwfksbillitems, customers));
                    fwfksbillitems = new List<BillItem>();
                }
            }
            return retbyte;
        }
        #endregion

        #region 获取导出结果集(服务方、信托方为同一家公司 TXT)
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual List<byte> GetBillList2(out decimal totalamount, List<Customer> customers)
        {
            List<byte> retbyte = new List<byte>(); totalamount = 0;
            List<byte> retbyte2 = new List<byte>();
            if (DeductCommandList.Count < 0)//如果没有此子公司的配置，则直接返回null
                return null;
            var query = _FinanceFactory.CreateBillDAL().GetBillList(EnumBillStatus.Debts)
               .Where(p => p.Business.ServicerSideID == DeductCommandList.FirstOrDefault().ServiceSideID)
               .GroupBy(p => p.BusinessID).Select(z => new
               {
                   ListBill = z.ToList()
               });
            if (query.Count() < 0)
                return null;
            List<BillItem> fwfksbillitems = new List<BillItem>();
            List<BillItem> bxksbillitems = new List<BillItem>();
            List<BillItem> fxbillitems = new List<BillItem>();
            foreach (var list in query)
            {
                List<Bill> bills = list.ListBill;
                for (int i = 0; i < bills.Count(); i++)
                {
                    if (bills[i].BillMonth.Trim() == GetDate())//判断是否是当期账单（若能，把存在逾期的本息扣失和罚息添加）
                    {
                        if (!IsExporeDQ(bills[i]))
                            continue;
                    }

                    if (GetIfResult(fwfksbillitems))//导出当期服务、担保费前，先把逾期服务费扣失添加上
                    {
                        totalamount += fwfksbillitems.Sum(p => p.Amount - p.ReceivedAmt);
                        retbyte2.AddRange(GetBankExportTemplate(SerDc).FomartTemplate(fwfksbillitems, customers));
                        fwfksbillitems = new List<BillItem>();
                    }
                    if (GetIfResult(bxksbillitems))//本息扣失
                    {
                        totalamount += bxksbillitems.Sum(p => p.Amount - p.ReceivedAmt);
                        retbyte.AddRange(GetBankExportTemplate(SerDc).FomartTemplate(bxksbillitems, customers));
                        bxksbillitems = new List<BillItem>();
                    }
                    if (GetIfResult(fxbillitems))//罚息
                    {
                        totalamount += fxbillitems.Sum(p => p.Amount - p.ReceivedAmt);
                        retbyte.AddRange(GetBankExportTemplate(SerDc).FomartTemplate(fxbillitems, customers));
                        fxbillitems = new List<BillItem>();
                    }
                    var bx = GetNoFullPayBX(bills[i].BillItems);//本息
                    if (bx.Count > 0)
                    {
                        totalamount += bx.Sum(p => p.Amount - p.ReceivedAmt);
                        retbyte.AddRange(GetBankExportTemplate(SerDc).FomartTemplate(bx, customers));
                    }
                    var bxks = GetNoFullPayBXKS(bills[i].BillItems);//本息扣失
                    if (bxks!= null)
                    {
                        bxksbillitems.Add(bxks);
                    }
                    var fx = GetNoFullPayFX(bills[i].BillItems);//罚息
                    if (fx != null)
                    {
                        fxbillitems.Add(fx);
                    }
                    var fd = GetNoFullPayFD(bills[i].BillItems);//服务费、担保费
                    if (fd.Count > 0)
                    {
                        totalamount += fd.Sum(p => p.Amount - p.ReceivedAmt);
                        retbyte2.AddRange(GetBankExportTemplate(SerDc).FomartTemplate(fd, customers));
                    }
                    var fwfks = GetNoFullPayFWFKS(bills[i].BillItems);//服务费扣失
                    if (fwfks != null)
                    {
                        fwfksbillitems.Add(fwfks);
                    }
                }

                if (bxksbillitems.Count() > 0)
                {
                    totalamount += bxksbillitems.Sum(p => p.Amount - p.ReceivedAmt);
                    retbyte.AddRange(GetBankExportTemplate(SerDc).FomartTemplate(bxksbillitems, customers));
                    bxksbillitems = new List<BillItem>();
                }
                if (fxbillitems.Count() > 0)
                {
                    totalamount += fxbillitems.Sum(p => p.Amount - p.ReceivedAmt);
                    retbyte.AddRange(GetBankExportTemplate(SerDc).FomartTemplate(fxbillitems, customers));
                    fxbillitems = new List<BillItem>();
                }
                if (fwfksbillitems.Count() > 0)
                {
                    totalamount += fwfksbillitems.Sum(p => p.Amount - p.ReceivedAmt);
                    retbyte2.AddRange(GetBankExportTemplate(SerDc).FomartTemplate(fwfksbillitems, customers));
                    fwfksbillitems = new List<BillItem>();
                }
                if (retbyte2.Count > 0)
                {
                    retbyte.AddRange(retbyte2);
                    retbyte2 = new List<byte>();
                }
            }
            return retbyte;
        }
        #endregion

        #region 获取导出结果集(Excel)
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int,List<string[]>> GetBillListExcel(out decimal TotalAmount,
            List<Customer> customers,List<BankAccount> bankaccounts,List<Enumeration> enums,string bankkey)
        {
            TotalAmount = 0; int num = 1; int num2 = 1;
            Dictionary<int, List<string[]>> retstring = new Dictionary<int, List<string[]>>();
            if (DeductCommandList.Count < 0)//如果没有此子公司的配置，则直接返回null
                return null;
            var query = _FinanceFactory.CreateBillDAL().GetBillList(EnumBillStatus.Debts)
               .Where(p => p.Business.ServicerSideID == DeductCommandList.FirstOrDefault().ServiceSideID
               && p.Business.BankKey ==bankkey)
               .GroupBy(p => p.BusinessID).Select(z => new
               {
                   ListBill = z.ToList()
               });
            if (query.Count() < 0)
                return null;
            retstring.Add(SerDc.ServiceSideID, new List<string[]>());
            List<BillItem> fwfksbillitems = new List<BillItem>();
            Dictionary<int, List<BillItem>> bxksbillitems = new Dictionary<int, List<BillItem>>();
            Dictionary<int, List<BillItem>> fxbillitems = new Dictionary<int, List<BillItem>>();
            foreach (var a in LendingDc)
            {
                retstring.Add(a.LendingSideID, new List<string[]>());
                bxksbillitems.Add(a.LendingSideID, new List<BillItem>());
                fxbillitems.Add(a.LendingSideID, new List<BillItem>());
            }
            foreach (var list in query)
            {
                List<Bill> bills = list.ListBill;
                for (int i = 0; i < bills.Count; i++)
                {
                    if (bills[i].BillMonth.Trim() == GetDate())//判断是否是当期账单（若能，把存在逾期的本息扣失和罚息添加）
                    {
                        if (!IsExporeDQ(bills[i]))
                            continue;
                    }

                    if (GetIfResult(fwfksbillitems))//导出当期服务、担保费前，先把逾期服务费扣失添加上
                    {
                        TotalAmount += fwfksbillitems.Sum(p => p.Amount - p.ReceivedAmt);
                        retstring[SerDc.ServiceSideID].Add(GetBankExportTemplate2(SerDc)
                            .GetGSImportExcelItem(fwfksbillitems, num, customers, bankaccounts, enums, SerDc.ServiceSideID));
                        fwfksbillitems = new List<BillItem>();
                        num++;
                    }
                    var fd = GetNoFullPayFD(bills[i].BillItems);//服务费、担保费
                    if (fd.Count > 0)
                    {
                        TotalAmount += fd.Sum(p => p.Amount - p.ReceivedAmt);
                        retstring[SerDc.ServiceSideID].Add(GetBankExportTemplate2(SerDc)
                            .GetGSImportExcelItem(fd, num, customers, bankaccounts, enums, SerDc.ServiceSideID));
                        num++;
                    }
                    var fwfks = GetNoFullPayFWFKS(bills[i].BillItems);//服务费扣失
                    if (fwfks!= null)
                    {
                        fwfksbillitems.Add(fwfks);
                    }

                    for (int j = 0; j < LendingDc.Count; j++)
                    {
                        if (GetIfResult(bxksbillitems[LendingDc[j].LendingSideID]))//本息扣失
                        {
                            TotalAmount += bxksbillitems[LendingDc[j].LendingSideID]
                                .Sum(p => p.Amount - p.ReceivedAmt);
                            retstring[LendingDc[j].LendingSideID].Add(GetBankExportTemplate2(LendingDc[j])
                                .GetGSImportExcelItem(bxksbillitems[LendingDc[j].LendingSideID], num2, customers, bankaccounts, enums, LendingDc[j].LendingSideID));
                            bxksbillitems[LendingDc[j].LendingSideID] = new List<BillItem>(); num2++;
                        }
                        if (GetIfResult(fxbillitems[LendingDc[j].LendingSideID]))//罚息
                        {
                            TotalAmount += fxbillitems[LendingDc[j].LendingSideID]
                                .Sum(p => p.Amount - p.ReceivedAmt);
                            retstring[LendingDc[j].LendingSideID].Add(GetBankExportTemplate2(LendingDc[j])
                                .GetGSImportExcelItem(fxbillitems[LendingDc[j].LendingSideID], num2, customers, bankaccounts, enums, LendingDc[j].LendingSideID));
                            fxbillitems[LendingDc[j].LendingSideID] = new List<BillItem>(); num2++;
                        }
                        if (bills[i].Business.LendingSideID == LendingDc[j].LendingSideID)
                        {
                            var bx = GetNoFullPayBX(bills[i].BillItems);//本息
                            if (bx.Count > 0)
                            {
                                TotalAmount += bx.Sum(p => p.Amount - p.ReceivedAmt);num++;
                                retstring[LendingDc[j].LendingSideID].Add(GetBankExportTemplate2(LendingDc[j])
                                    .GetGSImportExcelItem(bx, num2, customers, bankaccounts, enums, LendingDc[j].LendingSideID));
                                num2++;
                            }
                            var bxks = GetNoFullPayBXKS(bills[i].BillItems);//本息扣失
                            if (bxks != null)
                            {
                                bxksbillitems[LendingDc[j].LendingSideID].Add(bxks);
                            }
                            var fx = GetNoFullPayFX(bills[i].BillItems);//罚息
                            if (fx != null)
                            {
                                fxbillitems[LendingDc[j].LendingSideID].Add(fx);
                            }
                        }
                    }
                }
                for (int k = 0; k < LendingDc.Count; k++)
                {
                    if (bxksbillitems[LendingDc[k].LendingSideID].Count() > 0)
                    {
                        TotalAmount += bxksbillitems[LendingDc[k].LendingSideID].Sum(p => p.Amount - p.ReceivedAmt);
                        retstring[LendingDc[k].LendingSideID].Add(GetBankExportTemplate2(LendingDc[k])
                            .GetGSImportExcelItem(bxksbillitems[LendingDc[k].LendingSideID], num2, customers, bankaccounts, enums, LendingDc[k].LendingSideID));
                        bxksbillitems[LendingDc[k].LendingSideID] = new List<BillItem>(); num2++;
                    }
                    if (fxbillitems[LendingDc[k].LendingSideID].Count() > 0)
                    {
                        TotalAmount += fxbillitems[LendingDc[k].LendingSideID].Sum(p => p.Amount - p.ReceivedAmt);
                        retstring[LendingDc[k].LendingSideID].Add(GetBankExportTemplate2(LendingDc[k])
                            .GetGSImportExcelItem(fxbillitems[LendingDc[k].LendingSideID], num2, customers, bankaccounts, enums, LendingDc[k].LendingSideID));
                        fxbillitems[LendingDc[k].LendingSideID] = new List<BillItem>(); num2++;
                    }
                }
                if (fwfksbillitems.Count() > 0)
                {
                    TotalAmount += fwfksbillitems.Sum(p => p.Amount - p.ReceivedAmt);
                    retstring[SerDc.ServiceSideID].Add(GetBankExportTemplate2(SerDc)
                        .GetGSImportExcelItem(fwfksbillitems, num, customers, bankaccounts, enums, SerDc.ServiceSideID));
                    fwfksbillitems = new List<BillItem>(); num++;
                }
            }
            return retstring;
        }
        #endregion

        #region 获取导出结果集(Excel+TXT)
        /// <summary>
        /// 获取需导出的账单款项列表
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<int, List<string[]>> GetBillListExcelAndTxT(out decimal TotalAmount
            ,out Dictionary<int,List<byte>> retbyte, 
            List<Customer> customers, List<BankAccount> bankaccounts, List<Enumeration> enums, string bankkey)
        {
            TotalAmount = 0; int num = 1; retbyte = new Dictionary<int, List<byte>>(); int[] ser = new int[1]{1};
            Dictionary<int, List<string[]>> retstring = new Dictionary<int, List<string[]>>();
            if (DeductCommandList.Count < 0)//如果没有此子公司的配置，则直接返回null
                return null;
            var query = _FinanceFactory.CreateBillDAL().GetBillList(EnumBillStatus.Debts)
               .Where(p => p.Business.ServicerSideID == DeductCommandList.FirstOrDefault().ServiceSideID
               && p.Business.BankKey == bankkey)
               .GroupBy(p => p.BusinessID).Select(z => new
               {
                   ListBill = z.ToList()
               });
            if (query.Count() < 0)
                return null;
            List<BillItem> fwfksbillitems = new List<BillItem>();
            Dictionary<int, List<BillItem>> bxksbillitems = new Dictionary<int, List<BillItem>>();
            Dictionary<int, List<BillItem>> fxbillitems = new Dictionary<int, List<BillItem>>();
            var ServDc2 = DeductCommandList.Where(p =>p.LendingSideID==p.ServiceSideID).ToList();
            var LendingDc2 = DeductCommandList.Where(p => p.LendingSideID != p.ServiceSideID).ToList();

            retstring.Add(GetCmd(ServDc2,true).ServiceSideID , new List<string[]>());
            retbyte.Add(GetCmd(ServDc2, false).LendingSideID, new List<byte>());

            foreach (var a in LendingDc2.Where(p=>p.AnalysisCmdType!=0))
            {
                retstring.Add(a.LendingSideID, new List<string[]>());//Excel
                retbyte.Add(a.LendingSideID, new List<byte>());//TXT

                bxksbillitems.Add(a.LendingSideID, new List<BillItem>());
                fxbillitems.Add(a.LendingSideID, new List<BillItem>());
            }
            foreach (var list in query)
            {
                List<Bill> bills = list.ListBill;
                for (int i = 0; i < bills.Count(); i++)
                {
                    if (bills[i].BillMonth.Trim() == GetDate())//判断是否是当期账单（若能，把存在逾期的本息扣失和罚息添加）
                    {
                        if (!IsExporeDQ(bills[i]))
                            continue;
                    }

                    if (GetIfResult(fwfksbillitems))//导出当期服务、担保费前，先把逾期服务费扣失添加上
                    {
                        TotalAmount += fwfksbillitems.Sum(p => p.Amount - p.ReceivedAmt);
                        retbyte[GetCmd(ServDc2, true).ServiceSideID].AddRange(GetBankExportTemplate(GetCmd(ServDc2, true))
                            .FomartTemplate(fwfksbillitems, customers));
                        fwfksbillitems = new List<BillItem>();
                    }
                    var fd = GetNoFullPayFD(bills[i].BillItems);//服务费、担保费
                    if (fd.Count > 0)
                    {
                        TotalAmount += fd.Sum(p => p.Amount - p.ReceivedAmt);

                        retbyte[GetCmd(ServDc2, true).ServiceSideID]
                            .AddRange(GetBankExportTemplate(GetCmd(ServDc2, true))//TXT
                            .FomartTemplate(fd, customers));

                        var ret = GetNoFullPayFD(bills[i].BillItems);
                        var ret2 = GetNoFullPayFWFKS(bills[i].BillItems);
                        if (ret.Count>0  && ret2!=null)
                            ret.Add(GetNoFullPayFWFKS(bills[i].BillItems));
                        else if (ret.Count == 0 && ret2 != null)
                            ret = new List<BillItem> {ret2 };

                        if (ret.Count > 0)
                        {
                            retstring[GetCmd(ServDc2, false).ServiceSideID]
                                .Add(GetBankExportTemplate2(GetCmd(ServDc2, false))//Excel
                                .GetGSImportExcelItem(ret, num, customers, bankaccounts,
                                enums, GetCmd(ServDc2, false).ServiceSideID));
                            num++;
                        }
                    }
                    var fwfks = GetNoFullPayFWFKS(bills[i].BillItems);//服务费扣失
                    if (fwfks != null)
                    {
                        fwfksbillitems.Add(fwfks);
                    }

                    for (int j = 0; j < GetCmdList(LendingDc2,true).Count(); j++)
                    {
                        if (GetIfResult(bxksbillitems[GetCmdList(LendingDc2, true)[j].LendingSideID]))//本息扣失
                        {
                            TotalAmount += bxksbillitems[GetCmdList(LendingDc2, true)[j].LendingSideID]
                                .Sum(p => p.Amount - p.ReceivedAmt);
                            retbyte[GetCmdList(LendingDc2, true)[j].LendingSideID]
                                .AddRange(GetBankExportTemplate(GetCmdList(LendingDc2, true)[j])
                            .FomartTemplate(bxksbillitems[GetCmdList(LendingDc2, true)[j].LendingSideID], customers));

                            bxksbillitems[GetCmdList(LendingDc2, true)[j].LendingSideID] = new List<BillItem>();
                        }
                        if (GetIfResult(fxbillitems[GetCmdList(LendingDc2, true)[j].LendingSideID]))//罚息
                        {
                            TotalAmount += fxbillitems[GetCmdList(LendingDc2, true)[j].LendingSideID]
                                .Sum(p => p.Amount - p.ReceivedAmt);

                            retbyte[GetCmdList(LendingDc2, true)[j].LendingSideID]
                                .AddRange(GetBankExportTemplate(GetCmdList(LendingDc2, true)[j])
                                  .FomartTemplate(fxbillitems[GetCmdList(LendingDc2, true)[j].LendingSideID], customers));
                            fxbillitems[GetCmdList(LendingDc2, true)[j].LendingSideID] = new List<BillItem>();
                        }
                        if (bills[i].Business.LendingSideID == GetCmdList(LendingDc2, true)[j].LendingSideID)
                        {
                            var bx =  GetNoFullPayBX(bills[i].BillItems);//本息
                            if (bx.Count > 0)
                            {
                                TotalAmount += bx.Sum(p => p.Amount - p.ReceivedAmt);

                                retbyte[GetCmdList(LendingDc2, true)[j].LendingSideID]
                                    .AddRange(GetBankExportTemplate(GetCmdList(LendingDc2, true)[j])
                                .FomartTemplate(bx, customers));

                                var ret2 = GetNoFullPayBXKS(bills[i].BillItems);
                                var ret3= GetNoFullPayFX(bills[i].BillItems);
                                if (bx.Count > 0)
                                {
                                    if (ret2 != null)
                                        bx.Add(ret2);
                                    if (ret3 != null)
                                        bx.Add(ret3);
                                }
                                else if (bx.Count == 0 && (ret2 != null || ret3 != null))
                                    bx = new List<BillItem> { ret2, ret3 };

                                if (bx.Count > 0)
                                {
                                    retstring[GetCmdList(LendingDc2, false)[j].LendingSideID]
                                        .Add(GetBankExportTemplate2(GetCmdList(LendingDc2, false)[j])
                                        .GetGSImportExcelItem(bx, ser[0], customers, bankaccounts, enums,
                                        GetCmdList(LendingDc2, false)[j].LendingSideID)); ser[0]++;
                                }
                            }
                            var bxks = GetNoFullPayBXKS(bills[i].BillItems);//本息扣失
                            if (bxks!= null)
                            {
                                bxksbillitems[GetCmdList(LendingDc2, true)[j].LendingSideID].Add(bxks);
                            }
                            var fx = GetNoFullPayFX(bills[i].BillItems);//罚息
                            if (fx != null)
                            {
                                fxbillitems[GetCmdList(LendingDc2, true)[j].LendingSideID].Add(fx);
                            }
                        }
                    }
                }
                for (int k = 0; k < GetCmdList(LendingDc2, true).Count(); k++)
                {
                    if (bxksbillitems[GetCmdList(LendingDc2, true)[k].LendingSideID].Count() > 0)
                    {
                        TotalAmount += bxksbillitems[GetCmdList(LendingDc2, true)[k].LendingSideID]
                            .Sum(p => p.Amount - p.ReceivedAmt);
                        retbyte[GetCmdList(LendingDc2, true)[k].LendingSideID]
                            .AddRange(GetBankExportTemplate(GetCmdList(LendingDc2, true)[k])
                                .FomartTemplate(bxksbillitems[GetCmdList(LendingDc2, true)[k].LendingSideID], customers));
                        bxksbillitems[GetCmdList(LendingDc2, true)[k].LendingSideID] = new List<BillItem>();
                    }
                    if (fxbillitems[GetCmdList(LendingDc2, true)[k].LendingSideID].Count() > 0)
                    {
                        TotalAmount += fxbillitems[GetCmdList(LendingDc2, true)[k].LendingSideID]
                            .Sum(p => p.Amount - p.ReceivedAmt);
                        retbyte[GetCmdList(LendingDc2, true)[k].LendingSideID]
                            .AddRange(GetBankExportTemplate(GetCmdList(LendingDc2, true)[k])
                                .FomartTemplate(fxbillitems[GetCmdList(LendingDc2, true)[k].LendingSideID], customers));
                        fxbillitems[GetCmdList(LendingDc2, true)[k].LendingSideID] = new List<BillItem>();
                    }
                }
                if (fwfksbillitems.Count() > 0)
                {
                    TotalAmount += fwfksbillitems.Sum(p => p.Amount - p.ReceivedAmt);
                    retbyte[GetCmd(ServDc2, true).ServiceSideID].AddRange(GetBankExportTemplate(GetCmd(ServDc2, true))
                        .FomartTemplate(fwfksbillitems, customers));
                    fwfksbillitems = new List<BillItem>();
                }
            }
            return retstring;
        }
        #endregion

        #region 模版配置

        /// <summary>
        /// 通过模版配置，获取模版的格式处理方式
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
        #region - 导出配置工厂 -
        public ExportTemplate2 GetBankExportTemplate2(DeductCommand deductcommand)
        {
            switch (deductcommand.ExportCmdType.ValueToEnum<EnumExportCmdKind>())
            {
                case EnumExportCmdKind.TemplateFive:
                    return new BankExportFive();
                case EnumExportCmdKind.TemplateSix:
                    return new BankExportSix();
                case EnumExportCmdKind.TemplateEight:
                    return new BankExportEight();
                default:
                    return null;
            }
        }

        #endregion

        #endregion

        #region 计算款项
        /// <summary>
        /// 获取未足额支付本息账单款项
        /// </summary>
        /// <returns></returns>
        protected List<BillItem> GetNoFullPayBX(List<BillItem> list)
        {
            return list.Where(p => p.FullPaidTime == null &&
                    (p.Subject == (byte)EnumCostSubject.Capital
                    || p.Subject == (byte)EnumCostSubject.Interest)).ToList();
        }
        /// <summary>
        /// 获取未足额支付服务费、担保费款项
        /// </summary>
        /// <returns></returns>
        protected virtual List<BillItem> GetNoFullPayFD(List<BillItem> list)
        {
            return list.Where(p => p.FullPaidTime == null &&
                    (p.Subject == (byte)EnumCostSubject.GuaranteeFee
                    || p.Subject == (byte)EnumCostSubject.ServiceFee)).ToList();
        }
        /// <summary>
        /// 获取未足额支付本息扣失账单款项
        /// </summary>
        /// <returns></returns>
        protected BillItem GetNoFullPayBXKS(List<BillItem> list)
        {
            return list.FirstOrDefault(p => p.FullPaidTime == null &&
                    p.Subject == (byte)EnumCostSubject.InterestBuckleFail);

        } 

        /// <summary>
        /// 获取未足额支付罚息账单款项
        /// </summary>
        /// <returns></returns>
        protected BillItem GetNoFullPayFX(List<BillItem> list)
        {
            return list.FirstOrDefault(p => p.FullPaidTime == null &&
                    p.Subject == (byte)EnumCostSubject.PunitiveInterest);
        }

        /// <summary>
        /// 获取未足额支付服务费扣失账单款项
        /// </summary>
        /// <returns></returns>
        public virtual BillItem GetNoFullPayFWFKS(List<BillItem> list)
        {
            return list.FirstOrDefault(p => p.FullPaidTime == null &&
                    p.Subject == (byte)EnumCostSubject.ServiceBuckleFail);
        }
        /// <summary>
        /// 判断当前账单是否需要导出当前账单款项
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public bool IsExporeDQ(Bill bill)
        {
            List<DeductSequence> dedes = _FinanceFactory.CreateDeductSequenceDAL()
                     .GetDeductSequenceList((byte)bill.Business.DSeqType);
            var dede = dedes.FirstOrDefault(p => p.DeductTime < DateTime.Now);
            if (dede.DeductTime.Month != DateTime.Now.Month && dede.BillRegion == (byte)EnumBillRegion.CurrentAndOverdue)
                return false;
            return true;         
        }

        protected List<BillItem> GetTotalBxks(List<Bill> bills, bool IsCotainsDQ)
        {
            
            var list = bills.Select(p => new { Memb = p.BillItems.Where(o=>o.FullPaidTime==null).ToList() });
            //list.m
            //Where(p => p.BillItems.Where(p => p.FullPaidTime == null)); ;
            return null;
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 判断是否是当期账单
        /// </summary>
        /// <returns></returns>
        public string GetDate()
        {
            int month = DateTime.Now.Month;
            string months=string.Empty;
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
            if (billitem.Subject == (byte)EnumCostSubject.Capital
                || billitem.Subject == (byte)EnumCostSubject.Interest)
            {
                return "本息";
            }
            else if (billitem.Subject == (byte)EnumCostSubject.GuaranteeFee
                || billitem.Subject == (byte)EnumCostSubject.ServiceFee)
            {
                return "服务担保费";
            }
            else
            {
                return billitem.Subject.ValueToDesc<EnumCostSubject>();
            }
        }

        /// <summary>
        /// 判断IF条件
        /// </summary>
        /// <param name="billitem"></param>
        /// <param name="businessid"></param>
        /// <returns></returns>
        protected bool GetIfResult(List<BillItem> billitem)
        {
            if (billitem.Count < 1)
                return false;
            var bill = billitem.FirstOrDefault().Bill;
            return (bill.BillMonth.Trim() == GetDate()
                    && billitem.Count() > 0);
        }
        public DeductCommand GetCmd(List<DeductCommand> cmdlist,bool isTXT)
        {
            return cmdlist.FirstOrDefault(p => p.AnalysisCmdType == 0 != isTXT);
        }
        public List<DeductCommand> GetCmdList(List<DeductCommand> cmdlist, bool isTXT)
        {
            return cmdlist.Where(p => p.AnalysisCmdType == 0 != isTXT).ToList();
        }
        #endregion

        #endregion
    }
}

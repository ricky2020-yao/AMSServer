using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.Common.Tools;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.BLL.PayBank
{
    /// <summary>
    /// Author:陈伟
    /// CreateTime:2012年6月8日
    /// Description:支付工厂（提供对外的API）
    /// </summary>
    public class BankFactory : BaseDataSource
    {
        #region- 字段属性 -
        private Dictionary<int, DeductCommand> CmdList;
        #endregion

        #region- 构造函数 -
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="financeContext"></param>
        public BankFactory(Dictionary<int, DeductCommand> cmdlist)
            : base(cmdlist)
        {
            CmdList = cmdlist;
        }
        #endregion

        #region- 功能函数 -

        #region- 导出扣款指令 -

        #region- 导出方法(TxT) -（沪苏蓉外贸指令）
        /// <summary>
        /// 导出服务方、信托方TxT
        /// </summary>
        /// <returns></returns>
        public byte[] ExportDWJM(List<PayBankExportItem> datalend, List<PayBankExportItem> dataserv, int operatorid,
            List<Customer> customers, List<BankAccount> banks, List<Enumeration> companys,
            Dictionary<int, string> titles, int servid, int lendid, string lockkey)
        {
            string dt = DateTime.Now.ToString("yyyyMMdd");

            decimal ServAmount, LendAmount;
            ServAmount = LendAmount = 0;
            MemoryStream ms = new MemoryStream();
            List<string[]> retstringlend = new List<string[]>();
            List<string[]> retstringserv = new List<string[]>();
            List<string[]> retstringguarantee = new List<string[]>();
            string name = string.Empty;
            using (ZipFile f = ZipFile.Create(ms))
            {
                f.BeginUpdate();

                #region 信托方
                if (datalend.Count > 0)
                {
                    var lendbyte = GetBillListDWJM(out retstringlend, out LendAmount, banks, datalend, companys, servid, lendid);
                    name = lendbyte.Count > 1 ? "_" : "";
                    foreach (var item in lendbyte)
                    {
                        lendbyte[item.Key].RemoveRange(lendbyte[item.Key].Count - 2, 2);
                        f.Add(new StringDataSource(lendbyte[item.Key].ToArray()), dt + "_A2" + (
                            !string.IsNullOrEmpty(name) ? (name + item.Key) : "") + "_" + lockkey + ".txt");
                    }
                    f.Add(new StringDataSource(GetBankExportTemplate2(CmdList[lendid])
                          .RetByte(retstringlend, operatorid, "sheet1")), dt + "_A2_" + lockkey + ".xls");

                    ExportToSql(lendid, EnumIncomeType.BankIncomeLend, operatorid,
                        LendAmount, lockkey, lendbyte.Count, dt + "_A2_" + lockkey + (lendbyte.Count > 1 ? ".zip" : ".txt"));
                }
                #endregion

                # region All
                var dataservguarantee =
                    dataserv.Where(p => p.BusinessStatus == (byte)EnumBusinessStatus.Guarantee).ToList();

                dataserv = dataserv.Where(p => p.BusinessStatus != (byte)EnumBusinessStatus.Guarantee).ToList();
                #endregion

                #region 服务方
                if (dataserv.Count > 0)
                {
                    var servbyte = GetBillListServiceDWJM(out retstringserv, banks, dataserv, companys, servid, lendid);
                    name = servbyte.Count > 1 ? "_" : "";
                    foreach (var item in servbyte)
                    {
                        servbyte[item.Key].RemoveRange(servbyte[item.Key].Count - 2, 2);
                        f.Add(new StringDataSource(servbyte[item.Key].ToArray()), dt + "_A1" +
                            (!string.IsNullOrEmpty(name) ? (name + item.Key) : "") + "_" + lockkey + ".txt");
                    }
                    f.Add(new StringDataSource(GetBankExportTemplate2(CmdList[servid])
                            .RetByte(retstringserv, operatorid, "sheet1")), dt + "_A1_" + lockkey + ".xls");
                    ExportToSql(servid, EnumIncomeType.BankIncomeServ, operatorid,
                        dataserv.Sum(p => p.DunAmount), lockkey, servbyte.Count, dt + "_A1_" + lockkey + (servbyte.Count > 1 ? ".zip" : ".txt"));
                }
                #endregion

                #region 担保方
                if (dataservguarantee.Count > 0)
                {
                    var guarantbyte = GetBillListGuarantee(out retstringguarantee, customers, banks, dataservguarantee, companys, servid, lendid);
                    name = guarantbyte.Count > 1 ? "_" : "";
                    foreach (var item in guarantbyte)
                    {
                        guarantbyte[item.Key].RemoveRange(guarantbyte[item.Key].Count - 2, 2);
                        f.Add(new StringDataSource(guarantbyte[item.Key].ToArray()), dt + "_B" +
                            (!string.IsNullOrEmpty(name) ? (name + item.Key) : "") + "_" + lockkey + ".txt");
                    }
                    f.Add(new StringDataSource(GetBankExportTemplate2(CmdList[6])
                            .RetByte(retstringguarantee, operatorid, "sheet1")), dt + "_B_" + lockkey + ".xls");
                    ExportToSql(6, EnumIncomeType.BankIncomeGuarant, operatorid,
                        dataservguarantee.Sum(p => p.DunAmount), lockkey, guarantbyte.Count, dt + "_B_" + lockkey + (guarantbyte.Count > 1 ? ".zip" : ".txt"));
                }
                #endregion

                f.CommitUpdate();
                if (f.Count == 0)
                    return null;
            }
            byte[] byteBuffer = ms.ToArray();
            ms.Flush();
            ms.Close();
            return byteBuffer;
        }
        #endregion

        #region- 导出方法(TxT) -（渤海指令）
        /// <summary>
        /// 导出服务方、信托方TxT
        /// </summary>
        /// <returns></returns>
        public byte[] ExportBHXT(List<PayBankExportItem> datalend, List<PayBankExportItem> dataserv, int operatorid,
             List<BankAccount> banks, List<Enumeration> companys,
            Dictionary<int, string> titles, int servid, int lendid, string lockkey)
        {
            decimal ServAmount, LendAmount;
            MemoryStream ms = new MemoryStream();
            Dictionary<int, List<string[]>> retstringlend = new Dictionary<int, List<string[]>>();
            Dictionary<int, List<string[]>> retstringserv = new Dictionary<int, List<string[]>>();
            using (ZipFile f = ZipFile.Create(ms))
            {
                f.BeginUpdate();

                Dictionary<int, List<byte>> lendbyte =
                GetBillListBHXT(out retstringlend, out LendAmount, banks, datalend, companys, servid, lendid);

                Dictionary<int, List<byte>> servbyte =
                GetBillListService(out retstringserv, out ServAmount, banks, dataserv, companys, servid, lendid);

                foreach (var item in CmdList)
                {
                    if (lendbyte[item.Key].Count > 0 && retstringlend[item.Key].Count > 0)
                    {
                        lendbyte[item.Key].RemoveRange(lendbyte[item.Key].Count - 2, 2);

                        f.Add(new StringDataSource(lendbyte[item.Key].ToArray()), titles[item.Key] + ".txt");

                        f.Add(new StringDataSource(GetBankExportTemplate2(item.Value)
                            .RetByte(retstringlend[item.Key], operatorid, titles[item.Key]))
                                    , titles[item.Key] + ".xls");
                        ExportToSql(lendid, EnumIncomeType.BankIncomeLend, operatorid, LendAmount, lockkey, 1);
                    }
                    if (servbyte[item.Key].Count > 0 && retstringserv[item.Key].Count > 0)
                    {
                        servbyte[item.Key].RemoveRange(servbyte[item.Key].Count - 2, 2);

                        f.Add(new StringDataSource(servbyte[item.Key].ToArray()), titles[item.Key] + ".txt");

                        f.Add(new StringDataSource(GetBankExportTemplate2(item.Value)
                             .RetByte(retstringserv[item.Key], operatorid, titles[item.Key]))
                             , titles[item.Key] + ".xls");
                        ExportToSql(servid, EnumIncomeType.BankIncomeServ, operatorid, ServAmount, lockkey, 1);
                    }
                }
                f.CommitUpdate();

                if (f.Count == 0)
                    return null;
            }
            byte[] byteBuffer = ms.ToArray();
            ms.Flush();
            ms.Close();
            return byteBuffer;
        }
        #endregion

        #region- 导出方法(Excel) -（杭州工行指令）
        /// <summary>
        /// 导出服务方、信托方TxT
        /// </summary>
        /// <returns></returns>
        public byte[] ExportExcelBHXT(List<PayBankExportItem> datalend, List<PayBankExportItem> dataserv, int operatorid,
           List<BankAccount> banks, List<Enumeration> companys, Dictionary<int, string> titles, int servid, int lendid, string lockkey)
        {
            decimal ServAmount, LendAmount;
            MemoryStream ms = new MemoryStream();
            Dictionary<int, List<string[]>> retstringlend = new Dictionary<int, List<string[]>>();
            Dictionary<int, List<string[]>> retstringserv = new Dictionary<int, List<string[]>>();
            using (ZipFile f = ZipFile.Create(ms))
            {
                f.BeginUpdate();

                Dictionary<int, List<string[]>> lendbyte =
                GetBillListExcelBHXT(out retstringlend, out LendAmount, datalend, banks, companys, servid, lendid);

                Dictionary<int, List<string[]>> servbyte =
                GetBillListExcelService(out retstringserv, out ServAmount, dataserv, banks, companys, servid, lendid);

                foreach (var item in CmdList)
                {
                    if (lendbyte[item.Key].Count > 0 && retstringlend[item.Key].Count > 0)
                    {
                        f.Add(new StringDataSource(GetBankExportTemplate2(item.Value, true)
                            .RetByte(lendbyte[item.Key], operatorid, titles[item.Key]))
                                    , titles[item.Key] + "_指令.xls");

                        f.Add(new StringDataSource(GetBankExportTemplate2(item.Value)
                            .RetByte(retstringlend[item.Key], operatorid, titles[item.Key]))
                                    , titles[item.Key] + "_明细.xls");

                        ExportToSql(lendid, EnumIncomeType.BankIncomeLend, operatorid, LendAmount, lockkey, 1);
                    }
                    if (servbyte[item.Key].Count > 0 && retstringserv[item.Key].Count > 0)
                    {
                        f.Add(new StringDataSource(GetBankExportTemplate2(item.Value, true)
                            .RetByte(servbyte[item.Key], operatorid, titles[item.Key]))
                                    , titles[item.Key] + "_指令.xls");

                        f.Add(new StringDataSource(GetBankExportTemplate2(item.Value)
                             .RetByte(retstringserv[item.Key], operatorid, titles[item.Key]))
                             , titles[item.Key] + "_明细.xls");
                        ExportToSql(servid, EnumIncomeType.BankIncomeServ, operatorid, ServAmount, lockkey, 1);
                    }
                }
                f.CommitUpdate();

                if (f.Count == 0)
                    return null;
            }
            byte[] byteBuffer = ms.ToArray();
            ms.Flush();
            ms.Close();
            return byteBuffer;
        }
        #endregion

        #region- 导出方法(Excel) -（杭州工行外贸指令）
        /// <summary>
        /// 导出服务方、信托方TxT
        /// </summary>
        /// <returns></returns>
        public byte[] ExportExcelDWJM(List<PayBankExportItem> datasl, List<PayBankExportItem> dataguarantee, int operatorid,
            List<Customer> customers, List<BankAccount> banks, List<Enumeration> companys,
            Dictionary<int, string> titles, int servid, int lendid, string lockkey)
        {
            string dt = DateTime.Now.ToString("yyyyMMdd");
            MemoryStream ms = new MemoryStream();
            List<string[]> retstring = new List<string[]>();
            string name = string.Empty;
            using (ZipFile f = ZipFile.Create(ms))
            {
                f.BeginUpdate();

                #region 担保方
                if (dataguarantee.Count > 0)
                {
                    var guarantee = GetBillListExcelDWJM(out retstring, dataguarantee, customers, banks, companys, servid, lendid);
                    name = guarantee.Count > 1 ? "_" : "";
                    foreach (var item in guarantee)
                    {
                        f.Add(new StringDataSource(GetBankExportTemplate2(CmdList[6], true)
                            .RetByte(guarantee[item.Key], operatorid, "sheet1"))
                                    , dt + "_B" + (!string.IsNullOrEmpty(name) ? (name + item.Key) : "") + "_" + lockkey + ".xls");
                    }
                    f.Add(new StringDataSource(GetBankExportTemplate2(CmdList[6])
                             .RetByte(retstring, operatorid, "sheet1"))
                             , dt + "_B_" + lockkey + "_明细.xls");
                    ExportToSql(6, EnumIncomeType.BankIncomeGuarant, operatorid, dataguarantee.Sum(p => p.DunAmount), lockkey, guarantee.Count,
                        dt + "_B_" + lockkey + (guarantee.Count > 1 ? ".zip" : ".txt"));
                }
                #endregion

                #region 信托方和服务方
                if (datasl.Count > 0)
                {
                    var dataall = GetBillListExcelDWJM(out retstring, datasl, customers, banks, companys, servid, lendid);
                    name = dataall.Count > 1 ? "_" : "";
                    foreach (var item in dataall)
                    {
                        f.Add(new StringDataSource(GetBankExportTemplate2(CmdList[lendid], true)
                            .RetByte(dataall[item.Key], operatorid, "sheet1"))
                                    , dt + "_A" + (!string.IsNullOrEmpty(name) ? (name + item.Key) : "") + "_" + lockkey + ".xls");
                    }
                    f.Add(new StringDataSource(GetBankExportTemplate2(CmdList[lendid])
                             .RetByte(retstring, operatorid, "sheet1"))
                             , dt + "_A_" + lockkey + "_明细.xls");
                    ExportToSql(lendid, EnumIncomeType.BankIncomeLend, operatorid, datasl.Sum(p => p.DunAmount), lockkey, dataall.Count,
                        dt + "_A_" + lockkey + (dataall.Count > 1 ? ".zip" : ".txt"));
                }
                #endregion

                f.CommitUpdate();
                if (f.Count == 0)
                    return null;
            }
            byte[] byteBuffer = ms.ToArray();
            ms.Flush();
            ms.Close();
            return byteBuffer;
        }
        #endregion

        #region- 导出方法(TxT) -（成都指令）
        /// <summary>
        /// 导出服务方、信托方TxT
        /// </summary>
        /// <returns></returns>
        public byte[] ExportChengdu(List<PayBankExportItem> datalend, int operatorid,
            List<Customer> customers, List<BankAccount> banks, List<Enumeration> companys,
            Dictionary<int, string> titles, int servid, int lendid, string lockkey)
        {
            decimal LendAmount;
            MemoryStream ms = new MemoryStream();
            Dictionary<int, List<string[]>> retstringlend = new Dictionary<int, List<string[]>>();
            Dictionary<int, List<string[]>> retstringserv = new Dictionary<int, List<string[]>>();
            using (ZipFile f = ZipFile.Create(ms))
            {
                f.BeginUpdate();

                Dictionary<int, List<byte>> lendbyte =
                GetBillListChengdu(out retstringlend, out LendAmount, customers, banks, datalend, companys, servid, lendid);

                foreach (var item in CmdList)
                {
                    if (lendbyte[item.Key].Count > 0 && retstringlend.Count > 0)
                    {
                        lendbyte[item.Key].RemoveRange(lendbyte[item.Key].Count - 2, 2);

                        f.Add(new StringDataSource(lendbyte[item.Key].ToArray()), titles[item.Key] + ".txt");

                        f.Add(new StringDataSource(GetBankExportTemplate2(item.Value)
                            .RetByte(retstringlend[item.Key], operatorid, titles[item.Key]))
                                    , titles[item.Key] + ".xls");
                    }
                }
                f.CommitUpdate();

                if (f.Count == 0)
                    return null;
                if (!ExportToSql(servid, EnumIncomeType.BankIncomeLend, operatorid, LendAmount, lockkey, 1))
                    return null;
            }
            byte[] byteBuffer = ms.ToArray();
            ms.Flush();
            ms.Close();
            return byteBuffer;
        }
        #endregion

        #endregion

        #region- 导入扣款指令 -
        public bool BankImport(out string mismsg, Stream stream, PayTrace trance, DeductCommand deductcommand,
            List<BankAccount> bankaccounts, int operateid, string receviedtime, out string message, string luckkey, bool IsZip = false)
        {
            message = mismsg = string.Empty;
            List<BaseImportItem> list = new List<BaseImportItem>();
            string companykey = bankaccounts.FirstOrDefault
                    (p => p.BankAccountID == deductcommand.ServiceSideID).CompanyKey;
            if (!IsZip)
                list = GetBankImportTemplate(deductcommand).MatchBillItems(stream, out message);
            else
            {
                list.AddRange(GetList(stream, deductcommand, out message));
            }

            if (message == string.Empty && list.Count == 0)
            {
                #region- 扣款指令导入日志 -
                PayTrace log = new PayTrace
                {
                    AccountID = deductcommand.ServiceSideID,
                    IncomeType = trance.IncomeType,
                    OperatorID = operateid,
                    TraceTime = DateTime.Now,
                    PayKind = (byte)EnumPayKind.Payment_Bank,
                    CallDirection = (byte)EnumCallDirectionKind.Import_Pay,
                    PayTraceAmount = 0,
                    RequestState = (byte)EnumRequestState.Success,
                    ResponseTime = DateTime.Now,
                    Content = "导入银扣指令",
                    LockKey = luckkey,
                    FileName = trance.FileName
                };

                PayTraceDal dal = new PayTraceDal();
                dal.InsertPayTrace(log);

                //_PaymentFactory.CreatePayTraceDAL().Insert(log);
                //_PaymentFactory.CreatePayTraceDAL().Save();

                #endregion
                return true;
            }
            List<int> lengdings = new List<int>() { 11, 13, 14 };
            if (string.IsNullOrEmpty(message) && list.Count > 0)
            {
                if (trance.IncomeType == (byte)EnumIncomeType.BankIncomeServ)
                {
                    //服务方
                    return BankImportForService(list, operateid, deductcommand.ServiceSideID, deductcommand.LendingSideID,
                        companykey, receviedtime, luckkey, out message, out mismsg, trance.FileName);
                }
                else if (trance.IncomeType == (byte)EnumIncomeType.BankIncomeGuarant)
                {
                    //担保方
                    return BankImportForGuarantee(list, operateid, deductcommand.ServiceSideID, deductcommand.LendingSideID,
                        companykey, receviedtime, luckkey, out message, out mismsg, trance.FileName);
                }
                else if (trance.IncomeType == (byte)EnumIncomeType.BankIncomeLend &&
                    lengdings.Contains(trance.AccountID))
                {
                    //外贸信托方
                    return BankImportForDWJM(list, operateid, deductcommand.ServiceSideID,
                                    deductcommand.LendingSideID, companykey, receviedtime, luckkey,
                                    out message, out mismsg, trance.FileName, trance.AccountID == 14 ? true : false);
                }
                else
                {
                    //渤海信托方
                    return BankImportForBHXT(list, operateid, deductcommand.ServiceSideID,
                                      deductcommand.LendingSideID, companykey, receviedtime, luckkey, out message, out mismsg, trance.FileName);
                }
            }
            return false;
        }
        #endregion

        #region- 导入Zip解压缩辅助方法 -
        private List<BaseImportItem> GetList(Stream stream, DeductCommand cmd, out string message)
        {
            message = string.Empty;
            List<BaseImportItem> list = new List<BaseImportItem>();
            ZipInputStream zipInStream = new ZipInputStream(stream);
            ZipEntry entry = zipInStream.GetNextEntry();
            while (entry != null)
            {
                var a = entry.Name;
                int size = 2048;
                byte[] data = new byte[2048];
                MemoryStream s = new MemoryStream();
                while (true)
                {
                    size = zipInStream.Read(data, 0, data.Length);
                    if (size > 0)
                    {
                        s.Write(data, 0, size);
                    }
                    else
                    {
                        break;
                    }
                }
                s.Position = 0;
                list.AddRange(GetBankImportTemplate(cmd).MatchBillItems(s, out message));
                if (!string.IsNullOrEmpty(message))
                {
                    list.Clear();
                    break;
                }
                entry = zipInStream.GetNextEntry();
            }
            return list;
        }
        #endregion

        #endregion
    }
}

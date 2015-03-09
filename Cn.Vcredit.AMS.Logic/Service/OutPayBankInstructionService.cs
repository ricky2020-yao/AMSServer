using Cn.Vcredit.Common.DB;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Tools;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.AMS.Common.Data;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Common.Entity;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Common.Command;
using Cn.Vcredit.AMS.Common.Service.Data;
using Cn.Vcredit.AMS.Common.Service.Interface;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.DataAccess.Caches;
using Cn.Vcredit.AMS.Logic.BLL.PayBank;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.AMS.Common.Entity.ReponseResult;

namespace Cn.Vcredit.AMS.Logic.Service
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:导出扣款指令
    /// </summary>
    public class OutPayBankInstructionService:IService
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        private ILogger m_Logger = LogFactory.CreateLogger(typeof(OutPayBankInstructionService));

        /// <summary>
        /// 程序执行主入口
        /// </summary>
        /// <param name="serviceData"></param>
        public void Execute(object serviceData)
        {
            ResponseEntity responseEntity = new ResponseEntity();
            ServiceData data = serviceData as ServiceData;
            if (data == null)
                return;

            ServiceCommand command = data.Command;
            if (command == null)
                return;

            RequestEntity requestEntity = command.Entity;
            if (requestEntity == null)
            {
                responseEntity.ResponseStatus = (int)EnumResponseState.RequestCommandError;
                return;
            }

            //请求ID
            responseEntity.RequestId = requestEntity.RequestId;
            // 用户名
            responseEntity.UserId = requestEntity.UserId;

            string errorMessage = "";
            // 记录执行时间类
            using (StopWatcherAuto auto = new StopWatcherAuto())
            {
                int ServiceID = ContractSide.SHWS;
                int LendingBHID = ContractSide.SHBH;

                string lockkey = string.Empty;
                try
                {
                    if (HaveFrozenBusiness(LendingBHID, ServiceID, ref errorMessage))
                        return;

                    AnnulCLoanByBusinessesByBoHai(Const.COMPANY_WX_SHWS_SERVICE, Const.COMPANY_BHXT_LENDING);

                    string sqllend = SqlFile.PayBankSqlBH.ToFileContent
                                    (true, ServiceID, "AND BUS.LendingSideID =" + LendingBHID,
                                     ServiceID, "AND BUS.LendingSideID =" + LendingBHID);
                    string sqlserv = SqlFile.PayBankSqlServer.ToFileContent
                                    (true, ServiceID, "AND BUS.LendingSideID =" + LendingBHID);

                    PayBankDal dal = new PayBankDal();
                    var datalend = dal.GetPayBankExportItem(sqllend);
                    var dataserv = dal.GetPayBankExportItem(sqlserv);

                    if (datalend.Count == 0 && dataserv.Count == 0)
                    {
                        errorMessage = "无扣款指令导出。";
                        m_Logger.Info("无扣款指令导出!");
                        return;
                    }

                    // 为导出订单加锁
                    lockkey = SetBusinessLock(ServiceID, LendingBHID);

                    // 根据子公司编号获取导出扣款文件名
                    var titles = GetTitles(ServiceID, lockkey);

                    // 根据子公司编号获取导出扣款指令配置
                    var dic = GetDeductCommand(ServiceID);

                    var lstBankAccount = Singleton<BankAccountsCache>.Instance.BankAccounts;
                    BankFactory fac = new BankFactory(dic);

                    var cmpanys = Singleton<CompanyCache>.Instance.CompanyEnumeration;
                    byte[] ret = fac.ExportBHXT(datalend, dataserv, requestEntity.UserId,
                            lstBankAccount, cmpanys, titles, ServiceID, LendingBHID, lockkey);

                    if (ret == null)
                    {
                        //为导出订单解锁
                        SetBusinessUnLock(lockkey);
                        errorMessage = "无扣款指令导出。";
                        m_Logger.Info("无扣款指令导出。");
                    }
                    else
                    {
                        var result = new ResponseFileResult();
                        result.Result = ret;

                        responseEntity.ResponseStatus = (int)EnumResponseState.Success;
                        responseEntity.Results = result;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = "";
                    responseEntity.ResponseStatus = (int)EnumResponseState.Others;
                    responseEntity.ResponseMessage = "导出扣款指令失败。" + ex.Message.ToString();
                    m_Logger.Error("导出扣款指令失败:" + ex.Message.ToString());
                    m_Logger.Error("导出扣款指令失败:" + ex.StackTrace.ToString());
                    // 为导出订单解锁
                    SetBusinessUnLock(lockkey);
                }
                finally
                {
                    if (errorMessage.Length > 0)
                    {
                        responseEntity.ResponseStatus = (int)EnumResponseState.Others;
                        responseEntity.ResponseMessage = errorMessage;
                    }

                    Singleton<ResultCacheManager>.Instance.AddResult(command.Guid, responseEntity);
                }
            }
        }

        /// <summary>
        /// 是否有冻结订单
        /// </summary>
        /// <param name="lendingSideID">lendingSideID</param>
        /// <param name="serviceSideID">serviceSideID</param>
        /// <param name="errorMessage">serviceSideID</param>
        /// <returns>有冻结订单</returns>
        private bool HaveFrozenBusiness(int lendingSideID, int serviceSideID, ref string errorMessage)
        {
            int count = GetBHFrozenCount(lendingSideID, serviceSideID);
            if (count < 0)
            {
                errorMessage = "查询冻结状态出错!";
                m_Logger.Info("查询冻结状态出错!");
                return true;
            }
            if (count == 0)
            {
                return false;
            }
            else
            {
                errorMessage = string.Format("有{0}条记录在冻结中，请先处理再导出。", count);
                m_Logger.Info(string.Format("有{0}条记录在冻结中，请先处理再导出。", count));
                return true;
            }
        }

        /// <summary>
        /// Author:shwang
        /// Date:20140704
        /// Desc:得到渤海冻结数量
        /// </summary>
        /// <param name="lendingSideID">放贷方ID</param>
        /// <param name="serviceSideID">服务方ID</param>
        /// <returns>冻结数量</returns>
        private int GetBHFrozenCount(int lendingSideID, int serviceSideID)
        {
            SqlParameter lendingSideIDPara = new SqlParameter("@LendingSideID", lendingSideID);
            SqlParameter serviceSideIDPara = new SqlParameter("@ServiceSideID", serviceSideID);

            SqlHelperCommon sqlHelper = new SqlHelperCommon("PostLoanDB");

            object countObj = sqlHelper.ExecuteScalar(CommandType.StoredProcedure
                , "proc_FinanceManage_GetBHFrozenCount",
                new SqlParameter[] { lendingSideIDPara, serviceSideIDPara });

            if (countObj == null || countObj == DBNull.Value)
                return -1;

            return Convert.ToInt32(countObj);
        }

        /// <summary>
        /// Author:王正吉
        /// Description:注销所有正在提前清贷中的订单
        /// </summary>
        private string AnnulCLoanByBusinessesByBoHai(string companyKey, string lendingSide)
        {
            SqlHelperCommon sqlHelper = new SqlHelperCommon("PostLoanDB");

            var querySql = "SQL\\AdvCLoanCancel\\SELECT_BUSINESS_ADVCLOANING.sql".ToFileContent(true, companyKey, lendingSide);
            var reader = sqlHelper.ExecuteReader(CommandType.Text, querySql, null);
            string strContent = string.Empty;
            while (reader.Read())
            {
                strContent += string.Format("{0}\t{1}\t{2}\r\n", reader.GetInt32(0), reader.GetString(1),
                    reader.GetString(2));
            }

            reader.Close();
            var sql = "SQL\\AdvCLoanCancel\\UPDATE_BUSINESS_FORADVCLOAN.sql".ToFileContent(true, companyKey, lendingSide);
            sqlHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            return strContent;
        }

        /// <summary>
        /// 为导出订单加锁
        /// </summary>
        /// <param name="serid"></param>
        private string SetBusinessLock(int serid, int lendingid)
        {
            string lockkey = Crypto.GetFrozenNo((byte)EnumPayKind.Payment_Bank);
            string sql = "SQL\\BankExport\\PayBankBusinessLock.sql".ToFileContent(true, lockkey, serid, lendingid);

            SqlHelperCommon sqlHelper = new SqlHelperCommon("PostLoanDB");
            sqlHelper.ExecuteNonQuery(CommandType.Text, sql, 100000, null);
            return lockkey;
        }


        /// <summary>
        /// 根据子公司编号获取导出扣款文件名
        /// </summary>
        /// <param name="serid"></param>
        /// <returns></returns>
        private Dictionary<int, string> GetTitles(int serid, string lockkey)
        {
            var dic = new Dictionary<int, string>();

            string sql = "SELECT * FROM [dbo].[DeductCommand] WHERE ServiceSideID = {0}".StringFormat(serid);
            PayBankDal dal = new PayBankDal(); 
            var list = dal.GetDeductCommand(sql);

            List<BankAccount> lstBankAccount = Singleton<BankAccountsCache>.Instance.BankAccounts;
            foreach (var cmd in list)
            {
                dic.Add(cmd.LendingSideID, lstBankAccount.FirstOrDefault
                    (p => p.BankAccountID == cmd.LendingSideID).AccountNumber + "_" + lockkey);
            }
            return dic;
        }

        /// <summary>
        /// 根据子公司编号获取导出扣款指令配置
        /// </summary>
        /// <param name="serid"></param>
        /// <returns></returns>
        private Dictionary<int, DeductCommand> GetDeductCommand(int serid)
        {
            var dic = new Dictionary<int, DeductCommand>();

            string sql = "SELECT * FROM [dbo].[DeductCommand] WHERE ServiceSideID = {0}".StringFormat(serid);
            PayBankDal dal = new PayBankDal();
            var list = dal.GetDeductCommand(sql);

            foreach (var cmd in list)
            {
                dic.Add(cmd.LendingSideID, cmd);
            }
            return dic;
        }

        /// <summary>
        /// 为导出订单解锁（错误订单解锁、正常回盘解锁）
        /// </summary>
        /// <param name="serid"></param>
        public void SetBusinessUnLock(string lockkey)
        {
            string sql = string.Format("update Business set FrozenNo='' where FrozenNo='{0}'", lockkey);

            SqlHelperCommon sqlHelper = new SqlHelperCommon("PostLoanDB");
            sqlHelper.ExecuteNonQuery(CommandType.Text, sql, 100000, null);
        }
    }
}

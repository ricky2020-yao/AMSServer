using Cn.Vcredit.AMS.BaseService.Command;
using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.BaseService.Communication;
using Cn.Vcredit.AMS.BaseService.Communication.MSMQ;
using Cn.Vcredit.AMS.BaseService.Manager;
using Cn.Vcredit.AMS.BaseService.Service.Data;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Common.XmlConfigData;
using Cn.Vcredit.AMS.Controller.Manager;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using ExportLogHandler = Cn.Vcredit.AMS.Common.Define.DelegataDefine.ExportLogHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cn.Vcredit.Common.Tools;
using Cn.Vcredit.AMS.Common.Consts;
using System.Diagnostics;
using Cn.Vcredit.AMS.DataAccess.BLL.Redis;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.AMS.Data.DB.RedisData;

namespace Cn.Vcredit.AMS.Server
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月20日
    /// Description:服务器端启动画面
    /// </summary>
    public partial class ServiceMainForm : Form
    {
        #region 内部定义
        // 通信控制类
        private MSMQServer m_CommunicationControl;
        // 服务器ID 
        private string m_ServerId = "0123456789";
        // 日志记录
        private ILogger m_Logger = LogFactory.CreateLogger(typeof(ServiceMainForm));
        // 是否已经加载服务
        private bool m_IsLoadService = false;
        // 接收请求队列
        private ResultCacheManager<ServiceCommand> m_ReceiveCommandCaches = null;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化
        /// </summary>
        public ServiceMainForm()
        {
            InitializeComponent();
            //初始化接收请求队列
            m_ReceiveCommandCaches = new ResultCacheManager<ServiceCommand>();

            // 初始化通信控制类
            m_CommunicationControl = new MSMQServer();
            m_CommunicationControl.MessageReceived += CommunicationControl_MessageReceived;

            // 发送处理结果
            Thread thread = new Thread(new ThreadStart(DealResult));
            thread.IsBackground = true;
            thread.Start();
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="logMessage"></param>
        private void LogExport(string logMessage)
        {
            this.BeginInvoke(new ExportLogHandler(ExportLog), logMessage);
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="logMessage"></param>
        private void ExportLog(string logMessage)
        {
            if (this.TxtLog.Text.Trim().Length == 0)
                this.TxtLog.Text = logMessage;
            else
                this.TxtLog.Text
                    = string.Format("{0}{1}{2}", this.TxtLog.Text, Environment.NewLine, logMessage);
        }

        /// <summary>
        /// 处理结收到结果信息
        /// </summary>
        private void DealResult()
        {
            while (true)
            {
                if (!m_IsLoadService) continue;

                try
                {
                    string resultId = "";
                    ResponseEntity responseEntity = Singleton<ResultCacheManager<ResponseEntity>>.Instance
                        .GetOneResult(out resultId);

                    if (!string.IsNullOrEmpty(resultId) && responseEntity != null)
                    {
                        ServiceCommand resultCommand = new ServiceCommand();
                        ServiceCommand command = m_ReceiveCommandCaches.GetResult(resultId);
                        if (command == null)
                            continue;

                        m_Logger.Debug("处理结果返回。");

                        // 初始化结果
                        resultCommand.CreateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss:fff");
                        resultCommand.Entity = responseEntity;
                        resultCommand.Guid = command.Guid;
                        resultCommand.Priority = command.Priority;
                        resultCommand.ReceiveId = command.SendId;
                        resultCommand.SendId = m_ServerId;
                        resultCommand.ServiceName = command.ServiceName;

                        //m_CommunicationControl.Send(resultCommand);

                        ResultService.ResultService resultService = new ResultService.ResultService();
                        //this.BeginInvoke(new ExportLogHandler(ExportLog), "WebService URL:" + resultService.Url);
                        m_Logger.Debug(resultService.Url);
                        byte[] bytResult = ConvertUtility.CodingToByte(resultCommand.ClassToCommandString(), 2);
                        byte[] bytCompress = CompressHelper.Compress(EnumCompressType.MemCompress, bytResult);
                        resultService.SendByteResult(bytCompress);
                        
                    }
                }
                catch (Exception ex)
                {
                    LogExport(ex.Message);
                    LogExport(ex.StackTrace);
                }

                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 接收到的数据
        /// </summary>
        /// <param name="message">0</param>
        private void CommunicationControl_MessageReceived(ServiceCommand message)
        {
            RequestEntity requestEntity = message.Entity as RequestEntity;

            if (requestEntity == null)
                return;

            ServiceMap map = Singleton<ConfigManager>.Instance.GetServiceMap(requestEntity.ServiceId);

            // 请求命令格式错误
            if (map == null)
            {
                // 生成请求命令格式错误的响应消息
                ResponseEntity responseEntity = GenerateRequestCommandError(requestEntity);
                //放入字典
                Singleton<ResultCacheManager<ResponseEntity>>.Instance.AddResult(message.Guid, responseEntity);
                return;
            }

            m_Logger.Debug("调用的服务：" + map.ServiceFullName);
            message.ServiceName = map.ServiceName;
            message.ServiceFullName = map.ServiceFullName;
            message.ServiceDLLName = map.ServiceDLLName;
            message.Priority = map.Priority;

            ServiceData data = new ServiceData();
            data.Command = message;

            // 放入请求队列
            m_ReceiveCommandCaches.AddResult(message.Guid, message);
            m_Logger.Debug("请求入队。");
            // 请求入队
            Singleton<QueueManager>.Instance.Enqueue(data);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 生成请求命令格式错误的响应消息
        /// </summary>
        /// <param name="requestEntity"></param>
        /// <returns></returns>
        private ResponseEntity GenerateRequestCommandError(RequestEntity requestEntity)
        {
            ResponseEntity responseEntity = new ResponseEntity();

            responseEntity.RequestId = requestEntity.RequestId;
            responseEntity.UserId = requestEntity.UserId;
            responseEntity.CompressType = (int)EnumCompressType.None;
            responseEntity.EncyptionType = (int)EnumEncyptionType.None;

            ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.RequestCommandError);

            return responseEntity;
        }

        /// <summary>
        /// 加载所有服务
        /// </summary>
        private void LoadAllService()
        {
            this.TxtLog.Clear();
            this.BtnRegisterAll.Enabled = true;
            this.BtnAddService.Enabled = false;

            if (!m_IsLoadService)
            {
                Singleton<QueueManager>.Instance.LogExport += LogExport;
                Singleton<ConfigManager>.Instance.LogExport += LogExport;
                Singleton<ServiceManager>.Instance.LogExport += LogExport;
            }

            // 加载服务端配置文件
            Singleton<ConfigManager>.Instance.ReloadAllServiceMapConfig();
            Singleton<ServiceManager>.Instance.RegisterAllServices();
            m_IsLoadService = true;
            this.BtnRegisterAll.Enabled = false;
            this.BtnAddService.Enabled = true;
        }

        /// <summary>
        /// 卸载所有服务
        /// </summary>
        private void UnLoadAllService()
        {
            if (m_IsLoadService)
            {
                Singleton<QueueManager>.Instance.LogExport -= LogExport;
                Singleton<ConfigManager>.Instance.LogExport -= LogExport;
                Singleton<ServiceManager>.Instance.LogExport -= LogExport;
            }

            m_IsLoadService = false;
            Singleton<ServiceManager>.Instance.UnLoadAllServices();
        }
        #endregion

        #region 事件
        /// <summary>
        /// 注册所有服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRegisterAll_Click(object sender, EventArgs e)
        {
            LoadAllService();
            MessageBox.Show("注册所有服务成功。");
        }

        /// <summary>
        /// 注册增量服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddService_Click(object sender, EventArgs e)
        {
            m_IsLoadService = true;
        }

        /// <summary>
        /// 卸载所有服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUnLoad_Click(object sender, EventArgs e)
        {
            UnLoadAllService();
            this.BtnRegisterAll.Enabled = true;
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServiceMainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllService();
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new ExportLogHandler(ExportLog), ex.Message);
                this.BeginInvoke(new ExportLogHandler(ExportLog), ex.StackTrace);
                MessageBox.Show("自动初始化失败。请查看日志，修改错误，手动启动服务。");
            }
        }

        /// <summary>
        /// 单击显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NiAMSServer_Click(object sender, System.EventArgs e)
        {
            this.Visible = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        /// <summary>
        /// 画面关闭时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServiceMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TipForm tipForm = new TipForm();
            tipForm.ShowDialog();

            if (!tipForm.IsClose && !tipForm.IsHide)
            {
                e.Cancel = true;
                return;
            }

            if(tipForm.IsHide)
            {
                this.Hide();
                this.NiAMSServer.Visible = true;
                e.Cancel = true;
                return;
            }

            // 卸载所有服务
            UnLoadAllService();
        }

        /// <summary>
        /// Redis全量同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAllSync_Click(object sender, EventArgs e)
        {
            try
            {
                // 记录执行时间类
                using (StopWatcherAuto auto = new StopWatcherAuto())
                {
                    Singleton<RedisEnumOperatorBLL>.Instance.FullSyncRedisEnumData();
                }

                MessageBox.Show("全量同步成功。");
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new ExportLogHandler(ExportLog), ex.Message);
                this.BeginInvoke(new ExportLogHandler(ExportLog), ex.StackTrace);
                MessageBox.Show("全量同步失败。请查看日志，修改错误。");
            }
        }
        #endregion

        #region Key_Value
        /// <summary>
        /// Key_Value数据同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnKeySync_Click(object sender, EventArgs e)
        {
            try
            {
                // 记录执行时间类
                using (StopWatcherAuto auto = new StopWatcherAuto())
                {
                    // 用户
                    if (this.ChkUser.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncUserData();
                    // 服务方
                    if (this.ChkService.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncServiceGroup();
                    // 担保方
                    if (this.ChkGuarantee.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncGuaranteeGroup();
                    // 放贷方
                    if (this.ChkLending.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncLendingGroup();
                    // 订单类型
                    if (this.ChkBusinessStatus.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncTypeEnumData(
                            RedisEnumOperatorBLL.HashId_LoanKind_6, Const.LOANKIND);
                    // 贷款产品类型
                    if (this.ChkProductKind.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncTypeEnumData(
                            RedisEnumOperatorBLL.HashId_ProductKind_7, Const.PRODUCTKIND);
                    // 银行数据
                    if (this.ChkBank.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncTypeEnumData(
                            RedisEnumOperatorBLL.HashId_BankList_8, Const.BANKLIST);
                    // 销售模式数据
                    if (this.ChkSaleMode.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncTypeEnumData(
                            RedisEnumOperatorBLL.HashId_SaleMode_13, Const.SALEMODE);
                    // 工商注册类型
                    if (this.ChkEntregist.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncTypeEnumData(
                            RedisEnumOperatorBLL.HashId_Entregist_14, Const.ENTREGIST);
                    // 订单状态
                    if (this.ChkBusinessStatus.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncTypeEnumDataForValue(
                            RedisEnumOperatorBLL.HashId_BusinessStatus_10, Const.BUSINESSSTATUS);
                    // 清贷状态
                    if (this.ChkCloanStatus.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncTypeEnumDataForValue(
                            RedisEnumOperatorBLL.HashId_CLoanStatus_11, Const.CLOANSTATUS);
                    // 18位合同号适用地区
                    if (this.Chk18RegionKey.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncTypeEnumData(
                            RedisEnumOperatorBLL.HashId_18Region_15, Const.CODE_FITAREA);
                    // 分公司
                    if (this.ChkSubCompanyKey.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncTypeEnumData(
                            RedisEnumOperatorBLL.HashId_SubCompany_16, Const.SUBCOMPANY);
                    // 地区
                    if (this.ChkRegion.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncRegionEnumData();
                    // 门店
                    if (this.ChkStore.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncStoreEnumData();

                }

                MessageBox.Show("Key_Value数据同步成功。");
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new ExportLogHandler(ExportLog), ex.Message);
                this.BeginInvoke(new ExportLogHandler(ExportLog), ex.StackTrace);
                MessageBox.Show("Key_Value数据同步失败。请查看日志，修改错误。");
            }
        }
        #endregion

        #region 下拉框
        /// <summary>
        /// 下拉框数据同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDropSync_Click(object sender, EventArgs e)
        {
            try
            {
                // 记录执行时间类
                using (StopWatcherAuto auto = new StopWatcherAuto())
                {
                    // 公司
                    if (this.ChkDropCompany.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncDropListEnumData(SysConst.COMPANY);
                    // 订单类型
                    if (this.ChkDropLoanKind.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncDropListEnumData(SysConst.LOANKIND);
                    // 贷款产品种类
                    if (this.ChkDropProductKind.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncDropListEnumData(SysConst.PRODUCTKIND);
                    // 银行
                    if (this.ChkDropBank.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncDropListEnumData(Const.BANKLIST);
                    // 订单状态
                    if (this.ChkDropBusinessStatus.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncDropListEnumData(SysConst.BUSINESSSTATUS);
                    // 清贷状态
                    if (this.ChkDropClearStatus.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncDropListEnumData(SysConst.CLOANSTATUS);
                    // 账单状态类型
                    if (this.ChkDropBillStatus.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncDropListEnumData(SysConst.BILLSTATUS);
                    // 诉讼状态类型
                    if (this.ChkDropLawSuit.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncDropListEnumData(SysConst.LAWSUITSTATUS);
                    // 工商注册类型
                    if (this.ChkDropEntregist.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncDropListEnumData(SysConst.ENTREGIST);
                    // 18位合同号适用地区
                    if (this.Chk18Region.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncDropListEnumData(SysConst.CODE_FITAREA);
                    // 分公司
                    if (this.ChkSubCompany.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncDropListEnumData(SysConst.SUBCOMPANY);
                    // 地区和门店
                    if (this.ChkDropRS.Checked)
                    {
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncRegionData();
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncStoreData();
                    }
                    // 团队信息
                    if (this.ChkDropTeam.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncTeamData();
                    // 门店地区关联信息
                    if (this.ChkDropRSR.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncRegionStoreData();

                }

                MessageBox.Show("下拉框数据同步成功。");
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new ExportLogHandler(ExportLog), ex.Message);
                this.BeginInvoke(new ExportLogHandler(ExportLog), ex.StackTrace);
                MessageBox.Show("下拉框数据同步失败。请查看日志，修改错误。");
            }

        }
        #endregion

        #region 权限
        /// <summary>
        /// 同步权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSyncPermission_Click(object sender, EventArgs e)
        {
            try
            {
                // 记录执行时间类
                using (StopWatcherAuto auto = new StopWatcherAuto())
                {
                    // 用户菜单权限
                    if (this.ChkMenuPermission.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncMenuPermission();
                    // 地区权限
                    if (this.ChkRegionPermission.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncRegionPermission();
                    // 门店权限
                    if (this.ChkStroePermission.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncStroePermission();
                    // 分部权限
                    if (this.ChkDivisionPermission.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SynsDivisionPermission();
                }

                MessageBox.Show("权限数据同步成功。");
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new ExportLogHandler(ExportLog), ex.Message);
                this.BeginInvoke(new ExportLogHandler(ExportLog), ex.StackTrace);
                MessageBox.Show("权限数据同步失败。请查看日志，修改错误。");
            }
        }
        #endregion

        #region 同步基础数据
        /// <summary>
        /// 同步基础数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBaseInfo_Click(object sender, EventArgs e)
        {
            try
            {
                // 记录执行时间类
                using (StopWatcherAuto auto = new StopWatcherAuto())
                {
                    // 银行账户信息
                    if (this.ChkBankAccount.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncBankAccount();
                    // 关帐日信息
                    if (this.ChkCloseBillDay.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncCloseBillDay();
                    // 账户关帐日信息
                    if (this.ChkAccountingCloseBillDay.Checked)
                        Singleton<RedisEnumOperatorBLL>.Instance.SyncAccountingCloseBillDay();
                }

                MessageBox.Show("同步基础数据成功。");
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new ExportLogHandler(ExportLog), ex.Message);
                this.BeginInvoke(new ExportLogHandler(ExportLog), ex.StackTrace);
                MessageBox.Show("同步基础数据失败。请查看日志，修改错误。");
            }
        }
        #endregion
    }
}

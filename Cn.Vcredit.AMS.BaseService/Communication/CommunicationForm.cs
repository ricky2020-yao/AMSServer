using Cn.Vcredit.AMS.BaseService.Command;
using Cn.Vcredit.Common.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Cn.Vcredit.AMS.BaseService.Communication
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月20日
    /// Description:通信窗体
    /// </summary>
    public partial class CommunicationForm : Form,ICommunication
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public CommunicationForm()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 自己的ID
        /// </summary>
        private string m_HostID = "";
        /// <summary>
        /// 日志记录
        /// </summary>
        private ILogger m_Logger = LogFactory.CreateLogger(typeof(CommunicationForm));

        /// <summary>
        /// 获取自己的ID
        /// </summary>
        public string HostID
        {
            get { return m_HostID; }
        }

        /// <summary>
        /// 接收到消息触发的事件
        /// </summary>
        public event MessageReceivedHandler MessageReceived;

        /// <summary>
        /// 断开连接触发的事件
        /// </summary>
        public event DisconnectedHandler Disconnected;
        
        /// <summary>
        /// 消息名
        /// </summary>
        int WM_COPYDATA = 0x004A;

        /// <summary>
        /// 定义一个新的窗口消息
        /// </summary>
        /// <param name="lpString">（被注册）消息的名字</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "RegisterWindowMessageA")]
        private static extern int RegisterWindowMessage(string lpString);

        /// <summary>
        /// 寻找窗体
        /// </summary>
        /// <param name="lpClassName">指针类名</param>
        /// <param name="lpWindowName">指向窗口的名字</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="hWnd">目标窗口的句柄</param>
        /// <param name="Msg">消息</param>
        /// <param name="wParam">第一个消息参数</param>
        /// <param name="lParam">第二个消息参数</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(int hWnd,int Msg,int wParam,ref COPYDATASTRUCT lParam);

        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="hWnd">目标窗口的句柄</param>
        /// <param name="Msg">消息</param>
        /// <param name="wParam">第一个消息参数</param>
        /// <param name="lParam">第二个消息参数</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 通信结构
        /// </summary>
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }

        /// <summary>
        /// 创建窗体
        /// </summary>
        /// <param name="hostId"></param>
        public CommunicationForm(string hostId)
        {
            InitializeComponent();

            try
            {
                this.m_HostID = hostId;

                this.StartPosition = FormStartPosition.Manual;
                this.Size = new Size(1, 1);
                this.Location = new System.Drawing.Point(-1000000, -1000000);
                this.Text = hostId;
                //this.Handle = hostId;
                this.Name = hostId;
                //this.Load += new EventHandler(ClientCallBackFrm_Load);
                this.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                m_Logger.Error("初始化CommunicationControl" + ex.Message);
                m_Logger.Error("初始化CommunicationControl" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息</param>
        public void SendMessage(ServiceCommand message)
        {
            try
            {
                m_Logger.Debug("开始发送的消息。");
                int WINDOW_HANDLER = 0;
                message.SendId = m_HostID;

                string content = message.ClassToCommandString();
                m_Logger.Debug("发送的消息为：" + content);
                m_Logger.Debug("目标窗体ID：" + message.ReceiveId);
                WINDOW_HANDLER = FindWindow(null, message.ReceiveId);
                m_Logger.Debug("目标窗体句柄：" + WINDOW_HANDLER);
                if (WINDOW_HANDLER != 0)
                {
                    IntPtr ptr = Marshal.StringToHGlobalUni(content);
                    COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                    mystr.dwData = (IntPtr)59;
                    mystr.cbData = content.Length * 2;
                    mystr.lpData = ptr;
                    SendMessage(WINDOW_HANDLER, WM_COPYDATA, 0, ref mystr);
                    Marshal.FreeHGlobal(ptr);
                }
                m_Logger.Debug("发送的消息结束。");
            }
            catch(Exception ex)
            {
                m_Logger.Fatal(this.GetType().Name, ex);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="request">消息</param>
        private void ReceiveMessage(object request)
        {
            try
            {
                if (MessageReceived != null)
                {
                    ServiceCommand requestMessage = request as ServiceCommand;
                    if (requestMessage != null)
                    {
                        MessageReceived(requestMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                m_Logger.Fatal(this.GetType().Name, ex);
            }
        }

        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="m">消息</param>
        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_COPYDATA)
            {
                try
                {
                    COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                    mystr = (COPYDATASTRUCT)Marshal.PtrToStructure(m.LParam, typeof(COPYDATASTRUCT));

                    if (mystr.cbData > 0)
                    {
                        int nLength = mystr.cbData / 2;
                        string str = Marshal.PtrToStringUni(mystr.lpData, nLength);
                        if (str.Length > nLength)
                        {
                            str = str.Substring(0, nLength);
                        }

                        ServiceCommand requestMessage = new ServiceCommand();
                        requestMessage.CommandStringToClass(str);
                        ReceiveMessage(requestMessage);
                    }
                }
                catch (Exception ex)
                {
                    m_Logger.Fatal(this.GetType().Name, ex);
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
        }
    }
}

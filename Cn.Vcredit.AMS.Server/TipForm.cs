using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cn.Vcredit.AMS.Server
{
    public partial class TipForm : Form
    {
        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool IsClose
        {
            get;
            set;
        }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHide
        {
            get;
            set;
        }

        public TipForm()
        {
            InitializeComponent();
            IsClose = false;
            IsHide = false;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (this.Rab1.Checked)
                this.IsHide = true;
            else if (this.Rab2.Checked)
                this.IsClose = true;
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

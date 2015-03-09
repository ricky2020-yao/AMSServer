namespace Cn.Vcredit.AMS.Server
{
    partial class ServiceMainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceMainForm));
            this.TabMain = new System.Windows.Forms.TabControl();
            this.PageLog = new System.Windows.Forms.TabPage();
            this.TxtLog = new System.Windows.Forms.TextBox();
            this.PageControl = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnAddService = new System.Windows.Forms.Button();
            this.BtnUnLoad = new System.Windows.Forms.Button();
            this.BtnRegisterAll = new System.Windows.Forms.Button();
            this.RedisControl = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.ChkAccountingCloseBillDay = new System.Windows.Forms.CheckBox();
            this.ChkCloseBillDay = new System.Windows.Forms.CheckBox();
            this.ChkBankAccount = new System.Windows.Forms.CheckBox();
            this.BtnBaseInfo = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ChkDivisionPermission = new System.Windows.Forms.CheckBox();
            this.ChkStroePermission = new System.Windows.Forms.CheckBox();
            this.ChkRegionPermission = new System.Windows.Forms.CheckBox();
            this.ChkMenuPermission = new System.Windows.Forms.CheckBox();
            this.BtnSyncPermission = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ChkDropLawSuit = new System.Windows.Forms.CheckBox();
            this.ChkDropBillStatus = new System.Windows.Forms.CheckBox();
            this.ChkDropEntregist = new System.Windows.Forms.CheckBox();
            this.ChkDropTeam = new System.Windows.Forms.CheckBox();
            this.ChkDropRS = new System.Windows.Forms.CheckBox();
            this.ChkDropClearStatus = new System.Windows.Forms.CheckBox();
            this.ChkDropBusinessStatus = new System.Windows.Forms.CheckBox();
            this.ChkDropRSR = new System.Windows.Forms.CheckBox();
            this.ChkDropBank = new System.Windows.Forms.CheckBox();
            this.ChkDropProductKind = new System.Windows.Forms.CheckBox();
            this.ChkDropLoanKind = new System.Windows.Forms.CheckBox();
            this.ChkDropCompany = new System.Windows.Forms.CheckBox();
            this.BtnDropSync = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ChkEntregist = new System.Windows.Forms.CheckBox();
            this.ChkStore = new System.Windows.Forms.CheckBox();
            this.ChkRegion = new System.Windows.Forms.CheckBox();
            this.ChkCloanStatus = new System.Windows.Forms.CheckBox();
            this.ChkBusinessStatus = new System.Windows.Forms.CheckBox();
            this.ChkSaleMode = new System.Windows.Forms.CheckBox();
            this.ChkBank = new System.Windows.Forms.CheckBox();
            this.ChkProductKind = new System.Windows.Forms.CheckBox();
            this.ChkLoanKind = new System.Windows.Forms.CheckBox();
            this.ChkGuarantee = new System.Windows.Forms.CheckBox();
            this.ChkLending = new System.Windows.Forms.CheckBox();
            this.ChkService = new System.Windows.Forms.CheckBox();
            this.ChkUser = new System.Windows.Forms.CheckBox();
            this.BtnKeySync = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnAllSync = new System.Windows.Forms.Button();
            this.NiAMSServer = new System.Windows.Forms.NotifyIcon(this.components);
            this.Chk18Region = new System.Windows.Forms.CheckBox();
            this.ChkSubCompany = new System.Windows.Forms.CheckBox();
            this.ChkSubCompanyKey = new System.Windows.Forms.CheckBox();
            this.Chk18RegionKey = new System.Windows.Forms.CheckBox();
            this.TabMain.SuspendLayout();
            this.PageLog.SuspendLayout();
            this.PageControl.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.RedisControl.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabMain
            // 
            this.TabMain.Controls.Add(this.PageLog);
            this.TabMain.Controls.Add(this.PageControl);
            this.TabMain.Controls.Add(this.RedisControl);
            this.TabMain.Location = new System.Drawing.Point(15, 15);
            this.TabMain.Name = "TabMain";
            this.TabMain.SelectedIndex = 0;
            this.TabMain.Size = new System.Drawing.Size(794, 705);
            this.TabMain.TabIndex = 0;
            // 
            // PageLog
            // 
            this.PageLog.Controls.Add(this.TxtLog);
            this.PageLog.Location = new System.Drawing.Point(4, 24);
            this.PageLog.Name = "PageLog";
            this.PageLog.Padding = new System.Windows.Forms.Padding(3);
            this.PageLog.Size = new System.Drawing.Size(786, 677);
            this.PageLog.TabIndex = 1;
            this.PageLog.Text = "日志";
            this.PageLog.UseVisualStyleBackColor = true;
            // 
            // TxtLog
            // 
            this.TxtLog.Location = new System.Drawing.Point(7, 7);
            this.TxtLog.Multiline = true;
            this.TxtLog.Name = "TxtLog";
            this.TxtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtLog.Size = new System.Drawing.Size(770, 660);
            this.TxtLog.TabIndex = 0;
            // 
            // PageControl
            // 
            this.PageControl.Controls.Add(this.groupBox3);
            this.PageControl.Controls.Add(this.groupBox1);
            this.PageControl.Location = new System.Drawing.Point(4, 24);
            this.PageControl.Name = "PageControl";
            this.PageControl.Padding = new System.Windows.Forms.Padding(3);
            this.PageControl.Size = new System.Drawing.Size(786, 677);
            this.PageControl.TabIndex = 0;
            this.PageControl.Text = "控制面板";
            this.PageControl.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(17, 175);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(172, 137);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mongo控制";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnAddService);
            this.groupBox1.Controls.Add(this.BtnUnLoad);
            this.groupBox1.Controls.Add(this.BtnRegisterAll);
            this.groupBox1.Location = new System.Drawing.Point(17, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 137);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务控制";
            // 
            // BtnAddService
            // 
            this.BtnAddService.Location = new System.Drawing.Point(15, 62);
            this.BtnAddService.Name = "BtnAddService";
            this.BtnAddService.Size = new System.Drawing.Size(131, 27);
            this.BtnAddService.TabIndex = 1;
            this.BtnAddService.Text = "注册增量服务";
            this.BtnAddService.UseVisualStyleBackColor = true;
            this.BtnAddService.Click += new System.EventHandler(this.BtnAddService_Click);
            // 
            // BtnUnLoad
            // 
            this.BtnUnLoad.Location = new System.Drawing.Point(15, 97);
            this.BtnUnLoad.Name = "BtnUnLoad";
            this.BtnUnLoad.Size = new System.Drawing.Size(131, 27);
            this.BtnUnLoad.TabIndex = 2;
            this.BtnUnLoad.Text = "卸载所有服务";
            this.BtnUnLoad.UseVisualStyleBackColor = true;
            this.BtnUnLoad.Click += new System.EventHandler(this.BtnUnLoad_Click);
            // 
            // BtnRegisterAll
            // 
            this.BtnRegisterAll.Location = new System.Drawing.Point(15, 27);
            this.BtnRegisterAll.Name = "BtnRegisterAll";
            this.BtnRegisterAll.Size = new System.Drawing.Size(131, 27);
            this.BtnRegisterAll.TabIndex = 0;
            this.BtnRegisterAll.Text = "注册所有服务";
            this.BtnRegisterAll.UseVisualStyleBackColor = true;
            this.BtnRegisterAll.Click += new System.EventHandler(this.BtnRegisterAll_Click);
            // 
            // RedisControl
            // 
            this.RedisControl.Controls.Add(this.groupBox7);
            this.RedisControl.Controls.Add(this.groupBox6);
            this.RedisControl.Controls.Add(this.groupBox5);
            this.RedisControl.Controls.Add(this.groupBox4);
            this.RedisControl.Controls.Add(this.groupBox2);
            this.RedisControl.Location = new System.Drawing.Point(4, 24);
            this.RedisControl.Name = "RedisControl";
            this.RedisControl.Size = new System.Drawing.Size(786, 677);
            this.RedisControl.TabIndex = 2;
            this.RedisControl.Text = "Redis控制";
            this.RedisControl.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.ChkAccountingCloseBillDay);
            this.groupBox7.Controls.Add(this.ChkCloseBillDay);
            this.groupBox7.Controls.Add(this.ChkBankAccount);
            this.groupBox7.Controls.Add(this.BtnBaseInfo);
            this.groupBox7.Location = new System.Drawing.Point(17, 483);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(753, 87);
            this.groupBox7.TabIndex = 9;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "基础数据";
            // 
            // ChkAccountingCloseBillDay
            // 
            this.ChkAccountingCloseBillDay.AutoSize = true;
            this.ChkAccountingCloseBillDay.Location = new System.Drawing.Point(333, 22);
            this.ChkAccountingCloseBillDay.Name = "ChkAccountingCloseBillDay";
            this.ChkAccountingCloseBillDay.Size = new System.Drawing.Size(124, 18);
            this.ChkAccountingCloseBillDay.TabIndex = 8;
            this.ChkAccountingCloseBillDay.Text = "账户关帐日信息";
            this.ChkAccountingCloseBillDay.UseVisualStyleBackColor = true;
            // 
            // ChkCloseBillDay
            // 
            this.ChkCloseBillDay.AutoSize = true;
            this.ChkCloseBillDay.Location = new System.Drawing.Point(169, 22);
            this.ChkCloseBillDay.Name = "ChkCloseBillDay";
            this.ChkCloseBillDay.Size = new System.Drawing.Size(96, 18);
            this.ChkCloseBillDay.TabIndex = 5;
            this.ChkCloseBillDay.Text = "关帐日信息";
            this.ChkCloseBillDay.UseVisualStyleBackColor = true;
            // 
            // ChkBankAccount
            // 
            this.ChkBankAccount.AutoSize = true;
            this.ChkBankAccount.Location = new System.Drawing.Point(12, 22);
            this.ChkBankAccount.Name = "ChkBankAccount";
            this.ChkBankAccount.Size = new System.Drawing.Size(110, 18);
            this.ChkBankAccount.TabIndex = 2;
            this.ChkBankAccount.Text = "银行账户信息";
            this.ChkBankAccount.UseVisualStyleBackColor = true;
            // 
            // BtnBaseInfo
            // 
            this.BtnBaseInfo.Location = new System.Drawing.Point(645, 52);
            this.BtnBaseInfo.Name = "BtnBaseInfo";
            this.BtnBaseInfo.Size = new System.Drawing.Size(72, 27);
            this.BtnBaseInfo.TabIndex = 0;
            this.BtnBaseInfo.Text = "同步";
            this.BtnBaseInfo.UseVisualStyleBackColor = true;
            this.BtnBaseInfo.Click += new System.EventHandler(this.BtnBaseInfo_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ChkDivisionPermission);
            this.groupBox6.Controls.Add(this.ChkStroePermission);
            this.groupBox6.Controls.Add(this.ChkRegionPermission);
            this.groupBox6.Controls.Add(this.ChkMenuPermission);
            this.groupBox6.Controls.Add(this.BtnSyncPermission);
            this.groupBox6.Location = new System.Drawing.Point(17, 390);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(753, 87);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "权限";
            // 
            // ChkDivisionPermission
            // 
            this.ChkDivisionPermission.AutoSize = true;
            this.ChkDivisionPermission.Location = new System.Drawing.Point(333, 22);
            this.ChkDivisionPermission.Name = "ChkDivisionPermission";
            this.ChkDivisionPermission.Size = new System.Drawing.Size(82, 18);
            this.ChkDivisionPermission.TabIndex = 8;
            this.ChkDivisionPermission.Text = "分部权限";
            this.ChkDivisionPermission.UseVisualStyleBackColor = true;
            // 
            // ChkStroePermission
            // 
            this.ChkStroePermission.AutoSize = true;
            this.ChkStroePermission.Location = new System.Drawing.Point(227, 22);
            this.ChkStroePermission.Name = "ChkStroePermission";
            this.ChkStroePermission.Size = new System.Drawing.Size(82, 18);
            this.ChkStroePermission.TabIndex = 6;
            this.ChkStroePermission.Text = "门店权限";
            this.ChkStroePermission.UseVisualStyleBackColor = true;
            // 
            // ChkRegionPermission
            // 
            this.ChkRegionPermission.AutoSize = true;
            this.ChkRegionPermission.Location = new System.Drawing.Point(131, 22);
            this.ChkRegionPermission.Name = "ChkRegionPermission";
            this.ChkRegionPermission.Size = new System.Drawing.Size(82, 18);
            this.ChkRegionPermission.TabIndex = 5;
            this.ChkRegionPermission.Text = "地区权限";
            this.ChkRegionPermission.UseVisualStyleBackColor = true;
            // 
            // ChkMenuPermission
            // 
            this.ChkMenuPermission.AutoSize = true;
            this.ChkMenuPermission.Location = new System.Drawing.Point(12, 22);
            this.ChkMenuPermission.Name = "ChkMenuPermission";
            this.ChkMenuPermission.Size = new System.Drawing.Size(110, 18);
            this.ChkMenuPermission.TabIndex = 2;
            this.ChkMenuPermission.Text = "用户菜单权限";
            this.ChkMenuPermission.UseVisualStyleBackColor = true;
            // 
            // BtnSyncPermission
            // 
            this.BtnSyncPermission.Location = new System.Drawing.Point(645, 52);
            this.BtnSyncPermission.Name = "BtnSyncPermission";
            this.BtnSyncPermission.Size = new System.Drawing.Size(72, 27);
            this.BtnSyncPermission.TabIndex = 0;
            this.BtnSyncPermission.Text = "同步";
            this.BtnSyncPermission.UseVisualStyleBackColor = true;
            this.BtnSyncPermission.Click += new System.EventHandler(this.BtnSyncPermission_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ChkSubCompany);
            this.groupBox5.Controls.Add(this.Chk18Region);
            this.groupBox5.Controls.Add(this.ChkDropLawSuit);
            this.groupBox5.Controls.Add(this.ChkDropBillStatus);
            this.groupBox5.Controls.Add(this.ChkDropEntregist);
            this.groupBox5.Controls.Add(this.ChkDropTeam);
            this.groupBox5.Controls.Add(this.ChkDropRS);
            this.groupBox5.Controls.Add(this.ChkDropClearStatus);
            this.groupBox5.Controls.Add(this.ChkDropBusinessStatus);
            this.groupBox5.Controls.Add(this.ChkDropRSR);
            this.groupBox5.Controls.Add(this.ChkDropBank);
            this.groupBox5.Controls.Add(this.ChkDropProductKind);
            this.groupBox5.Controls.Add(this.ChkDropLoanKind);
            this.groupBox5.Controls.Add(this.ChkDropCompany);
            this.groupBox5.Controls.Add(this.BtnDropSync);
            this.groupBox5.Location = new System.Drawing.Point(16, 238);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(753, 146);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "下拉框";
            // 
            // ChkDropLawSuit
            // 
            this.ChkDropLawSuit.AutoSize = true;
            this.ChkDropLawSuit.Location = new System.Drawing.Point(645, 21);
            this.ChkDropLawSuit.Name = "ChkDropLawSuit";
            this.ChkDropLawSuit.Size = new System.Drawing.Size(82, 18);
            this.ChkDropLawSuit.TabIndex = 16;
            this.ChkDropLawSuit.Text = "诉讼状态";
            this.ChkDropLawSuit.UseVisualStyleBackColor = true;
            // 
            // ChkDropBillStatus
            // 
            this.ChkDropBillStatus.AutoSize = true;
            this.ChkDropBillStatus.Location = new System.Drawing.Point(548, 21);
            this.ChkDropBillStatus.Name = "ChkDropBillStatus";
            this.ChkDropBillStatus.Size = new System.Drawing.Size(82, 18);
            this.ChkDropBillStatus.TabIndex = 15;
            this.ChkDropBillStatus.Text = "账单状态";
            this.ChkDropBillStatus.UseVisualStyleBackColor = true;
            // 
            // ChkDropEntregist
            // 
            this.ChkDropEntregist.AutoSize = true;
            this.ChkDropEntregist.Location = new System.Drawing.Point(12, 57);
            this.ChkDropEntregist.Name = "ChkDropEntregist";
            this.ChkDropEntregist.Size = new System.Drawing.Size(110, 18);
            this.ChkDropEntregist.TabIndex = 14;
            this.ChkDropEntregist.Text = "工商注册类型";
            this.ChkDropEntregist.UseVisualStyleBackColor = true;
            // 
            // ChkDropTeam
            // 
            this.ChkDropTeam.AutoSize = true;
            this.ChkDropTeam.Location = new System.Drawing.Point(297, 57);
            this.ChkDropTeam.Name = "ChkDropTeam";
            this.ChkDropTeam.Size = new System.Drawing.Size(82, 18);
            this.ChkDropTeam.TabIndex = 13;
            this.ChkDropTeam.Text = "团队信息";
            this.ChkDropTeam.UseVisualStyleBackColor = true;
            // 
            // ChkDropRS
            // 
            this.ChkDropRS.AutoSize = true;
            this.ChkDropRS.Location = new System.Drawing.Point(170, 57);
            this.ChkDropRS.Name = "ChkDropRS";
            this.ChkDropRS.Size = new System.Drawing.Size(96, 18);
            this.ChkDropRS.TabIndex = 12;
            this.ChkDropRS.Text = "地区和门店";
            this.ChkDropRS.UseVisualStyleBackColor = true;
            // 
            // ChkDropClearStatus
            // 
            this.ChkDropClearStatus.AutoSize = true;
            this.ChkDropClearStatus.Location = new System.Drawing.Point(460, 21);
            this.ChkDropClearStatus.Name = "ChkDropClearStatus";
            this.ChkDropClearStatus.Size = new System.Drawing.Size(82, 18);
            this.ChkDropClearStatus.TabIndex = 11;
            this.ChkDropClearStatus.Text = "清贷状态";
            this.ChkDropClearStatus.UseVisualStyleBackColor = true;
            // 
            // ChkDropBusinessStatus
            // 
            this.ChkDropBusinessStatus.AutoSize = true;
            this.ChkDropBusinessStatus.Location = new System.Drawing.Point(363, 21);
            this.ChkDropBusinessStatus.Name = "ChkDropBusinessStatus";
            this.ChkDropBusinessStatus.Size = new System.Drawing.Size(82, 18);
            this.ChkDropBusinessStatus.TabIndex = 10;
            this.ChkDropBusinessStatus.Text = "订单状态";
            this.ChkDropBusinessStatus.UseVisualStyleBackColor = true;
            // 
            // ChkDropRSR
            // 
            this.ChkDropRSR.AutoSize = true;
            this.ChkDropRSR.Location = new System.Drawing.Point(460, 57);
            this.ChkDropRSR.Name = "ChkDropRSR";
            this.ChkDropRSR.Size = new System.Drawing.Size(138, 18);
            this.ChkDropRSR.TabIndex = 9;
            this.ChkDropRSR.Text = "门店地区关联信息";
            this.ChkDropRSR.UseVisualStyleBackColor = true;
            // 
            // ChkDropBank
            // 
            this.ChkDropBank.AutoSize = true;
            this.ChkDropBank.Location = new System.Drawing.Point(297, 21);
            this.ChkDropBank.Name = "ChkDropBank";
            this.ChkDropBank.Size = new System.Drawing.Size(54, 18);
            this.ChkDropBank.TabIndex = 8;
            this.ChkDropBank.Text = "银行";
            this.ChkDropBank.UseVisualStyleBackColor = true;
            // 
            // ChkDropProductKind
            // 
            this.ChkDropProductKind.AutoSize = true;
            this.ChkDropProductKind.Location = new System.Drawing.Point(170, 22);
            this.ChkDropProductKind.Name = "ChkDropProductKind";
            this.ChkDropProductKind.Size = new System.Drawing.Size(110, 18);
            this.ChkDropProductKind.TabIndex = 6;
            this.ChkDropProductKind.Text = "贷款产品类型";
            this.ChkDropProductKind.UseVisualStyleBackColor = true;
            // 
            // ChkDropLoanKind
            // 
            this.ChkDropLoanKind.AutoSize = true;
            this.ChkDropLoanKind.Location = new System.Drawing.Point(75, 23);
            this.ChkDropLoanKind.Name = "ChkDropLoanKind";
            this.ChkDropLoanKind.Size = new System.Drawing.Size(82, 18);
            this.ChkDropLoanKind.TabIndex = 5;
            this.ChkDropLoanKind.Text = "订单类型";
            this.ChkDropLoanKind.UseVisualStyleBackColor = true;
            // 
            // ChkDropCompany
            // 
            this.ChkDropCompany.AutoSize = true;
            this.ChkDropCompany.Location = new System.Drawing.Point(12, 23);
            this.ChkDropCompany.Name = "ChkDropCompany";
            this.ChkDropCompany.Size = new System.Drawing.Size(54, 18);
            this.ChkDropCompany.TabIndex = 2;
            this.ChkDropCompany.Text = "公司";
            this.ChkDropCompany.UseVisualStyleBackColor = true;
            // 
            // BtnDropSync
            // 
            this.BtnDropSync.Location = new System.Drawing.Point(646, 113);
            this.BtnDropSync.Name = "BtnDropSync";
            this.BtnDropSync.Size = new System.Drawing.Size(72, 27);
            this.BtnDropSync.TabIndex = 0;
            this.BtnDropSync.Text = "同步";
            this.BtnDropSync.UseVisualStyleBackColor = true;
            this.BtnDropSync.Click += new System.EventHandler(this.BtnDropSync_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Chk18RegionKey);
            this.groupBox4.Controls.Add(this.ChkSubCompanyKey);
            this.groupBox4.Controls.Add(this.ChkEntregist);
            this.groupBox4.Controls.Add(this.ChkStore);
            this.groupBox4.Controls.Add(this.ChkRegion);
            this.groupBox4.Controls.Add(this.ChkCloanStatus);
            this.groupBox4.Controls.Add(this.ChkBusinessStatus);
            this.groupBox4.Controls.Add(this.ChkSaleMode);
            this.groupBox4.Controls.Add(this.ChkBank);
            this.groupBox4.Controls.Add(this.ChkProductKind);
            this.groupBox4.Controls.Add(this.ChkLoanKind);
            this.groupBox4.Controls.Add(this.ChkGuarantee);
            this.groupBox4.Controls.Add(this.ChkLending);
            this.groupBox4.Controls.Add(this.ChkService);
            this.groupBox4.Controls.Add(this.ChkUser);
            this.groupBox4.Controls.Add(this.BtnKeySync);
            this.groupBox4.Location = new System.Drawing.Point(16, 96);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(753, 136);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Key_Value";
            // 
            // ChkEntregist
            // 
            this.ChkEntregist.AutoSize = true;
            this.ChkEntregist.Location = new System.Drawing.Point(432, 57);
            this.ChkEntregist.Name = "ChkEntregist";
            this.ChkEntregist.Size = new System.Drawing.Size(110, 18);
            this.ChkEntregist.TabIndex = 14;
            this.ChkEntregist.Text = "工商注册类型";
            this.ChkEntregist.UseVisualStyleBackColor = true;
            // 
            // ChkStore
            // 
            this.ChkStore.AutoSize = true;
            this.ChkStore.Location = new System.Drawing.Point(335, 57);
            this.ChkStore.Name = "ChkStore";
            this.ChkStore.Size = new System.Drawing.Size(54, 18);
            this.ChkStore.TabIndex = 13;
            this.ChkStore.Text = "门店";
            this.ChkStore.UseVisualStyleBackColor = true;
            // 
            // ChkRegion
            // 
            this.ChkRegion.AutoSize = true;
            this.ChkRegion.Location = new System.Drawing.Point(247, 57);
            this.ChkRegion.Name = "ChkRegion";
            this.ChkRegion.Size = new System.Drawing.Size(54, 18);
            this.ChkRegion.TabIndex = 12;
            this.ChkRegion.Text = "地区";
            this.ChkRegion.UseVisualStyleBackColor = true;
            // 
            // ChkCloanStatus
            // 
            this.ChkCloanStatus.AutoSize = true;
            this.ChkCloanStatus.Location = new System.Drawing.Point(150, 57);
            this.ChkCloanStatus.Name = "ChkCloanStatus";
            this.ChkCloanStatus.Size = new System.Drawing.Size(82, 18);
            this.ChkCloanStatus.TabIndex = 11;
            this.ChkCloanStatus.Text = "清贷状态";
            this.ChkCloanStatus.UseVisualStyleBackColor = true;
            // 
            // ChkBusinessStatus
            // 
            this.ChkBusinessStatus.AutoSize = true;
            this.ChkBusinessStatus.Location = new System.Drawing.Point(14, 57);
            this.ChkBusinessStatus.Name = "ChkBusinessStatus";
            this.ChkBusinessStatus.Size = new System.Drawing.Size(82, 18);
            this.ChkBusinessStatus.TabIndex = 10;
            this.ChkBusinessStatus.Text = "订单状态";
            this.ChkBusinessStatus.UseVisualStyleBackColor = true;
            // 
            // ChkSaleMode
            // 
            this.ChkSaleMode.AutoSize = true;
            this.ChkSaleMode.Location = new System.Drawing.Point(663, 23);
            this.ChkSaleMode.Name = "ChkSaleMode";
            this.ChkSaleMode.Size = new System.Drawing.Size(82, 18);
            this.ChkSaleMode.TabIndex = 9;
            this.ChkSaleMode.Text = "销售模式";
            this.ChkSaleMode.UseVisualStyleBackColor = true;
            // 
            // ChkBank
            // 
            this.ChkBank.AutoSize = true;
            this.ChkBank.Location = new System.Drawing.Point(558, 23);
            this.ChkBank.Name = "ChkBank";
            this.ChkBank.Size = new System.Drawing.Size(54, 18);
            this.ChkBank.TabIndex = 8;
            this.ChkBank.Text = "银行";
            this.ChkBank.UseVisualStyleBackColor = true;
            // 
            // ChkProductKind
            // 
            this.ChkProductKind.AutoSize = true;
            this.ChkProductKind.Location = new System.Drawing.Point(432, 23);
            this.ChkProductKind.Name = "ChkProductKind";
            this.ChkProductKind.Size = new System.Drawing.Size(110, 18);
            this.ChkProductKind.TabIndex = 6;
            this.ChkProductKind.Text = "贷款产品类型";
            this.ChkProductKind.UseVisualStyleBackColor = true;
            // 
            // ChkLoanKind
            // 
            this.ChkLoanKind.AutoSize = true;
            this.ChkLoanKind.Location = new System.Drawing.Point(335, 23);
            this.ChkLoanKind.Name = "ChkLoanKind";
            this.ChkLoanKind.Size = new System.Drawing.Size(82, 18);
            this.ChkLoanKind.TabIndex = 5;
            this.ChkLoanKind.Text = "订单类型";
            this.ChkLoanKind.UseVisualStyleBackColor = true;
            // 
            // ChkGuarantee
            // 
            this.ChkGuarantee.AutoSize = true;
            this.ChkGuarantee.Location = new System.Drawing.Point(247, 23);
            this.ChkGuarantee.Name = "ChkGuarantee";
            this.ChkGuarantee.Size = new System.Drawing.Size(68, 18);
            this.ChkGuarantee.TabIndex = 4;
            this.ChkGuarantee.Text = "担保方";
            this.ChkGuarantee.UseVisualStyleBackColor = true;
            // 
            // ChkLending
            // 
            this.ChkLending.AutoSize = true;
            this.ChkLending.Location = new System.Drawing.Point(150, 23);
            this.ChkLending.Name = "ChkLending";
            this.ChkLending.Size = new System.Drawing.Size(68, 18);
            this.ChkLending.TabIndex = 3;
            this.ChkLending.Text = "放贷方";
            this.ChkLending.UseVisualStyleBackColor = true;
            // 
            // ChkService
            // 
            this.ChkService.AutoSize = true;
            this.ChkService.Location = new System.Drawing.Point(74, 23);
            this.ChkService.Name = "ChkService";
            this.ChkService.Size = new System.Drawing.Size(68, 18);
            this.ChkService.TabIndex = 2;
            this.ChkService.Text = "服务方";
            this.ChkService.UseVisualStyleBackColor = true;
            // 
            // ChkUser
            // 
            this.ChkUser.AutoSize = true;
            this.ChkUser.Location = new System.Drawing.Point(14, 23);
            this.ChkUser.Name = "ChkUser";
            this.ChkUser.Size = new System.Drawing.Size(54, 18);
            this.ChkUser.TabIndex = 1;
            this.ChkUser.Text = "用户";
            this.ChkUser.UseVisualStyleBackColor = true;
            // 
            // BtnKeySync
            // 
            this.BtnKeySync.Location = new System.Drawing.Point(663, 103);
            this.BtnKeySync.Name = "BtnKeySync";
            this.BtnKeySync.Size = new System.Drawing.Size(72, 27);
            this.BtnKeySync.TabIndex = 0;
            this.BtnKeySync.Text = "同步";
            this.BtnKeySync.UseVisualStyleBackColor = true;
            this.BtnKeySync.Click += new System.EventHandler(this.BtnKeySync_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnAllSync);
            this.groupBox2.Location = new System.Drawing.Point(15, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(172, 81);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "全量同步";
            // 
            // BtnAllSync
            // 
            this.BtnAllSync.Location = new System.Drawing.Point(15, 27);
            this.BtnAllSync.Name = "BtnAllSync";
            this.BtnAllSync.Size = new System.Drawing.Size(131, 27);
            this.BtnAllSync.TabIndex = 0;
            this.BtnAllSync.Text = "全量同步";
            this.BtnAllSync.UseVisualStyleBackColor = true;
            this.BtnAllSync.Click += new System.EventHandler(this.BtnAllSync_Click);
            // 
            // NiAMSServer
            // 
            this.NiAMSServer.Icon = ((System.Drawing.Icon)(resources.GetObject("NiAMSServer.Icon")));
            this.NiAMSServer.Text = "notifyIcon1";
            this.NiAMSServer.Click += new System.EventHandler(this.NiAMSServer_Click);
            // 
            // Chk18Region
            // 
            this.Chk18Region.AutoSize = true;
            this.Chk18Region.Location = new System.Drawing.Point(12, 93);
            this.Chk18Region.Name = "Chk18Region";
            this.Chk18Region.Size = new System.Drawing.Size(152, 18);
            this.Chk18Region.TabIndex = 17;
            this.Chk18Region.Text = "18位合同号适用地区";
            this.Chk18Region.UseVisualStyleBackColor = true;
            // 
            // ChkSubCompany
            // 
            this.ChkSubCompany.AutoSize = true;
            this.ChkSubCompany.Location = new System.Drawing.Point(646, 57);
            this.ChkSubCompany.Name = "ChkSubCompany";
            this.ChkSubCompany.Size = new System.Drawing.Size(68, 18);
            this.ChkSubCompany.TabIndex = 18;
            this.ChkSubCompany.Text = "分公司";
            this.ChkSubCompany.UseVisualStyleBackColor = true;
            // 
            // ChkSubCompanyKey
            // 
            this.ChkSubCompanyKey.AutoSize = true;
            this.ChkSubCompanyKey.Location = new System.Drawing.Point(12, 91);
            this.ChkSubCompanyKey.Name = "ChkSubCompanyKey";
            this.ChkSubCompanyKey.Size = new System.Drawing.Size(68, 18);
            this.ChkSubCompanyKey.TabIndex = 19;
            this.ChkSubCompanyKey.Text = "分公司";
            this.ChkSubCompanyKey.UseVisualStyleBackColor = true;
            // 
            // Chk18RegionKey
            // 
            this.Chk18RegionKey.AutoSize = true;
            this.Chk18RegionKey.Location = new System.Drawing.Point(558, 57);
            this.Chk18RegionKey.Name = "Chk18RegionKey";
            this.Chk18RegionKey.Size = new System.Drawing.Size(152, 18);
            this.Chk18RegionKey.TabIndex = 20;
            this.Chk18RegionKey.Text = "18位合同号适用地区";
            this.Chk18RegionKey.UseVisualStyleBackColor = true;
            // 
            // ServiceMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 734);
            this.Controls.Add(this.TabMain);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ServiceMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "账务系统服务";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServiceMainForm_FormClosing);
            this.Load += new System.EventHandler(this.ServiceMainForm_Load);
            this.TabMain.ResumeLayout(false);
            this.PageLog.ResumeLayout(false);
            this.PageLog.PerformLayout();
            this.PageControl.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.RedisControl.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabMain;
        private System.Windows.Forms.TabPage PageControl;
        private System.Windows.Forms.TabPage PageLog;
        private System.Windows.Forms.TextBox TxtLog;
        private System.Windows.Forms.Button BtnUnLoad;
        private System.Windows.Forms.Button BtnAddService;
        private System.Windows.Forms.Button BtnRegisterAll;
        private System.Windows.Forms.NotifyIcon NiAMSServer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabPage RedisControl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnAllSync;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button BtnKeySync;
        private System.Windows.Forms.CheckBox ChkUser;
        private System.Windows.Forms.CheckBox ChkSaleMode;
        private System.Windows.Forms.CheckBox ChkBank;
        private System.Windows.Forms.CheckBox ChkProductKind;
        private System.Windows.Forms.CheckBox ChkLoanKind;
        private System.Windows.Forms.CheckBox ChkGuarantee;
        private System.Windows.Forms.CheckBox ChkLending;
        private System.Windows.Forms.CheckBox ChkService;
        private System.Windows.Forms.CheckBox ChkBusinessStatus;
        private System.Windows.Forms.CheckBox ChkCloanStatus;
        private System.Windows.Forms.CheckBox ChkEntregist;
        private System.Windows.Forms.CheckBox ChkStore;
        private System.Windows.Forms.CheckBox ChkRegion;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox ChkDropEntregist;
        private System.Windows.Forms.CheckBox ChkDropTeam;
        private System.Windows.Forms.CheckBox ChkDropRS;
        private System.Windows.Forms.CheckBox ChkDropClearStatus;
        private System.Windows.Forms.CheckBox ChkDropBusinessStatus;
        private System.Windows.Forms.CheckBox ChkDropRSR;
        private System.Windows.Forms.CheckBox ChkDropBank;
        private System.Windows.Forms.CheckBox ChkDropProductKind;
        private System.Windows.Forms.CheckBox ChkDropLoanKind;
        private System.Windows.Forms.CheckBox ChkDropCompany;
        private System.Windows.Forms.Button BtnDropSync;
        private System.Windows.Forms.CheckBox ChkDropBillStatus;
        private System.Windows.Forms.CheckBox ChkDropLawSuit;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox ChkDivisionPermission;
        private System.Windows.Forms.CheckBox ChkStroePermission;
        private System.Windows.Forms.CheckBox ChkRegionPermission;
        private System.Windows.Forms.CheckBox ChkMenuPermission;
        private System.Windows.Forms.Button BtnSyncPermission;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox ChkAccountingCloseBillDay;
        private System.Windows.Forms.CheckBox ChkCloseBillDay;
        private System.Windows.Forms.CheckBox ChkBankAccount;
        private System.Windows.Forms.Button BtnBaseInfo;
        private System.Windows.Forms.CheckBox ChkSubCompany;
        private System.Windows.Forms.CheckBox Chk18Region;
        private System.Windows.Forms.CheckBox Chk18RegionKey;
        private System.Windows.Forms.CheckBox ChkSubCompanyKey;
    }
}


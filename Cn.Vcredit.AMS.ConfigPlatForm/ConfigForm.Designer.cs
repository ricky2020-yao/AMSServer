using Cn.Vcredit.AMS.ConfigPlatForm.Control;
namespace Cn.Vcredit.AMS.ConfigPlatForm
{
    partial class ConfigForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.CbxModule = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtDLLPath = new System.Windows.Forms.TextBox();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.BtnGenerateServiceID = new System.Windows.Forms.Button();
            this.BtnGenerateConfig = new System.Windows.Forms.Button();
            this.DgvServiceConfigs = new System.Windows.Forms.DataGridView();
            this.ColIsDelayLoad = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColModuleType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColServiceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPriority = new Cn.Vcredit.AMS.ConfigPlatForm.Control.DataGridViewNumericUpDownColumn();
            this.ColFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColServiceAssemblyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvServiceConfigs)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(234, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择模块:";
            // 
            // CbxModule
            // 
            this.CbxModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbxModule.FormattingEnabled = true;
            this.CbxModule.Location = new System.Drawing.Point(296, 24);
            this.CbxModule.Name = "CbxModule";
            this.CbxModule.Size = new System.Drawing.Size(184, 20);
            this.CbxModule.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "选择DLL:";
            // 
            // TxtDLLPath
            // 
            this.TxtDLLPath.Location = new System.Drawing.Point(296, 52);
            this.TxtDLLPath.Name = "TxtDLLPath";
            this.TxtDLLPath.Size = new System.Drawing.Size(329, 21);
            this.TxtDLLPath.TabIndex = 3;
            // 
            // BtnSelect
            // 
            this.BtnSelect.Location = new System.Drawing.Point(632, 52);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Size = new System.Drawing.Size(56, 23);
            this.BtnSelect.TabIndex = 4;
            this.BtnSelect.Text = "选择";
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // BtnGenerateServiceID
            // 
            this.BtnGenerateServiceID.Location = new System.Drawing.Point(296, 106);
            this.BtnGenerateServiceID.Name = "BtnGenerateServiceID";
            this.BtnGenerateServiceID.Size = new System.Drawing.Size(131, 23);
            this.BtnGenerateServiceID.TabIndex = 5;
            this.BtnGenerateServiceID.Text = "生成服务标识";
            this.BtnGenerateServiceID.UseVisualStyleBackColor = true;
            this.BtnGenerateServiceID.Click += new System.EventHandler(this.BtnGenerateServiceID_Click);
            // 
            // BtnGenerateConfig
            // 
            this.BtnGenerateConfig.Location = new System.Drawing.Point(466, 106);
            this.BtnGenerateConfig.Name = "BtnGenerateConfig";
            this.BtnGenerateConfig.Size = new System.Drawing.Size(124, 23);
            this.BtnGenerateConfig.TabIndex = 6;
            this.BtnGenerateConfig.Text = "生成配置文件";
            this.BtnGenerateConfig.UseVisualStyleBackColor = true;
            this.BtnGenerateConfig.Click += new System.EventHandler(this.BtnGenerateConfig_Click);
            // 
            // DgvServiceConfigs
            // 
            this.DgvServiceConfigs.AllowUserToAddRows = false;
            this.DgvServiceConfigs.AllowUserToDeleteRows = false;
            this.DgvServiceConfigs.AllowUserToResizeRows = false;
            this.DgvServiceConfigs.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvServiceConfigs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvServiceConfigs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvServiceConfigs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColIsDelayLoad,
            this.ColModuleType,
            this.ColServiceId,
            this.ColDescription,
            this.ColServiceName,
            this.ColPriority,
            this.ColFullName,
            this.ColServiceAssemblyName});
            this.DgvServiceConfigs.Location = new System.Drawing.Point(12, 147);
            this.DgvServiceConfigs.Name = "DgvServiceConfigs";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvServiceConfigs.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DgvServiceConfigs.RowHeadersVisible = false;
            this.DgvServiceConfigs.RowTemplate.Height = 23;
            this.DgvServiceConfigs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvServiceConfigs.Size = new System.Drawing.Size(954, 520);
            this.DgvServiceConfigs.TabIndex = 7;
            this.DgvServiceConfigs.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvServiceConfigs_CellEndEdit);
            // 
            // ColIsDelayLoad
            // 
            this.ColIsDelayLoad.HeaderText = "是否延迟加载";
            this.ColIsDelayLoad.Name = "ColIsDelayLoad";
            // 
            // ColModuleType
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColModuleType.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColModuleType.HeaderText = "模块类型";
            this.ColModuleType.Name = "ColModuleType";
            this.ColModuleType.ReadOnly = true;
            // 
            // ColServiceId
            // 
            this.ColServiceId.HeaderText = "服务ID";
            this.ColServiceId.Name = "ColServiceId";
            this.ColServiceId.Width = 120;
            // 
            // ColDescription
            // 
            this.ColDescription.HeaderText = "描述";
            this.ColDescription.Name = "ColDescription";
            this.ColDescription.Width = 200;
            // 
            // ColServiceName
            // 
            this.ColServiceName.HeaderText = "服务名称";
            this.ColServiceName.Name = "ColServiceName";
            this.ColServiceName.Width = 200;
            // 
            // ColPriority
            // 
            this.ColPriority.HeaderText = "优先级";
            this.ColPriority.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ColPriority.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ColPriority.Name = "ColPriority";
            this.ColPriority.Width = 70;
            // 
            // ColFullName
            // 
            this.ColFullName.HeaderText = "服务全名（包含命名空间）";
            this.ColFullName.Name = "ColFullName";
            this.ColFullName.ReadOnly = true;
            this.ColFullName.Width = 400;
            // 
            // ColServiceAssemblyName
            // 
            this.ColServiceAssemblyName.HeaderText = "服务所在的DLL的程序集名称";
            this.ColServiceAssemblyName.Name = "ColServiceAssemblyName";
            this.ColServiceAssemblyName.ReadOnly = true;
            this.ColServiceAssemblyName.Visible = false;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 679);
            this.Controls.Add(this.DgvServiceConfigs);
            this.Controls.Add(this.BtnGenerateServiceID);
            this.Controls.Add(this.BtnGenerateConfig);
            this.Controls.Add(this.BtnSelect);
            this.Controls.Add(this.TxtDLLPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CbxModule);
            this.Controls.Add(this.label1);
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置平台";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvServiceConfigs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CbxModule;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtDLLPath;
        private System.Windows.Forms.Button BtnSelect;
        private System.Windows.Forms.Button BtnGenerateServiceID;
        private System.Windows.Forms.Button BtnGenerateConfig;
        private System.Windows.Forms.DataGridView DgvServiceConfigs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColIsDelayLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColModuleType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColServiceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColServiceName;
        private DataGridViewNumericUpDownColumn ColPriority;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColServiceAssemblyName;
    }
}


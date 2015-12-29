namespace KongQiang.DevTools.CodeGenerator.Forms
{
    partial class FrmCodeGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gboxDB = new System.Windows.Forms.GroupBox();
            this.pnlDBConfig = new System.Windows.Forms.Panel();
            this.cbxDbType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.btnConnection = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstbxTableNames = new System.Windows.Forms.ListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchText = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.cbxOutput = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlVspsd = new System.Windows.Forms.Panel();
            this.txtVspsdPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlFolder = new System.Windows.Forms.Panel();
            this.txtTemplate = new System.Windows.Forms.TextBox();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlPath = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFolder = new System.Windows.Forms.Button();
            this.lnkViewFile = new System.Windows.Forms.LinkLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.cbxCustom = new System.Windows.Forms.CheckBox();
            this.gboxDB.SuspendLayout();
            this.pnlDBConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlVspsd.SuspendLayout();
            this.pnlFolder.SuspendLayout();
            this.pnlPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // gboxDB
            // 
            this.gboxDB.Controls.Add(this.pnlDBConfig);
            this.gboxDB.Controls.Add(this.btnConnection);
            this.gboxDB.Controls.Add(this.groupBox1);
            this.gboxDB.Location = new System.Drawing.Point(17, 12);
            this.gboxDB.Name = "gboxDB";
            this.gboxDB.Size = new System.Drawing.Size(517, 376);
            this.gboxDB.TabIndex = 1;
            this.gboxDB.TabStop = false;
            this.gboxDB.Text = "数据库配置";
            // 
            // pnlDBConfig
            // 
            this.pnlDBConfig.Controls.Add(this.cbxDbType);
            this.pnlDBConfig.Controls.Add(this.label3);
            this.pnlDBConfig.Controls.Add(this.label25);
            this.pnlDBConfig.Controls.Add(this.label24);
            this.pnlDBConfig.Controls.Add(this.label23);
            this.pnlDBConfig.Controls.Add(this.txtUserName);
            this.pnlDBConfig.Controls.Add(this.label22);
            this.pnlDBConfig.Controls.Add(this.label20);
            this.pnlDBConfig.Controls.Add(this.txtServer);
            this.pnlDBConfig.Controls.Add(this.txtPassword);
            this.pnlDBConfig.Controls.Add(this.txtPort);
            this.pnlDBConfig.Controls.Add(this.txtDatabase);
            this.pnlDBConfig.Location = new System.Drawing.Point(6, 20);
            this.pnlDBConfig.Name = "pnlDBConfig";
            this.pnlDBConfig.Size = new System.Drawing.Size(424, 107);
            this.pnlDBConfig.TabIndex = 0;
            // 
            // cbxDbType
            // 
            this.cbxDbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDbType.FormattingEnabled = true;
            this.cbxDbType.Location = new System.Drawing.Point(75, 12);
            this.cbxDbType.Name = "cbxDbType";
            this.cbxDbType.Size = new System.Drawing.Size(137, 20);
            this.cbxDbType.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "数据库类型";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(5, 46);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(65, 12);
            this.label25.TabIndex = 0;
            this.label25.Text = "服务器地址";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(29, 81);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(41, 12);
            this.label24.TabIndex = 1;
            this.label24.Text = "端口号";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(227, 20);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(41, 12);
            this.label23.TabIndex = 2;
            this.label23.Text = "数据库";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(274, 43);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(137, 21);
            this.txtUserName.TabIndex = 4;
            this.txtUserName.Text = "sa";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(239, 81);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(29, 12);
            this.label22.TabIndex = 3;
            this.label22.Text = "密码";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(227, 46);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 12);
            this.label20.TabIndex = 10;
            this.label20.Text = "用户名";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(76, 43);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(137, 21);
            this.txtServer.TabIndex = 3;
            this.txtServer.Text = "QKONG";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(274, 75);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(137, 21);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.Text = "123456";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(76, 75);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(137, 21);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "可选";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(274, 17);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(137, 21);
            this.txtDatabase.TabIndex = 2;
            this.txtDatabase.Text = "KMRBS";
            // 
            // btnConnection
            // 
            this.btnConnection.Location = new System.Drawing.Point(436, 36);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(75, 76);
            this.btnConnection.TabIndex = 7;
            this.btnConnection.Text = "连接";
            this.btnConnection.UseVisualStyleBackColor = true;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstbxTableNames);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtSearchText);
            this.groupBox1.Location = new System.Drawing.Point(6, 133);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(505, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据表";
            // 
            // lstbxTableNames
            // 
            this.lstbxTableNames.FormattingEnabled = true;
            this.lstbxTableNames.ItemHeight = 12;
            this.lstbxTableNames.Location = new System.Drawing.Point(6, 47);
            this.lstbxTableNames.Name = "lstbxTableNames";
            this.lstbxTableNames.Size = new System.Drawing.Size(489, 184);
            this.lstbxTableNames.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(424, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "查找";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchText
            // 
            this.txtSearchText.Location = new System.Drawing.Point(281, 20);
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.Size = new System.Drawing.Size(137, 21);
            this.txtSearchText.TabIndex = 8;
            this.txtSearchText.Tag = "Ignore";
            this.txtSearchText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchText_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnConfirm);
            this.groupBox2.Controls.Add(this.cbxOutput);
            this.groupBox2.Controls.Add(this.flowLayoutPanel1);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.cbxCustom);
            this.groupBox2.Location = new System.Drawing.Point(17, 394);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(501, 190);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "代码生成配置";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(370, 15);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(81, 25);
            this.btnConfirm.TabIndex = 12;
            this.btnConfirm.Text = "生成代码";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // cbxOutput
            // 
            this.cbxOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOutput.FormattingEnabled = true;
            this.cbxOutput.Location = new System.Drawing.Point(140, 20);
            this.cbxOutput.Name = "cbxOutput";
            this.cbxOutput.Size = new System.Drawing.Size(137, 20);
            this.cbxOutput.TabIndex = 10;
            this.cbxOutput.SelectedIndexChanged += new System.EventHandler(this.cbxOutput_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pnlVspsd);
            this.flowLayoutPanel1.Controls.Add(this.pnlFolder);
            this.flowLayoutPanel1.Controls.Add(this.pnlPath);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(46, 46);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(405, 136);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // pnlVspsd
            // 
            this.pnlVspsd.Controls.Add(this.txtVspsdPath);
            this.pnlVspsd.Controls.Add(this.btnBrowse);
            this.pnlVspsd.Controls.Add(this.label6);
            this.pnlVspsd.Location = new System.Drawing.Point(3, 3);
            this.pnlVspsd.Name = "pnlVspsd";
            this.pnlVspsd.Size = new System.Drawing.Size(394, 30);
            this.pnlVspsd.TabIndex = 0;
            // 
            // txtVspsdPath
            // 
            this.txtVspsdPath.Location = new System.Drawing.Point(91, 4);
            this.txtVspsdPath.Name = "txtVspsdPath";
            this.txtVspsdPath.ReadOnly = true;
            this.txtVspsdPath.Size = new System.Drawing.Size(224, 21);
            this.txtVspsdPath.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(321, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(53, 21);
            this.btnBrowse.TabIndex = 13;
            this.btnBrowse.Text = "浏览";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "vspsd 文件：";
            // 
            // pnlFolder
            // 
            this.pnlFolder.Controls.Add(this.txtTemplate);
            this.pnlFolder.Controls.Add(this.btnOutputPath);
            this.pnlFolder.Controls.Add(this.label11);
            this.pnlFolder.Location = new System.Drawing.Point(3, 39);
            this.pnlFolder.Name = "pnlFolder";
            this.pnlFolder.Size = new System.Drawing.Size(394, 31);
            this.pnlFolder.TabIndex = 0;
            this.pnlFolder.Visible = false;
            // 
            // txtTemplate
            // 
            this.txtTemplate.Location = new System.Drawing.Point(91, 2);
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.ReadOnly = true;
            this.txtTemplate.Size = new System.Drawing.Size(224, 21);
            this.txtTemplate.TabIndex = 0;
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Location = new System.Drawing.Point(321, 3);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(53, 21);
            this.btnOutputPath.TabIndex = 14;
            this.btnOutputPath.Text = "浏览";
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "自定义：";
            // 
            // pnlPath
            // 
            this.pnlPath.Controls.Add(this.label1);
            this.pnlPath.Controls.Add(this.txtOutputPath);
            this.pnlPath.Controls.Add(this.label2);
            this.pnlPath.Controls.Add(this.btnFolder);
            this.pnlPath.Controls.Add(this.lnkViewFile);
            this.pnlPath.Location = new System.Drawing.Point(3, 76);
            this.pnlPath.Name = "pnlPath";
            this.pnlPath.Size = new System.Drawing.Size(394, 47);
            this.pnlPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(89, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "提示：不支持插入代码";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(91, 4);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.ReadOnly = true;
            this.txtOutputPath.Size = new System.Drawing.Size(224, 21);
            this.txtOutputPath.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "输出路径：";
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(321, 5);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(53, 21);
            this.btnFolder.TabIndex = 15;
            this.btnFolder.Text = "浏览";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // lnkViewFile
            // 
            this.lnkViewFile.AutoSize = true;
            this.lnkViewFile.Location = new System.Drawing.Point(297, 32);
            this.lnkViewFile.Name = "lnkViewFile";
            this.lnkViewFile.Size = new System.Drawing.Size(77, 12);
            this.lnkViewFile.TabIndex = 16;
            this.lnkViewFile.TabStop = true;
            this.lnkViewFile.Text = "查看生成文件";
            this.lnkViewFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewFile_LinkClicked);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(47, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "代码输出方式：";
            // 
            // cbxCustom
            // 
            this.cbxCustom.AutoSize = true;
            this.cbxCustom.Location = new System.Drawing.Point(283, 22);
            this.cbxCustom.Name = "cbxCustom";
            this.cbxCustom.Size = new System.Drawing.Size(84, 16);
            this.cbxCustom.TabIndex = 11;
            this.cbxCustom.Text = "自定义模板";
            this.cbxCustom.UseVisualStyleBackColor = true;
            this.cbxCustom.CheckedChanged += new System.EventHandler(this.cbxCustom_CheckedChanged);
            // 
            // FrmCodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 596);
            this.Controls.Add(this.gboxDB);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCodeGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCodeGenerator_FormClosing);
            this.gboxDB.ResumeLayout(false);
            this.pnlDBConfig.ResumeLayout(false);
            this.pnlDBConfig.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlVspsd.ResumeLayout(false);
            this.pnlVspsd.PerformLayout();
            this.pnlFolder.ResumeLayout(false);
            this.pnlFolder.PerformLayout();
            this.pnlPath.ResumeLayout(false);
            this.pnlPath.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gboxDB;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtVspsdPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.CheckBox cbxCustom;
        private System.Windows.Forms.TextBox txtTemplate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnOutputPath;
        private System.Windows.Forms.LinkLabel lnkViewFile;
        private System.Windows.Forms.Panel pnlPath;
        private System.Windows.Forms.ComboBox cbxOutput;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel pnlFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Panel pnlVspsd;
        private System.Windows.Forms.ListBox lstbxTableNames;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearchText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxDbType;
        private System.Windows.Forms.Button btnConnection;
        private System.Windows.Forms.Panel pnlDBConfig;
    }
}
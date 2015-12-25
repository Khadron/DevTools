using System;
using System.Data;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using KongQiang.DevTools.Environments;
using KongQiang.DevTools.Models.DB;
using KongQiang.DevTools.Utils;
using KongQiang.DevTools.Utils.Helper;

namespace KongQiang.DevTools.CodeGenerator.Forms
{
    public partial class FrmCodeGenerator : Form
    {
        private readonly DTE2 _dte;
        private bool _isExecuted;
        private bool _hasPath = false;
        private const string IgnoreStr = "Ignore";
        private CodeConfiguration _codeConfiguration;
        private DbManager _manager;

        public FrmCodeGenerator(DTE dte)
        {
            _dte = (DTE2)dte;
            InitializeComponent();
            ControlInit();
        }

        #region Process Event

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!ValidteControl(this))
            {
                MessageBox.Show(Resources.InputIsEmptyErrMsg);
                return;
            }
            _codeConfiguration = CreateCodeConfiguration();
            var ntc = new TemplateContext(_codeConfiguration, _manager);
            ntc.GenerateCode();
            if (ntc.HasError)
            {
                MessageBox.Show(ntc.Message);
            }
            else
            {
                MessageBox.Show(Resources.SuccessMsg);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = Resources.OpenFileDialogAlert;
            ofd.ReadOnlyChecked = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtPtcPath.Text = ofd.FileName;
            }
        }

        private void txt_LostFocus(object sender, EventArgs e)
        {
            //
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            var sfd = new FolderBrowserDialog { Description = "输出目录" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.txtOutputPath.Text = sfd.SelectedPath;
            }
        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog { Description = "选择一个含有ptc文件和模板的文件夹", ShowNewFolderButton = true };
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtTemplate.Text = fbd.SelectedPath;
            }
        }

        private void cbxOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = this.cbxOutput.SelectedItem.ToString();

            if (item == OutPutMode.Solution.ToString())
            {
                this.pnlPath.Hide();
                this.txtOutputPath.Text = "";
                _hasPath = false;
            }
            else if (item == OutPutMode.SpecifiedPath.ToString())
            {
                this.pnlPath.Show();
                _hasPath = true;
            }
        }

        private void lnkViewFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", this.txtOutputPath.Text);
        }

        private void FrmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_codeConfiguration != null)
            {
                ConfigHelper.GetInstance().ModifySection("DbConfigSection", _codeConfiguration.DbConfiguration);
                ConfigHelper.GetInstance().ModifySection("GenerateConfiguration", _codeConfiguration.GenerateConfiguration);
            }

            this.Dispose(true);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchKey(txtSearchText.Text);
        }

        private void txtSearchText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SearchKey(txtSearchText.Text);
            }
        }

        void lstbxTableNames_GotFocus(object sender, EventArgs e)
        {
            //
        }

        #endregion

        #region Private Method

        private CodeConfiguration CreateCodeConfiguration()
        {
            string tableName = this.lstbxTableNames.SelectedItem.ToString();
            var configuration = new CodeConfiguration()
            {
                Dte = _dte,
                IsCustomTemplete = this.cbxCustom.Checked,
                HasOutpath = _hasPath,
                DbConfiguration = new DbConfiguration()
                {
                    Server = this.txtServer.Text,
                    Port = this.txtPort.Text,
                    DbName = this.txtDatabase.Text,
                    UserName = this.txtUserName.Text,
                    Password = this.txtPassword.Text,
                    TableName = tableName,
                },
                GenerateConfiguration = new GenerateConfiguration()
                {
                    OutputPath = this.txtOutputPath.Text,
                    PtcFilePath = this.txtPtcPath.Text,
                    CustomTempletePath = txtTemplate.Text,
                },
            };
            return configuration;
        }

        private void ControlInit()
        {
            this.txtDatabase.TextChanged += txt_TextChanged;
            this.txtPassword.TextChanged += txt_TextChanged;
            this.txtServer.TextChanged += txt_TextChanged;
            this.txtUserName.TextChanged += txt_TextChanged;
            this.txtPort.TextChanged += txt_TextChanged;

            //数据库类型
            var dbType = Enum.GetNames(typeof(DbProviderType));
            foreach (var type in dbType)
            {
                this.cbxDbType.Items.Add(type);
            }

            //输出方式
            var outputModes = Enum.GetNames(typeof(OutPutMode));
            foreach (var outputMode in outputModes)
            {
                this.cbxOutput.Items.Add(outputMode);
            }
            cbxOutput.SelectedIndexChanged += cbxOutput_SelectedIndexChanged;
            cbxOutput.SelectedIndex = 0;

            this.lstbxTableNames.Sorted = true;
            this.lstbxTableNames.GotFocus += lstbxTableNames_GotFocus;

            var dbSection = ConfigHelper.GetInstance().GetSection<DbConfiguration>("DbConfigSection");
            this.txtServer.Text = dbSection.Server;
            this.txtUserName.Text = dbSection.UserName;
            this.txtPassword.Text = dbSection.Password;
            this.txtPort.Text = dbSection.Port;
            this.txtDatabase.Text = dbSection.DbName;

            var gSection = ConfigHelper.GetInstance().GetSection<GenerateConfiguration>("GenerateConfiguration");

            this.txtPtcPath.Text = gSection.PtcFilePath;
            this.txtTemplate.Text = gSection.CustomTempletePath;
            this.txtOutputPath.Text = gSection.OutputPath;
        }

        private bool ValidteControl(Control control)
        {

            return ValidteSubControl(control);
        }

        private bool ValidteSubControl(Control parent)
        {
            if (!parent.Visible)
            {
                return true;
            }
            foreach (Control control in parent.Controls)
            {
                if (!control.Visible)
                    return true;

                if (!ValidteSubControl(control))
                    return false;

                if (control is TextBox)
                {
                    var box = control as TextBox;
                    if (box.Tag != null && box.Tag.ToString() == IgnoreStr)
                        return true;

                    if (string.IsNullOrEmpty(box.Text))
                        return false;
                }
                else if (control is ListBox)
                {
                    var bobox = control as ListBox;
                    if (bobox.SelectedItem == null)
                        return false;
                }
            }
            return true;
        }

        #endregion

        private void SearchKey(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                int curIndex = 0;
                foreach (string item in this.lstbxTableNames.Items)
                {
                    if (item.ToUpper().Contains(searchText.Trim().ToUpper()))
                    {
                        this.lstbxTableNames.SelectedIndex = curIndex;
                        break;
                    }
                    curIndex++;
                }
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            var cur = sender as TextBox;
            if (cur != null && !string.IsNullOrWhiteSpace(cur.Text))
            {
                _isExecuted = false;
            }
        }

        private void cbxCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbxCustom.Checked)
            {
                this.pnlPtc.Hide();
                this.pnlFolder.Show();
            }
            else
            {
                this.pnlPtc.Show();
                this.pnlFolder.Hide();
            }
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            if (!ValidteControl(this.pnlDBConfig))
            {
                MessageBox.Show(Resources.DbCfgErrMsg);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.lstbxTableNames.Items.Clear();

                var dbProviderType =
                    (DbProviderType)Enum.Parse(typeof(DbProviderType), this.cbxDbType.SelectedItem.ToString());
                var dbCfg = new DbConfiguration(dbProviderType, this.txtServer.Text,
                    this.txtPort.Text, this.txtDatabase.Text, this.txtUserName.Text, this.txtPassword.Text,
                    this.lstbxTableNames.SelectedItem.ToString(), "");

                _manager = new DbManager(this.txtDatabase.Text, dbCfg.ToConnectionString(), dbProviderType);
                var tableNames = _manager.GetTableNames();
                foreach (var name in tableNames)
                {
                    this.lstbxTableNames.Items.Add(name);
                }

                if (this.lstbxTableNames.Items.Count > 0)
                {
                    this.lstbxTableNames.SelectedIndex = 0;
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}

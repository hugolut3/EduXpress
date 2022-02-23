namespace EduXpress.Settings
{
    partial class userControlEmailSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userControlEmailSettings));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItemProcess = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonPageEmailSettings = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupSMSsettings = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.dataGridViewEmails = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtID = new System.Windows.Forms.TextBox();
            this.chkIsEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsDefault = new DevExpress.XtraEditors.CheckEdit();
            this.cmbTSRequired = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtPort = new DevExpress.XtraEditors.TextEdit();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtRetypePassword = new DevExpress.XtraEditors.TextEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtEmailID = new DevExpress.XtraEditors.TextEdit();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.cmbServerName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtDisplayName = new DevExpress.XtraEditors.TextEdit();
            this.txtSMTPAddress = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl));
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTSRequired.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRetypePassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbServerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDisplayName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSMTPAddress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.btnNew,
            this.btnUpdate,
            this.btnSave,
            this.btnRemove,
            this.barStaticItemProcess,
            this.barStaticItem1});
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.MaxItemId = 8;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageEmailSettings});
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Id = 1;
            this.btnNew.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_add;
            this.btnNew.Name = "btnNew";
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Id = 2;
            this.btnUpdate.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_refresh;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUpdate_ItemClick);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Enabled = false;
            this.btnSave.Id = 3;
            this.btnSave.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.save;
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Enabled = false;
            this.btnRemove.Id = 4;
            this.btnRemove.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_removecircled;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRemove_ItemClick);
            // 
            // barStaticItemProcess
            // 
            resources.ApplyResources(this.barStaticItemProcess, "barStaticItemProcess");
            this.barStaticItemProcess.Id = 5;
            this.barStaticItemProcess.Name = "barStaticItemProcess";
            // 
            // barStaticItem1
            // 
            resources.ApplyResources(this.barStaticItem1, "barStaticItem1");
            this.barStaticItem1.Id = 7;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // ribbonPageEmailSettings
            // 
            this.ribbonPageEmailSettings.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupSMSsettings});
            this.ribbonPageEmailSettings.MergeOrder = 2;
            this.ribbonPageEmailSettings.Name = "ribbonPageEmailSettings";
            resources.ApplyResources(this.ribbonPageEmailSettings, "ribbonPageEmailSettings");
            // 
            // ribbonPageGroupSMSsettings
            // 
            this.ribbonPageGroupSMSsettings.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroupSMSsettings.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupSMSsettings.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupSMSsettings.ItemLinks.Add(this.btnRemove);
            this.ribbonPageGroupSMSsettings.Name = "ribbonPageGroupSMSsettings";
            resources.ApplyResources(this.ribbonPageGroupSMSsettings, "ribbonPageGroupSMSsettings");
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.HideWhenMerging = DevExpress.Utils.DefaultBoolean.False;
            resources.ApplyResources(this.ribbonStatusBar1, "ribbonStatusBar1");
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.dataGridViewEmails);
            this.xtraScrollableControl1.Controls.Add(this.txtID);
            this.xtraScrollableControl1.Controls.Add(this.chkIsEnabled);
            this.xtraScrollableControl1.Controls.Add(this.chkIsDefault);
            this.xtraScrollableControl1.Controls.Add(this.cmbTSRequired);
            this.xtraScrollableControl1.Controls.Add(this.txtPort);
            this.xtraScrollableControl1.Controls.Add(this.Label7);
            this.xtraScrollableControl1.Controls.Add(this.Label6);
            this.xtraScrollableControl1.Controls.Add(this.txtRetypePassword);
            this.xtraScrollableControl1.Controls.Add(this.label8);
            this.xtraScrollableControl1.Controls.Add(this.txtPassword);
            this.xtraScrollableControl1.Controls.Add(this.labelControl2);
            this.xtraScrollableControl1.Controls.Add(this.txtEmailID);
            this.xtraScrollableControl1.Controls.Add(this.Label5);
            this.xtraScrollableControl1.Controls.Add(this.Label4);
            this.xtraScrollableControl1.Controls.Add(this.cmbServerName);
            this.xtraScrollableControl1.Controls.Add(this.txtDisplayName);
            this.xtraScrollableControl1.Controls.Add(this.txtSMTPAddress);
            this.xtraScrollableControl1.Controls.Add(this.labelControl1);
            this.xtraScrollableControl1.Controls.Add(this.label1);
            this.xtraScrollableControl1.Controls.Add(this.Label3);
            this.xtraScrollableControl1.Controls.Add(this.Label2);
            resources.ApplyResources(this.xtraScrollableControl1, "xtraScrollableControl1");
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            // 
            // dataGridViewEmails
            // 
            resources.ApplyResources(this.dataGridViewEmails, "dataGridViewEmails");
            this.dataGridViewEmails.MainView = this.gridView1;
            this.dataGridViewEmails.Name = "dataGridViewEmails";
            this.dataGridViewEmails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.dataGridViewEmails.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewEmails_MouseClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dataGridViewEmails;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView1_CustomColumnDisplayText);
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            // 
            // chkIsEnabled
            // 
            resources.ApplyResources(this.chkIsEnabled, "chkIsEnabled");
            this.chkIsEnabled.Name = "chkIsEnabled";
            this.chkIsEnabled.Properties.Caption = resources.GetString("chkIsEnabled.Properties.Caption");
            // 
            // chkIsDefault
            // 
            resources.ApplyResources(this.chkIsDefault, "chkIsDefault");
            this.chkIsDefault.Name = "chkIsDefault";
            this.chkIsDefault.Properties.Caption = resources.GetString("chkIsDefault.Properties.Caption");
            // 
            // cmbTSRequired
            // 
            resources.ApplyResources(this.cmbTSRequired, "cmbTSRequired");
            this.cmbTSRequired.Name = "cmbTSRequired";
            this.cmbTSRequired.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbTSRequired.Properties.Buttons"))))});
            // 
            // txtPort
            // 
            resources.ApplyResources(this.txtPort, "txtPort");
            this.txtPort.Name = "txtPort";
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
            // 
            // Label7
            // 
            resources.ApplyResources(this.Label7, "Label7");
            this.Label7.Name = "Label7";
            // 
            // Label6
            // 
            resources.ApplyResources(this.Label6, "Label6");
            this.Label6.Name = "Label6";
            // 
            // txtRetypePassword
            // 
            resources.ApplyResources(this.txtRetypePassword, "txtRetypePassword");
            this.txtRetypePassword.Name = "txtRetypePassword";
            this.txtRetypePassword.Properties.PasswordChar = '*';
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // txtEmailID
            // 
            resources.ApplyResources(this.txtEmailID, "txtEmailID");
            this.txtEmailID.Name = "txtEmailID";
            this.txtEmailID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmailID_KeyPress);
            this.txtEmailID.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmailID_Validating);
            // 
            // Label5
            // 
            resources.ApplyResources(this.Label5, "Label5");
            this.Label5.Name = "Label5";
            // 
            // Label4
            // 
            resources.ApplyResources(this.Label4, "Label4");
            this.Label4.Name = "Label4";
            // 
            // cmbServerName
            // 
            resources.ApplyResources(this.cmbServerName, "cmbServerName");
            this.cmbServerName.Name = "cmbServerName";
            this.cmbServerName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbServerName.Properties.Buttons"))))});
            this.cmbServerName.SelectedIndexChanged += new System.EventHandler(this.cmbServerName_SelectedIndexChanged);
            // 
            // txtDisplayName
            // 
            resources.ApplyResources(this.txtDisplayName, "txtDisplayName");
            this.txtDisplayName.Name = "txtDisplayName";
            // 
            // txtSMTPAddress
            // 
            resources.ApplyResources(this.txtSMTPAddress, "txtSMTPAddress");
            this.txtSMTPAddress.Name = "txtSMTPAddress";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Label3
            // 
            resources.ApplyResources(this.Label3, "Label3");
            this.Label3.Name = "Label3";
            // 
            // Label2
            // 
            resources.ApplyResources(this.Label2, "Label2");
            this.Label2.Name = "Label2";
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // userControlEmailSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Name = "userControlEmailSettings";
            this.Load += new System.EventHandler(this.userControlEmailSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTSRequired.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRetypePassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbServerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDisplayName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSMTPAddress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.BarButtonItem btnUpdate;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnRemove;
        private DevExpress.XtraBars.BarStaticItem barStaticItemProcess;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageEmailSettings;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSMSsettings;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraGrid.GridControl dataGridViewEmails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        internal System.Windows.Forms.TextBox txtID;
        private DevExpress.XtraEditors.CheckEdit chkIsEnabled;
        private DevExpress.XtraEditors.CheckEdit chkIsDefault;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTSRequired;
        private DevExpress.XtraEditors.TextEdit txtPort;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        private DevExpress.XtraEditors.TextEdit txtRetypePassword;
        internal System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtEmailID;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        private DevExpress.XtraEditors.ComboBoxEdit cmbServerName;
        private DevExpress.XtraEditors.TextEdit txtDisplayName;
        private DevExpress.XtraEditors.TextEdit txtSMTPAddress;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
    }
}

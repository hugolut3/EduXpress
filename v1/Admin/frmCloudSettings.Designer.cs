namespace EduXpress.Admin
{
    partial class frmCloudSettings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCloudSettings));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            this.groupGoogleDriveSettings = new DevExpress.XtraEditors.GroupControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDirectoryId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.txtAuthenticationFile = new DevExpress.XtraEditors.TextEdit();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmbCloudProvider = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupGoogleDriveSettings)).BeginInit();
            this.groupGoogleDriveSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDirectoryId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthenticationFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCloudProvider.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // groupGoogleDriveSettings
            // 
            this.groupGoogleDriveSettings.AppearanceCaption.Options.UseTextOptions = true;
            this.groupGoogleDriveSettings.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupGoogleDriveSettings.Controls.Add(this.labelControl3);
            this.groupGoogleDriveSettings.Controls.Add(this.labelControl2);
            this.groupGoogleDriveSettings.Controls.Add(this.txtDirectoryId);
            this.groupGoogleDriveSettings.Controls.Add(this.labelControl1);
            this.groupGoogleDriveSettings.Controls.Add(this.txtEmail);
            this.groupGoogleDriveSettings.Controls.Add(this.txtAuthenticationFile);
            this.groupGoogleDriveSettings.Controls.Add(this.btnBrowse);
            resources.ApplyResources(this.groupGoogleDriveSettings, "groupGoogleDriveSettings");
            this.groupGoogleDriveSettings.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupGoogleDriveSettings.Name = "groupGoogleDriveSettings";
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // txtDirectoryId
            // 
            resources.ApplyResources(this.txtDirectoryId, "txtDirectoryId");
            this.txtDirectoryId.Name = "txtDirectoryId";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // txtEmail
            // 
            resources.ApplyResources(this.txtEmail, "txtEmail");
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
            // 
            // txtAuthenticationFile
            // 
            resources.ApplyResources(this.txtAuthenticationFile, "txtAuthenticationFile");
            this.txtAuthenticationFile.Name = "txtAuthenticationFile";
            this.txtAuthenticationFile.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtAuthenticationFile.Properties.Appearance.Font")));
            this.txtAuthenticationFile.Properties.Appearance.Options.UseFont = true;
            this.txtAuthenticationFile.Properties.ReadOnly = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.imageload;
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.save;
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbCloudProvider
            // 
            resources.ApplyResources(this.cmbCloudProvider, "cmbCloudProvider");
            this.cmbCloudProvider.Name = "cmbCloudProvider";
            this.cmbCloudProvider.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbCloudProvider.Properties.Buttons"))))});
            this.cmbCloudProvider.Properties.Items.AddRange(new object[] {
            resources.GetString("cmbCloudProvider.Properties.Items")});
            this.cmbCloudProvider.SelectedIndexChanged += new System.EventHandler(this.cmbCloudProvider_SelectedIndexChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.cmbCloudProvider);
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Name = "groupControl1";
            // 
            // frmCloudSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupGoogleDriveSettings);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.SvgImage = global::EduXpress.Properties.Resources.charttype_splinearea;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCloudSettings";
            ((System.ComponentModel.ISupportInitialize)(this.groupGoogleDriveSettings)).EndInit();
            this.groupGoogleDriveSettings.ResumeLayout(false);
            this.groupGoogleDriveSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDirectoryId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthenticationFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCloudProvider.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.GroupControl groupGoogleDriveSettings;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtAuthenticationFile;
        private DevExpress.XtraEditors.SimpleButton btnBrowse;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtDirectoryId;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCloudProvider;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}
namespace EduXpress.Admin
{
    partial class frmBackupRestore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackupRestore));
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnRestore = new DevExpress.XtraEditors.SimpleButton();
            this.btnBackup = new DevExpress.XtraEditors.SimpleButton();
            this.txtBackupDirectory = new DevExpress.XtraEditors.TextEdit();
            this.btnBrowseBackupFolder = new DevExpress.XtraEditors.SimpleButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupDirectory.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnRestore);
            this.groupControl2.Controls.Add(this.btnBackup);
            this.groupControl2.Controls.Add(this.txtBackupDirectory);
            this.groupControl2.Controls.Add(this.btnBrowseBackupFolder);
            this.groupControl2.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupControl2, "groupControl2");
            this.groupControl2.Name = "groupControl2";
            // 
            // btnRestore
            // 
            this.btnRestore.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.managequeries;
            resources.ApplyResources(this.btnRestore, "btnRestore");
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnBackup
            // 
            resources.ApplyResources(this.btnBackup, "btnBackup");
            this.btnBackup.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.managequeries;
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // txtBackupDirectory
            // 
            resources.ApplyResources(this.txtBackupDirectory, "txtBackupDirectory");
            this.txtBackupDirectory.Name = "txtBackupDirectory";
            this.txtBackupDirectory.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtBackupDirectory.Properties.Appearance.Font")));
            this.txtBackupDirectory.Properties.Appearance.Options.UseFont = true;
            this.txtBackupDirectory.Properties.ReadOnly = true;
            // 
            // btnBrowseBackupFolder
            // 
            this.btnBrowseBackupFolder.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.imageload;
            resources.ApplyResources(this.btnBrowseBackupFolder, "btnBrowseBackupFolder");
            this.btnBrowseBackupFolder.Name = "btnBrowseBackupFolder";
            this.btnBrowseBackupFolder.Click += new System.EventHandler(this.btnBrowseBackupFolder_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // btnClose
            // 
            this.btnClose.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmBackupRestore
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupControl2);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmBackupRestore.IconOptions.Icon")));
            this.IconOptions.SvgImage = global::EduXpress.Properties.Resources.datasource;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBackupRestore";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBackupRestore_FormClosing);
            this.Load += new System.EventHandler(this.frmBackupRestore_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupDirectory.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TextEdit txtBackupDirectory;
        private DevExpress.XtraEditors.SimpleButton btnBrowseBackupFolder;
        private DevExpress.XtraEditors.SimpleButton btnBackup;
        private DevExpress.XtraEditors.SimpleButton btnRestore;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}
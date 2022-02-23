namespace EduXpress.Students
{
    partial class frmDownloadFormCloud
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDownloadFormCloud));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cmbFormName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownload = new DevExpress.XtraEditors.SimpleButton();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            this.cmbFormID = new DevExpress.XtraEditors.ComboBoxEdit();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFormName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFormID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.cmbFormName);
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupControl1.Name = "groupControl1";
            // 
            // cmbFormName
            // 
            resources.ApplyResources(this.cmbFormName, "cmbFormName");
            this.cmbFormName.Name = "cmbFormName";
            this.cmbFormName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbFormName.Properties.Buttons"))))});
            this.cmbFormName.Properties.Items.AddRange(new object[] {
            resources.GetString("cmbFormName.Properties.Items")});
            this.cmbFormName.SelectedIndexChanged += new System.EventHandler(this.cmbFormName_SelectedIndexChanged);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDownload
            // 
            resources.ApplyResources(this.btnDownload, "btnDownload");
            this.btnDownload.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.next;
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // cmbFormID
            // 
            resources.ApplyResources(this.cmbFormID, "cmbFormID");
            this.cmbFormID.Name = "cmbFormID";
            this.cmbFormID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbFormID.Properties.Buttons"))))});
            this.cmbFormID.Properties.Items.AddRange(new object[] {
            resources.GetString("cmbFormID.Properties.Items")});
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmDownloadFormCloud
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbFormID);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.SvgImage = global::EduXpress.Properties.Resources.charttype_splinearea;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDownloadFormCloud";
            this.Load += new System.EventHandler(this.frmDownloadFormCloud_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbFormName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFormID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFormName;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnDownload;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFormID;
        private System.Windows.Forms.Timer timer1;
    }
}
namespace EduXpress.UI
{
    partial class frmRecoveryPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecoveryPassword));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.rdSMS = new System.Windows.Forms.RadioButton();
            this.rdEmail = new System.Windows.Forms.RadioButton();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.groupEmail = new DevExpress.XtraEditors.GroupControl();
            this.btnSendEmail = new DevExpress.XtraEditors.SimpleButton();
            this.groupSMS = new DevExpress.XtraEditors.GroupControl();
            this.btnSendSMS = new DevExpress.XtraEditors.SimpleButton();
            this.txtPhoneNo = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupEmail)).BeginInit();
            this.groupEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupSMS)).BeginInit();
            this.groupSMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhoneNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.rdSMS);
            this.groupControl1.Controls.Add(this.rdEmail);
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Name = "groupControl1";
            // 
            // rdSMS
            // 
            resources.ApplyResources(this.rdSMS, "rdSMS");
            this.rdSMS.Name = "rdSMS";
            this.rdSMS.TabStop = true;
            this.rdSMS.UseVisualStyleBackColor = true;
            this.rdSMS.CheckedChanged += new System.EventHandler(this.rdSMS_CheckedChanged);
            // 
            // rdEmail
            // 
            resources.ApplyResources(this.rdEmail, "rdEmail");
            this.rdEmail.Name = "rdEmail";
            this.rdEmail.TabStop = true;
            this.rdEmail.UseVisualStyleBackColor = true;
            this.rdEmail.CheckedChanged += new System.EventHandler(this.rdEmail_CheckedChanged);
            // 
            // txtEmail
            // 
            resources.ApplyResources(this.txtEmail, "txtEmail");
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
            // 
            // groupEmail
            // 
            this.groupEmail.Controls.Add(this.btnSendEmail);
            this.groupEmail.Controls.Add(this.txtEmail);
            this.groupEmail.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupEmail, "groupEmail");
            this.groupEmail.Name = "groupEmail";
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.forward;
            this.btnSendEmail.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            resources.ApplyResources(this.btnSendEmail, "btnSendEmail");
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // groupSMS
            // 
            this.groupSMS.Controls.Add(this.btnSendSMS);
            this.groupSMS.Controls.Add(this.txtPhoneNo);
            this.groupSMS.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupSMS, "groupSMS");
            this.groupSMS.Name = "groupSMS";
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.electronics_phoneandroid;
            this.btnSendSMS.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            resources.ApplyResources(this.btnSendSMS, "btnSendSMS");
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.Click += new System.EventHandler(this.btnSendSMS_Click);
            // 
            // txtPhoneNo
            // 
            resources.ApplyResources(this.txtPhoneNo, "txtPhoneNo");
            this.txtPhoneNo.Name = "txtPhoneNo";
            this.txtPhoneNo.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("txtPhoneNo.Properties.Mask.BeepOnError")));
            this.txtPhoneNo.Properties.Mask.EditMask = resources.GetString("txtPhoneNo.Properties.Mask.EditMask");
            this.txtPhoneNo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtPhoneNo.Properties.Mask.MaskType")));
            // 
            // btnClose
            // 
            this.btnClose.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            this.btnClose.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmRecoveryPassword
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupSMS);
            this.Controls.Add(this.groupEmail);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRecoveryPassword";
            this.Load += new System.EventHandler(this.frmRecoveryPassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupEmail)).EndInit();
            this.groupEmail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupSMS)).EndInit();
            this.groupSMS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPhoneNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.RadioButton rdSMS;
        private System.Windows.Forms.RadioButton rdEmail;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.GroupControl groupEmail;
        private DevExpress.XtraEditors.SimpleButton btnSendEmail;
        private DevExpress.XtraEditors.GroupControl groupSMS;
        private DevExpress.XtraEditors.SimpleButton btnSendSMS;
        private DevExpress.XtraEditors.TextEdit txtPhoneNo;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}
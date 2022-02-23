namespace EduXpress.UI
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelIntro = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureLogo = new DevExpress.XtraEditors.PictureEdit();
            this.lblLoginMessage = new DevExpress.XtraEditors.LabelControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.linkChangePassword = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.linklblForgetPasswordUser = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.splashScreenManagerWait = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true, true);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelIntro)).BeginInit();
            this.panelIntro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.bo_user;
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelIntro
            // 
            this.panelIntro.Controls.Add(this.labelControl1);
            resources.ApplyResources(this.panelIntro, "panelIntro");
            this.panelIntro.Name = "panelIntro";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // pictureLogo
            // 
            this.pictureLogo.EditValue = global::EduXpress.Properties.Resources.loginImage_graduation_cap;
            resources.ApplyResources(this.pictureLogo, "pictureLogo");
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureLogo.Properties.Appearance.Options.UseBackColor = true;
            this.pictureLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            // 
            // lblLoginMessage
            // 
            this.lblLoginMessage.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblLoginMessage.Appearance.Font")));
            this.lblLoginMessage.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.lblLoginMessage, "lblLoginMessage");
            this.lblLoginMessage.Name = "lblLoginMessage";
            // 
            // lblUserName
            // 
            this.lblUserName.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblUserName.Appearance.Font")));
            this.lblUserName.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.lblUserName, "lblUserName");
            this.lblUserName.Name = "lblUserName";
            // 
            // lblPassword
            // 
            this.lblPassword.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblPassword.Appearance.Font")));
            this.lblPassword.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.Name = "lblPassword";
            // 
            // txtUserName
            // 
            resources.ApplyResources(this.txtUserName, "txtUserName");
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtUserName.Properties.Appearance.Font")));
            this.txtUserName.Properties.Appearance.Options.UseFont = true;
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtPassword.Properties.Appearance.Font")));
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.PasswordChar = '*';
            // 
            // linkChangePassword
            // 
            this.linkChangePassword.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("linkChangePassword.Appearance.Font")));
            this.linkChangePassword.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.linkChangePassword, "linkChangePassword");
            this.linkChangePassword.Name = "linkChangePassword";
            this.linkChangePassword.Click += new System.EventHandler(this.linkChangePassword_Click);
            // 
            // linklblForgetPasswordUser
            // 
            this.linklblForgetPasswordUser.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("linklblForgetPasswordUser.Appearance.Font")));
            this.linklblForgetPasswordUser.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.linklblForgetPasswordUser, "linklblForgetPasswordUser");
            this.linklblForgetPasswordUser.Name = "linklblForgetPasswordUser";
            this.linklblForgetPasswordUser.Click += new System.EventHandler(this.linklblForgetPasswordUser_Click);
            // 
            // splashScreenManagerWait
            // 
            this.splashScreenManagerWait.ClosingDelay = 500;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnLogin;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.lblLoginMessage);
            this.Controls.Add(this.linklblForgetPasswordUser);
            this.Controls.Add(this.linkChangePassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.pictureLogo);
            this.Controls.Add(this.panelIntro);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.ShowIcon = false;
            this.Name = "frmLogin";
            this.Opacity = 0.8D;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.Shown += new System.EventHandler(this.frmLogin_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.panelIntro)).EndInit();
            this.panelIntro.ResumeLayout(false);
            this.panelIntro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelIntro;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureLogo;
        private DevExpress.XtraEditors.LabelControl lblLoginMessage;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.LabelControl lblPassword;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.HyperlinkLabelControl linkChangePassword;
        private DevExpress.XtraEditors.HyperlinkLabelControl linklblForgetPasswordUser;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManagerWait;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.TextEdit txtUserName;
    }
}
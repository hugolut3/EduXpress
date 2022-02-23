namespace EduXpress.Settings
{
    partial class userControlPreferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userControlPreferences));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPagePreferences = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupPreferences = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.btnResetPreferencesSettings = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoadPreferences = new DevExpress.XtraEditors.SimpleButton();
            this.groupCurrencySymbol = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.radioCurrencySymbolAfter = new System.Windows.Forms.RadioButton();
            this.radioCurrencySymbolBefore = new System.Windows.Forms.RadioButton();
            this.txtCurrencySymbol = new DevExpress.XtraEditors.TextEdit();
            this.groupPhotoDirectory = new DevExpress.XtraEditors.GroupControl();
            this.txtStudentsPhotosDirectory = new DevExpress.XtraEditors.TextEdit();
            this.btnBrowseImage = new DevExpress.XtraEditors.SimpleButton();
            this.groupTheme = new DevExpress.XtraEditors.GroupControl();
            this.cmbSkins = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupLanguage = new DevExpress.XtraEditors.GroupControl();
            this.comboLanguage = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtID = new System.Windows.Forms.TextBox();
            this.groupErrorLogging = new DevExpress.XtraEditors.GroupControl();
            this.radioNoLogErrors = new System.Windows.Forms.RadioButton();
            this.radioLogErrors = new System.Windows.Forms.RadioButton();
            this.groupInvoice = new System.Windows.Forms.GroupBox();
            this.rdSendSMSEmailInvoice = new System.Windows.Forms.RadioButton();
            this.rdSendEmailInvoice = new System.Windows.Forms.RadioButton();
            this.rdSendSMSInvoice = new System.Windows.Forms.RadioButton();
            this.rdNoNotificationInvoice = new System.Windows.Forms.RadioButton();
            this.groupRegUpDelUsers = new System.Windows.Forms.GroupBox();
            this.rdSendSMSEmailUsers = new System.Windows.Forms.RadioButton();
            this.rdSendEmailUsers = new System.Windows.Forms.RadioButton();
            this.rdSendSMSUsers = new System.Windows.Forms.RadioButton();
            this.rdNoNotificationUsers = new System.Windows.Forms.RadioButton();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl));
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupCurrencySymbol)).BeginInit();
            this.groupCurrencySymbol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencySymbol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupPhotoDirectory)).BeginInit();
            this.groupPhotoDirectory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStudentsPhotosDirectory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTheme)).BeginInit();
            this.groupTheme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSkins.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupLanguage)).BeginInit();
            this.groupLanguage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboLanguage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupErrorLogging)).BeginInit();
            this.groupErrorLogging.SuspendLayout();
            this.groupInvoice.SuspendLayout();
            this.groupRegUpDelUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.ExpandCollapseItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("ribbonControl1.ExpandCollapseItem.ImageOptions.ImageIndex")));
            this.ribbonControl1.ExpandCollapseItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("ribbonControl1.ExpandCollapseItem.ImageOptions.LargeImageIndex")));
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.btnSave});
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.MaxItemId = 3;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPagePreferences});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Id = 1;
            this.btnSave.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnSave.ImageOptions.ImageIndex")));
            this.btnSave.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnSave.ImageOptions.LargeImageIndex")));
            this.btnSave.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.save;
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // ribbonPagePreferences
            // 
            this.ribbonPagePreferences.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupPreferences});
            this.ribbonPagePreferences.MergeOrder = 2;
            this.ribbonPagePreferences.Name = "ribbonPagePreferences";
            resources.ApplyResources(this.ribbonPagePreferences, "ribbonPagePreferences");
            // 
            // ribbonPageGroupPreferences
            // 
            this.ribbonPageGroupPreferences.AllowTextClipping = false;
            this.ribbonPageGroupPreferences.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupPreferences.Name = "ribbonPageGroupPreferences";
            resources.ApplyResources(this.ribbonPageGroupPreferences, "ribbonPageGroupPreferences");
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
            this.xtraScrollableControl1.Controls.Add(this.btnResetPreferencesSettings);
            this.xtraScrollableControl1.Controls.Add(this.btnLoadPreferences);
            this.xtraScrollableControl1.Controls.Add(this.groupCurrencySymbol);
            this.xtraScrollableControl1.Controls.Add(this.groupPhotoDirectory);
            this.xtraScrollableControl1.Controls.Add(this.groupTheme);
            this.xtraScrollableControl1.Controls.Add(this.groupLanguage);
            this.xtraScrollableControl1.Controls.Add(this.txtID);
            this.xtraScrollableControl1.Controls.Add(this.groupErrorLogging);
            this.xtraScrollableControl1.Controls.Add(this.groupInvoice);
            this.xtraScrollableControl1.Controls.Add(this.groupRegUpDelUsers);
            resources.ApplyResources(this.xtraScrollableControl1, "xtraScrollableControl1");
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            // 
            // btnResetPreferencesSettings
            // 
            this.btnResetPreferencesSettings.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.resetlayoutoptions;
            resources.ApplyResources(this.btnResetPreferencesSettings, "btnResetPreferencesSettings");
            this.btnResetPreferencesSettings.Name = "btnResetPreferencesSettings";
            this.btnResetPreferencesSettings.Click += new System.EventHandler(this.btnResetPreferencesSettings_Click);
            // 
            // btnLoadPreferences
            // 
            this.btnLoadPreferences.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_refresh;
            resources.ApplyResources(this.btnLoadPreferences, "btnLoadPreferences");
            this.btnLoadPreferences.Name = "btnLoadPreferences";
            this.btnLoadPreferences.Click += new System.EventHandler(this.btnLoadPreferences_Click);
            // 
            // groupCurrencySymbol
            // 
            this.groupCurrencySymbol.Controls.Add(this.labelControl1);
            this.groupCurrencySymbol.Controls.Add(this.radioCurrencySymbolAfter);
            this.groupCurrencySymbol.Controls.Add(this.radioCurrencySymbolBefore);
            this.groupCurrencySymbol.Controls.Add(this.txtCurrencySymbol);
            this.groupCurrencySymbol.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupCurrencySymbol, "groupCurrencySymbol");
            this.groupCurrencySymbol.Name = "groupCurrencySymbol";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // radioCurrencySymbolAfter
            // 
            resources.ApplyResources(this.radioCurrencySymbolAfter, "radioCurrencySymbolAfter");
            this.radioCurrencySymbolAfter.Name = "radioCurrencySymbolAfter";
            this.radioCurrencySymbolAfter.TabStop = true;
            this.radioCurrencySymbolAfter.UseVisualStyleBackColor = true;
            this.radioCurrencySymbolAfter.CheckedChanged += new System.EventHandler(this.radioCurrencySymbolAfter_CheckedChanged);
            // 
            // radioCurrencySymbolBefore
            // 
            resources.ApplyResources(this.radioCurrencySymbolBefore, "radioCurrencySymbolBefore");
            this.radioCurrencySymbolBefore.Name = "radioCurrencySymbolBefore";
            this.radioCurrencySymbolBefore.TabStop = true;
            this.radioCurrencySymbolBefore.UseVisualStyleBackColor = true;
            this.radioCurrencySymbolBefore.CheckedChanged += new System.EventHandler(this.radioCurrencySymbolBefore_CheckedChanged);
            // 
            // txtCurrencySymbol
            // 
            resources.ApplyResources(this.txtCurrencySymbol, "txtCurrencySymbol");
            this.txtCurrencySymbol.Name = "txtCurrencySymbol";
            this.txtCurrencySymbol.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtCurrencySymbol.Properties.Appearance.Font")));
            this.txtCurrencySymbol.Properties.Appearance.Options.UseFont = true;
            this.txtCurrencySymbol.TextChanged += new System.EventHandler(this.txtCurrencySymbol_TextChanged);
            // 
            // groupPhotoDirectory
            // 
            this.groupPhotoDirectory.Controls.Add(this.txtStudentsPhotosDirectory);
            this.groupPhotoDirectory.Controls.Add(this.btnBrowseImage);
            this.groupPhotoDirectory.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupPhotoDirectory, "groupPhotoDirectory");
            this.groupPhotoDirectory.Name = "groupPhotoDirectory";
            // 
            // txtStudentsPhotosDirectory
            // 
            resources.ApplyResources(this.txtStudentsPhotosDirectory, "txtStudentsPhotosDirectory");
            this.txtStudentsPhotosDirectory.Name = "txtStudentsPhotosDirectory";
            this.txtStudentsPhotosDirectory.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtStudentsPhotosDirectory.Properties.Appearance.Font")));
            this.txtStudentsPhotosDirectory.Properties.Appearance.Options.UseFont = true;
            this.txtStudentsPhotosDirectory.Properties.ReadOnly = true;
            // 
            // btnBrowseImage
            // 
            this.btnBrowseImage.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.imageload;
            resources.ApplyResources(this.btnBrowseImage, "btnBrowseImage");
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Click += new System.EventHandler(this.btnBrowseImage_Click);
            // 
            // groupTheme
            // 
            this.groupTheme.Controls.Add(this.cmbSkins);
            this.groupTheme.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupTheme, "groupTheme");
            this.groupTheme.Name = "groupTheme";
            // 
            // cmbSkins
            // 
            resources.ApplyResources(this.cmbSkins, "cmbSkins");
            this.cmbSkins.Name = "cmbSkins";
            this.cmbSkins.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("cmbSkins.Properties.Appearance.Font")));
            this.cmbSkins.Properties.Appearance.Options.UseFont = true;
            this.cmbSkins.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbSkins.Properties.Buttons"))))});
            this.cmbSkins.SelectedIndexChanged += new System.EventHandler(this.cmbSkins_SelectedIndexChanged);
            // 
            // groupLanguage
            // 
            this.groupLanguage.Controls.Add(this.comboLanguage);
            this.groupLanguage.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupLanguage, "groupLanguage");
            this.groupLanguage.Name = "groupLanguage";
            // 
            // comboLanguage
            // 
            resources.ApplyResources(this.comboLanguage, "comboLanguage");
            this.comboLanguage.Name = "comboLanguage";
            this.comboLanguage.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("comboLanguage.Properties.Appearance.Font")));
            this.comboLanguage.Properties.Appearance.Options.UseFont = true;
            this.comboLanguage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboLanguage.Properties.Buttons"))))});
            this.comboLanguage.SelectedIndexChanged += new System.EventHandler(this.comboLanguage_SelectedIndexChanged);
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            // 
            // groupErrorLogging
            // 
            this.groupErrorLogging.Controls.Add(this.radioNoLogErrors);
            this.groupErrorLogging.Controls.Add(this.radioLogErrors);
            this.groupErrorLogging.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupErrorLogging, "groupErrorLogging");
            this.groupErrorLogging.Name = "groupErrorLogging";
            // 
            // radioNoLogErrors
            // 
            resources.ApplyResources(this.radioNoLogErrors, "radioNoLogErrors");
            this.radioNoLogErrors.Name = "radioNoLogErrors";
            this.radioNoLogErrors.TabStop = true;
            this.radioNoLogErrors.UseVisualStyleBackColor = true;
            this.radioNoLogErrors.CheckedChanged += new System.EventHandler(this.radioNoLogErrors_CheckedChanged);
            // 
            // radioLogErrors
            // 
            resources.ApplyResources(this.radioLogErrors, "radioLogErrors");
            this.radioLogErrors.Name = "radioLogErrors";
            this.radioLogErrors.TabStop = true;
            this.radioLogErrors.UseVisualStyleBackColor = true;
            this.radioLogErrors.CheckedChanged += new System.EventHandler(this.radioLogErrors_CheckedChanged);
            // 
            // groupInvoice
            // 
            this.groupInvoice.Controls.Add(this.rdSendSMSEmailInvoice);
            this.groupInvoice.Controls.Add(this.rdSendEmailInvoice);
            this.groupInvoice.Controls.Add(this.rdSendSMSInvoice);
            this.groupInvoice.Controls.Add(this.rdNoNotificationInvoice);
            resources.ApplyResources(this.groupInvoice, "groupInvoice");
            this.groupInvoice.Name = "groupInvoice";
            this.groupInvoice.TabStop = false;
            // 
            // rdSendSMSEmailInvoice
            // 
            resources.ApplyResources(this.rdSendSMSEmailInvoice, "rdSendSMSEmailInvoice");
            this.rdSendSMSEmailInvoice.Name = "rdSendSMSEmailInvoice";
            this.rdSendSMSEmailInvoice.TabStop = true;
            this.rdSendSMSEmailInvoice.UseVisualStyleBackColor = true;
            this.rdSendSMSEmailInvoice.CheckedChanged += new System.EventHandler(this.rdSendSMSEmailInvoice_CheckedChanged);
            // 
            // rdSendEmailInvoice
            // 
            resources.ApplyResources(this.rdSendEmailInvoice, "rdSendEmailInvoice");
            this.rdSendEmailInvoice.Name = "rdSendEmailInvoice";
            this.rdSendEmailInvoice.TabStop = true;
            this.rdSendEmailInvoice.UseVisualStyleBackColor = true;
            this.rdSendEmailInvoice.CheckedChanged += new System.EventHandler(this.rdSendEmailInvoice_CheckedChanged);
            // 
            // rdSendSMSInvoice
            // 
            resources.ApplyResources(this.rdSendSMSInvoice, "rdSendSMSInvoice");
            this.rdSendSMSInvoice.Name = "rdSendSMSInvoice";
            this.rdSendSMSInvoice.TabStop = true;
            this.rdSendSMSInvoice.UseVisualStyleBackColor = true;
            this.rdSendSMSInvoice.CheckedChanged += new System.EventHandler(this.rdSendSMSInvoice_CheckedChanged);
            // 
            // rdNoNotificationInvoice
            // 
            resources.ApplyResources(this.rdNoNotificationInvoice, "rdNoNotificationInvoice");
            this.rdNoNotificationInvoice.Name = "rdNoNotificationInvoice";
            this.rdNoNotificationInvoice.TabStop = true;
            this.rdNoNotificationInvoice.UseVisualStyleBackColor = true;
            this.rdNoNotificationInvoice.CheckedChanged += new System.EventHandler(this.rdNoNotificationInvoice_CheckedChanged);
            // 
            // groupRegUpDelUsers
            // 
            this.groupRegUpDelUsers.Controls.Add(this.rdSendSMSEmailUsers);
            this.groupRegUpDelUsers.Controls.Add(this.rdSendEmailUsers);
            this.groupRegUpDelUsers.Controls.Add(this.rdSendSMSUsers);
            this.groupRegUpDelUsers.Controls.Add(this.rdNoNotificationUsers);
            resources.ApplyResources(this.groupRegUpDelUsers, "groupRegUpDelUsers");
            this.groupRegUpDelUsers.Name = "groupRegUpDelUsers";
            this.groupRegUpDelUsers.TabStop = false;
            // 
            // rdSendSMSEmailUsers
            // 
            resources.ApplyResources(this.rdSendSMSEmailUsers, "rdSendSMSEmailUsers");
            this.rdSendSMSEmailUsers.Name = "rdSendSMSEmailUsers";
            this.rdSendSMSEmailUsers.TabStop = true;
            this.rdSendSMSEmailUsers.UseVisualStyleBackColor = true;
            this.rdSendSMSEmailUsers.CheckedChanged += new System.EventHandler(this.rdSendSMSEmailUsers_CheckedChanged);
            // 
            // rdSendEmailUsers
            // 
            resources.ApplyResources(this.rdSendEmailUsers, "rdSendEmailUsers");
            this.rdSendEmailUsers.Name = "rdSendEmailUsers";
            this.rdSendEmailUsers.TabStop = true;
            this.rdSendEmailUsers.UseVisualStyleBackColor = true;
            this.rdSendEmailUsers.CheckedChanged += new System.EventHandler(this.rdSendEmailUsers_CheckedChanged);
            // 
            // rdSendSMSUsers
            // 
            resources.ApplyResources(this.rdSendSMSUsers, "rdSendSMSUsers");
            this.rdSendSMSUsers.Name = "rdSendSMSUsers";
            this.rdSendSMSUsers.TabStop = true;
            this.rdSendSMSUsers.UseVisualStyleBackColor = true;
            this.rdSendSMSUsers.CheckedChanged += new System.EventHandler(this.rdSendSMSUsers_CheckedChanged);
            // 
            // rdNoNotificationUsers
            // 
            resources.ApplyResources(this.rdNoNotificationUsers, "rdNoNotificationUsers");
            this.rdNoNotificationUsers.Name = "rdNoNotificationUsers";
            this.rdNoNotificationUsers.TabStop = true;
            this.rdNoNotificationUsers.UseVisualStyleBackColor = true;
            this.rdNoNotificationUsers.CheckedChanged += new System.EventHandler(this.rdNoNotificationUsers_CheckedChanged);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // userControlPreferences
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "userControlPreferences";
            this.VisibleChanged += new System.EventHandler(this.userControlPreferences_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupCurrencySymbol)).EndInit();
            this.groupCurrencySymbol.ResumeLayout(false);
            this.groupCurrencySymbol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencySymbol.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupPhotoDirectory)).EndInit();
            this.groupPhotoDirectory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtStudentsPhotosDirectory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTheme)).EndInit();
            this.groupTheme.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbSkins.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupLanguage)).EndInit();
            this.groupLanguage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboLanguage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupErrorLogging)).EndInit();
            this.groupErrorLogging.ResumeLayout(false);
            this.groupErrorLogging.PerformLayout();
            this.groupInvoice.ResumeLayout(false);
            this.groupInvoice.PerformLayout();
            this.groupRegUpDelUsers.ResumeLayout(false);
            this.groupRegUpDelUsers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPagePreferences;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupPreferences;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private System.Windows.Forms.GroupBox groupRegUpDelUsers;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSkins;
        private System.Windows.Forms.GroupBox groupInvoice;
        private System.Windows.Forms.RadioButton rdSendEmailInvoice;
        private System.Windows.Forms.RadioButton rdSendSMSInvoice;
        private System.Windows.Forms.RadioButton rdNoNotificationInvoice;
        private System.Windows.Forms.RadioButton rdSendEmailUsers;
        private System.Windows.Forms.RadioButton rdSendSMSUsers;
        private System.Windows.Forms.RadioButton rdNoNotificationUsers;
        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraEditors.TextEdit txtStudentsPhotosDirectory;
        private DevExpress.XtraEditors.SimpleButton btnBrowseImage;
        private DevExpress.XtraEditors.ComboBoxEdit comboLanguage;
        private System.Windows.Forms.RadioButton rdSendSMSEmailUsers;
        private System.Windows.Forms.RadioButton rdSendSMSEmailInvoice;
        private DevExpress.XtraEditors.GroupControl groupErrorLogging;
        private System.Windows.Forms.RadioButton radioNoLogErrors;
        private System.Windows.Forms.RadioButton radioLogErrors;
        private System.Windows.Forms.TextBox txtID;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.GroupControl groupLanguage;
        private DevExpress.XtraEditors.GroupControl groupCurrencySymbol;
        private DevExpress.XtraEditors.GroupControl groupPhotoDirectory;
        private DevExpress.XtraEditors.GroupControl groupTheme;
        private DevExpress.XtraEditors.TextEdit txtCurrencySymbol;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.RadioButton radioCurrencySymbolAfter;
        private System.Windows.Forms.RadioButton radioCurrencySymbolBefore;
        private DevExpress.XtraEditors.SimpleButton btnLoadPreferences;
        private DevExpress.XtraEditors.SimpleButton btnResetPreferencesSettings;
    }
}

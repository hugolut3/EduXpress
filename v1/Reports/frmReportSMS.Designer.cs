namespace EduXpress.Reports
{
    partial class frmReportSMS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportSMS));
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.groupSMSTemplates = new DevExpress.XtraEditors.GroupControl();
            this.memoTemplateMessage = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.comboTemplateName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.memoMessage = new DevExpress.XtraEditors.MemoEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.phoneNumberLabel = new System.Windows.Forms.Label();
            this.txtPhoneNo = new DevExpress.XtraEditors.TextEdit();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.comboMessageID = new DevExpress.XtraEditors.ComboBoxEdit();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            this.btnUnsupportedCharacters = new DevExpress.XtraEditors.SimpleButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.splashScreenManager2 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm2), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.groupSMSTemplates)).BeginInit();
            this.groupSMSTemplates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoTemplateMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboTemplateName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhoneNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboMessageID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            this.btnClose.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupSMSTemplates
            // 
            resources.ApplyResources(this.groupSMSTemplates, "groupSMSTemplates");
            this.groupSMSTemplates.Controls.Add(this.memoTemplateMessage);
            this.groupSMSTemplates.Controls.Add(this.labelControl3);
            this.groupSMSTemplates.Controls.Add(this.comboTemplateName);
            this.groupSMSTemplates.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupSMSTemplates.Name = "groupSMSTemplates";
            // 
            // memoTemplateMessage
            // 
            resources.ApplyResources(this.memoTemplateMessage, "memoTemplateMessage");
            this.memoTemplateMessage.Name = "memoTemplateMessage";
            this.memoTemplateMessage.Properties.ReadOnly = true;
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
            // 
            // comboTemplateName
            // 
            resources.ApplyResources(this.comboTemplateName, "comboTemplateName");
            this.comboTemplateName.Name = "comboTemplateName";
            this.comboTemplateName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboTemplateName.Properties.Buttons"))))});
            this.comboTemplateName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboTemplateName.SelectedIndexChanged += new System.EventHandler(this.comboTemplateName_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Name = "label3";
            // 
            // lblCount
            // 
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            // 
            // memoMessage
            // 
            resources.ApplyResources(this.memoMessage, "memoMessage");
            this.memoMessage.Name = "memoMessage";
            this.memoMessage.Properties.AdvancedModeOptions.AllowCaretAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.memoMessage.Properties.AdvancedModeOptions.AllowSelectionAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.memoMessage.Properties.AdvancedModeOptions.Label = resources.GetString("memoMessage.Properties.AdvancedModeOptions.Label");
            this.memoMessage.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            this.memoMessage.EditValueChanged += new System.EventHandler(this.memoMessage_EditValueChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Name = "label10";
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.AllowHtmlString = true;
            this.labelControl2.Name = "labelControl2";
            // 
            // phoneNumberLabel
            // 
            resources.ApplyResources(this.phoneNumberLabel, "phoneNumberLabel");
            this.phoneNumberLabel.Name = "phoneNumberLabel";
            // 
            // txtPhoneNo
            // 
            resources.ApplyResources(this.txtPhoneNo, "txtPhoneNo");
            this.txtPhoneNo.Name = "txtPhoneNo";
            this.txtPhoneNo.Properties.AdvancedModeOptions.AllowCaretAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.txtPhoneNo.Properties.AdvancedModeOptions.AllowSelectionAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.txtPhoneNo.Properties.AdvancedModeOptions.Label = resources.GetString("txtPhoneNo.Properties.AdvancedModeOptions.Label");
            this.txtPhoneNo.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("txtPhoneNo.Properties.Mask.BeepOnError")));
            this.txtPhoneNo.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegExpMaskManager));
            this.txtPhoneNo.Properties.MaskSettings.Set("mask", "(\\d{7,},)*(\\d{7,})?");
            this.txtPhoneNo.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnSend
            // 
            resources.ApplyResources(this.btnSend, "btnSend");
            this.btnSend.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.electronics_phoneandroid;
            this.btnSend.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnSend.Name = "btnSend";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.clearall;
            this.btnClear.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnClear.Name = "btnClear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // comboMessageID
            // 
            resources.ApplyResources(this.comboMessageID, "comboMessageID");
            this.comboMessageID.Name = "comboMessageID";
            this.comboMessageID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboMessageID.Properties.Buttons"))))});
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // btnUnsupportedCharacters
            // 
            resources.ApplyResources(this.btnUnsupportedCharacters, "btnUnsupportedCharacters");
            this.btnUnsupportedCharacters.Appearance.Options.UseTextOptions = true;
            this.btnUnsupportedCharacters.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnUnsupportedCharacters.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.deletecomment;
            this.btnUnsupportedCharacters.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnUnsupportedCharacters.Name = "btnUnsupportedCharacters";
            this.btnUnsupportedCharacters.Click += new System.EventHandler(this.btnUnsupportedCharacters_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // splashScreenManager2
            // 
            this.splashScreenManager2.ClosingDelay = 500;
            // 
            // frmReportSMS
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUnsupportedCharacters);
            this.Controls.Add(this.comboMessageID);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupSMSTemplates);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.memoMessage);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.phoneNumberLabel);
            this.Controls.Add(this.txtPhoneNo);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.SvgImage = global::EduXpress.Properties.Resources.electronics_phoneandroid;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReportSMS";
            this.Load += new System.EventHandler(this.frmReportSMS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupSMSTemplates)).EndInit();
            this.groupSMSTemplates.ResumeLayout(false);
            this.groupSMSTemplates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoTemplateMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboTemplateName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhoneNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboMessageID.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.GroupControl groupSMSTemplates;
        private DevExpress.XtraEditors.MemoEdit memoTemplateMessage;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit comboTemplateName;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraEditors.MemoEdit memoMessage;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal System.Windows.Forms.Label phoneNumberLabel;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.ComboBoxEdit comboMessageID;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.TextEdit txtPhoneNo;
        private DevExpress.XtraEditors.SimpleButton btnUnsupportedCharacters;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager2;
    }
}
namespace EduXpress.Students
{
    partial class frmExchangeRates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExchangeRates));
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            this.btnConvert = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtAmountConvert = new DevExpress.XtraEditors.TextEdit();
            this.pictureCurrencyBase = new DevExpress.XtraEditors.PictureEdit();
            this.lblCurrencyBaseName = new DevExpress.XtraEditors.LabelControl();
            this.lblConversion = new DevExpress.XtraEditors.LabelControl();
            this.lblCurrency = new DevExpress.XtraEditors.LabelControl();
            this.pictureCurrency = new DevExpress.XtraEditors.PictureEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblLastUpdated = new DevExpress.XtraEditors.LabelControl();
            this.btnUpdateRates = new DevExpress.XtraEditors.SimpleButton();
            this.cmbSelectCurrency = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtDisplay = new DevExpress.XtraEditors.TextEdit();
            this.cmbCurrencyFrom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbCurrencyTo = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmountConvert.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCurrencyBase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSelectCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDisplay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyTo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // svgImageCollection1
            // 
            this.svgImageCollection1.Add("cd", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.cd"))));
            this.svgImageCollection1.Add("us", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.us"))));
            this.svgImageCollection1.Add("eu", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.eu"))));
            // 
            // btnConvert
            // 
            resources.ApplyResources(this.btnConvert, "btnConvert");
            this.btnConvert.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnConvert.Appearance.Font")));
            this.btnConvert.Appearance.Options.UseFont = true;
            this.btnConvert.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.currency;
            this.btnConvert.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // labelControl4
            // 
            resources.ApplyResources(this.labelControl4, "labelControl4");
            this.labelControl4.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl4.Appearance.Font")));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Name = "labelControl4";
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl3.Appearance.Font")));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Name = "labelControl3";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Name = "labelControl1";
            // 
            // txtAmountConvert
            // 
            resources.ApplyResources(this.txtAmountConvert, "txtAmountConvert");
            this.txtAmountConvert.Name = "txtAmountConvert";
            this.txtAmountConvert.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtAmountConvert.Properties.Appearance.Font")));
            this.txtAmountConvert.Properties.Appearance.Options.UseFont = true;
            this.txtAmountConvert.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmountConvert_KeyPress);
            // 
            // pictureCurrencyBase
            // 
            resources.ApplyResources(this.pictureCurrencyBase, "pictureCurrencyBase");
            this.pictureCurrencyBase.EditValue = global::EduXpress.Properties.Resources.cd;
            this.pictureCurrencyBase.Name = "pictureCurrencyBase";
            this.pictureCurrencyBase.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureCurrencyBase.Properties.SvgImageSize = new System.Drawing.Size(40, 40);
            // 
            // lblCurrencyBaseName
            // 
            resources.ApplyResources(this.lblCurrencyBaseName, "lblCurrencyBaseName");
            this.lblCurrencyBaseName.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCurrencyBaseName.Appearance.Font")));
            this.lblCurrencyBaseName.Appearance.Options.UseFont = true;
            this.lblCurrencyBaseName.Name = "lblCurrencyBaseName";
            // 
            // lblConversion
            // 
            resources.ApplyResources(this.lblConversion, "lblConversion");
            this.lblConversion.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblConversion.Appearance.Font")));
            this.lblConversion.Appearance.Options.UseFont = true;
            this.lblConversion.Appearance.Options.UseTextOptions = true;
            this.lblConversion.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblConversion.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblConversion.Name = "lblConversion";
            // 
            // lblCurrency
            // 
            resources.ApplyResources(this.lblCurrency, "lblCurrency");
            this.lblCurrency.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCurrency.Appearance.Font")));
            this.lblCurrency.Appearance.Options.UseFont = true;
            this.lblCurrency.Name = "lblCurrency";
            // 
            // pictureCurrency
            // 
            resources.ApplyResources(this.pictureCurrency, "pictureCurrency");
            this.pictureCurrency.EditValue = global::EduXpress.Properties.Resources.us;
            this.pictureCurrency.Name = "pictureCurrency";
            this.pictureCurrency.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureCurrency.Properties.SvgImageSize = new System.Drawing.Size(40, 40);
            // 
            // groupControl1
            // 
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.lblLastUpdated);
            this.groupControl1.Controls.Add(this.btnUpdateRates);
            this.groupControl1.Controls.Add(this.cmbSelectCurrency);
            this.groupControl1.Controls.Add(this.lblCurrency);
            this.groupControl1.Controls.Add(this.pictureCurrencyBase);
            this.groupControl1.Controls.Add(this.pictureCurrency);
            this.groupControl1.Controls.Add(this.lblCurrencyBaseName);
            this.groupControl1.Controls.Add(this.lblConversion);
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupControl1.Name = "groupControl1";
            // 
            // lblLastUpdated
            // 
            resources.ApplyResources(this.lblLastUpdated, "lblLastUpdated");
            this.lblLastUpdated.Name = "lblLastUpdated";
            // 
            // btnUpdateRates
            // 
            resources.ApplyResources(this.btnUpdateRates, "btnUpdateRates");
            this.btnUpdateRates.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnUpdateRates.Appearance.Font")));
            this.btnUpdateRates.Appearance.Options.UseFont = true;
            this.btnUpdateRates.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_refresh;
            this.btnUpdateRates.Name = "btnUpdateRates";
            this.btnUpdateRates.Click += new System.EventHandler(this.btnUpdateRates_Click);
            // 
            // cmbSelectCurrency
            // 
            resources.ApplyResources(this.cmbSelectCurrency, "cmbSelectCurrency");
            this.cmbSelectCurrency.Name = "cmbSelectCurrency";
            this.cmbSelectCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbSelectCurrency.Properties.Buttons"))))});
            this.cmbSelectCurrency.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSelectCurrency.SelectedIndexChanged += new System.EventHandler(this.cmbSelectCurrency_SelectedIndexChanged);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnClose.Appearance.Font")));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtDisplay
            // 
            resources.ApplyResources(this.txtDisplay, "txtDisplay");
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtDisplay.Properties.Appearance.Font")));
            this.txtDisplay.Properties.Appearance.Options.UseFont = true;
            this.txtDisplay.Properties.ReadOnly = true;
            // 
            // cmbCurrencyFrom
            // 
            resources.ApplyResources(this.cmbCurrencyFrom, "cmbCurrencyFrom");
            this.cmbCurrencyFrom.Name = "cmbCurrencyFrom";
            this.cmbCurrencyFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbCurrencyFrom.Properties.Buttons"))))});
            this.cmbCurrencyFrom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // cmbCurrencyTo
            // 
            resources.ApplyResources(this.cmbCurrencyTo, "cmbCurrencyTo");
            this.cmbCurrencyTo.Name = "cmbCurrencyTo";
            this.cmbCurrencyTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbCurrencyTo.Properties.Buttons"))))});
            this.cmbCurrencyTo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // frmExchangeRates
            // 
            resources.ApplyResources(this, "$this");
            this.Appearance.Options.UseTextOptions = true;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbCurrencyTo);
            this.Controls.Add(this.cmbCurrencyFrom);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtAmountConvert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.SvgImage = global::EduXpress.Properties.Resources.currency;
            this.MaximizeBox = false;
            this.Name = "frmExchangeRates";
            this.Load += new System.EventHandler(this.frmExchangeRates_Load);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmountConvert.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCurrencyBase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSelectCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDisplay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyTo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
        private DevExpress.XtraEditors.SimpleButton btnConvert;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtAmountConvert;
        private DevExpress.XtraEditors.PictureEdit pictureCurrencyBase;
        private DevExpress.XtraEditors.LabelControl lblCurrencyBaseName;
        private DevExpress.XtraEditors.LabelControl lblConversion;
        private DevExpress.XtraEditors.LabelControl lblCurrency;
        private DevExpress.XtraEditors.PictureEdit pictureCurrency;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnUpdateRates;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.TextEdit txtDisplay;
        private DevExpress.XtraEditors.LabelControl lblLastUpdated;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSelectCurrency;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCurrencyFrom;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCurrencyTo;
    }
}
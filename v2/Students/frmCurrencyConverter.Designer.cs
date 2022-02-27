namespace EduXpress.Students
{
    partial class frmCurrencyConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCurrencyConverter));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageFile = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupCurrencyConverter = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupClose = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cmbSelectCurrency = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblLastUpdated = new DevExpress.XtraEditors.LabelControl();
            this.txtRate = new DevExpress.XtraEditors.TextEdit();
            this.lblCurrency = new DevExpress.XtraEditors.LabelControl();
            this.pictureCurrencyBase = new DevExpress.XtraEditors.PictureEdit();
            this.pictureCurrency = new DevExpress.XtraEditors.PictureEdit();
            this.lblCurrencyBaseName = new DevExpress.XtraEditors.LabelControl();
            this.lblConversion = new DevExpress.XtraEditors.LabelControl();
            this.txtID = new System.Windows.Forms.TextBox();
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            this.gridControlCurrencyConverter = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSelectCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCurrencyBase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCurrencyConverter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.btnClose,
            this.btnNew,
            this.btnSave});
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.MaxItemId = 6;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageFile});
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Id = 1;
            this.btnClose.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            this.btnClose.Name = "btnClose";
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Id = 2;
            this.btnNew.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_add;
            this.btnNew.Name = "btnNew";
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
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
            // ribbonPageFile
            // 
            this.ribbonPageFile.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupCurrencyConverter,
            this.ribbonPageGroupClose});
            this.ribbonPageFile.Name = "ribbonPageFile";
            resources.ApplyResources(this.ribbonPageFile, "ribbonPageFile");
            // 
            // ribbonPageGroupCurrencyConverter
            // 
            this.ribbonPageGroupCurrencyConverter.AllowTextClipping = false;
            this.ribbonPageGroupCurrencyConverter.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroupCurrencyConverter.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupCurrencyConverter.Name = "ribbonPageGroupCurrencyConverter";
            resources.ApplyResources(this.ribbonPageGroupCurrencyConverter, "ribbonPageGroupCurrencyConverter");
            // 
            // ribbonPageGroupClose
            // 
            this.ribbonPageGroupClose.AllowTextClipping = false;
            this.ribbonPageGroupClose.ItemLinks.Add(this.btnClose);
            this.ribbonPageGroupClose.Name = "ribbonPageGroupClose";
            resources.ApplyResources(this.ribbonPageGroupClose, "ribbonPageGroupClose");
            // 
            // ribbonStatusBar1
            // 
            resources.ApplyResources(this.ribbonStatusBar1, "ribbonStatusBar1");
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            resources.ApplyResources(this.ribbonPage2, "ribbonPage2");
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.txtID);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.cmbSelectCurrency);
            this.groupControl1.Controls.Add(this.lblLastUpdated);
            this.groupControl1.Controls.Add(this.txtRate);
            this.groupControl1.Controls.Add(this.lblCurrency);
            this.groupControl1.Controls.Add(this.pictureCurrencyBase);
            this.groupControl1.Controls.Add(this.pictureCurrency);
            this.groupControl1.Controls.Add(this.lblCurrencyBaseName);
            this.groupControl1.Controls.Add(this.lblConversion);
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Name = "groupControl1";
            // 
            // cmbSelectCurrency
            // 
            resources.ApplyResources(this.cmbSelectCurrency, "cmbSelectCurrency");
            this.cmbSelectCurrency.MenuManager = this.ribbonControl1;
            this.cmbSelectCurrency.Name = "cmbSelectCurrency";
            this.cmbSelectCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbSelectCurrency.Properties.Buttons"))))});
            this.cmbSelectCurrency.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSelectCurrency.SelectedIndexChanged += new System.EventHandler(this.cmbSelectCurrency_SelectedIndexChanged);
            // 
            // lblLastUpdated
            // 
            resources.ApplyResources(this.lblLastUpdated, "lblLastUpdated");
            this.lblLastUpdated.Name = "lblLastUpdated";
            // 
            // txtRate
            // 
            resources.ApplyResources(this.txtRate, "txtRate");
            this.txtRate.MenuManager = this.ribbonControl1;
            this.txtRate.Name = "txtRate";
            this.txtRate.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtRate.Properties.Appearance.Font")));
            this.txtRate.Properties.Appearance.Options.UseFont = true;
            this.txtRate.Properties.ReadOnly = true;
            this.txtRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRate_KeyPress);
            // 
            // lblCurrency
            // 
            this.lblCurrency.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCurrency.Appearance.Font")));
            this.lblCurrency.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.lblCurrency, "lblCurrency");
            this.lblCurrency.Name = "lblCurrency";
            // 
            // pictureCurrencyBase
            // 
            this.pictureCurrencyBase.EditValue = global::EduXpress.Properties.Resources.cd;
            resources.ApplyResources(this.pictureCurrencyBase, "pictureCurrencyBase");
            this.pictureCurrencyBase.Name = "pictureCurrencyBase";
            this.pictureCurrencyBase.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureCurrencyBase.Properties.SvgImageSize = new System.Drawing.Size(40, 40);
            // 
            // pictureCurrency
            // 
            this.pictureCurrency.EditValue = global::EduXpress.Properties.Resources.us;
            resources.ApplyResources(this.pictureCurrency, "pictureCurrency");
            this.pictureCurrency.Name = "pictureCurrency";
            this.pictureCurrency.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureCurrency.Properties.SvgImageSize = new System.Drawing.Size(40, 40);
            // 
            // lblCurrencyBaseName
            // 
            this.lblCurrencyBaseName.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCurrencyBaseName.Appearance.Font")));
            this.lblCurrencyBaseName.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.lblCurrencyBaseName, "lblCurrencyBaseName");
            this.lblCurrencyBaseName.Name = "lblCurrencyBaseName";
            // 
            // lblConversion
            // 
            this.lblConversion.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblConversion.Appearance.Font")));
            this.lblConversion.Appearance.Options.UseFont = true;
            this.lblConversion.Appearance.Options.UseTextOptions = true;
            this.lblConversion.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblConversion.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            resources.ApplyResources(this.lblConversion, "lblConversion");
            this.lblConversion.Name = "lblConversion";
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            // 
            // svgImageCollection1
            // 
            this.svgImageCollection1.Add("cd", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.cd"))));
            this.svgImageCollection1.Add("us", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.us"))));
            this.svgImageCollection1.Add("eu", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.eu"))));
            // 
            // gridControlCurrencyConverter
            // 
            resources.ApplyResources(this.gridControlCurrencyConverter, "gridControlCurrencyConverter");
            this.gridControlCurrencyConverter.MainView = this.gridView1;
            this.gridControlCurrencyConverter.MenuManager = this.ribbonControl1;
            this.gridControlCurrencyConverter.Name = "gridControlCurrencyConverter";
            this.gridControlCurrencyConverter.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlCurrencyConverter;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmCurrencyConverter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlCurrencyConverter);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.IconOptions.SvgImage = global::EduXpress.Properties.Resources.currency;
            this.Name = "frmCurrencyConverter";
            this.Ribbon = this.ribbonControl1;
            this.StatusBar = this.ribbonStatusBar1;
            this.Load += new System.EventHandler(this.frmCurrencyConverter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSelectCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCurrencyBase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCurrencyConverter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageFile;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupCurrencyConverter;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupClose;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.TextBox txtID;
        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
        private DevExpress.XtraGrid.GridControl gridControlCurrencyConverter;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtRate;
        private DevExpress.XtraEditors.LabelControl lblCurrency;
        private DevExpress.XtraEditors.PictureEdit pictureCurrencyBase;
        private DevExpress.XtraEditors.PictureEdit pictureCurrency;
        private DevExpress.XtraEditors.LabelControl lblCurrencyBaseName;
        private DevExpress.XtraEditors.LabelControl lblConversion;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.LabelControl lblLastUpdated;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSelectCurrency;
    }
}
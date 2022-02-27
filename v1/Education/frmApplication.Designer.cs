namespace EduXpress.Education
{
    partial class frmApplication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmApplication));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageFile = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupApplication = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupClose = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.rangeControlPercentage = new DevExpress.XtraEditors.RangeControl();
            this.numericRangeControlClient1 = new DevExpress.XtraEditors.NumericRangeControlClient();
            this.label3 = new System.Windows.Forms.Label();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtAchievementAbbreviation = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCurrentAchievement = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtAchievementDescription = new DevExpress.XtraEditors.TextEdit();
            this.gridControlApplication = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rangeControlPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAchievementAbbreviation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAchievementDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlApplication)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.ExpandCollapseItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("ribbonControl1.ExpandCollapseItem.ImageOptions.ImageIndex")));
            this.ribbonControl1.ExpandCollapseItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("ribbonControl1.ExpandCollapseItem.ImageOptions.LargeImageIndex")));
            this.ribbonControl1.ExpandCollapseItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ribbonControl1.ExpandCollapseItem.ImageOptions.SvgImage")));
            this.ribbonControl1.ExpandCollapseItem.SearchTags = resources.GetString("ribbonControl1.ExpandCollapseItem.SearchTags");
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.btnNew,
            this.btnSave,
            this.btnUpdate,
            this.btnDelete,
            this.btnClose});
            this.ribbonControl1.MaxItemId = 6;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageFile});
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Id = 1;
            this.btnNew.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnNew.ImageOptions.ImageIndex")));
            this.btnNew.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnNew.ImageOptions.LargeImageIndex")));
            this.btnNew.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_add;
            this.btnNew.Name = "btnNew";
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Enabled = false;
            this.btnSave.Id = 2;
            this.btnSave.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnSave.ImageOptions.ImageIndex")));
            this.btnSave.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnSave.ImageOptions.LargeImageIndex")));
            this.btnSave.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.save;
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Id = 3;
            this.btnUpdate.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnUpdate.ImageOptions.ImageIndex")));
            this.btnUpdate.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnUpdate.ImageOptions.LargeImageIndex")));
            this.btnUpdate.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_refresh;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUpdate_ItemClick);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Enabled = false;
            this.btnDelete.Id = 4;
            this.btnDelete.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnDelete.ImageOptions.ImageIndex")));
            this.btnDelete.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnDelete.ImageOptions.LargeImageIndex")));
            this.btnDelete.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.delete;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Id = 5;
            this.btnClose.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnClose.ImageOptions.ImageIndex")));
            this.btnClose.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnClose.ImageOptions.LargeImageIndex")));
            this.btnClose.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            this.btnClose.Name = "btnClose";
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // ribbonPageFile
            // 
            this.ribbonPageFile.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupApplication,
            this.ribbonPageGroupClose});
            this.ribbonPageFile.Name = "ribbonPageFile";
            resources.ApplyResources(this.ribbonPageFile, "ribbonPageFile");
            // 
            // ribbonPageGroupApplication
            // 
            this.ribbonPageGroupApplication.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroupApplication.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupApplication.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupApplication.ItemLinks.Add(this.btnDelete);
            this.ribbonPageGroupApplication.Name = "ribbonPageGroupApplication";
            resources.ApplyResources(this.ribbonPageGroupApplication, "ribbonPageGroupApplication");
            // 
            // ribbonPageGroupClose
            // 
            this.ribbonPageGroupClose.AllowTextClipping = false;
            this.ribbonPageGroupClose.ItemLinks.Add(this.btnClose, true);
            this.ribbonPageGroupClose.Name = "ribbonPageGroupClose";
            resources.ApplyResources(this.ribbonPageGroupClose, "ribbonPageGroupClose");
            // 
            // ribbonStatusBar1
            // 
            resources.ApplyResources(this.ribbonStatusBar1, "ribbonStatusBar1");
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            // 
            // panelControl1
            // 
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Controls.Add(this.rangeControlPercentage);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.txtAchievementAbbreviation);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtCurrentAchievement);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtAchievementDescription);
            this.panelControl1.Name = "panelControl1";
            // 
            // rangeControlPercentage
            // 
            resources.ApplyResources(this.rangeControlPercentage, "rangeControlPercentage");
            this.rangeControlPercentage.Client = this.numericRangeControlClient1;
            this.rangeControlPercentage.Name = "rangeControlPercentage";
            this.rangeControlPercentage.SelectionType = DevExpress.XtraEditors.RangeControlSelectionType.ThumbAndFlag;
            this.rangeControlPercentage.ShowZoomScrollBar = false;
            // 
            // numericRangeControlClient1
            // 
            this.numericRangeControlClient1.FlagFormatString = "{0}%";
            this.numericRangeControlClient1.Maximum = 100;
            this.numericRangeControlClient1.RangeControl = this.rangeControlPercentage;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Name = "label3";
            // 
            // labelControl6
            // 
            resources.ApplyResources(this.labelControl6, "labelControl6");
            this.labelControl6.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl6.Appearance.Font")));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Name = "labelControl6";
            // 
            // txtAchievementAbbreviation
            // 
            resources.ApplyResources(this.txtAchievementAbbreviation, "txtAchievementAbbreviation");
            this.txtAchievementAbbreviation.MenuManager = this.ribbonControl1;
            this.txtAchievementAbbreviation.Name = "txtAchievementAbbreviation";
            this.txtAchievementAbbreviation.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtAchievementAbbreviation.Properties.Appearance.Font")));
            this.txtAchievementAbbreviation.Properties.Appearance.Options.UseFont = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Name = "label2";
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.AllowHtmlString = true;
            this.labelControl2.Name = "labelControl2";
            // 
            // txtCurrentAchievement
            // 
            resources.ApplyResources(this.txtCurrentAchievement, "txtCurrentAchievement");
            this.txtCurrentAchievement.Name = "txtCurrentAchievement";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Name = "label1";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Name = "labelControl1";
            // 
            // txtAchievementDescription
            // 
            resources.ApplyResources(this.txtAchievementDescription, "txtAchievementDescription");
            this.txtAchievementDescription.MenuManager = this.ribbonControl1;
            this.txtAchievementDescription.Name = "txtAchievementDescription";
            this.txtAchievementDescription.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtAchievementDescription.Properties.Appearance.Font")));
            this.txtAchievementDescription.Properties.Appearance.Options.UseFont = true;
            // 
            // gridControlApplication
            // 
            resources.ApplyResources(this.gridControlApplication, "gridControlApplication");
            this.gridControlApplication.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControlApplication.EmbeddedNavigator.AccessibleDescription");
            this.gridControlApplication.EmbeddedNavigator.AccessibleName = resources.GetString("gridControlApplication.EmbeddedNavigator.AccessibleName");
            this.gridControlApplication.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("gridControlApplication.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.gridControlApplication.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridControlApplication.EmbeddedNavigator.Anchor")));
            this.gridControlApplication.EmbeddedNavigator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControlApplication.EmbeddedNavigator.BackgroundImage")));
            this.gridControlApplication.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gridControlApplication.EmbeddedNavigator.BackgroundImageLayout")));
            this.gridControlApplication.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridControlApplication.EmbeddedNavigator.ImeMode")));
            this.gridControlApplication.EmbeddedNavigator.MaximumSize = ((System.Drawing.Size)(resources.GetObject("gridControlApplication.EmbeddedNavigator.MaximumSize")));
            this.gridControlApplication.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gridControlApplication.EmbeddedNavigator.TextLocation")));
            this.gridControlApplication.EmbeddedNavigator.ToolTip = resources.GetString("gridControlApplication.EmbeddedNavigator.ToolTip");
            this.gridControlApplication.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gridControlApplication.EmbeddedNavigator.ToolTipIconType")));
            this.gridControlApplication.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControlApplication.EmbeddedNavigator.ToolTipTitle");
            this.gridControlApplication.MainView = this.gridView1;
            this.gridControlApplication.MenuManager = this.ribbonControl1;
            this.gridControlApplication.Name = "gridControlApplication";
            this.gridControlApplication.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlApplication.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControlApplication_MouseClick);
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.GridControl = this.gridControlApplication;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmApplication
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlApplication);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.SvgImage = global::EduXpress.Properties.Resources.employeequickaward;
            this.MaximizeBox = false;
            this.Name = "frmApplication";
            this.Ribbon = this.ribbonControl1;
            this.StatusBar = this.ribbonStatusBar1;
            this.Load += new System.EventHandler(this.frmApplication_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rangeControlPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAchievementAbbreviation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAchievementDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlApplication)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnUpdate;
        private DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageFile;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupApplication;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupClose;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.TextBox txtCurrentAchievement;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtAchievementDescription;
        private DevExpress.XtraGrid.GridControl gridControlApplication;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtAchievementAbbreviation;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.RangeControl rangeControlPercentage;
        private DevExpress.XtraEditors.NumericRangeControlClient numericRangeControlClient1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}
namespace EduXpress.Admin
{
    partial class userControlAcademicYear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userControlAcademicYear));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItemProcess = new DevExpress.XtraBars.BarStaticItem();
            this.btnExit = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageAcademicYear = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupAcademicYear = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.txtCurrentAcademicYear = new System.Windows.Forms.TextBox();
            this.gridControlAcademicYear = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAcademicYear = new DevExpress.XtraEditors.TextEdit();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl));
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAcademicYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAcademicYear.Properties)).BeginInit();
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
            this.btnUpdate,
            this.btnRemove,
            this.barStaticItemProcess,
            this.btnExit,
            this.barStaticItem1,
            this.btnSave,
            this.barButtonItem1,
            this.barButtonItem2});
            this.ribbonControl1.MaxItemId = 13;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageAcademicYear});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
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
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Id = 2;
            this.btnUpdate.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnUpdate.ImageOptions.ImageIndex")));
            this.btnUpdate.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnUpdate.ImageOptions.LargeImageIndex")));
            this.btnUpdate.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_refresh;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUpdate_ItemClick);
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Enabled = false;
            this.btnRemove.Id = 4;
            this.btnRemove.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnRemove.ImageOptions.ImageIndex")));
            this.btnRemove.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnRemove.ImageOptions.LargeImageIndex")));
            this.btnRemove.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_removecircled;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRemove_ItemClick);
            // 
            // barStaticItemProcess
            // 
            resources.ApplyResources(this.barStaticItemProcess, "barStaticItemProcess");
            this.barStaticItemProcess.Id = 5;
            this.barStaticItemProcess.ImageOptions.ImageIndex = ((int)(resources.GetObject("barStaticItemProcess.ImageOptions.ImageIndex")));
            this.barStaticItemProcess.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("barStaticItemProcess.ImageOptions.LargeImageIndex")));
            this.barStaticItemProcess.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barStaticItemProcess.ImageOptions.SvgImage")));
            this.barStaticItemProcess.Name = "barStaticItemProcess";
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Id = 6;
            this.btnExit.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnExit.ImageOptions.ImageIndex")));
            this.btnExit.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnExit.ImageOptions.LargeImageIndex")));
            this.btnExit.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            this.btnExit.Name = "btnExit";
            // 
            // barStaticItem1
            // 
            resources.ApplyResources(this.barStaticItem1, "barStaticItem1");
            this.barStaticItem1.Id = 7;
            this.barStaticItem1.ImageOptions.ImageIndex = ((int)(resources.GetObject("barStaticItem1.ImageOptions.ImageIndex")));
            this.barStaticItem1.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("barStaticItem1.ImageOptions.LargeImageIndex")));
            this.barStaticItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barStaticItem1.ImageOptions.SvgImage")));
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Enabled = false;
            this.btnSave.Id = 9;
            this.btnSave.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnSave.ImageOptions.ImageIndex")));
            this.btnSave.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnSave.ImageOptions.LargeImageIndex")));
            this.btnSave.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.save;
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 11;
            this.barButtonItem1.ImageOptions.ImageIndex = ((int)(resources.GetObject("barButtonItem1.ImageOptions.ImageIndex")));
            this.barButtonItem1.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("barButtonItem1.ImageOptions.LargeImageIndex")));
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            resources.ApplyResources(this.barButtonItem2, "barButtonItem2");
            this.barButtonItem2.Id = 12;
            this.barButtonItem2.ImageOptions.ImageIndex = ((int)(resources.GetObject("barButtonItem2.ImageOptions.ImageIndex")));
            this.barButtonItem2.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("barButtonItem2.ImageOptions.LargeImageIndex")));
            this.barButtonItem2.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem2.ImageOptions.SvgImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // ribbonPageAcademicYear
            // 
            this.ribbonPageAcademicYear.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupAcademicYear});
            this.ribbonPageAcademicYear.MergeOrder = 2;
            this.ribbonPageAcademicYear.Name = "ribbonPageAcademicYear";
            resources.ApplyResources(this.ribbonPageAcademicYear, "ribbonPageAcademicYear");
            // 
            // ribbonPageGroupAcademicYear
            // 
            this.ribbonPageGroupAcademicYear.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroupAcademicYear.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupAcademicYear.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupAcademicYear.ItemLinks.Add(this.btnRemove);
            this.ribbonPageGroupAcademicYear.Name = "ribbonPageGroupAcademicYear";
            resources.ApplyResources(this.ribbonPageGroupAcademicYear, "ribbonPageGroupAcademicYear");
            // 
            // ribbonStatusBar1
            // 
            resources.ApplyResources(this.ribbonStatusBar1, "ribbonStatusBar1");
            this.ribbonStatusBar1.HideWhenMerging = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            // 
            // xtraScrollableControl1
            // 
            resources.ApplyResources(this.xtraScrollableControl1, "xtraScrollableControl1");
            this.xtraScrollableControl1.Controls.Add(this.txtCurrentAcademicYear);
            this.xtraScrollableControl1.Controls.Add(this.gridControlAcademicYear);
            this.xtraScrollableControl1.Controls.Add(this.label4);
            this.xtraScrollableControl1.Controls.Add(this.txtAcademicYear);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            // 
            // txtCurrentAcademicYear
            // 
            resources.ApplyResources(this.txtCurrentAcademicYear, "txtCurrentAcademicYear");
            this.txtCurrentAcademicYear.Name = "txtCurrentAcademicYear";
            // 
            // gridControlAcademicYear
            // 
            resources.ApplyResources(this.gridControlAcademicYear, "gridControlAcademicYear");
            this.gridControlAcademicYear.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControlAcademicYear.EmbeddedNavigator.AccessibleDescription");
            this.gridControlAcademicYear.EmbeddedNavigator.AccessibleName = resources.GetString("gridControlAcademicYear.EmbeddedNavigator.AccessibleName");
            this.gridControlAcademicYear.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("gridControlAcademicYear.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.gridControlAcademicYear.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridControlAcademicYear.EmbeddedNavigator.Anchor")));
            this.gridControlAcademicYear.EmbeddedNavigator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControlAcademicYear.EmbeddedNavigator.BackgroundImage")));
            this.gridControlAcademicYear.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gridControlAcademicYear.EmbeddedNavigator.BackgroundImageLayout")));
            this.gridControlAcademicYear.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridControlAcademicYear.EmbeddedNavigator.ImeMode")));
            this.gridControlAcademicYear.EmbeddedNavigator.MaximumSize = ((System.Drawing.Size)(resources.GetObject("gridControlAcademicYear.EmbeddedNavigator.MaximumSize")));
            this.gridControlAcademicYear.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gridControlAcademicYear.EmbeddedNavigator.TextLocation")));
            this.gridControlAcademicYear.EmbeddedNavigator.ToolTip = resources.GetString("gridControlAcademicYear.EmbeddedNavigator.ToolTip");
            this.gridControlAcademicYear.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gridControlAcademicYear.EmbeddedNavigator.ToolTipIconType")));
            this.gridControlAcademicYear.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControlAcademicYear.EmbeddedNavigator.ToolTipTitle");
            this.gridControlAcademicYear.MainView = this.gridView1;
            this.gridControlAcademicYear.MenuManager = this.ribbonControl1;
            this.gridControlAcademicYear.Name = "gridControlAcademicYear";
            this.gridControlAcademicYear.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlAcademicYear.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControlAcademicYear_MouseClick);
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.GridControl = this.gridControlAcademicYear;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtAcademicYear
            // 
            resources.ApplyResources(this.txtAcademicYear, "txtAcademicYear");
            this.txtAcademicYear.Name = "txtAcademicYear";
            this.txtAcademicYear.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("txtAcademicYear.Properties.Mask.BeepOnError")));
            this.txtAcademicYear.Properties.Mask.EditMask = resources.GetString("txtAcademicYear.Properties.Mask.EditMask");
            this.txtAcademicYear.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtAcademicYear.Properties.Mask.MaskType")));
            this.txtAcademicYear.Properties.ShowNullValuePrompt = ((DevExpress.XtraEditors.ShowNullValuePromptOptions)((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused | DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly)));
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // userControlAcademicYear
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "userControlAcademicYear";
            this.Load += new System.EventHandler(this.userControlAcademicYear_Load);
            this.VisibleChanged += new System.EventHandler(this.userControlAcademicYear_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAcademicYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAcademicYear.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.BarButtonItem btnUpdate;
        
        private DevExpress.XtraBars.BarButtonItem btnRemove;
        private DevExpress.XtraBars.BarStaticItem barStaticItemProcess;
        private DevExpress.XtraBars.BarButtonItem btnExit;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageAcademicYear;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupAcademicYear;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        internal System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtAcademicYear;
        private DevExpress.XtraGrid.GridControl gridControlAcademicYear;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private System.Windows.Forms.TextBox txtCurrentAcademicYear;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
    }
}

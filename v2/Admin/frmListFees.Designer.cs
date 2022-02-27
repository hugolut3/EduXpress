namespace EduXpress.Admin
{
    partial class frmListFees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListFees));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNewFee = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnRemove = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageFile = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupFees = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupClose = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtCurrentFeeName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFeeName = new DevExpress.XtraEditors.TextEdit();
            this.gridControlFeeName = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFeeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFeeName)).BeginInit();
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
            this.btnNewFee,
            this.btnSave,
            this.btnUpdate,
            this.btnRemove,
            this.btnClose,
            this.barButtonItem6,
            this.ribbonControl1.SearchEditItem});
            this.ribbonControl1.MaxItemId = 7;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageFile});
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnNewFee
            // 
            resources.ApplyResources(this.btnNewFee, "btnNewFee");
            this.btnNewFee.Id = 1;
            this.btnNewFee.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnNewFee.ImageOptions.ImageIndex")));
            this.btnNewFee.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnNewFee.ImageOptions.LargeImageIndex")));
            this.btnNewFee.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_add;
            this.btnNewFee.Name = "btnNewFee";
            this.btnNewFee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNewFee_ItemClick);
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
            // barButtonItem6
            // 
            resources.ApplyResources(this.barButtonItem6, "barButtonItem6");
            this.barButtonItem6.Id = 6;
            this.barButtonItem6.ImageOptions.ImageIndex = ((int)(resources.GetObject("barButtonItem6.ImageOptions.ImageIndex")));
            this.barButtonItem6.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("barButtonItem6.ImageOptions.LargeImageIndex")));
            this.barButtonItem6.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem6.ImageOptions.SvgImage")));
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // ribbonPageFile
            // 
            this.ribbonPageFile.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupFees,
            this.ribbonPageGroupClose});
            this.ribbonPageFile.Name = "ribbonPageFile";
            resources.ApplyResources(this.ribbonPageFile, "ribbonPageFile");
            // 
            // ribbonPageGroupFees
            // 
            this.ribbonPageGroupFees.ItemLinks.Add(this.btnNewFee);
            this.ribbonPageGroupFees.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupFees.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupFees.ItemLinks.Add(this.btnRemove);
            this.ribbonPageGroupFees.Name = "ribbonPageGroupFees";
            resources.ApplyResources(this.ribbonPageGroupFees, "ribbonPageGroupFees");
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
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Controls.Add(this.txtCurrentFeeName);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.txtFeeName);
            this.panelControl1.Name = "panelControl1";
            // 
            // txtCurrentFeeName
            // 
            resources.ApplyResources(this.txtCurrentFeeName, "txtCurrentFeeName");
            this.txtCurrentFeeName.Name = "txtCurrentFeeName";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtFeeName
            // 
            resources.ApplyResources(this.txtFeeName, "txtFeeName");
            this.txtFeeName.Name = "txtFeeName";
            // 
            // gridControlFeeName
            // 
            resources.ApplyResources(this.gridControlFeeName, "gridControlFeeName");
            this.gridControlFeeName.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControlFeeName.EmbeddedNavigator.AccessibleDescription");
            this.gridControlFeeName.EmbeddedNavigator.AccessibleName = resources.GetString("gridControlFeeName.EmbeddedNavigator.AccessibleName");
            this.gridControlFeeName.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("gridControlFeeName.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.gridControlFeeName.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridControlFeeName.EmbeddedNavigator.Anchor")));
            this.gridControlFeeName.EmbeddedNavigator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControlFeeName.EmbeddedNavigator.BackgroundImage")));
            this.gridControlFeeName.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gridControlFeeName.EmbeddedNavigator.BackgroundImageLayout")));
            this.gridControlFeeName.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridControlFeeName.EmbeddedNavigator.ImeMode")));
            this.gridControlFeeName.EmbeddedNavigator.MaximumSize = ((System.Drawing.Size)(resources.GetObject("gridControlFeeName.EmbeddedNavigator.MaximumSize")));
            this.gridControlFeeName.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gridControlFeeName.EmbeddedNavigator.TextLocation")));
            this.gridControlFeeName.EmbeddedNavigator.ToolTip = resources.GetString("gridControlFeeName.EmbeddedNavigator.ToolTip");
            this.gridControlFeeName.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gridControlFeeName.EmbeddedNavigator.ToolTipIconType")));
            this.gridControlFeeName.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControlFeeName.EmbeddedNavigator.ToolTipTitle");
            this.gridControlFeeName.MainView = this.gridView1;
            this.gridControlFeeName.MenuManager = this.ribbonControl1;
            this.gridControlFeeName.Name = "gridControlFeeName";
            this.gridControlFeeName.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlFeeName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControlFeeName_MouseClick);
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.GridControl = this.gridControlFeeName;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmListFees
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlFeeName);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "frmListFees";
            this.Ribbon = this.ribbonControl1;
            this.StatusBar = this.ribbonStatusBar1;
            this.Load += new System.EventHandler(this.frmListFees_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFeeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFeeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageFile;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupFees;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupClose;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.BarButtonItem btnNewFee;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnUpdate;
        private DevExpress.XtraBars.BarButtonItem btnRemove;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        internal System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtFeeName;
        private System.Windows.Forms.TextBox txtCurrentFeeName;
        private DevExpress.XtraGrid.GridControl gridControlFeeName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}
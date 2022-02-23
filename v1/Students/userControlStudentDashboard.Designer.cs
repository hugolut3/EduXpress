namespace EduXpress.Students
{
    partial class userControlStudentDashboard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userControlStudentDashboard));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnDashboardDesigner = new DevExpress.XtraBars.BarButtonItem();
            this.btnCloseApplication = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefreshDashboard = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageDashboard = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupDashboard = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.dashboardViewer1 = new DevExpress.DashboardWin.DashboardViewer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl));
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.btnDashboardDesigner,
            this.btnCloseApplication,
            this.btnRefreshDashboard});
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.MaxItemId = 7;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageDashboard});
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnDashboardDesigner
            // 
            resources.ApplyResources(this.btnDashboardDesigner, "btnDashboardDesigner");
            this.btnDashboardDesigner.Id = 1;
            this.btnDashboardDesigner.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.createpie3dchart;
            this.btnDashboardDesigner.Name = "btnDashboardDesigner";
            this.btnDashboardDesigner.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDashboardDesigner_ItemClick);
            // 
            // btnCloseApplication
            // 
            this.btnCloseApplication.Id = 5;
            this.btnCloseApplication.Name = "btnCloseApplication";
            // 
            // btnRefreshDashboard
            // 
            resources.ApplyResources(this.btnRefreshDashboard, "btnRefreshDashboard");
            this.btnRefreshDashboard.Id = 6;
            this.btnRefreshDashboard.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_refresh;
            this.btnRefreshDashboard.Name = "btnRefreshDashboard";
            this.btnRefreshDashboard.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefreshDashboard_ItemClick);
            // 
            // ribbonPageDashboard
            // 
            this.ribbonPageDashboard.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupDashboard});
            this.ribbonPageDashboard.MergeOrder = 2;
            this.ribbonPageDashboard.Name = "ribbonPageDashboard";
            resources.ApplyResources(this.ribbonPageDashboard, "ribbonPageDashboard");
            // 
            // ribbonPageGroupDashboard
            // 
            this.ribbonPageGroupDashboard.AllowTextClipping = false;
            this.ribbonPageGroupDashboard.ItemLinks.Add(this.btnDashboardDesigner);
            this.ribbonPageGroupDashboard.ItemLinks.Add(this.btnRefreshDashboard);
            this.ribbonPageGroupDashboard.Name = "ribbonPageGroupDashboard";
            resources.ApplyResources(this.ribbonPageGroupDashboard, "ribbonPageGroupDashboard");
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.HideWhenMerging = DevExpress.Utils.DefaultBoolean.False;
            resources.ApplyResources(this.ribbonStatusBar1, "ribbonStatusBar1");
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 3;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.dashboardViewer1);
            resources.ApplyResources(this.xtraScrollableControl1, "xtraScrollableControl1");
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            // 
            // dashboardViewer1
            // 
            this.dashboardViewer1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.dashboardViewer1.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.dashboardViewer1, "dashboardViewer1");
            this.dashboardViewer1.Name = "dashboardViewer1";
            this.dashboardViewer1.ConfigureDataConnection += new DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventHandler(this.dashboardViewer1_ConfigureDataConnection);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // userControlStudentDashboard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "userControlStudentDashboard";
            this.Load += new System.EventHandler(this.userControlStudentDashboard_Load);
            this.VisibleChanged += new System.EventHandler(this.userControlStudentDashboard_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.xtraScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dashboardViewer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageDashboard;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupDashboard;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.DashboardWin.DashboardViewer dashboardViewer1;
        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraBars.BarButtonItem btnDashboardDesigner;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnCloseApplication;
        private DevExpress.XtraBars.BarButtonItem btnRefreshDashboard;
    }
}

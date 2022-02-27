namespace EduXpress.Reports
{
    partial class userControlReportsLogs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userControlReportsLogs));
            this.cmbUserType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupDate = new System.Windows.Forms.GroupBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dtFrom = new DevExpress.XtraEditors.DateEdit();
            this.dtTo = new DevExpress.XtraEditors.DateEdit();
            this.btnGetData = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlLogs = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnReset = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteLogs = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItemProcess = new DevExpress.XtraBars.BarStaticItem();
            this.btnExportPDF = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportWord = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrintPreview = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageEventLogs = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupEventLogs = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupExport = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupPrint = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.btnLoadLogs = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl));
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserType.Properties)).BeginInit();
            this.groupDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.xtraScrollableControl1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbUserType
            // 
            resources.ApplyResources(this.cmbUserType, "cmbUserType");
            this.cmbUserType.Name = "cmbUserType";
            this.cmbUserType.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("cmbUserType.Properties.Appearance.Font")));
            this.cmbUserType.Properties.Appearance.Options.UseFont = true;
            this.cmbUserType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbUserType.Properties.Buttons"))))});
            this.cmbUserType.SelectedIndexChanged += new System.EventHandler(this.cmbUserType_SelectedIndexChanged);
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl3.Appearance.Font")));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Name = "labelControl3";
            // 
            // groupDate
            // 
            resources.ApplyResources(this.groupDate, "groupDate");
            this.groupDate.Controls.Add(this.labelControl2);
            this.groupDate.Controls.Add(this.labelControl1);
            this.groupDate.Controls.Add(this.dtFrom);
            this.groupDate.Controls.Add(this.dtTo);
            this.groupDate.Controls.Add(this.btnGetData);
            this.groupDate.Name = "groupDate";
            this.groupDate.TabStop = false;
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl2.Appearance.Font")));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Name = "labelControl2";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Name = "labelControl1";
            // 
            // dtFrom
            // 
            resources.ApplyResources(this.dtFrom, "dtFrom");
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("dtFrom.Properties.Appearance.Font")));
            this.dtFrom.Properties.Appearance.Options.UseFont = true;
            this.dtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtFrom.Properties.Buttons"))))});
            this.dtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtFrom.Properties.CalendarTimeProperties.Buttons"))))});
            this.dtFrom.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Fluent;
            this.dtFrom.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            // 
            // dtTo
            // 
            resources.ApplyResources(this.dtTo, "dtTo");
            this.dtTo.Name = "dtTo";
            this.dtTo.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("dtTo.Properties.Appearance.Font")));
            this.dtTo.Properties.Appearance.Options.UseFont = true;
            this.dtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtTo.Properties.Buttons"))))});
            this.dtTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtTo.Properties.CalendarTimeProperties.Buttons"))))});
            this.dtTo.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Fluent;
            this.dtTo.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            // 
            // btnGetData
            // 
            resources.ApplyResources(this.btnGetData, "btnGetData");
            this.btnGetData.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.detailed;
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.ShowToolTips = false;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // gridControlLogs
            // 
            resources.ApplyResources(this.gridControlLogs, "gridControlLogs");
            this.gridControlLogs.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControlLogs.EmbeddedNavigator.AccessibleDescription");
            this.gridControlLogs.EmbeddedNavigator.AccessibleName = resources.GetString("gridControlLogs.EmbeddedNavigator.AccessibleName");
            this.gridControlLogs.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("gridControlLogs.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.gridControlLogs.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridControlLogs.EmbeddedNavigator.Anchor")));
            this.gridControlLogs.EmbeddedNavigator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControlLogs.EmbeddedNavigator.BackgroundImage")));
            this.gridControlLogs.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gridControlLogs.EmbeddedNavigator.BackgroundImageLayout")));
            this.gridControlLogs.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridControlLogs.EmbeddedNavigator.ImeMode")));
            this.gridControlLogs.EmbeddedNavigator.MaximumSize = ((System.Drawing.Size)(resources.GetObject("gridControlLogs.EmbeddedNavigator.MaximumSize")));
            this.gridControlLogs.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gridControlLogs.EmbeddedNavigator.TextLocation")));
            this.gridControlLogs.EmbeddedNavigator.ToolTip = resources.GetString("gridControlLogs.EmbeddedNavigator.ToolTip");
            this.gridControlLogs.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gridControlLogs.EmbeddedNavigator.ToolTipIconType")));
            this.gridControlLogs.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControlLogs.EmbeddedNavigator.ToolTipTitle");
            this.gridControlLogs.MainView = this.gridView1;
            this.gridControlLogs.Name = "gridControlLogs";
            this.gridControlLogs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.GridControl = this.gridControlLogs;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView1_CustomColumnDisplayText);
            this.gridView1.PrintInitialize += new DevExpress.XtraGrid.Views.Base.PrintInitializeEventHandler(this.gridView1_PrintInitialize);
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
            this.btnReset,
            this.btnExportExcel,
            this.btnDeleteLogs,
            this.barStaticItemProcess,
            this.btnExportPDF,
            this.btnExportWord,
            this.btnPrintPreview,
            this.btnPrint});
            this.ribbonControl1.MaxItemId = 10;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageEventLogs});
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Id = 1;
            this.btnReset.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnReset.ImageOptions.ImageIndex")));
            this.btnReset.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnReset.ImageOptions.LargeImageIndex")));
            this.btnReset.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.resetlayoutoptions;
            this.btnReset.Name = "btnReset";
            this.btnReset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReset_ItemClick);
            // 
            // btnExportExcel
            // 
            resources.ApplyResources(this.btnExportExcel, "btnExportExcel");
            this.btnExportExcel.Id = 2;
            this.btnExportExcel.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnExportExcel.ImageOptions.ImageIndex")));
            this.btnExportExcel.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnExportExcel.ImageOptions.LargeImageIndex")));
            this.btnExportExcel.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.above_average;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportExcel_ItemClick);
            // 
            // btnDeleteLogs
            // 
            resources.ApplyResources(this.btnDeleteLogs, "btnDeleteLogs");
            this.btnDeleteLogs.Id = 3;
            this.btnDeleteLogs.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnDeleteLogs.ImageOptions.ImageIndex")));
            this.btnDeleteLogs.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnDeleteLogs.ImageOptions.LargeImageIndex")));
            this.btnDeleteLogs.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_trash;
            this.btnDeleteLogs.Name = "btnDeleteLogs";
            this.btnDeleteLogs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteLogs_ItemClick);
            // 
            // barStaticItemProcess
            // 
            resources.ApplyResources(this.barStaticItemProcess, "barStaticItemProcess");
            this.barStaticItemProcess.Id = 4;
            this.barStaticItemProcess.ImageOptions.ImageIndex = ((int)(resources.GetObject("barStaticItemProcess.ImageOptions.ImageIndex")));
            this.barStaticItemProcess.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("barStaticItemProcess.ImageOptions.LargeImageIndex")));
            this.barStaticItemProcess.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barStaticItemProcess.ImageOptions.SvgImage")));
            this.barStaticItemProcess.Name = "barStaticItemProcess";
            // 
            // btnExportPDF
            // 
            resources.ApplyResources(this.btnExportPDF, "btnExportPDF");
            this.btnExportPDF.Id = 6;
            this.btnExportPDF.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnExportPDF.ImageOptions.ImageIndex")));
            this.btnExportPDF.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnExportPDF.ImageOptions.LargeImageIndex")));
            this.btnExportPDF.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.documentpdf;
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportPDF_ItemClick);
            // 
            // btnExportWord
            // 
            resources.ApplyResources(this.btnExportWord, "btnExportWord");
            this.btnExportWord.Id = 7;
            this.btnExportWord.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnExportWord.ImageOptions.ImageIndex")));
            this.btnExportWord.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnExportWord.ImageOptions.LargeImageIndex")));
            this.btnExportWord.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.inserttextbox;
            this.btnExportWord.Name = "btnExportWord";
            this.btnExportWord.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportWord_ItemClick);
            // 
            // btnPrintPreview
            // 
            resources.ApplyResources(this.btnPrintPreview, "btnPrintPreview");
            this.btnPrintPreview.Id = 8;
            this.btnPrintPreview.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnPrintPreview.ImageOptions.ImageIndex")));
            this.btnPrintPreview.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnPrintPreview.ImageOptions.LargeImageIndex")));
            this.btnPrintPreview.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.preview;
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPrintPreview_ItemClick);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Id = 9;
            this.btnPrint.ImageOptions.ImageIndex = ((int)(resources.GetObject("btnPrint.ImageOptions.ImageIndex")));
            this.btnPrint.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("btnPrint.ImageOptions.LargeImageIndex")));
            this.btnPrint.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.print;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPrint_ItemClick);
            // 
            // ribbonPageEventLogs
            // 
            this.ribbonPageEventLogs.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupEventLogs,
            this.ribbonPageGroupExport,
            this.ribbonPageGroupPrint});
            this.ribbonPageEventLogs.MergeOrder = 2;
            this.ribbonPageEventLogs.Name = "ribbonPageEventLogs";
            resources.ApplyResources(this.ribbonPageEventLogs, "ribbonPageEventLogs");
            // 
            // ribbonPageGroupEventLogs
            // 
            this.ribbonPageGroupEventLogs.AllowTextClipping = false;
            this.ribbonPageGroupEventLogs.ItemLinks.Add(this.btnReset);
            this.ribbonPageGroupEventLogs.ItemLinks.Add(this.btnDeleteLogs);
            this.ribbonPageGroupEventLogs.Name = "ribbonPageGroupEventLogs";
            resources.ApplyResources(this.ribbonPageGroupEventLogs, "ribbonPageGroupEventLogs");
            // 
            // ribbonPageGroupExport
            // 
            this.ribbonPageGroupExport.AllowTextClipping = false;
            this.ribbonPageGroupExport.ItemLinks.Add(this.btnExportExcel);
            this.ribbonPageGroupExport.ItemLinks.Add(this.btnExportPDF);
            this.ribbonPageGroupExport.ItemLinks.Add(this.btnExportWord);
            this.ribbonPageGroupExport.Name = "ribbonPageGroupExport";
            resources.ApplyResources(this.ribbonPageGroupExport, "ribbonPageGroupExport");
            // 
            // ribbonPageGroupPrint
            // 
            this.ribbonPageGroupPrint.AllowTextClipping = false;
            this.ribbonPageGroupPrint.ItemLinks.Add(this.btnPrintPreview);
            this.ribbonPageGroupPrint.ItemLinks.Add(this.btnPrint);
            this.ribbonPageGroupPrint.Name = "ribbonPageGroupPrint";
            resources.ApplyResources(this.ribbonPageGroupPrint, "ribbonPageGroupPrint");
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
            this.xtraScrollableControl1.Controls.Add(this.btnLoadLogs);
            this.xtraScrollableControl1.Controls.Add(this.groupBox2);
            this.xtraScrollableControl1.Controls.Add(this.groupDate);
            this.xtraScrollableControl1.Controls.Add(this.gridControlLogs);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            // 
            // btnLoadLogs
            // 
            resources.ApplyResources(this.btnLoadLogs, "btnLoadLogs");
            this.btnLoadLogs.Appearance.Options.UseTextOptions = true;
            this.btnLoadLogs.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnLoadLogs.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.changeview;
            this.btnLoadLogs.Name = "btnLoadLogs";
            this.btnLoadLogs.ShowToolTips = false;
            this.btnLoadLogs.Click += new System.EventHandler(this.btnLoadLogs_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.cmbUserType);
            this.groupBox2.Controls.Add(this.labelControl3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // userControlReportsLogs
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "userControlReportsLogs";
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserType.Properties)).EndInit();
            this.groupDate.ResumeLayout(false);
            this.groupDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.xtraScrollableControl1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.ComboBoxEdit cmbUserType;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.GroupBox groupDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dtFrom;
        private DevExpress.XtraEditors.DateEdit dtTo;
        private DevExpress.XtraEditors.SimpleButton btnGetData;
        private DevExpress.XtraGrid.GridControl gridControlLogs;
        private DevExpress.XtraBars.BarButtonItem btnReset;
        private DevExpress.XtraBars.BarButtonItem btnExportExcel;
        private DevExpress.XtraBars.BarButtonItem btnDeleteLogs;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageEventLogs;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupEventLogs;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraBars.BarStaticItem barStaticItemProcess;
        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.SimpleButton btnLoadLogs;
        private DevExpress.XtraBars.BarButtonItem btnExportPDF;
        private DevExpress.XtraBars.BarButtonItem btnExportWord;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupExport;
        private DevExpress.XtraBars.BarButtonItem btnPrintPreview;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupPrint;
        private DevExpress.XtraBars.BarButtonItem btnPrint;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}

namespace EduXpress.Bus
{
    partial class userControlBusInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userControlBusInformation));
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
            this.ribbonPageSchoolBus = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupSchoolBus = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDriverAssistantContactNo = new DevExpress.XtraEditors.TextEdit();
            this.cmbDriverAssistant = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDriverContactNo = new DevExpress.XtraEditors.TextEdit();
            this.cmbDriverName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumberPlate = new DevExpress.XtraEditors.TextEdit();
            this.txtCurrentBusNo = new System.Windows.Forms.TextBox();
            this.gridControlBusInformation = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBusNo = new DevExpress.XtraEditors.TextEdit();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl));
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDriverAssistantContactNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDriverAssistant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDriverContactNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDriverName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberPlate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBusInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
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
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.MaxItemId = 13;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageSchoolBus});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Enabled = false;
            this.btnNew.Id = 1;
            this.btnNew.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_add;
            this.btnNew.Name = "btnNew";
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Id = 2;
            this.btnUpdate.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_refresh;
            this.btnUpdate.Name = "btnUpdate";
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Enabled = false;
            this.btnRemove.Id = 4;
            this.btnRemove.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_removecircled;
            this.btnRemove.Name = "btnRemove";
            // 
            // barStaticItemProcess
            // 
            resources.ApplyResources(this.barStaticItemProcess, "barStaticItemProcess");
            this.barStaticItemProcess.Id = 5;
            this.barStaticItemProcess.Name = "barStaticItemProcess";
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Id = 6;
            this.btnExit.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            this.btnExit.Name = "btnExit";
            // 
            // barStaticItem1
            // 
            resources.ApplyResources(this.barStaticItem1, "barStaticItem1");
            this.barStaticItem1.Id = 7;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Enabled = false;
            this.btnSave.Id = 9;
            this.btnSave.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.save;
            this.btnSave.Name = "btnSave";
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 11;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            resources.ApplyResources(this.barButtonItem2, "barButtonItem2");
            this.barButtonItem2.Id = 12;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // ribbonPageSchoolBus
            // 
            this.ribbonPageSchoolBus.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupSchoolBus});
            this.ribbonPageSchoolBus.MergeOrder = 2;
            this.ribbonPageSchoolBus.Name = "ribbonPageSchoolBus";
            resources.ApplyResources(this.ribbonPageSchoolBus, "ribbonPageSchoolBus");
            // 
            // ribbonPageGroupSchoolBus
            // 
            this.ribbonPageGroupSchoolBus.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroupSchoolBus.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupSchoolBus.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupSchoolBus.ItemLinks.Add(this.btnRemove);
            this.ribbonPageGroupSchoolBus.Name = "ribbonPageGroupSchoolBus";
            resources.ApplyResources(this.ribbonPageGroupSchoolBus, "ribbonPageGroupSchoolBus");
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
            this.xtraScrollableControl1.Controls.Add(this.label5);
            this.xtraScrollableControl1.Controls.Add(this.txtDriverAssistantContactNo);
            this.xtraScrollableControl1.Controls.Add(this.cmbDriverAssistant);
            this.xtraScrollableControl1.Controls.Add(this.label6);
            this.xtraScrollableControl1.Controls.Add(this.label3);
            this.xtraScrollableControl1.Controls.Add(this.txtDriverContactNo);
            this.xtraScrollableControl1.Controls.Add(this.cmbDriverName);
            this.xtraScrollableControl1.Controls.Add(this.label2);
            this.xtraScrollableControl1.Controls.Add(this.label1);
            this.xtraScrollableControl1.Controls.Add(this.txtNumberPlate);
            this.xtraScrollableControl1.Controls.Add(this.txtCurrentBusNo);
            this.xtraScrollableControl1.Controls.Add(this.gridControlBusInformation);
            this.xtraScrollableControl1.Controls.Add(this.label4);
            this.xtraScrollableControl1.Controls.Add(this.txtBusNo);
            resources.ApplyResources(this.xtraScrollableControl1, "xtraScrollableControl1");
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtDriverAssistantContactNo
            // 
            resources.ApplyResources(this.txtDriverAssistantContactNo, "txtDriverAssistantContactNo");
            this.txtDriverAssistantContactNo.Name = "txtDriverAssistantContactNo";
            this.txtDriverAssistantContactNo.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("txtDriverAssistantContactNo.Properties.Mask.BeepOnError")));
            this.txtDriverAssistantContactNo.Properties.Mask.EditMask = resources.GetString("txtDriverAssistantContactNo.Properties.Mask.EditMask");
            this.txtDriverAssistantContactNo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtDriverAssistantContactNo.Properties.Mask.MaskType")));
            this.txtDriverAssistantContactNo.Properties.ReadOnly = true;
            this.txtDriverAssistantContactNo.Properties.ShowNullValuePrompt = ((DevExpress.XtraEditors.ShowNullValuePromptOptions)((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused | DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly)));
            // 
            // cmbDriverAssistant
            // 
            resources.ApplyResources(this.cmbDriverAssistant, "cmbDriverAssistant");
            this.cmbDriverAssistant.MenuManager = this.ribbonControl1;
            this.cmbDriverAssistant.Name = "cmbDriverAssistant";
            this.cmbDriverAssistant.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbDriverAssistant.Properties.Buttons"))))});
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtDriverContactNo
            // 
            resources.ApplyResources(this.txtDriverContactNo, "txtDriverContactNo");
            this.txtDriverContactNo.Name = "txtDriverContactNo";
            this.txtDriverContactNo.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("txtDriverContactNo.Properties.Mask.BeepOnError")));
            this.txtDriverContactNo.Properties.Mask.EditMask = resources.GetString("txtDriverContactNo.Properties.Mask.EditMask");
            this.txtDriverContactNo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtDriverContactNo.Properties.Mask.MaskType")));
            this.txtDriverContactNo.Properties.ReadOnly = true;
            this.txtDriverContactNo.Properties.ShowNullValuePrompt = ((DevExpress.XtraEditors.ShowNullValuePromptOptions)((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused | DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly)));
            // 
            // cmbDriverName
            // 
            resources.ApplyResources(this.cmbDriverName, "cmbDriverName");
            this.cmbDriverName.MenuManager = this.ribbonControl1;
            this.cmbDriverName.Name = "cmbDriverName";
            this.cmbDriverName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbDriverName.Properties.Buttons"))))});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtNumberPlate
            // 
            resources.ApplyResources(this.txtNumberPlate, "txtNumberPlate");
            this.txtNumberPlate.Name = "txtNumberPlate";
            this.txtNumberPlate.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("txtNumberPlate.Properties.Mask.BeepOnError")));
            this.txtNumberPlate.Properties.Mask.EditMask = resources.GetString("txtNumberPlate.Properties.Mask.EditMask");
            this.txtNumberPlate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtNumberPlate.Properties.Mask.MaskType")));
            this.txtNumberPlate.Properties.ShowNullValuePrompt = ((DevExpress.XtraEditors.ShowNullValuePromptOptions)((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused | DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly)));
            // 
            // txtCurrentBusNo
            // 
            resources.ApplyResources(this.txtCurrentBusNo, "txtCurrentBusNo");
            this.txtCurrentBusNo.Name = "txtCurrentBusNo";
            // 
            // gridControlBusInformation
            // 
            resources.ApplyResources(this.gridControlBusInformation, "gridControlBusInformation");
            this.gridControlBusInformation.MainView = this.gridView1;
            this.gridControlBusInformation.MenuManager = this.ribbonControl1;
            this.gridControlBusInformation.Name = "gridControlBusInformation";
            this.gridControlBusInformation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlBusInformation;
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
            // txtBusNo
            // 
            resources.ApplyResources(this.txtBusNo, "txtBusNo");
            this.txtBusNo.Name = "txtBusNo";
            this.txtBusNo.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("txtBusNo.Properties.Mask.BeepOnError")));
            this.txtBusNo.Properties.Mask.EditMask = resources.GetString("txtBusNo.Properties.Mask.EditMask");
            this.txtBusNo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtBusNo.Properties.Mask.MaskType")));
            this.txtBusNo.Properties.ShowNullValuePrompt = ((DevExpress.XtraEditors.ShowNullValuePromptOptions)((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused | DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly)));
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // userControlBusInformation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "userControlBusInformation";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDriverAssistantContactNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDriverAssistant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDriverContactNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDriverName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberPlate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBusInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusNo.Properties)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageSchoolBus;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSchoolBus;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private System.Windows.Forms.TextBox txtCurrentBusNo;
        private DevExpress.XtraGrid.GridControl gridControlBusInformation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        internal System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtBusNo;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtNumberPlate;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDriverName;
        internal System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit txtDriverAssistantContactNo;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDriverAssistant;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtDriverContactNo;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}

namespace EduXpress.Admin
{
    partial class userControlClassSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userControlClassSetting));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItemProcess = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.btnSaveClass = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageClassSettings = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupClassSettings = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSectionAbbreviation = new DevExpress.XtraEditors.TextEdit();
            this.btnNewSection = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtClassName = new DevExpress.XtraEditors.TextEdit();
            this.cbSection = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label13 = new System.Windows.Forms.Label();
            this.cbCycle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClass = new DevExpress.XtraEditors.TextEdit();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl));
            this.gridControlClasses = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.txtID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSectionAbbreviation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCycle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlClasses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.btnAdd,
            this.btnUpdate,
            this.btnSave,
            this.btnRemove,
            this.barStaticItemProcess,
            this.barStaticItem1,
            this.btnSaveClass});
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.MaxItemId = 10;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageClassSettings});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Id = 1;
            this.btnAdd.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_add;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAdd_ItemClick);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Id = 2;
            this.btnUpdate.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_refresh;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUpdate_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Id = 8;
            this.btnSave.Name = "btnSave";
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Enabled = false;
            this.btnRemove.Id = 4;
            this.btnRemove.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_removecircled;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRemove_ItemClick);
            // 
            // barStaticItemProcess
            // 
            resources.ApplyResources(this.barStaticItemProcess, "barStaticItemProcess");
            this.barStaticItemProcess.Id = 5;
            this.barStaticItemProcess.Name = "barStaticItemProcess";
            // 
            // barStaticItem1
            // 
            resources.ApplyResources(this.barStaticItem1, "barStaticItem1");
            this.barStaticItem1.Id = 7;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // btnSaveClass
            // 
            resources.ApplyResources(this.btnSaveClass, "btnSaveClass");
            this.btnSaveClass.Enabled = false;
            this.btnSaveClass.Id = 9;
            this.btnSaveClass.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.save;
            this.btnSaveClass.Name = "btnSaveClass";
            this.btnSaveClass.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveClass_ItemClick);
            // 
            // ribbonPageClassSettings
            // 
            this.ribbonPageClassSettings.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupClassSettings});
            this.ribbonPageClassSettings.MergeOrder = 2;
            this.ribbonPageClassSettings.Name = "ribbonPageClassSettings";
            resources.ApplyResources(this.ribbonPageClassSettings, "ribbonPageClassSettings");
            // 
            // ribbonPageGroupClassSettings
            // 
            this.ribbonPageGroupClassSettings.ItemLinks.Add(this.btnAdd);
            this.ribbonPageGroupClassSettings.ItemLinks.Add(this.btnSaveClass);
            this.ribbonPageGroupClassSettings.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupClassSettings.ItemLinks.Add(this.btnRemove);
            this.ribbonPageGroupClassSettings.Name = "ribbonPageGroupClassSettings";
            resources.ApplyResources(this.ribbonPageGroupClassSettings, "ribbonPageGroupClassSettings");
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.HideWhenMerging = DevExpress.Utils.DefaultBoolean.False;
            resources.ApplyResources(this.ribbonStatusBar1, "ribbonStatusBar1");
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtSectionAbbreviation
            // 
            resources.ApplyResources(this.txtSectionAbbreviation, "txtSectionAbbreviation");
            this.txtSectionAbbreviation.Name = "txtSectionAbbreviation";
            this.txtSectionAbbreviation.Properties.ReadOnly = true;
            // 
            // btnNewSection
            // 
            resources.ApplyResources(this.btnNewSection, "btnNewSection");
            this.btnNewSection.Name = "btnNewSection";
            this.btnNewSection.Click += new System.EventHandler(this.btnNewSection_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtClassName
            // 
            resources.ApplyResources(this.txtClassName, "txtClassName");
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Properties.ReadOnly = true;
            // 
            // cbSection
            // 
            resources.ApplyResources(this.cbSection, "cbSection");
            this.cbSection.MenuManager = this.ribbonControl1;
            this.cbSection.Name = "cbSection";
            this.cbSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cbSection.Properties.Buttons"))))});
            this.cbSection.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbSection.SelectedIndexChanged += new System.EventHandler(this.cbSection_SelectedIndexChanged);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // cbCycle
            // 
            resources.ApplyResources(this.cbCycle, "cbCycle");
            this.cbCycle.MenuManager = this.ribbonControl1;
            this.cbCycle.Name = "cbCycle";
            this.cbCycle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cbCycle.Properties.Buttons"))))});
            this.cbCycle.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbCycle.SelectedIndexChanged += new System.EventHandler(this.cbCycle_SelectedIndexChanged);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtClass
            // 
            resources.ApplyResources(this.txtClass, "txtClass");
            this.txtClass.Name = "txtClass";
            this.txtClass.TextChanged += new System.EventHandler(this.txtClass_TextChanged);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // gridControlClasses
            // 
            resources.ApplyResources(this.gridControlClasses, "gridControlClasses");
            this.gridControlClasses.MainView = this.gridView1;
            this.gridControlClasses.MenuManager = this.ribbonControl1;
            this.gridControlClasses.Name = "gridControlClasses";
            this.gridControlClasses.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlClasses.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControlClasses_MouseClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlClasses;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnEdit);
            this.panelControl1.Controls.Add(this.txtID);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.txtSectionAbbreviation);
            this.panelControl1.Controls.Add(this.txtClass);
            this.panelControl1.Controls.Add(this.btnNewSection);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label12);
            this.panelControl1.Controls.Add(this.txtClassName);
            this.panelControl1.Controls.Add(this.cbCycle);
            this.panelControl1.Controls.Add(this.cbSection);
            this.panelControl1.Controls.Add(this.label13);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            // 
            // userControlClassSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlClasses);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "userControlClassSetting";
            this.Load += new System.EventHandler(this.userControlClassSetting_Load);
            this.VisibleChanged += new System.EventHandler(this.userControlClassSetting_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSectionAbbreviation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCycle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlClasses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem btnAdd;
        private DevExpress.XtraBars.BarButtonItem btnUpdate;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnRemove;
        private DevExpress.XtraBars.BarStaticItem barStaticItemProcess;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem btnSaveClass;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageClassSettings;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupClassSettings;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        internal System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtSectionAbbreviation;
        private DevExpress.XtraEditors.SimpleButton btnNewSection;
        internal System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtClassName;
        private DevExpress.XtraEditors.ComboBoxEdit cbSection;
        internal System.Windows.Forms.Label label13;
        private DevExpress.XtraEditors.ComboBoxEdit cbCycle;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtClass;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraGrid.GridControl gridControlClasses;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal System.Windows.Forms.TextBox txtID;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
    }
}

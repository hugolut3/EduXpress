namespace EduXpress.Admin
{
    partial class userControlClassFeeEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userControlClassFeeEntry));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnRemove = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageClassFeeEntry = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupClassFeeEntry = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.btnEditFeeStructure = new DevExpress.XtraEditors.SimpleButton();
            this.groupSchoolFeeStructure = new DevExpress.XtraEditors.GroupControl();
            this.radioQuarterly = new System.Windows.Forms.RadioButton();
            this.radioPerMonth = new System.Windows.Forms.RadioButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtSearchByFeeName = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtSearchByClass = new DevExpress.XtraEditors.TextEdit();
            this.txtID = new System.Windows.Forms.TextBox();
            this.btnNewFee = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlFees = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmbMonth = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFeeName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbClass = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFee = new DevExpress.XtraEditors.TextEdit();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl));
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupSchoolFeeStructure)).BeginInit();
            this.groupSchoolFeeStructure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchByFeeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchByClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFeeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFee.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.btnNew,
            this.btnSave,
            this.btnUpdate,
            this.btnRemove});
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.MaxItemId = 6;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageClassFeeEntry});
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Id = 1;
            this.btnNew.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_add;
            this.btnNew.Name = "btnNew";
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Enabled = false;
            this.btnSave.Id = 2;
            this.btnSave.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.save;
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Id = 3;
            this.btnUpdate.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_refresh;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUpdate_ItemClick);
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
            // ribbonPageClassFeeEntry
            // 
            this.ribbonPageClassFeeEntry.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupClassFeeEntry});
            this.ribbonPageClassFeeEntry.MergeOrder = 2;
            this.ribbonPageClassFeeEntry.Name = "ribbonPageClassFeeEntry";
            resources.ApplyResources(this.ribbonPageClassFeeEntry, "ribbonPageClassFeeEntry");
            // 
            // ribbonPageGroupClassFeeEntry
            // 
            this.ribbonPageGroupClassFeeEntry.AllowTextClipping = false;
            this.ribbonPageGroupClassFeeEntry.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroupClassFeeEntry.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupClassFeeEntry.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupClassFeeEntry.ItemLinks.Add(this.btnRemove);
            this.ribbonPageGroupClassFeeEntry.Name = "ribbonPageGroupClassFeeEntry";
            resources.ApplyResources(this.ribbonPageGroupClassFeeEntry, "ribbonPageGroupClassFeeEntry");
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
            this.xtraScrollableControl1.Controls.Add(this.btnEditFeeStructure);
            this.xtraScrollableControl1.Controls.Add(this.groupSchoolFeeStructure);
            this.xtraScrollableControl1.Controls.Add(this.groupControl2);
            this.xtraScrollableControl1.Controls.Add(this.groupControl1);
            this.xtraScrollableControl1.Controls.Add(this.txtID);
            this.xtraScrollableControl1.Controls.Add(this.btnNewFee);
            this.xtraScrollableControl1.Controls.Add(this.gridControlFees);
            this.xtraScrollableControl1.Controls.Add(this.cmbMonth);
            this.xtraScrollableControl1.Controls.Add(this.label1);
            this.xtraScrollableControl1.Controls.Add(this.cmbFeeName);
            this.xtraScrollableControl1.Controls.Add(this.label13);
            this.xtraScrollableControl1.Controls.Add(this.cmbClass);
            this.xtraScrollableControl1.Controls.Add(this.label12);
            this.xtraScrollableControl1.Controls.Add(this.label4);
            this.xtraScrollableControl1.Controls.Add(this.txtFee);
            resources.ApplyResources(this.xtraScrollableControl1, "xtraScrollableControl1");
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            // 
            // btnEditFeeStructure
            // 
            resources.ApplyResources(this.btnEditFeeStructure, "btnEditFeeStructure");
            this.btnEditFeeStructure.Name = "btnEditFeeStructure";
            this.btnEditFeeStructure.Click += new System.EventHandler(this.btnEditFeeStructure_Click);
            // 
            // groupSchoolFeeStructure
            // 
            this.groupSchoolFeeStructure.Controls.Add(this.radioQuarterly);
            this.groupSchoolFeeStructure.Controls.Add(this.radioPerMonth);
            resources.ApplyResources(this.groupSchoolFeeStructure, "groupSchoolFeeStructure");
            this.groupSchoolFeeStructure.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupSchoolFeeStructure.Name = "groupSchoolFeeStructure";
            // 
            // radioQuarterly
            // 
            resources.ApplyResources(this.radioQuarterly, "radioQuarterly");
            this.radioQuarterly.Name = "radioQuarterly";
            this.radioQuarterly.TabStop = true;
            this.radioQuarterly.UseVisualStyleBackColor = true;
            this.radioQuarterly.CheckedChanged += new System.EventHandler(this.radioQuarterly_CheckedChanged);
            // 
            // radioPerMonth
            // 
            resources.ApplyResources(this.radioPerMonth, "radioPerMonth");
            this.radioPerMonth.Name = "radioPerMonth";
            this.radioPerMonth.TabStop = true;
            this.radioPerMonth.UseVisualStyleBackColor = true;
            this.radioPerMonth.CheckedChanged += new System.EventHandler(this.radioPerMonth_CheckedChanged);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtSearchByFeeName);
            this.groupControl2.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupControl2, "groupControl2");
            this.groupControl2.Name = "groupControl2";
            // 
            // txtSearchByFeeName
            // 
            resources.ApplyResources(this.txtSearchByFeeName, "txtSearchByFeeName");
            this.txtSearchByFeeName.Name = "txtSearchByFeeName";
            this.txtSearchByFeeName.TextChanged += new System.EventHandler(this.txtSearchByFeeName_TextChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtSearchByClass);
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Name = "groupControl1";
            // 
            // txtSearchByClass
            // 
            resources.ApplyResources(this.txtSearchByClass, "txtSearchByClass");
            this.txtSearchByClass.Name = "txtSearchByClass";
            this.txtSearchByClass.TextChanged += new System.EventHandler(this.txtSearchByClass_TextChanged);
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            // 
            // btnNewFee
            // 
            resources.ApplyResources(this.btnNewFee, "btnNewFee");
            this.btnNewFee.Name = "btnNewFee";
            this.btnNewFee.Click += new System.EventHandler(this.btnNewFee_Click);
            // 
            // gridControlFees
            // 
            resources.ApplyResources(this.gridControlFees, "gridControlFees");
            this.gridControlFees.MainView = this.gridView1;
            this.gridControlFees.Name = "gridControlFees";
            this.gridControlFees.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlFees.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControlFees_MouseClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlFees;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // cmbMonth
            // 
            resources.ApplyResources(this.cmbMonth, "cmbMonth");
            this.cmbMonth.MenuManager = this.ribbonControl1;
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbMonth.Properties.Buttons"))))});
            this.cmbMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbFeeName
            // 
            resources.ApplyResources(this.cmbFeeName, "cmbFeeName");
            this.cmbFeeName.MenuManager = this.ribbonControl1;
            this.cmbFeeName.Name = "cmbFeeName";
            this.cmbFeeName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbFeeName.Properties.Buttons"))))});
            this.cmbFeeName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFeeName.SelectedIndexChanged += new System.EventHandler(this.cmbFeeName_SelectedIndexChanged);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // cmbClass
            // 
            resources.ApplyResources(this.cmbClass, "cmbClass");
            this.cmbClass.MenuManager = this.ribbonControl1;
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbClass.Properties.Buttons"))))});
            this.cmbClass.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.cmbClass_SelectedIndexChanged);
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
            // txtFee
            // 
            resources.ApplyResources(this.txtFee, "txtFee");
            this.txtFee.Name = "txtFee";
            this.txtFee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFee_KeyPress);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // userControlClassFeeEntry
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "userControlClassFeeEntry";
            this.Load += new System.EventHandler(this.userControlClassFeeEntry_Load);
            this.VisibleChanged += new System.EventHandler(this.userControlClassFeeEntry_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupSchoolFeeStructure)).EndInit();
            this.groupSchoolFeeStructure.ResumeLayout(false);
            this.groupSchoolFeeStructure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchByFeeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchByClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFeeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFee.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnUpdate;
        private DevExpress.XtraBars.BarButtonItem btnRemove;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageClassFeeEntry;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupClassFeeEntry;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraGrid.GridControl gridControlFees;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbMonth;
        internal System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFeeName;
        internal System.Windows.Forms.Label label13;
        private DevExpress.XtraEditors.ComboBoxEdit cmbClass;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtFee;
        private DevExpress.XtraEditors.SimpleButton btnNewFee;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        internal System.Windows.Forms.TextBox txtID;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtSearchByClass;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TextEdit txtSearchByFeeName;
        private DevExpress.XtraEditors.SimpleButton btnEditFeeStructure;
        private DevExpress.XtraEditors.GroupControl groupSchoolFeeStructure;
        private System.Windows.Forms.RadioButton radioQuarterly;
        private System.Windows.Forms.RadioButton radioPerMonth;
    }
}

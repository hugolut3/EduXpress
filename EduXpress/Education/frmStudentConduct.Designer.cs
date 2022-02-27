namespace EduXpress.Education
{
    partial class frmStudentConduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStudentConduct));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.btnGetConducts = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageStudentConduct = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupStudentConduct = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupClose = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtTeacherUserType = new System.Windows.Forms.TextBox();
            this.txtTeacherID = new System.Windows.Forms.TextBox();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnLoadStudents = new DevExpress.XtraEditors.SimpleButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSchoolYear = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbAssessmentPeriod = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbSection = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbCycle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbClass = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlStudentConduct = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSchoolYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAssessmentPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCycle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStudentConduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
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
            this.btnDelete,
            this.btnClose,
            this.btnGetConducts});
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.MaxItemId = 7;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageStudentConduct});
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Id = 1;
            this.btnNew.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.bo_document;
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
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Enabled = false;
            this.btnDelete.Id = 4;
            this.btnDelete.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.delete;
            this.btnDelete.Name = "btnDelete";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Id = 5;
            this.btnClose.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            this.btnClose.Name = "btnClose";
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // btnGetConducts
            // 
            resources.ApplyResources(this.btnGetConducts, "btnGetConducts");
            this.btnGetConducts.Id = 6;
            this.btnGetConducts.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.datasource;
            this.btnGetConducts.Name = "btnGetConducts";
            this.btnGetConducts.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGetConducts_ItemClick);
            // 
            // ribbonPageStudentConduct
            // 
            this.ribbonPageStudentConduct.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupStudentConduct,
            this.ribbonPageGroup1,
            this.ribbonPageGroupClose});
            this.ribbonPageStudentConduct.Name = "ribbonPageStudentConduct";
            resources.ApplyResources(this.ribbonPageStudentConduct, "ribbonPageStudentConduct");
            // 
            // ribbonPageGroupStudentConduct
            // 
            this.ribbonPageGroupStudentConduct.AllowTextClipping = false;
            this.ribbonPageGroupStudentConduct.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroupStudentConduct.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupStudentConduct.Name = "ribbonPageGroupStudentConduct";
            resources.ApplyResources(this.ribbonPageGroupStudentConduct, "ribbonPageGroupStudentConduct");
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.ItemLinks.Add(this.btnGetConducts);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDelete);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            resources.ApplyResources(this.ribbonPageGroup1, "ribbonPageGroup1");
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
            this.panelControl1.Controls.Add(this.txtTeacherUserType);
            this.panelControl1.Controls.Add(this.txtTeacherID);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.txtClass);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.labelControl2);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // txtTeacherUserType
            // 
            resources.ApplyResources(this.txtTeacherUserType, "txtTeacherUserType");
            this.txtTeacherUserType.Name = "txtTeacherUserType";
            // 
            // txtTeacherID
            // 
            resources.ApplyResources(this.txtTeacherID, "txtTeacherID");
            this.txtTeacherID.Name = "txtTeacherID";
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl2.Controls.Add(this.btnLoadStudents);
            this.groupControl2.Controls.Add(this.label6);
            this.groupControl2.Controls.Add(this.cmbSchoolYear);
            this.groupControl2.Controls.Add(this.cmbAssessmentPeriod);
            this.groupControl2.Controls.Add(this.label14);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.label10);
            this.groupControl2.Controls.Add(this.cmbSection);
            this.groupControl2.Controls.Add(this.cmbCycle);
            this.groupControl2.Controls.Add(this.label9);
            this.groupControl2.Controls.Add(this.label5);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.cmbClass);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupControl2, "groupControl2");
            this.groupControl2.Name = "groupControl2";
            // 
            // btnLoadStudents
            // 
            this.btnLoadStudents.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnLoadStudents.Appearance.Font")));
            this.btnLoadStudents.Appearance.Options.UseFont = true;
            this.btnLoadStudents.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.reviewers;
            this.btnLoadStudents.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.btnLoadStudents, "btnLoadStudents");
            this.btnLoadStudents.Name = "btnLoadStudents";
            this.btnLoadStudents.Click += new System.EventHandler(this.btnLoadStudents_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Name = "label6";
            // 
            // cmbSchoolYear
            // 
            resources.ApplyResources(this.cmbSchoolYear, "cmbSchoolYear");
            this.cmbSchoolYear.Name = "cmbSchoolYear";
            this.cmbSchoolYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbSchoolYear.Properties.Buttons"))))});
            this.cmbSchoolYear.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            this.cmbSchoolYear.SelectedIndexChanged += new System.EventHandler(this.cmbSchoolYear_SelectedIndexChanged);
            // 
            // cmbAssessmentPeriod
            // 
            resources.ApplyResources(this.cmbAssessmentPeriod, "cmbAssessmentPeriod");
            this.cmbAssessmentPeriod.Name = "cmbAssessmentPeriod";
            this.cmbAssessmentPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbAssessmentPeriod.Properties.Buttons"))))});
            this.cmbAssessmentPeriod.SelectedIndexChanged += new System.EventHandler(this.cmbAssessmentPeriod_SelectedIndexChanged);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Name = "label14";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Name = "label1";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Name = "label10";
            // 
            // cmbSection
            // 
            resources.ApplyResources(this.cmbSection, "cmbSection");
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbSection.Properties.Buttons"))))});
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.cmbSection_SelectedIndexChanged);
            // 
            // cmbCycle
            // 
            resources.ApplyResources(this.cmbCycle, "cmbCycle");
            this.cmbCycle.Name = "cmbCycle";
            this.cmbCycle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbCycle.Properties.Buttons"))))});
            this.cmbCycle.SelectedIndexChanged += new System.EventHandler(this.cmbCycle_SelectedIndexChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Name = "label9";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Name = "label5";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Name = "label3";
            // 
            // cmbClass
            // 
            resources.ApplyResources(this.cmbClass, "cmbClass");
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbClass.Properties.Buttons"))))});
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.cmbClass_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Name = "label4";
            // 
            // txtClass
            // 
            resources.ApplyResources(this.txtClass, "txtClass");
            this.txtClass.Name = "txtClass";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Name = "label2";
            // 
            // labelControl2
            // 
            this.labelControl2.AllowHtmlString = true;
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // gridControlStudentConduct
            // 
            resources.ApplyResources(this.gridControlStudentConduct, "gridControlStudentConduct");
            this.gridControlStudentConduct.MainView = this.gridView1;
            this.gridControlStudentConduct.MenuManager = this.ribbonControl1;
            this.gridControlStudentConduct.Name = "gridControlStudentConduct";
            this.gridControlStudentConduct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlStudentConduct;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmStudentConduct
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlStudentConduct);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.IconOptions.SvgImage = global::EduXpress.Properties.Resources.employeequickwelcome;
            this.Name = "frmStudentConduct";
            this.Ribbon = this.ribbonControl1;
            this.StatusBar = this.ribbonStatusBar1;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmStudentConduct_Load);
            this.VisibleChanged += new System.EventHandler(this.frmStudentConduct_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSchoolYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAssessmentPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCycle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStudentConduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageStudentConduct;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupStudentConduct;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupClose;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.ComboBoxEdit cmbAssessmentPeriod;
        private DevExpress.XtraEditors.SimpleButton btnLoadStudents;
        public System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSchoolYear;
        public System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSection;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCycle;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.ComboBoxEdit cmbClass;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gridControlStudentConduct;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnUpdate;
        private DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnGetConducts;
        private System.Windows.Forms.TextBox txtTeacherUserType;
        private System.Windows.Forms.TextBox txtTeacherID;
    }
}
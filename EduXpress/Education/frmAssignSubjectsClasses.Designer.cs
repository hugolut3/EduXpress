namespace EduXpress.Education
{
    partial class frmAssignSubjectsClasses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssignSubjectsClasses));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageFile = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupConduct = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupClose = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnLoadSubjectsTeacher = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnLoadSubjectsClass = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridLookUpSubject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSection = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbClass = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCycle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSubjectPositionBulletin = new DevExpress.XtraEditors.TextEdit();
            this.cmbTeacher = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label25 = new System.Windows.Forms.Label();
            this.txtSubjectCode = new System.Windows.Forms.TextBox();
            this.cmbTeacherID = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.txtMaximaExam = new System.Windows.Forms.TextBox();
            this.txtMaximaPeriode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlAssignSubjects = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCycle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubjectPositionBulletin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTeacher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTeacherID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAssignSubjects)).BeginInit();
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
            this.btnClose});
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.MaxItemId = 7;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageFile});
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Id = 1;
            this.btnNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnNew.ImageOptions.SvgImage")));
            this.btnNew.Name = "btnNew";
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Enabled = false;
            this.btnSave.Id = 2;
            this.btnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSave.ImageOptions.SvgImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Id = 3;
            this.btnUpdate.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnUpdate.ImageOptions.SvgImage")));
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUpdate_ItemClick);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Enabled = false;
            this.btnDelete.Id = 4;
            this.btnDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDelete.ImageOptions.SvgImage")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Id = 5;
            this.btnClose.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnClose.ImageOptions.SvgImage")));
            this.btnClose.Name = "btnClose";
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // ribbonPageFile
            // 
            this.ribbonPageFile.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupConduct,
            this.ribbonPageGroupClose});
            this.ribbonPageFile.Name = "ribbonPageFile";
            resources.ApplyResources(this.ribbonPageFile, "ribbonPageFile");
            // 
            // ribbonPageGroupConduct
            // 
            this.ribbonPageGroupConduct.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroupConduct.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupConduct.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupConduct.ItemLinks.Add(this.btnDelete);
            this.ribbonPageGroupConduct.Name = "ribbonPageGroupConduct";
            resources.ApplyResources(this.ribbonPageGroupConduct, "ribbonPageGroupConduct");
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
            this.panelControl1.Controls.Add(this.groupControl3);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.txtSubjectCode);
            this.panelControl1.Controls.Add(this.cmbTeacherID);
            this.panelControl1.Controls.Add(this.txtClass);
            this.panelControl1.Controls.Add(this.txtMaximaExam);
            this.panelControl1.Controls.Add(this.txtMaximaPeriode);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.labelControl2);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl3.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl3.Controls.Add(this.btnLoadSubjectsTeacher);
            this.groupControl3.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupControl3, "groupControl3");
            this.groupControl3.Name = "groupControl3";
            // 
            // btnLoadSubjectsTeacher
            // 
            this.btnLoadSubjectsTeacher.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.reviewers;
            this.btnLoadSubjectsTeacher.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.btnLoadSubjectsTeacher, "btnLoadSubjectsTeacher");
            this.btnLoadSubjectsTeacher.Name = "btnLoadSubjectsTeacher";
            this.btnLoadSubjectsTeacher.Click += new System.EventHandler(this.btnLoadSubjectsTeacher_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl2.Controls.Add(this.btnLoadSubjectsClass);
            this.groupControl2.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupControl2, "groupControl2");
            this.groupControl2.Name = "groupControl2";
            // 
            // btnLoadSubjectsClass
            // 
            this.btnLoadSubjectsClass.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.bo_position;
            this.btnLoadSubjectsClass.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.btnLoadSubjectsClass, "btnLoadSubjectsClass");
            this.btnLoadSubjectsClass.Name = "btnLoadSubjectsClass";
            this.btnLoadSubjectsClass.Click += new System.EventHandler(this.btnLoadSubjectsClass_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridLookUpSubject);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.cmbSection);
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.cmbClass);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.cmbCycle);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.txtSubjectPositionBulletin);
            this.groupControl1.Controls.Add(this.cmbTeacher);
            this.groupControl1.Controls.Add(this.label25);
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Name = "groupControl1";
            // 
            // gridLookUpSubject
            // 
            resources.ApplyResources(this.gridLookUpSubject, "gridLookUpSubject");
            this.gridLookUpSubject.MenuManager = this.ribbonControl1;
            this.gridLookUpSubject.Name = "gridLookUpSubject";
            this.gridLookUpSubject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("gridLookUpSubject.Properties.Buttons"))))});
            this.gridLookUpSubject.Properties.PopupView = this.gridLookUpEdit1View;
            this.gridLookUpSubject.EditValueChanged += new System.EventHandler(this.gridLookUpSubject_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Name = "label1";
            // 
            // cmbSection
            // 
            resources.ApplyResources(this.cmbSection, "cmbSection");
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbSection.Properties.Buttons"))))});
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.cmbSection_SelectedIndexChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Name = "label9";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Name = "label3";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Name = "label8";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Name = "label4";
            // 
            // cmbClass
            // 
            resources.ApplyResources(this.cmbClass, "cmbClass");
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbClass.Properties.Buttons"))))});
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.cmbClass_SelectedIndexChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Name = "label5";
            // 
            // cmbCycle
            // 
            resources.ApplyResources(this.cmbCycle, "cmbCycle");
            this.cmbCycle.Name = "cmbCycle";
            this.cmbCycle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbCycle.Properties.Buttons"))))});
            this.cmbCycle.SelectedIndexChanged += new System.EventHandler(this.cmbCycle_SelectedIndexChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Name = "label6";
            // 
            // txtSubjectPositionBulletin
            // 
            resources.ApplyResources(this.txtSubjectPositionBulletin, "txtSubjectPositionBulletin");
            this.txtSubjectPositionBulletin.Name = "txtSubjectPositionBulletin";
            // 
            // cmbTeacher
            // 
            resources.ApplyResources(this.cmbTeacher, "cmbTeacher");
            this.cmbTeacher.Name = "cmbTeacher";
            this.cmbTeacher.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbTeacher.Properties.Buttons"))))});
            this.cmbTeacher.SelectedIndexChanged += new System.EventHandler(this.cmbTeacher_SelectedIndexChanged);
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // txtSubjectCode
            // 
            resources.ApplyResources(this.txtSubjectCode, "txtSubjectCode");
            this.txtSubjectCode.Name = "txtSubjectCode";
            // 
            // cmbTeacherID
            // 
            resources.ApplyResources(this.cmbTeacherID, "cmbTeacherID");
            this.cmbTeacherID.MenuManager = this.ribbonControl1;
            this.cmbTeacherID.Name = "cmbTeacherID";
            this.cmbTeacherID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbTeacherID.Properties.Buttons"))))});
            // 
            // txtClass
            // 
            resources.ApplyResources(this.txtClass, "txtClass");
            this.txtClass.Name = "txtClass";
            // 
            // txtMaximaExam
            // 
            resources.ApplyResources(this.txtMaximaExam, "txtMaximaExam");
            this.txtMaximaExam.Name = "txtMaximaExam";
            // 
            // txtMaximaPeriode
            // 
            resources.ApplyResources(this.txtMaximaPeriode, "txtMaximaPeriode");
            this.txtMaximaPeriode.Name = "txtMaximaPeriode";
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
            // gridControlAssignSubjects
            // 
            resources.ApplyResources(this.gridControlAssignSubjects, "gridControlAssignSubjects");
            this.gridControlAssignSubjects.MainView = this.gridView1;
            this.gridControlAssignSubjects.MenuManager = this.ribbonControl1;
            this.gridControlAssignSubjects.Name = "gridControlAssignSubjects";
            this.gridControlAssignSubjects.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlAssignSubjects.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControlAssignSubjects_MouseClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlAssignSubjects;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmAssignSubjectsClasses
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlAssignSubjects);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("frmAssignSubjectsClasses.IconOptions.SvgImage")));
            this.Name = "frmAssignSubjectsClasses";
            this.Ribbon = this.ribbonControl1;
            this.StatusBar = this.ribbonStatusBar1;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAssignSubjectsClasses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCycle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubjectPositionBulletin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTeacher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTeacherID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAssignSubjects)).EndInit();
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
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupConduct;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupClose;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gridControlAssignSubjects;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTeacher;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCycle;
        public System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.ComboBoxEdit cmbClass;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSection;
        private System.Windows.Forms.TextBox txtMaximaPeriode;
        private DevExpress.XtraEditors.TextEdit txtSubjectPositionBulletin;
        internal System.Windows.Forms.Label label25;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.GridLookUpEdit gridLookUpSubject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.TextBox txtMaximaExam;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTeacherID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSubjectCode;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnLoadSubjectsClass;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton btnLoadSubjectsTeacher;
    }
}
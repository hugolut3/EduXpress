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
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.txtMaximaExam = new System.Windows.Forms.TextBox();
            this.gridLookUpSubject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chkFirstSubjectMaxima = new DevExpress.XtraEditors.CheckEdit();
            this.txtMaximaPeriode = new System.Windows.Forms.TextBox();
            this.txtSearchSurname = new DevExpress.XtraEditors.TextEdit();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbTeacher = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCycle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbClass = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSection = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCurrentClass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridControlAssignSubjects = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFirstSubjectMaxima.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchSurname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTeacher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCycle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSection.Properties)).BeginInit();
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
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 6;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageFile});
            this.ribbonControl1.Size = new System.Drawing.Size(953, 158);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnNew
            // 
            this.btnNew.Caption = "New";
            this.btnNew.Id = 1;
            this.btnNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnNew.ImageOptions.SvgImage")));
            this.btnNew.Name = "btnNew";
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Save";
            this.btnSave.Enabled = false;
            this.btnSave.Id = 2;
            this.btnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSave.ImageOptions.SvgImage")));
            this.btnSave.Name = "btnSave";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Caption = "Update";
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Id = 3;
            this.btnUpdate.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnUpdate.ImageOptions.SvgImage")));
            this.btnUpdate.Name = "btnUpdate";
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "Delete";
            this.btnDelete.Enabled = false;
            this.btnDelete.Id = 4;
            this.btnDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDelete.ImageOptions.SvgImage")));
            this.btnDelete.Name = "btnDelete";
            // 
            // btnClose
            // 
            this.btnClose.Caption = "Close";
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
            this.ribbonPageFile.Text = "File";
            // 
            // ribbonPageGroupConduct
            // 
            this.ribbonPageGroupConduct.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroupConduct.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroupConduct.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupConduct.ItemLinks.Add(this.btnDelete);
            this.ribbonPageGroupConduct.Name = "ribbonPageGroupConduct";
            this.ribbonPageGroupConduct.Text = "Conduct";
            // 
            // ribbonPageGroupClose
            // 
            this.ribbonPageGroupClose.AllowTextClipping = false;
            this.ribbonPageGroupClose.ItemLinks.Add(this.btnClose, true);
            this.ribbonPageGroupClose.Name = "ribbonPageGroupClose";
            this.ribbonPageGroupClose.Text = "Close";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 552);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(953, 24);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnRemove);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.txtClass);
            this.panelControl1.Controls.Add(this.txtMaximaExam);
            this.panelControl1.Controls.Add(this.gridLookUpSubject);
            this.panelControl1.Controls.Add(this.chkFirstSubjectMaxima);
            this.panelControl1.Controls.Add(this.txtMaximaPeriode);
            this.panelControl1.Controls.Add(this.txtSearchSurname);
            this.panelControl1.Controls.Add(this.label25);
            this.panelControl1.Controls.Add(this.cmbTeacher);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.cmbCycle);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.cmbClass);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.cmbSection);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtCurrentClass);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 158);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(953, 164);
            this.panelControl1.TabIndex = 6;
            // 
            // btnRemove
            // 
            this.btnRemove.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_removecircled;
            this.btnRemove.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnRemove.Location = new System.Drawing.Point(455, 135);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 534;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_add;
            this.btnAdd.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnAdd.Location = new System.Drawing.Point(359, 135);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 533;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(821, 96);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(120, 22);
            this.txtClass.TabIndex = 532;
            // 
            // txtMaximaExam
            // 
            this.txtMaximaExam.Location = new System.Drawing.Point(821, 68);
            this.txtMaximaExam.Name = "txtMaximaExam";
            this.txtMaximaExam.Size = new System.Drawing.Size(120, 22);
            this.txtMaximaExam.TabIndex = 531;
            // 
            // gridLookUpSubject
            // 
            this.gridLookUpSubject.EditValue = "";
            this.gridLookUpSubject.Location = new System.Drawing.Point(111, 83);
            this.gridLookUpSubject.MenuManager = this.ribbonControl1;
            this.gridLookUpSubject.Name = "gridLookUpSubject";
            this.gridLookUpSubject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridLookUpSubject.Properties.PopupView = this.gridLookUpEdit1View;
            this.gridLookUpSubject.Size = new System.Drawing.Size(171, 20);
            this.gridLookUpSubject.TabIndex = 530;
            this.gridLookUpSubject.EditValueChanged += new System.EventHandler(this.gridLookUpSubject_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // chkFirstSubjectMaxima
            // 
            this.chkFirstSubjectMaxima.Enabled = false;
            this.chkFirstSubjectMaxima.Location = new System.Drawing.Point(587, 81);
            this.chkFirstSubjectMaxima.MenuManager = this.ribbonControl1;
            this.chkFirstSubjectMaxima.Name = "chkFirstSubjectMaxima";
            this.chkFirstSubjectMaxima.Properties.Caption = "First Subject on Maxima Group";
            this.chkFirstSubjectMaxima.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkFirstSubjectMaxima.Size = new System.Drawing.Size(190, 20);
            this.chkFirstSubjectMaxima.TabIndex = 529;
            // 
            // txtMaximaPeriode
            // 
            this.txtMaximaPeriode.Location = new System.Drawing.Point(821, 37);
            this.txtMaximaPeriode.Name = "txtMaximaPeriode";
            this.txtMaximaPeriode.Size = new System.Drawing.Size(120, 22);
            this.txtMaximaPeriode.TabIndex = 528;
            // 
            // txtSearchSurname
            // 
            this.txtSearchSurname.Enabled = false;
            this.txtSearchSurname.Location = new System.Drawing.Point(737, 132);
            this.txtSearchSurname.Name = "txtSearchSurname";
            this.txtSearchSurname.Size = new System.Drawing.Size(43, 20);
            this.txtSearchSurname.TabIndex = 527;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.label25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label25.Location = new System.Drawing.Point(548, 135);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(183, 15);
            this.label25.TabIndex = 526;
            this.label25.Text = "Subject Position on Report Card:";
            // 
            // cmbTeacher
            // 
            this.cmbTeacher.Enabled = false;
            this.cmbTeacher.Location = new System.Drawing.Point(359, 81);
            this.cmbTeacher.Name = "cmbTeacher";
            this.cmbTeacher.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTeacher.Size = new System.Drawing.Size(171, 20);
            this.cmbTeacher.TabIndex = 525;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(301, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 524;
            this.label6.Text = "Teacher:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(55, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 15);
            this.label7.TabIndex = 522;
            this.label7.Text = "Subject:";
            // 
            // cmbCycle
            // 
            this.cmbCycle.Enabled = false;
            this.cmbCycle.Location = new System.Drawing.Point(111, 38);
            this.cmbCycle.Name = "cmbCycle";
            this.cmbCycle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCycle.Size = new System.Drawing.Size(171, 20);
            this.cmbCycle.TabIndex = 521;
            this.cmbCycle.SelectedIndexChanged += new System.EventHandler(this.cmbCycle_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(10, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 15);
            this.label5.TabIndex = 520;
            this.label5.Text = "Education Band:";
            // 
            // cmbClass
            // 
            this.cmbClass.Enabled = false;
            this.cmbClass.Location = new System.Drawing.Point(606, 40);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbClass.Size = new System.Drawing.Size(171, 20);
            this.cmbClass.TabIndex = 519;
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.cmbClass_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(557, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 15);
            this.label4.TabIndex = 518;
            this.label4.Text = "Grade:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(316, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 516;
            this.label3.Text = "Field:";
            // 
            // cmbSection
            // 
            this.cmbSection.Enabled = false;
            this.cmbSection.Location = new System.Drawing.Point(359, 40);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSection.Size = new System.Drawing.Size(171, 20);
            this.cmbSection.TabIndex = 517;
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.cmbSection_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(780, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 509;
            // 
            // labelControl2
            // 
            this.labelControl2.AllowHtmlString = true;
            this.labelControl2.Location = new System.Drawing.Point(240, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(177, 13);
            this.labelControl2.TabIndex = 506;
            this.labelControl2.Text = "Fields with an <color=255, 0, 0>*</color> are required fields";
            // 
            // txtCurrentClass
            // 
            this.txtCurrentClass.Location = new System.Drawing.Point(842, 9);
            this.txtCurrentClass.Name = "txtCurrentClass";
            this.txtCurrentClass.Size = new System.Drawing.Size(66, 22);
            this.txtCurrentClass.TabIndex = 473;
            this.txtCurrentClass.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(783, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 15);
            this.label1.TabIndex = 454;
            this.label1.Text = "*";
            // 
            // gridControlAssignSubjects
            // 
            this.gridControlAssignSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlAssignSubjects.Location = new System.Drawing.Point(0, 322);
            this.gridControlAssignSubjects.MainView = this.gridView1;
            this.gridControlAssignSubjects.MenuManager = this.ribbonControl1;
            this.gridControlAssignSubjects.Name = "gridControlAssignSubjects";
            this.gridControlAssignSubjects.Size = new System.Drawing.Size(953, 230);
            this.gridControlAssignSubjects.TabIndex = 9;
            this.gridControlAssignSubjects.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlAssignSubjects;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmAssignSubjectsClasses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 576);
            this.Controls.Add(this.gridControlAssignSubjects);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("frmAssignSubjectsClasses.IconOptions.SvgImage")));
            this.Name = "frmAssignSubjectsClasses";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "Assign Subjects to Classes";
            this.Load += new System.EventHandler(this.frmAssignSubjectsClasses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFirstSubjectMaxima.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchSurname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTeacher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCycle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSection.Properties)).EndInit();
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
        private System.Windows.Forms.TextBox txtCurrentClass;
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
        private DevExpress.XtraEditors.TextEdit txtSearchSurname;
        internal System.Windows.Forms.Label label25;
        private DevExpress.XtraEditors.CheckEdit chkFirstSubjectMaxima;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.GridLookUpEdit gridLookUpSubject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.TextBox txtMaximaExam;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
    }
}
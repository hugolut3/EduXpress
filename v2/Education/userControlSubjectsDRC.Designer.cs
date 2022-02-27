namespace EduXpress.Education
{
    partial class userControlSubjectsDRC
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
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItemProcess = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.btnSaveSubject = new DevExpress.XtraBars.BarButtonItem();
            this.btnApplication = new DevExpress.XtraBars.BarButtonItem();
            this.btnConduct = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageClassSettings = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupSubjects = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupConductApplication = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaximaExam = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddClassLevel = new DevExpress.XtraEditors.SimpleButton();
            this.txtMaximaPeriod = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSubjectName = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSubjectCode = new DevExpress.XtraEditors.TextEdit();
            this.cmbClass = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridControlSubjects = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl));
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaximaExam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaximaPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubjectName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubjectCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSubjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
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
            this.btnRemove,
            this.barStaticItemProcess,
            this.barStaticItem1,
            this.btnSaveSubject,
            this.btnApplication,
            this.btnConduct});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 12;
            this.ribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageClassSettings});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            this.ribbonControl1.Size = new System.Drawing.Size(805, 150);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // btnAdd
            // 
            this.btnAdd.Caption = "Add";
            this.btnAdd.Id = 1;
            this.btnAdd.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_add;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAdd_ItemClick);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Caption = "Update";
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Id = 2;
            this.btnUpdate.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_refresh;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUpdate_ItemClick);
            // 
            // btnRemove
            // 
            this.btnRemove.Caption = "Remove";
            this.btnRemove.Enabled = false;
            this.btnRemove.Id = 4;
            this.btnRemove.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.actions_removecircled;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRemove_ItemClick);
            // 
            // barStaticItemProcess
            // 
            this.barStaticItemProcess.Caption = "Ready!";
            this.barStaticItemProcess.Id = 5;
            this.barStaticItemProcess.Name = "barStaticItemProcess";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "barStaticItem1";
            this.barStaticItem1.Id = 7;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // btnSaveSubject
            // 
            this.btnSaveSubject.Caption = "Save";
            this.btnSaveSubject.Enabled = false;
            this.btnSaveSubject.Id = 9;
            this.btnSaveSubject.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.save;
            this.btnSaveSubject.Name = "btnSaveSubject";
            this.btnSaveSubject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveSubject_ItemClick);
            // 
            // btnApplication
            // 
            this.btnApplication.Caption = "Achievement Level";
            this.btnApplication.Id = 10;
            this.btnApplication.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.employeequickaward;
            this.btnApplication.Name = "btnApplication";
            this.btnApplication.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnApplication_ItemClick);
            // 
            // btnConduct
            // 
            this.btnConduct.Caption = "Conduct";
            this.btnConduct.Id = 11;
            this.btnConduct.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.employeequickwelcome;
            this.btnConduct.Name = "btnConduct";
            this.btnConduct.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnConduct_ItemClick);
            // 
            // ribbonPageClassSettings
            // 
            this.ribbonPageClassSettings.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupSubjects,
            this.ribbonPageGroupConductApplication});
            this.ribbonPageClassSettings.MergeOrder = 2;
            this.ribbonPageClassSettings.Name = "ribbonPageClassSettings";
            this.ribbonPageClassSettings.Text = "School Subjects";
            // 
            // ribbonPageGroupSubjects
            // 
            this.ribbonPageGroupSubjects.ItemLinks.Add(this.btnAdd);
            this.ribbonPageGroupSubjects.ItemLinks.Add(this.btnSaveSubject);
            this.ribbonPageGroupSubjects.ItemLinks.Add(this.btnUpdate);
            this.ribbonPageGroupSubjects.ItemLinks.Add(this.btnRemove);
            this.ribbonPageGroupSubjects.Name = "ribbonPageGroupSubjects";
            this.ribbonPageGroupSubjects.Text = "School Subjects";
            // 
            // ribbonPageGroupConductApplication
            // 
            this.ribbonPageGroupConductApplication.AllowTextClipping = false;
            this.ribbonPageGroupConductApplication.ItemLinks.Add(this.btnApplication);
            this.ribbonPageGroupConductApplication.ItemLinks.Add(this.btnConduct);
            this.ribbonPageGroupConductApplication.Name = "ribbonPageGroupConductApplication";
            this.ribbonPageGroupConductApplication.Text = "Conduct and Achievement Level";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.HideWhenMerging = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 516);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(805, 27);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.label9);
            this.panelControl1.Controls.Add(this.label8);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtMaximaExam);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.btnAddClassLevel);
            this.panelControl1.Controls.Add(this.txtMaximaPeriod);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.btnEdit);
            this.panelControl1.Controls.Add(this.txtID);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.txtSubjectName);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label12);
            this.panelControl1.Controls.Add(this.txtSubjectCode);
            this.panelControl1.Controls.Add(this.cmbClass);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 150);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(805, 189);
            this.panelControl1.TabIndex = 487;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(362, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 15);
            this.label9.TabIndex = 510;
            this.label9.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(362, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 15);
            this.label8.TabIndex = 509;
            this.label8.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(362, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 15);
            this.label7.TabIndex = 508;
            this.label7.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(362, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 15);
            this.label6.TabIndex = 507;
            this.label6.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(362, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 15);
            this.label5.TabIndex = 506;
            this.label5.Text = "*";
            // 
            // labelControl1
            // 
            this.labelControl1.AllowHtmlString = true;
            this.labelControl1.Location = new System.Drawing.Point(314, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(177, 13);
            this.labelControl1.TabIndex = 505;
            this.labelControl1.Text = "Fields with an <color=255, 0, 0>*</color> are required fields";
            // 
            // txtMaximaExam
            // 
            this.txtMaximaExam.Enabled = false;
            this.txtMaximaExam.Location = new System.Drawing.Point(145, 140);
            this.txtMaximaExam.Name = "txtMaximaExam";
            this.txtMaximaExam.Properties.BeepOnError = true;
            this.txtMaximaExam.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtMaximaExam.Properties.MaskSettings.Set("mask", "d");
            this.txtMaximaExam.Size = new System.Drawing.Size(211, 20);
            this.txtMaximaExam.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(54, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 504;
            this.label2.Text = "Maxima Exam:";
            // 
            // btnAddClassLevel
            // 
            this.btnAddClassLevel.Location = new System.Drawing.Point(381, 57);
            this.btnAddClassLevel.Name = "btnAddClassLevel";
            this.btnAddClassLevel.Size = new System.Drawing.Size(60, 20);
            this.btnAddClassLevel.TabIndex = 502;
            this.btnAddClassLevel.Text = "Add";
            this.btnAddClassLevel.Click += new System.EventHandler(this.btnAddClassLevel_Click);
            // 
            // txtMaximaPeriod
            // 
            this.txtMaximaPeriod.Enabled = false;
            this.txtMaximaPeriod.Location = new System.Drawing.Point(145, 114);
            this.txtMaximaPeriod.Name = "txtMaximaPeriod";
            this.txtMaximaPeriod.Properties.BeepOnError = true;
            this.txtMaximaPeriod.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtMaximaPeriod.Properties.MaskSettings.Set("mask", "d");
            this.txtMaximaPeriod.Size = new System.Drawing.Size(211, 20);
            this.txtMaximaPeriod.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(49, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 501;
            this.label1.Text = "Maxima Period:";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(381, 28);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(60, 20);
            this.btnEdit.TabIndex = 497;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(466, 26);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(49, 22);
            this.txtID.TabIndex = 496;
            this.txtID.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(59, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 15);
            this.label3.TabIndex = 481;
            this.label3.Text = "Subject Code:";
            // 
            // txtSubjectName
            // 
            this.txtSubjectName.Enabled = false;
            this.txtSubjectName.Location = new System.Drawing.Point(145, 80);
            this.txtSubjectName.Name = "txtSubjectName";
            this.txtSubjectName.Size = new System.Drawing.Size(211, 20);
            this.txtSubjectName.TabIndex = 1;
            this.txtSubjectName.TextChanged += new System.EventHandler(this.txtSubjectName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(68, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.TabIndex = 463;
            this.label4.Text = "Grade Level:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(54, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 15);
            this.label12.TabIndex = 467;
            this.label12.Text = "Subject Name:";
            // 
            // txtSubjectCode
            // 
            this.txtSubjectCode.Location = new System.Drawing.Point(145, 28);
            this.txtSubjectCode.Name = "txtSubjectCode";
            this.txtSubjectCode.Properties.ReadOnly = true;
            this.txtSubjectCode.Size = new System.Drawing.Size(211, 20);
            this.txtSubjectCode.TabIndex = 0;
            // 
            // cmbClass
            // 
            this.cmbClass.Enabled = false;
            this.cmbClass.Location = new System.Drawing.Point(145, 54);
            this.cmbClass.MenuManager = this.ribbonControl1;
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbClass.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbClass.Size = new System.Drawing.Size(211, 20);
            this.cmbClass.TabIndex = 0;
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.cmbClass_SelectedIndexChanged);
            // 
            // gridControlSubjects
            // 
            this.gridControlSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSubjects.Location = new System.Drawing.Point(0, 339);
            this.gridControlSubjects.MainView = this.gridView1;
            this.gridControlSubjects.MenuManager = this.ribbonControl1;
            this.gridControlSubjects.Name = "gridControlSubjects";
            this.gridControlSubjects.Size = new System.Drawing.Size(805, 177);
            this.gridControlSubjects.TabIndex = 488;
            this.gridControlSubjects.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlSubjects.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridControlSubjects_MouseClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlSubjects;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // userControlSubjectsDRC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlSubjects);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "userControlSubjectsDRC";
            this.Size = new System.Drawing.Size(805, 543);
            this.Load += new System.EventHandler(this.userControlSubjects_Load);
            this.VisibleChanged += new System.EventHandler(this.userControlSubjects_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaximaExam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaximaPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubjectName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubjectCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSubjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem btnAdd;
        private DevExpress.XtraBars.BarButtonItem btnUpdate;
        
        private DevExpress.XtraBars.BarButtonItem btnRemove;
        private DevExpress.XtraBars.BarStaticItem barStaticItemProcess;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem btnSaveSubject;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageClassSettings;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSubjects;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        internal System.Windows.Forms.TextBox txtID;
        internal System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtSubjectName;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.ComboBoxEdit cmbClass;
        private DevExpress.XtraGrid.GridControl gridControlSubjects;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraEditors.TextEdit txtSubjectCode;
        private DevExpress.XtraEditors.TextEdit txtMaximaPeriod;
        internal System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnAddClassLevel;
        private DevExpress.XtraEditors.TextEdit txtMaximaExam;
        internal System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraBars.BarButtonItem btnApplication;
        private DevExpress.XtraBars.BarButtonItem btnConduct;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupConductApplication;
    }
}
